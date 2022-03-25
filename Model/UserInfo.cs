using Microsoft.EntityFrameworkCore;
using shop_admin_api.swagger;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop_model
{
	/// <summary>
	/// 사용자 정보 모델
	/// </summary>
	[Table("userinfo")]
	public class UserInfo
	{
		/// <summary>
		/// 국가
		/// </summary>
		[Comment("국가아이디"), Required, Column("regionidx")]
		public int RegionIdx { get; set; }
		/// <summary>
		/// 아이디
		/// </summary>
		[SwaggerIgnoreProperty, Comment("아이디"), Key, Required, Column("useridx")]
		public int Useridx { get; set; }
		/// <summary>
		/// 이메일
		/// </summary>
		[Comment("이메일"), Column("email", TypeName = "varchar(200)"), Required]
		public string Email { get; set; }
		/// <summary>
		/// 비밀번호
		/// </summary>
		[SwaggerIgnoreProperty, Comment("비밀번호"), Column("password", TypeName = "varchar(500)")]
		public string Password { get; set; }
		/// <summary>
		/// 이름
		/// </summary>
		[Comment("이름"), Column("user_name", TypeName = "varchar(50)"), Required]
		public string Name { get; set; }
		/// <summary>
		/// 전화번호
		/// </summary>
		[Comment("전화번호"), Column("tell", TypeName = "varchar(50)"), Required]
		public string Tell { get; set; }
		/// <summary>
		/// 소속
		/// </summary>
		[Comment("소속"), Column("institution", TypeName = "varchar(100)"), Required]
		public string Institution { get; set; }
		/// <summary>
		/// 주소
		/// </summary>
		[Comment("주소"), Column("address", TypeName = "varchar(200)"), Required]
		public string Address { get; set; }
		/// <summary>
		/// 사용자 상태
		/// </summary>
		[Comment("사용자상태"), Required, Column("user_status")]
		public Status Status { get; set; }
		/// <summary>
		/// 사용자 타입
		/// </summary>
		[Comment("사용자 타입"), Required, Column("user_type")]
		public UserType Type { get; set; }
		/// <summary>
		/// 저장일
		/// </summary>
		[SwaggerIgnoreProperty, Comment("저장일"), Required, Column("regdate")]
		public DateTime Regdate { get; set; }
	}
}
