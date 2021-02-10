using BasketballApp.Asp.NetWebApi.Interfaces;
using BasketballApp.Asp.NetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace BasketballApp.Asp.NetWebApi.Repository
{
    public class PlayerRepository : IPlayerRepository, IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Player> GetAll()
        {
            return db.Players.Include(x => x.Club).OrderBy(x => x.PointsAverage);
        }

        public Player GetById(int id)
        {
            return db.Players.Find(id);
        }

        public IEnumerable<Player> GetByBorn(int born)
        {
            var result= db.Players.Where(x => x.Born > born).OrderBy(x => x.Born);
            return result;
            
        }

        public IEnumerable<Player> Search(Filter filter)
        {
            var result = db.Players.Include(x => x.Club).Where(x => x.Matches > filter.Min & filter.Max > x.Matches).OrderByDescending(x => x.PointsAverage);
            return result;
        }

        public void Create(Player player)
        {
            db.Players.Add(player);
            db.SaveChanges();
        }

        public void Update(Player player)
        {
            db.Entry(player).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }catch(DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Player player)
        {
            db.Players.Remove(player);
            db.SaveChanges();
        }

        public void Dispose(bool disposing)
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