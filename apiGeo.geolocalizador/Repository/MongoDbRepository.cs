using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace apiGeo.geolocalizador.Repository
{
    public class MongoDbRepository
    {
        public MongoClient client;
        public IMongoDatabase db;
        public MongoDbRepository()
        {
            client = new MongoClient("mongodb://127.0.0.1:27889");
            db = client.GetDatabase("AdressDb");


        }
    }
}
