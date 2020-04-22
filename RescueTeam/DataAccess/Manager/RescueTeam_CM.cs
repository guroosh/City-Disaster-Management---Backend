using RescueTeam.DataAccess.Context;
using RescueTeam.DataAccess.Repository;
using RescueTeam.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueTeam.DataAccess.Manager
{
    public class RescueTeam_CM : IOfficerDetailCollection
    {
        private readonly DB_Context _context;

        public RescueTeam_CM()
        {
        }

            

        private string _collectionCodePrefix
        {
            get
            {
                return "RD";
            }
        }

        string IOfficerDetailCollection._collectionCodePrefix => throw new NotImplementedException();

        public Task<bool> AddAsync(OfficerDetails document)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(VerifiedDisasterReport newReport)
        {
            throw new NotImplementedException();
        }

       
        public Task<bool> AddResourceAsync(ResourceAllocation newResource)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id, string userCode, string reason = "")
        {
            throw new NotImplementedException();
        }

        public Task<List<OfficerDetails>> GetAllAsync(string getDocs = "active")
        {
            throw new NotImplementedException();
        }

        public Task<OfficerDetails> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetAsync(OfficerDetails newAdmin)
        {
            throw new NotImplementedException();
        }

        public Task<List<OfficerDetails>> GetDetails(string department, int count)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(OfficerDetails document)
        {
            throw new NotImplementedException();
        }
    }
}