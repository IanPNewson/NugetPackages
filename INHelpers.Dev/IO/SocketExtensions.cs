using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace INHelpers.IO
{
    public static class SocketExtensions
    {

        /// <summary>
        /// Uses the global active TCP connections to see if this TCP is still esyablished
        /// </summary>
        public static bool IsConnectionEstablished(this Socket socket)
        {
            if (socket is null)
            {
                throw new ArgumentNullException(nameof(socket));
            }

            if (!socket.Connected) return false;

            var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnections = ipProperties.GetActiveTcpConnections()
                .Where(x => x.LocalEndPoint.Equals(socket.LocalEndPoint) && x.RemoteEndPoint.Equals(socket.RemoteEndPoint));

            var isConnected = false;

            if (tcpConnections != null && tcpConnections.Any())
            {
                TcpState stateOfConnection = tcpConnections.First().State;
                if (stateOfConnection == TcpState.Established)
                {
                    isConnected = true;
                }
            }

            return isConnected;
        }

    }
}
