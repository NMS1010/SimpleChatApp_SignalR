using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Interfaces
{
    public interface IUploadService
    {
        Task<string> UploadFile(IFormFile file);

        string GetFile(string filename);

        Task DeleteFile(string filename);
    }
}