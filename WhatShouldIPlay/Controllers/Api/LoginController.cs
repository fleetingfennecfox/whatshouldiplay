using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet, Route("echo/{msg}")]
        public HttpResponseMessage EchoBack(string msg)
        {
            return Request.CreateResponse(HttpStatusCode.OK, msg);
        }

        [HttpPost, AllowAnonymous]
        public HttpResponseMessage Login(LoginUser model)
        {
            bool res = false;
            LoginService loginService = new LoginService();

            try
            {
                res = loginService.Login(model);
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
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