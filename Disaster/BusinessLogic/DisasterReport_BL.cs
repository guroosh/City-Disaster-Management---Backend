using Disaster.Model.API;
using RSCD.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disaster.Model.DB;
using RSCD.Helper;
using Disaster.DataAccess.Repository;
using RSCD.Mqtt;
using Newtonsoft.Json;
using RSCD.Models.API;
using RSCD.Model.Custom.MinimalDetails;
using RSCD.Model.Custom.ExternalModel;

namespace Disaster.BusinessLogic
{
    public class DisasterReport_BL : IBusinessLogic
    {
        private readonly IReportedDisasterCollection _disasterCollection;
        private readonly Users_BL _usersBusinessLogic;
        private readonly MqttPublisher Mqtt;
        public DisasterReport_BL(IReportedDisasterCollection disasterCollection, Users_BL usersBusinessLogic, MqttPublisher mqtt)
        {
            _disasterCollection = disasterCollection;
            _usersBusinessLogic = usersBusinessLogic;
            Mqtt = mqtt;
        }

        public async Task<bool> CreateAsync(object request)
        {
            ReportDisasterRequest request_ = (ReportDisasterRequest)request;
            ClassValueCopier copier = new ClassValueCopier();
            ReportedDisaster newReport = copier.ConvertAndCopy<ReportedDisaster, ReportDisasterRequest>(request_);
            newReport.CreatedBy = request_.ReportedBy;
            newReport.IsVerfied = false;
            bool result = await _disasterCollection.AddAsync(newReport);

            if (result)
            {
                //Implemented::
                //create the message
                VerifyDisasterRequest verifyDisaster = copier.ConvertAndCopy<VerifyDisasterRequest, ReportedDisaster>(newReport);
                string data = JsonConvert.SerializeObject(verifyDisaster);
                //publishing the message
                await Mqtt.MqttPublish("RSCD/Server/Disaster/Verification", data);
                //return the http response

                //TO DO::
                //1.create the message 
                //2.push to the queue
                //3.return the http response
            }
            return result;
        }

        public Task<bool> DeleteDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetAllDocumentsAsync(object request = null)
        {
            GeneralListRequest request_ = (GeneralListRequest)request;

            var disasterList = await _disasterCollection.GetAllAsync();

            ClassValueCopier copier = new ClassValueCopier();
            List<DisasterReport_minimal> responseList = new List<DisasterReport_minimal>();


            foreach (var disaster in disasterList)
            {
                try
                {
                    var minimal = copier.ConvertAndCopy<DisasterReport_minimal, ReportedDisaster>(disaster);
                    minimal.ReportedBy = await _usersBusinessLogic.GetUserName(disaster.ReportedBy);
                    minimal.ReporterId = disaster.ReportedBy;
                    responseList.Add(minimal);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return responseList;
        }

        public async Task<object> GetDocumentAsync(object request)
        {
            GeneralFetchRequest request_ = (GeneralFetchRequest)request;
            var copier = new ClassValueCopier();
            var disaster = await _disasterCollection.GetAsync(request_.Code);
            var disasterReport = copier.ConvertAndCopy<Disaster_EM, ReportedDisaster>(disaster);
            disasterReport.ReportedId = disaster.ReportedBy;
            disasterReport.ReportedBy = await _usersBusinessLogic.GetUserName(disaster.ReportedBy);
            disasterReport.VerifiedId = disaster.VerifiedBy;
            disasterReport.VerifiedBy = await _usersBusinessLogic.GetUserName(disaster.VerifiedBy);
            return disasterReport;

        }

        public async Task<bool> CloseDisaster(GeneralFetchRequest request)
        {
            ReportedDisaster oldDisaster = await _disasterCollection.GetAsync(request.Code);
            var copier = new ClassValueCopier();
            var newDisaster = copier.ConvertAndCopy(request, oldDisaster);
            newDisaster.IsClosed = true;
            return await _disasterCollection.UpdateAsync(newDisaster);
        }

        public async Task<bool> UpdateDocumentAsync(object request = null)
        {
            VerifiedDisasterRequest request_ = (VerifiedDisasterRequest)request;
            ReportedDisaster oldDisaster = await _disasterCollection.GetAsync(request_.ReferenceCode);

            var copier = new ClassValueCopier();
            ReportedDisaster newDisaster = copier.ConvertAndCopy(request_, oldDisaster);
            newDisaster.LastUpdatedBy = request_.VerifiedBy;
            newDisaster.IsClosed = false;
            bool result = await _disasterCollection.UpdateAsync(newDisaster);

            if (result && newDisaster.IsInfoTrue)
            {
                VerifiedDisasterRequest verifiedDisaster = copier.ConvertAndCopy<VerifiedDisasterRequest, ReportedDisaster>(newDisaster);
                string data = JsonConvert.SerializeObject(verifiedDisaster);
                await Mqtt.MqttPublish("RSCD/Server/Disaster/Verified", data);
            }
            return result;
        }
    }
}

