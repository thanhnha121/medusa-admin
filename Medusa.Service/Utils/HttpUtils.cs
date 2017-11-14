using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Medusa.Service.Utils
{
    public class HttpUtils
    {
        public static bool MakeJsonRequest(string requestUrl,
            object data,
            String method,
            string contentType,
            Type jsonResponseObjectType,
            out int statusCode,
            String sessionId = "",
            int timeout = 10000)
        {
            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;

            Byte[] requestObject = new Byte[0];

            //serialize the JSON object if needed
            if (data != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(data.GetType());
                serializer.WriteObject(memoryStream, data);
                String sb = Encoding.Default.GetString(memoryStream.ToArray());
                requestObject = Encoding.UTF8.GetBytes(sb);
            }
            request.Method = method;
            request.ContentType = contentType;

            // The cookie container is necessary to set and also to retrieve cookies
            request.CookieContainer = new CookieContainer();

            if (requestObject.Length != 0)
            {
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(requestObject, 0, requestObject.Length);
                requestStream.Close();
            }

            // Sets a timeout
            request.Timeout = timeout;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                
                if (response != null)
                {
                    statusCode = (int)response.StatusCode;
                    return true;
                }
            }
            statusCode = 404;
            return false;
        }
    }
}