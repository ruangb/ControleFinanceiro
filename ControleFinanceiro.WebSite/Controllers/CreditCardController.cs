using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.WebSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.WebSite.Controllers
{
    public class CreditCardController : Controller
    {
        private readonly IBaseAppService<CreditCardDTO> _baseAppService;
        private readonly IMapper _mapper;

        public CreditCardController(IBaseAppService<CreditCardDTO> baseAppService, IMapper mapper)
        {
            _baseAppService = baseAppService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var creditCards = _baseAppService.GetAll();
            var creditCardsViewModel = _mapper.Map<CreditCardViewModel>(creditCards);

            return View(creditCardsViewModel);
        }
    }
}
