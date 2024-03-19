using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.WebSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.WebSite.Controllers
{
    public class CreditCardController : BaseController
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
            AppServiceResult<IEnumerable<CreditCardDTO>> result = _baseAppService.GetAll();

            if (!result.Success) return RedirectToError(result.Message);

            var viewModels = _mapper.Map<IList<CreditCardViewModel>>(result.Model);

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
            AppServiceResult<int> result = _baseAppService.Insert(dto);

            if (!result.Success) return RedirectToError(result.Message);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToError("Id não fornecido.");

            AppServiceResult<CreditCardDTO> result = _baseAppService.GetById(id.Value);

            if (!result.Success) return RedirectToError(result.Message);

            var viewModel = _mapper.Map<CreditCardViewModel>(result.Model);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CreditCardViewModel viewModel)
        {
            var dto = _mapper.Map<CreditCardDTO>(viewModel);

            AppServiceBaseResult result = _baseAppService.Update(dto);

            if (!result.Success) return RedirectToError(result.Message);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToError("Id não fornecido.");

            AppServiceResult<CreditCardDTO> result = _baseAppService.GetById(id.Value);

            if (!result.Success) return RedirectToError(result.Message);

            var viewModel = _mapper.Map<CreditCardViewModel>(result.Model);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            AppServiceBaseResult result = _baseAppService.Delete(id);

            if (!result.Success) return RedirectToError(result.Message);

            return RedirectToAction(nameof(Index));
        }
    }
}
