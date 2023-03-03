using albim.Result;
using Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using Models.Cache;
using Models.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Redis;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Albim.ActionFilters
{
    public class RedisCachableAttribute : ActionFilterAttribute, IScopedInjectable
    {
        private int _validHours = 24;
        private bool _includeUserId = false;
        private bool _shouldCache;
        private readonly IRedisService _redisService;

        public RedisCachableAttribute(int validHours, bool includeUserId, IRedisService redisService)
        {
            _validHours = validHours;
            _includeUserId = includeUserId;
            _shouldCache = false;
            _redisService = redisService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cacheKey = GenerateCacheKey(context);
            var cachedResult = _redisService.GetValueAsync<ApiResult<object>>(cacheKey.ToJson()).Result;

            if (cachedResult == null)
                _shouldCache = true;
            else
            {
                cachedResult.IsCached = true;
                context.Result = new OkObjectResult(cachedResult);
            }

            base.OnActionExecuting(context);
        }

        public async override void OnActionExecuted(ActionExecutedContext context)
        {
            if (_shouldCache)
            {
                ApiResult<object> result = (ApiResult<object>)((dynamic)context.Result).Value;
                result.Data = (object)(((dynamic)result.Data).Data);

                var cacheKey = GenerateCacheKey(context);

                await _redisService.SetValueAsync(cacheKey.ToJson(), result, TimeSpan.FromHours(_validHours));
            }

            base.OnActionExecuted(context);
        }


        private CacheKey GenerateCacheKey(FilterContext context)
        {
            CacheKey cacheKey = new()
            {
                Controller = context.RouteData.Values.FirstOrDefault(z => z.Key == "controller").Value.ToString(),
                Action = context.RouteData.Values.FirstOrDefault(z => z.Key == "action").Value.ToString(),
                Parameters = context.RouteData.Values.Where(z => z.Key != "action" && z.Key != "controller").OrderBy(z => z.Key).ToDictionary(z => z.Key, z => z.Value.ToString()),
            };

            if (_includeUserId)
            {
                var userToken = context.HttpContext.Request.Headers[HeaderNames.Authorization];
                if (!string.IsNullOrEmpty(userToken))
                {
                    var userClaims = (new JwtSecurityTokenHandler().ReadToken(userToken) as JwtSecurityToken).Claims;
                    var userId = userClaims.FirstOrDefault(z => z.Type == ClaimTypes.NameIdentifier).Value;

                    cacheKey.UserId = userId;
                }
            }

            return cacheKey;
        }
    }
}
