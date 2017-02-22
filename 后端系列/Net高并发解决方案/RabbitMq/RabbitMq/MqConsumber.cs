using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Net高并发解决方案.RabbitMqHelper;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace RabbitMq.RabbitMq
{
    /// <summary>
    /// Mq消费者
    /// </summary>
    public class MqConsumber
    {
        /// <summary>
        /// 消费队列
        /// </summary>
        public static void ConsumeQueue()
        {
            using (var channel = MqHelper.GetConnection().CreateModel())
            {

                channel.QueueDeclare("NET", true, false, false, null);

                var consumber = new EventingBasicConsumer(channel);
                channel.BasicQos(0, 1, false);


                consumber.Received += (sender, e) =>
                {
                    try
                    {
                        var user = JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(e.Body));
                        var flag = RedisHelper.GetRedisClient().Incr(user.Id.ToString());
                        if (flag == 1)
                        {
                            //File.AppendAllText($"{System.AppDomain.CurrentDomain.BaseDirectory}/bin/info.txt", $"有效！{user.Id}的标识为{flag},姓名为{user.Name}\r\n");
                            //用户的第一次请求,为有效请求
                            //下面开始入库,这里使用List做为模拟
                            var dbContext = Model1.GetDbContext();
                            dbContext.Person.Add(new Person() { Id2 = user.Id.ToString(), Name = user.Name });
                            Task ts = dbContext.SaveChangesAsync();
                            ts.Wait();
                            //添加入库标识
                            RedisHelper.GetRedisClient().Incr($"{user.Id.ToString()}入库");

                        }
                        //else
                        // File.AppendAllText($"{System.AppDomain.CurrentDomain.BaseDirectory}/bin/info.txt", $"无效！{user.Id}的标识为{flag},姓名为{user.Name}\r\n");

                        //用户的N次请求,为无效请求
                        channel.BasicAck(e.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        File.AppendAllText($"{System.AppDomain.CurrentDomain.BaseDirectory}/bin/log.txt", ex.Message);
                    }
                };

                channel.BasicConsume("NET", false, consumber);
                //停在这里
                while (true)
                {

                }

            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}