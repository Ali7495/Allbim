using Models.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.InsuranceStep
{
    public class InsuranceStepViewModel
    {
        public long Id { get; set; }
        public long? InsuranceId { get; set; }
        public string StepName { get; set; }

        public virtual InsuranceViewModel InsuranceViewModel { get; set; }
    }
}
