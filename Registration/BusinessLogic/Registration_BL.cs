using Registration.Model.API;
using RSCD.BLL;
using System;
using System.Threading.Tasks;
using RSCD.Helper;
using Registration.Model.DB;
using Registration.DataAccess.Repository;
using RSCD.Models.API;
using RSCD.Model.Custom.ExternalModel.Registration;
using RSCD.Model.Message;
using Newtonsoft.Json;
using RSCD.Mqtt;

namespace Registration.BusinessLogic
{
    public class Registration_BL : IBusinessLogic
    {
        private readonly IUsersCollection _usersCollection;
        private readonly MqttPublisher Mqtt;
        public Registration_BL(IUsersCollection usersCollection, MqttPublisher mqtt)
        {
            _usersCollection = usersCollection;
            Mqtt = mqtt;
        }

        public Task<bool> CreateAsync(object request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterCommonUser(RegisterCommonUserRequest request)
        {
            var copier = new ClassValueCopier();
            Users newUser = copier.ConvertAndCopy<Users, RegisterCommonUserRequest>(request); 
            newUser.Role = "CommonUser";
            newUser.IsCommonUser = true;
            newUser.ReferenceCode = await _usersCollection.RegisterUserAsync(newUser);
            bool result = newUser.ReferenceCode.Length != 0;
            
            if (result) 
            {
                await PublishUserCredentialAsync(newUser);
            }
            return result;
        }

        public async Task<bool> RegisterAdminUser(RegisterAdminUserRequest request)
        {
            var copier = new ClassValueCopier();
            Users newUser = copier.ConvertAndCopy<Users, RegisterAdminUserRequest>(request);
            newUser.IsCommonUser = false;
            string referenceCode = await _usersCollection.RegisterUserAsync(newUser);
            bool result = referenceCode.Length != 0;

            if (result)
            {
                await PublishUserCredentialAsync(newUser);
            }
            return result;
        }
        
        public async Task<bool> UpdateCommonUser(UpdateCommonUserRequest request)
        {
            Users oldUser = await _usersCollection.GetAsync(request.UserCode);
            var copier = new ClassValueCopier();
            Users newUser = copier.ConvertAndCopy(request, oldUser);
            newUser.LastUpdatedBy = request.CurrentUserCode;
            newUser.LastUpdatedAt = DateTime.Now.ToString();
            return await _usersCollection.UpdateAsync(newUser);
        }

        public async Task<bool> UpdateAdminUser(UpdateAdminUserRequest request)
        {
            Users oldUser = await _usersCollection.GetAsync(request.UserCode);
            var copier = new ClassValueCopier();
            Users newUser = copier.ConvertAndCopy(request, oldUser);
            newUser.LastUpdatedBy = request.CurrentUserCode;
            newUser.LastUpdatedAt = DateTime.Now.ToString();
            return await _usersCollection.UpdateAsync(newUser);
        }

        public Task<bool> DeleteDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAllDocumentsAsync(object request = null)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetDocumentAsync(object request)
        {
            GeneralFetchRequest request_ = (GeneralFetchRequest)request;
            Users user =  await _usersCollection.GetAsync(request_.Code);
            var copier = new ClassValueCopier();

            if (user.IsCommonUser)
            {
                return copier.ConvertAndCopy<CommonUser_EM, Users>(user);
            }
            else
            {
                return copier.ConvertAndCopy<AdminUser_EM, Users>(user);
            }
        }

        public Task<bool> UpdateDocumentAsync(object request)
        {
            throw new NotImplementedException();     
        }

        public async Task<bool> UpdateVolunteeringPreferenceAsync(UpdateVolunteeringPreferenceRequest request)
        {
            //get the data
            //update the data 
            //push it to DB
            var copier = new ClassValueCopier();
            var olduser = await _usersCollection.GetAsync(request.UserCode);
            var newuser = copier.ConvertAndCopy(request, olduser);
            newuser.LastUpdatedBy = request.UserCode;
            return await _usersCollection.UpdateAsync(newuser);
        }

        private async Task<bool> PublishUserCredentialAsync(Users newUser)
        {
            var copier = new ClassValueCopier();
            NewUser loginUser = copier.ConvertAndCopy<NewUser, Users>(newUser);
            string data = JsonConvert.SerializeObject(loginUser);
            await Mqtt.MqttPublish("RSCD/Message/AddNewUser", data);
            return true;
        }

        }
    }

