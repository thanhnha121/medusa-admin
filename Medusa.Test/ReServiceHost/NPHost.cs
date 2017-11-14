using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml;

namespace Medusa.Test.ReServiceHost
{
    public class NPHost : ServiceHost
    {
        private readonly Type _type;
        /// <summary>
        /// Initialize NPHost
        /// </summary>
        /// <param name="t"></param>
        /// <param name="baseAddresses"></param>
        public NPHost(Type t, Uri[] baseAddresses)
            : base(t, baseAddresses)
        {
            _type = t;
        }
        /// <summary>
        /// Open serivce host
        /// </summary>
        protected override void OnOpening()
        {
            ServiceEndpoint endPoint = AddServiceEndpoint(_type,
                new WebHttpBinding
                {
                    ReaderQuotas = new XmlDictionaryReaderQuotas
                    {
                        MaxStringContentLength = int.MaxValue
                    },
                    MaxReceivedMessageSize = int.MaxValue,
                    ReceiveTimeout = new TimeSpan(0, 10, 0),
                    SendTimeout = new TimeSpan(0, 10, 0)
                },
                string.Empty);
        }
    }
}
