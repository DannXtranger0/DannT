using System.Diagnostics.Eventing.Reader;

namespace DannT.Models.DTO
{
    public class FeedSearchDTO
    {
        public string InputSearch {  get; set; }
        public bool CbCompleted { get; set; }
        public bool CbPending { get; set; }
        public int TagId { get; set; }
    }
}
