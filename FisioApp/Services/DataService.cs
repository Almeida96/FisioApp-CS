using System;
using System.IO;
using System.Text.Json;
using FisioApp.Models;

namespace FisioApp.Services
{
    public static class DataService
    {
        private const string ARQUIVO_JSON = "pacientes.json";

        /// <summary>
        /// Carrega dados do arquivo JSON "pacientes.json".
        /// Se não existir ou estiver vazio, retorna uma nova instância (sem pacientes).
        /// </summary>
        public static PacientesRoot CarregarDados()
        {
            if (!File.Exists(ARQUIVO_JSON))
            {
                return new PacientesRoot();
            }

            try
            {
                string json = File.ReadAllText(ARQUIVO_JSON);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new PacientesRoot();
                }

                var dados = JsonSerializer.Deserialize<PacientesRoot>(json);
                return dados ?? new PacientesRoot();
            }
            catch
            {
                Console.WriteLine("Falha ao ler o arquivo JSON. Iniciando com dados vazios.");
                return new PacientesRoot();
            }
        }

        /// <summary>
        /// Salva dados no arquivo JSON "pacientes.json" com indentação.
        /// </summary>
        public static void SalvarDados(PacientesRoot dados)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(dados, options);
                File.WriteAllText(ARQUIVO_JSON, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar dados: " + ex.Message);
            }
        }
    }
}
