using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpDown.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceNew { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public int authorID { get; set; }
        public string Image { get; set; }
        public bool? IsRecommended { get; set; }
        public int? Rating { get; set; }
        public ProductStatusDTO ProductStatus { get; set; }
        public AuthorDTO Author { get; set; }
        public EffectDTO Effect { get; set; }
        public List<FormatDTO> Formats { get; set; }
        public List<InstrumentDTO> Instruments { get; set; }
        public List<SubGenreDTO> SubGenres { get; set; }
    }

    public class ProductStatusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cl { get; set; }
    }

    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class EffectDTO
    {
        public int Id { get; set; }
        public string EffectName { get; set; }
    }

    public class FormatDTO
    {
        public int Id { get; set; }
        public string FormatName { get; set; }
        public string FileName { get; set; }
    }

    public class InstrumentDTO
    {
        public int Id { get; set; }
        public string InstrumentName { get; set; }
    }

    public class GenreDTO
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
    }

    public class SubGenreDTO
    {
        public int Id { get; set; }
        public string SubGenreName { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public GenreDTO Genre { get; set; }
    }
}