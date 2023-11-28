namespace WeatherMVC.Models.ViewModels
{
    public class WeatherViewModel
    {
        public int month { get; set; }
        public int year { get; set; }
        public List<Weather> weathersYearMonth = new List<Weather>();
    }
}
