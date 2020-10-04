using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace ApiGeo.geodecodificador.Entities
{
    public class Coordinates
    {
        public string IdOperacion {get;set;}
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Estado { get; set; }

    }
}
