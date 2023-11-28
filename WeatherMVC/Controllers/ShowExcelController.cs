using Microsoft.AspNetCore.Mvc;
using WeatherMVC.Models.ViewModels;

namespace WeatherMVC.Controllers
{
    public class ShowExcelController : Controller
    {
        public IActionResult Show()
        {

            return View();
        }
        public IActionResult ShowNew(int year, int month)
        {
            WeatherViewModel W = new WeatherViewModel();
            W.month = month;
            W.year = year;
            return View(W);
        }
    }
}
