using System;
using FisioApp.Services;

namespace FisioApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Inicializa o sistema (carrega dados do JSON)
            PatientManager.Init();

            while (true)
            {
                Console.WriteLine("=== Sistema de Gerenciamento de Pacientes ===");
                Console.WriteLine("1. Sair");
                Console.WriteLine("2. Cadastrar Paciente");
                Console.WriteLine("3. Registrar Sessão");
                Console.WriteLine("4. Editar Paciente");
                Console.WriteLine("5. Excluir Paciente");
                Console.WriteLine("6. Listar Pacientes");
                Console.WriteLine("7. Visualizar Histórico de Sessões");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine()?.Trim();

                switch (opcao)
                {
                    case "1":
                        Console.WriteLine("Saindo... Até logo!");
                        return;

                    case "2":
                        PatientManager.CadastrarPaciente();
                        break;

                    case "3":
                        PatientManager.RegistrarSessao();
                        break;

                    case "4":
                        PatientManager.EditarPaciente();
                        break;

                    case "5":
                        PatientManager.ExcluirPaciente();
                        break;

                    case "6":
                        PatientManager.ListarPacientes();
                        break;

                    case "7":
                        PatientManager.VisualizarHistorico();
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.\n");
                        break;
                }
            }
        }
    }
}
