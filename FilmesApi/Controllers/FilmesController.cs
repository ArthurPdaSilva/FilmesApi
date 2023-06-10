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
    public void AdicionarFilme([FromBody] Filme filme)
    {
        filme.Id = _id++;
        _filmes.Add(filme);
        Console.WriteLine(filme.Titulo);
        Console.WriteLine(filme.Duracao);

    }

    [HttpGet]
    public IEnumerable<Filme> RecuparFilmes([FromQuery] int skip = 0, int take = 50)
    {
        return _filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public Filme? RecuparFilmePorId(int id)
    {
        // Skip (pular elementos) e take (pegar elementos)
        return _filmes.FirstOrDefault(filme => filme.Id == id);
    }

}
