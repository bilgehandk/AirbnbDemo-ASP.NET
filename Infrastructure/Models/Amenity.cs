using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Amenity
    {
        public int Id { get; set; }
        

        public string Name { get; set; }
    }
}