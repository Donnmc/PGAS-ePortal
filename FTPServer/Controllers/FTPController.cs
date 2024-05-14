using Microsoft.AspNetCore.Mvc;

namespace FTPServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FTPController : ControllerBase
    {
        private readonly FtpHelper ftpHelper;

        public FTPController(FtpHelper ftpHelper)
        {
            this.ftpHelper = ftpHelper ?? throw new ArgumentNullException(nameof(ftpHelper));
        }

        [HttpPost("upload")]
        public IActionResult UploadFile([FromForm] Microsoft.AspNetCore.Http.IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    // Upload file to FTP server
                    ftpHelper.UploadFile(file.OpenReadStream(), file.FileName);

                    return Ok("File uploaded to FTP server successfully.");
                }

                return BadRequest("No file uploaded.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error uploading file: {ex.Message}");
            }
        }

        [HttpGet("download")]
        public IActionResult DownloadFile(string remoteFilePath, string localFilePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(remoteFilePath) && !string.IsNullOrEmpty(localFilePath))
                {
                    // Download file from FTP server
                    ftpHelper.DownloadFile(remoteFilePath, localFilePath);

                    return Ok("File downloaded from FTP server successfully.");
                }

                return BadRequest("Invalid file paths.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error downloading file: {ex.Message}");
            }
        }
    }
}
