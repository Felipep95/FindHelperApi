using Microsoft.AspNetCore.Http;

namespace FindHelperApi.Models
{
    public class FileUpload
    {
        public IFormFile files { get; set; }
    }
}
