using System.IO;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.BZip2;


namespace DataIngestion.TestAssignment.Utils
{
    public class ExtractFileHelper
    {
        public async Task Extract(byte[] message)
        {
            await using var stream = new MemoryStream(message);
            await using var inStream = new BZip2InputStream(stream);
            await using var createFile = File.Create("linkFireCollection");
            
            var buffer = new byte[65536];
            int bytesRead;
            
            while ((bytesRead = stream.Read(buffer, 0, 65536)) != 0)
            {
              await createFile.WriteAsync(buffer, 0, bytesRead);
            }
        }
    }
}