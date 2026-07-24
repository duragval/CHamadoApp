using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata;
using System.Text;

namespace ChamadoApp
{
    abstract class Pessoa
    {

        public Pessoa(int id, string nome, string nascimento, string cpf)
        {
            this.Id = id;
            this.Nome = nome;

            if(!DateOnly.TryParseExact(
                nascimento,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateOnly dataConvertida))
            {
                throw new ArgumentException("Data de nascimento inválida. Use o formato dd/MM/yyyy.");
            }

            Nascimento = dataConvertida;

            var cpfLimpo = new string(cpf.Where(char.IsDigit).ToArray());
            if (string.IsNullOrEmpty(cpfLimpo) || cpfLimpo.Length != 11)
            {
                throw new ArgumentException("CPF inválido. Informe 11 dígitos numéricos.");
            }
        }

        internal int Id { get; }
        internal string Nome { get; }
        internal DateOnly Nascimento { get; }
        internal string Cpf {  get;}

    }

    internal class Funcionario : Pessoa 
    { 
        public Funcionario(string nome, DateOnly nascimento, string cpf, int id, string departamento) : base(id, nome, nascimento, cpf) 
        { 
            this.Departamento = departamento; 
        } 
        public string Departamento { get; set; }


    }
    internal class Tecnico : Pessoa 
    {
        public Tecnico(string nome, DateOnly nascimento, string cpf, int id, string funcao) : base(id, nome, nascimento, cpf)
        {
            this.Funcao = funcao; 
        }

        public string Funcao { get; set; }

    }
}
