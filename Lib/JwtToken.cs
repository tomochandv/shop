using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using shop_model;
using System;

namespace shop_admin_api.Lib
{
	public class JwtToken
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _acc;

		public JwtToken(IConfiguration configuration, IHttpContextAccessor acc)
		{
			_configuration = configuration;
			_acc = acc;
		}
		/// <summary>
		///  회원정보 토큰 생성
		/// </summary>
		/// <param name="userInfo"></param>
		/// <returns></returns>
		public string CreateToken(UserInfo userInfo)
		{
			try
			{
				var data = new UserInfo()
				{
					Useridx = userInfo.Useridx,
					Email = userInfo.Email,
					Status = userInfo.Status,
					Type = userInfo.Type,
					Name = userInfo.Name,
					RegionIdx = userInfo.RegionIdx
				};
				var token = new JwtBuilder()
					.WithAlgorithm(new HMACSHA256Algorithm())
					.WithSecret(_configuration["Jwt"])
					.AddClaim("exp", DateTimeOffset.UtcNow.AddHours(12).ToUnixTimeSeconds())
					.AddClaim("data", data)
					.Encode();
				return token;
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// 회원정보 디코딩
		/// </summary>
		/// <returns></returns>
		public UserInfo Decoding()
		{
			string encodeString = string.Empty;
			try
			{
				var token = _acc.HttpContext.Request.Headers["Bear-x-i-token"];
				var json = new JwtBuilder()
					 .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
					 .WithSecret(_configuration["Jwt"])
					 .MustVerifySignature()
					 .Decode(token);
				encodeString = json;
				JObject jsonData = JObject.Parse(json);
				var data = jsonData["data"].ToObject<UserInfo>();
				return data;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
