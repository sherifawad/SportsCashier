using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SportsCashier.Common.DataBase;
using SportsCashier.Droid;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

[assembly: Dependency(typeof(DownloadPath))]
namespace SportsCashier.Droid
{
    public class DownloadPath : IDownloadPath
    {
        public string Get()
        {
            //var status = await CheckAndRequestPermissionAsync(new Permissions.StorageWrite());
            //if (status != PermissionStatus.Granted)
            //{
            //    // Notify user permission was denied
            //    return string.Empty;
            //}
            return Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
        }


        public async Task<PermissionStatus> CheckAndRequestPermissionAsync<T>(T permission)
                where T : BasePermission
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }

            return status;
        }
    }
}