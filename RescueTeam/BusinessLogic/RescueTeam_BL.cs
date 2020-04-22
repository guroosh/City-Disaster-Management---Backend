using Newtonsoft.Json;
using RescueTeam.DataAccess.Repository;
using RescueTeam.Model.API;
using RescueTeam.Model.DB;
using RSCD.BLL;
using RSCD.Helper;
using RSCD.Mqtt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueTeam.BusinessLogic
{
    public class RescueTeam_BL : IBusinessLogic
    {

        private readonly IOfficerDetailCollection RescueTeamCollection;
        private readonly MqttPublisher Mqtt;

        public RescueTeam_BL(IOfficerDetailCollection rescueTeamCollection, MqttPublisher mqtt)
        {
            RescueTeamCollection = rescueTeamCollection;
            Mqtt = mqtt;
        }

        public async Task<bool> CreateAdminAsync(AdminUserRequest request)
        {
            var copier = new ClassValueCopier();
            OfficerDetails newAdmin = copier.ConvertAndCopy<OfficerDetails, AdminUserRequest>(request);
            bool result = await RescueTeamCollection.GetAsync(newAdmin);
            return result;
            
        }
        public async Task<bool> UpdateAdminAsync(UpdateAdminUserRequest request)
        {
            OfficerDetails oldUser = await RescueTeamCollection.GetAsync(request.UserCode);
            var copier = new ClassValueCopier();
            OfficerDetails newUser = copier.ConvertAndCopy(request, oldUser);
            newUser.LastUpdatedBy = request.CurrentUserCode;
            newUser.LastUpdatedAt = DateTime.Now.ToString();
            return await RescueTeamCollection.UpdateAsync(newUser);
        }

       
        public async Task<bool> ResourceAllocationAsync(ResourceAllocationRequest request)
        {
            var copier = new ClassValueCopier();
            VerifiedDisasterReport newReport = copier.ConvertAndCopy<VerifiedDisasterReport, ResourceAllocationRequest> (request);
            bool result = await RescueTeamCollection.AddAsync(newReport);
            
            try
            {
               
                if (request.ScaleOfDisaster == "low")
                {

                    //allocate 1 unit ie 3 ppl of each requested dep
                    var medicallist = RescueTeamCollection.GetDetails(request.MedicalAssistanceRequired, 3);
                    var firelist = RescueTeamCollection.GetDetails(request.FireBrigadeAssistanceRequired, 3);
                    var trafficlist = RescueTeamCollection.GetDetails(request.TrafficPoliceAssistanceRequired, 3);
                    var otherlist = RescueTeamCollection.GetDetails(request.OtherResponseTeamRequired, 3);



                }
                else if(request.ScaleOfDisaster=="medium")
                {
                    var medicallist = RescueTeamCollection.GetDetails(request.MedicalAssistanceRequired, 6);
                    var firelist = RescueTeamCollection.GetDetails(request.FireBrigadeAssistanceRequired, 6);
                    var trafficlist = RescueTeamCollection.GetDetails(request.TrafficPoliceAssistanceRequired, 6);
                    var otherlist = RescueTeamCollection.GetDetails(request.OtherResponseTeamRequired, 6);
                }
                else
                {
                    var medicallist = RescueTeamCollection.GetDetails(request.MedicalAssistanceRequired, 9);
                    var firelist = RescueTeamCollection.GetDetails(request.FireBrigadeAssistanceRequired, 9);
                    var trafficlist = RescueTeamCollection.GetDetails(request.TrafficPoliceAssistanceRequired, 9);
                    var otherlist = RescueTeamCollection.GetDetails(request.OtherResponseTeamRequired, 9);
                }
                string data = JsonConvert.SerializeObject(request);
                await Mqtt.MqttPublish("RSCD/Message/Admin/ResourceAllocation", data);
               
                var copier1 = new ClassValueCopier();
                ResourceAllocation newResource = copier.ConvertAndCopy<ResourceAllocation, ResourceAllocationRequest>(request);
                bool result1 = await RescueTeamCollection.AddResourceAsync(newResource);

            }
            catch (Exception)
            {
                
                throw new Exception();
            }

            return true;
        }
          
         public async Task<AdditionalResourceAllocationResponse> AllocateAdditionalResource(ResourceAllocationRequest request)
        {
            VerifiedDisasterReport report = await RescueTeamCollection.GetDocumentAsync(report);

        }

        public Task<bool> CreateAsync(object request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAllDocumentsAsync(object request = null)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetDocumentAsync(ResourceAllocationRequest request)
        {
        }

        public Task<bool> UpdateDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }
    }
}
