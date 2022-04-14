using E_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.ConsoleApp.ModuloContato
{
    public class Contato : EntidadeBase
    {
        private readonly string nome;
        private readonly string telefone;
        private readonly string email;
        private readonly string empresa;
        private readonly string cargo;

        public string Nome { get => nome; }
        public string Telefone { get => telefone; }
        public string Email { get => email; }
        public string Empresa { get => empresa; }
        public string Cargo { get => cargo; }

        public Contato(string nome, string telefone, string email, string empresa, string cargo)
        {
            this.nome = nome;
            this.telefone = telefone;
            this.email = email;
            this.empresa = empresa;
            this.cargo = cargo;
        }

        public override string Validar()
        {
            string validacao = "";

            if (Telefone.Length <= 9)
                validacao += "O telefone não pode conter mais de 9 digitos!\n";
            if (Email.Length > 0 )
                validacao += "O email não pode ser vazio!\n";

            if (string.IsNullOrEmpty(validacao))
                return "REGISTRO_VALIDO";

            return validacao;
        }
        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                   "Nome: " + Nome + Environment.NewLine +
                   "Telefone: " + Telefone + Environment.NewLine +
                   "E-Mail: " + Email + Environment.NewLine +
                   "Empresa: " + Empresa + Environment.NewLine +
                   "Cargo: " + Cargo + Environment.NewLine;
        }
    }
}
