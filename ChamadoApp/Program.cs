using ChamadoApp;

Console.WriteLine("Bem vindo ao sisitema de chamado\n" +
    "Primeiro informe seu nome e setor:");

Console.WriteLine("Nome:");
var nome = Console.ReadLine();

Console.WriteLine("Setor");
var setor = Console.ReadLine();

Console.WriteLine("\nSelecione o tipo de problema que você deseja solucionar\n");

while (true)
{
    Console.WriteLine("1.Software\n2.Hardware\n3.Outros\n4.Sair do Programa\n");

    var selecao = Console.ReadLine();

    if (selecao != "1" && selecao != "2" && selecao != "3" && selecao != "4")
    {
        Console.WriteLine("opção invalida");
        break;
    }

    switch (selecao)
    {
        case "1":
            Console.WriteLine("\nRelate o problema do chamado:");
            var descricaoUm = Console.ReadLine();
            var chamado = $"Solução para: Software\n" +
                $"Data e Hora:{DateTime.Now}\nFuncionario:{nome}\nSetor:{setor}\n{descricaoUm}\n";
            string converteChamado = $"chamado_{DateTime.Now:dd-MM-yyyy_HHmmss}.txt";
            File.WriteAllText(converteChamado, chamado);
            break;
        case "2":
            Console.WriteLine("\nRelate o problema do chamado:");
            var descricaoDois = Console.ReadLine();
            var chamadoDois = $"Solução para: Hardware\n" +
                $"Data e Hora:{DateTime.Now}\nFuncionario:{nome}\nSetor:{setor}\n{descricaoDois}\n";
            string converteChamadoDois = $"chamado_{DateTime.Now:dd-MM-yyyy_HHmmss}.txt";
            File.WriteAllText(converteChamadoDois, chamadoDois);
            break;
        case "3":
            Console.WriteLine("\nRelate o problema do chamado:");
            var descricaoTres = Console.ReadLine();
            var chamadoTres = $"Solução para: Outos\n" +
                $"Data e Hora:{DateTime.Now}Funcionario:{nome}\nSetor:{setor}\n{descricaoTres}";
            string converteChamadoTres = $"chamado_{DateTime.Now:dd-MM-yyyy_HHmmss}.txt";
            File.WriteAllText(converteChamadoTres, chamadoTres);
            break;
        case "4":
            Console.WriteLine("\nVoce esta saindo do programa...");
            return;
        default:
            Console.WriteLine("\nOpção invalida, tente novamente");
            break;
    }
}