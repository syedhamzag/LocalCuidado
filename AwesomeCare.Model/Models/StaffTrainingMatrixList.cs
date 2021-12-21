using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffTrainingMatrixList
    {
        public int TrainingId { get; set; }
        public int MatrixId { get; set; }
        public DateTime Date { get; set; }

        public virtual StaffTrainingMatrix StaffTrainingMatrix { get; set; }
    }
}
