using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.DTOS
{
    public class ReadFilmeDTO
    {
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public int Duracao { get; set; }
        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
    }
}
