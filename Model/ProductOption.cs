using Microsoft.EntityFrameworkCore;
using shop_admin_api.swagger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop_model
{
	/// <summary>
	/// 상품 옵션
	/// </summary>
	[Table("product_option")]
	public class ProductOption
	{
		public Product Product { get; set; }
		/// <summary>
		/// 상품아이디
		/// </summary>
		[Comment("상품아이디"), Required, Column("productid")]
		public int ProductId { get; set; }
		/// <summary>
		/// 옵션아이디
		/// </summary>
		[Comment("상품옵션 아이디"), Key, Required, Column("productopid")]
		public int ProductOptionId { get; set; }
		/// <summary>
		/// 옵션 타입
		/// </summary>
		[Comment("상품옵션 타입"), Required, Column("option_type")]
		public OptionType OptionType { get; set; }
		/// <summary>
		/// 옵션명
		/// </summary>
		[Comment("상품옵션 명"), Column("option_name", TypeName = "varchar(100)"), Required]
		public string OptionName { get; set; }
		/// <summary>
		/// 추가 가격
		/// </summary>
		[Comment("추가 가격"), Required, Column("add_price")]
		public int AddPrice { get; set; }
		/// <summary>
		/// 사용여부
		/// </summary>
		[Comment("사용여부"), Required, Column("use_yn")]
		public Use Use { get; set; }
		/// <summary>
		/// 등록일
		/// </summary>
		[Comment("등록일"), Required, Column("regdate")]
		public DateTime Regdate { get; set; }

		[SwaggerIgnoreProperty]
		public List<ProductOptionValues> ProductOptionValues { get; set; }
	}
}
