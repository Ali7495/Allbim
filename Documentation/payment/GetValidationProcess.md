<div dir="rtl" align="right">

# روند validation و سرویس های Get



  </div>

<div dir="rtl" align="right">

به مثال زیر دقت کنید:

  </div>

```c#
public async Task<PagedResult<CompanyPolicyRequestFactorResultViewModel>> GetAllPolicyFactorsMine(long userId, Guid policyCode, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شما با این شرکت ارتباطی ندارید");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            PagedResult<PolicyRequestFactor> factors = await _policyRequestFactorRepository.GetAllPolicyFactorsOfCompany(company.Id, policyRequest.Id, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<CompanyPolicyRequestFactorResultViewModel>>(factors);
        }

```

<div dir="rtl" align="right">

این نمونه ای از دریافت لیستی از یک مدل است. در تمامی سرویس ها (به جز سرویس های Delete) خروجی، ویومدلی از مدل اصلی آن جدول است.

در این مثال که نام سرویس با کلمه کلیدی **mine** مشخص شده، ابتدا با دریافت **user** توسط userId و ارسال personId از این مدل جهت دریافت اطلاعات **personCompany **اقدام می شود. سپس بررسی می گردد اگر این کلاس null باشد آنگاه به این معنی است که دسترسی به هیچ شرکتی ندارد. درغیر این صورت با دریافت **Company** توسط companyId از کلاس **personCompany** مجدد وجود چنین اطلاعاتی بررسی می گردد که مشخص شود کاربر با آن شرکت ارتباط دارد.

بعد از برقرار بودن این شروط با ارسال companyId و policyCode به متد `GetMyCompanyPolicyRequestCommon()` وجود درخواست بیمه مورد نظر و ارتباط آن با شرکت بررسی می گردد. که بدنه این متد به شرح زیر است:

  </div>

```c#
 public async Task<DAL.Models.PolicyRequest> GetMyCompanyPolicyRequestCommon(long companyId, Guid policyCode, CancellationToken cancellationToken)
        {
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCodeNoTracking(policyCode, cancellationToken);
            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }

            if (policyRequest.Insurer.CompanyId != companyId)
            {
                throw new BadRequestException("درخواست متعلق به این شرکت نمی باشد");
            }

            return policyRequest;
        }
```
<div dir="rtl" align="right">

نهایتا با برقراری تمام این شرایط از طریق شی ریپازیتوری مدل مربوطه متد مناسب جهت دریافت اطلاعات صدا زده می شود و با استفاده از **AutoMapper** به ویومدل مپ می گردد.

> لازم به ذکر است تنها تفاوت سرویس های mine با سرویس های {code} در همین بررسی وجود و دسترسی کاربر می باشد