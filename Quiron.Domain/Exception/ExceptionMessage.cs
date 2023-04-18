using System.ComponentModel;

namespace Quiron.Domain.Exception
{
    [DisplayName("Mensagem")]
    public class ExceptionMessage
    {
        public ExceptionMessage(string mensagem)
            => Mensagem = mensagem;

        public string Mensagem { get; set; }
    }
}