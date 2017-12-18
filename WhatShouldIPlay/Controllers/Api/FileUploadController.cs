using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WhatShouldIPlay.Models.Domain;
using WhatShouldIPlay.Models.Request;
using WhatShouldIPlay.Services;

namespace WhatShouldIPlay.Controllers.Api
{
    [RoutePrefix("api/upload")]
    public class FileUploadController : ApiController
    {
        FileUploadService svc = new FileUploadService();

        [HttpPost, Route("file")]
        public HttpResponseMessage FilePost(EncodedImage encodedImage)
        {
            try
            {
                byte[] newBytes = Convert.FromBase64String(encodedImage.EncodedImageFile);

                UserFile model = new UserFile();
                model.UserFileName = "appimg";
                model.ByteArray = newBytes;
                model.Extension = encodedImage.FileExtension;
                model.SaveLocation = "Images";
                //model.UserId = 1;

                int fileId = svc.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, fileId);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}