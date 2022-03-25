using Microsoft.EntityFrameworkCore;
using shop_admin_api.swagger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop_model
{
	/// <summary>
	/// 상품 모델
	/// </summary>
	[Table("product")]
	public class Product
	{
		/// <summary>
		/// 카테고리
		/// </summary>
		[SwaggerIgnoreProperty]
		public Category Category { get; set; }
		[Required, Comment("카테고리 아이디"), Column("categoryid")]
		public int CategoryId { get; set; }
		/// <summary>
		/// 상품아이디
		/// </summary>
		[Comment("상품아이디"), Key, Required, Column("productid")]
		public int ProductId { get; set; }
		/// <summary>
		/// 상품명
		/// </summary>
		[Comment("상품명"), Column("product_name", TypeName = "varchar(100)"), Required]
		public string ProductName { get; set; }
		/// <summary>
		/// 가격
		/// </summary>
		[Comment("가격"), Required, Column("price")]
		public int Price { get; set; }
		/// <summary>
		/// 제목
		/// </summary>
		[Comment("제목"), Column("title", TypeName = "varchar(200)"), Required]
		public string Title { get; set; }
		/// <summary>
		/// 콘텐츠
		/// </summary>
		[Comment("콘텐츠"), Column("contents", TypeName = "text"), Required]
		public string Contents { get; set; }
		/// <summary>
		/// 리스트 이미지 버켓
		/// </summary>
		[Comment("리스트 이미지 버켓"), Column("list_img_bucket", TypeName = "varchar(100)"), Required]
		public string ListImageBucket { get; set; }
		/// <summary>
		/// 리스트 이미지 파일명
		/// </summary>
		[Comment("리스트 이미지 파일명"), Column("list_img_name", TypeName = "varchar(100)"), Required]
		public string ListImageFileName { get; set; }
		/// <summary>
		/// 리스트 이미지 url
		/// </summary>
		[Comment("리스트 이미지 url"), Column("list_img_url", TypeName = "varchar(200)"), Required]
		public string ListImageUrl { get; set; }
		/// <summary>
		/// 디테일 이미지 버켓
		/// </summary>
		[Comment("디테일 이미지 버켓"), Column("detail_img_bucket", TypeName = "varchar(100)"), Required]
		public string DetailImageBucket { get; set; }
		/// <summary>
		/// 디테일 이미지 파일명
		/// </summary>
		[Comment("디테일 이미지 파일명"), Column("detail_img_name", TypeName = "varchar(100)"), Required]
		public string DetailImageFileName { get; set; }
		/// <summary>
		/// 디테일 이미지 url
		/// </summary>
		[Comment("디테일 이미지 url"), Column("detail_img_url", TypeName = "varchar(200)"), Required]
		public string DetailImageUrl { get; set; }
		/// <summary>
		/// 사용여부
		/// </summary>
		[Comment("사용여부"), Required, Column("use_yn")]
		public Use Use { get; set; }
		/// <summary>
		/// 등록일
		/// </summary>
		[SwaggerIgnoreProperty, Comment("등록일"), Required, Column("regdate")]
		public DateTime Regdate { get; set; }

		[SwaggerIgnoreProperty]
		public List<ProductOption> ProductOptions { get; set; }
	}
}
