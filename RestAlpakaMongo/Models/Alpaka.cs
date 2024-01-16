using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RestAlpakaMongo.GenericBase;

namespace RestAlpakaMongo.Models;

public class Alpaka : BaseEntity // Inherit from BaseEntity
{
    // Properties specific to Alpaka
    public string AlpakaName { get; set; }
    public string Color { get; set; }
    public int Age { get; set; }
    public string Description { get; set; }
}