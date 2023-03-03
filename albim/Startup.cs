using albim.Versioning;
using AutoMapper;
using Common.Extensions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.DependencyInjection;
using Models.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Services;
using Services.City;
using Services.Policy;
using Services.PolicyRequest;
using Services.Redis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using albim.Configuration;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Services.Damage;
using Services.Product;
using DAL.Contracts.EnumRepositories;
using DAL.Repositories.EnumRepositories;
using DAL.Contracts.EnumIRepositories;
using Services.SmsService;
using Services.PolicyRequestStatus;
using Services.Agent;
using Services.CompanyCenter;
using Services.InsurerServices;
using FluentValidation.AspNetCore;
using FluentValidation;
using Models.Floent;
using Microsoft.Extensions.FileProviders;
using System.IO;
using albim.Result;
using Common;
using Logging.LogModels;

namespace albim
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private readonly SiteSettings _siteSetting;
        private readonly PagingSettings _pagingSettings;

        /// <summary>
        /// 
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
            _pagingSettings = configuration.GetSection(nameof(PagingSettings)).Get<PagingSettings>();
            
        }


        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        /// <summary>
        /// 
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // services.AddControllers().AddFluentValidation(s =>
            // {
            //     s.RegisterValidatorsFromAssemblyContaining<Startup>();
            //     s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            // });
            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));

                var jsonInputFormatter = options.InputFormatters
                    .OfType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>()
                    .Single();
                jsonInputFormatter.SupportedMediaTypes.Add("multipart/form-data");
            }).AddNewtonsoftJson(z =>
            {
                z.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                z.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<FlotentTestClsValidator>());
            //
            // services.AddTransient<IValidator<FlotentTestCls>, FlotentTestClsValidator>();
            // services.RegisterScoped<IScopedInjectable>(typeof(IScopedInjectable).Assembly);

            #region Dbupdate

            AllbimDBUp.Program.Main(new string[] { Configuration.GetConnectionString("Dev"),Configuration.GetConnectionString("Log") });

            #endregion

            #region Localization

            services.AddLocalization(options => options.ResourcesPath = "");
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("fa-IR"),
                        new CultureInfo("en-US"),
                       
                    };
                    options.DefaultRequestCulture = new RequestCulture(culture: "fa-IR", uiCulture: "fa-IR");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    // options.RequestCultureProviders = new[] {new RouteDataRequestCultureProvider {IndexOfCulture: 1, IndexOfUICulture = 1}};
                }
            );

            #endregion


            #region SiteSettings

            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            #endregion

            #region PagingSettings

            services.Configure<PagingSettings>(Configuration.GetSection(nameof(PagingSettings)));

            #endregion

            #region API Versioning

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader =
                    new HeaderApiVersionReader("X-API-Version");
            });

            #endregion

            #region Connection String

            //services.AddDbContext<AlbimDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            services.AddDbContextPool<AlbimDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("Dev")));
            services.AddDbContextPool<AlbimLogDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("Log")));

            // services.AddDbContext<AlbimDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("Dev")), ServiceLifetime.Transient);
            // services.AddDbContext<AlbimLogDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("Log")), ServiceLifetime.Transient);


            #endregion

            #region Enable Cors

            services.AddCors();

            #endregion

            #region Swagger

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = " Clean Architecture v1 API's",
                    Description =
                        $"Clean Architecture API's for integration with UI \r\n\r\n � Copyright {DateTime.Now.Year} JK. All rights reserved."
                });
                swagger.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "Clean Architecture v2 API's",
                    Description =
                        $"Clean Architecture API's for integration with UI \r\n\r\n � Copyright {DateTime.Now.Year} JK. All rights reserved."
                });
                swagger.ResolveConflictingActions(a => a.First());
                swagger.OperationFilter<RemoveVersionFromParameterv>();
                swagger.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                #region Enable Authorization using Swagger (JWT)

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                #endregion
            });

            #endregion

            #region Swagger Json property Support

            services.AddSwaggerGenNewtonsoftSupport();

            #endregion

            #region JWT

            // Adding Authentication    
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })

                // Adding Jwt Bearer    
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = _siteSetting.Jwt.Issuer,
                        ValidIssuer = _siteSetting.Jwt.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_siteSetting.Jwt.Key))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            // Call this to skip the default logic and avoid using the default response
                            context.HandleResponse();

                            // Write to the response in any way you wish
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            // context.Response.Headers.Append("my-custom-header", "custom-value");
                            DefaultContractResolver contractResolver = new DefaultContractResolver
                            {
                                NamingStrategy = new SnakeCaseNamingStrategy()
                            };
                            var apiStatusCode = ApiResultStatusCode.UnAuthorized;
                            var result = new ApiResult(false, apiStatusCode, message: apiStatusCode.ToDisplay());
                            var json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                            {
                                ContractResolver = contractResolver,
                                Formatting = Formatting.Indented
                            });
                            await context.Response.WriteAsync(json);
                        }
                    };
                });

            #endregion

            #region Dependency Injection

         

            #endregion

            #region Automapper

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #endregion

            services.AddIocConfig();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        /// <summary>
        /// 
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            // DbUpdate.Program.Main(new string[] { configuration.ConnectionStrings["dbConStr"].ConnectionString });
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            app.UseDeveloperExceptionPage();
            #region Global Cors Policy

            app.UseCors(x => x
                // .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin  
                .AllowCredentials()); // allow credentials  

            #endregion
            app.UseCustomExceptionHandler();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var prefix = _siteSetting.PrefixUrl;
                c.SwaggerEndpoint(prefix + "/swagger/v1/swagger.json", "API v1");
                c.SwaggerEndpoint(prefix + "/swagger/v2/swagger.json", "API v2");
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            //Add our new middleware to the pipeline
            // app.UseMiddleware<LogMiddleware>();
            // app.UseMiddleware<Albim.Middlewares.UnauthorizedMiddleware>();

            app.UseAuthorization();

        
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            // app.Run(async (context) =>
            // {
            //     string myValue = configuration["AppConfig"];
            //     await context.Response.WriteAsync(myValue);
            // });
            var localization = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localization.Value);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Public")),
                RequestPath = "/Public"
            });

            const string cacheMaxAge = "604800";
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    // using Microsoft.AspNetCore.Http;
                    ctx.Context.Response.Headers.Append(
                         "Cache-Control", $"public, max-age={cacheMaxAge}");
                }
            });
            
            
        }
    }
}