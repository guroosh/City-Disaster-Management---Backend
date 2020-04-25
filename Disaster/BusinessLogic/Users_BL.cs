﻿using Disaster.DataAccess.Repository;
using Disaster.Model.DB;
using RSCD.BLL;
using RSCD.Helper;
using RSCD.Model.Custom.MinimalDetails;
using RSCD.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster.BusinessLogic
{
    public class Users_BL : IBusinessLogic
    {
        private readonly IUsersCollection _usersCollection;
        public Users_BL(IUsersCollection usersCollection)
        {
            _usersCollection = usersCollection;
        }

        public async Task<bool> CreateAsync(object request)
        {
            UserDetailMessage request_ = (UserDetailMessage)request;
            var copier = new ClassValueCopier();
            UsersDetail users = copier.ConvertAndCopy<UsersDetail, UserDetailMessage>(request_);
            bool result = await _usersCollection.AddAsync(users);
            return result;
        }

        public Task<bool> DeleteDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }


        public async Task<object> GetDocumentAsync(object request)
        {
            string request_ = (string)request;
            UsersDetail user = await _usersCollection.GetAsync(request_);
            var copier = new ClassValueCopier();

            if (user.IsCommonUser)
            {
                return copier.ConvertAndCopy<CommonUserDetails_minimal, UsersDetail>(user);
            }
            else
            {
                return copier.ConvertAndCopy<AdminUserDetails_minimal, UsersDetail>(user);
            }
        }

        public async Task<bool> UpdateDocumentAsync(object request)
        {
            // recive the request
            UserDetailMessage request_ = (UserDetailMessage)request;
            var copier = new ClassValueCopier();
            UsersDetail oldUserCredentials = await _usersCollection.GetAsync(request_.ReferenceCode);
            //update the credentials
            UsersDetail updatedUsers = copier.ConvertAndCopy(request_, oldUserCredentials);
            updatedUsers.LastUpdatedBy = request_.ReferenceCode;
            updatedUsers.LastUpdatedAt = DateTime.Now.ToString();
            //push to DB
            bool result = await _usersCollection.UpdateAsync(updatedUsers);
            return result;
        }

        //function not used
        public Task<object> GetAllDocumentsAsync(object request = null)
        {
            throw new NotImplementedException();
        }
    }
}
