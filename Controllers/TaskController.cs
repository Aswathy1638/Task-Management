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
        [HttpGet("Edit/{id}")]
        public async Task<ActionResult> Edit(int id) {
            var task = await _taskContext.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, TaskModel task)
        {
            if(id != task.Id)
            {
                return BadRequest();
            }
            _taskContext.Entry(task).State = EntityState.Modified;

            try
            {
                await _taskContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var task = await _taskContext.Tasks.FindAsync(id);
            if(task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _taskContext.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            _taskContext.Remove(task);
            _taskContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> Search(string searchTerm, string sortOrder)
        {
            if(searchTerm != null)
            {
                if(string.IsNullOrWhiteSpace(searchTerm))
                {
                    return View();
                }
                var task1 = _taskContext.Tasks.Where(item => item.Title.Contains(searchTerm)).ToList();
                return View(task1);
            } 
            else
            {
                var task = _taskContext.Tasks.OrderByDescending(item => item.Priority).ToList();
                return View(task);
            }
        }

        private bool TaskExists(int id)
        {
            return _taskContext.Tasks.Any(t => t.Id == id);
        }
    }
}