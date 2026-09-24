namespace Products.Models
{

    public class Review
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int Id { get; set; }
        public int Rating { get; set; } // عدد النجوم (من 1 إلى 5)
        public string? Comment { get; set; } // تعليق اختياري من العميل

        // ربط التقييم بالمنتج
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;


        // ربط التقييم بالمستخدم (العميل)
        public int UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
    }
}