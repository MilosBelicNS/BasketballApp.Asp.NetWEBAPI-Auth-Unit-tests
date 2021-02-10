using BasketballApp.Asp.NetWebApi.Interfaces;
using BasketballApp.Asp.NetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketballApp.Asp.NetWebApi.Repository
{
    public class ClubRepository : IClubRepository, IDisposable

    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Club> GetAll()
        {
            return db.Clubs;
        }

        public Club GetById(int id)
        {
            return db.Clubs.Find(id);
        }


        public IEnumerable<Club> Extremes()
        {
            var min = db.Clubs.Min(x => x.Trophies);
            var max = db.Clubs.Max(x => x.Trophies);

            var result = db.Clubs.Where(x => x.Trophies == min & max == x.Trophies).OrderBy(x => x.Trophies);

            return result;
        }

        protected void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);

        }

    }
}