using RescueTeam.DataAccess.Repository;
using RSCD.BLL;
using RSCD.Mqtt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueTeam.BusinessLogic
{
    public class RescueTeam_BL : IBusinessLogic
    {
        private readonly IRescueTeamCollection RescueTeamCollection;
        private readonly MqttPublisher Mqtt;

        public RescueTeam_BL(IRescueTeamCollection rescueTeamCollection, MqttPublisher mqtt)
        {
            RescueTeamCollection = rescueTeamCollection;
            Mqtt = mqtt;
        }

        public Task<bool> CreateAsync(object request)
        {
            //Type cast obj type into officer details 
            //Create copier to convert copy into database 
            //push it to the collection manager
            //add async
        }

        public Task<bool> DeleteDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAllDocumentsAsync(object request = null)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }
    }
}
