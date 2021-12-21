using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffTrainingMatrix
{
    public class PostStaffTrainingMatrix
    {
        public PostStaffTrainingMatrix()
        {
            PostTrainingMatrixList = new List<PostTrainingMatrixList>();
        }
        public int MatrixId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        
        public List<PostTrainingMatrixList> PostTrainingMatrixList { get; set; }
    }
}
