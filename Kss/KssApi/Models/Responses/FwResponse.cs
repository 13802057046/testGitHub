using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace KssApi.Models.Responses
{
    [XmlRoot("response")]
    [Serializable]
    public class FwResponse
    {
        [XmlElement("flag")]
        public bool IsSuccess { get; set; }

        [XmlElement("code")]
        public string ErrCode { get; set; }

        [XmlElement("message")]
        public string ErrMsg { get; set; }

        public string Body { get; set; }

        public string ReqUrl { get; set; }

        public string ReqBody { get; set; }
    }
}