using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop_admin_api.Lib;
using shop_admin_api.Model.Request;
using shop_model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_admin_api.Controllers
{
	[Produces("application/json")]
	[Route("/[controller]")]
	[ApiController]
	public class UserInfoController : ControllerBase
	{
		private readonly ShopDbContext _context;
		private readonly Ses _ses;
		private readonly JwtToken _jwtToken;

		public UserInfoController(ShopDbContext context, Ses ses, JwtToken jwtToken)
		{
			_context = context;
			_ses = ses;
			_jwtToken = jwtToken;
		}

		/// <summary>
		/// 회원리스트
		/// </summary>
		/// <param name="page">페이지</param>
		/// <param name="rows">행수</param>
		/// <param name="email">이메일</param>
		/// <param name="name">이름</param>
		/// <param name="type">99는 검색안함</param>
		/// <param name="status">99는 검색안합</param>
		/// <returns></returns>
		//[ServiceFilter(typeof(Authfilter))]
		[HttpGet]
		#region public async Task<Payload> GetUsers(int page = 1, int rows = 10, string email = "", string name = "", int type = 99, int status = 99)
		public async Task<Payload> GetUsers(int page = 1, int rows = 10, string email = "", string name = "", int type = 99, int status = 99)
		{
			Payload payload = new Payload();
			try
			{
				var start = (page - 1) * rows;

				var list = from user in _context.Users
									 join region in _context.Regions
									 on user.RegionIdx equals region.RegionIdx
									 where (user.Email == null || user.Email.Contains(email)) &&
											(user.Name == null || user.Name.Contains(name))
									 select new { user.Useridx, user.RegionIdx, user.Name, user.Type, user.Status, user.Regdate, user.Email, region.RegionCode, region.RegionName };

				if (type != 99)
				{
					list = list.Where(u => u.Type == (UserType)type);
				}
				if (status != 99)
				{
					list = list.Where(u => u.Status == (Status)status);
				}
				list = list.OrderBy(x => x.Name);

				var total = await list.CountAsync();
				var data = from p in list.Skip(start).Take(rows)
									 select p;

				payload.Status = true;
				payload.Data = new Dictionary<string, object>
				{
					{ "list",  await data.ToListAsync() },
					{ "total", total }
				};
			}
			catch (Exception ex)
			{
				payload.Status = false;
				payload.Error = ex.Message;
			}
			return payload;
		}
		#endregion

		/// <summary>
		/// 사용자 상세
		/// </summary>
		/// <param name="id">회원아이디</param>
		/// <returns></returns>
		//[ServiceFilter(typeof(Authfilter))]
		[HttpGet("{id}")]
		#region public async Task<Payload> GetUserInfo(int id)
		public async Task<Payload> GetUserInfo(int id)
		{
			Payload payload = new Payload();
			try
			{
				var userInfo = await _context.Users.FindAsync(id);
				payload.Data = userInfo;
				payload.Status = true;
			}
			catch (Exception ex)
			{
				payload.Error = ex.Message;
				payload.Status = false;
			}
			return payload;
		}
		#endregion

		/// <summary>
		/// 회원정보 수정. 이름, 상태, 타입, 국가 만 변경 가능
		/// </summary>
		/// <param name="userInfo"></param>
		/// <returns></returns>
		////[ServiceFilter(typeof(Authfilter))]
		[HttpPut("Edit")]
		#region public async Task<Payload> PutUserInfo(UserInfo userInfo)
		public async Task<Payload> PutUserInfo(UserInfo userInfo)
		{
			Payload payload = new Payload();
			try
			{
				var info = await _context.Users.FindAsync(userInfo.Useridx);
				info.Type = userInfo.Type;
				info.Status = userInfo.Status;
				info.Name = userInfo.Name;
				info.RegionIdx = userInfo.RegionIdx;

				_context.Entry(info).State = EntityState.Modified;
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				payload.Error = ex.Message;
				payload.Status = false;
			}
			return payload;
		}
		#endregion

		/// <summary>
		/// 사용자 추가. 이메일, 국가, 이름, 타입만 저장
		/// </summary>
		/// <param name="userInfo"></param>
		/// <returns></returns>
		////[ServiceFilter(typeof(Authfilter))]
		[HttpPost("Add")]
		#region public async Task<Payload> PostUserInfo(UserInfo userInfo)
		public async Task<Payload> PostUserInfo(UserInfo userInfo)
		{
			Payload payload = new Payload();
			try
			{
				var info = await _context.Users.FirstOrDefaultAsync(i => i.Email == userInfo.Email);
				if (info != null)
				{
					payload.Status = false;
					payload.Data = "존재하는 이메일 입니다.";
				}
				else
				{
					// 임시 비번 생성
					Random rand = new Random();
					string input = "0123456789";
					var chars = Enumerable.Range(0, 8).Select(x => input[rand.Next(0, input.Length)]);
					var tmpPassword = new string(chars.ToArray());

					var hash = new PasswordHasher<string>();
					userInfo.Password = hash.HashPassword(null, tmpPassword);
					userInfo.Status = Status.NotAuth;
					_context.Users.Add(userInfo);
					await _context.SaveChangesAsync();

					// 이메일 보내기
					var html = $"email : {userInfo.Email} // password: {tmpPassword}";
					_ses.SendEmail(userInfo.Email, "계정 정보 임시비번", html);

					payload.Status = true;
				}
			}
			catch (Exception ex)
			{
				payload.Status = false;
				payload.Error = ex.Message;
			}
			return payload;
		}
		#endregion

		/// <summary>
		/// 로그인
		/// </summary>
		/// <remarks>
		/// 1. status :true, data: status == 1 -> 비번변경 필수.
		/// 2. status :true, data: status == 0 -> 정상 로그인.
		/// </remarks>
		/// <param name="login"></param>
		/// <returns></returns>
		[HttpPost("Login")]
		#region public async Task<Payload> Login(Login login)
		public async Task<Payload> Login(Login login)
		{
			Payload payload = new Payload();
			try
			{
				var info = await _context.Users.FirstOrDefaultAsync(i => i.Email == login.Email);
				if (info == null)
				{
					payload.Status = false;
					payload.Data = "존재하지 않는 이메일 입니다.";
				}
				else
				{
					var type = info.Type;
					if (type == UserType.Admin)
					{
						// 비번 체크
						var hash = new PasswordHasher<string>();
						var result = hash.VerifyHashedPassword(null, info.Password, login.Password);
						if (result == PasswordVerificationResult.Success)
						{
							if (info.Status == Status.NotAuth || info.Status == Status.Normal)
							{
								// 비밀번호 변경 요청
								var token = _jwtToken.CreateToken(info);
								payload.Data = new Dictionary<string, object>()
								{
									{ "status", info.Status },
									{ "token" , token }
								};
								payload.Status = true;
							}
							else if (info.Status == Status.Deny)
							{
								// 거부
								payload.Status = false;
								payload.Data = "관리자에게 문의 바랍니다.";
							}
							else if (info.Status == Status.Out)
							{
								// 탈퇴
								payload.Status = false;
								payload.Data = "탈퇴 회원입니다.";
							}
						}
						else
						{
							payload.Status = false;
							payload.Data = "비밀번호가 다릅니다.";
						}
					}
					else
					{
						payload.Status = false;
						payload.Data = "관리자가 아닙니다.";
					}
				}
			}
			catch (Exception ex)
			{
				payload.Status = false;
				payload.Error = ex.Message;
			}
			return payload;
		}
		#endregion

		/// <summary>
		/// 최초 비밀번호 변경
		/// </summary>
		/// <returns></returns>
		//[ServiceFilter(typeof(Authfilter))]
		[HttpPost("Reset")]
		#region public async Task<Payload> ResetPassword(PasswordChange passwordChange)
		public async Task<Payload> ResetPassword(PasswordChange passwordChange)
		{
			Payload payload = new Payload();
			try
			{
				var decode = _jwtToken.Decoding();
				var info = await _context.Users.FindAsync(decode.Useridx);
				if (info.Status == Status.NotAuth)
				{
					var hash = new PasswordHasher<string>();
					info.Password = hash.HashPassword(null, passwordChange.Password);
					info.Status = Status.Normal;
					_context.Entry(info).State = EntityState.Modified;
					await _context.SaveChangesAsync();
					payload.Status = true;
				}
				else
				{
					payload.Status = false;
					payload.Data = "비밀번호 변경 가능한 상태가 아닙니다.";
				}
			}
			catch (Exception ex)
			{
				payload.Status = false;
				payload.Error = ex.Message;
			}
			return payload;
		} 
		#endregion
	}
}
