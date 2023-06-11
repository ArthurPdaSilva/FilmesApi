using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.DTOS;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
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


    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDTO">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarFilme([FromBody] CreateFilmeDTO filmeDTO)
    {
        Filme filme = _mapper.Map<Filme>(filmeDTO);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        //Qual caminho ele pode seguir para verificar se foi adicionado?
        return CreatedAtAction(
            nameof(RecuperarFilmePorId),
            new { id = filme.Id },
            filme);
    }

    [HttpGet]
    public IEnumerable<ReadFilmeDTO> RecuperarFilmes([FromQuery] int skip = 0, int take = 50)
    {
        var filmes = _context.Filmes.Skip(skip).Take(take);
        return _mapper.Map<List<ReadFilmeDTO>>(filmes); 
    }

    [HttpGet("{id}")]
    public IActionResult RecuperarFilmePorId(int id)
    {
        // Skip (pular elementos) e take (pegar elementos)
        var filme = ProcurarFilme(id);
        if (filme == null) return NotFound();
        var filmeDTO = _mapper.Map<ReadFilmeDTO>(filme);
        return Ok(filmeDTO);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDTO filmeDTO)
    {
        var filme = ProcurarFilme(id);
        if (filme == null) return NotFound();
        _mapper.Map(filmeDTO, filme);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizarFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDTO> patch)
    {
        var filme = ProcurarFilme(id);
        if (filme == null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDTO>(filme);
        patch.ApplyTo(filmeParaAtualizar, ModelState);
        if (!TryValidateModel(filmeParaAtualizar)) return ValidationProblem(ModelState);


        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarFilme(int id)
    {
        var filme = ProcurarFilme(id);
        if (filme == null) return NotFound();
        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }

    private Filme? ProcurarFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        return filme;
    }

}
