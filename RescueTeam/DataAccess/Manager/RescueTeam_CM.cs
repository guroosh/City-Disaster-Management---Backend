using MongoDB.Driver;
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

        public RescueTeam_CM(DB_Context context)
        {
            _context = context;
        }

        public string _collectionCodePrefix
        {
            get
            {
                return "RD";
            }
        }

        public async Task<bool> AddAsync(OfficerDetails document)
        {
            try
            {
                Console.WriteLine(document);
                document.CreatedAt = DateTime.Now.ToString();
                document.LastUpdatedAt = "";
                document.IsActive = true;
                await _context.OfficerDetailsCollection.InsertOneAsync(document);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> DeleteAsync(string id, string userCode, string reason = "")
        {
            throw new NotImplementedException();
        }

        public async Task<List<OfficerDetails>> GetAllAsync(string getDocs = "active")
        {
            try
            {
                return (getDocs.ToLower()) switch
                {
                    "active" => await _context.OfficerDetailsCollection.Find(doc => doc.IsActive == true).ToListAsync(),
                    "inactive" => await _context.OfficerDetailsCollection.Find(doc => doc.IsActive == false).ToListAsync(),
                    "all" => await _context.OfficerDetailsCollection.Find(_ => true).ToListAsync(),
                    _ => throw new Exception(string.Format("the case {0} is not implemented", getDocs)),
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OfficerDetails> GetAsync(string id)
        {
            try
            {
                return await _context.OfficerDetailsCollection.Find(doc => doc.ReferenceCode == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OfficerDetails>> GetOfficerDetails(string department, int count)
        {
            try
            {
                return (await _context.OfficerDetailsCollection.Find(doc => doc.IsActive == true & doc.Department == department & !doc.IsOfficerAssigned ).ToListAsync()).Take(count).ToList();
            }
            catch(Exception ex)
            {
                throw ex;   
            }
        }

        public async Task<bool> UpdateAsync(OfficerDetails document)
        {
            try
            {
                document.LastUpdatedAt = DateTime.Now.ToString();
                FilterDefinition<OfficerDetails> filter = Builders<OfficerDetails>.Filter.Eq(doc => doc.ReferenceCode, document.ReferenceCode);
                var result = await _context.OfficerDetailsCollection.ReplaceOneAsync(filter, document);
                return (result.IsAcknowledged && result.ModifiedCount > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}