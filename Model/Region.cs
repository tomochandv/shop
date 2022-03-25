using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop_model
{
	/// <summary>
	/// 국가 모델
	/// </summary>
	[Table("region")]
	public class Region
	{
		[Comment("국가 아이디"), Key, Required, Column("regionidx")]
		public int RegionIdx { get; set; }
		/// <summary>
		/// 국가코드
		/// </summary>
		[Comment("국가코드"), Required, Column("region_code", TypeName = "varchar(2)")]
		public string RegionCode { get; set; }
		/// <summary>
		/// 국가명
		/// </summary>
		[Comment("국가명"), Column("reggion_name", TypeName = "varchar(100)"), Required]
		public string RegionName { get; set; }
		/// <summary>
		/// 사용여부
		/// </summary>
		[Comment("사용여부"), Required, Column("use_yn")]
		public Use Use { get; set; }

	}
}
