using DesafioDoisDIO.Models;

namespace DesafioDoisDIO.Models;

public class Pessoa
{
    public Pessoa() { }

    public Pessoa(string nome)
    {
        Nome = nome;
    }

    public Pessoa(string nome, string cpf)
    {
        Nome = nome;
        NumeroCPF = cpf;    
    }

    public Pessoa(int id, string nome, string sobrenome, string cpf)
    {
        Id = id;
        Nome = nome;
        Sobrenome = sobrenome;
        NumeroCPF = cpf;
    }

    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string NumeroCPF { get; set; }
    public string NomeCompleto => $"{Nome} {Sobrenome}".ToUpper();
}