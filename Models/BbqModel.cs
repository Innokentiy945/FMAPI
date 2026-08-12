
using System.Text.Json.Serialization;

namespace FMAPI.Models;

using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

[Table("restaurants")]
public class BbqModel
{
    [Column("id")]
    public long Id { get; set; }

    [Column("amenity")]
    public string Amenity { get; set; } = string.Empty;

    [Column("cuisine")]
    public string Cuisine { get; set; } = string.Empty;

    [Column("lat")]
    public double? Lat { get; set; }

    [Column("lon")]
    public double? Lon { get; set; }

    [Column("location")]
    [JsonIgnore]
    public Point? Location { get; set; }

    [Column("tags", TypeName = "jsonb")]
    public Dictionary<string, string>? Tags { get; set; } = new();
}