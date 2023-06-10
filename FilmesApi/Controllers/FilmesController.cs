using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmesController : ControllerBase
{
    private static List<Filme> _filmes = new List<Filme>();
    private static int _id = 0;

    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] Filme filme)
    {
        filme.Id = _id++;
        _filmes.Add(filme);
        //Qual caminho ele pode seguir para verificar se foi adicionado?
        return CreatedAtAction(
            nameof(RecuparFilmePorId),
            new { id = filme.Id },
            filme);
    }

    [HttpGet]
    public IEnumerable<Filme> RecuparFilmes([FromQuery] int skip = 0, int take = 50)
    {
        return _filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult RecuparFilmePorId(int id)
    {
        // Skip (pular elementos) e take (pegar elementos)
       var filme = _filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        return Ok(filme);
    }

}
