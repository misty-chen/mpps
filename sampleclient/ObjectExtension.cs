using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace sampleclient
{
    public static class ObjectExtension
    {
       
        public static string ToJSON(this Object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
       
    }
}
