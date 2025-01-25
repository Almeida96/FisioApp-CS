using System;
using System.Linq;
using System.Collections.Generic;
using FisioApp.Models;

namespace FisioApp.Services
{
    /// <summary>
    /// Classe responsável pelas operações de CRUD no sistema (pacientes, sessões etc.)
    /// </summary>
    public static class PatientManager
    {
        private static PacientesRoot _dados = new PacientesRoot();

        /// <summary>
        /// Inicializa o sistema carregando dados do arquivo JSON.
        /// </summary>
        public static void Init()
        {
            _dados = DataService.CarregarDados();
        }

        /// <summary>
        /// Salva os dados no arquivo JSON.
        /// </summary>
        private static void Save()
        {
            DataService.SalvarDados(_dados);
        }

        /// <summary>
        /// Retorna o menor ID disponível, partindo de 1.
        /// Ex.: se já existem os IDs [2,3,4], o próximo será 1;
        /// se existem [1,2,3], o próximo será 4.
        /// </summary>
        private static int GerarProximoId()
        {
            int candidato = 1;
            while (true)
            {
                bool existe = _dados.Pacientes.Any(p => p.Id == candidato);
                if (!existe)
                    return candidato;

                candidato++;
            }
        }

        /// <summary>
        /// Cadastra um novo paciente, gerando automaticamente o ID.
        /// </summary>
        public static void CadastrarPaciente()
        {
            Console.WriteLine("\n--- Cadastrar Paciente ---");

            // Gera automaticamente o menor ID disponível
            int novoId = GerarProximoId();

            Console.Write("Digite o nome do paciente: ");
            var nome = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome inválido.\n");
                return;
            }

            Console.Write("Digite a idade do paciente: ");
            if (!int.TryParse(Console.ReadLine(), out var idade))
            {
                Console.WriteLine("Idade inválida! Use apenas números.\n");
                return;
            }

            Console.Write("Digite o diagnóstico do paciente: ");
            var diagnostico = Console.ReadLine() ?? "";

            var novoPaciente = new Paciente
            {
                Id = novoId,
                Nome = nome.Trim(),
                Idade = idade,
                Diagnostico = diagnostico.Trim()
            };

            _dados.Pacientes.Add(novoPaciente);
            Save();
            Console.WriteLine($"Paciente '{nome}' cadastrado com sucesso! (ID {novoId})\n");
        }

        public static void RegistrarSessao()
        {
            Console.WriteLine("\n--- Registrar Sessão ---");
            Console.Write("Digite o ID do paciente: ");
            if (!int.TryParse(Console.ReadLine(), out var pacienteId))
            {
                Console.WriteLine("ID inválido!\n");
                return;
            }

            var paciente = _dados.Pacientes.Find(p => p.Id == pacienteId);
            if (paciente == null)
            {
                Console.WriteLine("Paciente não encontrado.\n");
                return;
            }

            Console.Write("Digite as observações da sessão: ");
            var observacoes = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(observacoes))
            {
                Console.WriteLine("Observação vazia. Sessão não registrada.\n");
                return;
            }

            var sessao = new HistoricoSessao { Observacoes = observacoes.Trim() };
            paciente.HistoricoSessoes.Add(sessao);

            Save();
            Console.WriteLine("Sessão registrada com sucesso!\n");
        }

        public static void EditarPaciente()
        {
            Console.WriteLine("\n--- Editar Paciente ---");
            Console.Write("Digite o ID do paciente que deseja editar: ");
            if (!int.TryParse(Console.ReadLine(), out var pacienteId))
            {
                Console.WriteLine("ID inválido!\n");
                return;
            }

            var paciente = _dados.Pacientes.Find(p => p.Id == pacienteId);
            if (paciente == null)
            {
                Console.WriteLine("Paciente não encontrado.\n");
                return;
            }

            Console.WriteLine($"Editando paciente: {paciente.Nome}");

            Console.Write($"Novo nome (deixe em branco para manter '{paciente.Nome}'): ");
            var novoNome = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoNome))
            {
                paciente.Nome = novoNome.Trim();
            }

            Console.Write($"Nova idade (deixe em branco para manter {paciente.Idade}): ");
            var novaIdadeStr = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novaIdadeStr))
            {
                if (int.TryParse(novaIdadeStr, out var novaIdade))
                {
                    paciente.Idade = novaIdade;
                }
                else
                {
                    Console.WriteLine("Idade inválida! Edição cancelada.\n");
                    return;
                }
            }

            Console.Write($"Novo diagnóstico (deixe em branco para manter '{paciente.Diagnostico}'): ");
            var novoDiagnostico = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoDiagnostico))
            {
                paciente.Diagnostico = novoDiagnostico.Trim();
            }

            Save();
            Console.WriteLine("Dados atualizados com sucesso!\n");
        }

        public static void ExcluirPaciente()
        {
            Console.WriteLine("\n--- Excluir Paciente ---");
            Console.Write("Digite o ID do paciente que deseja excluir: ");
            if (!int.TryParse(Console.ReadLine(), out var pacienteId))
            {
                Console.WriteLine("ID inválido!\n");
                return;
            }

            var paciente = _dados.Pacientes.Find(p => p.Id == pacienteId);
            if (paciente == null)
            {
                Console.WriteLine("Paciente não encontrado.\n");
                return;
            }

            Console.Write($"Tem certeza que deseja excluir o paciente {paciente.Nome}? (S/N): ");
            var confirmacao = Console.ReadLine()?.Trim().ToLower();
            if (confirmacao == "s")
            {
                _dados.Pacientes.Remove(paciente);
                Save();
                Console.WriteLine("Paciente excluído com sucesso!\n");
            }
            else
            {
                Console.WriteLine("Operação cancelada.\n");
            }
        }

        /// <summary>
        /// Lista todos os pacientes, ordenados pelo ID (crescente).
        /// </summary>
        public static void ListarPacientes()
        {
            Console.WriteLine("\n--- Lista de Pacientes ---");

            if (_dados.Pacientes.Count == 0)
            {
                Console.WriteLine("Não há pacientes cadastrados.\n");
                return;
            }

            // Ordena por ID
            var pacientesOrdenados = _dados.Pacientes
                                           .OrderBy(p => p.Id)
                                           .ToList();

            foreach (var p in pacientesOrdenados)
            {
                Console.WriteLine($"ID: {p.Id}, Nome: {p.Nome}, Idade: {p.Idade}, Diagnóstico: {p.Diagnostico}");
            }
            Console.WriteLine();
        }

        public static void VisualizarHistorico()
        {
            Console.WriteLine("\n--- Visualizar Histórico de Sessões ---");
            Console.Write("Digite o ID do paciente: ");
            if (!int.TryParse(Console.ReadLine(), out var pacienteId))
            {
                Console.WriteLine("ID inválido!\n");
                return;
            }

            var paciente = _dados.Pacientes.Find(p => p.Id == pacienteId);
            if (paciente == null)
            {
                Console.WriteLine("Paciente não encontrado.\n");
                return;
            }

            Console.WriteLine($"\nHistórico de Sessões do(a) {paciente.Nome}:");

            if (paciente.HistoricoSessoes.Count == 0)
            {
                Console.WriteLine("Nenhuma sessão registrada.\n");
            }
            else
            {
                for (int i = 0; i < paciente.HistoricoSessoes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {paciente.HistoricoSessoes[i].Observacoes}");
                }
                Console.WriteLine();
            }
        }
    }
}
