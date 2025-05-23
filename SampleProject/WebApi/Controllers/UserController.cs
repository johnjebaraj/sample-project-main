﻿using BusinessEntities;
using Core.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Xml.Linq;
using WebApi.Models.Users;

namespace WebApi.Controllers
{
    [RoutePrefix("users")]
    public class UserController : BaseApiController
    {
        private readonly ICreateUserService _createUserService;
        private readonly IDeleteUserService _deleteUserService;
        private readonly IGetUserService _getUserService;
        private readonly IUpdateUserService _updateUserService;

        public UserController(ICreateUserService createUserService, IDeleteUserService deleteUserService, IGetUserService getUserService, IUpdateUserService updateUserService)
        {
            _createUserService = createUserService;
            _deleteUserService = deleteUserService;
            _getUserService = getUserService;
            _updateUserService = updateUserService;
        }

        [Route("{userId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateUser(Guid userId, [FromBody] UserModel model)
        {
            var user = _getUserService.GetUser(userId);
            if (user != null)
            {
                return AlreadyExist(string.Format("User '{0}' already exist with id '{1}'. Consider changing the ID or Delete the existing user and attempt again", user.Name, user.Id));
            }
            user = _createUserService.Create(userId, model.Name, model.Email, model.Type, model.AnnualSalary, model.Age, model.Tags);
            return Found(new UserData(user));
        }

        [Route("{userId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateUser(Guid userId, [FromBody] UserModel model)
        {
            var user = _getUserService.GetUser(userId);
            if (user == null)
            {
                return DoesNotExist();
            }
            if (!ModelState.IsValid)
            {
                var modelValidationErrors = String.Join(":", ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)));
                return InvalidData(string.Format("Updating User '{0}' Failed with validation Error(s) '{1}'", user.Name, modelValidationErrors));
            }
            _updateUserService.Update(user, model.Name, model.Email, model.Type, model.AnnualSalary, model.Age, model.Tags);
            return Found(new UserData(user));
        }

        [Route("{userId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteUser(Guid userId)
        {
            var user = _getUserService.GetUser(userId);
            if (user == null)
            {
                return DoesNotExist();
            }
            _deleteUserService.Delete(user);
            return Found();
        }

        [Route("{userId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetUser(Guid userId)
        {
            var user = _getUserService.GetUser(userId);
            return Found(new UserData(user));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetUsers(int? skip, int? take, UserTypes? type = null, string name = null, string email = null,string tag = null)
        {
            //As per RevenDB, queries will return up to 1024 results due to the server default max page size value.
            var users = _getUserService.GetUsers(type, name, email,tag)
                                      .Skip(skip.HasValue ? skip.Value : 0).Take(take.HasValue ? take.Value : 9999)
                                       .Select(q => new UserData(q))
                                       .ToList();
            return Found(users);
        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllUsers()
        {
            _deleteUserService.DeleteAll();
            return Found();
        }

        [Route("list/tag")]
        [HttpGet]
        public HttpResponseMessage GetUsersByTag(string tag)
        {
            return this.GetUsers(null,null,null, null, null, tag);
        }
    }
}