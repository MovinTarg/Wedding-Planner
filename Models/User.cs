using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wedding_Planner.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Wedding> PlannedWeddings { get; set; }
        public List<Wedding> WeddingsAttending { get; set; }
        public User()
        {
            PlannedWeddings = new List<Wedding>();
            WeddingsAttending = new List<Wedding>();
        }
    }
}