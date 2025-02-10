using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data_TransferModels.ReportDTO
{
    public class PatientCountDto
    {
        public int TotalPatients { get; set; }
        public int ActivePatients { get; set; }
        public int DeletedPatients { get; set; }
    }

}
