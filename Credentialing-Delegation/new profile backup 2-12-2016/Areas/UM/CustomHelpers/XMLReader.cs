using PortalTemplate.Areas.UM.ProgrammableResources.XMLModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Xml;

namespace PortalTemplate.Areas.UM.CustomHelpers
{
    public class XMLReader
    {
        public List<IDictionary<string, object>> GetQueueCols()
        {
            string xmlData = HttpContext.Current.Server.MapPath("~/Areas/UM/ProgrammableResources/DynamicQueue.xml");//Path of the xml script  
            DataSet ds = new DataSet();
            ds.ReadXml(xmlData);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(HttpContext.Current.Server.MapPath("~/Areas/UM/ProgrammableResources/DynamicQueue.xml"));
            List<IDictionary<string, object>> queuecols = new List<IDictionary<string, object>>();
            foreach (var rows in ds.Tables[0].AsEnumerable())
            {
                var i = 0;
                IDictionary<string, object> item = new Dictionary<string, object>();
                foreach (var attribute in rows.ItemArray)
                {
                    item.Add(ds.Tables[0].Columns[i].ToString(), attribute.ToString());
                    i++;
                }
                queuecols.Add(item);
            }
            List<IDictionary<string, object>> ret = new List<IDictionary<string, object>>();
            foreach (var col in queuecols)
            {

            }
            return queuecols;
        }
    }
}

