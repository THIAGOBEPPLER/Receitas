using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Receitas.Entities
{
    public class Ingrediente
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
