namespace shop_model
{
	/// <summary>
	/// 결과 반환 모델
	/// </summary>
	public class Payload
	{
		/// <summary>
		/// 상태
		/// </summary>
		public bool Status { get; set; }
		/// <summary>
		/// 데이터
		/// </summary>
		public object Data { get; set; }
		/// <summary>
		/// 에러시 메세지
		/// </summary>
		public object Error { get; set; }
	}
}
