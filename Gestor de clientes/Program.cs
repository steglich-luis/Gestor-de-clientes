using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_de_clientes
{
    internal class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();

        enum Menu { Listagem = 1, Adicionar, Remover, Sair }
        static void Main(string[] args)
        {
            Carregar();
            bool Escolheusair = false;
            while (!Escolheusair)
            {
                Console.WriteLine("Gestor de clientes, seja bem-vindo!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int intop = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intop;
                switch (opcao)
                {
                    case Menu.Listagem:
                        ListarClientes();
                        break;
                    case Menu.Adicionar:
                        AdicionarCliente();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        Escolheusair = true;
                        break;
                }
                Console.Clear();
            }
        }

        static void AdicionarCliente()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Digite o nome do cliente:");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Digite o email do cliente:");
            cliente.email = Console.ReadLine();
            Console.WriteLine("Digite o CPF do cliente:");
            cliente.cpf = Console.ReadLine();
            clientes.Add(cliente);
            Salvar();
            Console.WriteLine("Cliente adicionado com sucesso!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadLine();
        }

        static void ListarClientes()
        {
            if (clientes.Count > 0)
            {
                Console.WriteLine("Lista de clientes:");
                foreach (Cliente cliente in clientes)
                {
                    int i = 0;
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}\nEmail: {cliente.email}\nCPF: {cliente.cpf}");
                    Console.WriteLine("================================");
                    i++;
                }
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadLine();
            }
        }

        static void Remover()
        {
            ListarClientes();
            Console.WriteLine("Digite o ID do cliente que deseja remover:");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("ID digitado é inválido, tente novamente!");
                Console.ReadLine();
            }
        }
        static void Salvar()
        {
            FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();
            encoder.Serialize(stream, clientes);
            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter encoder = new BinaryFormatter();
                clientes = (List<Cliente>)encoder.Deserialize(stream);
                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }
                
            }
            catch (Exception e)
            {                 
                clientes = new List<Cliente>();
            }
            stream.Close();

        }

    }
}
