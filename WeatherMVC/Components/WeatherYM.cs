using Microsoft.AspNetCore.Mvc;
using WeatherMVC.DAL.Repositories;
using WeatherMVC.Models;
using WeatherMVC.Models.ViewModels;

namespace WeatherMVC.Components
{
    public class WeatherYM : ViewComponent
    {
        private WeatherRepository repository;

        public WeatherYM()
        {
        }
        public IViewComponentResult Invoke(int year, int month)
        {
            var s = GetMonthYearData(year, month);
            if (s == null) { return View(); }
            else
            {
                WeatherViewModel result = new WeatherViewModel()
                {
                    month = month,
                    year = year,
                    weathersYearMonth = s
                };
                return View(result);
            }
        }
        public List<Weather> GetMonthYearData(int year, int month)
        {

            repository = new WeatherRepository(year, month);
            return repository.GetWeathers();

        }
    }
}
