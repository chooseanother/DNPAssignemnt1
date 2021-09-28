using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Assignment1.Data;
using Models;

namespace FileData
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

        public void SaveChanges()
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

        public IList<Family> GetFamilies()
        {
            return new List<Family>(Families);
        }

        public void AddFamily(Family family)
        {
            Families.Add(family);
            SaveChanges();
        }

        public void RemoveFamily(string streetName, int houseNumber)
        {
            var toRemove = Families.First(f => f.StreetName.Equals(streetName) && f.HouseNumber == houseNumber);
            Families.Remove(toRemove);
            SaveChanges();
        }

        public void Update(Family family)
        {
            var toUpdate = Families.First(f => f.StreetName.Equals(family.StreetName) && f.HouseNumber == family.HouseNumber);
            toUpdate.Adults = family.Adults;
            toUpdate.Children = family.Children;
            toUpdate.Pets = family.Pets;
            toUpdate.HouseNumber = family.HouseNumber;
            toUpdate.StreetName = family.StreetName;
            SaveChanges();
        }

        public Family Get(string streetName, int houseNumber)
        {
            return Families.FirstOrDefault(f => f.StreetName.Equals(streetName) && f.HouseNumber == houseNumber);
        }
    }
}