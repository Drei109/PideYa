using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PideYa.Models;

namespace PideYa.Controllers.API
{
    public class platosApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/platosApi
        public List<PlatosRestauranteDto> Getplato()
        {
            var platos = (from p in db.plato
                          select new PlatosRestauranteDto()
                          {
                              plato_id = p.plato_id,
                              nombre = p.nombre,
                              precio = p.precio,
                              descripcion = p.descripcion,
                              foto = p.foto,
                              estado = p.estado,
                              restaurante_nombre = p.restaurante.nombre,
                              restaurante_id_fk = p.restaurante_id_fk,
                              categoria_plato_id_fk = p.categoria_plato_id_fk
                          }).ToList();
            return platos;
        }

        // GET: api/platosApi/5
        //[ResponseType(typeof(plato))]
        [ResponseType(typeof(PlatosRestauranteDto))]
        public List<PlatosRestauranteDto> Getplato(int id, int idRes)
        {
            //plato plato = await db.plato.FindAsync(id);
            //if (plato == null)
            //{
            //    return NotFound();
            //}

            //return Ok(plato);

            var platos = (from p in db.plato
                          where p.categoria_plato_id_fk == id && p.restaurante_id_fk == idRes
                          select new PlatosRestauranteDto()
                          {
                              plato_id = p.plato_id,
                              nombre = p.nombre,
                              precio = p.precio,
                              descripcion = p.descripcion,
                              foto = p.foto,
                              estado = p.estado,
                              restaurante_nombre = p.restaurante.nombre,
                              restaurante_id_fk = p.restaurante_id_fk,
                              categoria_plato_id_fk = p.categoria_plato_id_fk
                          }).ToList();
            return platos;

        }

        // PUT: api/platosApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putplato(int id, plato plato)
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
                await db.SaveChangesAsync();
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

        // POST: api/platosApi
        [ResponseType(typeof(plato))]
        public async Task<IHttpActionResult> Postplato(plato plato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.plato.Add(plato);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = plato.plato_id }, plato);
        }

        // DELETE: api/platosApi/5
        [ResponseType(typeof(plato))]
        public async Task<IHttpActionResult> Deleteplato(int id)
        {
            plato plato = await db.plato.FindAsync(id);
            if (plato == null)
            {
                return NotFound();
            }

            db.plato.Remove(plato);
            await db.SaveChangesAsync();

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