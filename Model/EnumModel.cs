using System.ComponentModel;

namespace shop_model
{
	/// <summary>
	/// 사용여부
	/// 0: 사용, 1: 미사용
	/// </summary>
	public enum Use
	{
		Yes = 0,
		No = 1
	}

	/// <summary>
	/// 사용자 상태
	/// 0:정상, 1: 인증안됨 임시비번, 2: 거부, 3: 탈퇴
	/// </summary>
	public enum Status
	{
		/// <summary>
		/// 정상
		/// </summary>
		[Description("Man Description")]
		Normal = 0,
		/// <summary>
		/// 인증안됨. 즉 임시비번 상태
		/// </summary>
		NotAuth = 1,
		/// <summary>
		/// 거부
		/// </summary>
		Deny = 2,
		/// <summary>
		/// 탈퇴
		/// </summary>
		Out = 3
	}

	/// <summary>
	/// 사용자 타입
	/// 0 관리자 , 1: 사용자
	/// </summary>
	public enum UserType
	{
		Admin = 0,
		User = 1
	}

	/// <summary>
	/// 파일 타입
	/// 0: 리스트, 1: 디테일
	/// </summary>
	public enum FileTpye
	{
		/// <summary>
		/// 리스트 이미지
		/// </summary>
		ListImage = 0,
		/// <summary>
		/// 디테일 이미지
		/// </summary>
		DetailImage = 1
	}

	/// <summary>
	/// 상품옵션타입
	/// </summary>
	public enum OptionType
  {
		/// <summary>
		/// 셀렉트
		/// </summary>
		Select = 0,
		/// <summary>
		/// 텍스트
		/// </summary>
		Text = 1,
		/// <summary>
		/// 체크박스
		/// </summary>
		Checkbox = 3,
  }

	public enum OrderStatus
  {
		/// <summary>
		/// 주문
		/// </summary>
		Order = 0,
		/// <summary>
		/// 준비중
		/// </summary>
		Preparing = 1,
		/// <summary>
		/// 배송중
		/// </summary>
		Send = 2,
		/// <summary>
		/// 배송완료
		/// </summary>
		End = 3,
		/// <summary>
		/// 주문취소
		/// </summary>
		Cancle = 4
  }
}
