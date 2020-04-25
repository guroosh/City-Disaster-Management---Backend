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
        Task<List<OfficerDetails>> GetOfficerDetails(string department, int count);
    }
}

