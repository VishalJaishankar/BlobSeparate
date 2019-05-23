using System;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

namespace BlobContinue
{
    class DeleteFile
    {
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

        //any process can be done using the file name and container name

        public async Task Delete()
        {
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {
                    Console.WriteLine("Enter the container name");
                    string _containerName = Console.ReadLine();
                    Console.WriteLine("here1");
                    CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnectionString);
                    Console.WriteLine("here2");
                    CloudBlobClient _blobClient = cloudStorageAccount.CreateCloudBlobClient();
                    Console.WriteLine("here3");
                    CloudBlobContainer _cloudBlobContainer = _blobClient.GetContainerReference(_containerName);
                    Console.WriteLine("here4");
                    CloudBlockBlob _blockBlob = _cloudBlobContainer.GetBlockBlobReference("file7.txt");
                    Console.WriteLine("here1");
                    //delete blob from container    
                    await _cloudBlobContainer.DeleteIfExistsAsync();//deleting the whole container
                    //not able to delete whole block..have to see 
                    Console.WriteLine("here1");



                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error returned from the service: {0}", ex.Message);
                    Console.ReadKey();
                }
                /*
                finally
                 {
                     Console.WriteLine("Press any key to delete the sample files and example container.");
                     Console.ReadLine();
                     Console.WriteLine("Deleting the container and any blobs it contains");
                     if (cloudBlobContainer != null)
                     {
                         await cloudBlobContainer.DeleteIfExistsAsync();
                     }
                     Console.WriteLine("Deleting the local source file and local downloaded files");
                     Console.WriteLine();
                     File.Delete(sourceFile);
                     File.Delete(destinationFile);
                 }
                 */
            }
            else
            {
                Console.WriteLine(
                   "A connection string has not been defined in the system environment variables. " +
                   "Add a environment variable named 'storageconnectionstring' with your storage " +
                   "connection string as a value.");
            }
        }
    }
}
