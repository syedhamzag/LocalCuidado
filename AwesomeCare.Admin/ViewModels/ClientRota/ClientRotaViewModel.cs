using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaName;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.ClientRota
{
    public class ClientRotaViewModel
    {
        public ClientRotaViewModel()
        {
            Rotas = new List<GetClientRotaName>();
            Genders = new List<SelectListItem>();
        }

      //public  void GetGendersFromCache()
      //  {
      //      List<SelectListItem> selectListItems = new List<SelectListItem>();
      //      string key = "Gender";
      //      if (Cache.TryGetValue(key, out List<GetBaseRecordWithItems> baseRecords))
      //      {

      //          selectListItems = (from rec in baseRecords
      //                             where rec.KeyName == key
      //                             from recItem in rec.BaseRecordItems
      //                             select new SelectListItem
      //                             {
      //                                 Selected = false,
      //                                 Text = recItem.ValueName,
      //                                 Value = recItem.BaseRecordItemId.ToString()
      //                             }).ToList();
      //      }

      //      Genders = selectListItems;
      //  }
        public string SubTitle { get; set; } = "Add Rota";
        [Required]
        public int RotaId { get; set; }
        [Display(Name = "Number of Staff")]
        [Required]
        public int NumberOfStaff { get; set; }
        [Required]
        [Display(Name = "Rota Name")]
        public string RotaName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Area { get; set; }
        public List<GetClientRotaName> Rotas { get; set; }
        public bool Deleted { get; set; } = false;
        public List<SelectListItem> Genders { get; set; }
    }
}
