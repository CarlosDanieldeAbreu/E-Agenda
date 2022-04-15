using E_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.ConsoleApp.ModuloTarefa
{
    public class TelaCadastroTarefa : TelaBase, ITelaCadastravel
    {
        private readonly Notificador notificador;
        private readonly IRepositorio<Tarefa> repositorioTarefa;

        public TelaCadastroTarefa(IRepositorio<Tarefa> repositorioTarefa, Notificador notificador) : base("Cadastro de tarefas")
        {
            this.notificador = notificador;
            this.repositorioTarefa = repositorioTarefa;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar todas tarefas");
            Console.WriteLine("Digite 5 para Vizualizar tarefas do dia");
            Console.WriteLine("Digite 6 para Vizualizar todas tarefas da semana");
            Console.WriteLine("Digite 7 para Vizualizar todas tarefas do mês");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void Editar()
        {
            MostrarTitulo("Editando Tarefa");

            bool temTarefasCadastradas = VisualizarRegistros("Pesquisando");

            if (temTarefasCadastradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma Tarefa cadastrada para poder editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroTarefa();

            Tarefa tarefaAtualizada = ObterTarefa();

            bool conseguiuEditar = repositorioTarefa.Editar(numeroTarefa, tarefaAtualizada);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Tarefa editada com sucesso", TipoMensagem.Sucesso);
        }

        private int ObterNumeroTarefa()
        {
            int numeroTarefa;
            bool numeroTarefaEncontrado;

            do
            {
                Console.Write("Digite o número da Tarefa que deseja editar: ");
                numeroTarefa = Convert.ToInt32(Console.ReadLine());

                numeroTarefaEncontrado = repositorioTarefa.ExisteRegistro(numeroTarefa);

                if (numeroTarefaEncontrado == false)
                    notificador.ApresentarMensagem("Número da Tarefa não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroTarefaEncontrado == false);
            return numeroTarefa;
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Tarefa");

            bool temTarefasCadastradas = VisualizarRegistros("Pesquisando");

            if (temTarefasCadastradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma Tarefa cadastrada para poder editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroTarefa();

            bool conseguiuExcluir = repositorioTarefa.Excluir(numeroTarefa);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Tarefa excluida com sucesso", TipoMensagem.Sucesso);
        }

        public void Inserir()
        {
            MostrarTitulo("Inserindo nova tarefa");
            Tarefa novaTarefa = ObterTarefa();
            string statusValidacao = repositorioTarefa.Inserir(novaTarefa);
            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Tarefa cadastrada com sucesso", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        private Tarefa ObterTarefa()
        {
            Console.Write("Digite o titulo: ");
            string titulo = Console.ReadLine();
            DateTime dataCriacao = DateTime.Now;
            Console.Write("Digite a data de conclusão: ");
            DateTime dataConclusao = DateTime.Parse(Console.ReadLine());
            Console.Write("Digite o percentual de concluido: ");
            double percentualConcluido = double.Parse(Console.ReadLine());
            Console.Write("Digite a prioridade: \n1 - BAIXA \n2 - NORMAL \n3 - ALTA\nDigite: ");
            int prioridade = int.Parse(Console.ReadLine());

            return new Tarefa(titulo, dataCriacao, dataConclusao, percentualConcluido, prioridade); 
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Tarefas");

            List<Tarefa> tarefas = repositorioTarefa.SelecionarTodos();

            if (tarefas.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }
            foreach (Tarefa tarefa in tarefas)
            {
                tarefas.GroupBy(x => x.Prioridade);
                Console.WriteLine(tarefa.ToString());
            }
              

            Console.ReadLine();

            return true;
        }

        public bool VisualizarRegistrosDiarios(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Tarefas");

            List<Tarefa> tarefas = repositorioTarefa.SelecionarTodos();

            if (tarefas.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa tarefa in tarefas)
                if(CalcularDiferencaData(tarefa.DataDeCriacao) >=1 && CalcularDiferencaData(tarefa.DataDeCriacao) < 2)
                    Console.WriteLine(tarefa.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarRegistrosSemanias(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Tarefas");

            List<Tarefa> tarefas = repositorioTarefa.SelecionarTodos();

            if (tarefas.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa tarefa in tarefas)
                if (CalcularDiferencaData(tarefa.DataDeCriacao) <= 7)
                    Console.WriteLine(tarefa.ToString());

            Console.ReadLine();

            return true;
        }


        public bool VisualizarRegistrosMensais(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Tarefas");

            List<Tarefa> tarefas = repositorioTarefa.SelecionarTodos();

            if (tarefas.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }
            foreach (Tarefa tarefa in tarefas)
                if (CalcularDiferencaData(tarefa.DataDeCriacao) <= 30)
                    Console.WriteLine(tarefa.ToString());

            Console.ReadLine();

            return true;
        }
    }
}
