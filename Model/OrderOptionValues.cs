using Microsoft.EntityFrameworkCore;
using shop_admin_api.swagger;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop_model
{
  [Table("order_product_option")]
  public class OrderOptionValues
  {
    [SwaggerIgnoreProperty, Comment("주문상품옵션아이디"), Key, Required, Column("orderpoidx")]
    public int OrderProductOptionId { get; set; }

    /// <summary>
    /// 주문상품아이디
    /// </summary>
    [Comment("주문상품아이디"),  Required, Column("orderpidx")]
    public int OrderProductId { get; set; }

    /// <summary>
    /// 옵션아이디
    /// </summary>
    [Comment("상품옵션 아이디"), Required, Column("productopid")]
    public int ProductOptionId { get; set; }

    /// <summary>
    /// 옵션벨류 아이디
    /// </summary>
    [Comment("옵션벨류 아이디"), Required, Column("productopvid")]
    public int ProductOptionValueId { get; set; }

  }
}
