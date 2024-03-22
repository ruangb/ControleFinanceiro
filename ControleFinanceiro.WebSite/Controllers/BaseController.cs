using ControleFinanceiro.WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
    }
}
