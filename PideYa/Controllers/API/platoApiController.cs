using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PideYa.Models;

namespace PideYa.Controllers.API
{
    public class platoApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: api/platoApi
        public IQueryable<plato> Getplato()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.plato;
        }

        // GET: api/platoApi/5
        [ResponseType(typeof(plato))]
        public IHttpActionResult Getplato(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            //plato plato = db.plato.Find(id);
            var plato = db.plato.Include(x => x.plato_categoria).SingleOrDefault(x => x.plato_id == id);
            if (plato == null)
            {
                return NotFound();
            }

            return Ok(plato);
        }

        // PUT: api/platoApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putplato(int id, plato plato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plato.plato_id)
            {
                return BadRequest();
            }

            db.Entry(plato).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!platoExists(id))
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

        // POST: api/platoApi
        [ResponseType(typeof(plato))]
        public IHttpActionResult Postplato(plato plato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.plato.Add(plato);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = plato.plato_id }, plato);
        }

        // DELETE: api/platoApi/5
        [ResponseType(typeof(plato))]
        public IHttpActionResult Deleteplato(int id)
        {
            plato plato = db.plato.Find(id);
            if (plato == null)
            {
                return NotFound();
            }

            db.plato.Remove(plato);
            db.SaveChanges();

            return Ok(plato);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool platoExists(int id)
        {
            return db.plato.Count(e => e.plato_id == id) > 0;
        }
    }
}