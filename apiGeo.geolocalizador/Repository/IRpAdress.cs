using apiGeo.geolocalizador.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGeo.geolocalizador.Repository
{
    public interface IRpAdress
    {
        Task<string> InsertAdress(Adress adress);

        Task UpdateCoordinates(Coordinates coord);
        Task<Coordinates> GetCoordinatesId(string id);

    }
}
