using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop_admin_api.Lib;
using shop_admin_api.Model.S3;
using shop_model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shop_admin_api.Controllers
{
	[Produces("application/json")]
	[Route("[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly ShopDbContext _context;
		private readonly S3 _s3;

		public ProductController(ShopDbContext context, S3 s3)
		{
			_context = context;
			_s3 = s3;
		}

		/// <summary>
		/// 상품 리스트
		/// </summary>
		/// <param name="page">페이지</param>
		/// <param name="rows">행</param>
		/// <param name="categoryId">카테고리아이디</param>
		/// <param name="productName">상품명</param>
		/// <returns></returns>
		[HttpGet]
		#region public async Task<Payload> GetProducts(int page = 1, int rows = 10, int categoryId = 0, string productName = "")
		public async Task<Payload> GetProducts(int page = 1, int rows = 10, int categoryId = 0, string productName = "")
		{
			var payload = new Payload();
			try
			{
				var start = (page - 1) * rows;
				var list = await _context.Products
					.Where(x => x.ProductName == null || x.ProductName.Contains(productName))
					.Where(x => categoryId != 0 ? x.CategoryId.Equals(categoryId) : x.CategoryId.Equals(x.CategoryId))
					.ToListAsync();

				var total = list.Count;
				var query = from product in list
										join category in _context.Categorys on product.CategoryId equals category.CategoryId
										orderby product.ProductName
										select new
										{
											product.ProductName,
											product.ProductId,
											product.Use,
											product.Title,
											product.Regdate,
											product.Price,
											product.ListImageUrl,
											product.ListImageFileName,
											product.ListImageBucket,
											product.Category.CategoryName
										};

				var data = query.Skip(start).Take(rows);

				payload.Status = true;
				payload.Data = new Dictionary<string, object>
				{
					{ "list",  data.ToList() },
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
		/// 상품 상세 페이지
		/// </summary>
		/// <param name="id">상품아이디</param>
		/// <returns></returns>
		[HttpGet("{id}")]
		#region public async Task<Payload> GetProduct(int id)
		public async Task<Payload> GetProduct(int id)
		{
			var payload = new Payload();
			try
			{
				var query = from p in _context.Products
										join c in _context.Categorys on p.CategoryId equals c.CategoryId
										where p.ProductId == id
										select new
										{
											p.ProductName,
											p.ProductId,
											p.Use,
											p.Title,
											p.Regdate,
											p.Price,
											p.ListImageUrl,
											p.ListImageFileName,
											p.ListImageBucket,
											p.Category.CategoryName,
											p.Category.CategoryId,
											p.ProductOptions
										};

				var options = from p in _context.ProductOptions
											where p.ProductId == id
											select new
											{
												p.ProductOptionId,
												p.ProductOptionValues
											};

				var product = await query.FirstAsync();
				payload.Data = await options.ToListAsync();
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
		/// 상품 수정
		/// </summary>
		/// <param name="product">상품 정보</param>
		/// <param name="productOptions">상품 옵션 정보</param>
		/// <param name="files">상품 디테일 이미지들</param>
		/// <returns></returns>
		[HttpPut]
		#region public async Task<Payload> PutProduct(Product product, List<ProductOption> productOptions, List<IFormFile> files)
		public async Task<Payload> PutProduct(Product product, List<ProductOption> productOptions, List<IFormFile> files)
		{
			var payload = new Payload();
			try
			{
				_context.Entry(product).State = EntityState.Modified;
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
		/// 상품 저장
		/// </summary>
		/// <param name="product">상품 정보</param>
		/// <param name="productOptions">상품 옵션 정보</param>
		/// <param name="files">상품 디테일 이미지들</param>
		/// <returns></returns>
		#region public async Task<Payload> PostProduct(Product product, List<ProductOption> productOptions, List<IFormFile> files)
		[HttpPost, DisableRequestSizeLimit]
		public async Task<Payload> PostProduct(Product product, List<ProductOption> productOptions, List<IFormFile> files)
		{
			var payload = new Payload();
			try
			{
				_context.Products.Add(product);
				await _context.SaveChangesAsync();
				var productid = product.ProductId;
				// 옵션들 저장

				//vkdlfem,f wjwkd

				payload.Data = productid;
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
		/// 파일 업로드
		/// </summary>
		/// <param name="file"></param>
		/// <param name="type">0: 리스트, 1: 디테일</param>
		/// <returns></returns>
		[HttpPost("/Product/Upload")]
		#region public async Task<Payload> UploadImage([Required] IFormFile file, [Required] FileTpye type)
		public async Task<Payload> UploadImage([Required] IFormFile file = null, [Required] FileTpye type = FileTpye.DetailImage)
		{
			var payload = new Payload();
			try
			{
				var result = await _s3.Upload(file, type);

				payload.Data = result;
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
	}

}
