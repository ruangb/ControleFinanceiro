﻿using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.CrossCutting.Utilities;
using ControleFinanceiro.WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleFinanceiro.WebSite.Controllers
{
    public class ExpenseController : BaseController
    {
        private readonly IBaseAppService<ExpenseDTO> _expenseAppService;
        private readonly IBaseAppService<CreditCardDTO> _creditCardAppService;
        private readonly IBaseAppService<PersonDTO> _personAppService;
        private readonly IMapper _mapper;

        public ExpenseController(
            IBaseAppService<ExpenseDTO> expenseAppService, 
            IBaseAppService<CreditCardDTO> creditCardAppService, 
            IBaseAppService<PersonDTO> personAppService, 
            IMapper mapper)
        {
            _expenseAppService = expenseAppService;
            _creditCardAppService = creditCardAppService;
            _personAppService = personAppService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            AppServiceResult<IEnumerable<ExpenseDTO>> result = _expenseAppService.GetAll();

            if (!result.Success) return RedirectToError(result.Message);

            var viewModel = _mapper.Map<IList<ExpenseViewModel>>(result.Model);

            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewBag.CreditCards = BuildCreditCardSelectListItem(_mapper, _creditCardAppService, "Selecione");
            ViewBag.Persons = BuildPersonSelectListItem(_mapper, _personAppService, "Selecione");
            ViewBag.Status = new SelectList(Enums.GetDescriptions<Enums.ExpenseStatus>());

            return View();
        }

        [HttpPost]
        public IActionResult Create(ExpenseViewModel viewModel)
        {
            var dto = _mapper.Map<ExpenseDTO>(viewModel);
            AppServiceResult<int> result = _expenseAppService.Insert(dto);

            if (!result.Success) return RedirectToError(result.Message);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToError("Id não fornecido.");

            AppServiceResult<ExpenseDTO> result = _expenseAppService.GetById(id.Value);

            if (!result.Success) return RedirectToError(result.Message);

            var installment = _mapper.Map<List<ExpenseInstallmentViewModel>>((result.Model as ExpenseDTO).ExpenseInstallments);
            var viewModel = _mapper.Map<ExpenseViewModel>(result.Model);

            viewModel.ExpenseIntallments = installment;

            ViewBag.CreditCards = BuildCreditCardSelectListItem(_mapper, _creditCardAppService, "Selecione", viewModel.IdCreditCard);
            ViewBag.Persons = BuildPersonSelectListItem(_mapper, _personAppService, "Selecione", viewModel.IdPerson);
            ViewBag.Status = new SelectList(Enums.GetDescriptions<Enums.ExpenseStatus>(), viewModel.Status);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ExpenseViewModel viewModel)
        {
            var dto = _mapper.Map<ExpenseDTO>(viewModel);

            AppServiceBaseResult result = _expenseAppService.Update(dto);

            if (!result.Success) return RedirectToError(result.Message);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return RedirectToError("Id não fornecido.");

            AppServiceResult<ExpenseDTO> result = _expenseAppService.GetById(id.Value);

            if (!result.Success) return RedirectToError(result.Message);

            var viewModel = _mapper.Map<ExpenseViewModel>(result.Model);

            return View(viewModel);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToError("Id não fornecido.");

            AppServiceResult<ExpenseDTO> result = _expenseAppService.GetById(id.Value);

            if (!result.Success) return RedirectToError(result.Message);

            var viewModel = _mapper.Map<ExpenseViewModel>(result.Model);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            AppServiceBaseResult result = _expenseAppService.Delete(id);

            if (!result.Success) return RedirectToError(result.Message);

            return RedirectToAction(nameof(Index));
        }

        //protected List<SelectListItem> BuildCreditCardSelectListItem(int? id = null)
        //{
        //    List<SelectListItem> listItem = [];

        //    AppServiceResult<IEnumerable<CreditCardDTO>> creditCardsDTO = _creditCardAppService.GetAll();
        //    var creditCards = _mapper.Map<IList<CreditCardViewModel>>(creditCardsDTO.Model);

        //    foreach (var item in creditCards)
        //    {
        //        listItem.Add(new SelectListItem(item.Name, item.Id.ToString(), item.Id == id));
        //    }

        //    listItem.Insert(0, new SelectListItem("Selecione", null, id == null));
        //    return listItem;
        //}

        //protected List<SelectListItem> BuildPersonSelectListItem(int? id = null)
        //{
        //    List<SelectListItem> listItem = [];
        //    AppServiceResult<IEnumerable<PersonDTO>> personsDTO = _personAppService.GetAll();
        //    var persons = _mapper.Map<IList<PersonViewModel>>(personsDTO.Model);

        //    foreach (var item in persons)
        //    {
        //        listItem.Add(new SelectListItem(item.Name, item.Id.ToString(), item.Id == id));
        //    }

        //    listItem.Insert(0, new SelectListItem("Selecione", null, id == null));

        //    return listItem;
        //}
    }
}
