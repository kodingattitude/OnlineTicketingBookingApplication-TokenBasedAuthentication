using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApi_TicketBookingApplication
{
    public static class DictionayExtensions
    {
        //https://stackoverflow.com/questions/1799767/easy-way-to-convert-a-dictionarystring-string-to-xml-and-visa-versa

        static string rootName = "dictionary";
        static string itemName = "item";

        //public static Dictionary<string, object> ToDictionary(this XElement baseElm)
        //{
        //    Dictionary<string, object> dict = new Dictionary<string, object>();

        //    foreach (XElement elm in baseElm.Elements())
        //    {
        //        string dictKey = elm.Attribute("key").Value;
        //        object dictVal = elm.Attribute("value").Value;

        //        dict.Add(dictKey, dictVal);
        //    }

        //    return dict;
        //}

        //public static XElement ToXml(this Dictionary<string, object> inputDict)
        //{
        //    XElement outElm = new XElement(rootName);

        //    Dictionary<string, object>.KeyCollection keys = inputDict.Keys;

        //    foreach (string key in keys)
        //    {
        //        XElement inner = new XElement(itemName);
        //        inner.Add(new XAttribute("key", key));
        //        inner.Add(new XAttribute("value", inputDict[key]));
        //        outElm.Add(inner);
        //    }

        //    return outElm;
        //}


        //public static XElement ToXml(this string str)
        //{
        //    return XElement.Parse(str);
        //}

        public static XElement ToXml(this System.Collections.Specialized.NameValueCollection nv)
        {
            XElement outElm = new XElement(rootName);

            foreach (string key in nv.AllKeys)
            {
                XElement inner = new XElement(itemName);
                inner.Add(new XAttribute("key", key));
                inner.Add(new XAttribute("value", nv.Get(key)));
                outElm.Add(inner);
            }

            return outElm;
        }

        public static System.Collections.Specialized.NameValueCollection ToNameValueCollection(this string str)
        {
            var baseElm = XElement.Parse(str);

            var coll = new System.Collections.Specialized.NameValueCollection();

            foreach (XElement elm in baseElm.Elements())
            {
                string dictKey = elm.Attribute("key").Value;
                string dictVal = elm.Attribute("value").Value;

                coll.Add(dictKey, dictVal);
            }

            return coll;
        }


        public static Dictionary<string, object> ToDictionary(this string str)
        {
            var baseElm = XElement.Parse(str);

            Dictionary<string, object> dict = new Dictionary<string, object>();

            foreach (XElement elm in baseElm.Elements())
            {
                string dictKey = elm.Attribute("key").Value;
                object dictVal = elm.Attribute("value").Value;

                dict.Add(dictKey, dictVal);
            }

            return dict;
        }

    }
}