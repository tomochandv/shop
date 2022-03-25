using Microsoft.EntityFrameworkCore;
using shop_admin_api.swagger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop_model
{
	/// <summary>
	/// 장바구니
	/// </summary>
	[Table("cart")]
	public class Cart
  {
		public Product Product { get; set; }
		/// <summary>
		/// 상품아이디
		/// </summary>
		[Comment("상품아이디"), Required, Column("productid")]
		public int ProductId { get; set; }

		/// <summary>
		/// 회원아이디
		/// </summary>
		[Comment("회원아이디"), Required, Column("useridx")]
		public int Useridx { get; set; }

		/// <summary>
		/// 장바구니 아이디
		/// </summary>
		[Comment("장바구니 아이디"), Key, Required, Column("cartid")]
		public int CartId { get; set; }

		/// <summary>
		/// 등록일
		/// </summary>
		[Comment("등록일"), Required, Column("regdate")]
		public DateTime Regdate { get; set; }

		[SwaggerIgnoreProperty]
		public List<CartOptionValue> CartOptionValue { get; set; }
	}
}
