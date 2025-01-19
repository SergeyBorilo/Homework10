using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AnimalHotelWebApp.Controllers;

[Route("api/animals")]
[ApiController]
public class AnimalController : ControllerBase
{
    private static readonly List<Animal> s_animals = [];

    // GET: api/tasks?page=1&pageSize=3
    [HttpGet]
    public IEnumerable<Animal> GetAnimals(
        [Required] int page = 1,
        [Required] int pageSize = 3)
    {
        var skip = (page - 1) * pageSize;
        return s_animals.Skip(skip).Take(pageSize);
    }

    // GET: api/tasks/1
    [HttpGet("{id}")]
    public ActionResult<Animal> GetAnimalsDetails(
        [FromRoute][Required] int id)
    {
        var animal = s_animals.FirstOrDefault(a => a.Id == id);
        if (animal == null) return NotFound("Animal not found.");
        return Ok(animal);
    }

    [HttpPost]
    public IActionResult AddAnimal(
        [FromBody][Required] Animal animal)
    {
        s_animals.Add(new Animal
        {
            Id = s_animals.Count + 1,
            Name = animal.Name,
            Age = animal.Age
        });
        return CreatedAtAction(nameof(GetAnimalsDetails), new
        {
            id = s_animals.Last().Id,
        },
        s_animals.Last());
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAnimal(
        [FromRoute][Required] int id,
        [FromBody][Required] Animal updatedAnimal)
    {
        var animal = s_animals.FirstOrDefault(a => a.Id == id);
        if (animal == null) return NotFound("Animal not found.");

        animal.Age = updatedAnimal.Age;
        animal.Name = updatedAnimal.Name;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAnimal(
        [FromRoute][Required] int id)
    {
        var animal = s_animals.FirstOrDefault(a => a.Id == id);
        if (animal == null) return NotFound("Animal not found.");

        s_animals.Remove(animal);
        return NoContent();
    }
}
