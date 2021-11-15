using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Models
{
    public class Family
    {
        //public int Id { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public List<Adult> Adults { get; set; }
        public List<Child> Children { get; set; }
        public List<Pet> Pets { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}