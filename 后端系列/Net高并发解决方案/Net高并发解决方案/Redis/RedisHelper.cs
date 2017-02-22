using ServiceStack.Redis;

namespace Net高并发解决方案.Redis
{
    /// <summary>
    /// Redis帮助类
    /// </summary>
    public class RedisHelper
    {


        public static RedisClient GetRedisClient()
        {

            return new RedisClient("127.0.0.1", 6379);

        }
    }
}