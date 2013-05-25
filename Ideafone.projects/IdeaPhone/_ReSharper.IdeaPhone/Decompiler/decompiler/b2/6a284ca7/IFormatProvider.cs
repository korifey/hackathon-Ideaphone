// Type: System.IFormatProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  /// Provides a mechanism for retrieving an object to control formatting.
  /// </summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IFormatProvider
  {
    /// <summary>
    /// Returns an object that provides formatting services for the specified type.
    /// </summary>
    /// 
    /// <returns>
    /// An instance of the object specified by <paramref name="formatType"/>, if the <see cref="T:System.IFormatProvider"/> implementation can supply that type of object; otherwise, null.
    /// </returns>
    /// <param name="formatType">An object that specifies the type of format object to return. </param><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    object GetFormat(Type formatType);
  }
}
