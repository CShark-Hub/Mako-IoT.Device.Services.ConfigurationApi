using MakoIoT.Device.Services.Interface;

namespace MakoIoT.Device.Services.ConfigurationApi.Test.Mocks
{
    public class MockStorageService : IStorageService
    {
        public void WriteToFile(string fileName, string text)
        {
            throw new System.NotImplementedException();
        }

        public bool FileExists(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public string ReadFile(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteFile(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetFiles()
        {
            throw new System.NotImplementedException();
        }

        public string[] GetFileNames()
        {
            throw new System.NotImplementedException();
        }

        public long GetFileSize(string fileName)
        {
            throw new System.NotImplementedException();
        }
    }
}
