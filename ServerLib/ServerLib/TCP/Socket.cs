
using System.Net;
using System.Net.Sockets;

namespace ServerLib.TCP
{
	internal class Socket
	{
		private System.Net.Sockets.Socket _socket;

		public Socket()
		{
			_socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		public void Bind(ushort port)
		{
			_socket.Bind(new IPEndPoint(IPAddress.Any, port));
		}

		public void Listen(ushort backlog = 100)
		{
			_socket.Listen(backlog);
		}
	}
}
