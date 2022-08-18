using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace PersonsManager
{
    public class JsonSerializeWrite
    {
        public static string JsonSerialize()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(Form1.Peoples, options);

            using (FileStream fs = new FileStream("Persons.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize<string>(fs, jsonString);
            }
            return jsonString;
        }
    }
}
