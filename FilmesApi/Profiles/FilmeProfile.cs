using AutoMapper;
using FilmesApi.Data.DTOS;
using FilmesApi.Models;

namespace FilmesApi.Profiles;

public class FilmeProfile : Profile
{
    public FilmeProfile() {
        CreateMap<FilmeDTO, Filme>();
    }
}
