
using System;
using System.IO;

namespace Microsoft.AspNetCore.Http
{
    public static class IFormFileExtension
    {
        public static string ToBase64(this IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    formFile.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    return Convert.ToBase64String(fileBytes);
                    
                }
            }

            return string.Empty;
        }
    }
}
