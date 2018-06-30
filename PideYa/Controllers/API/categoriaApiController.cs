using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PideYa.Models;

namespace PideYa.Controllers.API
{
    public class categoriaApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/categoriaApi
        public List<Plato_CategoriaRestauranteDTO> Getplato_categoria()
        {
            var categorias = (from p in db.plato
                              join pc in db.plato_categoria on p.categoria_plato_id_fk equals pc.plato_categoria_id
                              select new Plato_CategoriaRestauranteDTO()
                              {
                                  plato_categoria_id = pc.plato_categoria_id,
                                  nombre = pc.nombre,
                                  restaurante_id_fk = p.restaurante_id_fk
                              }).ToList();
            return categorias;
        }

        // GET: api/categoriaApi/5
        [ResponseType(typeof(plato_categoria))]
        public List<Plato_CategoriaRestauranteDTO> Getplato_categoria(int id)
        {
            var categorias = (from p in db.plato.Distinct()
                              join pc in db.plato_categoria on p.categoria_plato_id_fk equals pc.plato_categoria_id
                              where p.restaurante_id_fk == id
                              select new Plato_CategoriaRestauranteDTO()
                              {
                                  plato_categoria_id = pc.plato_categoria_id,
                                  nombre = pc.nombre,
                                  restaurante_id_fk = p.restaurante_id_fk
                              }).Distinct().ToList();

            return categorias;
        }

        // PUT: api/categoriaApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putplato_categoria(int id, plato_categoria plato_categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plato_categoria.plato_categoria_id)
            {
                return BadRequest();
            }

            db.Entry(plato_categoria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!plato_categoriaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/categoriaApi
        [ResponseType(typeof(plato_categoria))]
        public IHttpActionResult Postplato_categoria(plato_categoria plato_categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.plato_categoria.Add(plato_categoria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = plato_categoria.plato_categoria_id }, plato_categoria);
        }

        // DELETE: api/categoriaApi/5
        [ResponseType(typeof(plato_categoria))]
        public IHttpActionResult Deleteplato_categoria(int id)
        {
            plato_categoria plato_categoria = db.plato_categoria.Find(id);
            if (plato_categoria == null)
            {
                return NotFound();
            }

            db.plato_categoria.Remove(plato_categoria);
            db.SaveChanges();

            return Ok(plato_categoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool plato_categoriaExists(int id)
        {
            return db.plato_categoria.Count(e => e.plato_categoria_id == id) > 0;
        }
    }
}