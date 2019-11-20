
using Fw.Api.Response;
using Fw.Api.Util;
using System.Collections.Generic;
using System.Xml.Serialization;
using Fw.Api;
using Fw.Api.Request;

namespace KssService.Requests
{
    [XmlRoot("request")]
    public class FwPurchaseOutinorderAddRequest : IFwRequest<WmsPurchaseOutinorderAddResponse>
    {
        [StringRequired]
        [StringVarCharLength(32)]
        [XmlElement("wareHouseCode")]
        public string WareHouseCode { get; set; }

        [StringVarCharLength(50)]
        [XmlElement("syncId")]
        [StringRequired]
        public string SyncId { get; set; }

        [StringVarCharLength(32)]
        [StringRequired]
        [XmlElement("actionType")]
        public string ActionType { get; set; }

        [XmlElement("remark")]
        [StringLength(200)]
        public string Remark { get; set; }

        [XmlArray("items")]
        [XmlArrayItem("item")]
        public List<FwPurchaseOutinorderAddRequest.Item> Items { get; set; }

        public string GetApiName()
        {
            return "fineex.wms.purchase.outinorder.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            return (IDictionary<string, string>)new FwDictionary();
        }

        public void Validate()
        {
            RequestValidator.Validate((object)this);
        }

        public string GetPostData()
        {
            return FwUtils.XMLSerializer<FwPurchaseOutinorderAddRequest>(this);
        }

        public class Item
        {
            [StringVarCharLength(64)]
            [XmlElement("barCode")]
            [StringRequired]
            public string BarCode { get; set; }

            [XmlElement("inventoryType")]
            [StringVarCharLength(32)]
            [StringRequired]
            public string InventoryType { get; set; }

            [XmlElement("quantity")]
            public int Quantity { get; set; }

            [XmlElement("productBatch")]
            [StringVarCharLength(50)]
            public string ProductBatch { get; set; }

            [XmlElement("lineNo")]
            public string LineNo { get; set; }

            [XmlElement("unitPrice")]
            public double UnitPrice { get; set; }

            [XmlElement("totalPrice")]
            public double TotalPrice { get; set; }

        }
    }
}
