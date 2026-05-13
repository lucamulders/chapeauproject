using ChapeauProject.Models;
using ChapeauProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChapeauProject.Controllers
{
    public class TablesController : Controller
    {
        private readonly ITableService _tableService;

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        public IActionResult Index()
        {
            List<Table> tables = _tableService.GetAll();
            return View(tables);
        }

        [HttpPost]
        public IActionResult ToggleOccupied(int tableNumber)
        {
            _tableService.ToggleOccupied(tableNumber);
            return RedirectToAction("Index");
        }
    }
}