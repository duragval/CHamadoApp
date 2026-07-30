using System;
using System.Collections.Generic;
using System.Text;

namespace ChamadoApp
{
    internal class Menu
    {
        private readonly ChamadoService _service = new ChamadoService();
        private readonly string[] _tiposProblema = { "Software", "Hardware", "Outros" };

        public void Executar()
        {
            Console.WriteLine("Bem vindo ao sistema de chamado\n");

            while (true)
            {
                Console.WriteLine("1.Funcionario\n2.Técnico\n3.Sair");
                var opcao = Console.ReadLine();

                if (!int.TryParse(opcao, out int selecao))
                {
                    Console.WriteLine("\nOpção inválida, digite um número.\n");
                    continue;
                }

                switch (selecao)
                {
                    case 1:
                        var funcionario = CriarFuncionario();
                        if (funcionario != null)
                        {
                            FluxoFuncionario(funcionario);
                        }
                        break;

                    case 2:
                        var tecnico = CriarTecnico();
                        if (tecnico != null)
                        {
                            FluxoTecnico(tecnico);
                        }
                        break;

                    case 3:
                        Console.WriteLine("\nVocê está saindo do programa...\n");
                        return;

                    default:
                        Console.WriteLine("\nOpção inválida, tente novamente\n");
                        break;
                }
            }
        }

        private Funcionario? CriarFuncionario()
        {
            Console.WriteLine("\nNome:");
            var nome = Console.ReadLine();

            Console.WriteLine("\nNascimento (dd/MM/yyyy):");
            var nascimento = Console.ReadLine();

            Console.WriteLine("\nCPF:");
            var cpf = Console.ReadLine();

            Console.WriteLine("\nDepartamento:");
            var departamento = Console.ReadLine();

            Console.WriteLine("\nId:");
            var idTExto = Console.ReadLine();

            if (!int.TryParse(idTExto, out int id))
            {
                Console.WriteLine("\nId inválido\n");
                return null;
            }

            try
            {
                return new Funcionario(id, nome, nascimento, cpf, departamento);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nErro ao cadastrar funcionário: {ex.Message}");
                return null;
            }
        }

        private Tecnico? CriarTecnico()
        {
            Console.WriteLine("\nNome:");
            var nome = Console.ReadLine();

            Console.WriteLine("\nNascimento (dd/MM/yyyy):");
            var nascimento = Console.ReadLine();

            Console.WriteLine("\nCPF:");
            var cpf = Console.ReadLine();

            Console.WriteLine("\nFunção:");
            var funcao = Console.ReadLine();

            Console.WriteLine("\nId:");
            var idTExto = Console.ReadLine();

            if (!int.TryParse(idTExto, out int id))
            {
                Console.WriteLine("\nId inválido\n");
                return null;
            }

            try
            {
                return new Tecnico(id, nome, nascimento, cpf, funcao);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nErro ao cadastrar técnico: {ex.Message}\n");
                return null;
            }
        }

        private void FluxoFuncionario(Funcionario? funcionario)
        {
            while (true)
            {
                Console.WriteLine("\n1.Software\n2.Hardware\n3.Outros\n4.Voltar ao menu principal\n");
                var opcao = Console.ReadLine();

                if (!int.TryParse(opcao, out int selecao))
                {
                    Console.WriteLine("\nOpção inválida, digite um número.\n");
                    continue;
                }

                if (selecao == 4)
                {
                    return;
                }

                if (selecao < 1 || selecao > 3)
                {
                    Console.WriteLine("\nOpção inválida, tente novamente\n");
                    continue;
                }

                var tipo = _tiposProblema[selecao - 1];

                Console.WriteLine("\nRelate o problema do chamado\nAVISO, NÃO USE QUEBRA DE LINHA!\n");
                var descricao = Console.ReadLine();

                Chamado chamado;
                try
                {
                    var proximoId = _service.ObterProximoId();
                    chamado = new Chamado(proximoId, tipo, descricao, funcionario.Nome, funcionario.Departamento);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\nErro ao abrir chamado: {ex.Message}\n");
                    continue;
                }

                Console.WriteLine("\nConfira os dados do chamado:\n");
                Console.WriteLine(chamado.GerarConteudo());

                Console.WriteLine("Confirma a abertura do chamado? (S/N)");
                var resposta = Console.ReadLine();

                if (resposta?.Trim().ToUpper() == "S")
                {
                    try
                    {
                        _service.Salvar(chamado);
                        Console.WriteLine("Chamado salvo com sucesso");
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"\n{ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("\nChamado descartado.");
                }
            }
        }

        private void FluxoTecnico(Tecnico? tecnico)
        {
            while (true)
            {
                var chamados = _service.ListarChamados();

                if (!chamados.Any())
                {
                    Console.WriteLine("\nNenhum chamado registrado ainda\n");
                    return;
                }

                Console.WriteLine("\nChamados registrados\n");
                foreach (var c in chamados)
                {
                    Console.WriteLine($"Id: {c.Id} | Tipo: {c.Tipo} | Status: {c.Status} | Funcionário: {c.NomeFuncionario}");
                }

                Console.WriteLine("\nDigite o Id do chamado que deseja atualizar ( 0 para voltar ao menu principal):");
                var idTexto = Console.ReadLine();

                if (!int.TryParse(idTexto, out int idEscolhido))
                {
                    Console.WriteLine("\nId inválido.\n");
                    continue;
                }

                if (idEscolhido == 0)
                {
                    return;
                }

                var chamado = chamados.FirstOrDefault(chamado => chamado.Id == idEscolhido);

                if (chamado == null)
                {
                    Console.WriteLine("Chamado não encontrado");
                    continue;
                }

                Console.WriteLine("\n1.Em andamento\n2.Resolvido\n");
                var opcaoStatus = Console.ReadLine();

                StatusChamado novoStatus;
                if (opcaoStatus == "1")
                {
                    novoStatus = StatusChamado.Andamento;
                }
                else if (opcaoStatus == "2")
                {
                    novoStatus = StatusChamado.Fechado;
                }
                else
                {
                    Console.WriteLine("\nOpção inválida.\n");
                    continue;
                }

                Console.WriteLine("Comentário (não use quebra de linha / Enter no meio do texto):");
                var comentario = Console.ReadLine();

                Console.WriteLine("\nConfira a atualização:\n");
                Console.WriteLine($"Chamado: {chamado.Id}");
                Console.WriteLine($"Novo status: {novoStatus}");
                Console.WriteLine($"Técnico: {tecnico.Nome}");
                Console.WriteLine($"Comentário: {comentario}\n");

                Console.WriteLine("Confirma a atualização? (S/N)");
                var resposta = Console.ReadLine();

                if (resposta?.Trim().ToUpper() == "S")
                {
                    try
                    {
                        chamado.AtualizarStatus(tecnico.Nome, comentario, novoStatus);
                        _service.Atualizar(chamado);
                        Console.WriteLine("\nChamado atualizado com sucesso.\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nErro ao atualizar chamado: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("\nAtualização descartada\n");
                }
            }
        }
    }
}
