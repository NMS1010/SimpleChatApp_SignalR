using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Helpers
{
    public class FileUploadHelper
    {
        private IHttpContextAccessor _httpContextAccessor;
        private const string USER_CONTENT_FOLDER = "user-content";

        public FileUploadHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetFile(string filename)
        {
            var req = _httpContextAccessor.HttpContext.Request;
            var path = $"{req.Scheme}://{req.Host}/{USER_CONTENT_FOLDER}/{filename}";
            return path;
        }
    }
}