using Nest;

namespace DataIngestion.TestAssignment.Models
{
    [ElasticsearchType]
    public class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}