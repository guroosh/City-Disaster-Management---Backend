using Newtonsoft.Json;
using RescueTeam.DataAccess.Repository;
using RescueTeam.Model.API;
using RescueTeam.Model.DB;
using RSCD.BLL;
using RSCD.Helper;
using RSCD.Model.Custom.MinimalDetails;
using RSCD.Model.Message;
using RSCD.Models.API;
using RSCD.Mqtt;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RescueTeam.BusinessLogic
{
    public class RescueTeam_BL : IBusinessLogic
    {

        private readonly IOfficerDetailCollection _officerDetailsCollection;
        private readonly IVerifiedDisasterReportCollection _verifiedDisasterReportCollection;
        private readonly MqttPublisher Mqtt;

        public RescueTeam_BL(IOfficerDetailCollection officerDetailsCollection, IVerifiedDisasterReportCollection verifiedDisasterReportCollection,MqttPublisher mqtt)
        {
            _officerDetailsCollection = officerDetailsCollection;
            _verifiedDisasterReportCollection = verifiedDisasterReportCollection;
            Mqtt = mqtt;
        }



        public async void ResourceAllocationAsync(VerifiedDisasterMessage request)
        {
            var copier = new ClassValueCopier();
            VerifiedDisasterReport newReport = copier.ConvertAndCopy<VerifiedDisasterReport, VerifiedDisasterMessage>(request);
            newReport.AssignedOfficers = new List<string>();
            // select the team count based on count
            int count = request.ScaleOfDisaster switch
            {
                "low" => 2,
                "Medium" => 4,
                "High" => 6,
                _ => 2,
            };


            // Fetch the assigning officer details
            List<OfficerDetails> officersList = new List<OfficerDetails>();
            officersList.AddRange((request.MedicalAssistanceRequired) ? await _officerDetailsCollection.GetOfficerDetails("Medical", count) : new List<OfficerDetails>());
            officersList.AddRange((request.TrafficPoliceAssistanceRequired) ? await _officerDetailsCollection.GetOfficerDetails("Traffic", count) : new List<OfficerDetails>());
            officersList.AddRange((request.FireBrigadeAssistanceRequired) ? await _officerDetailsCollection.GetOfficerDetails("FireBrigade", count) : new List<OfficerDetails>());
            officersList.AddRange(await _officerDetailsCollection.GetOfficerDetails("Law", count));



            foreach (OfficerDetails officer in officersList)
            {
                try
                {
                    // add the assign
                    newReport.AssignedOfficers.Add(officer.ReferenceCode);
                    officer.IsOfficerAssigned = true;
                    bool updateResult = await _officerDetailsCollection.UpdateAsync(officer);
                    //send notification introducing a pipeline or direct 
                }
                catch (NullReferenceException)
                {
                    continue;
                }
            }
            bool result = await _verifiedDisasterReportCollection.AddAsync(newReport);
            
        }

        public async Task<bool> AllocateAdditionalResourceAsync(AdditionalResourcesRequest request)
        {
            var disaster = await _verifiedDisasterReportCollection.GetAsync(request.ReferenceCode);

            // get the required officer
            List<OfficerDetails> officersList = await _officerDetailsCollection.GetOfficerDetails(request.Department, request.AdditionalUnits);
            foreach (OfficerDetails officer in officersList)
            {
                try
                {
                    // add the assign
                    disaster.AssignedOfficers.Add(officer.ReferenceCode);
                    officer.IsOfficerAssigned = true;
                    bool updateResult = await _officerDetailsCollection.UpdateAsync(officer);
                    //send notification introducing a pipeline or direct 
                }
                catch (NullReferenceException)
                {
                    continue;
                }
            }
            Trace.WriteLine(disaster.AssignedOfficers);
            return await _verifiedDisasterReportCollection.UpdateAsync(disaster);
        }

        public async Task<bool> ResourceDeallocationAsync(ResourceDeallocationRequest request)
        {
            var officer = await _officerDetailsCollection.GetAsync(request.OfficerReferenceCode);
            officer.IsOfficerAssigned = false;
            //send notification
            return await _officerDetailsCollection.UpdateAsync(officer);
        }

        public async Task<List<AdminUserDetails_minimal>> GetAllocatedOfficersAsync(GeneralFetchRequest request)
        {
            var disaster = await _verifiedDisasterReportCollection.GetAsync(request.Code);

            var copier = new ClassValueCopier();

            List<AdminUserDetails_minimal> officerList = new List<AdminUserDetails_minimal>();

            foreach(var officer in disaster.AssignedOfficers)
            {
                try
                {
                    officerList.Add(copier.ConvertAndCopy<AdminUserDetails_minimal, OfficerDetails>(await _officerDetailsCollection.GetAsync(officer)));
                }
                catch(Exception)
                {
                    continue;
                }
            }

            return officerList;
        }

        public async Task<bool> CreateAsync(object request)
        {
            UserDetailMessage request_ = (UserDetailMessage)request;
            var copier = new ClassValueCopier();
            OfficerDetails newAdmin = copier.ConvertAndCopy<OfficerDetails, UserDetailMessage>(request_);
            bool result = await _officerDetailsCollection.AddAsync(newAdmin);
            return result;
        }

       


        public async Task<bool> UpdateDocumentAsync(object request)
        {
            UserDetailMessage request_ = (UserDetailMessage)request;
            OfficerDetails oldUser = await _officerDetailsCollection.GetAsync(request_.ReferenceCode);
            var copier = new ClassValueCopier();
            OfficerDetails newUser = copier.ConvertAndCopy(request_, oldUser);
            newUser.LastUpdatedAt = DateTime.Now.ToString();
            return await _officerDetailsCollection.UpdateAsync(newUser);
        }


        // function not used
        public Task<object> GetDocumentAsync(object request)
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
    }
}


