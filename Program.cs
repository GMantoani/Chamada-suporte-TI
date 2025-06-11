using System;
using System.Collections.Generic;

class Program                //Etapa 3 Vetor para Classificar o tipo dos problemas
{
    static Queue<string> filaChamadas = new Queue<string>();
    static Stack<string> historicoResolvidos = new Stack<string>();
    static string[] tiposProblemas = { "Rede", "Hardware", "Software", "Acesso", "Outro" };
    static string[,] matrizChamadas = new string[10, 3];
    static int totalChamadas = 0;

    static void Main()
    {
        bool executando = true;
        // Introdução Menu de enterração  
        while (executando)
        {
            Console.WriteLine("\n SUPORTE TÉCNICO ");
            Console.WriteLine("1 - Adicionar nova chamada");
            Console.WriteLine("2 - Listar todas as chamadas");
            Console.WriteLine("3 - Ordenar por prioridade");
            Console.WriteLine("4 - Atender próxima chamada");
            Console.WriteLine("5 - Ver histórico de resoluções");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    AdicionarNovaChamada();
                    break;
                case "2":
                    ListarChamadas();
                    break;
                case "3":
                    OrdenarChamadasPorPrioridade();
                    Console.WriteLine("Chamadas ordenadas com sucesso.");
                    break;
                case "4":
                    AtenderProximaChamada();
                    break;
                case "5":
                    ExibirHistorico();
                    break;
                case "0":
                    executando = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void AdicionarNovaChamada()
    {
        if (totalChamadas >= 10)
        {
            Console.WriteLine("Limite de chamadas atingido.");
            return;
        }

        Console.Write("Nome do Cliente: ");
        string cliente = Console.ReadLine();

        Console.WriteLine("Tipo do problema:");
        for (int i = 0; i < tiposProblemas.Length; i++)
        {
            Console.WriteLine($"{i} - {tiposProblemas[i]}");
        }
        Console.Write("Escolha o número correspondente: ");
        int tipoIndex = int.Parse(Console.ReadLine());

        Console.Write("Prioridade (Alta / Média / Baixa): ");
        string prioridade = Console.ReadLine();

        string problema = tiposProblemas[tipoIndex];
        matrizChamadas[totalChamadas, 0] = cliente;
        matrizChamadas[totalChamadas, 1] = problema;
        matrizChamadas[totalChamadas, 2] = prioridade;
        filaChamadas.Enqueue(cliente);
        totalChamadas++;

        Console.WriteLine("Chamada adicionada com sucesso.");
    }
    //Etapa 4 Matriz para detalhar a chamada
    static void ListarChamadas()
    {
        if (totalChamadas == 0)
        {
            Console.WriteLine("Nenhuma chamada registrada.");
            return;
        }

        Console.WriteLine("\nChamadas Ativas:");
        for (int i = 0; i < totalChamadas; i++)
        {
            if (matrizChamadas[i, 2] != "Resolvido")
            {
                Console.WriteLine($"{matrizChamadas[i, 0]} - {matrizChamadas[i, 1]} - Prioridade: {matrizChamadas[i, 2]}");
            }
        }
    }

    static int PrioridadeParaNumero(string prioridade)
    {
        switch (prioridade)
        {
            case "Alta": return 1;
            case "Média": return 2;
            case "Baixa": return 3;
            default: return 4;
        }
    }
    //Etapa 5 Ordenação para Prioridades dos Chamados
    static void OrdenarChamadasPorPrioridade()
    {
        for (int i = 0; i < totalChamadas - 1; i++)
        {
            for (int j = i + 1; j < totalChamadas; j++)
            {
                int prioridadeI = PrioridadeParaNumero(matrizChamadas[i, 2]);
                int prioridadeJ = PrioridadeParaNumero(matrizChamadas[j, 2]);

                if (prioridadeI > prioridadeJ)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        string temp = matrizChamadas[i, k];
                        matrizChamadas[i, k] = matrizChamadas[j, k];
                        matrizChamadas[j, k] = temp;
                    }
                }
            }
        }
        //// Etapa 1 Fila para Organização de chamada
        filaChamadas.Clear();
        for (int i = 0; i < totalChamadas; i++)
        {
            if (matrizChamadas[i, 2] != "Resolvido")
            {
                filaChamadas.Enqueue(matrizChamadas[i, 0]);
            }
        }
    }

    static void AtenderProximaChamada()
    {
        if (filaChamadas.Count == 0)
        {
            Console.WriteLine("Nenhuma chamada na fila.");
            return;
        }

        string cliente = filaChamadas.Dequeue();

        for (int i = 0; i < totalChamadas; i++)
        {
            if (matrizChamadas[i, 0] == cliente && matrizChamadas[i, 2] != "Resolvido")
            {
                matrizChamadas[i, 2] = "Resolvido";
                historicoResolvidos.Push($"{cliente} - {matrizChamadas[i, 1]}");
                Console.WriteLine($"Chamada de {cliente} resolvida.");
                return;
            }
        }
    }
    //Etapa 2 Pilha para Histórico de chamadas
    static void ExibirHistorico()
    {
        if (historicoResolvidos.Count == 0)
        {
            Console.WriteLine("Nenhum problema resolvido ainda.");
            return;
        }

        Console.WriteLine("\nHistórico de Problemas Resolvidos:");
        foreach (var item in historicoResolvidos)
        {
            Console.WriteLine(item);
        }
    }
}
