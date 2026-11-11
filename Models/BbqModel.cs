namespace FMAPI.Models;

using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

[Table("restaurants")]
public class BbqModel
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("basic_category")]
    public string? BasicCategory { get; set; }

    [Column("cuisine")]
    public string? Cuisine { get; set; }

    [Column("confidence")]
    public string? Confidence { get; set; }

    [Column("operating_status")]
    public string? OperatingStatus { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("locality")]
    public string? Locality { get; set; }

    [Column("postcode")]
    public string? Postcode { get; set; }

    [Column("country")]
    public string? Country { get; set; }

    [Column("phones")]
    public string? Phones { get; set; }

    [Column("websites")]
    public string? Websites { get; set; }

    [Column("socials")]
    public string? Socials { get; set; }

    [Column("emails")]
    public string? Emails { get; set; }

    [Column("taxonomy_hierarchy")]
    public string? TaxonomyHierarchy { get; set; }

    [Column("brand")]
    public string? Brand { get; set; }

    [Column("sources")]
    public string? Sources { get; set; }

    [Column("latitude")]
    public decimal? Latitude { get; set; }

    [Column("longitude")]
    public decimal? Longitude { get; set; }
}