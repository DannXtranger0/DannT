using DannT.Models.Context;
using DannT.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DannT.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedApi : ControllerBase
    {
        private readonly MyContext _context;
        public FeedApi(MyContext context)
        {
            _context = context;
        }
        [HttpPost("Feed")]
        public async Task<IActionResult> Feed([FromBody] FeedSearchDTO feedSearch)
        {
            int userId = int.Parse(User.FindFirst("UserId")!.Value);
            var query = _context.Tasks.Where(x=>x.UserId==userId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(feedSearch.InputSearch))
                query = query.Where(x => x.Title.Contains(feedSearch.InputSearch) || x.Description.Contains(feedSearch.InputSearch));


            if (feedSearch.CbCompleted && !feedSearch.CbPending)
                query = query.Where(x => x.Status == Models.TaskStatus.Completed);
            else if (!feedSearch.CbCompleted && feedSearch.CbPending)
                query = query.Where(x => x.Status == Models.TaskStatus.Pending);

            if(feedSearch.TagId!=0)
                query = query.Where(x => x.TagId == feedSearch.TagId);

            var taskList = query.Select(x =>new FeedTaskDTO
            {
                Id = x.Id,
                Title = x.Title,
                Deadline = x.Deadline,
                Status = x.Status.ToString(),
                Tag = x.Tag.Name
            }).ToList();
            return Ok(taskList);


        }
    }
}
