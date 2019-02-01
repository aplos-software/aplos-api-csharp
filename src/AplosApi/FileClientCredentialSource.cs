using System.IO;

namespace AplosApi
{
    internal class FileClientCredentialSource : IClientCredentialSource
    {
        public FileClientCredentialSource(string keyFile)
        {
            using (var file = File.OpenRead(keyFile))
            using (var reader = new StreamReader(file))
            {
                ClientId = Path.GetFileName(file.Name).Replace(".key", "");
                Key = reader.ReadToEnd();
            }
        }

        public string ClientId { get; }
        public string Key { get; }
    }
}