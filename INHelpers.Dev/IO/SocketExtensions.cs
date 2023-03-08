using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                .Where(x => x.LocalEndPoint is IPEndPoint && x.RemoteEndPoint is IPEndPoint && x.LocalEndPoint != null && x.RemoteEndPoint != null)
                .Where(x => AreEndpointsEqual(x.LocalEndPoint, (IPEndPoint)socket.LocalEndPoint!) && AreEndpointsEqual(x.RemoteEndPoint, (IPEndPoint)socket.RemoteEndPoint!));

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

        public static bool AreEndpointsEqual(IPEndPoint left, IPEndPoint right)
        {
            if (left.AddressFamily == AddressFamily.InterNetwork &&
                right.AddressFamily == AddressFamily.InterNetworkV6)
            {
                left = new IPEndPoint(left.Address.MapToIPv6(), left.Port);
            }

            if (left.AddressFamily == AddressFamily.InterNetworkV6 &&
                right.AddressFamily == AddressFamily.InterNetwork)
            {
                right = new IPEndPoint(right.Address.MapToIPv6(), right.Port);
            }

            return left.Equals(right);
        }
    }
}
