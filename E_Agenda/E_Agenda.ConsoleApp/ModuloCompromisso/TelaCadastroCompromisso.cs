using E_Agenda.ConsoleApp.Compartilhado;
using E_Agenda.ConsoleApp.ModuloContato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.ConsoleApp.ModuloCompromisso
{
    public class TelaCadastroCompromisso : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Contato> repositorioContato;
        private readonly IRepositorio<Compromisso> repositorioCompromisso;
        private readonly TelaCadastroContato telaCadastroContato;
        private readonly Notificador notificador;

        public TelaCadastroCompromisso(IRepositorio<Contato> repositorioContato, IRepositorio<Compromisso> repositorioCompromisso, TelaCadastroContato telaCadastroContato, Notificador notificador) : base("telaCadastroContato de compromisso")
        {
            this.repositorioContato = repositorioContato;
            this.repositorioCompromisso = repositorioCompromisso;
            this.telaCadastroContato = telaCadastroContato;
            this.notificador = notificador;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar todos compromissos");
            Console.WriteLine("Digite 5 para Vizualizar compromissos do dia");
            Console.WriteLine("Digite 6 para Vizualizar todas tarefas da semana");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void Editar()
        {
            MostrarTitulo("Editando Compromisso");

            bool temCompromissosRegistrados = VisualizarRegistros("Pesquisando");

            if (temCompromissosRegistrados == false)
            {
                notificador.ApresentarMensagem("Nenhum Compromisso cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            Contato contatoSelecionado = ObtemContato();
            Compromisso compromissoAtualizado = ObterItem(contatoSelecionado);
            int numeroCompromisso = ObterNumeroRegistro();

            bool conseguiuEditar = repositorioCompromisso.Editar(numeroCompromisso, compromissoAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Compromisso editado com sucesso", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Compromisso");

            bool temCompromissosRegistrados = VisualizarRegistros("Pesquisando");

            if (temCompromissosRegistrados == false)
            {
                notificador.ApresentarMensagem("Nenhum Compromissos cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroCompromissos = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorioCompromisso.Excluir(numeroCompromissos);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Compromisso excluído com sucesso", TipoMensagem.Sucesso);
        }

        private int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Compromisso que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioCompromisso.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID do Compromisso não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Compromisso");
            Contato contatoSelecionada = ObtemContato();
            Compromisso novoCompromisso = ObterItem(contatoSelecionada);
            repositorioCompromisso.Inserir(novoCompromisso);
            notificador.ApresentarMensagem("Compromisso cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        private Compromisso ObterItem(Contato contatoSelecionado)
        {
            Console.Write("Digite a hora de inicio: ");
            DateTime dataInicio = DateTime.Parse(Console.ReadLine());
            Console.Write("Digite a hora de termino: ");
            DateTime dataTermino = DateTime.Parse(Console.ReadLine());
            Console.Write("Digite a data do compromisso: ");
            DateTime dataCompromisso = DateTime.Parse(Console.ReadLine());
            Console.Write("Digite o assunto: ");
            string assunto = Console.ReadLine();
            Console.Write("Digite o local: ");
            string local = Console.ReadLine();

            return new Compromisso(dataInicio, dataTermino, dataCompromisso, assunto, local, contatoSelecionado);
        }

        private Contato ObtemContato()
        {
            bool temContatosDisponiveis = telaCadastroContato.VisualizarRegistros("");

            if (!temContatosDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhum contato disponível para cadastrar em compromisso", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o Id do Contato que irá inserir: ");
            int numContatoSelecionado = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Contato contatoSelecionado = repositorioContato.SelecionarRegistro(numContatoSelecionado);

            return contatoSelecionado;
        }

        public bool VisualizarRegistros(string tipoVisualizado)
        {
            if (tipoVisualizado == "Tela")
                MostrarTitulo("Visualização de Compromisso");

            List<Compromisso> compromissos = repositorioCompromisso.SelecionarTodos();

            if (compromissos.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum Compromisso disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissos)
                Console.WriteLine(compromisso.ToString());

            Console.ReadLine();

            return true;
        }
        public bool VisualizarRegistrosDiarios(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Tarefas");

            List<Compromisso> compromissos = repositorioCompromisso.SelecionarTodos();

            if (compromissos.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhum Compromisso disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissos)
                if (compromisso.DataCompromisso.Equals(CalcularDiferencaData(compromisso.DataCompromisso) >= 1 && CalcularDiferencaData(compromisso.DataCompromisso) < 2))
                    Console.WriteLine(compromisso.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarRegistrosSemanais(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Compromisso mensal");

            List<Compromisso> compromissos = repositorioCompromisso.SelecionarTodos();

            if (compromissos.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhum compromisso disponível.", TipoMensagem.Atencao);
                return false;
            }
            foreach (Compromisso compromisso in compromissos)
                if (compromisso.DataCompromisso.Equals(CalcularDiferencaData(compromisso.DataCompromisso) <= 7))
                    Console.WriteLine(compromisso.ToString());

            Console.ReadLine();

            return true;
        }
    }
}
