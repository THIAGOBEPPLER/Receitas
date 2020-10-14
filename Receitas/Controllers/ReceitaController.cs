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
    public class ReceitaController : ControllerBase
    {
        ReceitaContext bd = new ReceitaContext();

        [HttpPost()]
        public string Adiciona([FromBody] NovaReceitaModel request)
        {
            var receita = new Receita();
            int[] ingredientes = request.Ingredientes;

            receita.Nome = request.Nome;
            receita.Categoria = request.Categoria;
            receita.Descricao = request.Descricao;
            receita.Duracao = request.Duracao;
            // ingredientes = request.Ingredientes;

            bd.Receitas.Add(receita);
            bd.SaveChanges();

            
            foreach (int i in ingredientes)
            {
                var ri = new ReceitaIngrediente();
                ri.ReceitaId = receita.Id;
                ri.IngredienteId = i;
                bd.ReceitasIngredientes.Add(ri);
            }
            bd.SaveChanges();

            return ("Cadastrado.");
        }
        [HttpPut()]
        public string Edita([FromBody] ReceitaModel request)
        {
            var id = request.Id;

            var query =
               (from r in bd.Receitas
                where r.Id == id
                select r).SingleOrDefault();

            if (query == null)
                return ("Receita nao encontrada.");

            var receita = query;
            int[] ingredientes = request.Ingredientes;



            receita.Nome = request.Nome;
            receita.Categoria = request.Categoria;
            receita.Descricao = request.Descricao;
            receita.Duracao = request.Duracao;

            bd.Receitas.Update(receita);
            // bd.SaveChanges();


            var limpa = (from r in bd.ReceitasIngredientes
                         where r.ReceitaId == id
                         select r);

            bd.ReceitasIngredientes.RemoveRange(limpa);
            // bd.SaveChanges();

            
            foreach (int i in ingredientes)
            {
                var ri = new ReceitaIngrediente();
                ri.ReceitaId = receita.Id;
                ri.IngredienteId = i;
                bd.ReceitasIngredientes.Add(ri);
                
            }
            bd.SaveChanges();


            return ("Editado.");
        }
        [HttpDelete("{id}")]
        public ActionResult<string> Deleta(int id)
        {
            var query =
                  (from r in bd.Receitas
                   where r.Id == id
                   select r).SingleOrDefault();

            if (query == null)
                return BadRequest("Receita nao encontrada.");

            var receita = query;

            bd.Receitas.Remove(receita);
            bd.SaveChanges();


            return Ok("Receita deletada.");
        }
        [HttpGet("{id}")]
        public dynamic BuscaId(int id)
        {
            var query1 =
                  (from r in bd.Receitas
                   where r.Id == id
                   select new ReceitaModel
                   {
                       Id = r.Id,
                       Nome = r.Nome,
                       Categoria = r.Categoria,
                       Descricao = r.Descricao,
                       Duracao = r.Duracao
                   }
                   ).SingleOrDefault();

            var query2 =
                  (from ri in bd.ReceitasIngredientes
                   where ri.ReceitaId == id
                   select ri.IngredienteId).ToArray();

            query1.Ingredientes = query2;

            return (query1);
        }
        [HttpGet()]
        public ActionResult BuscaDinamica()
        {
            string nome = Request.Query["nome"];
            string categoria = Request.Query["categoria"];
            dynamic query = null;

            if (nome == "" && categoria == "")
            {
                query =
                   (from r in bd.Receitas
                    select new RetornaBuscaModel
                    {
                        Id = r.Id,
                        Nome = r.Nome,
                        Categoria = r.Categoria,
                        Duracao = r.Duracao,
                    }).ToList();
            }
            else if (nome == "")
            {
                query =
                   (from r in bd.Receitas
                    where r.Categoria == categoria
                    select new RetornaBuscaModel
                    {
                        Id = r.Id,
                        Nome = r.Nome,
                        Categoria = r.Categoria,
                        Duracao = r.Duracao,
                    }).ToList();
            }

            else if (categoria == "")
            {
               query =
                   (from r in bd.Receitas
                    where r.Nome.Contains(nome)
                    select new RetornaBuscaModel
                    {
                        Id = r.Id,
                        Nome = r.Nome,
                        Categoria = r.Categoria,
                        Duracao = r.Duracao,
                    }).ToList();

            }
            else
            {
                query =
                   (from r in bd.Receitas
                    where r.Nome.Contains(nome) && r.Categoria == categoria
                    select new RetornaBuscaModel
                    {
                        Id = r.Id,
                        Nome = r.Nome,
                        Categoria = r.Categoria,
                        Duracao = r.Duracao,
                    }).ToList();
            }


            return Ok(query);
        }
    }
}
