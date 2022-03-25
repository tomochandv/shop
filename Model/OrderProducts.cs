using Microsoft.EntityFrameworkCore;
using shop_admin_api.swagger;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop_model
{
  /// <summary>
  /// 주문 상품 정보
  /// </summary>
  [Table("order_product")]
  public class OrderProducts
  {
    /// <summary>
    /// 주문상품아이디
    /// </summary>
    [SwaggerIgnoreProperty, Comment("주문상품아이디"), Key, Required, Column("orderpidx")]
    public int OrderProductId { get; set; }
    /// <summary>
    /// 주문번호
    /// </summary>
    [Comment("주문번호"), Required, Column("orderidx")]
    public int OrderIdx { get; set; }
    /// <summary>
    /// 상품아이디
    /// </summary>
    [Comment("상품아이디"), Required, Column("productid")]
    public int ProductId { get; set; }

  }
}
