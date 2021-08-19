using AwesomeCare.Admin.Services.Nutrition;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Nutrition;
using AwesomeCare.DataTransferObject.DTOs.ClientMealType;
using AwesomeCare.DataTransferObject.DTOs.ClientShopping;
using AwesomeCare.DataTransferObject.DTOs.ClientCleaning;
using AwesomeCare.DataTransferObject.DTOs.RotaDayofWeek;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Extensions;
using AwesomeCare.Admin.Services.RotaDayofWeek;
using Newtonsoft.Json;
using AwesomeCare.DataTransferObject.DTOs.ClientNutrition;
using AwesomeCare.DataTransferObject.DTOs.ClientMealDays;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Hosting;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.Admin.Services.ClientRotaType;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AwesomeCare.Admin.Controllers
{
    public class NutritionController : BaseController
    {
        private INutritionService _nutritionService;
        private IClientRotaTypeService _clientRotaTypeService;
        private IRotaDayofWeekService _MealDayOfWeekService;
        private IStaffService _staffService;
        private IClientService _clientService;
        private readonly IWebHostEnvironment _env;
        private readonly IMemoryCache _cache;

        public NutritionController(INutritionService nutritionService, IFileUpload fileUpload, IRotaDayofWeekService MealDayOfWeekService,
            IClientService clientService, IMemoryCache cache, IStaffService staffService, IClientRotaTypeService clientRotaTypeService, IWebHostEnvironment env) : base(fileUpload)
        {
            _nutritionService = nutritionService;
            _MealDayOfWeekService = MealDayOfWeekService;
            _staffService = staffService;
            _cache = cache;
            _clientService = clientService;
            _clientRotaTypeService = clientRotaTypeService;
            _env = env;
        }
        public async Task<IActionResult> List()
        {
            var entities = await _nutritionService.Get();

            var client = await _clientService.GetClientDetail();
            List<NutritionViewModel> reports = new List<NutritionViewModel>();
            foreach (GetClientNutrition item in entities)
            {
                var report = new NutritionViewModel();
                report.DATETO = item.DATETO;
                report.DATEFROM = item.DATEFROM;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).FirstOrDefault().FullName;
                reports.Add(report);
            }
            return View(reports);
        }
        public async Task<IActionResult> Reports(int clientId)
        {
            NutritionViewModel model = new NutritionViewModel();

            var Client = await _clientService.GetClient(clientId);
            var nutrition = await _nutritionService.GetForEdit(clientId);
            List<GetStaffs> AllStaffs = await _staffService.GetStaffs();
            var Clients = await _clientService.GetClientDetail();
            var MealTypes = await _clientRotaTypeService.Get();
            var weekDays = await _MealDayOfWeekService.Get();
            model.ClientList = Clients.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
            model.MealTypes = MealTypes;
            model.WeekDays = weekDays;
            if (nutrition.Count > 0)
            {
                model.ClientMealDays = nutrition.FirstOrDefault().ClientMealDays;
                model.ClientId = clientId;
                model.ClientName = Clients.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
                model.NutritionId = nutrition.FirstOrDefault().NutritionId;
                model.ClientCleaning = nutrition.FirstOrDefault().ClientCleaning;
                model.ClientShopping = nutrition.FirstOrDefault().ClientShopping;
                model.StaffId = nutrition.FirstOrDefault().StaffId;
                model.STAFF = AllStaffs;
                model.PlannerName = model.STAFF.FirstOrDefault(s => s.StaffPersonalInfoId == model.StaffId).Fullname.ToString();
                model.PlannerContact = model.STAFF.FirstOrDefault(s => s.StaffPersonalInfoId == model.StaffId).Telephone.ToString();
                model.DATEFROM = nutrition.FirstOrDefault().DATEFROM;
                model.DATETO = nutrition.FirstOrDefault().DATETO;
                ViewBag.Staff = AllStaffs;

            }
            return View(model);
        }
        public async Task<IActionResult> Index(int clientId)
        {
            NutritionViewModel model = new NutritionViewModel();
            model.ClientId = clientId;
            var MealTypes = await _clientRotaTypeService.Get();
            var weekDays = await _MealDayOfWeekService.Get();
            var Client = await _clientService.GetClient(clientId);
            var nutrition = await _nutritionService.GetForEdit(clientId);
           
            model.MealTypes = MealTypes;
            model.WeekDays = weekDays;
            model.ClientImage = Client.PassportFilePath;
            
            var staffNames = await _staffService.GetStaffs();

            ViewBag.GetStaffs = staffNames;

            if (nutrition.Count > 0)
            {
                var Staff = await _staffService.GetStaff(nutrition.FirstOrDefault().StaffId);
                model.ActionName = "Update";
                model.NutritionId = nutrition.FirstOrDefault().NutritionId;
                model.ClientCleaning = nutrition.FirstOrDefault().ClientCleaning;
                model.ClientShopping = nutrition.FirstOrDefault().ClientShopping;
                model.ClientMealDays = nutrition.FirstOrDefault().ClientMealDays;
                model.PlannerImage = Staff.ProfilePix;
                model.PlannerContact = Staff.Telephone;
                model.ShoppingRowCount = nutrition.FirstOrDefault().ClientShopping.Count;
                model.CleaningRowCount = nutrition.FirstOrDefault().ClientCleaning.Count;
                model.DATEFROM = nutrition.FirstOrDefault().DATEFROM;
                model.DATETO = nutrition.FirstOrDefault().DATETO;
                model.StaffId = nutrition.FirstOrDefault().StaffId;
                model.MealSpecialNote = nutrition.FirstOrDefault().MealSpecialNote;
                model.ShoppingSpecialNote = nutrition.FirstOrDefault().ShoppingSpecialNote;
                model.CleaningSpecialNote = nutrition.FirstOrDefault().CleaningSpecialNote;
            }

            HttpContext.Session.Set<List<GetRotaDayofWeek>>("weekDays", model.WeekDays);
            HttpContext.Session.Set<List<GetClientRotaType>>("MealTypes", MealTypes);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(NutritionViewModel model, IFormCollection formsCollection)
        {
            List<GetClientRotaType> MealTypes = HttpContext.Session.Get<List<GetClientRotaType>>("MealTypes");
            List<GetRotaDayofWeek> weekDays = HttpContext.Session.Get<List<GetRotaDayofWeek>>("weekDays");
            if (model == null || !ModelState.IsValid)
            {
                var Client = await _clientService.GetClient(model.ClientId);
                var staffNames = await _staffService.GetStaffs();

                model.ClientImage = Client.PassportFilePath;
                model.MealTypes = MealTypes;
                model.WeekDays = weekDays;
                ViewBag.GetStaffs = staffNames;
                return View(model);
            }
            var Nutrition = new CreateNutrition();
            

            Nutrition.ClientId = model.ClientId;
            Nutrition.NutritionId = model.NutritionId;
            Nutrition.ShoppingSpecialNote = model.ShoppingSpecialNote;
            Nutrition.CleaningSpecialNote = model.CleaningSpecialNote;
            Nutrition.DATEFROM = model.DATEFROM;
            Nutrition.DATETO = model.DATETO;
            Nutrition.MealSpecialNote = model.MealSpecialNote;
            Nutrition.StaffId = model.StaffId;
            #region MealDays
            var MealDays = new List<CreateClientMealDays>();

            foreach (var MealType in MealTypes)
            {
                var Mealtype = formsCollection[MealType.RotaType];

                string NutritionId = $"{MealType.RotaType}-NutritionId";

                if (MealType != null)
                {
                    var clientmeal = formsCollection[NutritionId];

                    if (Mealtype.Count > 0 && Mealtype[0].ToString().Equals("on", StringComparison.InvariantCultureIgnoreCase))
                    {
                        int i = 0;
                        foreach (var weekDay in weekDays)
                        {
                            string weekdayid = $"{MealType.RotaType}-isChecked-{weekDay.DayofWeek}";
                            var WeekDay = formsCollection[weekdayid];
                            if (WeekDay.Count > 0 && WeekDay[0].ToString().Equals("on", StringComparison.InvariantCultureIgnoreCase))
                            {
                                var MealDay = new CreateClientMealDays();

                                string mealdetailId = $"{MealType.RotaType}-{weekDay.DayofWeek}-MealDetails";
                                string howtoprepareId = $"{MealType.RotaType}-{weekDay.DayofWeek}-HowtoPrepare";
                                string typeId = $"TypeId";
                                string seeVideoId = $"{MealType.RotaType}-{weekDay.DayofWeek}-SeeVideo";
                                var pictureId = $"{MealType.RotaType}-{weekDay.DayofWeek}-Picture";
                                string weekDayId = $"{MealType.RotaType}-{weekDay.DayofWeek}-Day";
                                string clientMealDay = $"{MealType.RotaType}-{weekDay.DayofWeek}-DayId";


                                var mealDetail = formsCollection[mealdetailId];
                                var howtoPrepare = formsCollection[howtoprepareId];
                                var weekday = formsCollection[weekDayId];
                                var clientMealDayId = formsCollection[clientMealDay];
                                var TypeId = formsCollection[typeId];
                                var seeVideo = formsCollection[seeVideoId];
                                var picture = formsCollection.Files.GetFile(pictureId);
                                string path = "";

                                if (picture != null)
                                {
                                    string folder = "clientcomplain";
                                    string filename = pictureId;
                                    path = await _fileUpload.UploadFile(folder, true, filename, picture.OpenReadStream());
                                }
                                MealDay.MEALDETAILS = mealDetail[0].ToString();
                                MealDay.HOWTOPREPARE = howtoPrepare[0].ToString();
                                MealDay.TypeId = int.Parse(TypeId[i].ToString());
                                MealDay.SEEVIDEO = seeVideo[0].ToString();
                                MealDay.PICTURE = path;
                                MealDay.MealDayofWeekId = int.Parse(weekday);
                                MealDay.ClientMealId = int.TryParse(clientMealDayId, out int dayId) ? dayId : 0;
                                MealDay.NutritionId = Nutrition.NutritionId;
                                MealDay.ClientMealTypeId = MealTypes.FirstOrDefault(r => r.RotaType.Equals(MealType.RotaType)).ClientRotaTypeId;

                                MealDays.Add(MealDay);
                                i++;
                            }

                        }
                    }

                }

            }
            Nutrition.ClientMealDays = MealDays;
            #endregion
            #region Shopping
            var Shoppings = new List<CreateClientShopping>();

            for (int i = 0; i < model.ShoppingRowCount; i++)
            {
                var Shopping = new CreateClientShopping();

                string StaffId = "STAFFId";
                string MeasureId = "MeansOfPurchase";
                string LocationId = "LocationOfPurchase";
                string ItemId = "Item";
                string DescriptionId = "Description";
                string QuantityId = "Quantity";
                string AmountId = "Amount";
                string DAYOFSHOPPINGId = "DAYOFSHOPPING";
                string DATEFROMId = "DATEFROM";
                string DATETOId = "DATETO";
                string ImageId = string.Concat("Image_",DateTime.Now.ToString());

                var Staff = int.Parse(formsCollection[StaffId][i].ToString());
                var Measure = formsCollection[MeasureId][i].ToString();
                var Location = formsCollection[LocationId][i].ToString();
                var Item = formsCollection[ItemId][i].ToString();
                var Description = formsCollection[DescriptionId][i].ToString();
                var Quantity = int.Parse(formsCollection[QuantityId][i].ToString());
                var Amount = decimal.Parse(formsCollection[AmountId][i].ToString());
                var DAYOFSHOPPING = formsCollection[DAYOFSHOPPINGId][i].ToString();
                var DATEFROM = DateTime.Parse(formsCollection[DATEFROMId][i].ToString());
                var DATETO = DateTime.Parse(formsCollection[DATETOId][i].ToString());
                var Image = formsCollection.Files.GetFile(ImageId);
                string path = "";
                if (Image != null)
                {
                    string folder = "clientcomplain";
                    string filename = ImageId;
                    path = await _fileUpload.UploadFile(folder, true, filename, Image.OpenReadStream());
                }

                Shopping.Amount = Amount;
                Shopping.DATEFROM = DATEFROM;
                Shopping.DATETO = DATETO;
                Shopping.DAYOFSHOPPING = DAYOFSHOPPING;
                Shopping.Description = Description;
                Shopping.Image = path;
                Shopping.Item = Item;
                Shopping.LocationOfPurchase = Location;
                Shopping.MeansOfPurchase = Measure;
                Shopping.Quantity = Quantity;
                Shopping.STAFFId = Staff;
                Shopping.NutritionId = Nutrition.NutritionId;

                Shoppings.Add(Shopping);
            }
            Nutrition.ClientShopping = Shoppings;
            #endregion
            #region Cleaning
            var Cleanings = new List<CreateClientCleaning>();

            for (int i = 0; i < model.CleaningRowCount; i++)
            {
                var Cleaning = new CreateClientCleaning();
                string StaffId = "STAFFId";
                string AreasAndItemsId = "AreasAndItems";
                string DetailsId = "Details";
                string SafetyHazardId = "SafetyHazard";
                string LocationOfItemId = "LocationOfItem";
                string DescOfItemId = "DescOfItem";
                string MinuteAllotedId = "MinuteAlloted";
                string DisposalId = "Disposal";
                string WhereToGetId = "WhereToGet";
                string SEEVIDEOId = "SEEVIDEO";
                string DAYOFCLEANINGId = "DAYOFCLEANING";
                string DATEFROMId = "DATEFROM";
                string DATETOId = "DATETO";
                string WhereToKeepId = "WhereToKeep";
                string ImageId = "Image";

                var Staff = int.Parse(formsCollection[StaffId][i].ToString());
                var AreasAndItems = int.Parse(formsCollection[AreasAndItemsId][i].ToString());
                var Details = formsCollection[DetailsId][i].ToString();
                var SafetyHazard = formsCollection[SafetyHazardId][i].ToString();
                var LocationOfItem = formsCollection[LocationOfItemId][i].ToString();
                var DescOfItem = formsCollection[DescOfItemId][i].ToString();
                var MinuteAlloted = DateTime.Parse(formsCollection[MinuteAllotedId][i].ToString());
                var Disposal = formsCollection[DisposalId][i].ToString();
                var WhereToGet = int.Parse(formsCollection[WhereToGetId][i].ToString());
                var SEEVIDEO = formsCollection[SEEVIDEOId][i].ToString();
                var DAYOFCLEANING = formsCollection[DAYOFCLEANINGId][i].ToString();
                var WhereToKeep = formsCollection.Files.GetFile(WhereToKeepId);
                var DATEFROM = DateTime.Parse(formsCollection[DATEFROMId][i].ToString());
                var DATETO = DateTime.Parse(formsCollection[DATETOId][i].ToString());
                var Image = formsCollection.Files.GetFile(ImageId);
                string pathkeep = "";
                string path = "";
                if (WhereToKeep != null)
                {
                    string folder = "clientcomplain";
                    string filename = WhereToKeepId;
                    pathkeep = await _fileUpload.UploadFile(folder, true, filename, WhereToKeep.OpenReadStream());
                }
                if (Image != null)
                {
                    string _folder = "clientcomplain";
                    string _filename = ImageId;
                    path = await _fileUpload.UploadFile(_folder, true, _filename, Image.OpenReadStream());
                }

                Cleaning.AreasAndItems = AreasAndItems;
                Cleaning.DATEFROM = DATEFROM;
                Cleaning.DATETO = DATETO;
                Cleaning.DAYOFCLEANING = DAYOFCLEANING;
                Cleaning.DescOfItem = DescOfItem;
                Cleaning.Image = path;
                Cleaning.Details = Details;
                Cleaning.Disposal = Disposal;
                Cleaning.LocationOfItem = LocationOfItem;
                Cleaning.MinuteAlloted = MinuteAlloted;
                Cleaning.SafetyHazard = SafetyHazard;
                Cleaning.SEEVIDEO = SEEVIDEO;
                Cleaning.WhereToGet = WhereToGet;
                Cleaning.WhereToKeep = pathkeep;
                Cleaning.STAFFId = Staff;
                Cleaning.NutritionId = Nutrition.NutritionId;

                Cleanings.Add(Cleaning);
            }
            Nutrition.ClientCleaning = Cleanings;
            var json = JsonConvert.SerializeObject(Nutrition);
            #endregion
            if (Nutrition != null)
            {
                if (model.ActionName == "Update")
                {
                    var result = await _nutritionService.Edit(Nutrition, model.ClientId);
                    SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Nutrition successfully Updated" : "An Error Occurred" });
                    var content = await result.Content.ReadAsStringAsync();
                }
                else
                {
                    var result = await _nutritionService.CreateNutrition(Nutrition);
                    SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "New Nutrition successfully registered" : "An Error Occurred" });
                    var content = await result.Content.ReadAsStringAsync();
                }
            }
            
            return RedirectToAction("Index", new { clientId = Nutrition.ClientId });
        }
    }
}
