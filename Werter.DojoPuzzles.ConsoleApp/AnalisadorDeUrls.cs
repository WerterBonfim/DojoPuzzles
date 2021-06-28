using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Werter.DojoPuzzles.ConsoleApp
{
    
    /*
     *
     *  Dada uma URL, desenvolva um programa que indique se a URL é válida ou não e, caso seja válida, retorne as suas partes constituintes.
     *  Exemplos:

        Entrada: http://www.google.com/mail/user=fulano
        Saída:
            protocolo: http
            host: www
            domínio: google.com
            path: mail
            parâmetros: user=fulano
        
        Entrada: ssh://fulano%senha@git.com/
        Saída:
            protocolo: ssh
            usuário: fulano
            senha: senha
            dominio: git.com
        
        Entrada: https://www.ecomerce.com.br/tdd-com-cshap/livro/ref=1?keywords=tdd-C#&sr=8-1
        Saída:
            protocolo: https
            host: www
            dominio: ecomerce.com.br
            path:   tdd-com-cshap/livro
            parametros: ref=1, keywords=tdd-C#, sr=8-1
     * 
     */
    public class AnalisadorDeUrls
    {
        private readonly string _url;
        private PartesDaUrl _partesDaUrl;
        
        public AnalisadorDeUrls(string url)
        {
            _url = url;
            _partesDaUrl = new PartesDaUrl();
        }

        public PartesDaUrl Analisar()
        {
            EstrairProtocolo();
            EstrairUsuarioESenhaSeHouver();
            ExtrairHost();
            ExtrairDominio();
            ExtrairOsParametros();
            ExtrairPath();


            return _partesDaUrl;
        }

        private void ExtrairHost()
        {
            _partesDaUrl.Host = _url
                .Split('.')[0]
                .Split("//")[1];
        }

        private void EstrairProtocolo()
        {
            _partesDaUrl.Protocolo = _url.Split(':')[0];
        }
        
        private void EstrairUsuarioESenhaSeHouver()
        {
            if (_partesDaUrl.Protocolo != "ssh")
                return;

            var usuarioESenha = _url
                .Split("//")[1]
                .Split("@")[0]
                .Split('%');

            _partesDaUrl.Usuario = usuarioESenha[0];
            _partesDaUrl.Senha = usuarioESenha[1];

        }
        
        private void ExtrairDominio()
        {
            if (_partesDaUrl.Protocolo == "ssh")
                _partesDaUrl.Dominio = _url.Split('@')[1];
            else
                _partesDaUrl.Dominio = _url
                    .Split('/')[2]
                    .Replace($"{_partesDaUrl.Host}.", "");
        }

        private void ExtrairOsParametros()
        {
            _partesDaUrl.Parametro = _url
                .Split('/')
                .Last()
                .Split('?')[0];
        }

        private void ExtrairPath()
        {
            _partesDaUrl.QueryStrings = _url
                .Split('/')
                .Last()
                .Split('?')[1]
                .Split('&');
        }

      
        
        private void ExtrairUsuarioESenha()
        {
        }

        public class PartesDaUrl
        {
            public string Protocolo { get; set; }
            public string Dominio { get; set; }
            public string Host { get; set; }
            public string Path { get; set; }
            public string Parametro { get; set; }
            public string[] QueryStrings { get; set; }
            public string Usuario { get; set; }
            public string Senha { get; set; }

            public PartesDaUrl()
            {
                QueryStrings = new string[]{};
            }

            public override string ToString()
            {
                var queryStrings = string.Join(", ", QueryStrings);

                var builder = new StringBuilder();

                if (!string.IsNullOrEmpty(Protocolo))
                    builder.AppendLine($"Protocolo: {Protocolo}");

                if (!string.IsNullOrEmpty(Host))
                    builder.AppendLine($"Host: {Host}");

                if (!string.IsNullOrEmpty(Usuario))
                    builder.AppendLine($"Usuario: {Usuario}");

                if (!string.IsNullOrEmpty(Senha))
                    builder.AppendLine($"Senha: {Senha}");

                if (!string.IsNullOrEmpty($"Dominio: {Dominio}"))
                    builder.AppendLine($"Dominio: {Dominio}");

                if (!string.IsNullOrEmpty(Path))
                    builder.AppendLine($"Path: {Path}\n");

                if (!string.IsNullOrEmpty(Parametro))
                    builder.AppendLine($"Parametro: {Parametro}");

                if (!string.IsNullOrEmpty(queryStrings))
                    builder.AppendLine($"QueryStrings: {queryStrings}");

                return builder.ToString();
            }
        }
    }
}