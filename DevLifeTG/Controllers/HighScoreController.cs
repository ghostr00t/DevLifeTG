using DevLifeTG.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevLifeTG.Controllers
{
    public class HighScoreController : ApiController
    {
        TravieIOEntities1 db = new TravieIOEntities1();

        [HttpGet]
        public IEnumerable<HighScore> Get()
        {
            return db.HighScores.AsEnumerable();
        }

        public HighScore Get(string username)
        {
            HighScore highScore = db.HighScores.Find(username);
            if (highScore == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return highScore;
        }

        public HttpResponseMessage Post(HighScore highScore)
        {
            if (ModelState.IsValid)
            {
                db.HighScores.Add(highScore);
                db.SaveChanges();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, highScore);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { username = highScore.UserName }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        public HttpResponseMessage Put(HighScore highScore)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            db.Entry(highScore).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(string UserName)
        {
            HighScore highScore = db.HighScores.Find(UserName);
            if (highScore == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            db.HighScores.Remove(highScore);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            IEnumerable<HighScore>highScoreList = db.HighScores.AsEnumerable();
            return Request.CreateResponse(HttpStatusCode.OK, highScoreList);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
