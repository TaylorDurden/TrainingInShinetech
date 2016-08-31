using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace BugManagemnet.WebAPI.Controllers
{
    public class FilesController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            
            var originalFileName = GetDeserializedFileName(result.FileData.First());

            var uploadFileInfo = new FileInfo(result.FileData.First().LocalFileName);
            
            //var filleUploadObj = GetFormData<UploadDataModel>(result);

            var returnData = "ReturnTest";
            return Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        }

        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var uploadFolder = "~/App_Data/FileUploads";
            
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }
        
        private object GetFormData<T>(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData.GetValues(0).FirstOrDefault() ?? String.Empty);

                if (!String.IsNullOrEmpty(unescapedFormData))
                {
                    return JsonConvert.DeserializeObject<T>(unescapedFormData);
                }
            }
            return null;
        }
        
        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString(); 
        }
        
        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }
    }
}