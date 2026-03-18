using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementUnitsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MeasurementUnitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MeasurementUnits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeasurementUnits>>> GetMeasurementUnits()
        {
            return await _context.MeasurementUnits.ToListAsync();
        }

        // GET: api/MeasurementUnits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeasurementUnits>> GetMeasurementUnits(int id)
        {
            var measurementUnits = await _context.MeasurementUnits.FindAsync(id);

            if (measurementUnits == null)
            {
                return NotFound();
            }

            return measurementUnits;
        }

        // PUT: api/MeasurementUnits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeasurementUnits(int id, MeasurementUnits measurementUnits)
        {
            if (id != measurementUnits.UnitId)
            {
                return BadRequest();
            }

            _context.Entry(measurementUnits).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeasurementUnitsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MeasurementUnits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MeasurementUnits>> PostMeasurementUnits(MeasurementUnits measurementUnits)
        {
            _context.MeasurementUnits.Add(measurementUnits);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeasurementUnits", new { id = measurementUnits.UnitId }, measurementUnits);
        }

        // DELETE: api/MeasurementUnits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeasurementUnits(int id)
        {
            var measurementUnits = await _context.MeasurementUnits.FindAsync(id);
            if (measurementUnits == null)
            {
                return NotFound();
            }

            _context.MeasurementUnits.Remove(measurementUnits);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MeasurementUnitsExists(int id)
        {
            return _context.MeasurementUnits.Any(e => e.UnitId == id);
        }
    }
}
