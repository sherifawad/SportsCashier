using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace DataBase.Configuration
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyDataFixForSqlite(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().Property(x => x.Sports)
                .HasConversion(
                    x => x.ToJsonString(),
                    x => JsonSerializer.Deserialize<List<int>>(x, null)
                );
        }

        public static string ToJsonString(this List<int> list)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(list, options);

            //using var stream = new MemoryStream();
            //var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = false });
            //string json = JsonSerializer.Serialize(list, new JsonSerializerOptions { Indented = false });
            //json.WriteTo(writer);
            //writer.Flush();

            //return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}
