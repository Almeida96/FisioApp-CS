using System.Collections.Generic;

namespace FisioApp.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public string Diagnostico { get; set; } = string.Empty;

        public List<HistoricoSessao> HistoricoSessoes { get; set; } = new List<HistoricoSessao>();
    }
}
