using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomAuthentication.Models
{
    public abstract class Publication
    {
        [BsonElement("Title")]
        [Required]
        public string Title { get; set; }

        [BsonElement("Author")]
        [Required]
        public List<string> Author { get; set; }

        [BsonElement("Year")]
        [Required]
        public string Year { get; set; }

    }
}
