using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffTrainingMatrix
{
    public class GetStaffTrainingMatrix
    {
        public GetStaffTrainingMatrix()
        {
            GetTrainingMatrixList = new List<GetTrainingMatrixList>();
        }
        public int MatrixId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public List<GetTrainingMatrixList> GetTrainingMatrixList { get; set; }

    }
}
