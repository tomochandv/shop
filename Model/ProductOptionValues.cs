using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop_model
{
	/// <summary>
	/// 상품옵션벨류
	/// </summary>
	[Table("product_option_value")]
	public class ProductOptionValues
	{
		public ProductOption ProductOption { get; set; }
		/// <summary>
		/// 옵션 아이디
		/// </summary>
		[Comment("상품옵션 아이디"), Required, Column("productopid")]
		public int ProductOptionId { get; set; }
		/// <summary>
		/// 옵션벨류 아이디
		/// </summary>
		[Comment("옵션벨류 아이디"), Key, Required, Column("productopvid")]
		public int ProductOptionValueId { get; set; }
		/// <summary>
		/// 벨류값
		/// </summary>
		[Comment("벨류값"), Column("option_value", TypeName = "varchar(200)"), Required]
		public string Value { get; set; }
		/// <summary>
		/// 추가 가격
		/// </summary>
		[Comment("추가 가격"), Required, Column("price")]
		public int Price { get; set; }
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
	}
}
