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
    public class pedido_detalleApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/pedido_detalleApi
        public IQueryable<pedido_detalle> Getpedido_detalle()
        {
            return db.pedido_detalle;
        }

        // GET: api/pedido_detalleApi/5
        [ResponseType(typeof(pedido_detalle))]
        public IHttpActionResult Getpedido_detalle(int id)
        {
            pedido_detalle pedido_detalle = db.pedido_detalle.Find(id);
            if (pedido_detalle == null)
            {
                return NotFound();
            }

            return Ok(pedido_detalle);
        }

        // PUT: api/pedido_detalleApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpedido_detalle(int id, pedido_detalle pedido_detalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pedido_detalle.pedido_detalle_id)
            {
                return BadRequest();
            }

            db.Entry(pedido_detalle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!pedido_detalleExists(id))
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

        // POST: api/pedido_detalleApi
        [ResponseType(typeof(List<pedido_detalle>))]
        public async Task<IHttpActionResult> Postpedido_detalle(List<pedido_detalle> pedido_detalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            foreach (var p in pedido_detalle)
            {
                db.pedido_detalle.Add(p);
                await db.SaveChangesAsync();
            }

            return CreatedAtRoute("DefaultApi", new { id = pedido_detalle[0].pedido_detalle_id }, pedido_detalle);
        }

        // DELETE: api/pedido_detalleApi/5
        [ResponseType(typeof(pedido_detalle))]
        public IHttpActionResult Deletepedido_detalle(int id)
        {
            pedido_detalle pedido_detalle = db.pedido_detalle.Find(id);
            if (pedido_detalle == null)
            {
                return NotFound();
            }

            db.pedido_detalle.Remove(pedido_detalle);
            db.SaveChanges();

            return Ok(pedido_detalle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool pedido_detalleExists(int id)
        {
            return db.pedido_detalle.Count(e => e.pedido_detalle_id == id) > 0;
        }
    }
}