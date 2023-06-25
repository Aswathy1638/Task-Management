using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            // Create a dummy list of users
            //        var users = new List<User>
            //{
            //    new User { Id = 1, FullName = "User 1", Email = "user1@example.com", Password = "password1" },
            //    new User { Id = 2, FullName = "User 2", Email = "user2@example.com", Password = "password2" },
            //    new User { Id = 3, FullName = "User 3", Email = "user3@example.com", Password = "password3" }
            //};

            //        // Convert the dummy list to SelectListItem objects
            //        var selectListItems = users.Select(u => new SelectListItem
            //        {
            //            Value = u.Id.ToString(),
            //            Text = u.FullName
            //        });

            //        // Create a SelectList from the SelectListItem objects
            //        var usersSelectList = new SelectList(selectListItems, "Value", "Text");

            //        // Assign the SelectList to the ViewBag.Users
            //        ViewBag.Users = usersSelectList;
            PopulateUsers();
            return View();
        }


        private void PopulateUsers()
        {
            ViewBag.Users = GetUsers();
        }

        private IEnumerable<SelectListItem> GetUsers()
        {
            // Fetch and return the users from the database or any other source
            var users = _taskContext.Users.ToList();

            // Convert the users to SelectListItem objects
            var selectListItems = users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.FullName
            });

            return selectListItems;
        }
        //POST: Task/Create
        [HttpPost]
        public async Task<IActionResult> Create(TaskModel task){
          
                if (ModelState.IsValid)
                {

                   // task.AssignedUser = await _taskContext.Users.FindAsync(task.AssignedUserId);
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