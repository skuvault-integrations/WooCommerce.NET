using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WooCommerceNET.WooCommerce.v3
{
    [DataContract]
    public class SystemStatus : v2.SystemStatus 
    {
        /// <summary>
        /// WooCommerce version. 
        /// read-only
        /// </summary>
        [DataMember( EmitDefaultValue = false )]
        public string version { get; set; }
    }

    [DataContract]
    public class SystemStatusTool : v2.SystemStatusTool { }
}
