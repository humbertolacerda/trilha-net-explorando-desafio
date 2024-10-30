using DesafioDoisDIO.Models;

namespace DesafioDoisDIO.Models
{
    public class Suite
    {
        public Suite() { }

        public Suite(int id, string codigo, string tipoSuite, int capacidade, decimal valorDiaria)
        {
            Id = id;
            Codigo = codigo;
            TipoSuite = tipoSuite;
            Capacidade = capacidade;
            ValorDiaria = valorDiaria;
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string TipoSuite { get; set; }
        public int Capacidade { get; set; }
        public decimal ValorDiaria { get; set; }
    }
}