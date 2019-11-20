using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Util;
using System.Xml.Serialization;

namespace KssApi.Models.Responses
{
    [XmlRoot("request")]
    public class FwPurchaseOutinorderConfirmRequest //: IFwRequest<WmsPurchaseOutinorderConfirmResponse>
    {
        [Required]
        [XmlElement("wareHouseCode")]
        public string WareHouseCode { get; set; }

        [Required]
        [XmlElement("syncId")]
        [MaxLength(50)]
        public string SyncId { get; set; }

        [XmlElement("actionType")]
        [Required]
        [MaxLength(32)]
        public string ActionType { get; set; }

        [XmlElement("orderConfirmTime")]
        [Required]
        public string OrderConfirmTime { get; set; }

        [MaxLength(200)]
        [XmlElement("remark")]
        public string Remark { get; set; }

        [XmlArray("items")]
        [XmlArrayItem("item")]
        public List<FwPurchaseOutinorderConfirmRequest.Item> Items { get; set; }


        public class Item
        {
            [XmlElement("barCode")]
            [MaxLength(64)]
            [Required]
            public string BarCode { get; set; }

            [MaxLength(32)]
            [Required]
            [XmlElement("inventoryType")]
            public string InventoryType { get; set; }

            [XmlElement("quantity")]
            public int Quantity { get; set; }

            [MaxLength(50)]
            [XmlElement("productBatch")]
            public string ProductBatch { get; set; }

            [StringLength(19)]
            [XmlElement("expireDate")]
            public string ExpireDate { get; set; }
        }
    }
}