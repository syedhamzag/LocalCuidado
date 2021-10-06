using AwesomeCare.DataTransferObject.DTOs.FileUpload;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FileUploadController : ControllerBase
    {
        private readonly ILogger<FileUploadController> logger;
        private readonly IFileUpload fileUpload;

        public FileUploadController(ILogger<FileUploadController> logger,
            IFileUpload fileUpload)
        {
            this.logger = logger;
            this.fileUpload = fileUpload;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(string), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromForm] PostFile model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Upload the file if less than 2 MB
            if (model.File.Length > 2097152)
            {
                ModelState.AddModelError(nameof(model.File), "Max file size is 2MB");
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrWhiteSpace(model.FolderName))
            {
                model.FolderName = model.FolderName.ToLower();
            }

            string filename = string.Concat(model.FileName.Replace(" ", ""), Path.GetExtension(model.File.FileName));

            var result = await fileUpload.UploadFile(model.FolderName, true, filename, model.File.OpenReadStream());

            return Ok(result);
        }
    }
}
