using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CachingService
{
    internal class RedisCachingService : ICachingService
    {
        //public string RedisServerHost { get; set; }
        private RedisClient redisClient = null;
        public RedisCachingService()
        {
            try
            {
                string redisHost = ConfigurationManager.AppSettings["redisHost"];
                redisClient = new RedisClient(redisHost);
            }
            catch (Exception)
            {
                //Suppress any exceptions raised
            }
        }

        public T Get<T>(string identity)
        {
            try
            {
                return redisClient.Get<T>(identity);
            }
            catch (Exception)
            {
               //Suppress any exceptions raised
            }
            return default(T);
        }

        public void Set<T>(string identity, T data)
        {
            try
            {
                redisClient.Set<T>(identity, data);
            }
            catch (Exception)
            {
               //Suppress any exceptions raised
            }
        }

        public void Remove(string identity)
        {
            try
            {
                redisClient.Remove(identity);
            }
            catch (Exception)
            {
               //Suppress any exceptions raised
            }
        }
    }
}
