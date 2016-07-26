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
using Lingoine1.Models;

namespace Lingoine1.Controllers
{
    public class LanguageTablesController : ApiController
    {
        private LeapNullEntities db = new LeapNullEntities();

        // GET: api/LanguageTables
        public IQueryable<LanguageTable> GetLanguageTables()
        {
            return db.LanguageTables;
        }

        // GET: api/LanguageTables/5
        [ResponseType(typeof(LanguageTable))]
        public async Task<IHttpActionResult> GetLanguageTable(int id)
        {
            LanguageTable languageTable = await db.LanguageTables.FindAsync(id);
            if (languageTable == null)
            {
                return NotFound();
            }

            return Ok(languageTable);
        }

        // PUT: api/LanguageTables/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLanguageTable(int id, LanguageTable languageTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != languageTable.Id)
            {
                return BadRequest();
            }

            db.Entry(languageTable).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageTableExists(id))
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

        // POST: api/LanguageTables
        [ResponseType(typeof(LanguageTable))]
        public async Task<IHttpActionResult> PostLanguageTable(LanguageTable languageTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LanguageTables.Add(languageTable);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = languageTable.Id }, languageTable);
        }

        // DELETE: api/LanguageTables/5
        [ResponseType(typeof(LanguageTable))]
        public async Task<IHttpActionResult> DeleteLanguageTable(int id)
        {
            LanguageTable languageTable = await db.LanguageTables.FindAsync(id);
            if (languageTable == null)
            {
                return NotFound();
            }

            db.LanguageTables.Remove(languageTable);
            await db.SaveChangesAsync();

            return Ok(languageTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LanguageTableExists(int id)
        {
            return db.LanguageTables.Count(e => e.Id == id) > 0;
        }
    }
}