using Disaster.DataAccess.Repository;
using Disaster.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster.DataAccess.Manager
{
    public class ReportDisaster_CM : IReportedDisasterCollection
    {

        public Task<bool> AddAsync(ReportedDisaster document)
        {
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(string id, string userCode, string reason = "")
        {
            throw new NotImplementedException();
        }

        public Task<List<ReportedDisaster>> GetAllAsync(string getDocs = "active")
        {
            throw new NotImplementedException();
        }

        public Task<ReportedDisaster> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ReportedDisaster document)
        {
            throw new NotImplementedException();
        }
    }
}
