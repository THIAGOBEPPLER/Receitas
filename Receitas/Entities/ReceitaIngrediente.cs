using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receitas.Entities
{
    public class ReceitaIngrediente
    {
        public int ReceitaId { get; set; }
        public Receita Receita { get; set; }

        public int IngredienteId { get; set; }
        public Ingrediente Ingrediente { get; set; }
    }
}
