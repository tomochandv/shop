using Microsoft.EntityFrameworkCore;
using shop_admin_api.swagger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop_model
{
	/// <summary>
	/// 주문마스터
	/// </summary>
	[Table("order_info")]
	public class Order
  {
		/// <summary>
		/// 주문번호
		/// </summary>
		[SwaggerIgnoreProperty, Comment("주문번호"), Key, Required, Column("orderidx")]
		public int OrderIdx { get; set; }
		/// <summary>
		/// 회원 아이디
		/// </summary>
		[SwaggerIgnoreProperty, Comment("회원 아이디"), Required, Column("useridx")]
		public int Useridx { get; set; }

		/// <summary>
		/// 우편번호
		/// </summary>
		[Comment("우편번호"), Required, Column("zip")]
		public int Zip { get; set; }
		/// <summary>
		/// 국가번호
		/// </summary>
		[Comment("국가번호"), Required, Column("regionidx")]
		public int RegionIdx { get; set; }
		/// <summary>
		/// 주소
		/// </summary>
		[Comment("주소"), Column("address", TypeName = "varchar(200)"), Required]
		public string Address { get; set; }
		/// <summary>
		/// 전화번호
		/// </summary>
		[Comment("전화번호"), Column("tell", TypeName = "varchar(30)"), Required]
		public string Tell { get; set; }
		/// <summary>
		/// 주문상태
		/// </summary>
		[Comment("주문상태"), Required, Column("order_status")]
		public OrderStatus OrderStatus { get; set; }
		/// <summary>
		/// 저장일
		/// </summary>
		[SwaggerIgnoreProperty, Comment("저장일"), Required, Column("regdate")]
		public DateTime Regdate { get; set; }
	}
}
