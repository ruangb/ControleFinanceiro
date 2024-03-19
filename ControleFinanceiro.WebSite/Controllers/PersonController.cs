using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.WebSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.WebSite.Controllers
{
    public class PersonController : BaseController
    {
        private readonly IBaseAppService<PersonDTO> _baseAppService;
        private readonly IMapper _mapper;

        public PersonController(IBaseAppService<PersonDTO> baseAppService, IMapper mapper)
        {
            _baseAppService = baseAppService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            AppServiceResult<IEnumerable<PersonDTO>> result = _baseAppService.GetAll();

            if (!result.Success) return RedirectToError(result.Message);

            var Models = _mapper.Map<IList<PersonViewModel>>(result.Model);

            return View(Models);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PersonViewModel viewModel)
        {
            var dto = _mapper.Map<PersonDTO>(viewModel);
            AppServiceResult<int> result = _baseAppService.Insert(dto);

            if (!result.Success) return RedirectToError(result.Message);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToError("Id não fornecido.");

            AppServiceResult<PersonDTO> result = _baseAppService.GetById(id.Value);

            if (!result.Success) return RedirectToError(result.Message);

            var viewModel = _mapper.Map<PersonViewModel>(result.Model);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(PersonViewModel viewModel)
        {
            var dto = _mapper.Map<PersonDTO>(viewModel);

            AppServiceBaseResult result = _baseAppService.Update(dto);

            if (!result.Success) return RedirectToError(result.Message);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToError("Id não fornecido.");

            AppServiceResult<PersonDTO> result = _baseAppService.GetById(id.Value);

            if (!result.Success) return RedirectToError(result.Message);

            var viewModel = _mapper.Map<PersonViewModel>(result.Model);

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
