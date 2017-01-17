using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AHC.CD.DataMigration
{
    class FileReader
    {
        private static XmlNode ReadXMLNode(string XPath, string filepath)
        {
            try
            {
                var xmldoc = new System.Xml.XmlDocument();

                xmldoc.Load(filepath);

                if(!String.IsNullOrEmpty(XPath))
                    return xmldoc.DocumentElement.ParentNode.SelectSingleNode(XPath);

                return xmldoc.DocumentElement;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Read(ref FileData fileData)
        {
            try
            {
                if (fileData == null)
                    throw new ArgumentNullException("fileData");

                //Check the existence of the file
                if (!File.Exists(fileData.FullFileName))
                    throw new FileNotFoundException(fileData.FullFileName);

                (fileData).XmlNode = ReadXMLNode((fileData).XPath, fileData.FullFileName);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
