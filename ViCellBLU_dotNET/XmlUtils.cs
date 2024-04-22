using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace ViCellBLU_dotNET
{
    public class XmlUtils<T>
    {
        /// <summary>
        /// Load serialized Class from XML file.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static public T Load(String filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                Stream reader = new FileStream(filename, FileMode.Open);
                var cfg = (T)serializer.Deserialize(reader);
                reader.Close();
                reader.Dispose();
                return cfg;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return default(T);
        }

        /// <summary>
        /// Save serialized Class to XML file.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="myObj"></param>
        static public void Save(String filename, T myObj)
        {
            try
            {
                FileInfo fo = new FileInfo(filename);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                Stream writer = new FileStream(fo.FullName, FileMode.Create);
                serializer.Serialize(writer, myObj);
                writer.Close();
                writer.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Save a serialized class to a .CSV file, adding headers if needed. 
        /// </summary>
        /// <param name="file"></param>
        static public void SaveToCsv(string file, T myObj)
        {
            var saveColumnHeaders = true;
            if (File.Exists(file))
            {
                saveColumnHeaders = false;
            }

            StringWriter textWriter = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(textWriter, myObj);

            XDocument doc = XDocument.Parse(textWriter.ToString());
            StreamWriter writer = new StreamWriter(file);

            foreach (XElement data in doc.Descendants(myObj.GetType().Name))
            {
                if (saveColumnHeaders)
                {
                    string[] headers = data.Elements().Select(x => x.Name.LocalName).ToArray();
                    string strHdr = string.Join(",", headers);
                    writer.WriteLine(strHdr);
                    saveColumnHeaders = false;
                }
                string[] row = data.Elements().Select(x => x.Value).ToArray();
                string strRow = string.Join(",", row);
                writer.WriteLine(strRow);
            }

            writer.Flush();
            writer.Close();
        }

    }
}
