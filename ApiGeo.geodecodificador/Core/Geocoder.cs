using ApiGeo.geodecodificador.Entities;
using Nominatim.API.Geocoders;
using Nominatim.API.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiGeo.geodecodificador.Core
{
    public class Geocoder : IGeocoder
    {
        public async Task GeocoderAdress(Adress adress)
        {

            var a = new ForwardGeocoder();
            var r = a.Geocode(new ForwardGeocodeRequest
            {
                queryString = string.Format("{0} {1},{2},{3},{4}",adress.Calle,adress.Numero,adress.Ciudad,adress.Provincia,adress.Pais),

                BreakdownAddressElements = true,
                ShowExtraTags = true,
                ShowAlternativeNames = true,
                ShowGeoJSON = true
            });
            r.Wait();


            var coord = new Coordinates();
            coord.Latitud = r.Result[0].Latitude.ToString();
            coord.Longitud = r.Result[0].Longitude.ToString();
            coord.IdOperacion = adress.IdOperacion;
            coord.Estado = "TERMINADO";
            SendMessage(coord);
        }

        private static void SendMessage(Coordinates coord)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "mensajeRetornoQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonSerializer.Serialize(coord);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "mensajeRetornoQueue",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
