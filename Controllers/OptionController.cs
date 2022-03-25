using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop_admin_api.Lib;
using shop_model;
using System;
using System.Threading.Tasks;

namespace shop_admin_api.Controllers
{
  [Produces("application/json")]
  [Route("/[controller]")]
  [ApiController]
  public class OptionController : ControllerBase
  {
    private readonly ShopDbContext _context;
    public OptionController(ShopDbContext context)
    {
      _context = context;
    }


		/// <summary>
		/// 상품옵션 생성
		/// </summary>
		/// <param name="option"></param>
		/// <returns></returns>
		//[ServiceFilter(typeof(Authfilter))]
		[HttpPost]
		#region public async Task<Payload> PostCategory(ProductOption option)
		public async Task<Payload> PostCategory(ProductOption option)
		{
			var payload = new Payload();
			try
			{
				_context.ProductOptions.Add(option);
				await _context.SaveChangesAsync();
				payload.Data = option.ProductId;
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
