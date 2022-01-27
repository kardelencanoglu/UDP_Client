using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

namespace UDP_Client_Kardelen
{
    class Program
    {
        private const int BufferSize = 1024;
        static void Main(string[] args)
        {
            Console.WriteLine("Enter 'S' for server 'C' for client");

            string input = Console.ReadLine();

            if (input.Equals("S"))
            {
                Console.WriteLine("Starting Server");
                Server();
            }
            else if (input.Equals("C"))
            {
                Console.WriteLine("Starting Client");
                Client();
            }
            else
            {
                Console.WriteLine("Unexpected input. Try again!");
            }

            Console.ReadLine();
        }
            private static void Server ()
        {
            
            // Create server socket
            Socket serverSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            IPAddress serverIpAddress = IPAddress.Parse("127.0.0.1");
            int clientPortNum = 5000;
            IPEndPoint serverEndPoint = new IPEndPoint(serverIpAddress, clientPortNum);
            serverSocket.Bind(serverEndPoint);

            // Listen for incoming message
            byte[] receivedBytes = new byte[BufferSize];
            EndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            serverSocket.Receive(receivedBytes);

            // Log received message to the user
            string receivedMessage = Encoding .ASCII.GetString(receivedBytes);
            Console.WriteLine(receivedMessage);

            //Echo received message
            serverSocket.SendTo(receivedBytes, clientEndPoint);

            // Close socket
            serverSocket.Close();
        }

        private static void Client()
        {
            //step
            //creat client socket | soket yapmak

            Socket clientSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            IPAddress clientIpAddress = IPAddress.Parse("192.168.1.38");  //Ip tanımlama
            int clientPortNum = 3702;
            IPEndPoint clientEndPoint = new IPEndPoint(clientIpAddress, clientPortNum);
            clientSocket.Bind(clientEndPoint);

            //Send a message to server | soketten mesaj göndermek
            IPAddress serverIpAddress = IPAddress.Parse("192.168.1.38");
            int serverPortNum = 4702;
            IPEndPoint serverEndPoint = new IPEndPoint(serverIpAddress, serverPortNum);
            string messageToSend = "Hello World";
            byte[] bytesToSend = Encoding.ASCII.GetBytes(messageToSend);
            clientSocket.SendTo(bytesToSend, serverEndPoint);


            //listen for incoming mesaj | gelen mesajı dinlemek
            byte[] receivedBytes = new byte[BufferSize];
            clientSocket.Receive(receivedBytes);
            //Log received message to user | mesajı kullanıcıya göstermek
            string receivedMessage = Encoding.ASCII.GetString(receivedBytes);
        }
    }
}