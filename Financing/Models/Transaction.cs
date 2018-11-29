using System;
namespace Financing.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public DateTime TargetMonth { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
    }
}
