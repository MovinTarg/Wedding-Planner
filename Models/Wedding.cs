using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wedding_Planner.Models
{
    public class Wedding : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string WedderOne { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string WedderTwo { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime WeddingDate { get; set; }
        [Required]
        public string WeddingAddress { get; set; }
        public int PlannerId { get; set; }
        public User Planner { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Guest> Attendees { get; set; }
        public Wedding()
        {
            Attendees = new List<Guest>();
        }
    }
}