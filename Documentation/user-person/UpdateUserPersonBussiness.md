<div align="right" dir="rtl" >

سرویس های مربوط به ویرایش اطلاعات شخص در کنترلر Person قرار دارند و به شرح زیر هستند:

</div>


```C#

        [HttpPut("MyInfo")]
        public async Task<ApiResult<MyPersonViewModel>> UpdatePersondata(MyPersonUpdateViewModel viewModel,CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            var person = await _personService.UpdatePersondata(userId,viewModel, cancellationToken);
            return person;
        }
        [HttpGet("MyInfo")]
        public async Task<ApiResult<MyPersonViewModel>> GetPersondata( CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            var person = await _personService.GetPersondata(userId, cancellationToken);
            return person;
        }


```

<br>

<div align="right" dir="rtl" >

سرویس `UpdatePersondata(userId,viewModel, cancellationToken)` ابتدا توسط userId کاربر استفاده کننده از سیستم بررسی می کند چنین کاربری وجود دارد یا خیر.

</div>


```C#

        public async Task<MyPersonViewModel> UpdatePersondataMine(long userId, MyPersonUpdateViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
            if (user == null)
            {
                throw new NotFoundException(" کاربر وجود ندارد");
            }

            var person = await _personRepository.GetByPersonIdwithPrimaryAddressAsync(user.PersonId, cancellationToken);
            if (person == null)
            {
                throw new NotFoundException(" اطلاعات وجود ندارد");
            }

            DAL.Models.Person Result = await UpdatePersondataCommon(person, viewModel, cancellationToken);
            return _mapper.Map<MyPersonViewModel>(Result);
        }






        public async Task<Person> UpdatePersondataCommon(Person person, MyPersonUpdateViewModel viewModel,
            CancellationToken cancellationToken)
        {
            PersonAddress personPrimaryAddress = person.PersonAddresses.Where(x => x.AddressTypeId == 1).FirstOrDefault();
            if (personPrimaryAddress != null)
            {
                personPrimaryAddress.Address.CityId = viewModel.CityId;
                personPrimaryAddress.Address.Description = viewModel.Address;
                await _addressRepository.UpdateAsync(personPrimaryAddress.Address, cancellationToken);
            }
            else
            {
                Address newAddress = new Address()
                {
                    CityId = viewModel.CityId,
                    Description = viewModel.Address,
                };

                await _addressRepository.AddAsync(newAddress, cancellationToken);
                PersonAddress personAddress = new PersonAddress()
                {
                    AddressId = newAddress.Id,
                    CreatedAt = DateTime.Now,
                    AddressTypeId = 1,
                };
                person.PersonAddresses.Add(personAddress);
            }

            person.FirstName = viewModel.FirstName;
            person.LastName = viewModel.LastName;
            person.BirthDate = viewModel.BirthDate;
            person.JobName = viewModel.JobName;
            person.NationalCode = viewModel.NationalCode;
            await _personRepository.UpdateAsync(person, cancellationToken);
            return person;
        }





```

<br>

<div align="right" dir="rtl" >

ویومدل ورودی نیز به ای صورت است

</div>


```C#

    public class MyPersonUpdateViewModel
    {
    
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        public string Address { get; set; }
        [Required]
        public string NationalCode { get; set; }
        public string JobName { get; set; }
        [Required]
        public long CityId { get; set; }
     
        public string Email { get; set; }

    }

```


<div align="right" dir="rtl" >

سرویس `GetPersondata(userId, cancellationToken)` ابتدا توسط userId کاربر استفاده کننده از سیستم بررسی می کند چنین کاربری وجود دارد یا خیر.

</div>

```C#

        public async Task<MyPersonViewModel> GetPersondataMine(long userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
            if (user == null)
            {
                throw new NotFoundException(" کاربر وجود ندارد");
            }

            //var person = await _personRepository.GetByPersonIdwithPrimaryAddressAsync(user.PersonId, cancellationToken);
            var person = await GetPersondata_Core(user.PersonId, cancellationToken);
            return _mapper.Map<MyPersonViewModel>(person);
        }




        public async Task<Person> GetPersondata_Core(long PersonId, CancellationToken cancellationToken)
        {
            return await _personRepository.GetByPersonIdwithPrimaryAddressAsync(PersonId, cancellationToken);
        }

```