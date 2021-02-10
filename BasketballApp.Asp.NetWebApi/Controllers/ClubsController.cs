using BasketballApp.Asp.NetWebApi.Interfaces;
using BasketballApp.Asp.NetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BasketballApp.Asp.NetWebApi.Controllers
{
    public class ClubsController : ApiController
    {


        public IClubRepository _repository { get; set; }

        public ClubsController(IClubRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Club> GetAll()
        {
            return _repository.GetAll();
        }
        [Authorize]
        [Route("api/extremes")]
        public IEnumerable<Club> Extremes()
        {
            return _repository.Extremes();
        }
        [Authorize]
        [ResponseType(typeof(Club))]
        public IHttpActionResult GetById(int id)
        {
           var club = _repository.GetById(id);

            if(club == null)
            {
                return NotFound();
            }

            return Ok(club);
        }



        
    }
}
