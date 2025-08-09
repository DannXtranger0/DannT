using DannT.Models.Context;
using DannT.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DannT.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskApi : ControllerBase
    {
        private readonly MyContext _context;

        public TaskApi( MyContext context)
        {
           
            _context = context;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]CreateTaskDTO taskModel)
        {
            var task = new Models.Task
            {
                Title = taskModel.Title,
                Deadline = taskModel.Deadline,
                Description = taskModel.Description,
                Status = Models.TaskStatus.Pending,
                UserId = int.Parse(User.FindFirst("UserId")!.Value),
                TagId= taskModel.TagId,
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        [HttpGet("Task/{id}")]
        public async Task<IActionResult> Show(int? id)
        {
            if (id == null)
                return NotFound();
            
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            var taskDto = new ShowTaskDTO
            {
                Id = task.Id,
                Deadline = task.Deadline,
                Description = task.Description,
                TagId = task.TagId,
                Title = task.Title
            };

            return Ok(taskDto);

        }
        
        [HttpGet("LoadTags")]
        public async Task<IActionResult> LoadTags()
        {
            var tags = _context.Tags;
            return Ok(tags);
        }

        [HttpPatch("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusDTO statusModel,int id)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);

            if (task == null) {
                return BadRequest("No esito");
            }
            if (statusModel.Status)
                task.Status = Models.TaskStatus.Completed;
            else
                task.Status = Models.TaskStatus.Pending;

            await _context.SaveChangesAsync();
            return Ok(new {Status="Actualizado"});


        }
    }
}
