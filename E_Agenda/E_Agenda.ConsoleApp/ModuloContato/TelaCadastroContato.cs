using E_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.ConsoleApp.ModuloContato
{
    public class TelaCadastroContato : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Contato> repositorioContato;
        private readonly Notificador notificador;

        public TelaCadastroContato(IRepositorio<Contato> repositorioContato, Notificador notificador) : base("Cadastro de contato")
        {
            this.repositorioContato = repositorioContato;
            this.notificador = notificador;
        }

        public void Editar()
        {
            MostrarTitulo("Editando Contato");

            bool temContatosCadastrados = VisualizarRegistros("Pesquisando");

            if (temContatosCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhum contato cadastrado para poder editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroContato = ObterNumeroContato();

            Contato contatoAtualizado = ObterContato();

            bool conseguiuEditar = repositorioContato.Editar(numeroContato, contatoAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Conatato editado com sucesso", TipoMensagem.Sucesso);
        }

        private int ObterNumeroContato()
        {
            int numeroContato;
            bool numeroContatoEncontrado;

            do
            {
                Console.Write("Digite o número do contato que deseja editar: ");
                numeroContato = Convert.ToInt32(Console.ReadLine());

                numeroContatoEncontrado = repositorioContato.ExisteRegistro(numeroContato);

                if (numeroContatoEncontrado == false)
                    notificador.ApresentarMensagem("Número do Contato não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroContatoEncontrado == false);
            return numeroContato;
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Contato");

            bool temContatosCadastradas = VisualizarRegistros("Pesquisando");

            if (temContatosCadastradas == false)
            {
                notificador.ApresentarMensagem("Nenhum Contato cadastrado para poder editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroContato = ObterNumeroContato();

            bool conseguiuExcluir = repositorioContato.Excluir(numeroContato);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Contato excluido com sucesso", TipoMensagem.Sucesso);
        }

        public void Inserir()
        {
            MostrarTitulo("Inserindo novo Contato");

            Contato novoContato = ObterContato();
            string statusValidacao = repositorioContato.Inserir(novoContato);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Contato cadastrado com sucesso", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        private Contato ObterContato()
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();
            Console.Write("Digite o telefone: ");
            string telefone = Console.ReadLine();
            Console.Write("Digite o e-mail: ");
            string email = Console.ReadLine();
            Console.Write("Digite o empresa: ");
            string empresa = Console.ReadLine();
            Console.Write("Digite o cargo: ");
            string cargo = Console.ReadLine();

            return new Contato(nome, telefone, email, empresa, cargo);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Contatos");

            List<Contato> contatos = repositorioContato.SelecionarTodos();

            if (contatos.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhum Contato disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Contato contato in contatos)
                Console.WriteLine(contato.ToString());

            Console.ReadLine();

            return true;
        }
    }
}
