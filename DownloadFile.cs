using System;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

namespace BlobContinue
{
    class DownloadFile
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

        public async Task Download()
        {
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {


                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                    //create a option for adding in existing or creating a new container
                    Console.WriteLine("enter the name of the container");
                    string containerName = Console.ReadLine();
                    cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
                    //try finding a method for checking existance of a container
                    Console.WriteLine("is this a new container? 1 for yes ,2 for no");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 1)
                    {
                        Console.WriteLine("Cant download from new container.Error");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Accessing Existing Container.");
                    }

                    //await cloudBlobContainer.CreateAsync();
                    //Console.WriteLine("Created container '{0}'", cloudBlobContainer.Name);
                    Console.WriteLine();
                    BlobContainerPermissions permissions = new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    };
                    await cloudBlobContainer.SetPermissionsAsync(permissions);
                    //till here the container is created ..now you were put a file that one
                    /*
                     string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                     string localFileName = "QuickStart_" + Guid.NewGuid().ToString() + ".txt";
                     sourceFile = Path.Combine(localPath, localFileName);
                     // Write text to the file.
                     File.WriteAllText(sourceFile, "Hello, World!");
                     */
                    //here you can tell what path it has to take  the
                    string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string localFileName = destinationFile;
                    destinationFile = Path.Combine(localPath, localFileName);
                    Console.WriteLine("Temp file = {0}", destinationFile);
                    Console.WriteLine("Downloading from Blob storage as blob '{0}'", localFileName);
                    Console.WriteLine();

                    //this were very important 
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);
                    //await cloudBlockBlob.UploadFromFileAsync(sourceFile);

                    /*
                    // List the blobs in the container.
                    Console.WriteLine("Listing blobs in container.");
                    BlobContinuationToken blobContinuationToken = null;
                    do
                    {
                        var resultSegment = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
                        // Get the value of the continuation token returned by the listing call.
                        blobContinuationToken = resultSegment.ContinuationToken;
                        foreach (IListBlobItem item in resultSegment.Results)
                        {
                            Console.WriteLine(item.Uri);
                        }
                    } while (blobContinuationToken != null); 
                    Console.WriteLine();
                    */
                    
                    //destinationFile = destinationFile.Replace(".txt", "_DOWNLOADED.txt");
                    Console.WriteLine("Downloading blob to {0}", destinationFile);
                    Console.WriteLine();
                    await cloudBlockBlob.DownloadToFileAsync(destinationFile, FileMode.Create);
                    Console.ReadKey();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error returned from the service: {0}", ex.Message);
                    Console.ReadKey();
                }
                /* finally
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
