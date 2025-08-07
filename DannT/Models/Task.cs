namespace DannT.Models
{
    public enum TaskStatus
    {
        Completed,
        Pending
    }
    public class Task
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description {  get; set; }
        public DateTime Deadline {  get; set; }
        public TaskStatus Status { get; set; }
        public int TagId { get; set; }
        public int UserId { get; set; }
        public virtual Tag? Tag { get; set; }
        public virtual User? User { get; set; }
    }
}
