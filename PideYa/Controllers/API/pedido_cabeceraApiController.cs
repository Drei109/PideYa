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
    public class pedido_cabeceraApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/pedido_cabeceraApi
        [ResponseType(typeof(pedido_cabecera))]
        public IHttpActionResult Getpedido_cabecera()
        {
            pedido_cabecera pedido_cabecera =
                db.pedido_cabecera.OrderByDescending(t => t.pedido_cabecera_id).FirstOrDefault();
            if (pedido_cabecera == null)
            {
                return NotFound();
            }

            return Ok(pedido_cabecera);
        }

        // GET: api/pedido_cabeceraApi/5
        [ResponseType(typeof(pedido_cabecera))]
        public IHttpActionResult Getpedido_cabecera(int id)
        {
            pedido_cabecera pedido_cabecera = db.pedido_cabecera.Find(id);
            if (pedido_cabecera == null)
            {
                return NotFound();
            }

            return Ok(pedido_cabecera);
        }

        // PUT: api/pedido_cabeceraApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpedido_cabecera(int id, pedido_cabecera pedido_cabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pedido_cabecera.pedido_cabecera_id)
            {
                return BadRequest();
            }

            db.Entry(pedido_cabecera).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!pedido_cabeceraExists(id))
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

        // POST: api/pedido_cabeceraApi
        [ResponseType(typeof(pedido_cabecera))]
        public IHttpActionResult Postpedido_cabecera(pedido_cabecera pedido_cabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.pedido_cabecera.Add(pedido_cabecera);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pedido_cabecera.pedido_cabecera_id }, pedido_cabecera);
        }

        // DELETE: api/pedido_cabeceraApi/5
        [ResponseType(typeof(pedido_cabecera))]
        public IHttpActionResult Deletepedido_cabecera(int id)
        {
            pedido_cabecera pedido_cabecera = db.pedido_cabecera.Find(id);
            if (pedido_cabecera == null)
            {
                return NotFound();
            }

            db.pedido_cabecera.Remove(pedido_cabecera);
            db.SaveChanges();

            return Ok(pedido_cabecera);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool pedido_cabeceraExists(int id)
        {
            return db.pedido_cabecera.Count(e => e.pedido_cabecera_id == id) > 0;
        }
    }
}