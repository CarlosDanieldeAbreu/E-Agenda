using E_Agenda.ConsoleApp.ModuloCompromisso;
using E_Agenda.ConsoleApp.ModuloContato;
using E_Agenda.ConsoleApp.ModuloItem ;
using E_Agenda.ConsoleApp.ModuloTarefa;
using System;

namespace E_Agenda.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private IRepositorio<Tarefa> repositorioTarefa;
        private TelaCadastroTarefa telaCadastroTarefa;

        private IRepositorio<Item> repositorioItem;
        private TelaCadastroItem telaCadastroItem;

        private IRepositorio<Contato> repositorioContato;
        private TelaCadastroContato telaCadastroContato;

        private IRepositorio<Compromisso> repositorioCompromisso;
        private TelaCadastroCompromisso telaCadastroCompromisso;

        public TelaMenuPrincipal(Notificador notificador)
        {
            repositorioTarefa = new RepositorioTarefa();
            telaCadastroTarefa = new TelaCadastroTarefa(repositorioTarefa, notificador);

            repositorioItem = new RepositorioItem();
            telaCadastroItem = new TelaCadastroItem(notificador, repositorioTarefa, telaCadastroTarefa, repositorioItem);

            repositorioContato = new RepositorioContato();
            telaCadastroContato = new TelaCadastroContato(repositorioContato, notificador);

            repositorioCompromisso = new RepositorioCompromisso();
            telaCadastroCompromisso = new TelaCadastroCompromisso(repositorioContato, repositorioCompromisso, telaCadastroContato, notificador);
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("E-Agenda 1.0");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Gerenciar Tarefas");
            Console.WriteLine("Digite 2 para Gerenciar Itens");
            Console.WriteLine("Digite 3 para Gerenciar Contatos");
            Console.WriteLine("Digite 4 para Gerenciar Compromissos");

            Console.WriteLine("Digite s para sair");

            string opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela =  telaCadastroTarefa;

            else if (opcao == "2")
                tela =  telaCadastroItem;

            else if (opcao == "3")
                tela =  telaCadastroContato;

            else if (opcao == "4")
                tela =  telaCadastroCompromisso;

            return tela;
        }
    }
}
