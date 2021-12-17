using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.StaffTrainingMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class CreateStaffTrainingMatrix
    {
        public CreateStaffTrainingMatrix()
        {
            baseRecordList = new List<GetBaseRecordItem>();
        }
        public int MatrixId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public int ListCount { get; set; }
        public string StaffName { get; set; }
        public List<GetTrainingMatrixList> GetTrainingMatrixList { get; set; }
        public List<GetBaseRecordItem> baseRecordList { get; set; }
    }
}
