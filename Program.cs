using System;
using MQTTnet.Server;
using MQTTnet;
using MQTTnet.Protocol;



namespace mqttNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        
            Run();
            
        }


        public static async void Run() {
                          // Configure MQTT server.
            // Configure MQTT server.
            var optionsBuilder = new MqttServerOptionsBuilder()
                .WithConnectionBacklog(100)
                .WithDefaultEndpointPort(1884)
                .WithDefaultEndpointPort(1883);

            var options = new MqttServerOptions();
            options.ConnectionValidator = c => 
            {
                // if (c.ClientId.Length  10)
                // {
                //     c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedIdentifierRejected;
                //     return;
                // }

                if (c.Username != "user")
                {
                    c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                    return;
                }

                if (c.Password != "pass")
                {
                    c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                    return;
                }
                c.ReturnCode = MqttConnectReturnCode.ConnectionAccepted;
            };


            var mqttServer = new MqttFactory().CreateMqttServer();
        
            await  mqttServer.StartAsync(options);
            
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
            await mqttServer.StopAsync();
        }


       

    }    
}
