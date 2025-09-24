using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning4.Models.Coupons
{
    public class CoouponBase
    {
        [Key]
        public Guid CouponId { get; set; }
        public string CouponCode { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal DiscountAmount { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal MinimumPurchaseAmount { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
