﻿using AwesomeCare.DataTransferObject.Validations;
using AwesomeCare.DataTransferObject.DTOs.Staff;
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
    public class CreateComplainRegister
    {

        public CreateComplainRegister()
        {
            STAFFINVOLVED = new List<GetStaffs>();
            OFFICERTOACT = new List<GetStaffs>();
        }
        public int ComplainId { get; set; }
        public int ClientId { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile Evidence { get; set; }
        #region DropDowns
        public IEnumerable<GetStaffs> OFFICERTOACT { get; set; }
        public IEnumerable<GetStaffs> STAFFINVOLVED { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        #endregion

        #region Tabs
        public string ActiveTab { get; set; } = "complaintregister";
        // public string[] Tabs { get; set; } = new string[] { "" };
        #endregion
        public string ClientName { get; set; }
        #region ComplainRegister
        [Required]
        [MaxLength(255)]
        public string LINK { get; set; }
        [Required]
        [MaxLength(50)]
        public string IRFNUMBER { get; set; }
        [Required]
        public DateTime INCIDENTDATE { get; set; }
        [Required]
        public DateTime DATERECIEVED { get; set; }
        [Required]
        public DateTime? DATEOFACKNOWLEDGEMENT { get; set; }
        [Required]
        [Display(Name = "OFFICERTOACT")]
        public int OFFICERTOACTId { get; set; }
        [Required]
        [MaxLength(255)]
        public string SOURCEOFCOMPLAINTS { get; set; }

        [Required]
        [MaxLength(50)]
        public string COMPLAINANTCONTACT { get; set; }
        [Required]
        [Display(Name = "STAFFINVOLVED")]
        public int STAFFId { get; set; }
        [Required]
        [MaxLength(255)]
        public string CONCERNSRAISED { get; set; }
        [Required]
        public DateTime DUEDATE { get; set; }
        [Required]
        [MaxLength(255)]
        public string LETTERTOSTAFF { get; set; }
        [Required]
        [MaxLength(255)]
        public string INVESTIGATIONOUTCOME { get; set; }
        [Required]
        [MaxLength(50)]
        public string ACTIONTAKEN { get; set; }
        [Required]
        [MaxLength(255)]
        public string FINALRESPONSETOFAMILY  { get; set; }
        [Required]
        [MaxLength(50)]
        public string ROOTCAUSE { get; set; }
        [MaxLength(255)]
        public string REMARK { get; set; }
        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        public string EvidenceFilePath { get; set; }
        public async Task SaveFileToDisk(IWebHostEnvironment env)
        {
            string filePath = GetFilePath(env);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await this.Evidence.CopyToAsync(stream);
            }
        }
        public void DeleteFileFromDisk(IWebHostEnvironment env)
        {
            string filePath = GetFilePath(env);
            System.IO.File.Delete(filePath);
        }
        string GetFilePath(IWebHostEnvironment env)
        {
            string fileName = string.Concat(ComplainId, Path.GetExtension(Evidence.FileName));
            string filePath = Path.Combine(env.ContentRootPath, "Uploads", fileName);
            return filePath;
        }
        #endregion
    }

}
