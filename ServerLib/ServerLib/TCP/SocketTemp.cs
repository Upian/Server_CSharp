
using System.Net;
using System.Net.Sockets;

namespace ServerLib.TCP
{
	internal class SocketTemp
	{
		private System.Net.Sockets.Socket _socket;

		public SocketTemp()
		{
			_socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		public void Bind(ushort port)
		{
			_socket.Bind(new IPEndPoint(IPAddress.Any, port));
		}

		public void Listen(ushort backlog)
		{
			_socket.Listen(backlog);
		}

		public void Listen()
		{
			_socket.Listen();
		}

		public void AcceptAsync()
		{

		}


	}
}
