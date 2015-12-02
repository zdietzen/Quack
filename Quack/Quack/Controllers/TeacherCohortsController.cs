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
using AutoMapper;
using Quack.Core.Models;

namespace Quack.Controllers
{
    public class TeacherCohortsController : ApiController
    {
        private QuackDbContext db = new QuackDbContext();

        // GET: api/TeacherCohorts || Controller Method [0]
        public IEnumerable<TeacherCohortModel> GetTeacherCohorts()
        {
            return Mapper.Map<IEnumerable<TeacherCohortModel>>(db.TeacherCohorts);
        }

        // GET: api/TeacherCohorts/5 || Get By ID [1]
        [ResponseType(typeof(TeacherCohortModel))]
        public IHttpActionResult GetTeacherCohort(int id)
        {
            TeacherCohort dbTeacherCohort = db.TeacherCohorts.Find(id);
            if (dbTeacherCohort == null)
            {
                return NotFound();
            }
            TeacherCohortModel teacherCohort = Mapper.Map<TeacherCohortModel>(dbTeacherCohort);

            return Ok(teacherCohort);
        }
    }
}