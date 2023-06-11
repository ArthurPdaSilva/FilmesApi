using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.DTOS;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmesController : ControllerBase
{

    private readonly FilmeContext _context;
    private readonly IMapper _mapper;

    public FilmesController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] FilmeDTO filmeDTO)
    {
        Filme filme = _mapper.Map<Filme>(filmeDTO);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        //Qual caminho ele pode seguir para verificar se foi adicionado?
        return CreatedAtAction(
            nameof(RecuparFilmePorId),
            new { id = filme.Id },
            filme);
    }

    [HttpGet]
    public IEnumerable<Filme> RecuparFilmes([FromQuery] int skip = 0, int take = 50)
    {
        return _context.Filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult RecuparFilmePorId(int id)
    {
        // Skip (pular elementos) e take (pegar elementos)
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        return Ok(filme);
    }

}
