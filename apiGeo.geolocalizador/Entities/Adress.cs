using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace apiGeo.geolocalizador.Entities
{
    public class Adress
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Calle { get; set; }
        public int  Numero { get; set; }
        public string Ciudad { get; set; }
        public string Codigo_Postal { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public string IdOperacion { get; set; }
 
    }
}
