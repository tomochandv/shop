using Microsoft.EntityFrameworkCore;
using shop_model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop_admin_api.Model.Request
{
	/// <summary>
	/// 사용자 추가 모델 
	/// </summary>
	public class UserAdd
	{
		/// <summary>
		/// 국가
		/// </summary>
		[Comment("국가아이디")]
		[Required]
		public int RegionIdx { get; set; }

		/// <summary>
		/// 이메일
		/// </summary>
		[Comment("이메일")]
		[Column(TypeName = "varchar(200)"), Required]
		public string Email { get; set; }

		/// <summary>
		/// 이름
		/// </summary>
		[Comment("이름")]
		[Column(TypeName = "varchar(50)"), Required]
		public string Name { get; set; }

		/// <summary>
		/// 사용자 타입
		/// </summary>
		[Comment("사용자 타입")]
		[Required]
		public UserType Type { get; set; }
	}
}
