using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace ServerJava
{
    internal class Connect
    {
        Repository rp;
        IPHostEntry ipHost;
        IPAddress ipAddr;
        IPEndPoint ipEndPoint;
        int command;
        Socket sListener;
        public Connect()
        {
            Repository rp = new Repository();
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);
            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


        }

        public void Listener()
        { // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);
                Repository rp = new Repository(); 
                // Начинаем слушать соединения
                while (true)
                {

                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    string data = null;

                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    // Мы дождались клиента, пытающегося с нами соединиться
                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    string[] words = data.Split();
                    // работа с прилетевшими данными
                    command = Convert.ToInt32(words[0]);
                    switch (command)
                    {
                        case 0:// Добавление CREATE
                            rp.Create(words[1]);

                           /* rp.SenPlayer(words[1], Convert.ToInt32(words[2]), Convert.ToInt32(words[3]));
                            Console.WriteLine("Case 0");
                            // Отправляем ответ клиенту\

                            string reply = "Запрос обработан";
                            byte[] msg = Encoding.UTF8.GetBytes(reply);
                            handler.Send(msg);*/


                            break;
                        case 1:
                            rp.Update(words[1]);
                            
                            
                            // Редактирование Update
                           /* Console.WriteLine("Case 1 ");
                            // rp.GetPlayer();
                            byte[] msg1 = Encoding.UTF8.GetBytes(rp.GetPlayer());
                            handler.Send(msg1);*/
                            break;

                        case 2:// Удаление Delete
                            rp.Delete(words[1]);
                            

                            break;
                        case 3:// Чтение read
                            rp.Read(words[1]);
                            break;
                    }

                    
                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        Console.WriteLine("Сервер завершил соединение с клиентом.");
                        break;
                    }
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }






        }
    }
}
