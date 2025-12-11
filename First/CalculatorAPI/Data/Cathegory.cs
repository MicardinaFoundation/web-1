using System.ComponentModel.DataAnnotations;

namespace CalculatorAPI.Data
{
    public class Cathegory
    {
        [Key]
        public int Id { get; set; }

        public int IdChanged { get; set; }

        public string Cathegories { get; set; }

    }
}
