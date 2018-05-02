using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace JSONWebService
{
    [DataContract]
    public class wsOrder
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public string OrderDate { get; set; }

        [DataMember]
        public string ShippedDate { get; set; }

        [DataMember]
        public string ShipName { get; set; }

        [DataMember]
        public string ShipAddress { get; set; }

        [DataMember]
        public string ShipCity { get; set; }

        [DataMember]
        public string ShipPostcode { get; set; }
    } 
}