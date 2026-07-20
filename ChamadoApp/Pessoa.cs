using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace ChamadoApp
{

    abstract class Pessoa
    {
        public Pessoa()
        {
        }

        public Pessoa(string nome, string nascimento, long cpf)
        {
            this.Nome = nome;
            this.Nascimento = nascimento;
            this.Cpf = cpf;
        }


        internal string Nome { get; set; }
        internal string Nascimento { get; set; }
        internal long Cpf {  get; set; }

        public abstract void Info();
    }

    internal class Funcionario : Pessoa 
    { 
        public Funcionario() { } 
        public Funcionario(string nome, string nascimento, long cpf, int id, string departamento) : base(nome, nascimento, cpf) 
        { 
            this.Id = id; 
            this.Departameto = departamento; 
        } 
        public int Id { get; set; } 
        public string Departameto { get; set; }

        public override void Info()
        {
            Console.WriteLine($"--INFORMAÇÕES--\nNome:{Nome}\nId:{Id}\nDepartamento:{Departameto}\n");
        }
    }
    internal class Tecnico : Pessoa 
    { 
        public Tecnico() { } 
        public Tecnico(string nome, string nascimento, long cpf, int id, string funcao) : base(nome, nascimento, cpf)
        {
            this.Id = id; 
            this.Funcao = funcao; 
        }
        public int Id { get; set; }
        public string Funcao { get; set; }

        public override void Info()
        {
            Console.WriteLine($"--INFORMAÇÕES--\nNome:{Nome}\nId:{Id}\nFunção:{Funcao}\n");
        }
    }
}
