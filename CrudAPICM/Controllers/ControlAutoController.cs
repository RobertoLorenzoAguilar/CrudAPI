using CrudAPICM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.Xml;

namespace CrudAPICM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlAutoController : Controller
    {
        private static ControlAutosDbContext _contexControlAuto;
        public ControlAutoController(ControlAutosDbContext context)
        {
            _contexControlAuto = context;
        }



        #region Ruta
        // GET: api/GetRutas
        [HttpGet("GetRutas")]
        public async Task<ActionResult<IEnumerable<Ruta>>> GetRutas()
        {            
            return await _contexControlAuto.Ruta.ToListAsync();
        }

        [HttpGet("GetRutaId")]
        public async Task<ActionResult<Ruta>> GetRutaId(int id)
        {            
            var ruta = await _contexControlAuto.Ruta
            .Include(r => r.DestinoRuta) // Incluye la entidad relacionada PuntosDeControl
            .FirstOrDefaultAsync(r => r.IdRuta == id); // Realiza la consulta por ID

            

            if (ruta == null)
            {
                return NotFound();
            }
            return ruta;
        }

        [HttpPost("GuardarRuta")]
        public async Task<ActionResult<Ruta>> GuardarRuta(Ruta objRuta)
        {               
            _contexControlAuto.Ruta.Add(objRuta);            
            await _contexControlAuto.SaveChangesAsync();
            return CreatedAtAction("GetRutas", new { id = objRuta.IdRuta }, objRuta);
        }


        [HttpPut("ActualizarRuta")]
        public async Task<IActionResult> ActualizarRuta(long id, Ruta objRuta)
        {
            if (id != objRuta.IdRuta)
            {
                return BadRequest();
            }

            _contexControlAuto.Entry(objRuta).State = EntityState.Modified;            

            try
            {                
                var lstDestino = await _contexControlAuto.DestinoRuta.Where(d=> d.IdRuta==id).ToListAsync();
                if (lstDestino != null)
                {
                    foreach (var itemDestino in lstDestino)
                    {
                        foreach (var itemRuta in objRuta.DestinoRuta)
                        {
                            if (itemDestino.IdDestino == itemRuta.IdDestino)
                            {
                                itemDestino.NombreDestino = itemRuta.NombreDestino;
                                itemDestino.RutaDestino = itemRuta.RutaDestino;
                            }
                            else if(itemRuta.IdDestino == 0){

                                _contexControlAuto.DestinoRuta.Add(itemRuta);
                            }
                        }
                    }

                }

                    await _contexControlAuto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RutaExists(id))
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


        private bool RutaExists(long id)
        {
            return _contexControlAuto.Ruta.Any(e => e.IdRuta == id);
        }


        // DELETE: api/DeleteRuta/5
        [HttpDelete("DeleteRuta")]
        public async Task<IActionResult> DeleteRuta(int id)
        {
            var objRuta = await _contexControlAuto.Ruta.FindAsync(id);
            if (objRuta == null)
            {
                return NotFound();
            }

            _contexControlAuto.Ruta.Remove(objRuta);
            await _contexControlAuto.SaveChangesAsync();

            return NoContent();
        }
        #endregion Ruta

        # region Destino
        // GET: api/GetRutaId/5        
        [HttpGet("GetDestinoIdRuta")]
        public async Task<ActionResult<IEnumerable<DestinoRuta>>> GetDestinoIdRuta(int id)
        {
            var ruta = await _contexControlAuto.DestinoRuta.Where(r => r.IdRuta == id).ToListAsync();
            if (ruta.Count() == 0)
            {
                return NotFound();
            }
            return ruta;
        }

        // DELETE: api/DeleteRuta/5
        [HttpDelete("DeleteDestino")]
        public async Task<IActionResult> DeleteDestino(int id)
        {
            var objDestino = await _contexControlAuto.DestinoRuta.FindAsync(id);
            if (objDestino == null)
            {
                return NotFound();
            }
            _contexControlAuto.DestinoRuta.Remove(objDestino);
            await _contexControlAuto.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("ActualizarDestino")]
        public async Task<IActionResult> ActualizarDestino(long id, DestinoRuta objDestino)
        {
            if (id != objDestino.IdDestino)
            {
                return BadRequest();
            }
            try
            {
                var objDestinoRuta = await _contexControlAuto.DestinoRuta.Where(d => d.IdDestino == id).FirstOrDefaultAsync();
                if (objDestinoRuta != null)
                {

                    if (objDestinoRuta.IdDestino == objDestino.IdDestino)
                    {
                        objDestinoRuta.NombreDestino = objDestino.NombreDestino;
                        objDestinoRuta.RutaDestino = objDestino.RutaDestino;
                    }

                }
                await _contexControlAuto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RutaExists(id))
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
        #endregion Destino

    }
}
