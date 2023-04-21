using Quiron.Domain.Entities.Base;
using System;

namespace Quiron.Domain.Entities
{
    public class Cidade : Entity
    {
        public Cidade()
            => Id = Guid.NewGuid();

        public Cidade(Guid id, string nome, Guid idEstado)
        {
            Id = id;
            Nome = nome;
            IdEstado = idEstado;
        }

        public string Nome { get; set; }

        public Guid IdEstado { get; set; }

        public Estado Estado { get; set; }
    }
}