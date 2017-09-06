﻿using DentalDiary.Data;
using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DentalDiary.Controllers
{
    public class DropBoxController : ApiController
    {
        DiaryContext db = new DiaryContext();

        [HttpPost]
        [Route("addimages")]
        public async Task GetUser()
        {
            var dbx = new DropboxClient("zmZXUjw_0iAAAAAAAAAAHcDiDjUBP7lc7plc576DbJdaEerfaT90i6kAj0081TLu");
            var httpRequest = HttpContext.Current.Request;
            var imageInfo = HttpContext.Current.Request.Form;
            var folderName = imageInfo.Get("id");
            var file5 = imageInfo.Get("file");
            var id = Convert.ToInt32(folderName);
            var person = db.Persons.Single(p => p.Id == id);
            if(person.LinkToImages == null)
            {
                var folder = await dbx.Files.CreateFolderAsync("/" + folderName);
                var baseUrl = "https://www.dropbox.com/home/Приложения/DanteDiary/";
                person.LinkToImages = baseUrl + folderName;
                db.SaveChanges();
            }
            var files = httpRequest.Files;

            foreach(string file in files)
            {
                var postedFile = httpRequest.Files[file];
                var fileName = postedFile.FileName;

                var upload = await dbx.Files.UploadAsync("/" + folderName + "/" + fileName,
                        WriteMode.Overwrite.Instance,
                        body: postedFile.InputStream
                    );
            }
            
        }
    }
}