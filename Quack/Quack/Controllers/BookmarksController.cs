using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Quack.Core.Domain;
using Quack.Core.Infrastructure;
using System.Collections.Generic;
using Quack.Core.Models;
using AutoMapper;
using System;

namespace Quack.Controllers
{
    public class BookmarksController : ApiController
    {
        private QuackDbContext db = new QuackDbContext();

        // GET: api/Bookmarks || Controller Method [0]
        public IEnumerable<BookmarkModel> GetBookmarks()
        {
            return Mapper.Map<IEnumerable<BookmarkModel>>(db.Bookmarks);
        }

        // GET: api/Bookmarks/5 || Get By ID [1]
        [ResponseType(typeof(BookmarkModel))]
        public IHttpActionResult GetBookmark(int id)
        {
            Bookmark dbBookmark = db.Bookmarks.Find(id);
            if (dbBookmark == null)
            {
                return NotFound();
            }
            BookmarkModel bookmark = Mapper.Map<BookmarkModel>(dbBookmark);

            return Ok(bookmark);
        }

        // PUT: api/Bookmarks/5 || Update Bookmarks [2]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBookmark(int id, BookmarkModel bookmark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookmark.BookmarkId)
            {
                return BadRequest();
            }
            var dbBookmark = db.Bookmarks.Find(id);

            dbBookmark.Update(bookmark);

            db.Entry(dbBookmark).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookmarkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception("Unable to update the bookmark in the database");
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Bookmarks || New Bookmarks [3]
        [ResponseType(typeof(BookmarkModel))]
        public IHttpActionResult PostBookmark(BookmarkModel bookmark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbBookmark = new Bookmark();

            dbBookmark.Update(bookmark);
            db.Bookmarks.Add(dbBookmark);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Unable to add the bookmark to the database.");
            }
            bookmark.BookmarkId = dbBookmark.BookmarkId;

            return CreatedAtRoute("DefaultApi", new { id = bookmark.BookmarkId }, bookmark);
        }

        // DELETE: api/Bookmarks/5 || Delete Bookmarks [4]
        [ResponseType(typeof(BookmarkModel))]
        public IHttpActionResult DeleteBookmark(int id)
        {
            Bookmark bookmark = db.Bookmarks.Find(id);
            if (bookmark == null)
            {
                return NotFound();
            }

            db.Bookmarks.Remove(bookmark);

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Unable to delete the bookmark from the database.");
            }

            return Ok(Mapper.Map<BookmarkModel>(bookmark));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookmarkExists(int id)
        {
            return db.Bookmarks.Count(e => e.BookmarkId == id) > 0;
        }
    }
}