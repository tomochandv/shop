using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop_model
{
	/// <summary>
	/// 상품 디테일 이미지
	/// </summary>
	[Table("product_detail_img")]
	public class ProductDetailImage
	{
		public Product Product { get; set; }
		/// <summary>
		/// 상품아이디
		/// </summary>
		[Comment("상품아이디"), Required, Column("productid")]
		public int ProductId { get; set; }

		[Comment("이미지 아이디"), Key, Column("imgidx")]
		public int DetailImageId { get; set; }
		/// <summary>
		/// 리스트 이미지 버켓
		/// </summary>
		[Comment("이미지 버켓"), Column("img_bucket", TypeName = "varchar(100)"), Required]
		public string ListImageBucket { get; set; }
		/// <summary>
		/// 리스트 이미지 파일명
		/// </summary>
		[Comment("이미지 파일명"), Column("img_name", TypeName = "varchar(100)"), Required]
		public string ListImageFileName { get; set; }
		/// <summary>
		/// 리스트 이미지 url
		/// </summary>
		[Comment("이미지 url"), Column("img_url", TypeName = "varchar(200)"), Required]
		public string ListImageUrl { get; set; }
		/// <summary>
		/// 등록일
		/// </summary>
		[Comment("등록일"), Required, Column("regdate")]
		public DateTime Regdate { get; set; }
	}
}
