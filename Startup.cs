using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using shop_admin_api.Lib;
using shop_admin_api.swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace shop_admin_api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shop Admin Api", Version = "v1" });

				var requiredAuthToken = new OpenApiSecurityScheme
				{
					Description = "Login Token",
					Name = "Bear-x-i-token",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
				};

				c.AddSecurityDefinition("Bear-x-i-token", requiredAuthToken);

				c.AddSecurityRequirement(new OpenApiSecurityRequirement()
				 {
					{
						new OpenApiSecurityScheme
						{
								Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bear-x-i-token"
								},
								Scheme = "oauth2",
								Name = "Bear-x-i-token",
								In = ParameterLocation.Header,

						},
						new List<string>()
					}
				 });

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
				c.SchemaFilter<SwaggerExcludeFilter>();
			});

			var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
			services.AddDbContext<ShopDbContext>(options =>
					 options.UseMySql(Configuration.GetConnectionString("ShopContext"), serverVersion));

			// 미들웨어
			services.AddScoped<Authfilter>();

			// 의존성 주입
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddSingleton<Ses>();
			services.AddSingleton<S3>();
			services.AddSingleton<JwtToken>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (!env.IsProduction())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "shop_admin_api v1"));
			}

			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});


			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
