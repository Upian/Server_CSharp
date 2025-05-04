using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.ObjectPool;
using ServerLib.TCP.Policy;

namespace ServerLib.TCP
{
	internal class SessionManager
	{
		private DefaultObjectPool<Session> _sessionPool;
		public SessionManager() 
		{
			Policy.SessionPoolPolicy policy = new Policy.SessionPoolPolicy();
			_sessionPool = new DefaultObjectPool<Session>(policy);
		}

		public Session CreateSession()
		{
			var session = _sessionPool.Get();
			return session;
		}

		public void ReleaseSession(Session session)
		{
			_sessionPool.Return(session);
		}
	}
}
