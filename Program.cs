using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

DirectoryInfo di = new DirectoryInfo(@"E:\develop\C#\ChangeImageInfo\image");
IEnumerable<FileInfo> files = di.EnumerateFiles("*", SearchOption.AllDirectories);

Console.WriteLine(files.Count<FileInfo>());

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    foreach (FileInfo file in files)
    {
        Console.WriteLine(file.FullName);

        Bitmap image = new Bitmap(file.FullName);
        PropertyItem[] propItems = image.PropertyItems;
        foreach (PropertyItem item in propItems)
        {
            if (item.Type == 2)
            {
                if (item.Id == 0x9003)
                {
                    string val = Encoding.ASCII.GetString(item.Value);
                    val = val.Trim(new char[] { '\0' });
                    DateTime dt = DateTime.ParseExact(val, "yyyy:MM:dd HH:mm:ss", null);
                    Console.WriteLine(dt);
                    File.SetCreationTime(@file.FullName, dt);
                    File.SetLastWriteTime(@file.FullName, dt);
                }
            }
        }
    }
}