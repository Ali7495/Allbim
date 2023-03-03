using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PolicyRequestInspection
    {
        public long Id { get; set; }
        public long PolicyRequestId { get; set; }
        public byte? InspectionTypeId { get; set; }
        public long? InspectionAddressId { get; set; }
        public DateTime? InspectionSessionDate { get; set; }
        public long? CompanyCenterScheduleId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public long? InspectionSessionId { get; set; }

        public virtual CompanyCenterSchedule CompanyCenterSchedule { get; set; }
        public virtual Address InspectionAddress { get; set; }
        public virtual InspectionSession InspectionSession { get; set; }
        public virtual PolicyRequest PolicyRequest { get; set; }
    }
}
