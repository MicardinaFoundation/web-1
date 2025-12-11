using System.Collections.Generic;
using System.Linq;
using CalculatorAPI.Data;
using CalculatorAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CalculatorController> _logger;

        private readonly CalculationDbContext _context;

        public CalculatorController(ILogger<CalculatorController> logger, CalculationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public IEnumerable<Variant> Post([FromBody] VariantFilterDto model)
        {
            if (model.Name != "-1")
            {
                var cat = _context.Cathegories
                    .Where(i => i.Cathegories == model.Name)
                    .ToList();
                    
                var data = _context.Variants
                    .Where(x => x.NamsName.Contains(string.IsNullOrEmpty(model.NamsName) ? "" : model.NamsName == "All" ? "" : model.NamsName))
                    .Where(s => s.GroupId == cat[0].IdChanged)
                    .ToList();
                return data;

            }
            else
            {
                var data = _context.Variants
                    .Where(x => x.NamsName.Contains(string.IsNullOrEmpty(model.NamsName) ? "" : model.NamsName == "All" ? "" : model.NamsName))
                    .ToList();
                return data;


            }


        }

        [HttpGet]
        public Datas Get()
        {
            Datas a = new Datas();
            a.Variant = _context.Variants.ToList();
            a.Cathegory = _context.Cathegories.ToList();

            var d = a.Cathegory;
            for (int i = 0; i < d.Count; i++)
            {
                d[i].IdChanged = i;
                _context.Cathegories.Update(d[i]);
                _context.SaveChanges();
            }

            a.Cathegory = d;


            return a;
        }

        [HttpGet("{id}")]
        public Variant Get(int id)
        {
            var data = _context.Variants.FirstOrDefault(x => x.Id == id);

            return data;
        }



        [HttpGet("CategoriesList")]
        public IEnumerable<Cathegory> GetList()
        {
            var data = _context.Cathegories
                .ToList();


            return data;
        }
        [HttpGet("CategorieView")]
        public Cathegory GetCathg(int id)
        {
            var data = _context.Cathegories.FirstOrDefault(x => x.Id == id);


            return data;
        }
        /// <summary>
        /// Добавляет категорию
        /// </summary>
        /// <param name="nameCathegories"></param>
        /// <returns></returns>
        [HttpPut("AddCategories")]
        public Cathegory Put(string name)
        {
            var data = new Cathegory()
            {
                Cathegories = name,
            };
            _context.Cathegories.Add(data);
            _context.SaveChanges();
            return data;
        }
        [HttpPatch("CategoriesPatch")]
        public IActionResult EditListCathegories([FromBody] Cathegory cathegory)
        {
            var variant = _context.Cathegories.FirstOrDefault(x => x.Id == cathegory.Id);

            if (variant == null)
                return NotFound();


            variant.Cathegories = cathegory.Cathegories;

            _context.Cathegories.Update(variant);
            _context.SaveChanges();

            return new JsonResult(variant);
        }

        [HttpDelete("CategoriesDelete")]
        public IActionResult DeleteListCathegories(int id)
        {
            var variant = _context.Cathegories.FirstOrDefault(x => x.Id == id);


            if (variant == null)
                return NotFound();

            if (id <= 0 || _context.Cathegories.ToList().Count <= 1) return NotFound();


            _context.Cathegories.Remove(variant);
            _context.SaveChanges();

            return Ok();
        }





        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var variant = _context.Variants.FirstOrDefault(x => x.Id == id);

            if (variant == null)
                return NotFound();

            _context.Variants.Remove(variant);
            _context.SaveChanges();


            return Ok();
        }




        [HttpPut]
        public Variant Put([FromBody] VariantAddDto model)
        {
            var cathegotysz = _context.Cathegories.FirstOrDefault(x => x.Cathegories == model.Name);

            var variant = new Variant()
            {
                GroupId = cathegotysz.IdChanged,
                NamsName = model.NamsName,
                Numb = model.Numb,
                Name = "",
                Description = model.Description,
                CreatedAt = DateTime.Now
            };
            _context.Variants.Add(variant);
            _context.SaveChanges();
            return variant;
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] VariantEditDto model)
        {
            var variant = _context.Variants.FirstOrDefault(x => x.Id == model.Id);

            if (variant == null)
                return NotFound();

            var cathegotysz = _context.Cathegories.FirstOrDefault(x => x.Cathegories == model.Name);

            variant.NamsName = model.NamsName;
            variant.GroupId = cathegotysz.IdChanged;
            variant.Name = model.Name;
            variant.Numb = model.Numb;
            variant.Description = model.Description;
            variant.UpdatedAt = DateTime.Now;

            _context.Variants.Update(variant);
            _context.SaveChanges();

            return new JsonResult(variant);
        }

    }
}
