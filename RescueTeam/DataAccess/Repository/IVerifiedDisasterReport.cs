using RescueTeam.Model.API;
using RescueTeam.Model.DB;
using RSCD.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueTeam.DataAccess.Repository
{
    public interface IVerifiedDisasterReport : IDataRepository<VerifiedDisasterReport>
    {
        string _collectionCodePrefix { get; }
        Task<bool> ResourceAllocationAsync(ResourceAllocationRequest request);

    }
}
