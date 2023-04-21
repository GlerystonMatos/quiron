using Quiron.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Quiron.Domain.Entities
{
    public class Estado : Entity
    {
        public Estado()
            => Id = Guid.NewGuid();

        public Estado(Guid id, string nome, string uf)
        {
            Id = id;
            Uf = uf;
            Nome = nome;
        }

        public string Nome { get; set; }

        public string Uf { get; set; }

        public ICollection<Cidade> Cidades { get; set; }
    }
}