using E_Agenda.ConsoleApp.Compartilhado;
using E_Agenda.ConsoleApp.ModuloCompromisso;
using E_Agenda.ConsoleApp.ModuloTarefa;
using System;

namespace E_Agenda.ConsoleApp
{
    internal class Program
    {
        static Notificador notificador = new Notificador();
        static TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(notificador);

        static void Main(string[] args)
        {
            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    return;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ITelaCadastravel)
                    GerenciarCadastroBasico(telaSelecionada, opcaoSelecionada);
                else if (telaSelecionada is ITelaCadastravel)
                    GerenciarCadastroTarefas(telaSelecionada, opcaoSelecionada);
                else if (telaSelecionada is ITelaCadastravel)
                    GerenciarCadastroCompromissos(telaSelecionada, opcaoSelecionada);
            }
        }

        private static void GerenciarCadastroCompromissos(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroCompromisso telaCadastroCompromisso = (TelaCadastroCompromisso)telaSelecionada;

            if (opcaoSelecionada == "1")
                telaCadastroCompromisso.Inserir();

            else if (opcaoSelecionada == "2")
                telaCadastroCompromisso.Editar();

            else if (opcaoSelecionada == "3")
                telaCadastroCompromisso.Excluir();

            else if (opcaoSelecionada == "4")
            {
                bool temRegistros = telaCadastroCompromisso.VisualizarRegistros("Tela");

                if (!temRegistros)
                    notificador.ApresentarMensagem("Não há nenhum compromisso cadastrado!", TipoMensagem.Atencao);
            }
            else if (opcaoSelecionada == "5")
            {
                bool temRegistros = telaCadastroCompromisso.VisualizarRegistrosDiarios("Tela");

                if (!temRegistros)
                    notificador.ApresentarMensagem("Não há nenhum compromisso para vizualização diaria!", TipoMensagem.Atencao);
            }
            else if (opcaoSelecionada == "6")
            {
                bool temRegistros = telaCadastroCompromisso.VisualizarRegistrosSemanais("Tela");

                if (!temRegistros)
                    notificador.ApresentarMensagem("Não há nenhum compromisso para vizualização semanal!", TipoMensagem.Atencao);
            }
            else if (opcaoSelecionada == "7")
            {
                bool temRegistros = telaCadastroCompromisso.VisualizarRegistrosPassados("Tela");

                if (!temRegistros)
                    notificador.ApresentarMensagem("Não há nenhum compromisso passado!", TipoMensagem.Atencao);
            }
            else if (opcaoSelecionada == "8")
            {
                bool temRegistros = telaCadastroCompromisso.VisualizarRegistrosFuturos("Tela");

                if (!temRegistros)
                    notificador.ApresentarMensagem("Não há nenhum compromisso futuro!", TipoMensagem.Atencao);
            }
        }

        public static void GerenciarCadastroBasico(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            ITelaCadastravel telaCadastroBasico = telaSelecionada as ITelaCadastravel;

            if (telaCadastroBasico is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroBasico.Inserir();

            else if (opcaoSelecionada == "2")
                telaCadastroBasico.Editar();

            else if (opcaoSelecionada == "3")
                telaCadastroBasico.Excluir();

            else if (opcaoSelecionada == "4")
                telaCadastroBasico.VisualizarRegistros("Tela");
        }

        private static void GerenciarCadastroTarefas(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroTarefa telaCadastroTarefa = (TelaCadastroTarefa)telaSelecionada;

            if (opcaoSelecionada == "1")
                telaCadastroTarefa.Inserir();

            else if (opcaoSelecionada == "2")
                telaCadastroTarefa.Editar();

            else if (opcaoSelecionada == "3")
                telaCadastroTarefa.Excluir();

            else if (opcaoSelecionada == "4")
            {
                bool temRegistros = telaCadastroTarefa.VisualizarRegistros("Tela");

                if (!temRegistros)
                    notificador.ApresentarMensagem("Não há nenhuma tarefa cadastrada!", TipoMensagem.Atencao);
            }
            else if (opcaoSelecionada == "5")
            {
                bool temRegistros = telaCadastroTarefa.VisualizarRegistrosDiarios("Tela");

                if (!temRegistros)
                    notificador.ApresentarMensagem("Não há nenhuma tarefa para vizualização diaria!", TipoMensagem.Atencao);
            }
            else if (opcaoSelecionada == "6")
            {
                bool temRegistros = telaCadastroTarefa.VisualizarRegistrosSemanias("Tela");

                if (!temRegistros)
                    notificador.ApresentarMensagem("Não há nenhuma tarefa para vizualização semanal!", TipoMensagem.Atencao);
            }
            else if (opcaoSelecionada == "7")
            {
                bool temRegistros = telaCadastroTarefa.VisualizarRegistrosMensais("Tela");

                if (!temRegistros)
                    notificador.ApresentarMensagem("Não há nenhuma tarefa para vizualização mensal!", TipoMensagem.Atencao);
            }
            
        }
    }
}
