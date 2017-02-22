using System;
using System.Collections.Generic;
using System.IO;

using System.Text;
using System.Threading.Tasks;
using EF;
using MqConsumer.RabbitMq;
using Net高并发解决方案.Models;
using Net高并发解决方案.Redis;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MqConsumer
{
    class Program
    {
        static void Main(string[] args)
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
                            //用户的第一次请求,为有效请求
                            //下面开始入库,这里使用List做为模拟
                            Console.WriteLine($"{user.Id}标识为{flag} {user.Name}");
                            var dbContext = new Model1();
                            dbContext.Person.Add(new Person() { Id2 = user.Id.ToString(), Name = user.Name });
                            Task ts = dbContext.SaveChangesAsync();
                            ts.Wait();
                            //添加入库标识
                            RedisHelper.GetRedisClient().Incr($"{user.Id.ToString()}入库");

                            Console.WriteLine("入库成功");

                        }

                        //用户的N次请求,为无效请求
                        channel.BasicAck(e.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        File.AppendAllText($"{System.AppDomain.CurrentDomain.BaseDirectory}/bin/log.txt", ex.Message);
                    }
                };
                Console.WriteLine("开始工作");

                channel.BasicConsume("NET", false, consumber);
                Console.ReadKey();
            }
        }
    }
}
