namespace WeatherMVC.Models
{
    public class Weather
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Time { get; set; }
        public string T { get; set; }
        public string Vlaga { get; set; }
        public string Td { get; set; }
        public string Ps { get; set; }
        public string NaprVet { get; set; }
        public string SpeedVetra { get; set; }
        public string Oblach { get; set; }
        public string h { get; set; }
        public string VV { get; set; }
        public string pogoda { get; set; }
        public Weather()
        {

        }
        public Weather(List<string> nn)
        {

            Date = nn[0];
            Month = DateTime.Parse(nn[0]).Month.ToString();
            Year = DateTime.Parse(nn[0]).Year.ToString();
            Time = nn[1];
            T = nn[2];
            Vlaga = nn[3];
            Td = nn[4];
            Ps = nn[5];
            NaprVet = nn[6];
            SpeedVetra = nn[7];
            Oblach = nn[8];
            h = nn[9];
            VV = nn[10];
            pogoda = nn[11];

        }
    }

}
