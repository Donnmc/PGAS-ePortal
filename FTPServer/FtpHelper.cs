using System.Net;

public class FtpHelper
{
    private readonly string ftpServer;
    private readonly string ftpUsername;
    private readonly string ftpPassword;

    public FtpHelper()
    {
        // Replace these values with your actual FTP server details
        ftpServer = "ftp://192.168.110.105";
        ftpUsername = "test";
        ftpPassword = "testing123";
    }

    public void UploadFile(Stream fileStream, string fileName)
    {
        try
        {
            var request = (FtpWebRequest)WebRequest.Create($"{ftpServer}/{fileName}");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

            using (var ftpStream = request.GetRequestStream())
            {
                fileStream.CopyTo(ftpStream);
            }

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void DownloadFile(string remoteFilePath, string localFilePath)
    {
        try
        {
            var request = (FtpWebRequest)WebRequest.Create($"{ftpServer}/{remoteFilePath}");
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

            using (var response = (FtpWebResponse)request.GetResponse())
            using (var ftpStream = response.GetResponseStream())
            using (var fileStream = File.Create(localFilePath))
            {
                ftpStream.CopyTo(fileStream);
            }

            Console.WriteLine($"Download File Complete");
        }
        catch (WebException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
