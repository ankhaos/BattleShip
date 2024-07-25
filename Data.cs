using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Морской_Бой
{
    internal class Data
    {
        DateTime _date;
        string _object;
    public Data(string Obj)
        {
            _date = DateTime.Now;
            _object = Obj;
        }
        public void WriteToFile(string p)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(p);
            XmlElement xroot = doc.DocumentElement; //главный корневой узел
           
            int i = 1;
            foreach (XmlNode node in xroot) i++;

            //создание новго элемента game
            XmlNode game = doc.CreateElement("Game");
            //создание атрибута number
            XmlAttribute number = doc.CreateAttribute("Number");
            //создание элементов datetine and winner
            XmlNode datetime = doc.CreateElement("Datetime");
            XmlNode winner = doc.CreateElement("Winner");

            //создание текстовых значений
            XmlText numberText = doc.CreateTextNode(i.ToString());
            XmlText datetimeText = doc.CreateTextNode(_date.ToUniversalTime().ToString());
            XmlText winnerText = doc.CreateTextNode(_object.ToString());

            //добавление узлов
            number.AppendChild(numberText);
            datetime.AppendChild(datetimeText); 
            winner.AppendChild(winnerText);
            game.Attributes.Append(number);
            game.AppendChild(datetime);
            game.AppendChild(winner);

            xroot.AppendChild(game);
            doc.Save(p);

            //создание xml
            //doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", "yes"));
            //XmlNode rootNode = doc.AppendChild(doc.CreateElement("Game"));
            //XmlNode NumberNode = rootNode.Attributes.Append(doc.CreateAttribute("Number"));
            //XmlNode DateNode = rootNode.AppendChild(doc.CreateElement("Datetime"));
            //XmlNode WinnerNode = rootNode.AppendChild(doc.CreateElement("Winner"));
            //doc.Save(p);
        }

    }
}
