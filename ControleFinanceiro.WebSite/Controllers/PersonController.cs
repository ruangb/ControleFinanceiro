using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ControleFinanceiro.WebSite.Controllers
{
    public class PersonController : Controller
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
            var dtos = _baseAppService.GetAll();

            if (!dtos.Sucess) return RedirectToError(dtos.Message);

            var viewModels = _mapper.Map<IList<PersonViewModel>>(dtos.Model);

            return View(viewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PersonViewModel viewModel)
        {
            var dto = _mapper.Map<PersonDTO>(viewModel);
            _baseAppService.Insert(dto);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToError("Id não fornecido.");

            var dto = _baseAppService.GetById(id.Value);
            var viewModel = _mapper.Map<PersonViewModel>(dto);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(PersonViewModel viewModel)
        {
            var dto = _mapper.Map<PersonDTO>(viewModel);

            _baseAppService.Update(dto);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToError("Id não fornecido.");

            var dto = _baseAppService.GetById(id.Value);
            var viewModel = _mapper.Map<PersonViewModel>(dto);

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

        private RedirectToActionResult RedirectToError(string message)
        {
            return RedirectToAction(nameof(Error), new { message });
        }
    }
}
