using AutoMapper;
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
        private readonly IMapper _mapper;

        public BillController(IBillAppService billAppService, IMapper mapper)
        {
            _billAppService = billAppService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            AppServiceResult<IEnumerable<BillDTO>> result = _billAppService.GetAllBills();

            if (!result.Success) return RedirectToError(result.Message);

            var Models = _mapper.Map<IList<BillViewModel>>(result.Model);

            return View(Models);
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
    }
}
