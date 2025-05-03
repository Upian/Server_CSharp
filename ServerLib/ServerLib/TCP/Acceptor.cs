using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.ObjectPool;

namespace ServerLib.TCP
{
    internal class Acceptor
    {
        private Socket? _socket = null;
        private DefaultObjectPool<SocketAsyncEventArgs>? _readEventPool = null;
        SocketAsyncEventArgs _acceptEventArg;

        public Acceptor() 
        {
        }

        public void Initialize(ushort port)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Policy.SocketAsyncEventArgPolicy policy = new Policy.SocketAsyncEventArgPolicy(1024, HandleIOCompleted);
            _readEventPool = new DefaultObjectPool<SocketAsyncEventArgs>(policy);

            _socket.Bind(new IPEndPoint(IPAddress.Any, port));
            _socket.Listen();
        }
        public void Start()
        {
            _acceptEventArg = new SocketAsyncEventArgs();
            _acceptEventArg.Completed += (sender, e) =>
            {
                this.ProcessAccept(e);
            };

            this.StartAccept();
        }
        private void StartAccept()
        {
            _acceptEventArg.AcceptSocket = null;
            if (false == _socket.AcceptAsync(_acceptEventArg))
            {
                ProcessAccept(_acceptEventArg);
            }
        }
        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            Console.WriteLine("Accept");
            if (SocketError.Success != e.SocketError ||
                null == e.AcceptSocket)
            {
                Console.WriteLine($"Accept 실패: {e.SocketError}");
                e.AcceptSocket = null;

                if (false == _socket.AcceptAsync(e))
                {
                    this.ProcessAccept(e);
                }
                return;
            }

            Socket clientSocket = e.AcceptSocket;
            var readEvent = _readEventPool.Get();
            if (null == readEvent)
            {
                Console.WriteLine("ERROR");
                return;
            }
            readEvent.UserToken = clientSocket; //나중에 ClientSession으로
            readEvent.AcceptSocket = clientSocket;
            
            StartAccept();
        }

        private void HandleIOCompleted(object sender, SocketAsyncEventArgs e)
        {
            Console.WriteLine($"IO completed: {e.SocketError}");
        }
    }
}
