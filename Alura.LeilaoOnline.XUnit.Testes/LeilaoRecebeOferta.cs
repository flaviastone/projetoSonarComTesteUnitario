using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Alura.LeilaoOnline.Core;


namespace Alura.LeilaoOnline.XUnit.Testes
{
    public class LeilaoRecebeOferta
    {

        [Fact]
        public void NaoAceitaLancesConsecutivosDoMesmoCliente()
        {
            var modalidade = new MaiorOferta();
            var leilao = new Leilao("Van Gogh", modalidade);
            var pessoa1 = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();
            leilao.RecebeLance(pessoa1, 1000);
            leilao.RecebeLance(pessoa1, 1200);

            leilao.TerminaPregao();
            var qtdeEsperada = 1;
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeObtida, qtdeEsperada);


        }



        [Theory]
        [InlineData(4, new double[] { 1000, 1200, 1400, 1300 })]
        [InlineData(2, new double[] { 800, 900 })]
        private void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdeEsperada, double[] ofertas)
        {
            var modalidade = new MaiorOferta();
            var leilao = new Leilao("Van Gogh", modalidade);
            var pessoa1 = new Interessada("Fulano", leilao);
            var pessoa2 = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];

                if (i % 2 == 0)
                {
                    leilao.RecebeLance(pessoa1, valor);
                }
                else
                {
                    leilao.RecebeLance(pessoa2, valor);
                }

            }

            leilao.TerminaPregao();

            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeObtida, qtdeEsperada);

        }
    }
}
