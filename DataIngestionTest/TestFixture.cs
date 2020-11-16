using System;
using System.Collections.Generic;
using DataIngestion.TestAssignment.Mediator;
using DataIngestion.TestAssignment.Models;

namespace TestProject2
{
    public class TestFixture
    {
        public CollectionCommand GetCommand()
        {
            CollectionCommand collection = new CollectionCommand()
            {
                AlbumCollection = new List<Album>
                {
                    new Album
                    {
                        Id = "1", Label = "123", Upc = "12333", Url = "http://foo", Name = "Mike",
                        ImageUrl = "http://foo", IsCompilation = true, ReleaseDate = DateTime.Now, Artists =
                            new List<Artist>()
                            {
                                new Artist {Id = "1", Name = "Mike"}
                            }
                    },
                    new Album
                    {
                        Id = "2", Label = "123", Upc = "12333", Url = "http://foo", Name = "Mike",
                        ImageUrl = "http://foo", IsCompilation = true, ReleaseDate = DateTime.Now, Artists =
                            new List<Artist>()
                            {
                                new Artist {Id = "2", Name = "Mike"}
                            }
                    }
                }
            };

            return collection;
        }

        public virtual IEnumerable<Album> GetAlbunsList()
        {
            var albuns = new  List<Album>()
            {
                new Album
                {
                    Id = "1", Label = "123", Upc = "12333", Url = "http://foo", Name = "Mike",
                    ImageUrl = "http://foo", IsCompilation = true, ReleaseDate = DateTime.Now, Artists =
                        new List<Artist>()
                        {
                            new Artist {Id = "1", Name = "Mike"}
                        }
                },
                
                new Album
                {
                    Id = "2", Label = "123", Upc = "12333", Url = "http://foo", Name = "Mike",
                    ImageUrl = "http://foo", IsCompilation = true, ReleaseDate = DateTime.Now, Artists =
                        new List<Artist>()
                        {
                            new Artist {Id = "2", Name = "Mike"}
                        }
                }
            };
            return albuns;
        }
    }
}