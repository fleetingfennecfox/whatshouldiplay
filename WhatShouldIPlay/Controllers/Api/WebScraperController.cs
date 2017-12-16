using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WhatShouldIPlay.Models.Domain;
using WhatShouldIPlay.Services;

namespace WhatShouldIPlay.Controllers.Api
{
    [RoutePrefix("api/scraper")]
    public class WebScraperController : ApiController
    {
        [HttpGet, AllowAnonymous, Route("getall")]
        public HttpResponseMessage GetAll()
        {
            List<ScrapedPost> res = new List<ScrapedPost>();
            WebScraperService svc = new WebScraperService();

            try
            {
                res = svc.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpPost, AllowAnonymous, Route("save")]
        public HttpResponseMessage SavePost(ScrapedPost model)
        {
            int res = 0;
            WebScraperService svc = new WebScraperService();

            try
            {
                res = svc.SavePost(model);
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}