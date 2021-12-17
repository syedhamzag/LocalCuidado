using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffTrainingMatrix
    {
        public StaffTrainingMatrix()
        {
            StaffTrainingMatrixList = new HashSet<StaffTrainingMatrixList>();
        }
        public int MatrixId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public ICollection<StaffTrainingMatrixList> StaffTrainingMatrixList { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }

    }
}
