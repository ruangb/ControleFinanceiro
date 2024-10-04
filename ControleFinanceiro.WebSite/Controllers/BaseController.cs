using ControleFinanceiro.Application.Implementation;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.CrossCutting;
using ControleFinanceiro.WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using ControleFinanceiro.Application.Interfaces;

namespace ControleFinanceiro.WebSite.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }

        protected RedirectToActionResult RedirectToError(string message)
        {
            return RedirectToAction("Error", "Base", new { message });
        }

        protected JsonResult RedirectToErrorJson(string message)
        {
            return Json(new JsonResultViewModel(false, message));
        }

        protected List<SelectListItem> BuildCreditCardSelectListItem(IMapper mapper, IBaseAppService<CreditCardDTO> creditCardAppService, int? id = null)
        {
            List<SelectListItem> listItem = [];

            AppServiceResult<IEnumerable<CreditCardDTO>> creditCardsDTO = creditCardAppService.GetAll();
            var creditCards = mapper.Map<IList<CreditCardViewModel>>(creditCardsDTO.Model);

            foreach (var item in creditCards)
            {
                listItem.Add(new SelectListItem(item.Name, item.Id.ToString(), item.Id == id));
            }

            listItem.Insert(0, new SelectListItem("Selecione", null, id == null));
            return listItem;
        }

        protected List<SelectListItem> BuildPersonSelectListItem(IMapper mapper, IBaseAppService<PersonDTO> personAppService, string optionText, int? id = null)
        {
            List<SelectListItem> listItem = [];
            AppServiceResult<IEnumerable<PersonDTO>> personsDTO = personAppService.GetAll();
            var persons = mapper.Map<IList<PersonViewModel>>(personsDTO.Model);

            foreach (var item in persons)
            {
                listItem.Add(new SelectListItem(item.Name, item.Id.ToString(), item.Id == id));
            }

            listItem.Insert(0, new SelectListItem(optionText, null, id == null));

            return listItem;
        }
    }
}
