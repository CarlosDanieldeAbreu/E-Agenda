using E_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.ConsoleApp.ModuloTarefa
{
    public class Tarefa : EntidadeBase
    {
        private readonly string titulo;
        private readonly DateTime dataDeCriacao;
        private readonly DateTime dataDeConclusao;
        private readonly double percentualConcluido;
        private readonly int prioridade;

        public string Titulo { get => titulo; }
        public DateTime DataDeCriacao { get => dataDeCriacao; }
        public DateTime DataDeConclusao { get => dataDeConclusao; }
        public double PercentualConcluido { get => percentualConcluido; }
        public int Prioridade { get => prioridade; } 

        public Tarefa(string titulo, DateTime dataDeCriacao, DateTime dataDeConclusao, double percentualConcluido, int prioridade)
        {
            this.titulo = titulo;
            this.dataDeCriacao = dataDeCriacao;
            this.dataDeConclusao = dataDeConclusao;
            this.percentualConcluido = percentualConcluido;
            this.prioridade = prioridade;
        }
        private string ValidarPrioridade()
        {
            string prioridade = "";

            if (Prioridade == 1)
                prioridade = "BAIXA";
            else if (Prioridade == 2)
                prioridade = "NORMAL";
            else if (Prioridade == 3)
                prioridade = "ALTA";

            return prioridade;
        }

        public enum CategoriasDaTarefa
        {
            Baixa, Normal, Alta
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                   "Título: " + Titulo + Environment.NewLine +
                   "Data de criação: " + DataDeCriacao.ToString("dd/MM/yyyy") + Environment.NewLine +
                   "Data de conclusão: " + DataDeConclusao.ToString("dd/MM/yyyy") + Environment.NewLine +
                   "Percentual concluido: " + PercentualConcluido + "%" + Environment.NewLine +
                   "Prioridade: " + ValidarPrioridade() + Environment.NewLine;
        }

        public override string Validar()
        {
            throw new NotImplementedException();
        }
    }
}
