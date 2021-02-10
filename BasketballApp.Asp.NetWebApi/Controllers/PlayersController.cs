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
    public class PlayersController : ApiController
    {
        public IPlayerRepository _repository { get; set; }

        public PlayersController(IPlayerRepository repository)
        {
            _repository = repository;
        }


        public IEnumerable<Player> GetAll()
        {
            return _repository.GetAll();
        }

        [Authorize]
        public IEnumerable<Player> GetByBorn(int year)
        {
            return _repository.GetByBorn(year);
        }

      


        [Authorize]
        [Route("api/search")]
        public IEnumerable<Player> Search(Filter filter)
        {
            return _repository.Search(filter);
        }

        [Authorize]
        [ResponseType(typeof(Club))]
        public IHttpActionResult GetById(int id)
        {
            var player = _repository.GetById(id);

            if(player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        [Authorize]
        [ResponseType(typeof(Club))]
        public IHttpActionResult Post(Player player)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repository.Create(player);
            return CreatedAtRoute("DefaultApi", new { Id = player.Id }, player);
        }

        [Authorize]
        [ResponseType(typeof(Club))]
        public IHttpActionResult Put(int id, Player player)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != player.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(player);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(player);
        }

        [Authorize]
        [ResponseType(typeof(Club))]
        public IHttpActionResult Delete(int id)
        {
            var player = _repository.GetById(id);

            if(player == null)
            {

                return NotFound();
            }

            _repository.Delete(player);

            return Ok();

        }


    }
}
