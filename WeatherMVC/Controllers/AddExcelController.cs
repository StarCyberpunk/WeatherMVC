using Microsoft.AspNetCore.Mvc;

namespace WeatherMVC.Controllers
{
    public class AddExcelController : Controller
    {

        IWebHostEnvironment _appEnvironment;
        public IActionResult AddExcel()
        {
            return View();
        }
        public AddExcelController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "DAL/Excel/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream( path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                WeatherMVC.DAL.Repositories.WeatherRepository.UpdateDataFromExcel(path);
            }

            return RedirectToAction("AddExcel");
        }
    }
}
