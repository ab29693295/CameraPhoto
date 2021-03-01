using System;
using System.Xml;
namespace CameraPhoto.Pay
{
    public class SafeXmlDocument:XmlDocument
    {
        public SafeXmlDocument()
        {
            this.XmlResolver = null;
        }
    }
}
