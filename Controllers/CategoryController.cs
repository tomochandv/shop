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
	public class CategoryController : ControllerBase
	{
		private readonly ShopDbContext _context;

		public CategoryController(ShopDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// 카테고리 리스트
		/// </summary>
		/// <param name="page">페이지</param>
		/// <param name="rows">행</param>
		/// <param name="name">카테고리명</param>
		/// <returns></returns>
		//[ServiceFilter(typeof(Authfilter))]
		[HttpGet]
		#region public async Task<Payload> GetCategorys(int page = 1, int rows = 10, string name = "")
		public async Task<Payload> GetCategorys(int page = 1, int rows = 10, string name = "")
		{
			Payload payload = new Payload();
			try
			{
				var start = (page - 1) * rows;
				var query = await _context.Categorys.Where(x => x.CategoryName == null || x.CategoryName.Contains(name)).Include(product => product.Products).ToListAsync();


				var total = query.Count();

				var list = query.OrderBy(x => x.CategoryId)
					.Skip(start)
					.Take(rows)
					.ToList();

				var data = from p in list
									 select new { p.CategoryId, p.CategoryName, p.Use, p.Regdate, Count = p.Products.Count() };
				payload.Data = new Dictionary<string, object>()
				{
					{ "total", total },
					{ "list", data }
				};
				payload.Status = true;
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
		/// 카테고리 상세
		/// </summary>
		/// <param name="id">카테고리 아이디</param>
		/// <returns></returns>
		//[ServiceFilter(typeof(Authfilter))]
		[HttpGet("{id}")]
		#region public async Task<Payload> GetCategory(int id)
		public async Task<Payload> GetCategory(int id)
		{
			Payload payload = new Payload();
			try
			{
				var category = await _context.Categorys.FindAsync(id);
				payload.Status = true;
				payload.Data = category;
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
		/// 카테고리 수정
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		//[ServiceFilter(typeof(Authfilter))]
		[HttpPut]
		#region public async Task<Payload> PutCategory(Category category)
		public async Task<Payload> PutCategory(Category category)
		{
			var payload = new Payload();
			try
			{
				category.Regdate = DateTime.Now;
				_context.Entry(category).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				payload.Status = true;
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
		/// 카테고리 생성
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		//[ServiceFilter(typeof(Authfilter))]
		[HttpPost]
		#region public async Task<Payload> PostCategory(Category category)
		public async Task<Payload> PostCategory(Category category)
		{
			Payload payload = new Payload();
			var info = await _context.Categorys.FirstOrDefaultAsync(i => i.CategoryName == category.CategoryName);
			try
			{
				if (info != null)
				{
					payload.Status = false;
					payload.Data = "존재하는 카테고리명 입니다.";
				}
				else
				{
					_context.Categorys.Add(category);
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
		#endregion
	}
}
