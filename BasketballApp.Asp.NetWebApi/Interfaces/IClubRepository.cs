using BasketballApp.Asp.NetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballApp.Asp.NetWebApi.Interfaces
{
    public interface IClubRepository
    {
        IEnumerable<Club> GetAll();
        Club GetById(int id);
        IEnumerable<Club> Extremes();

    }
}
