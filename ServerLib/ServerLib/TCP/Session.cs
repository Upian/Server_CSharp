using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace ServerLib.TCP
{
	public abstract class BaseSession
	{
		protected abstract void HandleSend(SocketAsyncEventArgs e);
		protected abstract void HandleRecieve(SocketAsyncEventArgs e);
	}

	//연결된 클라이언트 객체
	//상속하여 사용
	public class Session : BaseSession
	{
		private SocketAsyncEventArgs _socketAsyncEventArg;
		
		public Session() 
		{
			_socketAsyncEventArg = new SocketAsyncEventArgs();
			_socketAsyncEventArg.SetBuffer(new byte[Common.BufferSize], 0, Common.BufferSize);// 버퍼 설정

			_socketAsyncEventArg.Completed += this.HandleIOCompleted;
		}

		//풀 반환시 초기화
		public void Reset()
		{
			_socketAsyncEventArg.SetBuffer(_socketAsyncEventArg.Buffer, 0, Common.BufferSize);
			_socketAsyncEventArg.AcceptSocket = null;
			_socketAsyncEventArg.UserToken = null;
		}

		//초기 설정
		public void Initialize(Socket socket)
		{
			_socketAsyncEventArg.AcceptSocket = socket;
			_socketAsyncEventArg.UserToken = socket;
		}
		private void HandleIOCompleted(object sender, SocketAsyncEventArgs e)
		{
			switch (e.LastOperation)
			{
				case SocketAsyncOperation.Receive:
					this.HandleRecieve(e);
					break;
				case SocketAsyncOperation.Send:
					this.HandleSend(e);
					break;
			}
		}

		public void Start()
		{
			bool result = _socketAsyncEventArg.AcceptSocket.ReceiveAsync(_socketAsyncEventArg);
			if (false == result)
				this.HandleRecieve(_socketAsyncEventArg);
		}

		protected override void HandleSend(SocketAsyncEventArgs e)
		{
		}
		protected override void HandleRecieve(SocketAsyncEventArgs e)
		{ 
		}
	}
}
