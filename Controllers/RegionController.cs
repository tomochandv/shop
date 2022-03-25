using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop_admin_api.Lib;
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
	public class RegionController : ControllerBase
	{
		private readonly ShopDbContext _context;

		public RegionController(ShopDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// 국가 코드 리스트
		/// </summary>
		/// <param name="page">페이지</param>
		/// <param name="rows">행</param>
		/// <returns></returns>
		//[ServiceFilter(typeof(Authfilter))]
		[HttpGet]
		public async Task<Payload> GetRegions(int page = 1, int rows = 10)
		{
			Payload payload = new Payload();
			try
			{
				var start = (page - 1) * rows;
				var list = await _context.Regions.Skip(start).Take(rows).ToListAsync();
				var total = await _context.Regions.CountAsync();
				payload.Status = true;
				payload.Data = new Dictionary<string, object>
				{
					{ "list",  list },
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

		/// <summary>
		/// 국가 코드 상세
		/// </summary>
		/// <param name="id">코드아이디</param>
		/// <returns></returns>
		//[ServiceFilter(typeof(Authfilter))]
		[HttpGet("{id}")]
		public async Task<Payload> GetRegion(int id)
		{
			Payload payload = new Payload();
			try
			{
				var region = await _context.Regions.FindAsync(id);
				payload.Status = true;
				payload.Data = region;
			}
			catch (Exception ex)
			{
				payload.Status = false;
				payload.Error = ex.Message;
			}
			return payload;
		}

		/// <summary>
		/// 국가 코드 상태 수정 [Use만 수정 가능]
		/// Use: 0 사용, 1 미사용
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		//[ServiceFilter(typeof(Authfilter))]
		[HttpPut()]
		public async Task<Payload> PutRegion(Region region)
		{
			Payload payload = new Payload();
			try
			{
				var info = await _context.Regions.FindAsync(region.RegionIdx);
				if (info != null && info.RegionIdx != 0)
				{
					info.Use = region.Use;
					_context.Regions.Update(info);
					await _context.SaveChangesAsync();
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

		/// <summary>
		/// 국가 코드 저장
		/// Use: 0 사용, 1 미사용
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		//[ServiceFilter(typeof(Authfilter))]
		[HttpPost]
		public async Task<Payload> PostRegion(Region region)
		{
			Payload payload = new Payload();
			try
			{
				var info = await _context.Regions.FirstOrDefaultAsync(i => i.RegionCode == region.RegionCode);
				if (info != null)
				{
					payload.Status = false;
					payload.Data = "존재하는 국가코드 입니다.";
				}
				else
				{
					_context.Regions.Add(region);
					await _context.SaveChangesAsync();
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

	}
}
