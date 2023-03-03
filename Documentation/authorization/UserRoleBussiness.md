<div align="right" dir="rtl">

عملیات CRUD جدول Role بصورت زیر پیاده سازی شده. این عملیات ها در کنترلر Role قرار دارند .

>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*

</div>

```C#

        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<ApiResult<List<RoleResultViewModel>>> GetRoleList( CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            long roleId = long.Parse(userRole);
            var role = await _roleService.GetListByUserId(userId,roleId, cancellationToken);

            return role;
        }
        
        [AllowAnonymous]
        [HttpGet("{roleId}")]
        public async Task<ApiResult<RoleResultViewModel>> GetRoles(long id, CancellationToken cancellationToken)
        {
            var role = await _roleService.GetRole(id, cancellationToken);

            return role;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ApiResult<RoleResultViewModel>> CreateRole(RoleInputViewModel roleViewModel, CancellationToken cancellationToken)
        {
            var role = await _roleService.Create(roleViewModel, cancellationToken);

            return role;
        }
        

        [AllowAnonymous]
        [HttpPut("")]
        public async Task<ApiResult<RoleResultViewModel>> UpdateRole(long id, RoleInputViewModel roleViewModel, CancellationToken cancellationToken)
        {
            var role = await _roleService.Update(id, roleViewModel, cancellationToken);

            return role;
        }

        [AllowAnonymous]
        [HttpDelete("{roleId}")]
        public async Task<ApiResult<string>> DeleteRole(long id, CancellationToken cancellationToken)
        {
            bool result = await _roleService.Delete(id, cancellationToken);

            return result.ToString();
        }



```

<div align="right" dir="rtl">

**درج (سرویس Post)** : این سرویس متد `Create(roleViewModel, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

        public async Task<RoleResultViewModel> Create(RoleInputViewModel roleViewModel, CancellationToken cancellationToken)
        {
            Role role = new Role()
            {
                Name = roleViewModel.Name
            };

            await _repository.AddAsync(role,cancellationToken);
            
            return _mapper.Map<RoleResultViewModel>(role);
        }

```


<div align="right" dir="rtl">


ویومدل ورودی این سرویس : 

</div>

```C#

 public class RoleInputViewModel
    {
    
        public string Name { get; set; }
        public string Caption { get; set; }

        public virtual ICollection<RolePermissionResultViewModel> RolePermissions { get; set; }
    }

```



<div align="right" dir="rtl">

**ویرایش (سرویس Put)** : این سرویس متد `Update(id, roleViewModel, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

  public async Task<RoleResultViewModel> Update(long id, RoleInputViewModel roleViewModel, CancellationToken cancellationToken)
        {
            Role role = await _repository.GetByIdAsync(cancellationToken, id);

            if (role == null)
                throw new BadRequestException("سمت");

            role.Name = roleViewModel.Name;

            await _repository.UpdateAsync(role, cancellationToken);

            return _mapper.Map<RoleResultViewModel>(role);
        }



```

<div align="right" dir="rtl">

<br>

**دریافت تکی (سرویس Get)** : این سرویس متد `GetRole(id, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

        public async Task<bool> UpdatePermissions(long roleId, long[] permissionIds, CancellationToken cancellationToken)
        {
            Role role = await _roleRepository.GetRole(roleId, cancellationToken);

            if (role == null)
                throw new BadRequestException("سمت");

            List<RolePermission> permissions = role.RolePermissions.Where(r => permissionIds.Contains(r.PermissionId)).ToList();

            role.RolePermissions = permissions;

            await _repository.UpdateAsync(role, cancellationToken);

            return true;
        }

```


<div align="right" dir="rtl">

**دریافت لیستی (سرویس Get)** : این سرویس متد `GetListByUserId(userId,roleId, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

public async Task<List<RoleResultViewModel>> GetListByUserId(long userId,long roleId, CancellationToken cancellation)
        {
            //todo: حتما باید براساس نقش کاربر فراخوانی کننده، نقش ها فیلتر شود فقط زیرمجموعه خودش را ببیند
            List<Role> roles = new List<Role>();
            // اگر ادمین بود
            if (roleId == 3)
            {
                roles = await _roleRepository.GetAllAsync(cancellation);
            }
            else
            {
                roles = await _roleRepository.GetByParentId(roleId,cancellation);
            }
           

            return _mapper.Map<List<RoleResultViewModel>>(roles);
        }

```




```

