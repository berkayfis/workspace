using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CustomAuthentication.Models.Publications
{
    public class Article : Publication 
    {
        [BsonElement("Journal")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="Journal is required.")]
        public string Journal { get; set; }

        [BsonElement("Volume")]
        public string Volume { get; set; }

        [BsonElement("Number")]
        public string Number { get; set; }

        [BsonElement("Pages")]
        public string Pages { get; set; }

    }
}
