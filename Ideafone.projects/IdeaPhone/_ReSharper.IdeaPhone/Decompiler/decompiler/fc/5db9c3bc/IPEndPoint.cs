// Type: System.Net.IPEndPoint
// Assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.dll

using System;
using System.Globalization;
using System.Net.Sockets;
using System.Runtime;

namespace System.Net
{
  /// <summary>
  /// Represents a network endpoint as an IP address and a port number.
  /// </summary>
  [Serializable]
  public class IPEndPoint : EndPoint
  {
    internal static IPEndPoint Any = new IPEndPoint(IPAddress.Any, 0);
    internal static IPEndPoint IPv6Any = new IPEndPoint(IPAddress.IPv6Any, 0);
    /// <summary>
    /// Specifies the minimum value that can be assigned to the <see cref="P:System.Net.IPEndPoint.Port"/> property. This field is read-only.
    /// </summary>
    public const int MinPort = 0;
    /// <summary>
    /// Specifies the maximum value that can be assigned to the <see cref="P:System.Net.IPEndPoint.Port"/> property. The MaxPort value is set to 0x0000FFFF. This field is read-only.
    /// </summary>
    public const int MaxPort = 65535;
    internal const int AnyPort = 0;
    private IPAddress m_Address;
    private int m_Port;

    /// <summary>
    /// Gets the Internet Protocol (IP) address family.
    /// </summary>
    /// 
    /// <returns>
    /// Returns <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork"/>.
    /// </returns>
    public override AddressFamily AddressFamily
    {
      get
      {
        return this.m_Address.AddressFamily;
      }
    }

    /// <summary>
    /// Gets or sets the IP address of the endpoint.
    /// </summary>
    /// 
    /// <returns>
    /// An <see cref="T:System.Net.IPAddress"/> instance containing the IP address of the endpoint.
    /// </returns>
    public IPAddress Address
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.m_Address;
      }
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set
      {
        this.m_Address = value;
      }
    }

    /// <summary>
    /// Gets or sets the port number of the endpoint.
    /// </summary>
    /// 
    /// <returns>
    /// An integer value in the range <see cref="F:System.Net.IPEndPoint.MinPort"/> to <see cref="F:System.Net.IPEndPoint.MaxPort"/> indicating the port number of the endpoint.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The value that was specified for a set operation is less than <see cref="F:System.Net.IPEndPoint.MinPort"/> or greater than <see cref="F:System.Net.IPEndPoint.MaxPort"/>. </exception>
    public int Port
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.m_Port;
      }
      set
      {
        if (!ValidationHelper.ValidateTcpPort(value))
          throw new ArgumentOutOfRangeException("value");
        this.m_Port = value;
      }
    }

    static IPEndPoint()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Net.IPEndPoint"/> class with the specified address and port number.
    /// </summary>
    /// <param name="address">The IP address of the Internet host. </param><param name="port">The port number associated with the <paramref name="address"/>, or 0 to specify any available port. <paramref name="port"/> is in host order.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="port"/> is less than <see cref="F:System.Net.IPEndPoint.MinPort"/>.-or- <paramref name="port"/> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort"/>.-or- <paramref name="address"/> is less than 0 or greater than 0x00000000FFFFFFFF. </exception>
    public IPEndPoint(long address, int port)
    {
      if (!ValidationHelper.ValidateTcpPort(port))
        throw new ArgumentOutOfRangeException("port");
      this.m_Port = port;
      this.m_Address = new IPAddress(address);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Net.IPEndPoint"/> class with the specified address and port number.
    /// </summary>
    /// <param name="address">An <see cref="T:System.Net.IPAddress"/>. </param><param name="port">The port number associated with the <paramref name="address"/>, or 0 to specify any available port. <paramref name="port"/> is in host order.</param><exception cref="T:System.ArgumentNullException"><paramref name="address"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="port"/> is less than <see cref="F:System.Net.IPEndPoint.MinPort"/>.-or- <paramref name="port"/> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort"/>.-or- <paramref name="address"/> is less than 0 or greater than 0x00000000FFFFFFFF. </exception>
    public IPEndPoint(IPAddress address, int port)
    {
      if (address == null)
        throw new ArgumentNullException("address");
      if (!ValidationHelper.ValidateTcpPort(port))
        throw new ArgumentOutOfRangeException("port");
      this.m_Port = port;
      this.m_Address = address;
    }

    /// <summary>
    /// Returns the IP address and port number of the specified endpoint.
    /// </summary>
    /// 
    /// <returns>
    /// A string containing the IP address and the port number of the specified endpoint (for example, 192.168.1.2:80).
    /// </returns>
    /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public override string ToString()
    {
      return string.Format(this.m_Address.AddressFamily != AddressFamily.InterNetworkV6 ? "{0}:{1}" : "[{0}]:{1}", (object) this.m_Address.ToString(), (object) this.Port.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo));
    }

    /// <summary>
    /// Serializes endpoint information into a <see cref="T:System.Net.SocketAddress"/> instance.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Net.SocketAddress"/> instance containing the socket address for the endpoint.
    /// </returns>
    public override SocketAddress Serialize()
    {
      return new SocketAddress(this.Address, this.Port);
    }

    /// <summary>
    /// Creates an endpoint from a socket address.
    /// </summary>
    /// 
    /// <returns>
    /// An <see cref="T:System.Net.EndPoint"/> instance using the specified socket address.
    /// </returns>
    /// <param name="socketAddress">The <see cref="T:System.Net.SocketAddress"/> to use for the endpoint. </param><exception cref="T:System.ArgumentException">The AddressFamily of <paramref name="socketAddress"/> is not equal to the AddressFamily of the current instance.-or- <paramref name="socketAddress"/>.Size &lt; 8. </exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
    public override EndPoint Create(SocketAddress socketAddress)
    {
      if (socketAddress.Family != this.AddressFamily)
      {
        throw new ArgumentException(SR.GetString("net_InvalidAddressFamily", (object) ((object) socketAddress.Family).ToString(), (object) this.GetType().FullName, (object) ((object) this.AddressFamily).ToString()), "socketAddress");
      }
      else
      {
        if (socketAddress.Size >= 8)
          return (EndPoint) socketAddress.GetIPEndPoint();
        throw new ArgumentException(SR.GetString("net_InvalidSocketAddressSize", (object) socketAddress.GetType().FullName, (object) this.GetType().FullName), "socketAddress");
      }
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Net.IPEndPoint"/> instance.
    /// </summary>
    /// 
    /// <returns>
    /// true if the specified object  is equal to the current object; otherwise, false.
    /// </returns>
    /// <param name="comparand">The specified <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Net.IPEndPoint"/> instance.</param>
    public override bool Equals(object comparand)
    {
      if (!(comparand is IPEndPoint) || !((object) ((IPEndPoint) comparand).m_Address).Equals((object) this.m_Address))
        return false;
      else
        return ((IPEndPoint) comparand).m_Port == this.m_Port;
    }

    /// <summary>
    /// Returns a hash value for a <see cref="T:System.Net.IPEndPoint"/> instance.
    /// </summary>
    /// 
    /// <returns>
    /// An integer hash value.
    /// </returns>
    /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public override int GetHashCode()
    {
      return this.m_Address.GetHashCode() ^ this.m_Port;
    }

    internal IPEndPoint Snapshot()
    {
      return new IPEndPoint(this.Address.Snapshot(), this.Port);
    }
  }
}
