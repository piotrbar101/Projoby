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
    internal class Program
    {
        static void Main(string[] args)
        {
            String xsdpath = "C:\\Users\\piotr\\OneDrive\\Pulpit\\borygo\\T1\\race.xsd";
            String path = "C:\\Users\\piotr\\OneDrive\\Pulpit\\borygo\\T1\\race.xml";

            if (RaceReader.validateXML(path, xsdpath))
            {
                Console.WriteLine("Validation: OK");
            }
            else
            {
                Console.WriteLine("Validation: NOT OK");
                return;
            }

            formulaT Ft = RaceReader.readRace(path);

            var events = Ft.events;
            var tts = Ft.people.tortoise;

            foreach( var e in events )
            {
                string s = e.name;
                s += " uczestnicy:";

                foreach(var pp in e.participant )
                {
                    foreach(var tt in tts)
                    {
                        if (pp == tt.startNumber) s += $" {tt.nick}";
                    }
                }
                System.Console.WriteLine(s);


            }
        }
    }
}
