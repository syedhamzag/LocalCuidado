using AwesomeCare.DataTransferObject.Validations;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Client
{
    public class CreateClient: DataTransferObject.DTOs.Client.PostClient
    {
        
        public CreateClient()
        {
            Gender = new List<SelectListItem> {
                new SelectListItem("Male","Male"),
                new SelectListItem("Female","Female")
            };
            InvolvingParties = new List<ClientInvolvingParty>();
            RegulatoryContacts = new List<ClientRegulatoryContact>();
            CareDetails = new List<ClientCareDetailsHeading>();
        }
        public int ClientId { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght =1)]
        [AllowedExtensions(new string[] { ".png", ".jpg" , ".jpeg" })]
        public IFormFile ClientImage { get; set; }
        #region DropDowns
        public IEnumerable<SelectListItem> Gender { get; set; }

        public bool CanContinue { get; set; }
        #endregion

        #region Tabs
        public string ActiveTab { get; set; } =  "personalInfo";
       // public string[] Tabs { get; set; } = new string[] { "" };
        #endregion

        public List<ClientInvolvingParty> InvolvingParties { get; set; }
        public List<ClientRegulatoryContact> RegulatoryContacts { get; set; }
        public List<ClientCareDetailsHeading> CareDetails { get; set; }
        #region Methods
        public async Task SaveFileToDisk(IHostingEnvironment env)
        {
            string filePath = GetFilePath(env);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await this.ClientImage.CopyToAsync(stream);
            }
        }
        public void DeleteFileFromDisk(IHostingEnvironment env)
        {
            string filePath = GetFilePath(env);
            System.IO.File.Delete(filePath);
        }
        string GetFilePath(IHostingEnvironment env)
        {
            string fileName = string.Concat(IdNumber, Path.GetExtension(ClientImage.FileName));
            string filePath = Path.Combine(env.ContentRootPath, "Uploads", fileName);
            return filePath;
        }
        #endregion
    }

}
