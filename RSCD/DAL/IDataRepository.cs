using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSCD.DAL
{
    public interface IDataRepository<Collection> where Collection : class
    {
        Task<bool> AddAsync(Collection document);
        Task<bool> UpdateAsync(Collection document);
        Task<List<Collection>> GetAllAsync(string getDocs = "active");
        Task<Collection> GetAsync(string id);
        Task<bool> DeleteAsync(string id, string userCode, string reason = "");
        Task<bool> AddAsync(global::RescueTeam.DataAccess.Repository.OfficerDetails.AdminUser newAdmin);
    }
}
