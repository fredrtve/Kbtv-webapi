using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Application.Common
{
    public class StorageFileUrl
    {
        public Uri FileUrl { get; set; }

        public StorageFileUrl(string FileName, string Folder)
        {
            FileUrl = new Uri($"https://kbtv.blob.core.windows.net/{Folder}/{FileName}");
        }
    }
}
