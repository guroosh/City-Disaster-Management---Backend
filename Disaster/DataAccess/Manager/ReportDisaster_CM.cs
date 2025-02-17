﻿using Disaster.DataAccess.Repository;
using Disaster.Model.DB;
using Disaster.DataAccess.Context;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Disaster.DataAccess.Manager
{
    public class ReportDisaster_CM : IReportedDisasterCollection
    {
        private readonly DB_Context _context; 

        public ReportDisaster_CM(DB_Context context)
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

        public async Task<bool> AddAsync(ReportedDisaster document)
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
                document.IsActive = true;
                await _context.ReportedDisasterCollection.InsertOneAsync(document);
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

        public async Task<List<ReportedDisaster>> GetAllAsync(string getDocs = "active")
        {
            try
            {
                return (getDocs.ToLower()) switch
                {
                    "active" => await _context.ReportedDisasterCollection.Find(doc => doc.IsActive == true).ToListAsync(),
                    "inactive" => await _context.ReportedDisasterCollection.Find(doc => doc.IsActive == false).ToListAsync(),
                    "all" => await _context.ReportedDisasterCollection.Find(_ => true).ToListAsync(),
                    _ => throw new Exception(string.Format("the case {0} is not implemented", getDocs)),
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ReportedDisaster> GetAsync(string id)
        {
            try
            {
                return await _context.ReportedDisasterCollection.Find(doc => doc.ReferenceCode == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateAsync(ReportedDisaster document)
        {
            try
            {
                document.LastUpdatedAt = DateTime.Now.ToString();
                FilterDefinition<ReportedDisaster> filter = Builders<ReportedDisaster>.Filter.Eq(doc => doc.ReferenceCode, document.ReferenceCode);
                var result = await _context.ReportedDisasterCollection.ReplaceOneAsync(filter, document);
                return (result.IsAcknowledged && result.ModifiedCount > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
