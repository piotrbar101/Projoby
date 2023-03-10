using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace race
{
    public class RaceReader
    {
        public static formulaT readRace(string path)
        {
            Stream file = File.OpenRead(path);
            XmlSerializer xmls = new XmlSerializer(typeof(formulaT));
            return (formulaT)xmls.Deserialize(file);
        }
        public static bool validateXML(string path, string xsdpath)
        {
            bool isValid = true;
            void ValidationHandler(Object sender, ValidationEventArgs args)
            {
                if (args.Severity == XmlSeverityType.Warning)
                {
                    Console.WriteLine("Warning: {0}", args.Message);
                }
                else
                {
                    Console.WriteLine("Error: {0}", args.Message);
                    isValid = false;
                }
            }

            XmlReaderSettings settings = new XmlReaderSettings();
            // Ustawienia walidacji
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;

            // Dodanie pliku xsd dla danej przestrzeni nazw
            // Chcemy dodać zdefiniowaną w zadaniu przestrzeń. 
            // (Tak jak w ustawieniach XML -> Schemas w VS
            settings.Schemas.Add("http://example.org/race.xsd", xsdpath);

            // Przetwarzanie schemaLocation z XSI
            // Domyślnie wyłączone ze względów bezpieczeństwa
            // (można zmusić program do przeczytania dowolnego pliku). 
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;

            // Funkcja, która będzie obsługiwać pojawiające się błędy walidacji
            // Może zostać wywołana wielokrotnie dla jednego dokumentu
            settings.ValidationEventHandler += ValidationHandler;

            XmlReader reader = XmlReader.Create(path, settings);

            // Read odczytuje kolejny element/atrybut z dokumentu. 
            // Moze spowodować wywołanie delegacji przypiętej do ValidationEventHandler. 
            while (reader.Read())
            {
            }

            return isValid;
        }

    }
}
