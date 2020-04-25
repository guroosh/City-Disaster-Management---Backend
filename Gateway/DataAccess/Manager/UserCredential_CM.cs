using Gateway.DataAccess.Repository;
using Gateway.Model.DB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.DataAccess.Manager
{
    public  class UserCredential_CM : IUserCredentialCollection
    {
        private readonly DB_Context _context;

        public UserCredential_CM(DB_Context context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(UserCredentials document)
        {
           try
            {
                Console.WriteLine(document);
                document.CreatedAt = DateTime.Now.ToString();
                document.LastUpdatedAt = "";
                document.IsActive = true;
                await _context.UserCredentialCollection.InsertOneAsync(document);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }

        public async Task<UserCredentials> CheckUserExistence(string loginId)
        {
            try
            {
                return await _context.UserCredentialCollection.Find(doc => doc.EmailId == loginId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public Task<bool> DeleteAsync(string id, string userCode, string reason = "")
        {
            throw new NotImplementedException();
        }

        public Task<List<UserCredentials>> GetAllAsync(string getDocs = "active")
        {
            throw new NotImplementedException();
        }

        public async Task<UserCredentials> GetAsync(string id)
        {
            try
            {
                return await _context.UserCredentialCollection.Find(doc => doc.ReferenceCode == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public async Task<bool> UpdateAsync(UserCredentials document)
        {
            try
            {
                document.LastUpdatedAt = DateTime.Now.ToString();
                FilterDefinition<UserCredentials> filter = Builders<UserCredentials>.Filter.Eq(doc => doc.ReferenceCode, document.ReferenceCode);
                var result = await _context.UserCredentialCollection.ReplaceOneAsync(filter, document);
                return (result.IsAcknowledged && result.ModifiedCount > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
