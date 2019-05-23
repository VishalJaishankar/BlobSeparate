using System;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

namespace BlobContinue
{   //to move a file from one container to another
    class Traansfer
    {
        //specify the source file and container name . specify the destination container name
        //first download from one container and then upload it into other and delete from system after that


        CloudStorageAccount storageAccount = null;
        CloudStorageAccount GetStorageAccount()
        {
            return storageAccount;
        }


        CloudBlobContainer cloudBlobContainer = null;
        CloudBlobContainer GetCloudBlobContainer()
        {
            return cloudBlobContainer;
        }
        string sourceFile;
        public void setSource(string _sourceFile)
        {
            sourceFile = _sourceFile;
        }


        string destinationFile;
        public void setDest(string _destinationFile)
        {
            destinationFile = _destinationFile;
        }

        string storageConnectionString = Environment.GetEnvironmentVariable("storageconnectionstring");
        public string GetStorageConnectionString()
        {
            return storageConnectionString;
        }

        public async Task TransferFile()
        {
            Console.WriteLine("Enter the file name and container name you want to take from");
            var _sourceFile = Console.ReadLine();
            var downloadFile = new DownloadFile();
            downloadFile.setDest(_sourceFile);
            downloadFile.Download().GetAwaiter().GetResult();
            Console.ReadKey();
            //here you will hve our file in mydocs the upload this to a new container/old if you want and del this locally
            //start here
        }
    }
}
