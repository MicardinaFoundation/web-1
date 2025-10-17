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
            var data = _context.Variants
                .Where(x => x.Name.Contains(string.IsNullOrEmpty(model.Name) ? "" : model.Name == "All" ? "" : model.Name))
                .Where(s => s.NamsName.Contains(string.IsNullOrEmpty(model.NamsName) ? "" : model.NamsName))
                .ToList();


            return data;
        }

        [HttpGet]
        public IEnumerable<Variant> Get()
        {
            var data = _context.Variants.ToList();
            return data;
        }

        [HttpGet("{id}")]
        public Variant Get(int id)
        {
            var data = _context.Variants.FirstOrDefault(x => x.Id == id);

            return data;
        }

        [HttpOptions]
        public List<string> GetList()
        {
            var data = _context.Variants
                .Select(x => x.Name)
                .Distinct()
                .ToList();


            return data;
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
            var variant = new Variant()
            {
                NamsName = model.NamsName,
                Name = model.Name,
                Numb = model.Numb,
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

            variant.NamsName = model.NamsName;
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
