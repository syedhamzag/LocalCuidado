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

namespace AwesomeCare.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        #region declare Service
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
        ITrackingConcernNote concernNote) : base(fileUpload)
        {
            #region Services
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
            #endregion
        }
        public async Task<IActionResult> Dashboard()
        {
            var dashboard = await _dashboardService.Get();
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");
            bool isStopDateValid = DateTime.TryParseExact(endDate, "yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime stopDate);
            #region variables
            int TeleNormal = 0;
            int TeleObservation = 0;
            int TeleAttention = 0;
            int TeleReceived = 0;

            int clientPending = 0;
            int clientClosed = 0;
            int clientLate = 0;

            int staffPending = 0;
            int staffClosed = 0;
            int staffLate = 0;

            var nId = dashboard.nId;
            var oId = dashboard.oId;
            var aId = dashboard.aId;
            var rId = dashboard.rId;

            var pId = dashboard.pId;
            var cId = dashboard.cId;
            var lId = dashboard.lId;

            var oncallIdP = dashboard.oncallP;
            var oncallIdC = dashboard.oncallC;
            var oncallIdO = dashboard.oncallO;

            var ConcernIdP = dashboard.ConcernIdP;
            var ConcernIdC = dashboard.ConcernIdC;
            var ConcernIdO = dashboard.ConcernIdO;

            var getStaff = await _staffService.GetAsync();
            var getClient = await _clientService.GetClients();
            var log = await _clientLogAuditService.Get();
            var med = await _clientMedAuditService.Get();
            var complain = await _clientComplainService.Get();
            var voice = await _clientVoiceService.Get();
            var watch = await _clientServiceWatchService.Get();
            var mgt = await _clientMgtVisitService.Get();
            var prog = await _clientProgramService.Get();
            var coag = await _clientBloodCoagService.Get();
            var bmi = await _clientBMIService.Get();
            var body = await _clientBodyService.Get();
            var bowel = await _clientBowelService.Get();
            var eye = await _clientEyeService.Get();
            var food = await _clientFoodService.Get();
            var heart = await _clientHeartService.Get();
            var oxygen = await _clientOxygenService.Get();
            var pain = await _clientPainService.Get();
            var pulse = await _clientPulseService.Get();
            var pressure = await _clientPressureService.Get();
            var seizure = await _clientSeizureService.Get();
            var wound = await _clientWoundService.Get();
            var adl = await _staffAdlObsService.Get();
            var key = await _staffKeyWorkerService.Get();
            var comp = await _staffMedCompService.Get();
            var one = await _staffOneToOneService.Get();
            var refs = await _staffReferenceService.Get();
            var spot = await _staffSpotCheckService.Get();
            var super = await _staffSupervisionService.Get();
            var survey = await _staffSurveyService.Get();
            var rating = await _staffService.GetClientFeedback();
            var oncall = await _oncallService.GetWithPersonToAct();
            var task = await _taskService.GetWithStaff();
            var concern = await _concernNote.GetWithChild();
            var baserecordItem = await _baseService.GetBaseRecordsWithItems();
            #endregion
            var Client = new List<Status>();
            #region Oncall
            var Oncall = new List<Status>();
            var oncallpending = oncall.Where(s => s.Status == oncallIdP).Count();
            var oncallclosed = oncall.Where(s => s.Status == oncallIdC).Count();
            var oncallopen = oncall.Where(s => s.Status == oncallIdO).Count();
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
            dashboard.OnCallGraph = Oncall;
            #endregion
            #region Concern
            var Concern = new List<Status>();
            var Concernpending = concern.Where(s => s.Status == ConcernIdP).Count();
            var Concernclosed = concern.Where(s => s.Status == ConcernIdC).Count();
            var Concernopen = concern.Where(s => s.Status == ConcernIdO).Count();
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
            dashboard.concernNoteGraph = Concern;
            #endregion
            #region LogAudit
            var log_pending = log.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var log_closed = log.Where(j =>  j.Status == cId).Count();
            var log_late = log.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
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
            dashboard.LogAudit = LogAudit;
            #endregion
            #region MedAudit
            var med_pending = med.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var med_closed = med.Where(j => j.Status == cId).Count();
            var med_late = med.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
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
            dashboard.MedAudit = MedAudit;
            #endregion
            #region CompAudit
            var Comp_pending = complain.Where(j => j.StatusId == pId && (j.DUEDATE.Year >= stopDate.Year && j.DUEDATE.Month >= stopDate.Month && j.DUEDATE.Day > stopDate.Day)).Count();
            var Comp_closed = complain.Where(j => j.StatusId == cId).Count();
            var Comp_late = complain.Where(j => j.StatusId == pId && (j.DUEDATE.Year <= stopDate.Year && j.DUEDATE.Month <= stopDate.Month && j.DUEDATE.Day < stopDate.Day)).Count();
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
            dashboard.Complain = Comp;
            #endregion
            #region Voice
            var voice_pending = voice.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var voice_closed = voice.Where(j => j.Status == cId).Count();
            var voice_late = voice.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
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
            dashboard.Voice = Voice;
            #endregion
            #region Program
            var prog_pending = prog.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var prog_closed = prog.Where(j => j.Status == cId).Count();
            var prog_late = prog.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
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
            dashboard.Program = Program;
            #endregion
            #region Watch
            var watch_pending = watch.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var watch_closed = watch.Where(j => j.Status == cId).Count();
            var watch_late = watch.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
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
            dashboard.ServiceWatch = Watch;
            #endregion
            #region MgtVisit
            var mgt_pending = mgt.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var mgt_closed = mgt.Where(j => j.Status == cId).Count();
            var mgt_late = mgt.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
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
            dashboard.MgtVisit = MgtVisit;
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
            dashboard.ClientMatrix = Client;
            #endregion

            var Staff = new List<Status>();
            #region AdlObs
            var obs_pending = adl.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var obs_closed = adl.Where(j => j.Status == cId).Count();
            var obs_late = adl.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
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
            dashboard.AdlObs = AdlObs;
            #endregion
            #region KeyWorker
            var key_pending = key.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var key_closed = key.Where(j => j.Status == cId).Count();
            var key_late = key.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
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
            dashboard.KeyWorker = KeyWorker;
            #endregion
            #region MedComp
            var md_pending = comp.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var md_closed = comp.Where(j => j.Status == cId).Count();
            var md_late = comp.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
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
            dashboard.MedComp = MedComp;
            #endregion
            #region One
            var one_pending = one.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var one_closed = one.Where(j => j.Status == cId).Count();
            var one_late = one.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
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
            dashboard.OneToOne = One;
            #endregion
            #region Ref
            var ref_pending = refs.Where(j => j.Status == pId).Count();
            var ref_closed = refs.Where(j => j.Status == cId).Count();
            var ref_late = refs.Where(j => j.Status == lId).Count();
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
            dashboard.Reference = Ref;
            #endregion
            #region Spot
            var spot_pending = spot.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var spot_closed = spot.Where(j => j.Status == cId).Count();
            var spot_late = spot.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
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
            dashboard.SpotCheck = Spot;
            #endregion
            #region Super
            var sup_pending = super.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var sup_closed = super.Where(j => j.Status == cId).Count();
            var sup_late = super.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
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
            dashboard.Supervision = Super;
            #endregion
            #region Survey
            var sur_pending = survey.Where(j => j.Status == pId && (j.Deadline.Year >= stopDate.Year && j.Deadline.Month >= stopDate.Month && j.Deadline.Day > stopDate.Day)).Count();
            var sur_closed = survey.Where(j => j.Status == cId).Count();
            var sur_late = survey.Where(j => j.Status == pId && (j.Deadline.Year <= stopDate.Year && j.Deadline.Month <= stopDate.Month && j.Deadline.Day < stopDate.Day)).Count();
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
            dashboard.Survey = Survey;
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
            dashboard.StaffMatrix = Staff;
            #endregion

            var TeleHeath = new List<Status>();

            #region BloodCoag
            var normal = coag.Where(j => j.Status == nId).Count();
            var observation = coag.Where(j => j.Status == oId).Count();
            var attention = coag.Where(j => j.Status == aId).Count();
            var received = coag.Where(j => j.Status == rId).Count();
            TeleNormal += normal;
            TeleObservation += observation;
            TeleAttention += attention;
            TeleReceived += received;
            var BloodCoag = new List<Status>();
            BloodCoag.Add(new Status
            {
                Key = "Normal",
                Value = normal
            });
            BloodCoag.Add(new Status
            {
                Key = "Under Observation",
                Value = observation
            });
            BloodCoag.Add(new Status
            {
                Key = "Urgent Attention",
                Value = attention
            });
            BloodCoag.Add(new Status
            {
                Key = "Response Received",
                Value = received
            });
            dashboard.BloodCoag = BloodCoag;
            #endregion
            #region BloodPressure
            var pnormal = pressure.Where(j => j.Status == nId).Count();
            var pobservation = pressure.Where(j => j.Status == oId).Count();
            var pattention = pressure.Where(j => j.Status == aId).Count();
            var preceived = pressure.Where(j => j.Status == rId).Count();
            TeleNormal += pnormal;
            TeleObservation += pobservation;
            TeleAttention += pattention;
            TeleReceived += preceived;
            var Pressure = new List<Status>();
            Pressure.Add(new Status
            {
                Key = "Normal",
                Value = pnormal
            });
            Pressure.Add(new Status
            {
                Key = "Under Observation",
                Value = pobservation
            });
            Pressure.Add(new Status
            {
                Key = "Urgent Attention",
                Value = pattention
            });
            Pressure.Add(new Status
            {
                Key = "Response Received",
                Value = preceived
            });
            dashboard.Pressure = Pressure;
            #endregion
            #region BMI
            var bmi_normal = bmi.Where(j => j.Status == nId).Count();
            var bmi_observation = bmi.Where(j => j.Status == oId).Count();
            var bmi_attention = bmi.Where(j => j.Status == aId).Count();
            var bmi_received = bmi.Where(j => j.Status == rId).Count();
            TeleNormal += bmi_normal;
            TeleObservation += bmi_observation;
            TeleAttention += bmi_attention;
            TeleReceived += bmi_received;
            var BMI = new List<Status>();
            BMI.Add(new Status
            {
                Key = "Normal",
                Value = bmi_normal
            });
            BMI.Add(new Status
            {
                Key = "Under Observation",
                Value = bmi_observation
            });
            BMI.Add(new Status
            {
                Key = "Urgent Attention",
                Value = bmi_attention
            });
            BMI.Add(new Status
            {
                Key = "Response Received",
                Value = bmi_received
            });
            dashboard.BMI = BMI;
            #endregion
            #region Body
            var Body_normal = body.Where(j => j.Status == nId).Count();
            var Body_observation = body.Where(j => j.Status == oId).Count();
            var Body_attention = body.Where(j => j.Status == aId).Count();
            var Body_received = body.Where(j => j.Status == rId).Count();
            TeleNormal += Body_normal;
            TeleObservation += Body_observation;
            TeleAttention += Body_attention;
            TeleReceived += Body_received;
            var Body = new List<Status>();
            Body.Add(new Status
            {
                Key = "Normal",
                Value = Body_normal
            });
            Body.Add(new Status
            {
                Key = "Under Observation",
                Value = Body_observation
            });
            Body.Add(new Status
            {
                Key = "Urgent Attention",
                Value = Body_attention
            });
            Body.Add(new Status
            {
                Key = "Response Received",
                Value = Body_received
            });
            dashboard.Body = Body;
            #endregion
            #region Bowel
            var Bowel_normal = bowel.Where(j => j.Status == nId).Count();
            var Bowel_observation = bowel.Where(j => j.Status == oId).Count();
            var Bowel_attention = bowel.Where(j => j.Status == aId).Count();
            var Bowel_received = bowel.Where(j => j.Status == rId).Count();
            TeleNormal += Bowel_normal;
            TeleObservation += Bowel_observation;
            TeleAttention += Bowel_attention;
            TeleReceived += Bowel_received;
            var Bowel = new List<Status>();
            Bowel.Add(new Status
            {
                Key = "Normal",
                Value = Bowel_normal
            });
            Bowel.Add(new Status
            {
                Key = "Under Observation",
                Value = Bowel_observation
            });
            Bowel.Add(new Status
            {
                Key = "Urgent Attention",
                Value = Bowel_attention
            });
            Bowel.Add(new Status
            {
                Key = "Response Received",
                Value = Bowel_received
            });
            dashboard.Bowel = Bowel;
            #endregion
            #region Eye
            var Eye_normal = eye.Where(j => j.Status == nId).Count();
            var Eye_observation = eye.Where(j => j.Status == oId).Count();
            var Eye_attention = eye.Where(j => j.Status == aId).Count();
            var Eye_received = eye.Where(j => j.Status == rId).Count();
            TeleNormal += Eye_normal;
            TeleObservation += Eye_observation;
            TeleAttention += Eye_attention;
            TeleReceived += Eye_received;
            var Eye = new List<Status>();
            Eye.Add(new Status
            {
                Key = "Normal",
                Value = Eye_normal
            });
            Eye.Add(new Status
            {
                Key = "Under Observation",
                Value = Eye_observation
            });
            Eye.Add(new Status
            {
                Key = "Urgent Attention",
                Value = Eye_attention
            });
            Eye.Add(new Status
            {
                Key = "Response Received",
                Value = Eye_received
            });
            dashboard.EyeHealth = Eye;
            #endregion
            #region Food
            var Food_normal = food.Where(j => j.Status == nId).Count();
            var Food_observation = food.Where(j => j.Status == oId).Count();
            var Food_attention = food.Where(j => j.Status == aId).Count();
            var Food_received = food.Where(j => j.Status == rId).Count();
            TeleNormal += Food_normal;
            TeleObservation += Food_observation;
            TeleAttention += Food_attention;
            TeleReceived += Food_received;
            var Food = new List<Status>();
            Food.Add(new Status
            {
                Key = "Normal",
                Value = Food_normal
            });
            Food.Add(new Status
            {
                Key = "Under Observation",
                Value = Food_observation
            });
            Food.Add(new Status
            {
                Key = "Urgent Attention",
                Value = Food_attention
            });
            Food.Add(new Status
            {
                Key = "Response Received",
                Value = Food_received
            });
            dashboard.Food = Food;
            #endregion
            #region Heart
            var Heart_normal = heart.Where(j => j.Status == nId).Count();
            var Heart_observation = heart.Where(j => j.Status == oId).Count();
            var Heart_attention = heart.Where(j => j.Status == aId).Count();
            var Heart_received = heart.Where(j => j.Status == rId).Count();
            TeleNormal += Heart_normal;
            TeleObservation += Heart_observation;
            TeleAttention += Heart_attention;
            TeleReceived += Heart_received;
            var Heart = new List<Status>();
            Heart.Add(new Status
            {
                Key = "Normal",
                Value = Heart_normal
            });
            Heart.Add(new Status
            {
                Key = "Under Observation",
                Value = Heart_observation
            });
            Heart.Add(new Status
            {
                Key = "Urgent Attention",
                Value = Heart_attention
            });
            Heart.Add(new Status
            {
                Key = "Response Received",
                Value = Heart_received
            });
            dashboard.Heart = Heart;
            #endregion
            #region Oxygen
            var Oxygen_normal = oxygen.Where(j => j.Status == nId).Count();
            var Oxygen_observation = oxygen.Where(j => j.Status == oId).Count();
            var Oxygen_attention = oxygen.Where(j => j.Status == aId).Count();
            var Oxygen_received = oxygen.Where(j => j.Status == rId).Count();
            TeleNormal += Oxygen_normal;
            TeleObservation += Oxygen_observation;
            TeleAttention += Oxygen_attention;
            TeleReceived += Oxygen_received;
            var Oxygen = new List<Status>();
            Oxygen.Add(new Status
            {
                Key = "Normal",
                Value = Oxygen_normal
            });
            Oxygen.Add(new Status
            {
                Key = "Under Observation",
                Value = Oxygen_observation
            });
            Oxygen.Add(new Status
            {
                Key = "Urgent Attention",
                Value = Oxygen_attention
            });
            Oxygen.Add(new Status
            {
                Key = "Response Received",
                Value = Oxygen_received
            });
            dashboard.Oxygen = Oxygen;
            #endregion
            #region Pain
            var Pain_normal = pain.Where(j => j.Status == nId).Count();
            var Pain_observation = pain.Where(j => j.Status == oId).Count();
            var Pain_attention = pain.Where(j => j.Status == aId).Count();
            var Pain_received = pain.Where(j => j.Status == rId).Count();
            TeleNormal += Pain_normal;
            TeleObservation += Pain_observation;
            TeleAttention += Pain_attention;
            TeleReceived += Pain_received;
            var Pain = new List<Status>();
            Pain.Add(new Status
            {
                Key = "Normal",
                Value = Pain_normal
            });
            Pain.Add(new Status
            {
                Key = "Under Observation",
                Value = Pain_observation
            });
            Pain.Add(new Status
            {
                Key = "Urgent Attention",
                Value = Pain_attention
            });
            Pain.Add(new Status
            {
                Key = "Response Received",
                Value = Pain_received
            });
            dashboard.Pain = Pain;
            #endregion
            #region Pulse
            var Pulse_normal = pulse.Where(j => j.Status == nId).Count();
            var Pulse_observation = pulse.Where(j => j.Status == oId).Count();
            var Pulse_attention = pulse.Where(j => j.Status == aId).Count();
            var Pulse_received = pulse.Where(j => j.Status == rId).Count();
            TeleNormal += Pulse_normal;
            TeleObservation += Pulse_observation;
            TeleAttention += Pulse_attention;
            TeleReceived += Pulse_received;
            var Pulse = new List<Status>();
            Pulse.Add(new Status
            {
                Key = "Normal",
                Value = Pulse_normal
            });
            Pulse.Add(new Status
            {
                Key = "Under Observation",
                Value = Pulse_observation
            });
            Pulse.Add(new Status
            {
                Key = "Urgent Attention",
                Value = Pulse_attention
            });
            Pulse.Add(new Status
            {
                Key = "Response Received",
                Value = Pulse_received
            });
            dashboard.Pulse = Pulse;
            #endregion
            #region Seizure
            var Seizure_normal = seizure.Where(j => j.Status == nId).Count();
            var Seizure_observation = seizure.Where(j => j.Status == oId).Count();
            var Seizure_attention = seizure.Where(j => j.Status == aId).Count();
            var Seizure_received = seizure.Where(j => j.Status == rId).Count();
            TeleNormal += Seizure_normal;
            TeleObservation += Seizure_observation;
            TeleAttention += Seizure_attention;
            TeleReceived += Seizure_received;
            var Seizure = new List<Status>();
            Seizure.Add(new Status
            {
                Key = "Normal",
                Value = Seizure_normal
            });
            Seizure.Add(new Status
            {
                Key = "Under Observation",
                Value = Seizure_observation
            });
            Seizure.Add(new Status
            {
                Key = "Urgent Attention",
                Value = Seizure_attention
            });
            Seizure.Add(new Status
            {
                Key = "Response Received",
                Value = Seizure_received
            });
            dashboard.Seizure = Seizure;
            #endregion
            #region Wound
            var Wound_normal = wound.Where(j => j.Status == nId).Count();
            var Wound_observation = wound.Where(j => j.Status == oId).Count();
            var Wound_attention = wound.Where(j => j.Status == aId).Count();
            var Wound_received = wound.Where(j => j.Status == rId).Count();
            TeleNormal += Wound_normal;
            TeleObservation += Wound_observation;
            TeleAttention += Wound_attention;
            TeleReceived += Wound_received;
            var Wound = new List<Status>();
            Wound.Add(new Status
            {
                Key = "Normal",
                Value = Wound_normal
            });
            Wound.Add(new Status
            {
                Key = "Under Observation",
                Value = Wound_observation
            });
            Wound.Add(new Status
            {
                Key = "Urgent Attention",
                Value = Wound_attention
            });
            Wound.Add(new Status
            {
                Key = "Response Received",
                Value = Wound_received
            });
            dashboard.Wound = Wound;
            #endregion
            #region TeleHealth
            TeleHeath.Add(new Status
            {
                Key = "Normal",
                Value = TeleNormal
            });
            TeleHeath.Add(new Status
            {
                Key = "Under Observation",
                Value = TeleObservation
            });
            TeleHeath.Add(new Status
            {
                Key = "Urgent Attention",
                Value = TeleAttention
            });
            TeleHeath.Add(new Status
            {
                Key = "Response Received",
                Value = TeleReceived
            });
            dashboard.TeleHealth = TeleHeath;
            #endregion
            var types = new List<string> { "AM", "LUNCH", "TEA", "BED", "OTHERS1", "OTHERS2" };
            var months = new List<string>();
            var days = new List<string>();
            for (int day = 0; day < 7; day++)
            {
                days.Add(DateTime.Now.AddDays(-day).ToString("ddd"));
            }
            for (int month = 0; month < 12; month++)
            {
                months.Add(DateTime.Now.AddMonths(-month).ToString("MMM-yy"));
            }
            days.Reverse();
            months.Reverse();
            
            var startDateM = DateTime.Now.AddDays(-365).Date.ToString("yyyy-MM-dd");
            var startDateW = DateTime.Now.AddDays(-7).Date.ToString("yyyy-MM-dd");
            var rotaAdmins = await _rotaTaskService.LiveRota(startDateM, endDate);

            var taskitem = baserecordItem.Where(s => s.KeyName == "Task_Board_Status").FirstOrDefault();
            dashboard.GetAllStaff = getStaff;
            dashboard.GetTaskBoard = GetTaskBoard(task, taskitem);
            dashboard.ActiveUser = getClient.Where(s => s.Status == "Active").Count();
            dashboard.ApprovedStaff = getStaff.Where(s => s.Status == StaffRegistrationEnum.Approved).Count();
            dashboard.StaffRating = GetStaffRating(rating);
            dashboard.GetClients = GetClients(getClient);
            dashboard.OnCall = GetOnCall(oncall, getClient);
            dashboard.concernNotes = GetConcernNotes(concern);
            dashboard.GetStaffPersonalInfos = GetStaffs(getStaff);
            dashboard.LiveTrackerM = GetTrackerMonthly(startDateM, endDate, months, rotaAdmins);
            dashboard.LiveTrackerW = GetTrackerWeekly(startDateW, endDate, days, rotaAdmins);
            dashboard.LiveTrackerD = GetTrackerDaily(endDate, types, rotaAdmins);
            dashboard.Months = months.Distinct().ToList();
            dashboard.Days = days.Distinct().ToList();
            dashboard.Types = types.Distinct().ToList();
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
    }
}
