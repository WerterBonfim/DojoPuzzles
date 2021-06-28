using System;
using Werter.DojoPuzzles.ConsoleApp;
using Xunit;

namespace Werter.DojoPuzzles.TDD
{
    public class EscrevendoNoCeluarTeste
    {
        /// <summary>
        /// Texto no formato celular
        /// </summary>
        private readonly string _textoCelular = "83_3033022666_6";
        
        private readonly string _textoSMS = "TDD E BOM";
        
        [Fact(DisplayName = "Dado a entrada '777' deve retornar a letra V" )]
        [Trait("Celular", "Letras")]
        public void DeveRetornarALetraR()
        {
            // Arrange
            var app = new EscrevendoNoCelular();

            // Act
            var letra = app.TraduzirCaractere("777");

            // Assert
            Assert.Equal(letra, 'R');
            Assert.NotEqual(letra, 'W');
        }


        [Fact(DisplayName = "Deve agrupar por digito")]
        [Trait("Celular", "Frase")]
        public void DeveExtrairFrasesDeUmaEntrada()
        {
            // Arrange
            var celular = new EscrevendoNoCelular();

            // Act
            var letras = celular.EstrairListaPadronizada(_textoCelular);

            // Assert
            var fraseTddEBom = new[]
            {
                "8",    // T
                "3",   // D
                "3",    // D
                "0",    // Espaço
                "33",   // E
                "0",    // Espaço
                "22",   // B
                "666", // O
                "6"     // M
            };
            
            Assert.Equal(fraseTddEBom, letras);
        }

        [Fact(DisplayName = "Deve retornar o texto corretamente")]
        [Trait("Celular", "Texto")]
        public void DeveRetornarOTextoCorretamente()
        {
            // Arrange
            var celular = new EscrevendoNoCelular();

            // Act
            var mensage = celular.TraduzirSms(_textoCelular);

            // Assert
            
            Assert.Equal(_textoSMS, mensage);
        }

        [Fact(DisplayName = "Deve notificar um erro para entrada inválida")]
        [Trait("Celular", "Entrada")]
        public void DeveNotificarErroParaEntradaInvalida()
        {
            // Arrange
            var celular = new EscrevendoNoCelular();
            
            // Act
            Action acao = () => celular.TraduzirSms("alsdjf");

            // Assert
            Assert.Throws<Exception>(acao);


        }
    }
}