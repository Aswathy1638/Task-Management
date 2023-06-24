using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Views.Task
{
    public class IndexModel : PageModel
    {
        private readonly TaskManagement.Data.TaskContext _context;

        public IndexModel(TaskManagement.Data.TaskContext context)
        {
            _context = context;
        }

        public IList<TaskModel> TaskModel { get;set; } = default!;

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            if (_context.Tasks != null)
            {
                TaskModel = await _context.Tasks.ToListAsync();
            }
        }
    }
}
