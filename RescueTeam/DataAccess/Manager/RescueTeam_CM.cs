using RescueTeam.DataAccess.Context;
using RescueTeam.DataAccess.Repository;
using RescueTeam.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueTeam.DataAccess.Manager
{
    public class RescueTeam_CM : IRescueTeamCollection
    {
        private readonly DB_Context _context;

        public RescueTeam_CM(DB_Context context)
        {
            _context = context;
        }

        private string _collectionCodePrefix
        {
            get
            {
                return "RD";
            }
        }

        public Task<bool> AddAsync(AssistanceRequired document)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id, string userCode, string reason = "")
        {
            throw new NotImplementedException();
        }

        public Task<List<AssistanceRequired>> GetAllAsync(string getDocs = "active")
        {
            throw new NotImplementedException();
        }

        public Task<AssistanceRequired> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(AssistanceRequired document)
        {
            throw new NotImplementedException();
        }
    }
}