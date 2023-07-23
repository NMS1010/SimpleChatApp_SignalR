using Microsoft.AspNetCore.Http;
using Social_Backend.Application.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Helpers
{
    public class MessageHelpers
    {
        public static string GetMessageTypeFromFile(IFormFile file)
        {
            FileInfo fi = new(file.FileName);
            var fileExt = fi.Extension.ToLower();

            if (fileExt.Contains("mp3"))
                return MESSAGE_TYPE.AUDIO;

            if (fileExt.Contains("pdf"))
                return MESSAGE_TYPE.FILE;
            if (fileExt.Contains("doc"))
                return MESSAGE_TYPE.FILE;
            if (fileExt.Contains("docx"))
                return MESSAGE_TYPE.FILE;
            if (fileExt.Contains("xls"))
                return MESSAGE_TYPE.FILE;
            if (fileExt.Contains("xlsx"))
                return MESSAGE_TYPE.FILE;
            if (fileExt.Contains("txt"))
                return MESSAGE_TYPE.FILE;

            if (fileExt.Contains("mp4"))
                return MESSAGE_TYPE.VIDEO;
            if (fileExt.Contains("mov"))
                return MESSAGE_TYPE.VIDEO;
            if (fileExt.Contains("wmv"))
                return MESSAGE_TYPE.VIDEO;
            if (fileExt.Contains("avi"))
                return MESSAGE_TYPE.VIDEO;
            if (fileExt.Contains("mkv"))
                return MESSAGE_TYPE.VIDEO;
            if (fileExt.Contains("flv"))
                return MESSAGE_TYPE.VIDEO;
            if (fileExt.Contains("webm"))
                return MESSAGE_TYPE.VIDEO;

            if (fileExt.Contains("png"))
                return MESSAGE_TYPE.IMAGE;
            if (fileExt.Contains("jpg"))
                return MESSAGE_TYPE.IMAGE;

            return MESSAGE_TYPE.UNDEFINED;
        }
    }
}