using Receitas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receitas.Models
{
    public class NovaReceitaModel
    {
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public int Duracao { get; set; }
        public int[] Ingredientes { get; set; }
    }
}
