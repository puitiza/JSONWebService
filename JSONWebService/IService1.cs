using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace JSONWebService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {

        //BodyStyle = WebMessageBodyStyle.Wrapped
        /*
         * Este es el Formato que te bota de poner eso BodyStyle = WebMessageBodyStyle.Wrapped sino quiero que se muestro solo lo quito
                "GetAllCustomersResult": [ 
                                            {
                                            "City": "Berlin",
                                            "CompanyName": "Alfreds Futterkiste",
                                            "CustomerID": "ALFKI"
                                            },
                                            {
                                            "City": "México D.F.",
                                            "CompanyName": "Ana Trujillo Emparedados y helados",
                                            "CustomerID": "ANATR"
                                            }
                ]
         * 
         * Te tiene que quedar asi sin el BodyStyle = WebMessageBodyStyle.Wrapped
           {
              {
                "City": "Berlin",
                "CompanyName": "Alfreds Futterkiste",
                "CustomerID": "ALFKI"
              },
              {
                "City": "México D.F.",
                "CompanyName": "Ana Trujillo Emparedados y helados",
                "CustomerID": "ANATR"
              }
            }
         */

        //Esto es un metodo GET
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getAllCustomers")]
        List<wsCustomer> GetAllCustomers();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getData/{value}")]
        string GetData(string value);

        //Esto es un metodo POST
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getOrdersForCustomer/{customerID}")]
        List<wsOrder> GetOrdersForCustomer(string customerID); 


        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: agregue aquí sus operaciones de servicio
    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
