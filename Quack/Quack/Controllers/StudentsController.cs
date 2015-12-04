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
            var role = db.Roles.FirstOrDefault(r => r.Name == "Student");

            if(role == null)
            {
                role = db.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "Student" });
                db.SaveChanges();
            }

            return Mapper.Map<IEnumerable<StudentModel>>(db.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id)));
        }

        // GET: api/Students/5 || Get By ID [1]
        [ResponseType(typeof(StudentModel))]
        public IHttpActionResult GetStudent(int id)
        {
            QuackUser dbStudent = db.Users.Find(id);
            if (dbStudent == null)
            {
                return NotFound();
            }
            StudentModel student = Mapper.Map<StudentModel>(dbStudent);

            return Ok(student);
        }

        // PUT: api/Students/5 || Update Bookmarks [2]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(string id, StudentModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.Id)
            {
                return BadRequest();
            }
            var dbStudent = db.Users.Find(id);

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
            var dbStudent = new QuackUser();

            dbStudent.Update(student);

            db.AddUserToRole(dbStudent, "Student");

            db.Users.Add(dbStudent);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Unable to add the student to the database.");
            }
            student.Id = dbStudent.Id;

            return CreatedAtRoute("DefaultApi", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5 || Delete Bookmarks [4]
        [ResponseType(typeof(StudentModel))]
        public IHttpActionResult DeleteStudent(int id)
        {
            QuackUser student = db.Users.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            db.Users.Remove(student);
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

        private bool StudentExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}