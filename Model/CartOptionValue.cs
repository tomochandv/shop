using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop_model
{
  /// <summary>
  /// 장바구니 옵션 벨류
  /// </summary>
  [Table("cart_option_value")]
  public class CartOptionValue
  {
    [Comment("카트 옵션벨류 아이디"), Key, Required, Column("cartopvid")]
    public int CartOptionValueIdx { get; set; }
    /// <summary>
    /// 장바구니 아이디
    /// </summary>
    [Comment("장바구니 아이디"), Required, Column("cartid")]
    public int CartId { get; set; }

    /// <summary>
    /// 옵션벨류 아이디
    /// </summary>
    [Comment("옵션벨류 아이디"), Required, Column("productopvid")]
    public int ProductOptionValues { get; set; }
  }
}
