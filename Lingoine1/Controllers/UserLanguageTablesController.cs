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
    public class UserLanguageTablesController : ApiController
    {
        private LeapNullEntities db = new LeapNullEntities();

        // GET: api/UserLanguageTables
        public IQueryable<UserLanguageTable> GetUserLanguageTables()
        {
            return db.UserLanguageTables;
        }

        // GET: api/UserLanguageTables/5
        [ResponseType(typeof(UserLanguageTable))]
        public async Task<IHttpActionResult> GetUserLanguageTable(string id)
        {
            UserLanguageTable userLanguageTable = await db.UserLanguageTables.FindAsync(id);
            if (userLanguageTable == null)
            {
                return NotFound();
            }

            return Ok(userLanguageTable);
        }

        // PUT: api/UserLanguageTables/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserLanguageTable(string id, UserLanguageTable userLanguageTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userLanguageTable.UserEmailId)
            {
                return BadRequest();
            }

            db.Entry(userLanguageTable).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLanguageTableExists(id))
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

        // POST: api/UserLanguageTables
        [ResponseType(typeof(UserLanguageTable))]
        public async Task<IHttpActionResult> PostUserLanguageTable(UserLanguageTable userLanguageTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserLanguageTables.Add(userLanguageTable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserLanguageTableExists(userLanguageTable.UserEmailId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userLanguageTable.UserEmailId }, userLanguageTable);
        }

        // DELETE: api/UserLanguageTables/5
        [ResponseType(typeof(UserLanguageTable))]
        public async Task<IHttpActionResult> DeleteUserLanguageTable(string id)
        {
            UserLanguageTable userLanguageTable = await db.UserLanguageTables.FindAsync(id);
            if (userLanguageTable == null)
            {
                return NotFound();
            }

            db.UserLanguageTables.Remove(userLanguageTable);
            await db.SaveChangesAsync();

            return Ok(userLanguageTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserLanguageTableExists(string id)
        {
            return db.UserLanguageTables.Count(e => e.UserEmailId == id) > 0;
        }
    }
}