using System.Collections.Generic;
using Models;

namespace Assignment1.Data
{
    public interface IFamilyDataService
    {
        IList<Family> GetFamilies();
        void AddFamily(Family family);
        void RemoveFamily(string streetName, int houseNumber);
        void Update(Family family);
        Family Get(string streetName, int houseNumber);
        void AddAdult(string streetName, int houseNumber,Adult adult);
    }
}