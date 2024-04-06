using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleFinanceiro.WebSite.Controllers
{
    public class ExpenseInstallmentController : BaseController
    {
        private readonly IExpenseInstallmentAppService _expenseInstallmentAppService;
        private readonly IBaseAppService<CreditCardDTO> _creditCardAppService;
        private readonly IBaseAppService<PersonDTO> _personAppService;
        private readonly IMapper _mapper;

        public ExpenseInstallmentController(
            IExpenseInstallmentAppService expenseInstallmentAppService, 
            IBaseAppService<CreditCardDTO> creditCardAppService, 
            IBaseAppService<PersonDTO> personAppService, 
            IMapper mapper)
        {
            _expenseInstallmentAppService = expenseInstallmentAppService;
            _creditCardAppService = creditCardAppService;
            _personAppService = personAppService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            AppServiceResult<IEnumerable<ExpenseInstallmentDTO>> result = _expenseInstallmentAppService.GetAll();

            if (!result.Success) return RedirectToError(result.Message);

            var viewModel = _mapper.Map<IList<ExpenseInstallmentViewModel>>(result.Model);

            return View(viewModel.OrderBy(x => x.DueDate));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToError("Id não fornecido.");

            AppServiceResult<ExpenseInstallmentDTO> result = _expenseInstallmentAppService.GetById(id.Value);

            if (!result.Success) return RedirectToError(result.Message);

            var viewModel = _mapper.Map<ExpenseInstallmentViewModel>(result.Model);

            //ViewBag.CreditCards = BuildCreditCardSelectListItem(viewModel.IdCreditCard);
            //ViewBag.Persons = BuildPersonSelectListItem(viewModel.IdPerson);
            //ViewBag.Status = new SelectList(Enums.GetDescriptions<Enums.ExpenseInstallmentStatus>(), viewModel.Status);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ExpenseInstallmentViewModel viewModel)
        {
            var dto = _mapper.Map<ExpenseInstallmentDTO>(viewModel);

            AppServiceBaseResult result = _expenseInstallmentAppService.Update(dto);

            if (!result.Success) return RedirectToError(result.Message);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToError("Id não fornecido.");

            AppServiceResult<ExpenseInstallmentDTO> result = _expenseInstallmentAppService.GetById(id.Value);

            if (!result.Success) return RedirectToError(result.Message);

            var viewModel = _mapper.Map<ExpenseInstallmentViewModel>(result.Model);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            AppServiceBaseResult result = _expenseInstallmentAppService.Delete(id);

            if (!result.Success) return RedirectToError(result.Message);

            return RedirectToAction(nameof(Index));
        }

        private List<SelectListItem> BuildCreditCardSelectListItem(int? id = null)
        {
            List<SelectListItem> listItem = [];

            AppServiceResult<IEnumerable<CreditCardDTO>> creditCardsDTO = _creditCardAppService.GetAll();
            var creditCards = _mapper.Map<IList<CreditCardViewModel>>(creditCardsDTO.Model);

            foreach (var item in creditCards)
            {
                listItem.Add(new SelectListItem(item.Name, item.Id.ToString(), item.Id == id));
            }

            listItem.Insert(0, new SelectListItem("Selecione", null, id == null));

            return listItem;
        }

        private List<SelectListItem> BuildPersonSelectListItem(int? id = null)
        {
            List<SelectListItem> listItem = [];
            AppServiceResult<IEnumerable<PersonDTO>> personsDTO = _personAppService.GetAll();
            var persons = _mapper.Map<IList<PersonViewModel>>(personsDTO.Model);

            foreach (var item in persons)
            {
                listItem.Add(new SelectListItem(item.Name, item.Id.ToString(), item.Id == id));
            }

            listItem.Insert(0, new SelectListItem("Selecione", null, id == null));

            return listItem;
        }
    }
}
