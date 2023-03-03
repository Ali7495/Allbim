using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Enums
{

    public enum FinancialDamageStatus
    {
        [Display(Name = "فاقد خسارت مالی")]
        DamageNone = 0,
        [Display(Name = "یک بار خسارت مالی")]
        DamageOnce = 1,
        [Display(Name = "دوبار خسارت مالی")]
        DamageTwice = 2,
        [Display(Name = "سه بار خسارت مالی و بیشتر")]
        MultipleDamages = 3
    }

    public enum DriverDamageStatus
    {
        [Display(Name = "فاقد خسارت حوادث راننده")]
        DamageNone = 0,
        [Display(Name = "یک بار خسارت حوادث راننده")]
        DamageOnce = 1,
        [Display(Name = "دوبار خسارت حوادث راننده")]
        DamageTwice = 2,
        [Display(Name = "سه بار خسارت حوادث راننده و بیشتر")]
        MultipleDamages = 3
    }

    public enum DamageToLifeStatus
    {
        [Display(Name = "فاقد خسارت جانی")]
        DamageNone = 0,
        [Display(Name = "یک بار خسارت جانی")]
        DamageOnce = 1,
        [Display(Name = "دوبار خسارت جانی")]
        DamageTwice = 2,
        [Display(Name = "سه بار خسارت جانی و بیشتر")]
        MultipleDamages = 3
    }

    public enum DriverDiscountOnInsurence
    {
        Zero = 0,
        Five = 5,
        Ten = 10,
        Fifteen = 15,
        Twenty = 20,
        TwentyFive = 25,
        Thirty = 30,
        ThirtyFive = 35,
        Fourty = 40,
        FourtyFive = 45,
        Fifty = 50,
        FiftyFive = 55,
        Sixty = 60,
        SixtyFive = 65,
        Seventy = 70,
    }

    public enum ThirdDiscountOnInsurance
    {
        [Display(Name = "صفر")]
        Zero = 0,
        Five = 5,
        Ten = 10,
        Fifteen = 15,
        Twenty = 20,
        TwentyFive = 25,
        Thirty = 30,
        ThirtyFive = 35,
        Fourty = 40,
        FourtyFive = 45,
        Fifty = 50,
        FiftyFive = 55,
        Sixty = 60,
        SixtyFive = 65,
        Seventy = 70,
    }
}
