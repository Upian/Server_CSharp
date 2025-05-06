using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.ObjectPool;

namespace ServerLib.TCP.Policy
{
	internal class IOHandlerPoolPolicy : IPooledObjectPolicy<IOHandler>
	{
		public IOHandlerPoolPolicy()
		{

		}

		public IOHandler Create()
		{
			var e = new IOHandler();
			return e;
		}

		public bool Return(IOHandler obj)
		{
			//반환 전 초기화
			obj.Reset();
			
			return true; //풀에 다시 넣을지 여부
		}
	}
}
