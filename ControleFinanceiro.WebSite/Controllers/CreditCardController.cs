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
            var dtos = _baseAppService.GetAll();
            var viewModels = _mapper.Map<IList<CreditCardViewModel>>(dtos);

            return View(viewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreditCardViewModel viewModel)
        {
            var dto = _mapper.Map<CreditCardDTO>(viewModel);
            _baseAppService.Insert(dto);

            return Index();
        }
    }
}
