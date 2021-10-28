using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Data
{
    public interface IFamilyDataService
    {
        Task<IList<Family>> GetFamiliesAsync();
        Task<Family> GetAsync(string streetName, int houseNumber);
        Task AddAdultAsync(string streetName, int houseNumber,Adult adult);
        Task RemoveAdultAsync(int adultId);
    }
}