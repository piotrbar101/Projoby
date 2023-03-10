using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ClassLibrary1
{
    public static class LibraryReader
    {
        public static typeLibrary ReadLibrary(string path)
        {

            XmlReaderSettings settings = new XmlReaderSettings();
            // Ustawienia walidacji
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;

            // Dodanie pliku xsd dla danej przestrzeni nazw
            // Chcemy dodać zdefiniowaną w zadaniu przestrzeń. 
            // (Tak jak w ustawieniach XML -> Schemas w VS
            settings.Schemas.Add("http://example.org/kb/library", "C:\\Users\\piotr\\OneDrive\\Pulpit\\4 semestr\\Projektowanie obiektowe\\Lab01\\ClassLibrary1\\XMLSchema1.xsd");

            // Przetwarzanie schemaLocation z XSI
            // Domyślnie wyłączone ze względów bezpieczeństwa
            // (można zmusić program do przeczytania dowolnego pliku). 
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;

            // Funkcja, która będzie obsługiwać pojawiające się błędy walidacji
            // Może zostać wywołana wielokrotnie dla jednego dokumentu
            settings.ValidationEventHandler += ValidationHandler;

            //XmlReader reader = XmlReader.Create(path, settings);

            // Read odczytuje kolejny element/atrybut z dokumentu. 
            // Moze spowodować wywołanie delegacji przypiętej do ValidationEventHandler. 
            //while (reader.Read())
            //{
            //}
            XmlReader reader = XmlReader.Create(path, settings);
            while (reader.Read()) { }

            XmlSerializer serializer = new XmlSerializer(typeof(typeLibrary));
            typeLibrary lib;
            using (reader = XmlReader.Create(path))
            {
                lib = (typeLibrary)serializer.Deserialize(reader);
            }
            return lib;
        }

        private static void ValidationHandler(Object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("Warning: {0}", args.Message);
            else
                Console.WriteLine("Error: {0}", args.Message);
        }
    }
    public class Class1
    {

    }
}