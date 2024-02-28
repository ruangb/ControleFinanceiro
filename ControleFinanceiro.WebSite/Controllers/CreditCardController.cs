using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.WebSite.Controllers
{
    public class CreditCardController : Controller
    {
        private readonly IBaseAppService<CreditCard> _baseAppService;

        public CreditCardController(IBaseAppService<CreditCard> baseAppService)
        {
            _baseAppService = baseAppService;
        }

        public IActionResult Index()
        {
            var creditCards = _baseAppService.GetAll();
            return View(creditCards);
        }
    }
}
