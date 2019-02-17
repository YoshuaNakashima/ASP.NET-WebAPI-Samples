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
using WebApiSample.Models;

namespace WebApiSample.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Login
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Users> GetLogins()
        {
            return db.Users;
        }

        // GET: api/Login/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionName("GetLogin")]
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> GetUsersLogin(int id)
        {
            Users users = await db.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            users.SessionId = Guid.NewGuid();
            db.Entry(users).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(users);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [ActionName("HasSession")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetIsSession(int id, string sessionId)
        {
            Users users = await db.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            Guid.TryParse(sessionId, out Guid requestGuid);

            bool result = users.SessionId == requestGuid;

            return Ok(result);
        }

        // PUT: api/Login/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLogins(int id, Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != users.Id)
            {
                return BadRequest();
            }

            db.Entry(users).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/Login
        /// <summary>
        /// 
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> PostLogins(Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(users);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsersExists(users.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = users.Id }, users);
        }

        // DELETE: api/Login/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> DeleteLogins(int id)
        {
            Users users = await db.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            db.Users.Remove(users);

            await db.SaveChangesAsync();

            return Ok(users);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool UsersExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}