using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NJsonSchema.Generation;
using Valheim.ThisGoesHere.Configs;

namespace Valheim.ThisGoesHere.SchemaGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var settings = new JsonSchemaGeneratorSettings();
            var generator = new JsonSchemaGenerator(settings);
            var schema = generator.Generate(typeof(Config));

            var serialized = schema.ToJson();

            Console.WriteLine(serialized);

            var file = Path.Combine("..", "..", "..", "..", "docs", "Valheim.ThisGoesHere.schema");

            File.WriteAllText(file, serialized);
        }
    }
}
