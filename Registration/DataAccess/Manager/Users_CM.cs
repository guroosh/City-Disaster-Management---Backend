 using MongoDB.Driver;
using Registration.DataAccess.Repository;
using Registration.DataEntry.DataAccess.Context;
using Registration.Model.API;
using Registration.Model.DB;
using RSCD.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.DataAccess.Manager
{
    public class Users_CM : IUsersCollection
    {
        private readonly DB_Context _context;

        public Users_CM (DB_Context context)
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

        public async Task<Users> RegisterUserAsync(Users document)
        {
            try
            {
                Random _rdm = new Random();
                int id2 = _rdm.Next(000000, 1000000);
                int id1 = Convert.ToInt32(DateTime.Now.ToString("mmssff"));
                var id = id1 | id2;

                document.ReferenceCode = _collectionCodePrefix + id.ToString();
                
                document.CreatedAt = DateTime.Now.ToString();
                document.LastUpdatedAt = "";
                document.LastUpdatedBy = "";
                document.IsActive = true;
                await _context.UsersCollection.InsertOneAsync(document);
                
                return document;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(string id, string userCode, string reason = "")
        {
            try
            {
                string editedAt = DateTime.Now.ToString();

                FilterDefinition<Users> empFilter = Builders<Users>.Filter.Eq(doc => doc.ReferenceCode, id);
                UpdateDefinition<Users> empUpdate = Builders<Users>.Update
                    .Set(doc => doc.IsActive, false)
                    .Set(doc => doc.InActiveReason, reason)
                    .Set(doc => doc.LastUpdatedAt, editedAt)
                    .Set(doc => doc.LastUpdatedBy, userCode);
                
                UpdateResult result = await _context.UsersCollection.UpdateOneAsync(empFilter, empUpdate);
                return (result.IsAcknowledged && result.ModifiedCount > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Users>> GetAllAsync(string getDocs = "active")
        {
            try
            {
                return (getDocs.ToLower()) switch
                {
                    "active" => await _context.UsersCollection.Find(doc => doc.IsActive == true).ToListAsync(),
                    "inactive" => await _context.UsersCollection.Find(doc => doc.IsActive == false).ToListAsync(),
                    "all" => await _context.UsersCollection.Find(_ => true).ToListAsync(),
                    _ => throw new Exception(string.Format("the case {0} is not implemented", getDocs)),
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Users> GetAsync(string id)
        {
            try
            {
                return await _context.UsersCollection.Find(doc => doc.ReferenceCode == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateAsync(Users document)
        {
            try
            {
                document.LastUpdatedAt = DateTime.Now.ToString();
                FilterDefinition<Users> filter = Builders<Users>.Filter.Eq(doc => doc.ReferenceCode, document.ReferenceCode);
                var result = await _context.UsersCollection.ReplaceOneAsync(filter, document);
                return (result.IsAcknowledged && result.ModifiedCount > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Function not used

        public Task<bool> AddAsync(Users document)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckUserExistance(string emailId)
        {
            try
            {
                var user = await _context.UsersCollection.Find(doc => doc.EmailId == emailId).FirstOrDefaultAsync();
                return user == null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
