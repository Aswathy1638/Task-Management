using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    //[Route("[controller]")]
    //[ApiController]
    public class TaskController : Controller
    {
        private readonly TaskContext _taskContext;
        public TaskController(TaskContext taskContext)
        {
            _taskContext = taskContext;
        }
        // GET: /task
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var tasks = await _taskContext.Tasks.ToListAsync();
            return View(tasks);
            //return Ok(tasks);
        }
        [HttpGet]
        // GET: Task/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Task/Create
        [HttpPost]
        public async Task<IActionResult> Create(TaskModel task){
            if (ModelState.IsValid)
            {
                await _taskContext.Tasks.AddAsync(task);
                await _taskContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(task);
        }

        //GET: Task/Edit/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> Edit(int id) {
            var task = await _taskContext.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        private bool TaskExists(int id)
        {
            return _taskContext.Tasks.Any(t => t.Id == id);
        }
    }
}