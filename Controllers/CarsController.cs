using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using addroles.Data;
using addroles.Models;
using addroles.Utils;


namespace Tropicint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        [Authorize(Roles = AppRoles.AdminEndUser)]
        public async Task<ActionResult<IEnumerable<Cars>>> GetProducts()
        {
            var cars = await _context.Cars.ToListAsync();

            if (cars == null)
            {
                return NotFound();
            }

            return cars;
        }
    }
}