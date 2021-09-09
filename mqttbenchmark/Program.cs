using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace mqttbenchmark
{
    class Program

    {
        static async Task Main(string[] args)
        {

            List<IMqttClient> mqttClient = new List<IMqttClient>();
            var factory = new MqttFactory();
            Console.WriteLine("請輸入Broker ip:");
            string ip = Console.ReadLine();
            Console.WriteLine("請輸入Broker port:");
            int port = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("請輸入帳號:");
            string account = Console.ReadLine();
            Console.WriteLine("請輸入密碼:");
            string passwd = Console.ReadLine();
            Console.WriteLine("請輸入要連接的Client數:");
            int client = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("請輸入要Publish的次數:");
            int publish = Convert.ToInt32(Console.ReadLine());
            // TODO 程式執行時間
            
            for (int i = 0; i < client; i++)
            {
                var options = new MqttClientOptionsBuilder()
                    .WithTcpServer(ip, port)
                    .WithCredentials(account, passwd)
                    .WithClientId(Guid.NewGuid().ToString().Substring(0, 5))
                    .Build();
                mqttClient.Add(factory.CreateMqttClient());
                await mqttClient[i].ConnectAsync(options);
                //await mqttClient[i].SubscribeAsync("cute/123");
                for (int j = 0; j < publish; j++)
                {
                   await mqttClient[i].PublishAsync("cute/123", "Hi");
                }
                Console.WriteLine("(" + (i + 1) * publish + "/" + (client * publish) + ")");
            }

        }

    }

}