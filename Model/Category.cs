using Microsoft.EntityFrameworkCore;
using shop_admin_api.swagger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop_model
{
	/// <summary>
	/// 카테고리 모델
	/// </summary>
	[Table("category")]
	public class Category
	{
		/// <summary>
		/// 카테고리 아이디
		/// </summary>
		[Comment("카테고리 아이디"), Column("categoryid"), Key, Required]
		public int CategoryId { get; set; }
		/// <summary>
		/// 카테고리 명
		/// </summary>
		[Comment("카테고리 명"), Column("category_name", TypeName = "varchar(100)"), Required]
		public string CategoryName { get; set; }
		/// <summary>
		/// 사용여부
		/// </summary>
		[Required, Comment("사용여부"), Column("use_yn")]
		public Use Use { get; set; }
		/// <summary>
		/// 저장일
		/// </summary>
		[SwaggerIgnoreProperty, Comment("저장일"), Required, Column("regdate")]
		public DateTime Regdate { get; set; }
		[SwaggerIgnoreProperty]
		public List<Product> Products { get; set; }
	}
}
