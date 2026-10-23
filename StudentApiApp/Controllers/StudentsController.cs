using Microsoft.AspNetCore.Mvc;
using StudentApiApp.Models;

namespace StudentApiApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private static readonly List<Student> _students = new()
    {
        new Student { Id = 1, Name = "Алихан Смагулов", Group = "SE-301" },
        new Student { Id = 2, Name = "Дина Сарсенова", Group = "SE-302" },
        new Student { Id = 3, Name = "Ерсултан Омаров", Group = "SE-301" }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetAll()
    {
        Console.WriteLine("запрос на получение всех студентов");
        return Ok(_students);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Student> GetById(int id)
    {
        Console.WriteLine($"поиск студента с идентификатором {id}");
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            Console.WriteLine($"студент с кодом {id} не найден");
            return NotFound(new { message = "студент не найден" });
        }

        return Ok(student);
    }

    [HttpPost]
    public ActionResult<Student> Create([FromBody] Student student)
    {
        Console.WriteLine($"добавление студента: {student.Name}, группа {student.Group}");
        _students.Add(student);
        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
    }
}
