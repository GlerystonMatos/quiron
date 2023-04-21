using Quiron.Domain.Entities.Base;
using System;

namespace Quiron.Domain.Entities
{
    public class Usuario : Entity
    {
        public Usuario()
            => Id = Guid.NewGuid();

        public Usuario(Guid id, string nome, string login, string senha)
        {
            Id = id;
            Nome = nome;
            Login = login;
            Senha = senha;
        }

        public string Nome { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }
    }
}