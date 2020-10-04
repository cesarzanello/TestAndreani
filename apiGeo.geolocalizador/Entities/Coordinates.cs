using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace apiGeo.geolocalizador.Entities
{
    public class Coordinates
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string IdOperacion {get;set;}
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Estado { get; set; }

    }
}
