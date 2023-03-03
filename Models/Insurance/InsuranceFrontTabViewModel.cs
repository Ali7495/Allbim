using Models.Company;
using System.ComponentModel.DataAnnotations;

namespace Models.Insurance
{
    public class InsuranceFrontTabViewModel
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
