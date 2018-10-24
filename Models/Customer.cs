using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace E_Commerce.Models
{
    public class Customer
    {
        public int CustomerId {get; set;}

        [Required(ErrorMessage = "Customer name can't be blank")]
        public string Name {get; set;}

        // list of orders
        public List<Order> Orders {get; set;}

        public Customer()
        {
            Orders = new List<Order> ();
        }

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