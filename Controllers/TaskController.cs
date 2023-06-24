using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TaskController : ControllerBase
    {
       private readonly TaskContext _taskContext;
        public TaskController(TaskContext taskContext)
        {
            _taskContext = taskContext;
        }
        //GET : api/task
        [HttpGet]
        public async Task<String> GetString() {
            return "hello world";
        }
    }
}
