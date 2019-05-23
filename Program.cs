using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobContinue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 for uploading ,2 for dowloaading, 3 for moving ");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 1)
            {
                //this is to upload
                var test_upload = new UploadFile();
                Console.WriteLine("Enter the Filename you want to upload");
                string src = Console.ReadLine();
                test_upload.setSource(src);
                test_upload.Upload().GetAwaiter().GetResult();
                Console.ReadKey();
            }
            else if (choice == 2)
            {

                //this is to download
                var test_download = new DownloadFile();
                Console.WriteLine("Enter the file name you want to download. This will get downloaded in Dcoment folder by default");
                string dest = Console.ReadLine();
                test_download.setDest(dest);
                test_download.Download().GetAwaiter().GetResult();
                Console.ReadKey();
            }

            else
            {
                //this is to delete a particular file from a container
                var test_delete = new DeleteFile();
                Console.WriteLine("Enter the file name you want to delete. This will get deleted in Dcoment folder by default");
                string dest = Console.ReadLine();
                test_delete.setDest(dest);
                test_delete.Delete().GetAwaiter().GetResult();
                Console.ReadKey();
            }
        }
    }
}
