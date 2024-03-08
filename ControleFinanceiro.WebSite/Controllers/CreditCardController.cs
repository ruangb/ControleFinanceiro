using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.WebSite.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id não fornecido." });

            var dto = _baseAppService.GetById(id.Value);
            var viewModel = _mapper.Map<CreditCardViewModel>(dto);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _baseAppService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
