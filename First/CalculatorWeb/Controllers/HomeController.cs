using System.Diagnostics;
using CalculatorAPI.Data;
using CalculatorAPI.Models;
using CalculatorWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var responce = await httpClient.GetAsync("http://127.0.0.1:10280/Calculator");

            var responseData = await responce.Content.ReadFromJsonAsync<List<Variant>>();

            return View(responseData);
        }

        [HttpPost]
        public async Task<IActionResult> Index(VariantFilterDto model)
        {
            var httpClient = new HttpClient();
            var responce = await httpClient.PostAsJsonAsync("http://127.0.0.1:10280/Calculator", model);


            if (!responce.IsSuccessStatusCode)
            {
                return Redirect(nameof(Index));
            }
            else
            {
                var responseData = await responce.Content.ReadFromJsonAsync<List<Variant>>();

                //responce = await httpClient.GetAsync("http://127.0.0.1:10280/Calculator");
                //var s = await responce.Content.ReadFromJsonAsync<List<Variant>>();

                //var d = s
                //    .Select(x => x.Name)
                //    .Distinct()
                //    .ToList();
                //List<object> a = new();
                //a.Add(responseData);
                //a.Add(d);
                //return View(a);
                return View(responseData);
            }

        }

        #region - Add -
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] VariantAddDto model)
        {
            var httpClient = new HttpClient();
            var responce = await httpClient.PutAsJsonAsync("http://127.0.0.1:10280/Calculator", model);

            var responseData = await responce.Content.ReadFromJsonAsync<Variant>();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region - Edit -

            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var httpClient = new HttpClient();
                var responce = await httpClient.GetAsync($"http://127.0.0.1:10280/Calculator/{id}");
                var responseData = await responce.Content.ReadFromJsonAsync<Variant>();

                return View(responseData);
            }

            [HttpPost]
            public async Task<IActionResult> Edit([FromForm] VariantEditDto model)
            {
                var httpClient = new HttpClient();
                var responce = await httpClient.PatchAsJsonAsync("http://127.0.0.1:10280/Calculator", model);

                var responseData = await responce.Content.ReadFromJsonAsync<Variant>();

                return RedirectToAction(nameof(Index));
            }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = new HttpClient();
            var responce = await httpClient.DeleteAsync($"http://127.0.0.1:10280/Calculator?id={id}");

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
