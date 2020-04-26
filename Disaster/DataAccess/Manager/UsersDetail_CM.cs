using Disaster.DataAccess.Context;
using Disaster.DataAccess.Repository;
using Disaster.Model.DB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster.DataAccess.Manager
{
    public class UsersDetail_CM : IUsersCollection
    {

        private readonly DB_Context _context;

        public UsersDetail_CM(DB_Context context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(UsersDetail document)
        {
            try
            {
                Console.WriteLine(document);
                document.CreatedAt = DateTime.Now.ToString();
                document.LastUpdatedAt = "";
                document.IsActive = true;
                await _context.UsersDetailCollection.InsertOneAsync(document);
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

        public Task<List<UsersDetail>> GetAllAsync(string getDocs = "active")
        {
            throw new NotImplementedException();
        }

        public async Task<UsersDetail> GetAsync(string id)
        {
            try
            {
                return await _context.UsersDetailCollection.Find(doc => doc.ReferenceCode == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> UpdateAsync(UsersDetail document)
        {
            throw new NotImplementedException();
        }
    }
}
