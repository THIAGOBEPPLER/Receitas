using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Receitas.Data;
using Receitas.Entities;
using Receitas.Models;


namespace Receitas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredienteController : ControllerBase
    {
        
        ReceitaContext bd = new ReceitaContext();
        [HttpGet()]
        public List<RetornaIndredienteModel> BuscaIngrediente()
        {
            var query =
                (from i in bd.Ingredientes
                 select new RetornaIndredienteModel
                 {
                     Id = i.Id,
                     Nome = i.Nome
                 }).ToList();

            return query;
        }
        [HttpGet("{sujestao}")]
        public dynamic Sugestao()
        {
            string ingredientes = Request.Query["ingredientes"];

            if (ingredientes == null)
                return "Lista vazia.";

            var arrayIngredientes = ingredientes.Split(',');

            List<int> listaIngredientes = new List<int>();

            foreach (var i in arrayIngredientes)
            {
                listaIngredientes.Add(int.Parse(i));
            }

            List<Receita> listaReceitas = new List<Receita>();

            foreach (var i in listaIngredientes)
            {
                var query =
                  (from r in bd.Receitas
                   join ri in bd.ReceitasIngredientes on r.Id equals ri.ReceitaId
                   where ri.IngredienteId == i
                   select r).ToList();

                foreach (var q in query)
                {
                    listaReceitas.Add(q);
                }
            }


            var listaDeRepetidos = listaReceitas.GroupBy(x => x)
                    .Where(x => x.Count() == arrayIngredientes.Length)
                    .Select(x => x.Key)
                    .ToList();


            return (listaDeRepetidos);

        }

    }

}
