using E_Agenda.ConsoleApp.ModuloTarefa;
using E_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.ConsoleApp.ModuloItem
{
    public class Item : EntidadeBase
    {
        private readonly Tarefa tarefa;
        private readonly string descricao;
        private readonly int status;

        public Item(Tarefa tarefa, string descricao, int status)
        {
            this.tarefa = tarefa;
            this.descricao = descricao;
            this.status = status;
        }

        public Tarefa Tarefa { get => tarefa; }
        public string Descricao { get => descricao; }
        public int Status { get => status; }

        private string ValidarStatus()
        {
            string status = "";
            if (Status == 1)
                status = "PENDENTE";
            else if (Status == 2)
                status = "CONCLUIDO";

            return status;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                   "Tarefa: " + Tarefa.Titulo + Environment.NewLine +
                   "Descrição: " + Descricao + Environment.NewLine +
                   "Status: " + ValidarStatus() + Environment.NewLine;
        }
    }
}
