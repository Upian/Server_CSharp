using Microsoft.Extensions.ObjectPool;
using System.Net.Sockets;

namespace ServerLib.TCP.Policy
{
	internal class SocketAsyncEventArgPolicy : IPooledObjectPolicy<SocketAsyncEventArgs>
	{
		private readonly int _bufferSize;
		private readonly EventHandler<SocketAsyncEventArgs> _completedHandler;

		public SocketAsyncEventArgPolicy(int bufferSize, EventHandler<SocketAsyncEventArgs> completedHandler)
		{
			_bufferSize = bufferSize;
			_completedHandler = completedHandler;
		}

		public SocketAsyncEventArgs Create()
		{
			var e = new SocketAsyncEventArgs();
			e.SetBuffer(new byte[_bufferSize], 0, _bufferSize);// 버퍼 설정
			e.Completed += _completedHandler;
			return e;
		}

		public bool Return(SocketAsyncEventArgs obj)
		{
			//반환 전 초기화
			obj.SetBuffer(obj.Buffer, 0, _bufferSize);
			obj.AcceptSocket = null;
			obj.UserToken = null;
			
			return true; //풀에 다시 넣을지 여부
		}
	}
}
