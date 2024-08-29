using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

      private static List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero
                {
                    Id = 1,
                    Name = "Spider Man",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Place = "New York City"
                } /*,
                new SuperHero
                 {
                    Id = 2,
                    Name = "Superman",
                    FirstName = "Clark",
                    LastName = "Kent",
                    Place = "Metropolis"
                },
                 new SuperHero
                  {
                    Id = 3,
                    Name = "Iron Man",
                    FirstName = "Tony",
                    LastName = "Stark",
                    Place = "UNited States Of America"
                 },
                 new SuperHero
                 {
                     Id = 4,
                     Name = "Batman",
                     FirstName = "Bruce",
                     LastName = "Wayne",
                     Place = "Gotham City"
                 }
                */
       };

        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult <List <SuperHero>>> Get()
        {
            return Ok(await context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            //var hero = heroes.Find(h => h.Id == id);
            var hero = await context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not Found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            // heroes.Add(hero);
            context.SuperHeroes.Add(hero);
            await context.SaveChangesAsync();

            return Ok(await context.SuperHeroes.ToListAsync());

        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            // var hero = heroes.Find(h => h.Id == request.Id);
            var hero = await context.SuperHeroes.FindAsync(request.Id);
            if (hero == null)
                return BadRequest("Hero not Found.");
            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            await context.SaveChangesAsync();

            return Ok(await context.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var hero = await context.SuperHeroes.FindAsync(id); 
            if (hero == null)
                return BadRequest("Hero not Found.");

            context.SuperHeroes.Remove(hero);
            await context.SaveChangesAsync();
            return Ok(await context.SuperHeroes.ToListAsync());
        }
    }

}
