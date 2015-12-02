using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Quack.Core.Domain;
using Quack.Core.Infrastructure;
using AutoMapper;
using Quack.Core.Models;
using System;

namespace Quack.Controllers
{
    public class CohortsController : ApiController
    {
        private QuackDbContext db = new QuackDbContext();

        // GET: api/Cohorts || Controller Method [0]
        public IEnumerable<CohortModel> GetCohorts()
        {
            return Mapper.Map<IEnumerable<CohortModel>>(db.Cohorts);
        }

        // GET: api/Cohorts/5 || Get By ID [1]
        [ResponseType(typeof(CohortModel))]
        public IHttpActionResult GetCohort(int id)
        {
            Cohort dbCohort = db.Cohorts.Find(id);
            if (dbCohort == null)
            {
                return NotFound();
            }
            CohortModel cohort = Mapper.Map<CohortModel>(dbCohort);

            return Ok(cohort);
        }

        // PUT: api/Cohorts/5 || Update Bookmarks [2]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCohort(int id, CohortModel cohort)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cohort.CohortId)
            {
                return BadRequest();
            }
            var dbCohort = db.Cohorts.Find(id);

            dbCohort.Update(cohort);

            db.Entry(cohort).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CohortExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception("Unable to update cohort in the database.");
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cohorts || New Bookmarks [3]
        [ResponseType(typeof(CohortModel))]
        public IHttpActionResult PostCohort(CohortModel cohort)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbCohort = new Cohort();

            dbCohort.Update(cohort);
            db.Cohorts.Add(dbCohort);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Unable to add the cohort to the database.");
            }
            cohort.CohortId = dbCohort.CohortId;

            return CreatedAtRoute("DefaultApi", new { id = cohort.CohortId }, cohort);
        }

        // DELETE: api/Cohorts/5 || Delete Bookmarks [4]
        [ResponseType(typeof(CohortModel))]
        public IHttpActionResult DeleteCohort(int id)
        {
            Cohort cohort = db.Cohorts.Find(id);
            if (cohort == null)
            {
                return NotFound();
            }

            db.Cohorts.Remove(cohort);

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Unable to delete the cohort from database.");
            }

            return Ok(Mapper.Map<CohortModel>(cohort));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CohortExists(int id)
        {
            return db.Cohorts.Count(e => e.CohortId == id) > 0;
        }
    }
}