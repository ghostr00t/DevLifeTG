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

        public static HighScore newUser(HighScore user)
        {
            TravieIOEntities1 db2 = new TravieIOEntities1();
            db2.HighScores.Add(user);
            db2.SaveChanges();
            return user;

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

            IEnumerable<HighScore> highScoreList = db.HighScores.AsEnumerable();
            return Request.CreateResponse(HttpStatusCode.OK, highScoreList);
        }

        //public static createUser()
        //{
        //    User user = new User();
        //    user.Username = "JsonGuy";
        //    user.Email = "jsonguy@asp.net";
        //    user.Password = "12345";


        //    string jsonString = JsonConvert.SerializeObject(user);
        //    Console.WriteLine(jsonString);
        //    Output: { "Username":"JsonGuy","Email":"jsonguy@asp.net","Password":"12345" }


        //    string jsonString = @"{'Username':'JsonGuy','Email':'jsonguy@asp.net','Password':'12345'}";
        //    User objUser = JsonConvert.DeserializeObject<User>(jsonString);
        //    Console.WriteLine(objUser.Email);
        //    Output: jsonguy @asp.net
        //}


        //public static createUserList()
        //{
        //IList<User> users = new List<User>() {
        //    new User() { Username = "JsonGuy", Email = "jsonguy@asp.net", Password = 12345} ,
        //    new User() { Username = "CanCode", Email = "icancode@earthlink.net", Password = 21444} ,
        //    new User() { Username = "TGisCool", Email = "tgcool@compuserve.com", Password = 18564} ,
        //    new User() { Username = "alltheRam", Email = "2muchram@prodigy.com" , Password = 20444} ,
        //    new User() { Username = "steve", Email = "steve@juno.com" , Password = 15754}
        //};

        //    var selected = users.Where(u => u.Email
        //            .Any(a => listOfSearchedEmails.Contains(a.Email))
        //            .ToList();

        //    foreach (string Email in users)
        //    {
        //        if ((user.Email).Contains("asp.net"))
        //        {
        //            System.Text.StringBuilder BuildThatString = new StringBuilder("I be making strings!");
        //            Console.WriteLine(user.Username + "is pretty cool");
        //            Console.ReadLine();
        //        }
        //    }
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}
