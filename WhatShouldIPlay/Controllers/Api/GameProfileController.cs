using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WhatShouldIPlay.Models.Domain;
using WhatShouldIPlay.Models.Request;
using WhatShouldIPlay.Services;

namespace WhatShouldIPlay.Controllers.Api
{
    [RoutePrefix("api/games")]
    public class GameProfileController : ApiController
    {
        [HttpPost, AllowAnonymous, Route("add")]
        public HttpResponseMessage AddGame(GameProfileRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Field is invalid");
            }

            int res = 0;
            GameProfileService gPSvc = new GameProfileService();

            try
            {
                res = gPSvc.AddGame(model);
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet, AllowAnonymous, Route("getall")]
        public HttpResponseMessage SelectAllGames()
        {
            List<GameProfile> gamesList = new List<GameProfile>();
            GameProfileService gPSvc = new GameProfileService();

            try
            {
                gamesList = gPSvc.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, gamesList);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet, AllowAnonymous, Route("get/{id:int}")]
        public HttpResponseMessage SelectGameById(int id)
        {
            GameProfile res = new GameProfile();
            GameProfileService gPSvc = new GameProfileService();

            try
            {
                res = gPSvc.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut, AllowAnonymous, Route("update")]
        public HttpResponseMessage UpdateGame(GameProfileRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Field is invalid");
            }

            bool res = false;
            GameProfileService gPSvc = new GameProfileService();

            try
            {
                res = gPSvc.UpdateGame(model);
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete, AllowAnonymous, Route("delete/{id:int}")]
        public HttpResponseMessage DeleteGame(int id)
        {
            bool res = false;
            GameProfileService gPSvc = new GameProfileService();

            try
            {
                res = gPSvc.DeleteGame(id);
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet, AllowAnonymous, Route("attributes")]
        public HttpResponseMessage SelectAllAttributes()
        {
            GameAttributes attributes = new GameAttributes();
            GameProfileService gPSvc = new GameProfileService();

            try
            {
                attributes = gPSvc.SelectAllAttributes();
                return Request.CreateResponse(HttpStatusCode.OK, attributes);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}