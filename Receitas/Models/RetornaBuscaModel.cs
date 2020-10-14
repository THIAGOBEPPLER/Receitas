using Receitas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receitas.Models
{
    public class RetornaBuscaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public int Duracao { get; set; }
    }
}
