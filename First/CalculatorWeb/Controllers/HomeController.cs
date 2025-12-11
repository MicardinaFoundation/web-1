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

            var responseData = await responce.Content.ReadFromJsonAsync<Data>();
            

            //ViewBag.Cathegories = 

            return View(responseData);
        }

        [HttpGet]
        public async Task<IActionResult> Cathegory()
        {
            var httpClient = new HttpClient();
            var responce = await httpClient.GetAsync("http://127.0.0.1:10280/Calculator");

            var responseData = await responce.Content.ReadFromJsonAsync<Data>();


            //ViewBag.Cathegories = 

            return View(responseData);
        }


        [HttpPost]
        public async Task<IActionResult> Index(VariantFilterDto model)
        {
            var httpClient = new HttpClient();
            var responce = await httpClient.GetAsync("http://127.0.0.1:10280/Calculator");
            var responcea = await httpClient.PostAsJsonAsync("http://127.0.0.1:10280/Calculator", model);


            if (!responcea.IsSuccessStatusCode)
            {
                return Redirect(nameof(Index));
            }
            else
            {
                var responseData = await responcea.Content.ReadFromJsonAsync<List<Variant>>();

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

                var responseDataa = await responce.Content.ReadFromJsonAsync<Data>();

                //responseDataa.Varianto = responseData;

                Data data = new()
                {
                    Variant = responseData,
                    Cathegory = responseDataa.Cathegory,
                };

                return View(data);
            }

        }

        #region - Add -
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var httpClient = new HttpClient();

            var responcea = await httpClient.GetAsync("http://127.0.0.1:10280/Calculator");

            var responseDataa = await responcea.Content.ReadFromJsonAsync<Data>();


            return View(responseDataa);
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

        #region - Add Cathegory -
        [HttpGet]
        public async Task<IActionResult> AddCathegory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCathegory([FromForm] CategoriesAddDto model)
        {
            var httpClient = new HttpClient();
            var responce = await httpClient.PutAsJsonAsync($"http://127.0.0.1:10280/Calculator/AddCategories?name={model.Cathegories}", model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

                var responseData = await responce.Content.ReadFromJsonAsync<CategoriesAddDto>();
            if (!responce.IsSuccessStatusCode)
            {
                _logger.LogError("FS{code}: {body}", responce.StatusCode, responseData);
            }


            return RedirectToAction(nameof(Cathegory));
        }
        #endregion

        #region - Edit -

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = new HttpClient();
            var responce = await httpClient.GetAsync($"http://127.0.0.1:10280/Calculator/{id}");
            var responseData = await responce.Content.ReadFromJsonAsync<Variant>();

            var responcea = await httpClient.GetAsync("http://127.0.0.1:10280/Calculator");

            var responseDataa = await responcea.Content.ReadFromJsonAsync<Data>();

            responseDataa.Varianto = responseData;

            ViewBag.EditId = id;


            return View(responseDataa);
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

        #region - Edit Cathegory-

        [HttpGet]
        public async Task<IActionResult> EditCathegory(int id)
        {
            var httpClient = new HttpClient();

            var responcea = await httpClient.GetAsync($"http://127.0.0.1:10280/Calculator/CategorieView?id={id}");

            var responseDataa = await responcea.Content.ReadFromJsonAsync<Cathegory>();


            ViewBag.EditId = id;


            return View(responseDataa);
        }

        [HttpPost]
        public async Task<IActionResult> EditCathegory([FromForm] Cathegory model)
        {
            var httpClient = new HttpClient();
            var responce = await httpClient.PatchAsJsonAsync("http://127.0.0.1:10280/Calculator/CategoriesPatch", model);

            var responseData = await responce.Content.ReadFromJsonAsync<Cathegory>();

            return RedirectToAction(nameof(Cathegory));
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = new HttpClient();
            var responce = await httpClient.DeleteAsync($"http://127.0.0.1:10280/Calculator?id={id}");

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var httpClient = new HttpClient();
            var responce = await httpClient.DeleteAsync($"http://127.0.0.1:10280/Calculator/CategoriesDelete?id={id}");

            return RedirectToAction(nameof(Cathegory));
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
