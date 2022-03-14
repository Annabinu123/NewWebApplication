using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace NewWebApplication.Controllers
{
    public class ExcelFileUploaderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("ExcelFileUploader")]
        public async Task<IActionResult> Index(IFormFile files)
        {
            string message = null;
            if (files == null || files.Length == 0)
            {
                message = "Please upload a valid excel file";
            }

            else
            {
                string connectionstring = "DefaultEndpointsProtocol=https;AccountName=annabinuaccount;AccountKey=FTXjugxhZNgGpk4ez5inE903dmiGptkQe+bIh91GtW8sMnMW+Awdp4m72+JtkzeQ0kkBfaeeteWv+AStIN5kcw==;EndpointSuffix=core.windows.net";
                string blobContainerName = "excelfile";
                BlobClient blobClient = new BlobClient(connectionString: connectionstring, blobContainerName: blobContainerName, blobName: files.FileName);
                try
                {
                    var result = await blobClient.UploadAsync(files.OpenReadStream());
                    message = files.FileName + " successfully uploaded to blob storage";
                }
                catch (Exception)
                {
                    message = "An Error occored, please try again later!";
                }
            }
            return Ok(message);
        }
    }
}