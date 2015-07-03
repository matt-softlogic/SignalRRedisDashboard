using System.Configuration;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace SignalRDashboard.Common
{
    /// <summary>
    /// A singleton class used to handle communications with the Web sites SignalR hub.
    /// </summary>
    public sealed class RedisSupport
    {
        // hold a refernce to the single instance of the RedisSupport class.
        private static volatile ConnectionMultiplexer _redis;

        static RedisSupport()
        {
            // create a connection to redis
            // note that this is a disposable object, but I do not dispose of it here.
            // the connection will be used for the life of the application, so this is lazy but acceptable.
            if (_redis == null)
            {
                _redis = ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["rediServer"]);
            }

        }

        public static void SendStatusUpdate(string clientId, string systemName, string value, string status)
        {
            // invoke the SendStatus method that is defined on the hub.
            var statusUpdate = new StatusUpate
            {
                ClientId = clientId, 
                SystemName = systemName,
                Value = value,
                Status = status
            };

            string redisKey = string.Format("SystemHealth/{0}/{1}", clientId, systemName);

            _redis.GetDatabase(0).StringSet(redisKey, statusUpdate.ToJson());

            ISubscriber sub = _redis.GetSubscriber();
            sub.Publish("SignalRDashboard", statusUpdate.ToJson());

        }
    }

    public class StatusUpate
    {
        public string ClientId { get; set; }
        public string SystemName { get; set; }
        public string Value { get; set; }
        public string Status { get; set; }
        
    }

    public static class Extensions
    {
        public static string ToJson(this object input, bool indented = true)
        {
            return JsonConvert.SerializeObject(input, indented ? Formatting.Indented : Formatting.None);
        }
    }

    
}
