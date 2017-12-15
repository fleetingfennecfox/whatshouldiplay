using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WhatShouldIPlay.Models.Domain;
using WhatShouldIPlay.Services;

namespace WhatShouldIPlay.Controllers.Api
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpPost, AllowAnonymous]
        public HttpResponseMessage Login(User model)
        {
            if(!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Email or Password are invalid");
            }

            bool res = false;
            LoginService loginSvc = new LoginService();

            try
            {
                res = loginSvc.Login(model);
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet, AllowAnonymous]
        public HttpResponseMessage SelectAllUsers()
        {
            List<User> usersList = new List<User>();
            LoginService loginSvc = new LoginService();

            try
            {
                usersList = loginSvc.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, usersList);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}