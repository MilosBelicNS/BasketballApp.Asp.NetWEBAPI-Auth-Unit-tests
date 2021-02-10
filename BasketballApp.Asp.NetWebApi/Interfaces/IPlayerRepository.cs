using BasketballApp.Asp.NetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballApp.Asp.NetWebApi.Interfaces
{
    public interface IPlayerRepository
    {

        IEnumerable<Player> GetAll();
        Player GetById(int id);
        IEnumerable<Player> GetByBorn(int born);
        IEnumerable<Player> Search(Filter filter);
        void Create(Player player);
        void Update(Player player);
        void Delete(Player player);
    }
}
