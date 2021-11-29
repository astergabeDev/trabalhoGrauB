using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace trabalho_grau_b
{
    public class Sistema
    {
        bool menuV = true;
        Queue<QueueItem> lista;
        int pidAtual;

        public Sistema()
        {
            lista = new Queue<QueueItem>();
            lista.Clear();
            pidAtual = 0;
        }

        public void menu()
        {
            int escolha;
            while (menuV)
            {
                Console.Clear();
                Console.WriteLine("Menu:");
                Console.WriteLine("");
                Console.WriteLine("1 - Criar processo");
                Console.WriteLine("2 - Executar proximo");
                Console.WriteLine("3 - Executar processo especifico");
                Console.WriteLine("4 - Salvar fila de processos");
                Console.WriteLine("5 - Carregar do arq a fila de processos");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("");
                Console.WriteLine("Insira a opção desejada: ");
                escolha = int.Parse(Console.ReadLine());

                switch (escolha)
                {
                    case 1:
                        criarProcesso();
                        break;
                    case 2:
                        executarProximo();
                        Console.ReadKey();
                        break;
                    case 3:
                        executarProcEsp();
                        Console.ReadKey();
                        break;
                    case 4:
                        salvarFila();
                        Console.ReadKey();
                        break;
                    case 5:
                        carregarArq();
                        Console.ReadKey();
                        break;
                    case 0:
                        menuV = false;
                        break;
                    default:
                        System.Console.WriteLine("Insira uma opção valida! ");
                        break;
                }
            }
        }

        public void criarProcesso()
        {
            int escolha;
            Console.Clear();
            Console.WriteLine("1 - Calculo");
            Console.WriteLine("2 - Escrita");
            Console.WriteLine("3 - Leitura");
            Console.WriteLine("4 - Impressão");
            Console.WriteLine("");
            Console.WriteLine("Insira a opção desejada: ");
            escolha = int.Parse(Console.ReadLine());

            switch (escolha)
            {
                case 1:
                    Console.WriteLine("Insira a operaçao: ");
                    char op = char.Parse(Console.ReadLine());
                    Console.WriteLine("Insira o primeiro operando: ");
                    float op1 = float.Parse(Console.ReadLine());
                    Console.WriteLine("Insira o segundo operando: ");
                    float op2 = float.Parse(Console.ReadLine());
                    lista.Enqueue(new QueueItem("Calculo", pidAtual, new ComputingProcess(pidAtual, op1, op2, op)));
                    break;
                case 2:
                    Console.WriteLine("Insira a operaçao: ");
                    char opExp = char.Parse(Console.ReadLine());
                    Console.WriteLine("Insira o primeiro operando: ");
                    float opExp1 = float.Parse(Console.ReadLine());
                    Console.WriteLine("Insira o segundo operando: ");
                    float opExp2 = float.Parse(Console.ReadLine());
                    string exp = opExp1.ToString() + " " + opExp + " " +  opExp2.ToString();
                    lista.Enqueue(new QueueItem("Escrita", pidAtual, new WritingProcess(pidAtual, exp)));
                    break;
                case 3:
                    lista.Enqueue(new QueueItem("Leitura", pidAtual, new ReadingProcess(pidAtual)));
                    break;
                case 4:
                    lista.Enqueue(new QueueItem("Impressao", pidAtual, new PrintingProcess(pidAtual, lista)));
                    break;
                default:
                    System.Console.WriteLine("Insira uma opção valida! ");
                    break;
            }
            System.Console.WriteLine("Processo PID: "+pidAtual+" criado com sucesso");
            pidAtual++;
            Console.ReadKey();
        }
        public void executarProximo()
        {
            if(lista.Count == 0)
            {
                System.Console.WriteLine("Fila vazia");
                return;
            }
            QueueItem var;
            var = lista.Dequeue();
            var.process.execute();
            
        }
        public void executarProcEsp()
        {
            Console.WriteLine("Insira o id: ");
            int id = int.Parse(Console.ReadLine());
            QueueItem procSelec = null;
            foreach (QueueItem queueItem in lista)
            {
                if (queueItem.pid == id)
                {
                    procSelec = queueItem;
                    break;
                }
            }
            if (procSelec != null)
            {
                procSelec.process.execute();
                lista = new Queue<QueueItem>(lista.Where((value,index) => value.pid!=id));               
            }
            else
            {
                System.Console.WriteLine("Processo não encontrado");
            }
        }
        public void salvarFila()
        {
            string conteudoProc = "";
            StreamWriter arquivoTextFila;
            arquivoTextFila = File.CreateText("Arquivo.txt");

            foreach (var item in lista)
            { 
                switch (item.tipoProc)
                {
                    case "Calculo":
                        conteudoProc = "|" + item.process.operacao.ToString() + " " + item.process.op1.ToString() + " " + item.process.op2.ToString();
                        break;
                    case "Escrita":
                        
                        conteudoProc = "|" + string.Join(" ",item.process.exp.Split("|"));
                        break;
                    default:
                        conteudoProc = "";
                        break;
                }
                
                string texto = item.pid.ToString() + "|" + item.tipoProc + conteudoProc;
                arquivoTextFila.WriteLine(texto);
                System.Console.WriteLine(texto);
            }
            arquivoTextFila.Close(); 
        }
        public void carregarArq()
        {
            string[] cLines = System.IO.File.ReadAllLines("Arquivo.txt");

            QueueItem[] queueItem = new QueueItem[cLines.Length];

            //System.Console.WriteLine(cLines.Length); teste

            for(int i = 0; i < cLines.Length; i++)
            {
                string[] processoStr = cLines[i].Split("|");
                //System.Console.WriteLine(processoStr[1]); teste
                switch (processoStr[1])
                {
                    case "Calculo":
                        if (processoStr[2] != null)
                        {
                            string[] expStr = processoStr[2].Split(" ");
                            queueItem[i] = new QueueItem(processoStr[1],pidAtual, new ComputingProcess(pidAtual, float.Parse(expStr[1]), float.Parse(expStr[2]), char.Parse(expStr[0])));
                            lista.Enqueue(queueItem[i]);
                            pidAtual++;
                        }
                        break;
                    case "Escrita":
                        if (processoStr[2] != null)
                        {
                            queueItem[i] = new QueueItem(processoStr[1],pidAtual, new WritingProcess(pidAtual, processoStr[2]));
                            lista.Enqueue(queueItem[i]);
                            pidAtual++;
                        }
                        break;
                    case "Leitura":
                        queueItem[i] = new QueueItem(processoStr[1],pidAtual, new ReadingProcess(pidAtual));
                        lista.Enqueue(queueItem[i]);
                        pidAtual++;
                        break;
                    default:
                        queueItem[i] = new QueueItem(processoStr[1],pidAtual, new PrintingProcess(pidAtual, lista));
                        lista.Enqueue(queueItem[i]);
                        pidAtual++;
                        break;
                }
            }
        }
    }    
}