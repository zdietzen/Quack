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
    public class StudentsController : ApiController
    {
        private QuackDbContext db = new QuackDbContext();

        // GET: api/Students || Controller Method [0]
        public IEnumerable<StudentModel> GetStudents()
        {
            return Mapper.Map<IEnumerable<StudentModel>>(db.Students);
        }

        // GET: api/Students/5 || Get By ID [1]
        [ResponseType(typeof(StudentModel))]
        public IHttpActionResult GetStudent(int id)
        {
            Student dbStudent = db.Students.Find(id);
            if (dbStudent == null)
            {
                return NotFound();
            }
            StudentModel student = Mapper.Map<StudentModel>(dbStudent);

            return Ok(student);
        }

        // PUT: api/Students/5 || Update Bookmarks [2]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(int id, StudentModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.StudentId)
            {
                return BadRequest();
            }
            var dbStudent = db.Students.Find(id);

            dbStudent.Update(student);

            db.Entry(student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception("Unable to update student in the database.");
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Students || New Bookmarks [3]
        [ResponseType(typeof(StudentModel))]
        public IHttpActionResult PostStudent(StudentModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbStudent = new Student();

            dbStudent.Update(student);
            db.Students.Add(dbStudent);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Unable to add the student to the database.");
            }
            student.StudentId = dbStudent.StudentId;

            return CreatedAtRoute("DefaultApi", new { id = student.StudentId }, student);
        }

        // DELETE: api/Students/5 || Delete Bookmarks [4]
        [ResponseType(typeof(StudentModel))]
        public IHttpActionResult DeleteStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Unable to delete the student from the database.");
            }

            return Ok(Mapper.Map<CohortModel>(student));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.Students.Count(e => e.StudentId == id) > 0;
        }
    }
}