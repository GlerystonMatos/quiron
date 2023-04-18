using Quiron.Domain.Entities.Base;
using System;

namespace Quiron.Domain.Entities
{
    public class Animal : Entity
    {
        public Animal(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public string Nome { get; set; }
    }
}