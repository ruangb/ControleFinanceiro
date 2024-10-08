﻿using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.WebSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.WebSite.Controllers
{
    public class BillController : BaseController
    {
        private readonly IBillAppService _billAppService;
        private readonly IExpenseInstallmentAppService _expenseInstallmentAppService;
        private readonly IBaseAppService<PersonDTO> _personAppService;
        private readonly IBaseAppService<CreditCardDTO> _creditCardAppService;
        private readonly IMapper _mapper;

        public BillController(IBillAppService billAppService, 
                              IExpenseInstallmentAppService expenseInstallmentAppService, 
                              IBaseAppService<PersonDTO> personAppService, 
                              IBaseAppService<CreditCardDTO> creditCardAppService,
                              IMapper mapper)
        {
            _billAppService = billAppService;
            _expenseInstallmentAppService = expenseInstallmentAppService;
            _personAppService = personAppService;
            _creditCardAppService = creditCardAppService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            DateTime initialDate = DateTime.Now.AddMonths(-2);
            initialDate = new DateTime(initialDate.Year, initialDate.Month, 01);

            AppServiceResult<IEnumerable<BillDTO>> result = _billAppService.GetAllBills(new BillDTO(initialDate));

            if (!result.Success) return RedirectToError(result.Message);

            var model = _mapper.Map<IList<BillViewModel>>(result.Model);

            ViewBag.OnlyThirds = false;
            ViewBag.Persons = BuildPersonSelectListItem(_mapper, _personAppService, "Todos");
            ViewBag.CreditCards = BuildCreditCardSelectListItem(_mapper, _creditCardAppService, "Todos");

            model.FirstOrDefault().StartDueDate = initialDate;

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(BillViewModel filter)
        {
            BillDTO dto = _mapper.Map<BillDTO>(filter);

            AppServiceResult<IEnumerable<BillDTO>> result = _billAppService.GetAllBills(dto);

            if (!result.Success) return RedirectToError(result.Message);

            var model = _mapper.Map<IList<BillViewModel>>(result.Model);

            ViewBag.OnlyThirds = filter.OnlyThirds;
            ViewBag.Persons = BuildPersonSelectListItem(_mapper, _personAppService, "Todos");
            ViewBag.CreditCards = BuildCreditCardSelectListItem(_mapper, _creditCardAppService, "Todos");

            return View("Index", model);
        }

        [HttpGet]
        [Route("bill/list-items")]
        public IActionResult GetExpenseInstallmentsByBill(BillViewModel filter)
        {
            BillDTO dto = _mapper.Map<BillDTO>(filter);

            AppServiceResult<IEnumerable<ExpenseInstallmentDTO>> result = _expenseInstallmentAppService.GetAllExpenseInstallmentsByBill(dto);

            if (!result.Success) return RedirectToErrorJson(result.Message);

            var model = _mapper.Map<IList<ExpenseInstallmentViewModel>>(result.Model);

            string detailsHtml = this.RenderViewAsync("_Details", model.OrderBy(x => x.Expense.OperationDate), true).Result;

            return Json(new JsonResultViewModel(true, detailsHtml));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BillViewModel viewModel)
        {
            var dto = _mapper.Map<BillDTO>(viewModel);
            AppServiceResult<int> result = _billAppService.Insert(dto);

            if (!result.Success) return RedirectToError(result.Message);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToError("Id não fornecido.");

            AppServiceResult<BillDTO> result = _billAppService.GetById(id.Value);

            if (!result.Success) return RedirectToError(result.Message);

            var viewModel = _mapper.Map<BillViewModel>(result.Model);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(BillViewModel viewModel)
        {
            var dto = _mapper.Map<BillDTO>(viewModel);

            AppServiceBaseResult result = _billAppService.Update(dto);

            if (!result.Success) return RedirectToError(result.Message);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToError("Id não fornecido.");

            AppServiceResult<BillDTO> result = _billAppService.GetById(id.Value);

            if (!result.Success) return RedirectToError(result.Message);

            var viewModel = _mapper.Map<BillViewModel>(result.Model);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            AppServiceBaseResult result = _billAppService.Delete(id);

            if (!result.Success) return RedirectToError(result.Message);

            return RedirectToAction(nameof(Index));
        }

        static string RenderViewToString(System.Web.Mvc.ControllerContext context,
        string viewPath, object model = null, bool partial = false)
        {
            // first find the ViewEngine for this view
            System.Web.Mvc.ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(context, viewPath);
            else
                viewEngineResult = System.Web.Mvc.ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException($"A Página {viewPath} não foi localizada.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new System.Web.Mvc.ViewContext(context, view, context.Controller.ViewData, context.Controller.TempData, sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }
    }
}
