using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiGeo.geolocalizador.Repository;
using apiGeo.geolocalizador.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiGeo.geolocalizador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeolocalizarController : ControllerBase
    {
        private IRpAdress db = new RpAdress();
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoordinates(string id) 
        {
            return Ok(await db.GetCoordinatesId(id));
        }

        [HttpPost]
        public async Task<IActionResult> geolocaliarDireccion([FromBody] Adress newAdress) {

            if (newAdress == null)
                return BadRequest();

            if (newAdress.Calle == string.Empty)
            {
                ModelState.AddModelError("Calle", "Se debe ingresar una Calle");
            }
            if (newAdress.Numero == 0)
            {
                ModelState.AddModelError("Numero", "Se debe ingresar una Numeracion de Calle");
            }
            if (newAdress.Ciudad == string.Empty)
            {
                ModelState.AddModelError("Ciudad", "Se debe ingresar una Ciudad");
            }
            if (newAdress.Provincia== string.Empty)
            {
                ModelState.AddModelError("Provincia", "Se debe ingresar una Provincia");
            }
            if (newAdress.Pais == string.Empty)
            {
                ModelState.AddModelError("Pais", "Se debe ingresar una Pais");
            }
            var res = await db.InsertAdress(newAdress);

            return Created("Direccion Ingresada", res);
        }
    }
}
