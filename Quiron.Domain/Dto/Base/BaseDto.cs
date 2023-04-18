using System;
using System.ComponentModel;

namespace Quiron.Domain.Dto.Base
{
    [DisplayName("BaseDto")]
    public class BaseDto
    {
        public Guid Id { get; set; }
    }
}