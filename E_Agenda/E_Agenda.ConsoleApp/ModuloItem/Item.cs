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
        public int status;
        private readonly double percentualItem;

        public Item(Tarefa tarefa, string descricao, double percentualItem)
        {
            this.tarefa = tarefa;
            this.descricao = descricao;
            this.percentualItem = percentualItem;
        }

        public Tarefa Tarefa { get => tarefa; }
        public string Descricao { get => descricao; }
        public double PercentualItem { get => percentualItem; }

        public int RetornaStatus()
        {
            if (PercentualItem < 100)
                status = 1;
            else if (PercentualItem == 100)
                status = 2;

            return status;
        }

        private string AplicarMaskStatus()
        {
            string status = "";
            if (RetornaStatus() == 1)
                status = "PENDENTE";
            else if (RetornaStatus() == 2)
                status = "CONCLUIDO";

            return status;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                   "Tarefa: " + Tarefa.Titulo + Environment.NewLine +
                   "Descrição: " + Descricao + Environment.NewLine +
                   "Percentual: " + PercentualItem + "%" + Environment.NewLine +
                   "Status: " + AplicarMaskStatus() + Environment.NewLine;
        }

        public override string Validar()
        {
            throw new NotImplementedException();
        }
    }
}
