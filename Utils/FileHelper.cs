using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using DataIngestion.TestAssignment.Models;

namespace DataIngestion.TestAssignment.Utils
{
    public class FileHelper
    {
        private readonly List<Album> _collection;

        public FileHelper()
        {
            _collection = new List<Album>();
        }

        public IEnumerable<Album> ParseFile()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "linkFireCollection");

            using var reader = new StreamReader(file, new UTF8Encoding(true));
            string line = reader.ReadLine();

            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                   var array = line.Split("\u0001");
                   try
                   {
                       if (!line.StartsWith("#"))
                       {
                           var album = new Album()
                           {
                               Id = (1 < array.Length) ? array[1] : string.Empty,
                               Name = (2 < array.Length) ? array[2] : string.Empty,
                               ImageUrl = (8 < array.Length) ? array[8] : string.Empty,
                               Url = (7 < array.Length) ? array[7] : string.Empty,
                               ReleaseDate = (9 < array.Length)
                                   ? DateTime.TryParse(array[9], out var release)
                                       ? release
                                       : DateTime.Now
                                   : DateTime.Now,

                               Label = (11 < array.Length) ? array[11] : string.Empty,
                               IsCompilation = (16 < array.Length) && array[16].ToLower().Contains("true")
                           };
                           _collection.Add(album);
                       }
                   }
                   catch (Exception e)
                   {
                       Console.WriteLine(e);
                       throw;
                   }
                }
            }

            if (File.Exists(file))
            {
                File.Delete(file);
            }

            return _collection;
        }
    }
}