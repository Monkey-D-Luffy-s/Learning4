using System.ComponentModel.DataAnnotations;

namespace Learning4.Models.Coupons
{
    public class AddCouponModel
    {
        [Required(ErrorMessage = "Required CouponCode")]
        public string CouponCode { get; set; }
        [Required(ErrorMessage = "Required DiscountAmount")]
        public decimal DiscountAmount { get; set; }
        [Required(ErrorMessage = " Required MinimumPurchaseAmount")]

        public decimal MinimumPurchaseAmount { get; set; }
        [Required(ErrorMessage = " Required ExpiryDate ")]
        public DateTime ExpiryDate { get; set; }
    }
}
