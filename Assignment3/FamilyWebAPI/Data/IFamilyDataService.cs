using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Data
{
    public interface IFamilyDataService
    {
        Task<IList<Family>> GetFamiliesAsync();
        // Task<Family> AddFamilyAsync(Family family);
        // void RemoveFamily(string streetName, int houseNumber);
        // void Update(Family family);
        Task<Family> GetFamilyAsync(string streetName, int? houseNumber);
        Task<Adult> AddAdultAsync(string streetName, int? houseNumber,Adult adult);
        Task<int> RemoveAdultAsync(int id);
    }
}