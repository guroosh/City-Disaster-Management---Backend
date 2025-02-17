﻿using Registration.Model.API;
using RSCD.BLL;
using System;
using System.Threading.Tasks;
using RSCD.Helper;
using Registration.Model.DB;
using Registration.DataAccess.Repository;
using RSCD.Models.API;
using RSCD.Model.Custom.ExternalModel.Registration;
using Newtonsoft.Json;
using RSCD.Mqtt;
using RSCD.Model.Message;

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
            //check for used existance
            var result = await  _usersCollection.CheckUserExistance(request.EmailId);

            if (!result)
            {
                throw new Exception("User Exists");
            }

            //if no create user
            Users newUser = copier.ConvertAndCopy<Users, RegisterCommonUserRequest>(request); 
            newUser.Role = "CommonUser";
            newUser.IsCommonUser = true;
            
            //push to DB
            newUser = await _usersCollection.RegisterUserAsync(newUser);

            //Send the userDetails to all the service
            await PublishUserCredentialAsync(newUser);

            
            return true;
        }

        public async Task<bool> RegisterAdminUser(RegisterAdminUserRequest request)
        {
            var copier = new ClassValueCopier();
            //check for used existance
            var result = await _usersCollection.CheckUserExistance(request.EmailId);

            if (!result)
            {
                throw new Exception("User Exists");
            }

            Users newUser = copier.ConvertAndCopy<Users, RegisterAdminUserRequest>(request);
            newUser.IsCommonUser = false;
            newUser.Password = newUser.BadgeId;
            newUser = await _usersCollection.RegisterUserAsync(newUser);

            await PublishUserCredentialAsync(newUser);
            return true;
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

        public async Task<bool> PublishUserCredentialAsync(Users newUser)
        {
            try
            {
                var copier = new ClassValueCopier();
                var message = copier.ConvertAndCopy<UserDetailMessage, Users>(newUser);
                string data = JsonConvert.SerializeObject(message);
                await Mqtt.MqttPublish("RSCD/Message/Registration/userCreated", data);
                return true;
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        }
    }

