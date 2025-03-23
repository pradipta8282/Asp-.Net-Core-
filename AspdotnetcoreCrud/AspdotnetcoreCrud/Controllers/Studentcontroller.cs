using AspdotnetcoreCrud.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspdotnetcoreCrud.Controllers
{
    [Route("api/[controller]")]//attribute routing for routing.
    [ApiController]// this attribute make it as api controller.so this will apply all the features in this class.
    public class Studentcontroller : ControllerBase
    {
        private readonly mydbContext context;

        public Studentcontroller(mydbContext context)
        {
            this.context = context;
        }
        [HttpGet]

        public async Task<ActionResult<List<Student>>> getstudent()
        {
            var data = await context.Students.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentByID(int id)
        {
            var student = await context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            else { return student; }
        }

        [HttpPost]
        public async Task<ActionResult<Student>> createstudent(Student std)
        {
            var student = await context.Students.AddAsync(std);
            await context.SaveChangesAsync();
            return Ok(std);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> updatestudent(int id, Student std)
        {
            if (id != std.Id)
            {
                return BadRequest();
            }
            context.Entry(std).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(std);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> Deletetestudent(int id)
        {
            var std = await context.Students.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }
             context.Students.Remove(std);
            await context.SaveChangesAsync();
            return Ok(std);
        }
    }

}
