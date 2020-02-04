using Registration.DataAccess.Repository;
using Registration.DataEntry.DataAccess.Context;
using Registration.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.DataAccess.Manager
{
    public class Redistration_CM : IUsersCollection
    {
        private readonly DB_Context _context;

        public Redistration_CM(DB_Context context)
        {
            _context = context;
        }

        public string _collectionCodePrefix
        {
            get
            {
                return "USR";
            }
        }

        public async Task<bool> AddAsync(Users document)
        {
            try
            {
                Random _rdm = new Random();
                int id2 = _rdm.Next(000000, 1000000);
                int id1 = Convert.ToInt32(DateTime.Now.ToString("mmssff"));
                var id = id1 | id2;

                document.ReferenceId = _collectionCodePrefix + id.ToString();
                
                document.CreatedAt = DateTime.Now.ToString();
                document.LastUpdatedAt = "";
                document.LastUpdatedBy = "";
                document.IsActive = true;
              
                await _context.UsersCollection.InsertOneAsync(document);
                
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

        public Task<List<Users>> GetAllAsync(string getDocs = "active")
        {
            throw new NotImplementedException();
        }

        public Task<Users> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Users document)
        {
            throw new NotImplementedException();
        }
    }
}
