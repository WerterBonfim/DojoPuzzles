using System;

namespace Werter.DojoPuzzles.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //DesafioAnalisadorDeUrls();
            new EscrevendoNoCelular().Executar();

        }

        private static void DesafioAnalisadorDeUrls()
        {
            var partesDaUrl =
                new AnalisadorDeUrls("https://www.ecomerce.com.br/tdd-com-cshap/livro/ref=1?keywords=tdd-C#&sr=8-1")
                    .Analisar();

            Console.WriteLine(partesDaUrl.ToString());
        }
    }
}   