
using AHC.CD.Entities.ProviderInfo;
//using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AHC.CD.DataMigration
{
    class Transformer
    {
        public void Process()
        {
            FileReader reader = new FileReader();
            AHC.CD.Entities.ProviderInfo.IndividualProvider profile = new IndividualProvider();
            
            //Read the profile data
            var profileData = new FileData()
            {
                FullFileName = @"F:\Office\HealthCare\Credentialing\Xml-Profile-Data\XML Provider Profiles\Standard profile - Dr Singh.xml"
            };
            reader.Read(ref profileData);

            //Read the schema
            var schemaMapper = new FileData()
            {
                FullFileName = @"F:\Office\HealthCare\Credentialing\Source\Credentialing-Delegation\Development\AHC.CD\AHC.CD.DataMigration\XMLFiles\Schema.xml"
            };

            foreach (XmlNode item in profileData.XmlNode.ChildNodes)
            {
                schemaMapper.XPath = String.Format(".//Schema[@key='{0}']", item.Name);
                reader.Read(ref schemaMapper);

                Transform(schemaMapper.XmlNode, item.ChildNodes, ref profile);
            }           

            //Transform the file data into the schema
            

            //Store it in database
        }

        private void Transform(XmlNode schema, XmlNodeList data, ref IndividualProvider profile)
        {
            foreach (XmlNode item in schema.ChildNodes)
            {
                PopulateData(item, data, ref profile);
            }
        }

        private void PopulateData(XmlNode item, XmlNodeList data, ref IndividualProvider profile)
        {
            var className = item.Attributes["ClassName"].ToString();
            var propertyName = item.Attributes["PropertyName"].ToString();

            PropertyInfo propertyInfo = profile.GetType().GetProperty(className);            
            var propertyObject = propertyInfo.GetValue(profile);


            //if(propertyObject == null)
                //propertyInfo.SetValue(profile, Activator.CreateInstance(typeof(propertyInfo.PropertyType)));


            //propertyInfo.SetValue(ship, Convert.ChangeType(value, propertyInfo.PropertyType), null);
        }
    }
}
