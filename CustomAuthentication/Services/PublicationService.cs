using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibTeXLibrary;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CustomAuthentication.Services
{
    public class PublicationService
    {
        private readonly IMongoCollection<BibEntry> _entries;
        public PublicationService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("FinalProjectDB"));
            var database = client.GetDatabase("FinalProjectDB");
            _entries = database.GetCollection<BibEntry>("Entries");
        }

        public List<BibEntry> GetAllEntriesFromFile(string filePath)
        {
            BibParser parser = new BibParser(new StreamReader(filePath, Encoding.Default));
            return parser.GetAllResult();
        }

    }
}
