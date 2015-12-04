using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Quack.Core.Domain;
using Quack.Core.Infrastructure;
using Quack.Core.Models;
using AutoMapper;

namespace Quack.Controllers
{
    public class TeachersController : ApiController
    {
        private QuackDbContext db = new QuackDbContext();

        // GET: api/Teachers || Controller Method [0]
        public IEnumerable<TeacherModel> GetTeachers()
        {
            return Mapper.Map<IEnumerable<TeacherModel>>(db.Users);
        }

        // GET: api/Teachers/5 || Get By ID [1]
        [ResponseType(typeof(TeacherModel))]
        public IHttpActionResult GetTeacher(string id)
        {
            QuackUser dbTeacher = db.Teachers.FirstOrDefault(t => t.Id == id);

            if (dbTeacher == null)
            {
                return NotFound();
            }
            TeacherModel teacher = Mapper.Map<TeacherModel>(dbTeacher);

            return Ok(teacher);
        }

        // PUT: api/Teachers/5 || Update Bookmarks [2]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeacher(string id, TeacherModel teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacher.Id)
            {
                return BadRequest();
            }

            var dbTeacher = db.Teachers.FirstOrDefault(t => t.Id == id);
            dbTeacher.Update(teacher);

            db.Entry(dbTeacher).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception("Unable to update teacher in the database.");
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Teachers || New Bookmarks [3]
        [ResponseType(typeof(TeacherModel))]
        public IHttpActionResult PostTeacher(TeacherModel teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbTeacher = new QuackUser();

            dbTeacher.Update(teacher);
            db.Users.Add(dbTeacher);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Unable to add teacher to the database");
            }
            teacher.Id = dbTeacher.Id;

            return CreatedAtRoute("DefaultApi", new { id = teacher.Id }, teacher);
        }

        // DELETE: api/Teachers/5 || Delete Bookmarks [4]
        [ResponseType(typeof(TeacherModel))]
        public IHttpActionResult DeleteTeacher(string id)
        {
            QuackUser dbTeacher = db.Teachers.FirstOrDefault(t => t.Id == id);
            if (dbTeacher == null)
            {
                return NotFound();
            }

            db.Users.Remove(dbTeacher);

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Unable to delete teacher from the database.");
            }
         
            return Ok(Mapper.Map<TeacherModel>(dbTeacher));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeacherExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}

//commenting on this Hi Stacy was here <3 