using FilesUrl.Common.Redis;
using FilesUrl.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilesUrl.Services
{
    public class RedisService : IRedisService
    {

        IRedisCacheManager redisManager;

        public RedisService(IRedisCacheManager redisManager)
        {
            this.redisManager = redisManager;
        }

        public string getCode(string key)
        {
            return redisManager.Get<String>(key);
        }

        public bool generateCode(String key, String code)
        {

            redisManager.Set(key, code, TimeSpan.FromMinutes(5));

            return true;
        }

        public string getString()
        {
            throw new NotImplementedException();
        }
    }
}