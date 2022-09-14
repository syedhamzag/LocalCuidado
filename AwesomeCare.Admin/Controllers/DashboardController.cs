using AwesomeCare.Admin.Services.Dashboard;
using AwesomeCare.Admin.Services.RotaTask;
using AwesomeCare.Admin.ViewModels.Rotering;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using AwesomeCare.DataTransferObject.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.ClientLogAudit;
using AwesomeCare.Admin.Services.ClientMedAudit;
using AwesomeCare.Admin.Services.ComplainRegister;
using AwesomeCare.Admin.Services.ClientVoice;
using AwesomeCare.Admin.Services.ClientServiceWatch;
using AwesomeCare.Admin.Services.ClientMgtVisit;
using AwesomeCare.Admin.Services.ClientProgram;
using AwesomeCare.Admin.Services.ClientBloodCoagulationRecord;
using AwesomeCare.Admin.Services.ClientBMIChart;
using AwesomeCare.Admin.Services.ClientBodyTemp;
using AwesomeCare.Admin.Services.ClientBowelMovement;
using AwesomeCare.Admin.Services.ClientEyeHealthMonitoring;
using AwesomeCare.Admin.Services.ClientFoodIntake;
using AwesomeCare.Admin.Services.ClientHeartRate;
using AwesomeCare.Admin.Services.ClientOxygenLvl;
using AwesomeCare.Admin.Services.ClientPainChart;
using AwesomeCare.Admin.Services.ClientPulseRate;
using AwesomeCare.Admin.Services.ClientBloodPressure;
using AwesomeCare.Admin.Services.ClientSeizure;
using AwesomeCare.Admin.Services.ClientWoundCare;
using AwesomeCare.Admin.Services.StaffAdlObs;
using AwesomeCare.Admin.Services.StaffKeyWorkerVoice;
using AwesomeCare.Admin.Services.StaffMedComp;
using AwesomeCare.Admin.Services.StaffOneToOne;
using AwesomeCare.Admin.Services.StaffReference;
using AwesomeCare.Admin.Services.StaffSpotCheck;
using AwesomeCare.Admin.Services.StaffSupervisionAppraisal;
using AwesomeCare.Admin.Services.StaffSurvey;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.Enums;
using AwesomeCare.DataTransferObject.DTOs.Rotering;
using AwesomeCare.DataTransferObject.DTOs.StaffRating;
using AwesomeCare.Admin.Services.DutyOnCall;
using AwesomeCare.DataTransferObject.DTOs.DutyOnCall;
using AwesomeCare.Admin.Services.TaskBoard;
using AwesomeCare.DataTransferObject.DTOs.TaskBoard;
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.Admin.Services.TrackingConcernNote;
using AwesomeCare.DataTransferObject.DTOs.TrackingConcernNote;
using AwesomeCare.Admin.Services.ClientCareObj;
using AwesomeCare.Admin.Services.PerformanceIndicator;

namespace AwesomeCare.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        #region declare Service
        private IPerformanceIndicatorService _performanceIndicator;
        private IDashboardService _dashboardService;
        private IRotaTaskService _rotaTaskService;
        private IClientLogAuditService _clientLogAuditService;
        private IClientMedAuditService _clientMedAuditService;
        private IComplainService _clientComplainService;
        private IClientVoiceService _clientVoiceService;
        private IClientServiceWatchService _clientServiceWatchService;
        private IClientMgtVisitService _clientMgtVisitService;
        private IClientProgramService _clientProgramService;
        private IClientBloodCoagulationRecordService _clientBloodCoagService;
        private IClientBMIChartService _clientBMIService;
        private IClientBodyTempService _clientBodyService;
        private IClientBowelMovementService _clientBowelService;
        private IClientEyeHealthMonitoringService _clientEyeService;
        private IClientFoodIntakeService _clientFoodService;
        private IClientHeartRateService _clientHeartService;
        private IClientOxygenLvlService _clientOxygenService;
        private IClientPainChartService _clientPainService;
        private IClientPulseRateService _clientPulseService;
        private IClientBloodPressureService _clientPressureService;
        private IClientSeizureService _clientSeizureService;
        private IClientWoundCareService _clientWoundService;
        private IStaffAdlObsService _staffAdlObsService;
        private IStaffKeyWorkerVoiceService _staffKeyWorkerService;
        private IStaffMedCompService _staffMedCompService;
        private IStaffOneToOneService _staffOneToOneService;
        private IStaffReferenceService _staffReferenceService;
        private IStaffSpotCheckService _staffSpotCheckService;
        private IStaffSupervisionAppraisalService _staffSupervisionService;
        private IStaffSurveyService _staffSurveyService;
        private IStaffService _staffService;
        private IClientService _clientService;
        private IDutyOnCallService _oncallService;
        private IBaseRecordService _baseService;
        private ITaskBoardService _taskService;
        private ITrackingConcernNote _concernNote;
        private IClientCareObjService _clientCareObj;
        #endregion

        public DashboardController(IDashboardService dashboardService, IFileUpload fileUpload, IRotaTaskService rotaTaskService,
            IClientLogAuditService clientLogAuditService, IClientMedAuditService clientMedAuditService, IComplainService clientComplainService,
        IClientVoiceService clientVoiceService, IClientServiceWatchService clientServiceWatchService, IClientMgtVisitService clientMgtVisitService,
        IClientProgramService clientProgramService, IClientBloodCoagulationRecordService clientBloodCoagService, IClientBMIChartService clientBMIService,
        IClientBodyTempService clientBodyService, IClientBowelMovementService clientBowelService, IClientEyeHealthMonitoringService clientEyeService,
        IClientFoodIntakeService clientFoodService, IClientHeartRateService clientHeartService, IClientOxygenLvlService clientOxygenService,
        IClientPainChartService clientPainService, IClientPulseRateService clientPulseService, IClientBloodPressureService clientPressureService,
        IClientSeizureService clientSeizureService, IClientWoundCareService clientWoundService, IStaffAdlObsService staffAdlObsService,
        IStaffKeyWorkerVoiceService staffKeyWorkerService, IStaffMedCompService staffMedCompService, IStaffOneToOneService staffOneToOneService,
        IStaffReferenceService staffReferenceService, IStaffSpotCheckService staffSpotCheckService, IStaffSupervisionAppraisalService staffSupervisionService,
        IStaffSurveyService staffSurveyService, IStaffService staffService, IClientService clientService, IDutyOnCallService oncallService, IBaseRecordService baseService, ITaskBoardService taskService,
        ITrackingConcernNote concernNote, IClientCareObjService clientCareObj, IPerformanceIndicatorService performanceIndicator) : base(fileUpload)
        {
            #region Services
            _performanceIndicator = performanceIndicator;
            _dashboardService = dashboardService;
            _rotaTaskService = rotaTaskService;
            _clientLogAuditService = clientLogAuditService;
            _clientMedAuditService = clientMedAuditService;
            _clientComplainService = clientComplainService;
            _clientVoiceService = clientVoiceService;
            _clientServiceWatchService = clientServiceWatchService;
            _clientMgtVisitService = clientMgtVisitService;
            _clientProgramService = clientProgramService;
            _clientBloodCoagService = clientBloodCoagService;
            _clientBMIService = clientBMIService;
            _clientBodyService = clientBodyService;
            _clientBowelService = clientBowelService;
            _clientEyeService = clientEyeService;
            _clientFoodService = clientFoodService;
            _clientHeartService = clientHeartService;
            _clientOxygenService = clientOxygenService;
            _clientPainService = clientPainService;
            _clientPulseService = clientPulseService;
            _clientPressureService = clientPressureService;
            _clientSeizureService = clientSeizureService;
            _clientWoundService = clientWoundService;
            _staffAdlObsService = staffAdlObsService;
            _staffKeyWorkerService = staffKeyWorkerService;
            _staffMedCompService = staffMedCompService;
            _staffOneToOneService = staffOneToOneService;
            _staffReferenceService = staffReferenceService;
            _staffSpotCheckService = staffSpotCheckService;
            _staffSupervisionService = staffSupervisionService;
            _staffSurveyService = staffSurveyService;
            _staffService = staffService;
            _clientService = clientService;
            _oncallService = oncallService;
            _baseService = baseService;
            _taskService = taskService;
            _concernNote = concernNote;
            _clientCareObj = clientCareObj;
            #endregion
        }
        public async Task<IActionResult> Dashboard()
        {
            var dashboard = await _dashboardService.Get();
            return View(dashboard);
        }
        [HttpGet]
        public JsonResult GetDashboard()
        {
            var dashboard = _dashboardService.Get();
            #region variables
            var getStaff =  _staffService.GetAsync();
            var getClient =  _clientService.GetClients();
            #endregion
            dashboard.Result.GetAllStaff = getStaff.Result;
            dashboard.Result.ActiveUser = getClient.Result.Where(s => s.Status == "Active").Count();
            dashboard.Result.ApprovedStaff = getStaff.Result.Where(s => s.Status == StaffRegistrationEnum.Approved).Count();
            dashboard.Result.GetClients = GetClients(getClient.Result);
            dashboard.Result.GetStaffPersonalInfos = GetStaffs(getStaff.Result);           

            return Json(dashboard.Result);
        }
        [HttpGet]
        public JsonResult GetTask()
        {
            var dashboard = _dashboardService.Get();
            #region variables
            var getStaff = _staffService.GetAsync();
            var task = _taskService.GetWithStaff();
            var baserecordItem = _baseService.GetBaseRecordsWithItems();
            #endregion
            var taskitem = baserecordItem.Result.Where(s => s.KeyName == "Task_Board_Status").FirstOrDefault();
            dashboard.Result.GetTaskBoard = GetTaskBoard(task.Result, taskitem);
            dashboard.Result.GetAllStaff = getStaff.Result;
            return Json(dashboard.Result);
        }
        [HttpGet]
        public JsonResult GetRatting()
        {
            var dashboard = _dashboardService.Get();
            #region variables
            var rating = _staffService.GetClientFeedback();
            #endregion
            dashboard.Result.StaffRating = GetStaffRating(rating.Result);
            return Json(dashboard.Result);
        }
        [HttpGet]
        public JsonResult GetConcern()
        {
            var dashboard = _dashboardService.Get();
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");
            bool isStopDateValid = DateTime.TryParseExact(endDate, "yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime stopDate);
            #region variables
            var ConcernIdP = dashboard.Result.ConcernIdP;
            var ConcernIdC = dashboard.Result.ConcernIdC;
            var ConcernIdO = dashboard.Result.ConcernIdO;
            var concern = _concernNote.GetWithChild();
            var baserecordItem = _baseService.GetBaseRecordsWithItems();
            #endregion
            #region Concern
            var Concern = new List<Status>();
            var concernTotal = 0;
            var Concernpending = concern.Result.Where(s => s.Status == ConcernIdP).Count();
            concernTotal += Concernpending;
            var Concernclosed = concern.Result.Where(s => s.Status == ConcernIdC).Count();
            concernTotal += Concernclosed;
            var Concernopen = concern.Result.Where(s => s.Status == ConcernIdO).Count();
            concernTotal += Concernopen;
            Concern.Add(new Status
            {
                Key = "Pending",
                Value = Concernpending
            });
            Concern.Add(new Status
            {
                Key = "Closed",
                Value = Concernclosed
            });
            Concern.Add(new Status
            {
                Key = "Open",
                Value = Concernopen
            });
            Concern.Add(new Status
            {
                Key = "Total",
                Value = concernTotal
            });
            dashboard.Result.concernNoteGraph = Concern;
            #endregion
            dashboard.Result.concernNotes = GetConcernNotes(concern.Result);

            return Json(dashboard.Result);
        }
        [HttpGet]
        public JsonResult GetOncall()
        {
            var dashboard = _dashboardService.Get();
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");
            bool isStopDateValid = DateTime.TryParseExact(endDate, "yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime stopDate);
            #region variables
            var oncallIdP = dashboard.Result.oncallP;
            var oncallIdC = dashboard.Result.oncallC;
            var oncallIdO = dashboard.Result.oncallO;
            var getClient = _clientService.GetClients();
            var oncall = _oncallService.GetWithPersonToAct();
            #endregion
            #region Oncall
            var Oncall = new List<Status>();
            var oncallTotal = 0;
            var oncallpending = oncall.Result.Where(s => s.Status == oncallIdP).Count();
            oncallTotal += oncallpending;
            var oncallclosed = oncall.Result.Where(s => s.Status == oncallIdC).Count();
            oncallTotal += oncallclosed;
            var oncallopen = oncall.Result.Where(s => s.Status == oncallIdO).Count();
            oncallTotal += oncallopen;
            Oncall.Add(new Status
            {
                Key = "Pending",
                Value = oncallpending
            });
            Oncall.Add(new Status
            {
                Key = "Closed",
                Value = oncallclosed
            });
            Oncall.Add(new Status
            {
                Key = "Open",
                Value = oncallopen
            });
            Oncall.Add(new Status
            {
                Key = "Total",
                Value = oncallTotal
            });
            dashboard.Result.OnCallGraph = Oncall;
            #endregion
            var onCallResult = oncall.Result.TakeLast(5).ToList();
            dashboard.Result.OnCall = GetOnCall(onCallResult, getClient.Result);

            return Json(dashboard.Result);
        }
        [HttpGet]
        public JsonResult GetClientMatrix()
        {
            var dashboard = _dashboardService.Get();
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");
            bool isStopDateValid = DateTime.TryParseExact(endDate, "yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime stopDate);
            #region variables

            int clientPending = 0;
            int clientClosed = 0;
            int clientLate = 0;

            var pId = dashboard.Result.pId;
            var cId = dashboard.Result.cId;
            var lId = dashboard.Result.lId;

            var getClient = _clientService.GetClients();
            var log = _clientLogAuditService.Get();
            var med = _clientMedAuditService.Get();
            var complain = _clientComplainService.Get();
            var voice = _clientVoiceService.Get();
            var watch = _clientServiceWatchService.Get();
            var mgt = _clientMgtVisitService.Get();
            var prog = _clientProgramService.Get();

            var baserecordItem = _baseService.GetBaseRecordsWithItems();
            #endregion
            var Client = new List<Status>();

            #region LogAudit
            var log_pending = log.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var log_closed = log.Result.Where(j => j.Status == cId).Count();
            var log_late = log.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
            clientPending += log_pending;
            clientClosed += log_closed;
            clientLate += log_late;
            var LogAudit = new List<Status>();
            LogAudit.Add(new Status
            {
                Key = "Pending",
                Value = log_pending
            });
            LogAudit.Add(new Status
            {
                Key = "Closed",
                Value = log_closed
            });
            LogAudit.Add(new Status
            {
                Key = "Late",
                Value = log_late
            });
            dashboard.Result.LogAudit = LogAudit;
            #endregion
            #region MedAudit
            var med_pending = med.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var med_closed = med.Result.Where(j => j.Status == cId).Count();
            var med_late = med.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
            clientPending += med_pending;
            clientClosed += med_closed;
            clientLate += med_late;
            var MedAudit = new List<Status>();
            MedAudit.Add(new Status
            {
                Key = "Pending",
                Value = med_pending
            });
            MedAudit.Add(new Status
            {
                Key = "Closed",
                Value = med_closed
            });
            MedAudit.Add(new Status
            {
                Key = "Late",
                Value = med_late
            });
            dashboard.Result.MedAudit = MedAudit;
            #endregion
            #region CompAudit
            var Comp_pending = complain.Result.Where(j => j.StatusId == pId && (j.DUEDATE.Year >= stopDate.Year && j.DUEDATE.Month >= stopDate.Month && j.DUEDATE.Day > stopDate.Day)).Count();
            var Comp_closed = complain.Result.Where(j => j.StatusId == cId).Count();
            var Comp_late = complain.Result.Where(j => j.StatusId == pId && (j.DUEDATE.Year <= stopDate.Year && j.DUEDATE.Month <= stopDate.Month && j.DUEDATE.Day < stopDate.Day)).Count();
            clientPending += Comp_pending;
            clientClosed += Comp_closed;
            clientLate += Comp_late;
            var Comp = new List<Status>();
            Comp.Add(new Status
            {
                Key = "Pending",
                Value = Comp_pending
            });
            Comp.Add(new Status
            {
                Key = "Closed",
                Value = Comp_closed
            });
            Comp.Add(new Status
            {
                Key = "Late",
                Value = Comp_late
            });
            dashboard.Result.Complain = Comp;
            #endregion
            #region Voice
            var voice_pending = voice.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var voice_closed = voice.Result.Where(j => j.Status == cId).Count();
            var voice_late = voice.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
            clientPending += voice_pending;
            clientClosed += voice_closed;
            clientLate += voice_late;
            var Voice = new List<Status>();
            Voice.Add(new Status
            {
                Key = "Pending",
                Value = voice_pending
            });
            Voice.Add(new Status
            {
                Key = "Closed",
                Value = voice_closed
            });
            Voice.Add(new Status
            {
                Key = "Late",
                Value = voice_late
            });
            dashboard.Result.Voice = Voice;
            #endregion
            #region Program
            var prog_pending = prog.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var prog_closed = prog.Result.Where(j => j.Status == cId).Count();
            var prog_late = prog.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
            clientPending += prog_pending;
            clientClosed += prog_closed;
            clientLate += prog_late;
            var Program = new List<Status>();
            Program.Add(new Status
            {
                Key = "Pending",
                Value = prog_pending
            });
            Program.Add(new Status
            {
                Key = "Closed",
                Value = prog_closed
            });
            Program.Add(new Status
            {
                Key = "Late",
                Value = prog_late
            });
            dashboard.Result.Program = Program;
            #endregion
            #region Watch
            var watch_pending = watch.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var watch_closed = watch.Result.Where(j => j.Status == cId).Count();
            var watch_late = watch.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
            clientPending += watch_pending;
            clientClosed += watch_closed;
            clientLate += watch_late;
            var Watch = new List<Status>();
            Watch.Add(new Status
            {
                Key = "Pending",
                Value = watch_pending
            });
            Watch.Add(new Status
            {
                Key = "Closed",
                Value = watch_closed
            });
            Watch.Add(new Status
            {
                Key = "Late",
                Value = watch_late
            });
            dashboard.Result.ServiceWatch = Watch;
            #endregion
            #region MgtVisit
            var mgt_pending = mgt.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var mgt_closed = mgt.Result.Where(j => j.Status == cId).Count();
            var mgt_late = mgt.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
            clientPending += mgt_pending;
            clientClosed += mgt_closed;
            clientLate += mgt_late;
            var MgtVisit = new List<Status>();
            MgtVisit.Add(new Status
            {
                Key = "Pending",
                Value = mgt_pending
            });
            MgtVisit.Add(new Status
            {
                Key = "Closed",
                Value = mgt_closed
            });
            MgtVisit.Add(new Status
            {
                Key = "Late",
                Value = mgt_late
            });
            dashboard.Result.MgtVisit = MgtVisit;
            #endregion
            #region ClientMatrix
            Client.Add(new Status
            {
                Key = "Pending",
                Value = clientPending
            });
            Client.Add(new Status
            {
                Key = "Closed",
                Value = clientClosed
            });
            Client.Add(new Status
            {
                Key = "Late",
                Value = clientLate
            });
            dashboard.Result.ClientMatrix = Client;
            #endregion
            return Json(dashboard.Result);
        }
        [HttpGet]
        public JsonResult GetStaffMatrix()
        {
            var dashboard = _dashboardService.Get();
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");
            bool isStopDateValid = DateTime.TryParseExact(endDate, "yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime stopDate);
            #region variables

            int staffPending = 0;
            int staffClosed = 0;
            int staffLate = 0;

            var pId = dashboard.Result.pId;
            var cId = dashboard.Result.cId;
            var lId = dashboard.Result.lId;


            var adl = _staffAdlObsService.Get();
            var key = _staffKeyWorkerService.Get();
            var comp = _staffMedCompService.Get();
            var one = _staffOneToOneService.Get();
            var refs = _staffReferenceService.Get();
            var spot = _staffSpotCheckService.Get();
            var super = _staffSupervisionService.Get();
            var survey = _staffSurveyService.Get();
            #endregion

            var Staff = new List<Status>();
            #region AdlObs
            var obs_pending = adl.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var obs_closed = adl.Result.Where(j => j.Status == cId).Count();
            var obs_late = adl.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
            staffPending += obs_pending;
            staffClosed += obs_closed;
            staffLate += obs_late;
            var AdlObs = new List<Status>();
            AdlObs.Add(new Status
            {
                Key = "Pending",
                Value = obs_pending
            });
            AdlObs.Add(new Status
            {
                Key = "Closed",
                Value = obs_closed
            });
            AdlObs.Add(new Status
            {
                Key = "Late",
                Value = obs_late
            });
            dashboard.Result.AdlObs = AdlObs;
            #endregion
            #region KeyWorker
            var key_pending = key.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var key_closed = key.Result.Where(j => j.Status == cId).Count();
            var key_late = key.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
            staffPending += key_pending;
            staffClosed += key_closed;
            staffLate += key_late;
            var KeyWorker = new List<Status>();
            KeyWorker.Add(new Status
            {
                Key = "Pending",
                Value = key_pending
            });
            KeyWorker.Add(new Status
            {
                Key = "Closed",
                Value = key_closed
            });
            KeyWorker.Add(new Status
            {
                Key = "Late",
                Value = key_late
            });
            dashboard.Result.KeyWorker = KeyWorker;
            #endregion
            #region MedComp
            var md_pending = comp.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var md_closed = comp.Result.Where(j => j.Status == cId).Count();
            var md_late = comp.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
            staffPending += md_pending;
            staffClosed += md_closed;
            staffLate += md_late;
            var MedComp = new List<Status>();
            MedComp.Add(new Status
            {
                Key = "Pending",
                Value = md_pending
            });
            MedComp.Add(new Status
            {
                Key = "Closed",
                Value = md_closed
            });
            MedComp.Add(new Status
            {
                Key = "Late",
                Value = md_late
            });
            dashboard.Result.MedComp = MedComp;
            #endregion
            #region One
            var one_pending = one.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var one_closed = one.Result.Where(j => j.Status == cId).Count();
            var one_late = one.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
            staffPending += one_pending;
            staffClosed += one_closed;
            staffLate += one_late;
            var One = new List<Status>();
            One.Add(new Status
            {
                Key = "Pending",
                Value = one_pending
            });
            One.Add(new Status
            {
                Key = "Closed",
                Value = one_closed
            });
            One.Add(new Status
            {
                Key = "Late",
                Value = one_late
            });
            dashboard.Result.OneToOne = One;
            #endregion
            #region Ref
            var ref_pending = refs.Result.Where(j => j.Status == pId).Count();
            var ref_closed = refs.Result.Where(j => j.Status == cId).Count();
            var ref_late = refs.Result.Where(j => j.Status == lId).Count();
            staffPending += ref_pending;
            staffClosed += ref_closed;
            staffLate += ref_late;
            var Ref = new List<Status>();
            Ref.Add(new Status
            {
                Key = "Pending",
                Value = ref_pending
            });
            Ref.Add(new Status
            {
                Key = "Closed",
                Value = ref_closed
            });
            Ref.Add(new Status
            {
                Key = "Late",
                Value = ref_late
            });
            dashboard.Result.Reference = Ref;
            #endregion
            #region Spot
            var spot_pending = spot.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var spot_closed = spot.Result.Where(j => j.Status == cId).Count();
            var spot_late = spot.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
            staffPending += spot_pending;
            staffClosed += spot_closed;
            staffLate += spot_late;
            var Spot = new List<Status>();
            Spot.Add(new Status
            {
                Key = "Pending",
                Value = spot_pending
            });
            Spot.Add(new Status
            {
                Key = "Closed",
                Value = spot_closed
            });
            Spot.Add(new Status
            {
                Key = "Late",
                Value = spot_late
            });
            dashboard.Result.SpotCheck = Spot;
            #endregion
            #region Super
            var sup_pending = super.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var sup_closed = super.Result.Where(j => j.Status == cId).Count();
            var sup_late = super.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
            staffPending += sup_pending;
            staffClosed += sup_closed;
            staffLate += sup_late;
            var Super = new List<Status>();
            Super.Add(new Status
            {
                Key = "Pending",
                Value = sup_pending
            });
            Super.Add(new Status
            {
                Key = "Closed",
                Value = sup_closed
            });
            Super.Add(new Status
            {
                Key = "Late",
                Value = sup_late
            });
            dashboard.Result.Supervision = Super;
            #endregion
            #region Survey
            var sur_pending = survey.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var sur_closed = survey.Result.Where(j => j.Status == cId).Count();
            var sur_late = survey.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
            staffPending += sur_pending;
            staffClosed += sur_closed;
            staffLate += sur_late;
            var Survey = new List<Status>();
            Survey.Add(new Status
            {
                Key = "Pending",
                Value = sur_pending
            });
            Survey.Add(new Status
            {
                Key = "Closed",
                Value = sur_closed
            });
            Survey.Add(new Status
            {
                Key = "Late",
                Value = sur_late
            });
            dashboard.Result.Survey = Survey;
            #endregion
            #region StaffMatrix
            Staff.Add(new Status
            {
                Key = "Pending",
                Value = staffPending
            });
            Staff.Add(new Status
            {
                Key = "Closed",
                Value = staffClosed
            });
            Staff.Add(new Status
            {
                Key = "Late",
                Value = staffLate
            });
            dashboard.Result.StaffMatrix = Staff;
            #endregion
            return Json(dashboard.Result);
        }
        [HttpGet]
        public JsonResult GetTeleHealth()
        {
            var dashboard = _dashboardService.Get();
            #region variables
            int TeleNormal = 0;
            int TeleObservation = 0;
            int TeleAttention = 0;
            int TeleReceived = 0;

            var nId = dashboard.Result.nId;
            var oId = dashboard.Result.oId;
            var aId = dashboard.Result.aId;
            var rId = dashboard.Result.rId;

            var pId = dashboard.Result.pId;
            var cId = dashboard.Result.cId;
            var lId = dashboard.Result.lId;



            var coag = _clientBloodCoagService.Get();
            var bmi = _clientBMIService.Get();
            var body = _clientBodyService.Get();
            var bowel = _clientBowelService.Get();
            var eye = _clientEyeService.Get();
            var food = _clientFoodService.Get();
            var heart = _clientHeartService.Get();
            var oxygen = _clientOxygenService.Get();
            var pain = _clientPainService.Get();
            var pulse = _clientPulseService.Get();
            var pressure = _clientPressureService.Get();
            var seizure = _clientSeizureService.Get();
            var wound = _clientWoundService.Get();
            #endregion
            var TeleHeath = new List<Status>();

            #region BloodCoag
            var normal = coag.Result.Where(j => j.Status == nId).Count();
            var observation = coag.Result.Where(j => j.Status == oId).Count();
            var attention = coag.Result.Where(j => j.Status == aId).Count();
            var received = coag.Result.Where(j => j.Status == rId).Count();
            TeleNormal += normal;
            TeleObservation += observation;
            TeleAttention += attention;
            TeleReceived += received;
            var BloodCoag = new List<Status>();
            BloodCoag.Add(new Status
            {
                Key = "N",
                Value = normal
            });
            BloodCoag.Add(new Status
            {
                Key = "O",
                Value = observation
            });
            BloodCoag.Add(new Status
            {
                Key = "A",
                Value = attention
            });
            BloodCoag.Add(new Status
            {
                Key = "R",
                Value = received
            });
            dashboard.Result.BloodCoag = BloodCoag;
            #endregion
            #region BloodPressure
            var pnormal = pressure.Result.Where(j => j.Status == nId).Count();
            var pobservation = pressure.Result.Where(j => j.Status == oId).Count();
            var pattention = pressure.Result.Where(j => j.Status == aId).Count();
            var preceived = pressure.Result.Where(j => j.Status == rId).Count();
            TeleNormal += pnormal;
            TeleObservation += pobservation;
            TeleAttention += pattention;
            TeleReceived += preceived;
            var Pressure = new List<Status>();
            Pressure.Add(new Status
            {
                Key = "N",
                Value = pnormal
            });
            Pressure.Add(new Status
            {
                Key = "O",
                Value = pobservation
            });
            Pressure.Add(new Status
            {
                Key = "A",
                Value = pattention
            });
            Pressure.Add(new Status
            {
                Key = "R",
                Value = preceived
            });
            dashboard.Result.Pressure = Pressure;
            #endregion
            #region BMI
            var bmi_normal = bmi.Result.Where(j => j.Status == nId).Count();
            var bmi_observation = bmi.Result.Where(j => j.Status == oId).Count();
            var bmi_attention = bmi.Result.Where(j => j.Status == aId).Count();
            var bmi_received = bmi.Result.Where(j => j.Status == rId).Count();
            TeleNormal += bmi_normal;
            TeleObservation += bmi_observation;
            TeleAttention += bmi_attention;
            TeleReceived += bmi_received;
            var BMI = new List<Status>();
            BMI.Add(new Status
            {
                Key = "N",
                Value = bmi_normal
            });
            BMI.Add(new Status
            {
                Key = "O",
                Value = bmi_observation
            });
            BMI.Add(new Status
            {
                Key = "A",
                Value = bmi_attention
            });
            BMI.Add(new Status
            {
                Key = "R",
                Value = bmi_received
            });
            dashboard.Result.BMI = BMI;
            #endregion
            #region Body
            var Body_normal = body.Result.Where(j => j.Status == nId).Count();
            var Body_observation = body.Result.Where(j => j.Status == oId).Count();
            var Body_attention = body.Result.Where(j => j.Status == aId).Count();
            var Body_received = body.Result.Where(j => j.Status == rId).Count();
            TeleNormal += Body_normal;
            TeleObservation += Body_observation;
            TeleAttention += Body_attention;
            TeleReceived += Body_received;
            var Body = new List<Status>();
            Body.Add(new Status
            {
                Key = "N",
                Value = Body_normal
            });
            Body.Add(new Status
            {
                Key = "O",
                Value = Body_observation
            });
            Body.Add(new Status
            {
                Key = "A",
                Value = Body_attention
            });
            Body.Add(new Status
            {
                Key = "R",
                Value = Body_received
            });
            dashboard.Result.Body = Body;
            #endregion
            #region Bowel
            var Bowel_normal = bowel.Result.Where(j => j.Status == nId).Count();
            var Bowel_observation = bowel.Result.Where(j => j.Status == oId).Count();
            var Bowel_attention = bowel.Result.Where(j => j.Status == aId).Count();
            var Bowel_received = bowel.Result.Where(j => j.Status == rId).Count();
            TeleNormal += Bowel_normal;
            TeleObservation += Bowel_observation;
            TeleAttention += Bowel_attention;
            TeleReceived += Bowel_received;
            var Bowel = new List<Status>();
            Bowel.Add(new Status
            {
                Key = "N",
                Value = Bowel_normal
            });
            Bowel.Add(new Status
            {
                Key = "O",
                Value = Bowel_observation
            });
            Bowel.Add(new Status
            {
                Key = "A",
                Value = Bowel_attention
            });
            Bowel.Add(new Status
            {
                Key = "R",
                Value = Bowel_received
            });
            dashboard.Result.Bowel = Bowel;
            #endregion
            #region Eye
            var Eye_normal = eye.Result.Where(j => j.Status == nId).Count();
            var Eye_observation = eye.Result.Where(j => j.Status == oId).Count();
            var Eye_attention = eye.Result.Where(j => j.Status == aId).Count();
            var Eye_received = eye.Result.Where(j => j.Status == rId).Count();
            TeleNormal += Eye_normal;
            TeleObservation += Eye_observation;
            TeleAttention += Eye_attention;
            TeleReceived += Eye_received;
            var Eye = new List<Status>();
            Eye.Add(new Status
            {
                Key = "N",
                Value = Eye_normal
            });
            Eye.Add(new Status
            {
                Key = "O",
                Value = Eye_observation
            });
            Eye.Add(new Status
            {
                Key = "A",
                Value = Eye_attention
            });
            Eye.Add(new Status
            {
                Key = "R",
                Value = Eye_received
            });
            dashboard.Result.EyeHealth = Eye;
            #endregion
            #region Food
            var Food_normal = food.Result.Where(j => j.Status == nId).Count();
            var Food_observation = food.Result.Where(j => j.Status == oId).Count();
            var Food_attention = food.Result.Where(j => j.Status == aId).Count();
            var Food_received = food.Result.Where(j => j.Status == rId).Count();
            TeleNormal += Food_normal;
            TeleObservation += Food_observation;
            TeleAttention += Food_attention;
            TeleReceived += Food_received;
            var Food = new List<Status>();
            Food.Add(new Status
            {
                Key = "N",
                Value = Food_normal
            });
            Food.Add(new Status
            {
                Key = "O",
                Value = Food_observation
            });
            Food.Add(new Status
            {
                Key = "A",
                Value = Food_attention
            });
            Food.Add(new Status
            {
                Key = "R",
                Value = Food_received
            });
            dashboard.Result.Food = Food;
            #endregion
            #region Heart
            var Heart_normal = heart.Result.Where(j => j.Status == nId).Count();
            var Heart_observation = heart.Result.Where(j => j.Status == oId).Count();
            var Heart_attention = heart.Result.Where(j => j.Status == aId).Count();
            var Heart_received = heart.Result.Where(j => j.Status == rId).Count();
            TeleNormal += Heart_normal;
            TeleObservation += Heart_observation;
            TeleAttention += Heart_attention;
            TeleReceived += Heart_received;
            var Heart = new List<Status>();
            Heart.Add(new Status
            {
                Key = "N",
                Value = Heart_normal
            });
            Heart.Add(new Status
            {
                Key = "O",
                Value = Heart_observation
            });
            Heart.Add(new Status
            {
                Key = "A",
                Value = Heart_attention
            });
            Heart.Add(new Status
            {
                Key = "R",
                Value = Heart_received
            });
            dashboard.Result.Heart = Heart;
            #endregion
            #region Oxygen
            var Oxygen_normal = oxygen.Result.Where(j => j.Status == nId).Count();
            var Oxygen_observation = oxygen.Result.Where(j => j.Status == oId).Count();
            var Oxygen_attention = oxygen.Result.Where(j => j.Status == aId).Count();
            var Oxygen_received = oxygen.Result.Where(j => j.Status == rId).Count();
            TeleNormal += Oxygen_normal;
            TeleObservation += Oxygen_observation;
            TeleAttention += Oxygen_attention;
            TeleReceived += Oxygen_received;
            var Oxygen = new List<Status>();
            Oxygen.Add(new Status
            {
                Key = "N",
                Value = Oxygen_normal
            });
            Oxygen.Add(new Status
            {
                Key = "O",
                Value = Oxygen_observation
            });
            Oxygen.Add(new Status
            {
                Key = "A",
                Value = Oxygen_attention
            });
            Oxygen.Add(new Status
            {
                Key = "R",
                Value = Oxygen_received
            });
            dashboard.Result.Oxygen = Oxygen;
            #endregion
            #region Pain
            var Pain_normal = pain.Result.Where(j => j.Status == nId).Count();
            var Pain_observation = pain.Result.Where(j => j.Status == oId).Count();
            var Pain_attention = pain.Result.Where(j => j.Status == aId).Count();
            var Pain_received = pain.Result.Where(j => j.Status == rId).Count();
            TeleNormal += Pain_normal;
            TeleObservation += Pain_observation;
            TeleAttention += Pain_attention;
            TeleReceived += Pain_received;
            var Pain = new List<Status>();
            Pain.Add(new Status
            {
                Key = "N",
                Value = Pain_normal
            });
            Pain.Add(new Status
            {
                Key = "O",
                Value = Pain_observation
            });
            Pain.Add(new Status
            {
                Key = "A",
                Value = Pain_attention
            });
            Pain.Add(new Status
            {
                Key = "R",
                Value = Pain_received
            });
            dashboard.Result.Pain = Pain;
            #endregion
            #region Pulse
            var Pulse_normal = pulse.Result.Where(j => j.Status == nId).Count();
            var Pulse_observation = pulse.Result.Where(j => j.Status == oId).Count();
            var Pulse_attention = pulse.Result.Where(j => j.Status == aId).Count();
            var Pulse_received = pulse.Result.Where(j => j.Status == rId).Count();
            TeleNormal += Pulse_normal;
            TeleObservation += Pulse_observation;
            TeleAttention += Pulse_attention;
            TeleReceived += Pulse_received;
            var Pulse = new List<Status>();
            Pulse.Add(new Status
            {
                Key = "N",
                Value = Pulse_normal
            });
            Pulse.Add(new Status
            {
                Key = "O",
                Value = Pulse_observation
            });
            Pulse.Add(new Status
            {
                Key = "A",
                Value = Pulse_attention
            });
            Pulse.Add(new Status
            {
                Key = "R",
                Value = Pulse_received
            });
            dashboard.Result.Pulse = Pulse;
            #endregion
            #region Seizure
            var Seizure_normal = seizure.Result.Where(j => j.Status == nId).Count();
            var Seizure_observation = seizure.Result.Where(j => j.Status == oId).Count();
            var Seizure_attention = seizure.Result.Where(j => j.Status == aId).Count();
            var Seizure_received = seizure.Result.Where(j => j.Status == rId).Count();
            TeleNormal += Seizure_normal;
            TeleObservation += Seizure_observation;
            TeleAttention += Seizure_attention;
            TeleReceived += Seizure_received;
            var Seizure = new List<Status>();
            Seizure.Add(new Status
            {
                Key = "N",
                Value = Seizure_normal
            });
            Seizure.Add(new Status
            {
                Key = "O",
                Value = Seizure_observation
            });
            Seizure.Add(new Status
            {
                Key = "A",
                Value = Seizure_attention
            });
            Seizure.Add(new Status
            {
                Key = "R",
                Value = Seizure_received
            });
            dashboard.Result.Seizure = Seizure;
            #endregion
            #region Wound
            var Wound_normal = wound.Result.Where(j => j.Status == nId).Count();
            var Wound_observation = wound.Result.Where(j => j.Status == oId).Count();
            var Wound_attention = wound.Result.Where(j => j.Status == aId).Count();
            var Wound_received = wound.Result.Where(j => j.Status == rId).Count();
            TeleNormal += Wound_normal;
            TeleObservation += Wound_observation;
            TeleAttention += Wound_attention;
            TeleReceived += Wound_received;
            var Wound = new List<Status>();
            Wound.Add(new Status
            {
                Key = "N",
                Value = Wound_normal
            });
            Wound.Add(new Status
            {
                Key = "O",
                Value = Wound_observation
            });
            Wound.Add(new Status
            {
                Key = "A",
                Value = Wound_attention
            });
            Wound.Add(new Status
            {
                Key = "R",
                Value = Wound_received
            });
            dashboard.Result.Wound = Wound;
            #endregion
            #region TeleHealth
            TeleHeath.Add(new Status
            {
                Key = "N",
                Value = TeleNormal
            });
            TeleHeath.Add(new Status
            {
                Key = "O",
                Value = TeleObservation
            });
            TeleHeath.Add(new Status
            {
                Key = "A",
                Value = TeleAttention
            });
            TeleHeath.Add(new Status
            {
                Key = "R",
                Value = TeleReceived
            });
            dashboard.Result.TeleHealth = TeleHeath;
            #endregion

            return Json(dashboard.Result);
        }
        [HttpGet]
        public JsonResult GetAdlTrackerD()
        {
            var dashboard = _dashboardService.Get();
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");
            var types = new List<string> { "AM", "LUNCH", "TEA", "BED", "OTHERS1", "OTHERS2" };
            var rotaAdmins = _rotaTaskService.LiveRota(endDate, endDate);


            dashboard.Result.LiveTrackerD = GetTrackerDaily(endDate, types, rotaAdmins.Result);
            dashboard.Result.Types = types.Distinct().ToList();

            return Json(dashboard.Result);
        }
        [HttpGet]
        public JsonResult GetAdlTrackerW()
        {
            var dashboard = new GetDashboard();
            var days = new List<string>();
            for (int day = 0; day < 7; day++)
            {
                days.Add(DateTime.Now.AddDays(-day).ToString("ddd"));
            }
            days.Reverse();
            var startDate = DateTime.Now.AddDays(-7).Date.ToString("yyyy-MM-dd");
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");
            var rotaAdmins = _rotaTaskService.LiveRota(startDate, endDate);
            dashboard.LiveTrackerW = GetTrackerWeekly(startDate, endDate, days, rotaAdmins.Result);
            dashboard.Days = days.Distinct().ToList();
            return Json(dashboard);
        }
        [HttpGet]
        public JsonResult GetAdlTrackerM()
        {
            var dashboard = new GetDashboard();
            var months = new List<string>();
            for (int month = 0; month < 12; month++)
            {
                months.Add(DateTime.Now.AddMonths(-month).ToString("MMM-yy"));
            }
            months.Reverse();
            var startDate = DateTime.Now.AddDays(-365).Date.ToString("yyyy-MM-dd");
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");
            var rotaAdmins = _rotaTaskService.LiveRota(startDate, endDate);
            dashboard.LiveTrackerM = GetTrackerMonthly(startDate, endDate, months, rotaAdmins.Result);
            dashboard.Months = months.Distinct().ToList();
            return Json(dashboard);
        }

        public async Task<IActionResult> Client()
        {
            var client = await _clientService.GetClients();
            var dashboard = await _dashboardService.Get();
            dashboard.GetClients = client.ToList();
            dashboard.ClientId = 0;
            return View(dashboard);
        }
        [HttpGet]
        public JsonResult ClientDashboard(int clientId)
        {
            var clients = _clientService.GetClients();
            var staffs = _staffService.GetAsync();
            var client = clients.Result.Where(s => s.ClientId == clientId).FirstOrDefault();
            var manager = staffs.Result.Where(s => s.StaffPersonalInfoId == client.ClientManager).FirstOrDefault();
            var keyworker = new GetStaffPersonalInfo();
            var teamleader = new GetStaffPersonalInfo();
            var dashboard =  _dashboardService.Get();
            dashboard.Result.ClientId = clientId;
            dashboard.Result.ClientName = client.Firstname +" "+ client.Middlename +" "+ client.Surname;
            dashboard.Result.GetClients = clients.Result.ToList();
            dashboard.Result.ClientManager = manager != null ? manager : new GetStaffPersonalInfo();
            dashboard.Result.TeamLeader = teamleader != null ? teamleader : new GetStaffPersonalInfo();
            dashboard.Result.KWorker = keyworker != null ? keyworker : new GetStaffPersonalInfo();
            return Json(dashboard.Result);
        }
        public async Task<IActionResult> Employee(int staffId)
        {
            var dashboard = await _dashboardService.Get();
            dashboard.StaffId = staffId;
            return View(dashboard);
        }
        private Dictionary<string,List<GetTaskBoard>> GetTaskBoard(List<GetTaskBoard> task, GetBaseRecordWithItems getBases)
        {
            int creId = getBases.BaseRecordItems.Where(s => s.ValueName == "Created").FirstOrDefault().BaseRecordItemId;
            int progId = getBases.BaseRecordItems.Where(s => s.ValueName == "In Progress").FirstOrDefault().BaseRecordItemId;
            int comId = getBases.BaseRecordItems.Where(s => s.ValueName == "Completed").FirstOrDefault().BaseRecordItemId;
            var _task = new Dictionary<string, List<GetTaskBoard>>();
            var created = task.Where(s => s.Status == creId).ToList();
            var inprogress = task.Where(s => s.Status == progId).ToList();
            var completed = task.Where(s => s.Status == comId).ToList();
            _task.Add("Created",created);
            _task.Add("InProgress", inprogress);
            _task.Add("Completed", completed);
            return _task;
        }

        private List<OnCall> GetOnCall(List<GetDutyOnCall> oncall, List<GetClient> client)
        {
            List<OnCall> reports = new List<OnCall>();
            foreach (GetDutyOnCall item in oncall)
            {
                var report = new OnCall();
                report.DutyOnCallId = item.DutyOnCallId;
                report.Date = item.DateOfCall;
                report.Concern = item.Subject;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.NotificationStatusName = _baseService.GetBaseRecordItemById(item.NotificationStatus).Result.ValueName;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.Firstname +" "+ s.Middlename +" "+ s.Surname).FirstOrDefault();
                report.StaffName = item.PersonToAct.FirstOrDefault().StaffName;
                reports.Add(report);
            }
            return reports;
        }
        private List<ConcernNotes> GetConcernNotes(List<GetTrackingConcernNote> concern)
        {
            List<ConcernNotes> reports = new List<ConcernNotes>();
            foreach (GetTrackingConcernNote item in concern)
            {
                var report = new ConcernNotes();
                report.Ref = item.Ref;
                report.DateOfIncident = item.DateOfIncident;
                report.ExpectedDeadline = item.ExpectedDeadline;
                report.ConcernNote = item.ConcernNote;
                report.ActionRequired = item.ActionRequired;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.ManagerInvolved = item.ManagerInvolved.ToList();
                report.StaffInvolved = item.StaffInvolved.ToList();
                reports.Add(report);
            }
            return reports;
        }
        private Dictionary<string, List<GetStaffRating>> GetStaffRating(List<GetStaffRating> StaffRating)
        {
            Dictionary<string, List<GetStaffRating>> staffRating = new Dictionary<string, List<GetStaffRating>>();
            List<GetStaffRating> ratings = new List<GetStaffRating>();
            var Ids = StaffRating.Select(s => s.StaffPersonalInfoId).Distinct();
            foreach (var item in Ids)
            {
                if (ratings.Count == 0 || ratings.Any(s => s.StaffPersonalInfoId != item))
                {
                    string staffName = StaffRating.Where(s => s.StaffPersonalInfoId == item).FirstOrDefault().Staff;
                    var count = StaffRating.Where(s => s.StaffPersonalInfoId == item).Count();
                    var _rating = StaffRating.Where(s => s.StaffPersonalInfoId == item).Select(s => s.Rating).Sum();
                    int newrating = _rating / count;
                    ratings.Add(new GetStaffRating { Staff = staffName, StaffPersonalInfoId = item, Rating = newrating });
                }                
            }
            var fivestar = ratings.Where(s => s.Rating == 5).ToList();
            var fourstar =  ratings.Where(s => s.Rating == 4).ToList();
            var threestar = ratings.Where(s => s.Rating == 3).ToList();
            var twostar =   ratings.Where(s => s.Rating == 2).ToList();
            var onestar =   ratings.Where(s => s.Rating == 1).ToList();
            
            staffRating.Add("5-Star", fivestar);
            staffRating.Add("4-Star", fourstar);
            staffRating.Add("3-Star", threestar);
            staffRating.Add("2-Star", twostar);
            staffRating.Add("1-Star", onestar);
            staffRating.Add("Total", ratings);

            return staffRating;
        }
        private List<GetStaffPersonalInfo> GetStaffs(List<GetStaffPersonalInfo> getStaffs)
        {
            var currentMonth = DateTime.Now.Date.ToString("MM");
            var staffs = getStaffs.Where(s => Convert.ToDateTime(s.DateOfBirth.ToString()).Date.ToString("MM") == currentMonth && s.Status==StaffRegistrationEnum.Approved).ToList();
            return staffs;
        }
        private List<GetClient> GetClients(List<GetClient> getClients)
        {
            var currentMonth = DateTime.Now.Date.ToString("MM");
            var clients = getClients.Where(s => Convert.ToDateTime(s.DateOfBirth.ToString()).Date.ToString("MM") == currentMonth && s.Status == "Active").ToList();
            return clients;
        }
        #region Adl Tracker
        private Dictionary<string, List<Status>> GetTrackerMonthly(string sdate, string edate, List<string> _months, List<LiveTracker> rotaAdmins)
        {
            var liveTrack = new Dictionary<string, List<Status>>();
            List<Status> missed = new List<Status>();
            List<Status> ontime = new List<Status>();
            List<Status> early = new List<Status>();
            List<Status> mild = new List<Status>();
            List<Status> late = new List<Status>();
            var month = new List<string>();
            var ontimestatus = new List<Status>();
            var earlystatus = new List<Status>();
            var mildstatus = new List<Status>();
            var latestatus = new List<Status>();
            var missedstatus = new List<Status>();
            var months = _months;
            
            string format = "yyyy-MM-dd";
            bool isStartDateValid = DateTime.TryParseExact(sdate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime startDate);
            bool isStopDateValid = DateTime.TryParseExact(edate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime stopDate);
            var rotaAdmin = rotaAdmins.Where(s => s.RotaDate >= startDate && s.RotaDate <= stopDate).ToList();

            var currentTime = DateTimeOffset.UtcNow.AddHours(1).TimeOfDay;
            
            var groupedRota = (from rt in rotaAdmin
                               group rt by rt.Staff into rtGrp
                               select new GroupLiveRota
                               {
                                   StaffName = rtGrp.Key,
                                   Trackers = rtGrp.Where(t => TimeSpan.ParseExact(t.StartTime, "h\\:mm", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.TimeSpanStyles.None) <= currentTime).OrderBy(t => TimeSpan.ParseExact(t.StartTime, "h\\:mm", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.TimeSpanStyles.None)).ToList()

                               }).ToList();

            foreach (GroupLiveRota model in groupedRota)
            {
                foreach (var rota in model.Trackers)
                {
                    string labelCall = "";
                    string callStatus = "MISSED";
                    if (rota.ClockInTime.HasValue)
                    {
                        month.Add(rota.RotaDate.ToString("MMM-yy"));
                        var st = TimeSpan.TryParseExact(rota.StartTime, "h\\:mm", CultureInfo.CurrentCulture, TimeSpanStyles.None, out TimeSpan d) ? d : default(TimeSpan);
                        var ct = TimeSpan.TryParseExact(rota.ClockInTime.Value.AddHours(1).DateTime.TimeOfDay.ToString(), "hh\\:mm\\:ss", CultureInfo.CurrentCulture, TimeSpanStyles.None, out TimeSpan c) ? c : default(TimeSpan);
                        var df = st.Subtract(ct).TotalMinutes;

                        if (df <= 15 && df >= -15)
                        {
                            callStatus = "ONTIME";
                            ontime.Add(new Status { Key = rota.RotaDate.ToString("MMM-yy"), Value = 1 });

                        }
                        else if (df > 15 && df <= 30)
                        {
                            callStatus = "EARLY";
                            early.Add(new Status { Key = rota.RotaDate.ToString("MMM-yy"), Value = 1 });

                        }
                        else if (df >= -30)
                        {
                            callStatus = "MILD";
                            mild.Add(new Status { Key = rota.RotaDate.ToString("MMM-yy"), Value = 1 });

                        }
                        else
                        {
                            callStatus = "LATE";
                            late.Add(new Status { Key = rota.RotaDate.ToString("MMM-yy"), Value = 1 });

                        }
                    }
                    else
                    {
                        month.Add(rota.RotaDate.ToString("MMM-yy"));
                        labelCall = callStatus;
                        missed.Add(new Status { Key = rota.RotaDate.ToString("MMM-yy"), Value = 1 });

                    }

                }
            }
            foreach (var item in months)
            {
                if (month.Contains(item))
                {
                    ontimestatus.Add(new Status { Key = item, Value = ontime.Where(s => s.Key == item).Sum(s => s.Value) });
                    earlystatus.Add(new Status { Key = item, Value = early.Where(s => s.Key == item).Sum(s => s.Value) });
                    mildstatus.Add(new Status { Key = item, Value = mild.Where(s => s.Key == item).Sum(s => s.Value) });
                    latestatus.Add(new Status { Key = item, Value = late.Where(s => s.Key == item).Sum(s => s.Value) });
                    missedstatus.Add(new Status { Key = item, Value = missed.Where(s => s.Key == item).Sum(s => s.Value) });
                }
                else
                {
                    ontimestatus.Add(new Status { Key = item, Value = 0 });
                    earlystatus.Add(new Status { Key = item, Value = 0 });
                    mildstatus.Add(new Status { Key = item, Value = 0 });
                    latestatus.Add(new Status { Key = item, Value = 0 });
                    missedstatus.Add(new Status { Key = item, Value = 0 });
                }
            }
            liveTrack.Add("ONTIME", ontimestatus);
            liveTrack.Add("EARLY", earlystatus);
            liveTrack.Add("MILD", mildstatus);
            liveTrack.Add("LATE", latestatus);
            liveTrack.Add("MISSED", missedstatus);
            return (liveTrack);
        }
        private Dictionary<string, List<Status>> GetTrackerWeekly(string sdate, string edate, List<string> _days, List<LiveTracker> rotaAdmins)
        {
            var liveTrack = new Dictionary<string, List<Status>>();
            List<Status> missed = new List<Status>();
            List<Status> ontime = new List<Status>();
            List<Status> early = new List<Status>();
            List<Status> mild = new List<Status>();
            List<Status> late = new List<Status>();
            var ontimestatus = new List<Status>();
            var earlystatus = new List<Status>();
            var mildstatus = new List<Status>();
            var latestatus = new List<Status>();
            var missedstatus = new List<Status>();
            var day = new List<string>();
            var days = _days;

            string format = "yyyy-MM-dd";
            bool isStartDateValid = DateTime.TryParseExact(sdate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime startDate);
            bool isStopDateValid = DateTime.TryParseExact(edate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime stopDate);
            var rotaAdmin = rotaAdmins.Where(s => s.RotaDate >= startDate && s.RotaDate <= stopDate).ToList();

            var currentTime = DateTimeOffset.UtcNow.AddHours(1).TimeOfDay;

            var groupedRota = (from rt in rotaAdmin
                               group rt by rt.Staff into rtGrp
                               select new GroupLiveRota
                               {
                                   StaffName = rtGrp.Key,
                                   Trackers = rtGrp.Where(t => TimeSpan.ParseExact(t.StartTime, "h\\:mm", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.TimeSpanStyles.None) <= currentTime).OrderBy(t => TimeSpan.ParseExact(t.StartTime, "h\\:mm", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.TimeSpanStyles.None)).ToList()

                               }).ToList();
            foreach (GroupLiveRota model in groupedRota)
            {
                foreach (var rota in model.Trackers)
                {
                    string labelCall = "";
                    string callStatus = "MISSED";
                    if (rota.ClockInTime.HasValue)
                    {
                        day.Add(rota.RotaDate.ToString("ddd"));
                        var st = TimeSpan.TryParseExact(rota.StartTime, "h\\:mm", CultureInfo.CurrentCulture, TimeSpanStyles.None, out TimeSpan d) ? d : default(TimeSpan);
                        var ct = TimeSpan.TryParseExact(rota.ClockInTime.Value.AddHours(1).DateTime.TimeOfDay.ToString(), "hh\\:mm\\:ss", CultureInfo.CurrentCulture, TimeSpanStyles.None, out TimeSpan c) ? c : default(TimeSpan);
                        var df = st.Subtract(ct).TotalMinutes;

                        if (df <= 15 && df >= -15)
                        {
                            callStatus = "ONTIME";
                            ontime.Add(new Status { Key = rota.RotaDate.ToString("ddd"), Value = 1 });

                        }
                        else if (df > 15 && df <= 30)
                        {
                            callStatus = "EARLY";
                            early.Add(new Status { Key = rota.RotaDate.ToString("ddd"), Value = 1 });

                        }
                        else if (df >= -30)
                        {
                            callStatus = "MILD";
                            mild.Add(new Status { Key = rota.RotaDate.ToString("ddd"), Value = 1 });

                        }
                        else
                        {
                            callStatus = "LATE";
                            late.Add(new Status { Key = rota.RotaDate.ToString("ddd"), Value = 1 });

                        }
                    }
                    else
                    {
                        day.Add(rota.RotaDate.ToString("ddd"));
                        labelCall = callStatus;
                        missed.Add(new Status { Key = rota.RotaDate.ToString("ddd"), Value = 1 });

                    }

                }
            }
            foreach (var item in days)
            {
                if (day.Contains(item))
                {
                    ontimestatus.Add(new Status { Key = item, Value = ontime.Where(s => s.Key == item).Sum(s => s.Value) });
                    earlystatus.Add(new Status { Key = item, Value = early.Where(s => s.Key == item).Sum(s => s.Value) });
                    mildstatus.Add(new Status { Key = item, Value = mild.Where(s => s.Key == item).Sum(s => s.Value) });
                    latestatus.Add(new Status { Key = item, Value = late.Where(s => s.Key == item).Sum(s => s.Value) });
                    missedstatus.Add(new Status { Key = item, Value = missed.Where(s => s.Key == item).Sum(s => s.Value) });
                }
                else
                {
                    ontimestatus.Add(new Status { Key = item, Value = 0 });
                    earlystatus.Add(new Status { Key = item, Value = 0 });
                    mildstatus.Add(new Status { Key = item, Value = 0 });
                    latestatus.Add(new Status { Key = item, Value = 0 });
                    missedstatus.Add(new Status { Key = item, Value = 0 });
                }
            }
            liveTrack.Add("ONTIME", ontimestatus);
            liveTrack.Add("EARLY", earlystatus);
            liveTrack.Add("MILD", mildstatus);
            liveTrack.Add("LATE", latestatus);
            liveTrack.Add("MISSED", missedstatus);
            return (liveTrack);
        }
        private Dictionary<string, List<Status>> GetTrackerDaily(string edate, List<string> _types, List<LiveTracker> rotaAdmins)
        {
            var liveTrack = new Dictionary<string, List<Status>>();
            List<Status> missed = new List<Status>();
            List<Status> ontime = new List<Status>();
            List<Status> early = new List<Status>();
            List<Status> mild = new List<Status>();
            List<Status> late = new List<Status>();
            var ontimestatus = new List<Status>();
            var earlystatus = new List<Status>();
            var mildstatus = new List<Status>();
            var latestatus = new List<Status>();
            var missedstatus = new List<Status>();
            var type = new List<string>();
            var types = _types;

            string format = "yyyy-MM-dd";
            bool isStopDateValid = DateTime.TryParseExact(edate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime stopDate);
            var rotaAdmin = rotaAdmins.Where(s => s.RotaDate >= stopDate && s.RotaDate <= stopDate).ToList();

            var currentTime = DateTimeOffset.UtcNow.AddHours(1).TimeOfDay;

            var groupedRota = (from rt in rotaAdmin
                               group rt by rt.Staff into rtGrp
                               select new GroupLiveRota
                               {
                                   StaffName = rtGrp.Key,
                                   Trackers = rtGrp.Where(t => TimeSpan.ParseExact(t.StartTime, "h\\:mm", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.TimeSpanStyles.None) <= currentTime).OrderBy(t => TimeSpan.ParseExact(t.StartTime, "h\\:mm", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.TimeSpanStyles.None)).ToList()

                               }).ToList();
            foreach (GroupLiveRota model in groupedRota)
            {
                foreach (var rota in model.Trackers)
                {
                    string labelCall = "";
                    string callStatus = "MISSED";
                    if (rota.ClockInTime.HasValue)
                    {
                        type.Add(rota.Period.ToString());
                        var st = TimeSpan.TryParseExact(rota.StartTime, "h\\:mm", CultureInfo.CurrentCulture, TimeSpanStyles.None, out TimeSpan d) ? d : default(TimeSpan);
                        var ct = TimeSpan.TryParseExact(rota.ClockInTime.Value.AddHours(1).DateTime.TimeOfDay.ToString(), "hh\\:mm\\:ss", CultureInfo.CurrentCulture, TimeSpanStyles.None, out TimeSpan c) ? c : default(TimeSpan);
                        var df = st.Subtract(ct).TotalMinutes;

                        if (df <= 15 && df >= -15)
                        {
                            callStatus = "ONTIME";
                            ontime.Add(new Status { Key = rota.Period, Value = 1 });

                        }
                        else if (df > 15 && df <= 30)
                        {
                            callStatus = "EARLY";
                            early.Add(new Status { Key = rota.Period, Value = 1 });

                        }
                        else if (df >= -30)
                        {
                            callStatus = "MILD";
                            mild.Add(new Status { Key = rota.Period, Value = 1 });

                        }
                        else
                        {
                            callStatus = "LATE";
                            late.Add(new Status { Key = rota.Period, Value = 1 });

                        }
                    }
                    else
                    {
                        type.Add(rota.Period.ToString());
                        labelCall = callStatus;
                        missed.Add(new Status { Key = rota.Period, Value = 1 });

                    }

                }
            }
            foreach (var item in types)
            {
                if (type.Contains(item))
                {
                    ontimestatus.Add(new Status { Key = item, Value = ontime.Where(s => s.Key == item).Sum(s => s.Value) });
                    earlystatus.Add(new Status { Key = item, Value = early.Where(s => s.Key == item).Sum(s => s.Value) });
                    mildstatus.Add(new Status { Key = item, Value = mild.Where(s => s.Key == item).Sum(s => s.Value) });
                    latestatus.Add(new Status { Key = item, Value = late.Where(s => s.Key == item).Sum(s => s.Value) });
                    missedstatus.Add(new Status { Key = item, Value = missed.Where(s => s.Key == item).Sum(s => s.Value) });
                }
                else
                {
                    ontimestatus.Add(new Status { Key = item, Value = 0 });
                    earlystatus.Add(new Status { Key = item, Value = 0 });
                    mildstatus.Add(new Status { Key = item, Value = 0 });
                    latestatus.Add(new Status { Key = item, Value = 0 });
                    missedstatus.Add(new Status { Key = item, Value = 0 });
                }
            }
            liveTrack.Add("ONTIME", ontimestatus);
            liveTrack.Add("EARLY", earlystatus);
            liveTrack.Add("MILD", mildstatus);
            liveTrack.Add("LATE", latestatus);
            liveTrack.Add("MISSED", missedstatus);
            return (liveTrack);
        }
        #endregion

        [HttpGet]
        public JsonResult CareObj(int carePId, int careCId, int careLId, int clientId)
        {
            var care = _clientCareObj.GetByClient(clientId);
            var pending = care.Result.Where(j => j.Status == carePId).Count();
            var closed = care.Result.Where(j => j.Status == careCId).Count();
            var late = care.Result.Where(j => j.Status == careLId).Count();
            var careObj = new List<Status>();
            careObj.Add(new Status
            {
                Key = "Pending",
                Value = pending
            });
            careObj.Add(new Status
            {
                Key = "Progressing",
                Value = closed
            });
            careObj.Add(new Status
            {
                Key = "Achieved",
                Value = late
            });
            return Json(careObj);
        }

        [HttpGet]
        public JsonResult Telehealth(int nId, int oId, int aId, int rId, int clientId)
        {
            int TeleNormal = 0;
            int TeleObservation = 0;
            int TeleAttention = 0;
            int TeleReceived = 0;

            var coag = _clientBloodCoagService.Get();
            var bmi = _clientBMIService.Get();
            var body = _clientBodyService.Get();
            var bowel = _clientBowelService.Get();
            var eye = _clientEyeService.Get();
            var food = _clientFoodService.Get();
            var heart = _clientHeartService.Get();
            var oxygen = _clientOxygenService.Get();
            var pain = _clientPainService.Get();
            var pulse = _clientPulseService.Get();
            var pressure = _clientPressureService.Get();
            var seizure = _clientSeizureService.Get();
            var wound = _clientWoundService.Get();

            var dashboard = _dashboardService.Get();
            var TeleHeath = new List<Status>();

            #region BloodCoag
            var normal = coag.Result.Where(j => j.Status == nId && j.ClientId == clientId).Count();
            var observation = coag.Result.Where(j => j.Status == oId && j.ClientId == clientId).Count();
            var attention = coag.Result.Where(j => j.Status == aId && j.ClientId == clientId).Count();
            var received = coag.Result.Where(j => j.Status == rId && j.ClientId == clientId).Count();
            TeleNormal += normal;
            TeleObservation += observation;
            TeleAttention += attention;
            TeleReceived += received;
            var BloodCoag = new List<Status>();
            BloodCoag.Add(new Status
            {
                Key = "N",
                Value = normal
            });
            BloodCoag.Add(new Status
            {
                Key = "O",
                Value = observation
            });
            BloodCoag.Add(new Status
            {
                Key = "A",
                Value = attention
            });
            BloodCoag.Add(new Status
            {
                Key = "R",
                Value = received
            });
            dashboard.Result.BloodCoag = BloodCoag;
            #endregion
            #region BloodPressure
            var pnormal = pressure.Result.Where(j => j.Status == nId && j.ClientId == clientId).Count();
            var pobservation = pressure.Result.Where(j => j.Status == oId && j.ClientId == clientId).Count();
            var pattention = pressure.Result.Where(j => j.Status == aId && j.ClientId == clientId).Count();
            var preceived = pressure.Result.Where(j => j.Status == rId && j.ClientId == clientId).Count();
            TeleNormal += pnormal;
            TeleObservation += pobservation;
            TeleAttention += pattention;
            TeleReceived += preceived;
            var Pressure = new List<Status>();
            Pressure.Add(new Status
            {
                Key = "N",
                Value = pnormal
            });
            Pressure.Add(new Status
            {
                Key = "O",
                Value = pobservation
            });
            Pressure.Add(new Status
            {
                Key = "A",
                Value = pattention
            });
            Pressure.Add(new Status
            {
                Key = "R",
                Value = preceived
            });
            dashboard.Result.Pressure = Pressure;
            #endregion
            #region BMI
            var bmi_normal = bmi.Result.Where(j => j.Status == nId && j.ClientId == clientId).Count();
            var bmi_observation = bmi.Result.Where(j => j.Status == oId && j.ClientId == clientId).Count();
            var bmi_attention = bmi.Result.Where(j => j.Status == aId && j.ClientId == clientId).Count();
            var bmi_received = bmi.Result.Where(j => j.Status == rId && j.ClientId == clientId).Count();
            TeleNormal += bmi_normal;
            TeleObservation += bmi_observation;
            TeleAttention += bmi_attention;
            TeleReceived += bmi_received;
            var BMI = new List<Status>();
            BMI.Add(new Status
            {
                Key = "N",
                Value = bmi_normal
            });
            BMI.Add(new Status
            {
                Key = "O",
                Value = bmi_observation
            });
            BMI.Add(new Status
            {
                Key = "A",
                Value = bmi_attention
            });
            BMI.Add(new Status
            {
                Key = "R",
                Value = bmi_received
            });
            dashboard.Result.BMI = BMI;
            #endregion
            #region Body
            var Body_normal = body.Result.Where(j => j.Status == nId && j.ClientId == clientId).Count();
            var Body_observation = body.Result.Where(j => j.Status == oId && j.ClientId == clientId).Count();
            var Body_attention = body.Result.Where(j => j.Status == aId && j.ClientId == clientId).Count();
            var Body_received = body.Result.Where(j => j.Status == rId && j.ClientId == clientId).Count();
            TeleNormal += Body_normal;
            TeleObservation += Body_observation;
            TeleAttention += Body_attention;
            TeleReceived += Body_received;
            var Body = new List<Status>();
            Body.Add(new Status
            {
                Key = "N",
                Value = Body_normal
            });
            Body.Add(new Status
            {
                Key = "O",
                Value = Body_observation
            });
            Body.Add(new Status
            {
                Key = "A",
                Value = Body_attention
            });
            Body.Add(new Status
            {
                Key = "R",
                Value = Body_received
            });
            dashboard.Result.Body = Body;
            #endregion
            #region Bowel
            var Bowel_normal = bowel.Result.Where(j => j.Status == nId && j.ClientId == clientId).Count();
            var Bowel_observation = bowel.Result.Where(j => j.Status == oId && j.ClientId == clientId).Count();
            var Bowel_attention = bowel.Result.Where(j => j.Status == aId && j.ClientId == clientId).Count();
            var Bowel_received = bowel.Result.Where(j => j.Status == rId && j.ClientId == clientId).Count();
            TeleNormal += Bowel_normal;
            TeleObservation += Bowel_observation;
            TeleAttention += Bowel_attention;
            TeleReceived += Bowel_received;
            var Bowel = new List<Status>();
            Bowel.Add(new Status
            {
                Key = "N",
                Value = Bowel_normal
            });
            Bowel.Add(new Status
            {
                Key = "O",
                Value = Bowel_observation
            });
            Bowel.Add(new Status
            {
                Key = "A",
                Value = Bowel_attention
            });
            Bowel.Add(new Status
            {
                Key = "R",
                Value = Bowel_received
            });
            dashboard.Result.Bowel = Bowel;
            #endregion
            #region Eye
            var Eye_normal = eye.Result.Where(j => j.Status == nId && j.ClientId == clientId).Count();
            var Eye_observation = eye.Result.Where(j => j.Status == oId && j.ClientId == clientId).Count();
            var Eye_attention = eye.Result.Where(j => j.Status == aId && j.ClientId == clientId).Count();
            var Eye_received = eye.Result.Where(j => j.Status == rId && j.ClientId == clientId).Count();
            TeleNormal += Eye_normal;
            TeleObservation += Eye_observation;
            TeleAttention += Eye_attention;
            TeleReceived += Eye_received;
            var Eye = new List<Status>();
            Eye.Add(new Status
            {
                Key = "N",
                Value = Eye_normal
            });
            Eye.Add(new Status
            {
                Key = "O",
                Value = Eye_observation
            });
            Eye.Add(new Status
            {
                Key = "A",
                Value = Eye_attention
            });
            Eye.Add(new Status
            {
                Key = "R",
                Value = Eye_received
            });
            dashboard.Result.EyeHealth = Eye;
            #endregion
            #region Food
            var Food_normal = food.Result.Where(j => j.Status == nId && j.ClientId == clientId).Count();
            var Food_observation = food.Result.Where(j => j.Status == oId && j.ClientId == clientId).Count();
            var Food_attention = food.Result.Where(j => j.Status == aId && j.ClientId == clientId).Count();
            var Food_received = food.Result.Where(j => j.Status == rId && j.ClientId == clientId).Count();
            TeleNormal += Food_normal;
            TeleObservation += Food_observation;
            TeleAttention += Food_attention;
            TeleReceived += Food_received;
            var Food = new List<Status>();
            Food.Add(new Status
            {
                Key = "N",
                Value = Food_normal
            });
            Food.Add(new Status
            {
                Key = "O",
                Value = Food_observation
            });
            Food.Add(new Status
            {
                Key = "A",
                Value = Food_attention
            });
            Food.Add(new Status
            {
                Key = "R",
                Value = Food_received
            });
            dashboard.Result.Food = Food;
            #endregion
            #region Heart
            var Heart_normal = heart.Result.Where(j => j.Status == nId && j.ClientId == clientId).Count();
            var Heart_observation = heart.Result.Where(j => j.Status == oId && j.ClientId == clientId).Count();
            var Heart_attention = heart.Result.Where(j => j.Status == aId && j.ClientId == clientId).Count();
            var Heart_received = heart.Result.Where(j => j.Status == rId && j.ClientId == clientId).Count();
            TeleNormal += Heart_normal;
            TeleObservation += Heart_observation;
            TeleAttention += Heart_attention;
            TeleReceived += Heart_received;
            var Heart = new List<Status>();
            Heart.Add(new Status
            {
                Key = "N",
                Value = Heart_normal
            });
            Heart.Add(new Status
            {
                Key = "O",
                Value = Heart_observation
            });
            Heart.Add(new Status
            {
                Key = "A",
                Value = Heart_attention
            });
            Heart.Add(new Status
            {
                Key = "R",
                Value = Heart_received
            });
            dashboard.Result.Heart = Heart;
            #endregion
            #region Oxygen
            var Oxygen_normal = oxygen.Result.Where(j => j.Status == nId && j.ClientId == clientId).Count();
            var Oxygen_observation = oxygen.Result.Where(j => j.Status == oId && j.ClientId == clientId).Count();
            var Oxygen_attention = oxygen.Result.Where(j => j.Status == aId && j.ClientId == clientId).Count();
            var Oxygen_received = oxygen.Result.Where(j => j.Status == rId && j.ClientId == clientId).Count();
            TeleNormal += Oxygen_normal;
            TeleObservation += Oxygen_observation;
            TeleAttention += Oxygen_attention;
            TeleReceived += Oxygen_received;
            var Oxygen = new List<Status>();
            Oxygen.Add(new Status
            {
                Key = "N",
                Value = Oxygen_normal
            });
            Oxygen.Add(new Status
            {
                Key = "O",
                Value = Oxygen_observation
            });
            Oxygen.Add(new Status
            {
                Key = "A",
                Value = Oxygen_attention
            });
            Oxygen.Add(new Status
            {
                Key = "R",
                Value = Oxygen_received
            });
            dashboard.Result.Oxygen = Oxygen;
            #endregion
            #region Pain
            var Pain_normal = pain.Result.Where(j => j.Status == nId && j.ClientId == clientId).Count();
            var Pain_observation = pain.Result.Where(j => j.Status == oId && j.ClientId == clientId).Count();
            var Pain_attention = pain.Result.Where(j => j.Status == aId && j.ClientId == clientId).Count();
            var Pain_received = pain.Result.Where(j => j.Status == rId && j.ClientId == clientId).Count();
            TeleNormal += Pain_normal;
            TeleObservation += Pain_observation;
            TeleAttention += Pain_attention;
            TeleReceived += Pain_received;
            var Pain = new List<Status>();
            Pain.Add(new Status
            {
                Key = "N",
                Value = Pain_normal
            });
            Pain.Add(new Status
            {
                Key = "O",
                Value = Pain_observation
            });
            Pain.Add(new Status
            {
                Key = "A",
                Value = Pain_attention
            });
            Pain.Add(new Status
            {
                Key = "R",
                Value = Pain_received
            });
            dashboard.Result.Pain = Pain;
            #endregion
            #region Pulse
            var Pulse_normal = pulse.Result.Where(j => j.Status == nId && j.ClientId == clientId).Count();
            var Pulse_observation = pulse.Result.Where(j => j.Status == oId && j.ClientId == clientId).Count();
            var Pulse_attention = pulse.Result.Where(j => j.Status == aId && j.ClientId == clientId).Count();
            var Pulse_received = pulse.Result.Where(j => j.Status == rId && j.ClientId == clientId).Count();
            TeleNormal += Pulse_normal;
            TeleObservation += Pulse_observation;
            TeleAttention += Pulse_attention;
            TeleReceived += Pulse_received;
            var Pulse = new List<Status>();
            Pulse.Add(new Status
            {
                Key = "N",
                Value = Pulse_normal
            });
            Pulse.Add(new Status
            {
                Key = "O",
                Value = Pulse_observation
            });
            Pulse.Add(new Status
            {
                Key = "A",
                Value = Pulse_attention
            });
            Pulse.Add(new Status
            {
                Key = "R",
                Value = Pulse_received
            });
            dashboard.Result.Pulse = Pulse;
            #endregion
            #region Seizure
            var Seizure_normal = seizure.Result.Where(j => j.Status == nId && j.ClientId == clientId).Count();
            var Seizure_observation = seizure.Result.Where(j => j.Status == oId && j.ClientId == clientId).Count();
            var Seizure_attention = seizure.Result.Where(j => j.Status == aId && j.ClientId == clientId).Count();
            var Seizure_received = seizure.Result.Where(j => j.Status == rId && j.ClientId == clientId).Count();
            TeleNormal += Seizure_normal;
            TeleObservation += Seizure_observation;
            TeleAttention += Seizure_attention;
            TeleReceived += Seizure_received;
            var Seizure = new List<Status>();
            Seizure.Add(new Status
            {
                Key = "N",
                Value = Seizure_normal
            });
            Seizure.Add(new Status
            {
                Key = "O",
                Value = Seizure_observation
            });
            Seizure.Add(new Status
            {
                Key = "A",
                Value = Seizure_attention
            });
            Seizure.Add(new Status
            {
                Key = "R",
                Value = Seizure_received
            });
            dashboard.Result.Seizure = Seizure;
            #endregion
            #region Wound
            var Wound_normal = wound.Result.Where(j => j.Status == nId && j.ClientId == clientId).Count();
            var Wound_observation = wound.Result.Where(j => j.Status == oId && j.ClientId == clientId).Count();
            var Wound_attention = wound.Result.Where(j => j.Status == aId && j.ClientId == clientId).Count();
            var Wound_received = wound.Result.Where(j => j.Status == rId && j.ClientId == clientId).Count();
            TeleNormal += Wound_normal;
            TeleObservation += Wound_observation;
            TeleAttention += Wound_attention;
            TeleReceived += Wound_received;
            var Wound = new List<Status>();
            Wound.Add(new Status
            {
                Key = "N",
                Value = Wound_normal
            });
            Wound.Add(new Status
            {
                Key = "O",
                Value = Wound_observation
            });
            Wound.Add(new Status
            {
                Key = "A",
                Value = Wound_attention
            });
            Wound.Add(new Status
            {
                Key = "R",
                Value = Wound_received
            });
            dashboard.Result.Wound = Wound;
            #endregion
            #region TeleHealth
            TeleHeath.Add(new Status
            {
                Key = "N",
                Value = TeleNormal
            });
            TeleHeath.Add(new Status
            {
                Key = "O",
                Value = TeleObservation
            });
            TeleHeath.Add(new Status
            {
                Key = "A",
                Value = TeleAttention
            });
            TeleHeath.Add(new Status
            {
                Key = "R",
                Value = TeleReceived
            });
            dashboard.Result.TeleHealth = TeleHeath;
            #endregion
            return Json(dashboard.Result);
        }
        [HttpGet]
        public JsonResult Ongoing(int carePId, int careCId, int careLId, int clientId)
        {
            var care = _clientServiceWatchService.Get();
            var pending = care.Result.Where(j => j.Status == carePId && j.ClientId == clientId).Count();
            var closed = care.Result.Where(j => j.Status == careCId && j.ClientId == clientId).Count();
            var late = care.Result.Where(j => j.Status == careLId && j.ClientId == clientId).Count();
            var careObj = new List<Status>();
            careObj.Add(new Status
            {
                Key = "Pending",
                Value = pending
            });
            careObj.Add(new Status
            {
                Key = "Progressing",
                Value = closed
            });
            careObj.Add(new Status
            {
                Key = "Achieved",
                Value = late
            });
            return Json(careObj);
        }
        [HttpGet]
        public JsonResult Performance(int carePId, int careCId, int careLId, int clientId)
        {
            var dashboard = _dashboardService.Get();
            #region variables
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");
            bool isStopDateValid = DateTime.TryParseExact(endDate, "yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime stopDate);

            int clientPending = 0;
            int clientClosed = 0;
            int clientLate = 0;

            var pId = carePId;
            var cId = careCId;
            var lId = careLId;

            var getClient = _clientService.GetClients();
            var log = _clientLogAuditService.Get();
            var med = _clientMedAuditService.Get();
            var complain = _clientComplainService.Get();
            var voice = _clientVoiceService.Get();
            var watch = _clientServiceWatchService.Get();
            var mgt = _clientMgtVisitService.Get();
            var prog = _clientProgramService.Get();

            var baserecordItem = _baseService.GetBaseRecordsWithItems();
            #endregion
            var Client = new List<Status>();

            #region LogAudit
            var log_pending = log.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day) && j.ClientId == clientId).Count();
            var log_closed = log.Result.Where(j => j.Status == cId && j.ClientId == clientId).Count();
            var log_late = log.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day) && j.ClientId == clientId).Count();
            clientPending += log_pending;
            clientClosed += log_closed;
            clientLate += log_late;
            var LogAudit = new List<Status>();
            LogAudit.Add(new Status
            {
                Key = "Pending",
                Value = log_pending
            });
            LogAudit.Add(new Status
            {
                Key = "Closed",
                Value = log_closed
            });
            LogAudit.Add(new Status
            {
                Key = "Late",
                Value = log_late
            });
            dashboard.Result.LogAudit = LogAudit;
            #endregion
            #region MedAudit
            var med_pending = med.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day) && j.ClientId == clientId).Count();
            var med_closed = med.Result.Where(j => j.Status == cId && j.ClientId == clientId).Count();
            var med_late = med.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day) && j.ClientId == clientId).Count();
            clientPending += med_pending;
            clientClosed += med_closed;
            clientLate += med_late;
            var MedAudit = new List<Status>();
            MedAudit.Add(new Status
            {
                Key = "Pending",
                Value = med_pending
            });
            MedAudit.Add(new Status
            {
                Key = "Closed",
                Value = med_closed
            });
            MedAudit.Add(new Status
            {
                Key = "Late",
                Value = med_late
            });
            dashboard.Result.MedAudit = MedAudit;
            #endregion
            #region CompAudit
            var Comp_pending = complain.Result.Where(j => j.StatusId == pId && (j.DUEDATE.Year >= stopDate.Year && j.DUEDATE.Month >= stopDate.Month && j.DUEDATE.Day > stopDate.Day) && j.ClientId == clientId).Count();
            var Comp_closed = complain.Result.Where(j => j.StatusId == cId && j.ClientId == clientId).Count();
            var Comp_late = complain.Result.Where(j => j.StatusId == pId && (j.DUEDATE.Year <= stopDate.Year && j.DUEDATE.Month <= stopDate.Month && j.DUEDATE.Day < stopDate.Day) && j.ClientId == clientId).Count();
            clientPending += Comp_pending;
            clientClosed += Comp_closed;
            clientLate += Comp_late;
            var Comp = new List<Status>();
            Comp.Add(new Status
            {
                Key = "Pending",
                Value = Comp_pending
            });
            Comp.Add(new Status
            {
                Key = "Closed",
                Value = Comp_closed
            });
            Comp.Add(new Status
            {
                Key = "Late",
                Value = Comp_late
            });
            dashboard.Result.Complain = Comp;
            #endregion
            #region Voice
            var voice_pending = voice.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day) && j.ClientId == clientId).Count();
            var voice_closed = voice.Result.Where(j => j.Status == cId && j.ClientId == clientId).Count();
            var voice_late = voice.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day) && j.ClientId == clientId).Count();
            clientPending += voice_pending;
            clientClosed += voice_closed;
            clientLate += voice_late;
            var Voice = new List<Status>();
            Voice.Add(new Status
            {
                Key = "Pending",
                Value = voice_pending
            });
            Voice.Add(new Status
            {
                Key = "Closed",
                Value = voice_closed
            });
            Voice.Add(new Status
            {
                Key = "Late",
                Value = voice_late
            });
            dashboard.Result.Voice = Voice;
            #endregion
            #region Program
            var prog_pending = prog.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day) && j.ClientId == clientId).Count();
            var prog_closed = prog.Result.Where(j => j.Status == cId && j.ClientId == clientId).Count();
            var prog_late = prog.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day) && j.ClientId == clientId).Count();
            clientPending += prog_pending;
            clientClosed += prog_closed;
            clientLate += prog_late;
            var Program = new List<Status>();
            Program.Add(new Status
            {
                Key = "Pending",
                Value = prog_pending
            });
            Program.Add(new Status
            {
                Key = "Closed",
                Value = prog_closed
            });
            Program.Add(new Status
            {
                Key = "Late",
                Value = prog_late
            });
            dashboard.Result.Program = Program;
            #endregion
            #region Watch
            var watch_pending = watch.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day) && j.ClientId == clientId).Count();
            var watch_closed = watch.Result.Where(j => j.Status == cId && j.ClientId == clientId).Count();
            var watch_late = watch.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day) && j.ClientId == clientId).Count();
            clientPending += watch_pending;
            clientClosed += watch_closed;
            clientLate += watch_late;
            var Watch = new List<Status>();
            Watch.Add(new Status
            {
                Key = "Pending",
                Value = watch_pending
            });
            Watch.Add(new Status
            {
                Key = "Closed",
                Value = watch_closed
            });
            Watch.Add(new Status
            {
                Key = "Late",
                Value = watch_late
            });
            dashboard.Result.ServiceWatch = Watch;
            #endregion
            #region MgtVisit
            var mgt_pending = mgt.Result.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day) && j.ClientId == clientId).Count();
            var mgt_closed = mgt.Result.Where(j => j.Status == cId && j.ClientId == clientId).Count();
            var mgt_late = mgt.Result.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day) && j.ClientId == clientId).Count();
            clientPending += mgt_pending;
            clientClosed += mgt_closed;
            clientLate += mgt_late;
            var MgtVisit = new List<Status>();
            MgtVisit.Add(new Status
            {
                Key = "Pending",
                Value = mgt_pending
            });
            MgtVisit.Add(new Status
            {
                Key = "Closed",
                Value = mgt_closed
            });
            MgtVisit.Add(new Status
            {
                Key = "Late",
                Value = mgt_late
            });
            dashboard.Result.MgtVisit = MgtVisit;
            #endregion
            #region ClientMatrix
            Client.Add(new Status
            {
                Key = "Pending",
                Value = clientPending
            });
            Client.Add(new Status
            {
                Key = "Closed",
                Value = clientClosed
            });
            Client.Add(new Status
            {
                Key = "Late",
                Value = clientLate
            });
            dashboard.Result.ClientMatrix = Client;
            #endregion
            return Json(dashboard.Result);
        }
        [HttpGet]
        public JsonResult Supervision(int pId, int cId, int lId)
        {
            var care = _staffSupervisionService.Get();
            var pending = care.Result.Where(j => j.Status == pId).Count();
            var closed = care.Result.Where(j => j.Status == cId).Count();
            var late = care.Result.Where(j => j.Status == lId).Count();
            var careObj = new List<Status>();
            careObj.Add(new Status
            {
                Key = "Pending",
                Value = pending
            });
            careObj.Add(new Status
            {
                Key = "Closed",
                Value = closed
            });
            careObj.Add(new Status
            {
                Key = "Late",
                Value = late
            });
            return Json(careObj);
        }
    }
}
