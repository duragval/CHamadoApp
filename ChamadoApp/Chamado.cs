using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChamadoApp
{
    internal class Chamado
    {

        public Chamado(int id, string tipo, string descricao, string nomeFuncionario, string departamento,
               DateTime dataAbertura, StatusChamado status,
               string? nomeTecnico, string? comentario, DateTime? dataAtualizacao)
        {
            if (string.IsNullOrWhiteSpace(tipo))
            {
                throw new ArgumentException("Tipo não pode ser nulo ou vazio.");
            }

            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new ArgumentException("Descrição não pode ser nula ou vazia.");
            }

            if (string.IsNullOrWhiteSpace(nomeFuncionario))
            {
                throw new ArgumentException("Nome do funcionário não pode ser nulo ou vazio.");
            }

            if (string.IsNullOrWhiteSpace(departamento))
            {
                throw new ArgumentException("Departamento não pode ser nulo ou vazio.");
            }

            Id = id;
            Tipo = tipo;
            Descricao = descricao;
            NomeFuncionario = nomeFuncionario;
            Departamento = departamento;
            DataAbertura = dataAbertura;
            Status = status;
            NomeTecnico = nomeTecnico;
            Comentario = comentario;
            DataAtualizacao = dataAtualizacao;
        }

        public Chamado(int id, string tipo, string descricao, string nomeFuncionario, string departamento) : this(id, tipo, descricao, nomeFuncionario, departamento, DateTime.Now, StatusChamado.Aberto, null, null, null)
        {
        }

        public int Id { get; }
        public string Tipo { get; }
        public string Descricao { get; }
        public string NomeFuncionario { get; }
        public string Departamento { get; }
        public DateTime DataAbertura { get; }
        public StatusChamado Status { get; private set; }
        public string? NomeTecnico { get;  private set; }
        public string? Comentario { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }

        public void AtualizarStatus(string nomeTecnico, string comentario, StatusChamado novoStatus)
        {
            if (string.IsNullOrEmpty(nomeTecnico))
            {
                throw new ArgumentException("Nome do tecnico não pode ser nulo ou vazio.");
            }

            if (string.IsNullOrEmpty(comentario))
            {
                throw new ArgumentException("Comentário não pode ser nulo ou vazio.");
            }

            NomeTecnico = nomeTecnico;
            Comentario = comentario;
            Status = novoStatus;
            DataAtualizacao = DateTime.Now;
        }

        public string GerarConteudo()
        {
            return
                $"Id={Id}\n" +
                $"Tipo={Tipo}\n" +
                $"Descricao={Descricao}\n" +
                $"NomeFuncionario={NomeFuncionario}\n" +
                $"Departamento={Departamento}\n" +
                $"DataAbertura={DataAbertura:dd/MM/yyyy HH:mm:ss}\n" +
                $"Status={Status}\n" +
                $"NomeTecnico={NomeTecnico}\n" +
                $"Comentario={Comentario}\n" +
                $"DataAtualizacao={(DataAtualizacao.HasValue ? DataAtualizacao.Value.ToString("dd/MM/yyyy HH:mm:ss") : "")}\n";
        }
        public static Chamado CarregarDeTexto(string conteudo)
        {
            var linhas = conteudo.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var campos = new Dictionary<string, string>();

            foreach (var linha in linhas)
            {
                var partes = linha.Split('=', 2);
                if (partes.Length != 2)
                {
                    continue;
                }

                campos[partes[0]] = partes[1];
            }

            var id = int.Parse(campos["Id"]);
            var tipo = campos["Tipo"];
            var descricao = campos["Descricao"];
            var nomeFuncionario = campos["NomeFuncionario"];
            var departamento = campos["Departamento"];

            var dataAbertura = DateTime.ParseExact(
                campos["DataAbertura"],
                "dd/MM/yyyy HH:mm:ss",
                CultureInfo.InvariantCulture);

            var status = Enum.Parse<StatusChamado>(campos["Status"]);

            var nomeTecnico = string.IsNullOrEmpty(campos["NomeTecnico"]) ? null : campos["NomeTecnico"];
            var comentario = string.IsNullOrEmpty(campos["Comentario"]) ? null : campos["Comentario"];

            DateTime? dataAtualizacao = string.IsNullOrEmpty(campos["DataAtualizacao"])
                ? null
                : DateTime.ParseExact(campos["DataAtualizacao"], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            return new Chamado(id, tipo, descricao, nomeFuncionario, departamento,
                dataAbertura, status, nomeTecnico, comentario, dataAtualizacao);
        }
    }
    public enum StatusChamado { Aberto, Andamento, Fechado }
}
