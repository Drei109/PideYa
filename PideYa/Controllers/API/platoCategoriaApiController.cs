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
    public class platoCategoriaApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/platoApiCategoria
        public IQueryable<plato_categoria> Getplato_categoria()
        {
            return db.plato_categoria;
        }

        // GET: api/platoApiCategoria/5
        [ResponseType(typeof(plato_categoria))]
        public async Task<IHttpActionResult> Getplato_categoria(int id)
        {
            plato_categoria plato_categoria = await db.plato_categoria.FindAsync(id);
            if (plato_categoria == null)
            {
                return NotFound();
            }

            return Ok(plato_categoria);
        }

        // PUT: api/platoApiCategoria/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putplato_categoria(int id, plato_categoria plato_categoria)
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
                await db.SaveChangesAsync();
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

        // POST: api/platoApiCategoria
        [ResponseType(typeof(plato_categoria))]
        public async Task<IHttpActionResult> Postplato_categoria(plato_categoria plato_categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.plato_categoria.Add(plato_categoria);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = plato_categoria.plato_categoria_id }, plato_categoria);
        }

        // DELETE: api/platoApiCategoria/5
        [ResponseType(typeof(plato_categoria))]
        public async Task<IHttpActionResult> Deleteplato_categoria(int id)
        {
            plato_categoria plato_categoria = await db.plato_categoria.FindAsync(id);
            if (plato_categoria == null)
            {
                return NotFound();
            }

            db.plato_categoria.Remove(plato_categoria);
            await db.SaveChangesAsync();

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