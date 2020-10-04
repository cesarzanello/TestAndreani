using ApiGeo.geodecodificador.Entities;
using System.Threading.Tasks;

namespace ApiGeo.geodecodificador.Core
{
    public interface IGeocoder
    {
        Task GeocoderAdress(Adress adress);
    }
}
