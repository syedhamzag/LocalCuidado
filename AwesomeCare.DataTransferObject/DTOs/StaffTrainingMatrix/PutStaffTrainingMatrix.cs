using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffTrainingMatrix
{
    public class PutStaffTrainingMatrix
    {
        public PutStaffTrainingMatrix()
        {
            PutTrainingMatrixList = new List<PutTrainingMatrixList>();
        }
        public int MatrixId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public List<PutTrainingMatrixList> PutTrainingMatrixList { get; set; }

    }
}
