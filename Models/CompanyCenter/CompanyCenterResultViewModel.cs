﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyCenter
{
    public class CompanyCenterResultViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("code")]
        public Guid Code { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("city_id")]
        public long? CityId { get; set; }

        [JsonProperty("city")]
        public virtual CenterCityViewModel City { get; set; }
        [JsonProperty("company")]
        public virtual CenterCompanyResultViewModel Company { get; set; }
        [JsonProperty("company_center_schedules")]
        public virtual List<CenterScheduleResultViewModel> CompanyCenterSchedules { get; set; }
    }
}
