using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FamilyWebAPI.Models;

namespace FamilyWebAPI.Data
{
    public class FamilyDataPersistence : IFamilyDataService
    {
        public IList<Family> Families { get; private set; }

        private readonly string familiesFile = "families.json";

        public FamilyDataPersistence()
        {
            Families = File.Exists(familiesFile) ? ReadData<Family>(familiesFile) : new List<Family>();
        }

        private IList<T> ReadData<T>(string s)
        {
            using (var jsonReader = File.OpenText(familiesFile))
            {
                return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd());
            }
        }

        private void SaveChanges()
        {
            // storing families
            string jsonFamilies = JsonSerializer.Serialize(Families, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(familiesFile, false))
            {
                outputFile.Write(jsonFamilies);
            }
        }

        public async Task<IList<Family>> GetFamiliesAsync()
        {
            return new List<Family>(Families);
        }

        // public async Task<Family> AddFamilyAsync(Family family)
        // {
        //     Families.Add(family);
        //     SaveChanges();
        //     return family;
        // }
        //
        // public void RemoveFamily(string streetName, int houseNumber)
        // {
        //     var toRemove = Families.First(f => f.StreetName.Equals(streetName) && f.HouseNumber == houseNumber);
        //     Families.Remove(toRemove);
        //     SaveChanges();
        // }

        private void Update(Family family)
        {
            var toUpdate = Families.First(f => f.StreetName.Equals(family.StreetName) && f.HouseNumber == family.HouseNumber);
            toUpdate.Adults = family.Adults;
            toUpdate.Children = family.Children;
            toUpdate.Pets = family.Pets;
            toUpdate.HouseNumber = family.HouseNumber;
            toUpdate.StreetName = family.StreetName;
            SaveChanges();
        }
        
        private Family Get(string streetName, int houseNumber)
        {
            return Families.FirstOrDefault(f => f.StreetName.Equals(streetName) && f.HouseNumber == houseNumber);
        }

        public async Task<Family> GetFamilyAsync(string streetName, int houseNumber)
        {
            return Get(streetName,houseNumber);
        }

        public async Task<Adult> AddAdultAsync(string streetName, int houseNumber, Adult adult)
        {
            var max = Int32.MinValue;
            foreach (var family in Families)
            {
                var tmp = family.Adults.Max(a => a.Id);
                if (tmp > max)
                {
                    max = tmp;
                }
            }

            adult.Id = max + 1;
            var familyToUpdate = Get(streetName, houseNumber);
            familyToUpdate.Adults.Add(adult);
            Update(familyToUpdate);

            return adult;
        }

        public async Task<int> RemoveAdultAsync(int id)
        {
            Adult toRemove = null;
            foreach (var family in Families)
            {
                if (family.Adults.Any())
                {
                    try
                    {
                        toRemove = family.Adults.First(a => a.Id == id);
                        family.Adults.Remove(toRemove);
                    }
                    catch
                    {
                        Console.WriteLine($"This is stupid. Adult with id: {id} not in this family");
                    }
                }
            }

            return toRemove.Id;
        }
    }
}