using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace E_Commerce.Models
{
    public class Product 
    {
        public int ProductId {get; set;}

        [Required(ErrorMessage = "Invalid name length! Product name should be minimum 3 characters long.")]
        public string Name {get; set;}

        [Required(ErrorMessage = "A url is required")]
        [Url]
        public string Img {get; set;}

        [MinLength(10, ErrorMessage = "Product description should be minimum 10 characters long")]
        public string Description {get; set;}

        [Range(1, 1000000000, ErrorMessage = "Invalid Price")]
        public decimal Price {get; set;}

        [Range(1, 10000000000, ErrorMessage = "Invalid quantity")]
        public int Qty {get; set;}

        // list of orders
        public List<Order> Orders {get; set;}

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