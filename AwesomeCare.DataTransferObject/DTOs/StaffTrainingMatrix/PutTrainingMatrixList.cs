﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffTrainingMatrix
{
    public class PutTrainingMatrixList
    {
        public int TrainingId { get; set; }
        public int MatrixId { get; set; }
        public DateTime Date { get; set; }
    }
}
