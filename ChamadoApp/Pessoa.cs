using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace ChamadoApp
{
    public class Chamado()
    {
        public void MostraTipo()
        {
            List<string> tipos = new List<string> { "0.Software", "1.Hardware", "2.Outros" };

            foreach (string tipo in tipos)
            {
                Console.WriteLine($"{tipo}\n");
            }
        }

        public void AddNota()
        {
            Console.WriteLine("Digite o motivo do chamado:\n");
            string nota = Console.ReadLine();
        }
    }
    abstract class Pessoa
    {

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
        public Funcionario(string nome, string nascimento, long cpf, int id, string departamento) : base(nome, nascimento, cpf) 
        { 
            this.Id = id; 
            this.Departamento = departamento; 
        } 
        public int Id { get; set; } 
        public string Departamento { get; set; }

        public override void Info()
        {
            Console.WriteLine($"--INFORMAÇÕES--\nNome:{Nome}\nId:{Id}\nDepartamento:{Departamento}\n");
        }

        public void AbrirChamado()
        {
            Chamado x = new Chamado();
            Console.WriteLine("Para abertura de chamado, selecine o tipo de problema que você quer resolver\n");
            x.MostraTipo();
            var opcao = Console.ReadLine();
            var tipoEscolhido = opcao;
            Console.WriteLine("Digite o motivo do chamado:\n");
            string nota = Console.ReadLine();
            string caminhoNota = @"C:\Users\varlei.060219\Desktop\Nova pasta\poo\ChamadoApp\CHamadoApp\ChamadoApp\chamado\chamado.txt";
            File.AppendAllText(caminhoNota, nota);
            Console.WriteLine("Arquivo salvo com sucesso");
        }

    }
    internal class Tecnico : Pessoa 
    {
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

        public void AtendeChamado() { }
    }
}
