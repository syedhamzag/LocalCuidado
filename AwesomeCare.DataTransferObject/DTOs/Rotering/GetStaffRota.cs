using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Rotering
{
   public class GetStaffRota
    {
        [JsonProperty("rotaType")]
        public string RotaType { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Task
    {
        [JsonProperty("rotaTaskId")]
        public int RotaTaskId { get; set; }

        [JsonProperty("taskName")]
        public string TaskName { get; set; }

        [JsonProperty("givenAcronym")]
        public string GivenAcronym { get; set; }

        [JsonProperty("notGivenAcronym")]
        public string NotGivenAcronym { get; set; }
    }

    public class StaffRotaPartner
    {
        [JsonProperty("partner")]
        public string Partner { get; set; }

        [JsonProperty("telephone")]
        public string Telephone { get; set; }
    }

    public class Item
    {
        [JsonProperty("areaCode")]
        public int AreaCode { get; set; }

        [JsonProperty("clientRotaId")]
        public int ClientRotaId { get; set; }

        [JsonProperty("clientId")]
        public int ClientId { get; set; }

        [JsonProperty("clientProviderReference")]
        public string ClientProviderReference { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("clientPostCode")]
        public string ClientPostCode { get; set; }

        [JsonProperty("rotaDate")]
        public DateTime RotaDate { get; set; }

        [JsonProperty("dayofWeek")]
        public string DayofWeek { get; set; }

        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("stopTime")]
        public string StopTime { get; set; }

        [JsonProperty("clockInTime")]
        public object ClockInTime { get; set; }

        [JsonProperty("clockOutTime")]
        public object ClockOutTime { get; set; }

        [JsonProperty("rota")]
        public string Rota { get; set; }

        [JsonProperty("staff")]
        public string Staff { get; set; }

        [JsonProperty("staffId")]
        public int StaffId { get; set; }
        
        [JsonProperty("remark")]
        public object Remark { get; set; }

        [JsonProperty("referenceNumber")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("clientKeySafe")]
        public string ClientKeySafe { get; set; }

        [JsonProperty("clientRate")]
        public decimal ClientRate { get; set; }

        [JsonProperty("clientTelephone")]
        public string ClientTelephone { get; set; }

        [JsonProperty("clockInMethod")]
        public object ClockInMethod { get; set; }

        [JsonProperty("clockOutMethod")]
        public object ClockOutMethod { get; set; }

        [JsonProperty("feedback")]
        public object Feedback { get; set; }

        [JsonProperty("handOver")]
        public object HandOver { get; set; }

        [JsonProperty("comment")]
        public object Comment { get; set; }

        [JsonProperty("clockInAddress")]
        public object ClockInAddress { get; set; }

        [JsonProperty("clockOutAddress")]
        public object ClockOutAddress { get; set; }

        [JsonProperty("numberOfStaff")]
        public int NumberOfStaff { get; set; }

        [JsonProperty("staffTelephone")]
        public string StaffTelephone { get; set; }

        [JsonProperty("staffRate")]
        public decimal? StaffRate { get; set; }

        [JsonProperty("clientRotaDaysId")]
        public int ClientRotaDaysId { get; set; }

        [JsonProperty("staffRotaId")]
        public int StaffRotaId { get; set; }

        [JsonProperty("staffRotaPeriodId")]
        public int StaffRotaPeriodId { get; set; }

        [JsonProperty("rotaId")]
        public int RotaId { get; set; }

        [JsonProperty("rotaDayOfWeekId")]
        public int? RotaDayOfWeekId { get; set; }

        [JsonProperty("tasks")]
        public List<Task> Tasks { get; set; }

        [JsonProperty("partners")]
        public List<StaffRotaPartner> Partners { get; set; }
    }

   


}
