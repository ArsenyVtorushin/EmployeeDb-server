using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Listener
    {
        public async void Run()
        {
            var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();
            Console.WriteLine("Сервер запущен на порт 8080...");

            while (true)
            {
                var context = await listener.GetContextAsync();
                Task.Run(() => HandleRequest(context));
            }
        }

        public void HandleRequest(HttpListenerContext context)
        {
            string responseMessage;

            var request = context.Request;
            Console.WriteLine($"Получен запрос: {request.HttpMethod} {request.Url}");

            if (request.HttpMethod == "GET" && request.Url.LocalPath == "/GetEmployees")
            {
                responseMessage = "Here you are";
                context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                responseMessage = "Bad Request";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            // Отправка ответа
            var responseBytes = Encoding.UTF8.GetBytes(responseMessage);
            context.Response.ContentType = "text/plain";
            context.Response.ContentLength64 = responseBytes.Length;
            context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
            context.Response.OutputStream.Close();

            Console.WriteLine($"Ответ отправлен: {responseMessage}");
        }
    }
}
