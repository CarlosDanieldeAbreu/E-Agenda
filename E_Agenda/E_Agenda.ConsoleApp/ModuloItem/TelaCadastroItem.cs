using E_Agenda.ConsoleApp.Compartilhado;
using E_Agenda.ConsoleApp.ModuloTarefa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.ConsoleApp.ModuloItem
{
    public class TelaCadastroItem : TelaBase, ITelaCadastravel
    {
        private readonly Notificador notificador;
        private readonly IRepositorio<Tarefa> repositorioTarefa;
        private readonly TelaCadastroTarefa telaCadastroTarefa;
        private readonly IRepositorio<Item> repositorioItem;

        public TelaCadastroItem(Notificador notificador, IRepositorio<Tarefa> repositorioTarefa, TelaCadastroTarefa telaCadastroTarefa, IRepositorio<Item> repositorioItem) : base("Cadastro de Item")
        {
            this.notificador = notificador;
            this.repositorioTarefa = repositorioTarefa;
            this.telaCadastroTarefa = telaCadastroTarefa;
            this.repositorioItem = repositorioItem;
        }

        public void Editar()
        {
            MostrarTitulo("Editando Item");

            bool temItensRegistrados = VisualizarRegistros("Pesquisando");

            if (temItensRegistrados == false)
            {
                notificador.ApresentarMensagem("Nenhum Item cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            Tarefa tarefaSelecionada = ObtemTarefa();
            Item itemAtualizado = ObterItem(tarefaSelecionada);
            int numeroItem = ObterNumeroRegistro();

            bool conseguiuEditar = repositorioItem.Editar(numeroItem, itemAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Item editado com sucesso", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Item");

            bool temItensRegistrados = VisualizarRegistros("Pesquisando");

            if (temItensRegistrados == false)
            {
                notificador.ApresentarMensagem("Nenhum Item cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroItem = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorioItem.Excluir(numeroItem);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Item excluído com sucesso", TipoMensagem.Sucesso);
        }

        private int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Item que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioItem.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID do Item não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Item");
            Tarefa tarefaSelecionada = ObtemTarefa();
            Item novoItem = ObterItem(tarefaSelecionada);
            repositorioItem.Inserir(novoItem);
            notificador.ApresentarMensagem("Item cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        private Item ObterItem(Tarefa tarefaSelecionada)
        {
            Console.Write("Digite a descrição: ");
            string descricao = Console.ReadLine();
            Console.Write("Digite o status: \n1 - PENDENTE \n2 - CONCLUIDO \nDigite: ");
            int status = int.Parse(Console.ReadLine());

            return new Item(tarefaSelecionada ,descricao, status);
        }

        private Tarefa ObtemTarefa()
        {
            bool temTarefasDisponiveis = telaCadastroTarefa.VisualizarRegistros("");

            if (!temTarefasDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhuma Tarefa disponível para cadastrar em Itens", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número da tarefa que irá inserir: ");
            int numTarefaSelecionado = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Tarefa tarefaSelecionado = repositorioTarefa.SelecionarRegistro(numTarefaSelecionado);

            return tarefaSelecionado;
        }

        public bool VisualizarRegistros(string tipoVisualizado)
        {
            if (tipoVisualizado == "Tela")
                MostrarTitulo("Visualização de Itens");

            List<Item> itens = repositorioItem.SelecionarTodos();

            if (itens.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum Item disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Item item in itens)
                Console.WriteLine(item.ToString());

            Console.ReadLine();

            return true;
        }
    }
}
