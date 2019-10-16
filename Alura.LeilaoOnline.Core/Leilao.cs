using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        EstadoLeilaoAntesdoPregao,
        EstadoLeilaoEmAndamento,
        EstadoLeilaoFinalizado
    }


    public class Leilao
    {

        private IList<Lance> _lances;
        private Interessada _ultimoCliente;
        private IModalidadeAvaliacao _avaliador;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; private set; }
       

        public Leilao(string peca, IModalidadeAvaliacao avaliador)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.EstadoLeilaoAntesdoPregao;
            _avaliador = avaliador;
        }


        private bool NovoLanceAceito(Interessada cliente, double valor)
        {
            return (Estado == EstadoLeilao.EstadoLeilaoEmAndamento) &&
                 (cliente != _ultimoCliente);
        }


        public void RecebeLance(Interessada cliente, double valor)
        {
            if (NovoLanceAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }

        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.EstadoLeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.EstadoLeilaoEmAndamento)
            {
                throw new System.InvalidOperationException("Não é possivel terminar o pregão sem que ele tenha começado! Utilize o metodo IniciarPregao() primeiro.");
            }

            Ganhador = _avaliador.Avalia(this);
            Estado = EstadoLeilao.EstadoLeilaoFinalizado;

        }
    }
}
