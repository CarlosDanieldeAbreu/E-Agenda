using E_Agenda.ConsoleApp.ModuloContato;
using E_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_Agenda.ConsoleApp.ModuloCompromisso
{
    public class Compromisso : EntidadeBase
    {
        private readonly DateTime horaInicio;
        private readonly DateTime horaTermino;
        private readonly DateTime dataCompromisso;
        private readonly string assunto;
        private readonly string local;
        private readonly Contato contato;

        public DateTime HoraInicio { get => horaInicio;}
        public DateTime HoraTermino { get => horaTermino;}
        public DateTime DataCompromisso { get => dataCompromisso;}
        public string Assunto { get => assunto;}
        public Contato Contato { get => contato;}
        public string Local { get => local;}

        public Compromisso(DateTime horaInicio, DateTime horaTermino, DateTime dataCompromisso, string assunto, string local, Contato contato)
        {
            this.horaInicio = horaInicio;
            this.horaTermino = horaTermino;
            this.dataCompromisso = dataCompromisso;
            this.assunto = assunto;
            this.local = local;
            this.contato = contato;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                   "Data de inicio: " + HoraInicio.ToString("HH:mm") + Environment.NewLine +
                   "Data de termino: " + HoraTermino.ToString("HH:mm") + Environment.NewLine +
                   "Data do Compromisso: " + DataCompromisso.ToString("dd/MM/yyyy") + Environment.NewLine +
                   "Assunto: " + Assunto + Environment.NewLine +
                   "Local: " + Local + Environment.NewLine +
                   "Contato: " + Contato.Nome + Environment.NewLine;
        }
    }
}
