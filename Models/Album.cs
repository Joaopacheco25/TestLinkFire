using System;
using System.Collections.Generic;
using Nest;

namespace DataIngestion.TestAssignment.Models
{
    [ElasticsearchType]
    public class Album
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Upc { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsCompilation { get; set; }
        public string Label { get; set; }
        public string ImageUrl { get; set; }
        public List<Artist> Artists { get; set; } = new List<Artist>();
    }

    [ElasticsearchType]
    public class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}