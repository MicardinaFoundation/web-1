using System.ComponentModel.DataAnnotations;

namespace CalculatorAPI.Data
{
    public class Variant
    {
        [Key]
        public int Id { get; set; }

        public string NamsName { get; set; }

        public string Name { get; set; }

        public float Numb { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
