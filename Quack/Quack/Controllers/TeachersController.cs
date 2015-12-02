using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            return Mapper.Map<IEnumerable<TeacherModel>>(db.Teachers);
        }

        // GET: api/Teachers/5 || Get By ID [1]
        [ResponseType(typeof(TeacherModel))]
        public IHttpActionResult GetTeacher(int id)
        {
            Teacher dbTeacher = db.Teachers.Find(id);
            if (dbTeacher == null)
            {
                return NotFound();
            }
            TeacherModel teacher = Mapper.Map<TeacherModel>(dbTeacher);

            return Ok(teacher);
        }

        // PUT: api/Teachers/5 || Update Bookmarks [2]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeacher(int id, TeacherModel teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacher.TeacherId)
            {
                return BadRequest();
            }
            var dbTeacher = db.Teachers.Find(id);
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
            var dbTeacher = new Teacher();

            dbTeacher.Update(teacher);
            db.Teachers.Add(dbTeacher);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Unable to add teacher to the database");
            }
            teacher.TeacherId = dbTeacher.TeacherId;

            return CreatedAtRoute("DefaultApi", new { id = teacher.TeacherId }, teacher);
        }

        // DELETE: api/Teachers/5 || Delete Bookmarks [4]
        [ResponseType(typeof(TeacherModel))]
        public IHttpActionResult DeleteTeacher(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return NotFound();
            }

            db.Teachers.Remove(teacher);

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Unable to delete teacher from the database.");
            }
         
            return Ok(Mapper.Map<TeacherModel>(teacher));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeacherExists(int id)
        {
            return db.Teachers.Count(e => e.TeacherId == id) > 0;
        }
    }
}

//commenting on this Hi Stacy was here <3 