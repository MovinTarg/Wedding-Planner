namespace Wedding_Planner.Models
{
    public class Guest : BaseEntity
    {
        public int Id { get; set; }
 
        public int AttendeeId { get; set; }
        public User Attendee { get; set; }
 
        public int WeddingId { get; set; }
        public Wedding WeddingAttended { get; set; }
    }
}