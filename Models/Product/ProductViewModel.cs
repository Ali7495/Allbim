using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Product
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            ThirdInsuranceCreditDurations = new List<ThirdFactorViewModel>();
            ThirdMaxFinancialCovers = new List<ThirdFactorViewModel>();
        }
        public string Title { get; set; }
        public decimal Price { get; set; } = 0;
        public decimal BacePrice { get; set; } = 0;

        // این فیلد جهت دریافت قیمت خودرو
        //public decimal Value { get; set; } = 0;
        public decimal PriceWithDiscount { get; set; }
        public int GroupDiscount { get; set; } = 0;
        public int CashDiscount { get; set; } = 0;
        public int NoDamageDiscount { get; set; } = 0;
        public int InsuranceDiscount { get; set; } = 0;
        public int DriverDiscount { get; set; } = 0;
        public int BranchNumber { get; set; }
        public int WealthLevel { get; set; }
        public int DamagePaymentSatisfactionRating { get; set; }
        public string LogoUrl { get; set; }


        public List<ThirdFactorViewModel> ThirdMaxFinancialCovers { get; set; }
        public List<ThirdFactorViewModel> ThirdInsuranceCreditDurations { get; set; }
    }
}
