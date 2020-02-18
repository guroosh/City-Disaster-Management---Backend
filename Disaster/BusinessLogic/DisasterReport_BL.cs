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

namespace Disaster.BusinessLogic
{
    public class DisasterReport_BL : IBusinessLogic
    {
        private readonly IReportedDisasterCollection DisasterCollection;
        private readonly MqttPublisher Mqtt;
        public DisasterReport_BL(IReportedDisasterCollection disasterCollection, MqttPublisher mqtt)
        {
            DisasterCollection = disasterCollection;
            Mqtt = mqtt;
        }

        public async Task<bool> CreateAsync(object request)
        {
            ReportDisasterRequest request_ = (ReportDisasterRequest)request;
            ClassValueCopier copier = new ClassValueCopier();
            ReportedDisaster newReport = copier.ConvertAndCopy<ReportedDisaster, ReportDisasterRequest>(request_);
            newReport.CreatedBy = request_.ReportedBy;
            bool result = await DisasterCollection.AddAsync(newReport);
            
            if(result)
            {
                //Implemented::
                //create the message
                VerifyDisasterRequest verifyDisaster = copier.ConvertAndCopy<VerifyDisasterRequest, ReportedDisaster>(newReport);
                string data = JsonConvert.SerializeObject(verifyDisaster);
                //publishing the message
                Mqtt.MqttPublish("RSCD/Server/Disaster/Verification", data);
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

        public Task<object> GetAllDocumentsAsync(object request = null)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateDocumentAsync(object request)
        {
            VerifiedDisasterRequest request_ = (VerifiedDisasterRequest)request;
            ReportedDisaster oldDisaster = await DisasterCollection.GetAsync(request_.ReferenceCode);
            var copier = new ClassValueCopier();
            var newDisaster = copier.ConvertAndCopy(request_, oldDisaster);
            newDisaster.LastUpdatedBy = request_.VerifiedBy;
            bool result = await DisasterCollection.UpdateAsync(newDisaster);
            if (result)
            {
                VerifiedDisasterRequest verifiedDisaster = copier.ConvertAndCopy<VerifiedDisasterRequest, ReportedDisaster>(newDisaster);
                string data = JsonConvert.SerializeObject(verifiedDisaster);
                Mqtt.MqttPublish("RSCD/Server/Disaster/Verified", data);
            }
            return result;
        }
    }
}
