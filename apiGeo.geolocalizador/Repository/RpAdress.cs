using apiGeo.geolocalizador.Core;
using apiGeo.geolocalizador.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using MongoDB.Driver;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MassTransit.Configurators;

namespace apiGeo.geolocalizador.Repository
{
    public class RpAdress : IRpAdress
    {
        internal MongoDbRepository _repository = new MongoDbRepository();
        private IMongoCollection<Adress> Collection;
        private IMongoCollection<Coordinates> CollectionCD;
        private SendMessages _send;

        public RpAdress() 
        {
            Collection = _repository.db.GetCollection<Adress>("AdressDb");
            CollectionCD = _repository.db.GetCollection<Coordinates>("AdressDb");
        }
        public async Task<Coordinates> GetCoordinatesId(string id)
        {
            var filter = Builders<Coordinates>.Filter.Eq(x => x.IdOperacion ,id);
            return await CollectionCD.FindAsync<Coordinates>(filter).Result.FirstAsync();

        }

        public async Task<string> InsertAdress(Adress adress)
        {
            await Collection.InsertOneAsync(adress);
            var coord = new Coordinates();
            var Uid = Guid.NewGuid();
            coord.IdOperacion = Uid.ToString();
            coord.Latitud = "";
            coord.Longitud = "";
            coord.Estado = "PROCESANDO";
            adress.IdOperacion = coord.IdOperacion;
            await CollectionCD.InsertOneAsync(coord);

            _send = new SendMessages();
            _send.SendMessage(adress);

            return Uid.ToString();
        }

       

        public async Task UpdateCoordinates(Coordinates coord)
        {
            var filter = Builders<Coordinates>
                .Filter
                .Eq(s => s.IdOperacion, coord.IdOperacion);
            var update = Builders<Coordinates>.Update.Set(x => x.Latitud, coord.Latitud).Set(x => x.Longitud, coord.Longitud).Set(x => x.Estado, "TERMINADO");
            await CollectionCD.UpdateOneAsync(filter, update);
        }
    }
}
