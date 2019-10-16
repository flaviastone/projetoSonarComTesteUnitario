using System;
using System.Collections.Generic;
using System.Text;
using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.XUnit.Testes
{
    public class LanceConstructor
    {
        [Fact]
        public void LancarArgumentExceptionDadoValorNegativo()
        {

            var valorNegativo = -100;
            Assert.Throws<System.ArgumentException>(

                () => new Lance(null,valorNegativo)

                );

        }


    }
}
