using MongoDB.Driver;
using RescueTeam.DataAccess.Context;
using RescueTeam.DataAccess.Repository;
using RescueTeam.Model.API;
using RescueTeam.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueTeam.DataAccess.Manager
{
    public class VerifiedDisasterReport_CM : IVerifiedDisasterReportCollection
    {
        private readonly DB_Context _context;

        public VerifiedDisasterReport_CM(DB_Context context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(VerifiedDisasterReport document)
        {
            try
            {
                Console.WriteLine(document);
                document.CreatedAt = DateTime.Now.ToString();
                document.LastUpdatedAt = "";
                document.IsActive = true;
                await _context.VerifiedDisasterReportCollection.InsertOneAsync(document);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }

        public Task<bool> DeleteAsync(string id, string userCode, string reason = "")
        {
            throw new NotImplementedException();
        }

        public async Task<List<VerifiedDisasterReport>> GetAllAsync(string getDocs = "active")
        {
            try
            {
                return (getDocs.ToLower()) switch
                {
                    "active" => await _context.VerifiedDisasterReportCollection.Find(doc => doc.IsActive == true).ToListAsync(),
                    "inactive" => await _context.VerifiedDisasterReportCollection.Find(doc => doc.IsActive == false).ToListAsync(),
                    "all" => await _context.VerifiedDisasterReportCollection.Find(_ => true).ToListAsync(),
                    _ => throw new Exception(string.Format("the case {0} is not implemented", getDocs)),
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<VerifiedDisasterReport> GetAsync(string id)
        {
            try
            {
                return await _context.VerifiedDisasterReportCollection.Find(doc => doc.ReferenceCode == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> ResourceAllocationAsync(ResourceAllocationRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(VerifiedDisasterReport document)
        {
            try
            {
                document.LastUpdatedAt = DateTime.Now.ToString();
                FilterDefinition<VerifiedDisasterReport> filter = Builders<VerifiedDisasterReport>.Filter.Eq(doc => doc.ReferenceCode, document.ReferenceCode);
                var result = await _context.VerifiedDisasterReportCollection.ReplaceOneAsync(filter, document);
                return (result.IsAcknowledged && result.ModifiedCount > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
