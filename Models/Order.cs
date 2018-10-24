using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace E_Commerce.Models
{
    public class Order 
    {
        public int OrderId {get; set;}

        [Required]
        [Range(1, 10000000, ErrorMessage = "Invalid purchase amount!")]
        public int Amount {get; set;}

        public int CustomerId {get; set;}
        public Customer Customer {get; set;}

        public int ProductId {get; set;}
        public Product Product {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        public string TimeSpan {
            get {
                TimeSpan timeSpan = DateTime.Now.Subtract(this.CreatedAt);
                if (timeSpan.TotalSeconds < 60)
                {
                    return $"{Math.Round(timeSpan.TotalSeconds)} seconds ago";
                }
                else if (timeSpan.TotalMinutes < 60)
                {
                    return $"{Math.Round(timeSpan.TotalMinutes)} minutes ago";
                }
                else if (timeSpan.TotalHours < 24)
                {
                    return $"{Math.Round(timeSpan.TotalHours)} hours ago";
                }
                else if (timeSpan.Days <= 7)
                {
                    return $"{timeSpan.Days} days ago";
                }
                else
                {
                    return null;
                }
            }
        }
    }
}