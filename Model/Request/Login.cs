using System.ComponentModel.DataAnnotations;

namespace shop_admin_api.Model.Request
{
	/// <summary>
	/// 이메일 비번 모델
	/// </summary>
	public class Login
	{
		/// <summary>
		/// 이메일
		/// </summary>
		[Required]
		public string Email { get; set; }
		/// <summary>
		/// 비밀번호
		/// </summary>
		[Required]
		public string Password { get; set; }
	}
}
