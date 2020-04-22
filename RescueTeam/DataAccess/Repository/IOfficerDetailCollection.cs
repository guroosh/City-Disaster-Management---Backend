using RescueTeam.Model.DB;
using RSCD.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueTeam.DataAccess.Repository
{
    public interface IOfficerDetailCollection : IDataRepository<OfficerDetails>
    {
        string _collectionCodePrefix { get; }
        Task<List<OfficerDetails>> GetDetails(string department, int count);
        Task<bool> AddAsync(OfficerDetails newAdmin);
        Task<bool> UpdateAsync(OfficerDetails newUser);
        Task<bool> AddAsync(VerifiedDisasterReport newReport);
        Task<bool> GetAsync(OfficerDetails newAdmin);
        Task<bool> GetDocumentAsync();
        Task<bool> AddResourceAsync(ResourceAllocation newResource);
        object GetDetails(bool medicalAssistanceRequired, int v);
        //Task<List<Adm>> GetResource(Department);
    }
}

