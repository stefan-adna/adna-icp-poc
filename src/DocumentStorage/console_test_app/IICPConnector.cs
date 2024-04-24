using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_test_app
{
    internal interface IICPConnector
    {
        public Task<bool> UploadDocument(string identifier, string filePath);
        public Task<byte[]> DownloadDocument(string identifier);
    }
}
