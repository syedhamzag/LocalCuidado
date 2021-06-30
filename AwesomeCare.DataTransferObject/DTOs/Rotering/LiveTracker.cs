﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Rotering
{
    public class LiveTracker
    {
        public LiveTracker()
        {
            TaskPerformed = new List<string>();
        }
        public int ClientRotaId { get; set; }
        public int StaffRotaId { get; set; }
        public int StaffRotaPeriodId { get; set; }
        public int ClientRotaDaysId { get; set; }
        public int ClientId { get; set; }
        public string Period { get; set; }
        public string ClientName { get; set; }
        public string ClientKeySafe { get; set; }
        public string ClientPostCode { get; set; }
        public string ClientTelephone { get; set; }
        public string ClientProviderReference { get; set; }
        public decimal ClientRate { get; set; }
        public int NumberOfStaff { get; set; }
        public int AreaCode { get; set; }

        public string DayofWeek { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public DateTimeOffset? ClockInTime { get; set; }
        public DateTimeOffset? ClockOutTime { get; set; }
        public string ClockInMethod { get; set; }
        public string ClockOutMethod { get; set; }
        public string Feedback { get; set; }
        public string Rota { get; set; }
        public string Staff { get; set; }
        public string StaffTelephone { get; set; }
        public decimal? StaffRate { get; set; }
        public DateTime RotaDate { get; set; }
        public string Remark { get; set; }
        public string ReferenceNumber { get; set; }
        public List<string> TaskPerformed { get; set; }
        public string ClockInAddress { get; set; }
        public string ClockOutAddress { get; set; }
        public string Comment { get; set; }
        public string HandOver { get; set; }

        public string RowClass()
        {
            try
            {
                string rowClass = "";
                if (ClockInTime.HasValue)
                {
                    var st = DateTime.TryParseExact(StartTime, "h:mm tt", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime d) ? d : default(DateTime);
                    var df = st.Subtract(ClockInTime.Value.DateTime).TotalMinutes;

                    if (df <= 15 && df >= -15)
                    {
                        rowClass = "success";
                    }
                    else if (df > 15 && df <= 30)
                    {
                        // "blue";
                       
                    }
                    else if (df >= -30)
                    {
                        // "yellow";
                    }
                    else
                    {
                        rowClass = "danger";
                    }


                }

                return rowClass;
            }
            catch (Exception ex)
            {
                return "";

            }
        }
    }
}
