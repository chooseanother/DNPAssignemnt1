using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyWebAPI.Models;
using FamilyWebAPI.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FamilyWebAPI.Data
{
    public class FamilySqliteService : IFamilyDataService
    {
        private FamilyContext _familyContext;

        public FamilySqliteService(FamilyContext familyContext)
        {
            _familyContext = familyContext;
        }

        public async Task<IList<Family>> GetFamiliesAsync()
        {
            return await _familyContext.Families.Include(family => family.Adults).ThenInclude(adult => adult.JobTitle)
                .Include(family => family.Children).ThenInclude(child => child.Pets)
                .Include(family => family.Children).ThenInclude(child => child.Interests)
                .Include(family => family.Pets).ToListAsync();
        }

        public async Task<Family> GetFamilyAsync(string streetName, int houseNumber)
        { 
            var fam = await _familyContext.Families.Where(family =>
                family.StreetName.Equals(streetName) && family.HouseNumber == houseNumber)
                .Include(family => family.Adults).ThenInclude(adult => adult.JobTitle)
                .Include(family => family.Children).ThenInclude(child => child.Pets)
                .Include(family => family.Children).ThenInclude(child => child.Interests)
                .Include(family => family.Pets).ToListAsync();
            return fam[0];
        }

        public async Task<Adult> AddAdultAsync(string streetName, int houseNumber, Adult adult)
        {
            var fam = await _familyContext.Families.Include(family => family.Adults).FirstAsync(family =>
                family.HouseNumber == houseNumber && family.StreetName.Equals(streetName));
            fam.Adults.Add(adult);
            _familyContext.Update(fam);
            await _familyContext.SaveChangesAsync();
            return adult;
        }

        public async Task<int> RemoveAdultAsync(int id)
        {
            var adult = await _familyContext.Adults.FirstAsync(a => a.Id == id);
            var removed = _familyContext.Adults.Remove(adult).Entity;
            await _familyContext.SaveChangesAsync();
            return removed.Id;
        }
    }
}