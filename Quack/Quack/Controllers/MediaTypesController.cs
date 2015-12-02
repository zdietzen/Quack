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
    public class MediaTypesController : ApiController
    {
        private QuackDbContext db = new QuackDbContext();

        // GET: api/MediaTypes || Controller Method [0]
        public IEnumerable<MediaTypeModel> GetMediaTypes()
        {
            return Mapper.Map<IEnumerable<MediaTypeModel>>(db.MediaTypes);
        }

        // GET: api/MediaTypes/5 || Get By ID [1]
        [ResponseType(typeof(MediaTypeModel))]
        public IHttpActionResult GetMediaType(int id)
        {
            MediaType dbMediaType = db.MediaTypes.Find(id);
            if (dbMediaType == null)
            {
                return NotFound();
            }
            MediaTypeModel mediaType = Mapper.Map<MediaTypeModel>(dbMediaType);

            return Ok(mediaType);
        }

        // PUT: api/MediaTypes/5 || Update Bookmarks [2]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMediaType(int id, MediaTypeModel mediaType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mediaType.MediaTypeId)
            {
                return BadRequest();
            }
            var dbMediaType = db.MediaTypes.Find(id);

            dbMediaType.Update(mediaType);

            db.Entry(mediaType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediaTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception("Unable to update the media type in the database.");
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MediaTypes || New Bookmarks [3]
        [ResponseType(typeof(MediaTypeModel))]
        public IHttpActionResult PostMediaType(MediaTypeModel mediaType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbMediaType = new MediaType();

            dbMediaType.Update(mediaType);
            db.MediaTypes.Add(dbMediaType);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                throw new Exception("Unable to add the media type to database.");
            }
            mediaType.MediaTypeId = dbMediaType.MediaTypeId;

            return CreatedAtRoute("DefaultApi", new { id = mediaType.MediaTypeId }, mediaType);
        }

        // DELETE: api/MediaTypes/5 || Delete Bookmarks [4]
        [ResponseType(typeof(MediaTypeModel))]
        public IHttpActionResult DeleteMediaType(int id)
        {
            MediaType mediaType = db.MediaTypes.Find(id);
            if (mediaType == null)
            {
                return NotFound();
            }

            db.MediaTypes.Remove(mediaType);

            try
            {
                db.SaveChanges();
            }
            catch
            {
                throw new Exception("Unable to delete the mediatype from database");
            }
            return Ok(mediaType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MediaTypeExists(int id)
        {
            return db.MediaTypes.Count(e => e.MediaTypeId == id) > 0;
        }
    }
}