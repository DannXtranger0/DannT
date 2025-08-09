using System.Diagnostics.Eventing.Reader;

namespace DannT.Models.DTO
{
    public class ShowTaskDTO
    {
        public int Id { get; set; }
        public string Title{  get; set; }
        public string? Description{  get; set; }
        public int TagId{  get; set; }
        public DateTime Deadline{ get; set; }
    }
}
