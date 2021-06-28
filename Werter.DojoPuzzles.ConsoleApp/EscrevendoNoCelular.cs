using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Werter.DojoPuzzles.ConsoleApp
{
    /*
     * Escrevendo no celular
     * 
     * https://dojopuzzles.com/problems/escrevendo-no-celular/
     * 
     * Um dos serviços mais utilizados pelos usuários de aparelhos celulares são os SMS (Short Message Service),
     * que permite o envio de mensagens curtas (até 255 caracteres em redes GSM e 160 caracteres em redes CDMA).
     *
     * Para digitar uma mensagem em um aparelho que não possui um teclado QWERTY embutido é necessário fazer algumas
     * combinações das 10 teclas numéricas do aparelho para conseguir digitar. Cada número é associado a um conjunto
     * de letras como a seguir:
     * 
     * Letras  ->  Número
     * ABC    ->  2
     * DEF    ->  3
     * GHI    ->  4
     * JKL    ->  5
     * MNO    ->  6
     * PQRS    ->  7
     * TUV    ->  8
     * WXYZ   ->  9
     * Espaço -> 0
     *
     * Desenvolva um programa que, dada uma mensagem de texto limitada a 255 caracteres, retorne a seqüência
     * de números que precisa ser digitada. Uma pausa, para ser possível obter duas letras referenciadas pelo mesmo
     * número, deve ser indicada como _.
     *
     * Por exemplo, para digitar "SEMPRE ACESSO O DOJOPUZZLES", você precisa digitar:
     *
     * 77773367_7773302_222337777_777766606660366656667889999_9999555337777
     *
     * 7777 = S
     * 33   = E
     * 6    = M
     * 7    = P
     * _    =
     * 777  = R
     * 33   = E
     * 0    =
     * 2    = A
     */

    public class EscrevendoNoCelular
    {
        private readonly Dictionary<string, string> Teclado = new()
        {
            {"2", "ABC"},
            {"3", "DEF"},
            {"4", "GHI"},
            {"5", "JKL"},
            {"6", "MNO"},
            {"7", "PQRS"},
            {"8", "TUV"},
            {"9", "WXYZ"},
            {"0", " "},
        };

        private string _textoDeEntrada;
        private string _letraAnterior;
        private string _letraCorrente;
        private StringBuilder _textoPadronizado;

        public void Executar()
        {
            Console.WriteLine("Digite texto SMS: ");
            var textoSms = Console.ReadLine();

            var texto = TraduzirSms(textoSms);
            Console.WriteLine("Mensagem SMS:\n");
            Console.WriteLine(texto);
        }
        
        public string TraduzirSms(string textoCelular)
        {
            if (EntradaInvalida(textoCelular))
                throw new Exception("Foi fornecido caracteres inválidos");
            
            var letras = EstrairListaPadronizada(textoCelular);
            var caracteres = letras
                .Select(x => TraduzirCaractere(x));
            
            var textoSms = string.Join("", caracteres);
            
            return textoSms;

        }

        public char TraduzirCaractere(string digitos)
        {
            var numero = digitos[0].ToString();
            var posicaoCaractere = digitos.Length - 1;
            var letra = Teclado[numero][posicaoCaractere];
            return letra;
        }

        /// <summary>
        /// Aplica um padrão para facilitar a tradução
        /// </summary>
        /// <param name="entrada">SMS digitado pelo usuário</param>
        /// <returns>Uma lista padronizada</returns>
        public IList<string> EstrairListaPadronizada(string entrada)
        {
            _letraAnterior = entrada[0].ToString();
            _textoPadronizado = new StringBuilder()
                .Append(_letraAnterior);

            for (var index = 1; index < entrada.Length; index++)
            {
                _letraCorrente = entrada[index].ToString();
                
                if (index > 1)
                    _letraAnterior = _textoPadronizado[_textoPadronizado.Length -1].ToString();

                if (AplicouUnderline()) continue;

                if (JaTemEspacoVazio()) continue;

                if (AplicouMesmaLetra()) continue;

                if (AplicouEspacoEmBranco()) continue;

                _textoPadronizado.Append(" " + _letraCorrente);
            }

            return _textoPadronizado.ToString().Split(" ");
        }
        
        private bool AplicouUnderline()
        {
            var eUnderline = _letraCorrente == "_";
            if (eUnderline)
                _textoPadronizado.Append(" ");

            return eUnderline;
        }
        
        /// <summary>
        /// Verifica se a letra corrente e letra anterior são
        /// espaços em branco
        /// </summary>
        /// <returns>Se já tem espaço em branco</returns>
        private bool JaTemEspacoVazio()
        {
            var temMesmasLetras = _letraCorrente == _letraAnterior;

            var letraEnteriorEEspacoEmBranco = _letraAnterior == " ";
            if (letraEnteriorEEspacoEmBranco && temMesmasLetras)
                return true;
            
            return false;
        }

        private bool AplicouEspacoEmBranco()
        {
            var letraEnteriorEEspacoEmBranco = _letraAnterior == " ";
            if (letraEnteriorEEspacoEmBranco)
                _textoPadronizado.Append(_letraCorrente);

            return letraEnteriorEEspacoEmBranco;
        }

        private bool AplicouMesmaLetra()
        {
            var temMesmasLetras = _letraCorrente == _letraAnterior;
            if (temMesmasLetras)
                _textoPadronizado.Append(_letraCorrente);
                

            return temMesmasLetras;
        }


        private bool EntradaInvalida(string textoCelular)
        {
            if (string.IsNullOrEmpty(textoCelular))
                return true;
            
            return Regex.IsMatch(textoCelular, "[a-zA-Z]");
        }
    }
}