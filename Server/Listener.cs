using Server.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Server
{
    public class Listener
    {
        public async void Run()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8080);
            server.Start();
            Console.WriteLine("Сервер запущен, ожидает подключения...");

            while (true)
            {
                var client = await server.AcceptTcpClientAsync();
                Console.WriteLine("Клиент подключился.");

                _ = HandleClientAsync(client);
            }
        }

        public async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            using (var stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Получено сообщение: {message}");

                if (message == "GetEmployees")
                {
                    var employees = DatabaseControl.GetEmployeesList();
                    string jsonResponse = JsonConvert.SerializeObject(employees, Formatting.Indented);

                    byte[] responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    Console.WriteLine("Ответ клиенту отправлен.");
                }
            }
        }
    }
}
