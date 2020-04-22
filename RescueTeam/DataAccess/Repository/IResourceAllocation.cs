using RescueTeam.Model.DB;
using RSCD.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueTeam.DataAccess.Repository
{
    public interface IResourceAllocation : IDataRepository<ResourceAllocation>
    {
        string _collectionCodePrefix { get; }
        
        Task<List<ResourceAllocation>> GetAdditionalResource(string AllocatedOfficeId, string ListOfDepartment, string AdditionalUnits);
    }
}
