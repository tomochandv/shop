using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_admin_api.Lib
{
	public class Authfilter : ActionFilterAttribute
	{
		private readonly IConfiguration _configuration;
		private readonly JwtToken _jwtToken;

		private const string headerKeyName = "Bear-x-i-token";
		public Authfilter(IConfiguration configuration, JwtToken jwtToken)
		{
			_configuration = configuration;
			_jwtToken = jwtToken;
		}

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			bool isOk = false;
			try
			{
				if (!context.HttpContext.Request.Headers.TryGetValue(headerKeyName, out var authText))
				{
					isOk = false;
				}

				if (authText.ToString() != "")
				{
					var jsonString = _jwtToken.Decoding();
					if (jsonString.Useridx != 0)
					{
						isOk = true;
					}
				}
			}
			catch (Exception)
			{
				isOk = false;
			}

			if (isOk == false)
			{
				context.Result = new ContentResult()
				{
					StatusCode = 401,
					Content = "Not Auth."
				};
				return;
			}
			await next();
		}
	}
}
