using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Gestor_Projeto_2
{
    class Program
    {   
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();

        enum Menu { Listagem = 1, Adicionar, Remover, Sair}

        static void Main(string[] args)
        {
            Carregar();

            bool escolheuSair = false;

            while (!escolheuSair)
            {
                Console.WriteLine("Sistema de Clientes - Bem Vindo!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int intOp = int.Parse(Console.ReadLine());

                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Listagem:
                        Listagem();
                        break;

                    case Menu.Adicionar:
                        Adicionar();
                        break;

                    case Menu.Remover:
                        Remover();
                        break;

                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();
            }

        }


        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de Cliente: ");

            Console.WriteLine("Qual nome do Cliente: ");
            cliente.nome = Console.ReadLine();

            Console.WriteLine("Qual Email do Cliente: ");
            cliente.email = Console.ReadLine();

            Console.WriteLine("Qual CPF do cliente: ");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro Concluido, aperte Enter para Sair");
            Console.ReadLine();
        }

        static void Listagem()
        {
            if(clientes.Count > 0)
            {
                Console.WriteLine("Listagem de Clientes: ");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("====================================");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado!");
            }
            
            Console.WriteLine("Aperte enter para Sair.");
            Console.ReadLine();
        }

        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite ID do Cliente para Remoção: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();

            }
            else
            {
                Console.WriteLine("ID INVÁLIDO");
                Console.ReadLine();
            }
        }

        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, clientes);

            stream.Close();

        }

        static void Carregar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);

            try
            { 
                BinaryFormatter enconder = new BinaryFormatter();

                clientes = (List<Cliente>)enconder.Deserialize(stream);
                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }

            }

            catch(Exception e)
            {
                
                clientes = new List<Cliente>();

            }

            stream.Close();
        }
    }
}
