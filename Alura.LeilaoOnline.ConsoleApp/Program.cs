using System;
using Alura.LeilaoOnline.Core;

namespace Alura.LeilaoOnline.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var modalidade = new MaiorOferta();
            var leilao = new Leilao("Van Gogh", modalidade);
            var pessoa1 = new Interessada("Fulano", leilao);
            var pessoa2 = new Interessada("Maria", leilao);

            leilao.RecebeLance(pessoa1, 800);
            leilao.RecebeLance(pessoa2, 900);
            leilao.RecebeLance(pessoa1, 1200);
            leilao.RecebeLance(pessoa2, 1000);

            leilao.TerminaPregao();


            Console.WriteLine(leilao.Ganhador.Valor);

        }
    }
}
