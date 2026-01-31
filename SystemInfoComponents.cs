using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Graph;

[ComVisible(true)]
public class SystemInfoComponents
{

    public string GetStorage()
    {
        DriveInfo drive = new DriveInfo(Path.GetPathRoot(
            Environment.GetFolderPath(Environment.SpecialFolder.System)));

        long total = drive.TotalSize;
        long used = total - drive.AvailableFreeSpace;

        double total_gb = (total / (1024.0 * 1024.0) / 1024.0);

        double used_gb = (used / (1024.0 * 1024.0) / 1024.0);

        double[] storageFolders = GetStorageInfo();
        var obj =  new StorageInfo
        {
            TotalBytes = total_gb,
            UsedBytes = used_gb,
            Desktop = storageFolders[0],
            Documents = storageFolders[1],
            Downloads = storageFolders[2],
            Pictures = storageFolders[3],
            Videos = storageFolders[4],

        };
        return System.Text.Json.JsonSerializer.Serialize(obj);
    }
    public double[] GetStorageInfo()
    {
        double[] storage;
        string Desktop_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string Documents_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        string downloads_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        string pictures_path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        string videos_path = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);


        double Desktop = GetFolderSize(Desktop_path);
        double Documents = GetFolderSize(Documents_path);
        double Downloads = GetFolderSize(downloads_path);
        double Pictures = GetFolderSize(pictures_path);
        double Videos = GetFolderSize(videos_path);
        storage = [Desktop, Documents, Downloads, Pictures, Videos];
        return storage;

    }

    public double GetFolderSize(string path)
    {
        long size = 0;
        foreach (var file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
        {
            try { size += new FileInfo(file).Length; }
            catch { }
        }

        double mb = size / (1024.0 * 1024.0);
        double gb = (mb / 1024.0);

        return gb;
    }
}


[ComVisible(true)]
public class StorageInfo
{
    public double TotalBytes { get; set; }
    public double UsedBytes { get; set; }
    public double Desktop { get; set; }
    public double Documents { get; set; }
    public double Downloads { get; set; }
    public double Pictures { get; set; }
    public double Videos { get; set; }

}
