using System;
using System.Collections.Generic;
using System.Text;

namespace ChamadoApp
{
    internal class Chamado
    {
        public Chamado(int id, string tipo, string descricao, string nomeFuncionario, string departamento)
        {
            Id = id;
            Tipo = tipo;
            Descricao = descricao;
            NomeFuncionario = nomeFuncionario;
            Departamento = departamento;
            DataAbertura = DateTime.Now;
            Status = StatusChamado.Aberto;
        }

        public Chamado(int id, string tipo, string descricao, string nomeFuncionario, string departamento, DateTime dataAbertura, StatusChamado status, string? nomeTecninco, string? comentario, DateTime? dataAtualizacao) : this(id, tipo, descricao, nomeFuncionario, departamento)
        {
            DataAbertura = dataAbertura;
            Status = status;
            NomeTecnico = nomeTecninco;
            Comentario = comentario;
            DataAtualizacao = dataAtualizacao;
        }

        public int Id { get; }
        public string Tipo { get; }
        public string Descricao { get; }
        public string NomeFuncionario { get; }
        public string Departamento { get; }
        public DateTime DataAbertura { get; }
        public StatusChamado Status { get; }
        public string? NomeTecnico { get; private set; }
        public string? Comentario { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }
    }

    public enum StatusChamado { Aberto, Andamento, Fechado }
}
