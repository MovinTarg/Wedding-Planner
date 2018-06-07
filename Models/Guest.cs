namespace Wedding_Planner.Models
{
    public class Guest : BaseEntity
    {
        public int GuestId { get; set; }
        public int UserId { get; set; }
        public User Attendee { get; set; }
        public int WeddingId { get; set; }
        public Wedding WeddingAttended { get; set; }
    }
}