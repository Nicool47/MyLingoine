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
using System.Data.Entity.Core.Objects;

namespace Lingoine1.Controllers
{
    [RoutePrefix("api/UserTables")]
    public class UserTablesController : ApiController
    {
        private LeapNullEntities db = new LeapNullEntities();

        [Route("")]
        // GET: api/UserTables
        public IQueryable<UserTable> GetUserTables()
        {
            return db.UserTables;
        }



        //// GET: api/UserTables/5
        //[ResponseType(typeof(UserTable))]
        //public async Task<IHttpActionResult> GetUserTable(string id)
        //{
        //    UserTable userTable = await db.UserTables.FindAsync(id);
        //    if (userTable == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(userTable);
        //}

        [Route("{email}/")]

        public IQueryable<UserTable> GetUserByName(string email)
        {
            return db.UserTables.Where(b => b.Email == email);
        }

        [Route("{learnerEmail}/{LanguageName}/{premium}")]
        public Object GetTeacherEmail(string learnerEmail, string LanguageName, int premium)
        {
            var teacherE=0;
            using (var context = new LeapNullEntities())
            {
                ObjectParameter teacherEmail = null;
                try
                {
                    if(premium == 1)
                    {
                        teacherEmail = new ObjectParameter("TeacherSkype", typeof(string));
                        teacherE = context.sp_AssignTeacher(learnerEmail, LanguageName, teacherEmail);
                    }else {
                        teacherEmail = new ObjectParameter("NTeacherSkype", typeof(string));
                        teacherE = context.Normal_UserAssign(learnerEmail, LanguageName, teacherEmail);
                    }
                    
                    Console.WriteLine("TeacherE: "+teacherE);
                }
                catch (Exception es) {
                    Console.WriteLine(es.StackTrace);
                }

                Console.WriteLine(teacherEmail);
                return teacherEmail.Value;
            }

            //return db.UserTables.Where(b => b.Email == email);
        }




        [Route("{id}/")]

        // PUT: api/UserTables/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserTable(string id, UserTable userTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userTable.Email)
            {
                return BadRequest();
            }

            db.Entry(userTable).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTableExists(id))
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

        // POST: api/UserTables
        [ResponseType(typeof(UserTable))]
        [Route("")]
        public async Task<IHttpActionResult> PostUserTable(UserTable userTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserTables.Add(userTable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserTableExists(userTable.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userTable.Email }, userTable);
        }

        [Route("{id}/")]
        // DELETE: api/UserTables/5
        [ResponseType(typeof(UserTable))]
        public async Task<IHttpActionResult> DeleteUserTable(string id)
        {
            UserTable userTable = await db.UserTables.FindAsync(id);
            if (userTable == null)
            {
                return NotFound();
            }

            db.UserTables.Remove(userTable);
            await db.SaveChangesAsync();

            return Ok(userTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserTableExists(string id)
        {
            return db.UserTables.Count(e => e.Email == id) > 0;
        }
    }
}