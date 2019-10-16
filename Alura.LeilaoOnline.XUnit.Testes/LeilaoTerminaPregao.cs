using System;
using System.Collections.Generic;
using System.Text;
using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.XUnit.Testes
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 900, 1400, 1250 })]

        private void RetornaSuperiorMaiorMaisProximo(double valorDestino, double valorEsperado, double[] ofertas)
        {
            IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);
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

            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);

        }

        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        private void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {

            IModalidadeAvaliacao modalidade = new MaiorOferta();
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

            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);

        }


        [Fact]
        private void LancaInvalidOperationExceptionDadoPregaoNaoInciado()
        {
            var modalidade = new MaiorOferta();
            var leilao = new Leilao("Van Gogh", modalidade);

            var exceptionObtida = Assert.Throws<System.InvalidOperationException>(
                () => leilao.TerminaPregao()
            );
            
            var msgEsperada ="Não é possivel terminar o pregão sem que ele tenha começado! Utilize o metodo IniciarPregao() primeiro.";

            Assert.Equal(msgEsperada, exceptionObtida.Message);
        }


        [Fact]
        private void RetornaZeroDadoLeilaoSemLances()
        {
            var modalidade = new MaiorOferta();
            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciaPregao();

            leilao.TerminaPregao();
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(0, valorObtido);

        }

    }
}
