// Type: System.String
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System
{
  /// <summary>
  /// Represents text as a series of Unicode characters.
  /// </summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class String : IComparable, ICloneable, IConvertible, IComparable<string>, IEnumerable<char>, IEnumerable, IEquatable<string>
  {
    [NonSerialized]
    private int m_stringLength;
    [ForceTokenStabilization]
    [NonSerialized]
    private char m_firstChar;
    /// <summary>
    /// Represents the empty string. This field is read-only.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly string Empty;
    private const int TrimHead = 0;
    private const int TrimTail = 1;
    private const int TrimBoth = 2;
    private const int charPtrAlignConst = 1;
    private const int alignConst = 3;

    internal char FirstChar
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.m_firstChar;
      }
    }

    /// <summary>
    /// Gets the <see cref="T:System.Char"/> object at a specified position in the current <see cref="T:System.String"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// The object at position <paramref name="index"/>.
    /// </returns>
    /// <param name="index">A position in the current string. </param><exception cref="T:System.IndexOutOfRangeException"><paramref name="index"/> is greater than or equal to the length of this object or less than zero. </exception><filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    [IndexerName("Chars")]
    public char this[int index] { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>
    /// Gets the number of characters in the current <see cref="T:System.String"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// The number of characters in the current string.
    /// </returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Length { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    internal static bool LegacyMode
    {
      get
      {
        return CompatibilitySwitches.IsAppEarlierThanSilverlight4;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.String"/> class to the value indicated by a specified pointer to an array of Unicode characters.
    /// </summary>
    /// <param name="value">A pointer to a null-terminated array of Unicode characters. </param><exception cref="T:System.ArgumentOutOfRangeException">The current process does not have read access to all the addressed characters.</exception><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentException"><paramref name="value"/> specifies an array that contains an invalid Unicode character, or <paramref name="value"/> specifies an address less than 64000.</exception>
    [CLSCompliant(false)]
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public String(char* value);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.String"/> class to the value indicated by a specified pointer to an array of Unicode characters, a starting character position within that array, and a length.
    /// </summary>
    /// <param name="value">A pointer to an array of Unicode characters. </param><param name="startIndex">The starting position within <paramref name="value"/>. </param><param name="length">The number of characters within <paramref name="value"/> to use. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> or <paramref name="length"/> is less than zero, <paramref name="value"/> + <paramref name="startIndex"/> cause a pointer overflow, or the current process does not have read access to all the addressed characters.</exception><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentException"><paramref name="value"/> specifies an array that contains an invalid Unicode character, or <paramref name="value"/> + <paramref name="startIndex"/> specifies an address less than 64000.</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public String(char* value, int startIndex, int length);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.String"/> class to the value indicated by a pointer to an array of 8-bit signed integers.
    /// </summary>
    /// <param name="value">A pointer to a null-terminated array of 8-bit signed integers. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentException">A new instance of <see cref="T:System.String"/> could not be initialized using <paramref name="value"/>, assuming <paramref name="value"/> is encoded in ANSI. </exception><exception cref="T:System.ArgumentOutOfRangeException">The length of the new string to initialize, which is determined by the null termination character of <paramref name="value"/>, is too large to allocate. </exception><exception cref="T:System.AccessViolationException"><paramref name="value"/> specifies an invalid address.</exception>
    [CLSCompliant(false)]
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public String(sbyte* value);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.String"/> class to the value indicated by a specified pointer to an array of 8-bit signed integers, a starting character position within that array, and a length.
    /// </summary>
    /// <param name="value">A pointer to an array of 8-bit signed integers. </param><param name="startIndex">The starting position within <paramref name="value"/>. </param><param name="length">The number of characters within <paramref name="value"/> to use. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> or <paramref name="length"/> is less than zero. -or-The address specified by <paramref name="value"/> + <paramref name="startIndex"/> is too large for the current platform; that is, the address calculation overflowed. -or-The length of the new string to initialize is too large to allocate.</exception><exception cref="T:System.ArgumentException">The address specified by <paramref name="value"/> + <paramref name="startIndex"/> is less than 64K.-or- A new instance of <see cref="T:System.String"/> could not be initialized using <paramref name="value"/>, assuming <paramref name="value"/> is encoded in ANSI. </exception><exception cref="T:System.AccessViolationException"><paramref name="value"/>, <paramref name="startIndex"/>, and <paramref name="length"/> collectively specify an invalid address.</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public String(sbyte* value, int startIndex, int length);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.String"/> class to the value indicated by a specified pointer to an array of 8-bit signed integers, a starting character position within that array, a length, and an <see cref="T:System.Text.Encoding"/> object.
    /// </summary>
    /// <param name="value">A pointer to an array of 8-bit signed integers. </param><param name="startIndex">The starting position within <paramref name="value"/>. </param><param name="length">The number of characters within <paramref name="value"/> to use. </param><param name="enc">An object that specifies how the array referenced by <paramref name="value"/> is encoded. If <paramref name="enc"/> is null, ANSI encoding is assumed.</param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> or <paramref name="length"/> is less than zero. -or-The address specified by <paramref name="value"/> + <paramref name="startIndex"/> is too large for the current platform; that is, the address calculation overflowed. -or-The length of the new string to initialize is too large to allocate.</exception><exception cref="T:System.ArgumentException">The address specified by <paramref name="value"/> + <paramref name="startIndex"/> is less than 64K.-or- A new instance of <see cref="T:System.String"/> could not be initialized using <paramref name="value"/>, assuming <paramref name="value"/> is encoded as specified by <paramref name="enc"/>. </exception><exception cref="T:System.AccessViolationException"><paramref name="value"/>, <paramref name="startIndex"/>, and <paramref name="length"/> collectively specify an invalid address.</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public String(sbyte* value, int startIndex, int length, Encoding enc);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.String"/> class to the value indicated by an array of Unicode characters, a starting character position within that array, and a length.
    /// </summary>
    /// <param name="value">An array of Unicode characters. </param><param name="startIndex">The starting position within <paramref name="value"/>. </param><param name="length">The number of characters within <paramref name="value"/> to use. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> or <paramref name="length"/> is less than zero.-or- The sum of <paramref name="startIndex"/> and <paramref name="length"/> is greater than the number of elements in <paramref name="value"/>. </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public String(char[] value, int startIndex, int length);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.String"/> class to the value indicated by an array of Unicode characters.
    /// </summary>
    /// <param name="value">An array of Unicode characters. </param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public String(char[] value);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.String"/> class to the value indicated by a specified Unicode character repeated a specified number of times.
    /// </summary>
    /// <param name="c">A Unicode character. </param><param name="count">The number of times <paramref name="c"/> occurs. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="count"/> is less than zero. </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public String(char c, int count);

    /// <summary>
    /// Determines whether two specified strings have the same value.
    /// </summary>
    /// 
    /// <returns>
    /// true if the value of <paramref name="a"/> is the same as the value of <paramref name="b"/>; otherwise, false.
    /// </returns>
    /// <param name="a">The first string to compare, or null. </param><param name="b">The second string to compare, or null. </param><filterpriority>3</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public static bool operator ==(string a, string b)
    {
      return string.Equals(a, b);
    }

    /// <summary>
    /// Determines whether two specified strings have different values.
    /// </summary>
    /// 
    /// <returns>
    /// true if the value of <paramref name="a"/> is different from the value of <paramref name="b"/>; otherwise, false.
    /// </returns>
    /// <param name="a">The first string to compare, or null. </param><param name="b">The second string to compare, or null. </param><filterpriority>3</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public static bool operator !=(string a, string b)
    {
      return !string.Equals(a, b);
    }

    /// <summary>
    /// Concatenates all the elements of a string array, using the specified separator between each element.
    /// </summary>
    /// 
    /// <returns>
    /// A string that consists of the elements in <paramref name="value"/> delimited by the <paramref name="separator"/> string. If <paramref name="value"/> is an empty array, the method returns <see cref="F:System.String.Empty"/>.
    /// </returns>
    /// <param name="separator">The string to use as a separator. </param><param name="value">An array that contains the elements to concatenate. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Join(string separator, params string[] value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      else
        return string.Join(separator, value, 0, value.Length);
    }

    /// <summary>
    /// Concatenates the elements of an object array, using the specified separator between each element.
    /// </summary>
    /// 
    /// <returns>
    /// A string that consists of the elements of <paramref name="values"/> delimited by the <paramref name="separator"/> string. If <paramref name="values"/> is an empty array, the method returns <see cref="F:System.String.Empty"/>.
    /// </returns>
    /// <param name="separator">The string to use as a separator.</param><param name="values">An array that contains the elements to concatenate.</param><exception cref="T:System.ArgumentNullException"><paramref name="values"/> is null. </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static string Join(string separator, params object[] values)
    {
      if (values == null)
        throw new ArgumentNullException("values");
      if (values.Length == 0 || values[0] == null)
        return string.Empty;
      if (separator == null)
        separator = string.Empty;
      StringBuilder sb = StringBuilderCache.Acquire(16);
      string str1 = values[0].ToString();
      if (str1 != null)
        sb.Append(str1);
      for (int index = 1; index < values.Length; ++index)
      {
        sb.Append(separator);
        if (values[index] != null)
        {
          string str2 = values[index].ToString();
          if (str2 != null)
            sb.Append(str2);
        }
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>
    /// Concatenates the members of a collection, using the specified separator between each member.
    /// </summary>
    /// 
    /// <returns>
    /// A string that consists of the members of <paramref name="values"/> delimited by the <paramref name="separator"/> string. If <paramref name="values"/> has no members, the method returns <see cref="F:System.String.Empty"/>.
    /// </returns>
    /// <param name="separator">The string to use as a separator.</param><param name="values">A collection that contains the objects to concatenate.</param><typeparam name="T">The type of the members of <paramref name="values"/>.</typeparam><exception cref="T:System.ArgumentNullException"><paramref name="values"/> is null. </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static string Join<T>(string separator, IEnumerable<T> values)
    {
      if (values == null)
        throw new ArgumentNullException("values");
      if (separator == null)
        separator = string.Empty;
      using (IEnumerator<T> enumerator = values.GetEnumerator())
      {
        if (!enumerator.MoveNext())
          return string.Empty;
        StringBuilder sb = StringBuilderCache.Acquire(16);
        if ((object) enumerator.Current != null)
        {
          string str = enumerator.Current.ToString();
          if (str != null)
            sb.Append(str);
        }
        while (enumerator.MoveNext())
        {
          sb.Append(separator);
          if ((object) enumerator.Current != null)
          {
            string str = enumerator.Current.ToString();
            if (str != null)
              sb.Append(str);
          }
        }
        return StringBuilderCache.GetStringAndRelease(sb);
      }
    }

    /// <summary>
    /// Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable`1"/> collection of type <see cref="T:System.String"/>, using the specified separator between each member.
    /// </summary>
    /// 
    /// <returns>
    /// A string that consists of the members of <paramref name="values"/> delimited by the <paramref name="separator"/> string. If <paramref name="values"/> has no members, the method returns <see cref="F:System.String.Empty"/>.
    /// </returns>
    /// <param name="separator">The string to use as a separator.</param><param name="values">A collection that contains the strings to concatenate.</param><exception cref="T:System.ArgumentNullException"><paramref name="values"/> is null. </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static string Join(string separator, IEnumerable<string> values)
    {
      if (values == null)
        throw new ArgumentNullException("values");
      if (separator == null)
        separator = string.Empty;
      using (IEnumerator<string> enumerator = values.GetEnumerator())
      {
        if (!enumerator.MoveNext())
          return string.Empty;
        StringBuilder sb = StringBuilderCache.Acquire(16);
        if (enumerator.Current != null)
          sb.Append(enumerator.Current);
        while (enumerator.MoveNext())
        {
          sb.Append(separator);
          if (enumerator.Current != null)
            sb.Append(enumerator.Current);
        }
        return StringBuilderCache.GetStringAndRelease(sb);
      }
    }

    /// <summary>
    /// Concatenates the specified elements of a string array, using the specified separator between each element.
    /// </summary>
    /// 
    /// <returns>
    /// A string that consists of the strings in <paramref name="value"/> delimited by the <paramref name="separator"/> string. -or-<see cref="F:System.String.Empty"/> if <paramref name="count"/> is zero, <paramref name="value"/> has no elements, or <paramref name="separator"/> and all the elements of <paramref name="value"/> are <see cref="F:System.String.Empty"/>.
    /// </returns>
    /// <param name="separator">The string to use as a separator. </param><param name="value">An array that contains the elements to concatenate. </param><param name="startIndex">The first element in <paramref name="value"/> to use. </param><param name="count">The number of elements of <paramref name="value"/> to use. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> or <paramref name="count"/> is less than 0.-or- <paramref name="startIndex"/> plus <paramref name="count"/> is greater than the number of elements in <paramref name="value"/>. </exception><exception cref="T:System.OutOfMemoryException">Out of memory.</exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe string Join(string separator, string[] value, int startIndex, int count)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      if (startIndex > value.Length - count)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (separator == null)
        separator = string.Empty;
      if (count == 0)
        return string.Empty;
      int num1 = 0;
      int num2 = startIndex + count - 1;
      for (int index = startIndex; index <= num2; ++index)
      {
        if (value[index] != null)
          num1 += value[index].Length;
      }
      int num3 = num1 + (count - 1) * separator.Length;
      if (num3 < 0 || num3 + 1 < 0)
        throw new OutOfMemoryException();
      if (num3 == 0)
        return string.Empty;
      string str = string.FastAllocateString(num3);
      fixed (char* buffer = &str.m_firstChar)
      {
        UnSafeCharBuffer unSafeCharBuffer = new UnSafeCharBuffer(buffer, num3);
        unSafeCharBuffer.AppendString(value[startIndex]);
        for (int index = startIndex + 1; index <= num2; ++index)
        {
          unSafeCharBuffer.AppendString(separator);
          unSafeCharBuffer.AppendString(value[index]);
        }
      }
      return str;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static int nativeCompareOrdinalEx(string strA, int indexA, string strB, int indexB, int count);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static int nativeCompareOrdinalIgnoreCaseWC(string strA, sbyte* strBBytes);

    [SecuritySafeCritical]
    internal static unsafe string SmallCharToUpper(string strIn)
    {
      int length = strIn.Length;
      string str = string.FastAllocateString(length);
      fixed (char* chPtr1 = &strIn.m_firstChar)
        fixed (char* chPtr2 = &str.m_firstChar)
        {
          for (int index = 0; index < length; ++index)
          {
            int num = (int) chPtr1[index];
            if ((uint) (num - 97) <= 25U)
              num -= 32;
            chPtr2[index] = (char) num;
          }
        }
      return str;
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private static unsafe bool EqualsHelper(string strA, string strB)
    {
      int length = strA.Length;
      if (length != strB.Length)
        return false;
      fixed (char* chPtr1 = &strA.m_firstChar)
        fixed (char* chPtr2 = &strB.m_firstChar)
        {
          char* chPtr3 = chPtr1;
          char* chPtr4 = chPtr2;
          while (length >= 10)
          {
            if (*(int*) chPtr3 != *(int*) chPtr4 || *(int*) (chPtr3 + 2) != *(int*) (chPtr4 + 2) || (*(int*) (chPtr3 + 4) != *(int*) (chPtr4 + 4) || *(int*) (chPtr3 + 6) != *(int*) (chPtr4 + 6)) || *(int*) (chPtr3 + 8) != *(int*) (chPtr4 + 8))
              return false;
            chPtr3 += 10;
            chPtr4 += 10;
            length -= 10;
          }
          while (length > 0 && *(int*) chPtr3 == *(int*) chPtr4)
          {
            chPtr3 += 2;
            chPtr4 += 2;
            length -= 2;
          }
          return length <= 0;
        }
    }

    [SecuritySafeCritical]
    private static unsafe int CompareOrdinalHelper(string strA, string strB)
    {
      int num1 = Math.Min(strA.Length, strB.Length);
      int num2 = -1;
      fixed (char* chPtr1 = &strA.m_firstChar)
        fixed (char* chPtr2 = &strB.m_firstChar)
        {
          char* chPtr3 = chPtr1;
          char* chPtr4 = chPtr2;
          while (num1 >= 10)
          {
            if (*(int*) chPtr3 != *(int*) chPtr4)
            {
              num2 = 0;
              break;
            }
            else if (*(int*) (chPtr3 + 2) != *(int*) (chPtr4 + 2))
            {
              num2 = 2;
              break;
            }
            else if (*(int*) (chPtr3 + 4) != *(int*) (chPtr4 + 4))
            {
              num2 = 4;
              break;
            }
            else if (*(int*) (chPtr3 + 6) != *(int*) (chPtr4 + 6))
            {
              num2 = 6;
              break;
            }
            else if (*(int*) (chPtr3 + 8) != *(int*) (chPtr4 + 8))
            {
              num2 = 8;
              break;
            }
            else
            {
              chPtr3 += 10;
              chPtr4 += 10;
              num1 -= 10;
            }
          }
          if (num2 != -1)
          {
            char* chPtr5 = chPtr3 + num2;
            char* chPtr6 = chPtr4 + num2;
            int num3;
            if ((num3 = (int) *chPtr5 - (int) *chPtr6) != 0)
              return num3;
            else
              return (int) chPtr5[1] - (int) chPtr6[1];
          }
          else
          {
            while (num1 > 0 && *(int*) chPtr3 == *(int*) chPtr4)
            {
              chPtr3 += 2;
              chPtr4 += 2;
              num1 -= 2;
            }
            if (num1 <= 0)
              return strA.Length - strB.Length;
            int num3;
            if ((num3 = (int) *chPtr3 - (int) *chPtr4) != 0)
              return num3;
            else
              return (int) chPtr3[1] - (int) chPtr4[1];
          }
        }
    }

    /// <summary>
    /// Determines whether this instance and a specified object, which must also be a <see cref="T:System.String"/> object, have the same value.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="obj"/> is a <see cref="T:System.String"/> and its value is the same as this instance; otherwise, false.
    /// </returns>
    /// <param name="obj">The string to compare to this instance. </param><filterpriority>2</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (this == null)
        throw new NullReferenceException();
      string strB = obj as string;
      if (strB == null)
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (this.Length != strB.Length)
        return false;
      else
        return string.EqualsHelper(this, strB);
    }

    /// <summary>
    /// Determines whether this instance and another specified <see cref="T:System.String"/> object have the same value.
    /// </summary>
    /// 
    /// <returns>
    /// true if the value of the <paramref name="value"/> parameter is the same as this instance; otherwise, false.
    /// </returns>
    /// <param name="value">The string to compare to this instance. </param><filterpriority>2</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public bool Equals(string value)
    {
      if (this == null)
        throw new NullReferenceException();
      if (value == null)
        return false;
      if (object.ReferenceEquals((object) this, (object) value))
        return true;
      if (this.Length != value.Length)
        return false;
      else
        return string.EqualsHelper(this, value);
    }

    /// <summary>
    /// Determines whether this string and a specified <see cref="T:System.String"/> object have the same value. A parameter specifies the culture, case, and sort rules used in the comparison.
    /// </summary>
    /// 
    /// <returns>
    /// true if the value of the <paramref name="value"/> parameter is the same as this string; otherwise, false.
    /// </returns>
    /// <param name="value">The string to compare to this instance.</param><param name="comparisonType">One of the enumeration values that specifies how the strings will be compared. </param><exception cref="T:System.ArgumentException"><paramref name="comparisonType"/> is not a <see cref="T:System.StringComparison"/> value. </exception><filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool Equals(string value, StringComparison comparisonType)
    {
      if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      if (this == value)
        return true;
      if (value == null)
        return false;
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(this, value, CompareOptions.None) == 0;
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(this, value, CompareOptions.IgnoreCase) == 0;
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(this, value, CompareOptions.None) == 0;
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(this, value, CompareOptions.IgnoreCase) == 0;
        case StringComparison.Ordinal:
          if (this.Length != value.Length)
            return false;
          else
            return string.EqualsHelper(this, value);
        case StringComparison.OrdinalIgnoreCase:
          if (this.Length != value.Length)
            return false;
          if (this.IsAscii() && value.IsAscii())
            return string.CompareOrdinalIgnoreCaseHelper(this, value) == 0;
          else
            return TextInfo.CompareOrdinalIgnoreCase(this, value) == 0;
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      }
    }

    /// <summary>
    /// Determines whether two specified <see cref="T:System.String"/> objects have the same value.
    /// </summary>
    /// 
    /// <returns>
    /// true if the value of <paramref name="a"/> is the same as the value of <paramref name="b"/>; otherwise, false. If both <paramref name="a"/> and <paramref name="b"/> are null, the method returns true.
    /// </returns>
    /// <param name="a">The first string to compare, or null. </param><param name="b">The second string to compare, or null. </param><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public static bool Equals(string a, string b)
    {
      if (a == b)
        return true;
      if (a == null || b == null || a.Length != b.Length)
        return false;
      else
        return string.EqualsHelper(a, b);
    }

    /// <summary>
    /// Determines whether two specified <see cref="T:System.String"/> objects have the same value. A parameter specifies the culture, case, and sort rules used in the comparison.
    /// </summary>
    /// 
    /// <returns>
    /// true if the value of the <paramref name="a"/> parameter is equal to the value of the <paramref name="b"/> parameter; otherwise, false.
    /// </returns>
    /// <param name="a">The first string to compare, or null. </param><param name="b">The second string to compare, or null. </param><param name="comparisonType">One of the enumeration values that specifies the rules for the comparison. </param><exception cref="T:System.ArgumentException"><paramref name="comparisonType"/> is not a <see cref="T:System.StringComparison"/> value. </exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool Equals(string a, string b, StringComparison comparisonType)
    {
      if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      if (a == b)
        return true;
      if (a == null || b == null)
        return false;
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(a, b, CompareOptions.None) == 0;
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(a, b, CompareOptions.IgnoreCase) == 0;
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(a, b, CompareOptions.None) == 0;
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(a, b, CompareOptions.IgnoreCase) == 0;
        case StringComparison.Ordinal:
          if (a.Length != b.Length)
            return false;
          else
            return string.EqualsHelper(a, b);
        case StringComparison.OrdinalIgnoreCase:
          if (a.Length != b.Length)
            return false;
          if (a.IsAscii() && b.IsAscii())
            return string.CompareOrdinalIgnoreCaseHelper(a, b) == 0;
          else
            return TextInfo.CompareOrdinalIgnoreCase(a, b) == 0;
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      }
    }

    /// <summary>
    /// Copies a specified number of characters from a specified position in this instance to a specified position in an array of Unicode characters.
    /// </summary>
    /// <param name="sourceIndex">The index of the first character in this instance to copy. </param><param name="destination">An array of Unicode characters to which characters in this instance are copied. </param><param name="destinationIndex">The index in <paramref name="destination"/> at which the copy operation begins. </param><param name="count">The number of characters in this instance to copy to <paramref name="destination"/>. </param><exception cref="T:System.ArgumentNullException"><paramref name="destination"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="sourceIndex"/>, <paramref name="destinationIndex"/>, or <paramref name="count"/> is negative -or- <paramref name="count"/> is greater than the length of the substring from <paramref name="startIndex"/> to the end of this instance -or- <paramref name="count"/> is greater than the length of the subarray from <paramref name="destinationIndex"/> to the end of <paramref name="destination"/></exception><filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
    {
      if (destination == null)
        throw new ArgumentNullException("destination");
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      if (sourceIndex < 0)
        throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count > this.Length - sourceIndex)
        throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
      if (destinationIndex > destination.Length - count || destinationIndex < 0)
        throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
      if (count <= 0)
        return;
      fixed (char* chPtr1 = &this.m_firstChar)
        fixed (char* chPtr2 = destination)
          string.wstrcpy(chPtr2 + destinationIndex, chPtr1 + sourceIndex, count);
    }

    /// <summary>
    /// Copies the characters in this instance to a Unicode character array.
    /// </summary>
    /// 
    /// <returns>
    /// A Unicode character array whose elements are the individual characters of this instance. If this instance is an empty string, the returned array is empty and has a zero length.
    /// </returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe char[] ToCharArray()
    {
      int length = this.Length;
      char[] chArray = new char[length];
      if (length > 0)
      {
        fixed (char* smem = &this.m_firstChar)
          fixed (char* dmem = chArray)
            string.wstrcpyPtrAligned(dmem, smem, length);
      }
      return chArray;
    }

    /// <summary>
    /// Copies the characters in a specified substring in this instance to a Unicode character array.
    /// </summary>
    /// 
    /// <returns>
    /// A Unicode character array whose elements are the <paramref name="length"/> number of characters in this instance starting from character position <paramref name="startIndex"/>.
    /// </returns>
    /// <param name="startIndex">The starting position of a substring in this instance. </param><param name="length">The length of the substring in this instance. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> or <paramref name="length"/> is less than zero.-or- <paramref name="startIndex"/> plus <paramref name="length"/> is greater than the length of this instance. </exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe char[] ToCharArray(int startIndex, int length)
    {
      if (startIndex < 0 || startIndex > this.Length || startIndex > this.Length - length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      char[] chArray = new char[length];
      if (length > 0)
      {
        fixed (char* chPtr = &this.m_firstChar)
          fixed (char* dmem = chArray)
            string.wstrcpy(dmem, chPtr + startIndex, length);
      }
      return chArray;
    }

    /// <summary>
    /// Indicates whether the specified string is null or an <see cref="F:System.String.Empty"/> string.
    /// </summary>
    /// 
    /// <returns>
    /// true if the <paramref name="value"/> parameter is null or an empty string (""); otherwise, false.
    /// </returns>
    /// <param name="value">The string to test. </param><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public static bool IsNullOrEmpty(string value)
    {
      if (value != null)
        return value.Length == 0;
      else
        return true;
    }

    /// <summary>
    /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
    /// </summary>
    /// 
    /// <returns>
    /// true if the <paramref name="value"/> parameter is null or <see cref="F:System.String.Empty"/>, or if <paramref name="value"/> consists exclusively of white-space characters.
    /// </returns>
    /// <param name="value">The string to test.</param>
    [__DynamicallyInvokable]
    public static bool IsNullOrWhiteSpace(string value)
    {
      if (value == null)
        return true;
      for (int index = 0; index < value.Length; ++index)
      {
        if (!char.IsWhiteSpace(value[index]))
          return false;
      }
      return true;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static int InternalMarvin32HashString(string s, int sLen, long additionalEntropy);

    [SecuritySafeCritical]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    internal static bool UseRandomizedHashing()
    {
      return string.InternalUseRandomizedHashing();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static bool InternalUseRandomizedHashing();

    /// <summary>
    /// Returns the hash code for this string.
    /// </summary>
    /// 
    /// <returns>
    /// A 32-bit signed integer hash code.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetHashCode()
    {
      if (HashHelpers.s_UseRandomizedStringHashing)
        return string.InternalMarvin32HashString(this, this.Length, 0L);
      fixed (char* chPtr = this)
      {
        int num1 = 352654597;
        int num2 = num1;
        int* numPtr = (int*) chPtr;
        int length = this.Length;
        while (length > 2)
        {
          num1 = (num1 << 5) + num1 + (num1 >> 27) ^ *numPtr;
          num2 = (num2 << 5) + num2 + (num2 >> 27) ^ numPtr[1];
          numPtr += 2;
          length -= 4;
        }
        if (length > 0)
          num1 = (num1 << 5) + num1 + (num1 >> 27) ^ *numPtr;
        return num1 + num2 * 1566083941;
      }
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal unsafe int GetLegacyNonRandomizedHashCode()
    {
      fixed (char* chPtr = this)
      {
        int num1 = 352654597;
        int num2 = num1;
        int* numPtr = (int*) chPtr;
        int length = this.Length;
        while (length > 2)
        {
          num1 = (num1 << 5) + num1 + (num1 >> 27) ^ *numPtr;
          num2 = (num2 << 5) + num2 + (num2 >> 27) ^ numPtr[1];
          numPtr += 2;
          length -= 4;
        }
        if (length > 0)
          num1 = (num1 << 5) + num1 + (num1 >> 27) ^ *numPtr;
        return num1 + num2 * 1566083941;
      }
    }

    /// <summary>
    /// Returns a string array that contains the substrings in this instance that are delimited by elements of a specified Unicode character array.
    /// </summary>
    /// 
    /// <returns>
    /// An array whose elements contain the substrings in this instance that are delimited by one or more characters in <paramref name="separator"/>. For more information, see the Remarks section.
    /// </returns>
    /// <param name="separator">An array of Unicode characters that delimit the substrings in this instance, an empty array that contains no delimiters, or null. </param><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public string[] Split(params char[] separator)
    {
      return this.SplitInternal(separator, int.MaxValue, StringSplitOptions.None);
    }

    /// <summary>
    /// Returns a string array that contains the substrings in this instance that are delimited by elements of a specified Unicode character array. A parameter specifies the maximum number of substrings to return.
    /// </summary>
    /// 
    /// <returns>
    /// An array whose elements contain the substrings in this instance that are delimited by one or more characters in <paramref name="separator"/>. For more information, see the Remarks section.
    /// </returns>
    /// <param name="separator">An array of Unicode characters that delimit the substrings in this instance, an empty array that contains no delimiters, or null. </param><param name="count">The maximum number of substrings to return. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="count"/> is negative. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public string[] Split(char[] separator, int count)
    {
      return this.SplitInternal(separator, count, StringSplitOptions.None);
    }

    /// <summary>
    /// Returns a string array that contains the substrings in this string that are delimited by elements of a specified Unicode character array. A parameter specifies whether to return empty array elements.
    /// </summary>
    /// 
    /// <returns>
    /// An array whose elements contain the substrings in this string that are delimited by one or more characters in <paramref name="separator"/>. For more information, see the Remarks section.
    /// </returns>
    /// <param name="separator">An array of Unicode characters that delimit the substrings in this string, an empty array that contains no delimiters, or null. </param><param name="options"><see cref="F:System.StringSplitOptions.RemoveEmptyEntries"/> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None"/> to include empty array elements in the array returned. </param><exception cref="T:System.ArgumentException"><paramref name="options"/> is not one of the <see cref="T:System.StringSplitOptions"/> values.</exception><filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public string[] Split(char[] separator, StringSplitOptions options)
    {
      return this.SplitInternal(separator, int.MaxValue, options);
    }

    /// <summary>
    /// Returns a string array that contains the substrings in this string that are delimited by elements of a specified Unicode character array. Parameters specify the maximum number of substrings to return and whether to return empty array elements.
    /// </summary>
    /// 
    /// <returns>
    /// An array whose elements contain the substrings in this string that are delimited by one or more characters in <paramref name="separator"/>. For more information, see the Remarks section.
    /// </returns>
    /// <param name="separator">An array of Unicode characters that delimit the substrings in this string, an empty array that contains no delimiters, or null. </param><param name="count">The maximum number of substrings to return. </param><param name="options"><see cref="F:System.StringSplitOptions.RemoveEmptyEntries"/> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None"/> to include empty array elements in the array returned. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="count"/> is negative. </exception><exception cref="T:System.ArgumentException"><paramref name="options"/> is not one of the <see cref="T:System.StringSplitOptions"/> values.</exception><filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public string[] Split(char[] separator, int count, StringSplitOptions options)
    {
      return this.SplitInternal(separator, count, options);
    }

    [ComVisible(false)]
    internal string[] SplitInternal(char[] separator, int count, StringSplitOptions options)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      if (options < StringSplitOptions.None || options > StringSplitOptions.RemoveEmptyEntries)
      {
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[1]
        {
          (object) options
        }));
      }
      else
      {
        bool flag = options == StringSplitOptions.RemoveEmptyEntries;
        if (count == 0 || flag && this.Length == 0)
          return new string[0];
        int[] sepList = new int[this.Length];
        int numReplaces = this.MakeSeparatorList(separator, ref sepList);
        if (numReplaces == 0 || count == 1)
          return new string[1]
          {
            this
          };
        else if (flag)
          return this.InternalSplitOmitEmptyEntries(sepList, (int[]) null, numReplaces, count);
        else
          return this.InternalSplitKeepEmptyEntries(sepList, (int[]) null, numReplaces, count);
      }
    }

    /// <summary>
    /// Returns a string array that contains the substrings in this string that are delimited by elements of a specified string array. A parameter specifies whether to return empty array elements.
    /// </summary>
    /// 
    /// <returns>
    /// An array whose elements contain the substrings in this string that are delimited by one or more strings in <paramref name="separator"/>. For more information, see the Remarks section.
    /// </returns>
    /// <param name="separator">An array of strings that delimit the substrings in this string, an empty array that contains no delimiters, or null. </param><param name="options"><see cref="F:System.StringSplitOptions.RemoveEmptyEntries"/> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None"/> to include empty array elements in the array returned. </param><exception cref="T:System.ArgumentException"><paramref name="options"/> is not one of the <see cref="T:System.StringSplitOptions"/> values.</exception><filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public string[] Split(string[] separator, StringSplitOptions options)
    {
      return this.Split(separator, int.MaxValue, options);
    }

    /// <summary>
    /// Returns a string array that contains the substrings in this string that are delimited by elements of a specified string array. Parameters specify the maximum number of substrings to return and whether to return empty array elements.
    /// </summary>
    /// 
    /// <returns>
    /// An array whose elements contain the substrings in this string that are delimited by one or more strings in <paramref name="separator"/>. For more information, see the Remarks section.
    /// </returns>
    /// <param name="separator">An array of strings that delimit the substrings in this string, an empty array that contains no delimiters, or null. </param><param name="count">The maximum number of substrings to return. </param><param name="options"><see cref="F:System.StringSplitOptions.RemoveEmptyEntries"/> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None"/> to include empty array elements in the array returned. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="count"/> is negative. </exception><exception cref="T:System.ArgumentException"><paramref name="options"/> is not one of the <see cref="T:System.StringSplitOptions"/> values.</exception><filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public string[] Split(string[] separator, int count, StringSplitOptions options)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      if (options < StringSplitOptions.None || options > StringSplitOptions.RemoveEmptyEntries)
      {
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[1]
        {
          (object) options
        }));
      }
      else
      {
        bool flag = options == StringSplitOptions.RemoveEmptyEntries;
        if (separator == null || separator.Length == 0)
          return this.SplitInternal((char[]) null, count, options);
        if (count == 0 || flag && this.Length == 0)
          return new string[0];
        int[] sepList = new int[this.Length];
        int[] lengthList = new int[this.Length];
        int numReplaces = this.MakeSeparatorList(separator, ref sepList, ref lengthList);
        if (numReplaces == 0 || count == 1)
          return new string[1]
          {
            this
          };
        else if (flag)
          return this.InternalSplitOmitEmptyEntries(sepList, lengthList, numReplaces, count);
        else
          return this.InternalSplitKeepEmptyEntries(sepList, lengthList, numReplaces, count);
      }
    }

    /// <summary>
    /// Retrieves a substring from this instance. The substring starts at a specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A string that is equivalent to the substring that begins at <paramref name="startIndex"/> in this instance, or <see cref="F:System.String.Empty"/> if <paramref name="startIndex"/> is equal to the length of this instance.
    /// </returns>
    /// <param name="startIndex">The zero-based starting character position of a substring in this instance. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero or greater than the length of this instance. </exception><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public string Substring(int startIndex)
    {
      return this.Substring(startIndex, this.Length - startIndex);
    }

    /// <summary>
    /// Retrieves a substring from this instance. The substring starts at a specified character position and has a specified length.
    /// </summary>
    /// 
    /// <returns>
    /// A string that is equivalent to the substring of length <paramref name="length"/> that begins at <paramref name="startIndex"/> in this instance, or <see cref="F:System.String.Empty"/> if <paramref name="startIndex"/> is equal to the length of this instance and <paramref name="length"/> is zero.
    /// </returns>
    /// <param name="startIndex">The zero-based starting character position of a substring in this instance. </param><param name="length">The number of characters in the substring. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> plus <paramref name="length"/> indicates a position not within this instance.-or- <paramref name="startIndex"/> or <paramref name="length"/> is less than zero. </exception><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string Substring(int startIndex, int length)
    {
      return this.InternalSubStringWithChecks(startIndex, length, false);
    }

    [SecurityCritical]
    internal string InternalSubStringWithChecks(int startIndex, int length, bool fAlwaysCopy)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (startIndex > this.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndexLargerThanLength"));
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (startIndex > this.Length - length)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_IndexLength"));
      if (length == 0)
        return string.Empty;
      else
        return this.InternalSubString(startIndex, length, fAlwaysCopy);
    }

    /// <summary>
    /// Removes all leading and trailing occurrences of a set of characters specified in an array from the current <see cref="T:System.String"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// The string that remains after all occurrences of the characters in the <paramref name="trimChars"/> parameter are removed from the start and end of the current string. If <paramref name="trimChars"/> is null or an empty array, white-space characters are removed instead.
    /// </returns>
    /// <param name="trimChars">An array of Unicode characters to remove, or null. </param><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public string Trim(params char[] trimChars)
    {
      if (trimChars == null || trimChars.Length == 0)
        return this.TrimHelper(2);
      else
        return this.TrimHelper(trimChars, 2);
    }

    /// <summary>
    /// Removes all leading occurrences of a set of characters specified in an array from the current <see cref="T:System.String"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// The string that remains after all occurrences of characters in the <paramref name="trimChars"/> parameter are removed from the start of the current string. If <paramref name="trimChars"/> is null or an empty array, white-space characters are removed instead.
    /// </returns>
    /// <param name="trimChars">An array of Unicode characters to remove, or null. </param><filterpriority>2</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public string TrimStart(params char[] trimChars)
    {
      if (trimChars == null || trimChars.Length == 0)
        return this.TrimHelper(0);
      else
        return this.TrimHelper(trimChars, 0);
    }

    /// <summary>
    /// Removes all trailing occurrences of a set of characters specified in an array from the current <see cref="T:System.String"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// The string that remains after all occurrences of the characters in the <paramref name="trimChars"/> parameter are removed from the end of the current string. If <paramref name="trimChars"/> is null or an empty array, Unicode white-space characters are removed instead.
    /// </returns>
    /// <param name="trimChars">An array of Unicode characters to remove, or null. </param><filterpriority>2</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public string TrimEnd(params char[] trimChars)
    {
      if (trimChars == null || trimChars.Length == 0)
        return this.TrimHelper(1);
      else
        return this.TrimHelper(trimChars, 1);
    }

    [SecurityCritical]
    internal static unsafe string CreateStringFromEncoding(byte* bytes, int byteLength, Encoding encoding)
    {
      int charCount = encoding.GetCharCount(bytes, byteLength, (DecoderNLS) null);
      if (charCount == 0)
        return string.Empty;
      string str = string.FastAllocateString(charCount);
      fixed (char* chars = &str.m_firstChar)
        encoding.GetChars(bytes, byteLength, chars, charCount, (DecoderNLS) null);
      return str;
    }

    [SecuritySafeCritical]
    internal unsafe int ConvertToAnsi(byte* pbNativeBuffer, int cbNativeBuffer, bool fBestFit, bool fThrowOnUnmappableChar)
    {
      uint flags = fBestFit ? 0U : 1024U;
      uint num = 0U;
      int index;
      fixed (char* pwzSource = &this.m_firstChar)
        index = Win32Native.WideCharToMultiByte(0U, flags, pwzSource, this.Length, pbNativeBuffer, cbNativeBuffer, IntPtr.Zero, fThrowOnUnmappableChar ? new IntPtr((void*) &num) : IntPtr.Zero);
      if ((int) num != 0)
        throw new ArgumentException(Environment.GetResourceString("Interop_Marshal_Unmappable_Char"));
      pbNativeBuffer[index] = (byte) 0;
      return index;
    }

    /// <summary>
    /// Indicates whether this string is in Unicode normalization form C.
    /// </summary>
    /// 
    /// <returns>
    /// true if this string is in normalization form C; otherwise, false.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public bool IsNormalized()
    {
      return this.IsNormalized(NormalizationForm.FormC);
    }

    /// <summary>
    /// Indicates whether this string is in the specified Unicode normalization form.
    /// </summary>
    /// 
    /// <returns>
    /// true if this string is in the normalization form specified by the <paramref name="normalizationForm"/> parameter; otherwise, false.
    /// </returns>
    /// <param name="normalizationForm">A Unicode normalization form. </param><exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
    [SecuritySafeCritical]
    public bool IsNormalized(NormalizationForm normalizationForm)
    {
      if (this.IsFastSort() && (normalizationForm == NormalizationForm.FormC || normalizationForm == NormalizationForm.FormKC || (normalizationForm == NormalizationForm.FormD || normalizationForm == NormalizationForm.FormKD)))
        return true;
      else
        return Normalization.IsNormalized(this, normalizationForm);
    }

    /// <summary>
    /// Returns a new string whose textual value is the same as this string, but whose binary representation is in Unicode normalization form C.
    /// </summary>
    /// 
    /// <returns>
    /// A new, normalized string whose textual value is the same as this string, but whose binary representation is in normalization form C.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public string Normalize()
    {
      return this.Normalize(NormalizationForm.FormC);
    }

    /// <summary>
    /// Returns a new string whose textual value is the same as this string, but whose binary representation is in the specified Unicode normalization form.
    /// </summary>
    /// 
    /// <returns>
    /// A new string whose textual value is the same as this string, but whose binary representation is in the normalization form specified by the <paramref name="normalizationForm"/> parameter.
    /// </returns>
    /// <param name="normalizationForm">A Unicode normalization form. </param><exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
    [SecuritySafeCritical]
    public string Normalize(NormalizationForm normalizationForm)
    {
      if (this.IsAscii() && (normalizationForm == NormalizationForm.FormC || normalizationForm == NormalizationForm.FormKC || (normalizationForm == NormalizationForm.FormD || normalizationForm == NormalizationForm.FormKD)))
        return this;
      else
        return Normalization.Normalize(this, normalizationForm);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static string FastAllocateString(int length);

    [SecurityCritical]
    internal static unsafe void wstrcpy(char* dmem, char* smem, int charCount)
    {
      if (charCount <= 0)
        return;
      if (((int) dmem & 2) != 0)
      {
        *dmem = *smem;
        ++dmem;
        ++smem;
        --charCount;
      }
      while (charCount >= 8)
      {
        *(int*) dmem = (int) *(uint*) smem;
        *(int*) (dmem + 2) = (int) *(uint*) (smem + 2);
        *(int*) (dmem + 4) = (int) *(uint*) (smem + 4);
        *(int*) (dmem + 6) = (int) *(uint*) (smem + 6);
        dmem += 8;
        smem += 8;
        charCount -= 8;
      }
      if ((charCount & 4) != 0)
      {
        *(int*) dmem = (int) *(uint*) smem;
        *(int*) (dmem + 2) = (int) *(uint*) (smem + 2);
        dmem += 4;
        smem += 4;
      }
      if ((charCount & 2) != 0)
      {
        *(int*) dmem = (int) *(uint*) smem;
        dmem += 2;
        smem += 2;
      }
      if ((charCount & 1) == 0)
        return;
      *dmem = *smem;
    }

    [SecurityCritical]
    [ForceTokenStabilization]
    private static unsafe int wcslen(char* ptr)
    {
      char* chPtr = ptr;
      while (((int) (uint) chPtr & 3) != 0 && (int) *chPtr != 0)
        ++chPtr;
      if ((int) *chPtr != 0)
      {
        while (((int) *chPtr & (int) chPtr[1]) != 0 || (int) *chPtr != 0 && (int) chPtr[1] != 0)
          chPtr += 2;
      }
      while ((int) *chPtr != 0)
        ++chPtr;
      return (int) (chPtr - ptr);
    }

    [ForceTokenStabilization]
    [SecurityCritical]
    private unsafe string CtorCharPtrStartLength(char* ptr, int startIndex, int length)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      char* smem = ptr + startIndex;
      if (smem < ptr)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
      if (length == 0)
        return string.Empty;
      string str = string.FastAllocateString(length);
      try
      {
        fixed (char* dmem = str)
          string.wstrcpy(dmem, smem, length);
        return str;
      }
      catch (NullReferenceException ex)
      {
        throw new ArgumentOutOfRangeException("ptr", Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
      }
    }

    /// <summary>
    /// Compares two specified <see cref="T:System.String"/> objects and returns an integer that indicates their relative position in the sort order.
    /// </summary>
    /// 
    /// <returns>
    /// A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA"/> is less than <paramref name="strB"/>. Zero <paramref name="strA"/> equals <paramref name="strB"/>. Greater than zero <paramref name="strA"/> is greater than <paramref name="strB"/>.
    /// </returns>
    /// <param name="strA">The first string to compare. </param><param name="strB">The second string to compare. </param><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public static int Compare(string strA, string strB)
    {
      return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
    }

    /// <summary>
    /// Compares two specified <see cref="T:System.String"/> objects, ignoring or honoring their case, and returns an integer that indicates their relative position in the sort order.
    /// </summary>
    /// 
    /// <returns>
    /// A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA"/> is less than <paramref name="strB"/>. Zero <paramref name="strA"/> equals <paramref name="strB"/>. Greater than zero <paramref name="strA"/> is greater than <paramref name="strB"/>.
    /// </returns>
    /// <param name="strA">The first string to compare. </param><param name="strB">The second string to compare. </param><param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public static int Compare(string strA, string strB, bool ignoreCase)
    {
      if (ignoreCase)
        return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
      else
        return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
    }

    /// <summary>
    /// Compares two specified <see cref="T:System.String"/> objects using the specified rules, and returns an integer that indicates their relative position in the sort order.
    /// </summary>
    /// 
    /// <returns>
    /// A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA"/> is less than <paramref name="strB"/>. Zero <paramref name="strA"/> equals <paramref name="strB"/>. Greater than zero <paramref name="strA"/> is greater than <paramref name="strB"/>.
    /// </returns>
    /// <param name="strA">The first string to compare.</param><param name="strB">The second string to compare. </param><param name="comparisonType">One of the enumeration values that specifies the rules to use in the comparison. </param><exception cref="T:System.ArgumentException"><paramref name="comparisonType"/> is not a <see cref="T:System.StringComparison"/> value. </exception><exception cref="T:System.NotSupportedException"><see cref="T:System.StringComparison"/> is not supported.</exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static int Compare(string strA, string strB, StringComparison comparisonType)
    {
      if ((uint) comparisonType > 5U)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      if (strA == strB)
        return 0;
      if (strA == null)
        return -1;
      if (strB == null)
        return 1;
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
        case StringComparison.Ordinal:
          if ((int) strA.m_firstChar - (int) strB.m_firstChar != 0)
            return (int) strA.m_firstChar - (int) strB.m_firstChar;
          else
            return string.CompareOrdinalHelper(strA, strB);
        case StringComparison.OrdinalIgnoreCase:
          if (strA.IsAscii() && strB.IsAscii())
            return string.CompareOrdinalIgnoreCaseHelper(strA, strB);
          else
            return TextInfo.CompareOrdinalIgnoreCase(strA, strB);
        default:
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_StringComparison"));
      }
    }

    /// <summary>
    /// Compares two specified <see cref="T:System.String"/> objects using the specified comparison options and culture-specific information to influence the comparison, and returns an integer that indicates the relationship of the two strings to each other in the sort order.
    /// </summary>
    /// 
    /// <returns>
    /// A 32-bit signed integer that indicates the lexical relationship between <paramref name="strA"/> and <paramref name="strB"/>, as shown in the following tableValueConditionLess than zero<paramref name="strA"/> is less than <paramref name="strB"/>.Zero<paramref name="strA"/> equals <paramref name="strB"/>.Greater than zero<paramref name="strA"/> is greater than <paramref name="strB"/>.
    /// </returns>
    /// <param name="strA">The first string to compare.  </param><param name="strB">The second string to compare.</param><param name="culture">The culture that supplies culture-specific comparison information.</param><param name="options">Options to use when performing the comparison (such as ignoring case or symbols).  </param><exception cref="T:System.ArgumentException"><paramref name="options"/> is not a <see cref="T:System.Globalization.CompareOptions"/> value.</exception><exception cref="T:System.ArgumentNullException"><paramref name="culture"/> is null.</exception>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public static int Compare(string strA, string strB, CultureInfo culture, CompareOptions options)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      else
        return culture.CompareInfo.Compare(strA, strB, options);
    }

    /// <summary>
    /// Compares two specified <see cref="T:System.String"/> objects, ignoring or honoring their case, and using culture-specific information to influence the comparison, and returns an integer that indicates their relative position in the sort order.
    /// </summary>
    /// 
    /// <returns>
    /// A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA"/> is less than <paramref name="strB"/>. Zero <paramref name="strA"/> equals <paramref name="strB"/>. Greater than zero <paramref name="strA"/> is greater than <paramref name="strB"/>.
    /// </returns>
    /// <param name="strA">The first string to compare. </param><param name="strB">The second string to compare. </param><param name="ignoreCase">true to ignore case during the comparison; otherwise, false. </param><param name="culture">An object that supplies culture-specific comparison information. </param><exception cref="T:System.ArgumentNullException"><paramref name="culture"/> is null. </exception><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public static int Compare(string strA, string strB, bool ignoreCase, CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      if (ignoreCase)
        return culture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
      else
        return culture.CompareInfo.Compare(strA, strB, CompareOptions.None);
    }

    /// <summary>
    /// Compares substrings of two specified <see cref="T:System.String"/> objects and returns an integer that indicates their relative position in the sort order.
    /// </summary>
    /// 
    /// <returns>
    /// A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition Less than zero The substring in <paramref name="strA"/> is less than the substring in <paramref name="strB"/>. Zero The substrings are equal, or <paramref name="length"/> is zero. Greater than zero The substring in <paramref name="strA"/> is greater than the substring in <paramref name="strB"/>.
    /// </returns>
    /// <param name="strA">The first string to use in the comparison. </param><param name="indexA">The position of the substring within <paramref name="strA"/>. </param><param name="strB">The second string to use in the comparison. </param><param name="indexB">The position of the substring within <paramref name="strB"/>. </param><param name="length">The maximum number of characters in the substrings to compare. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="indexA"/> is greater than <paramref name="strA"/>.<see cref="P:System.String.Length"/>.-or- <paramref name="indexB"/> is greater than <paramref name="strB"/>.<see cref="P:System.String.Length"/>.-or- <paramref name="indexA"/>, <paramref name="indexB"/>, or <paramref name="length"/> is negative. -or-Either <paramref name="indexA"/> or <paramref name="indexB"/> is null, and <paramref name="length"/> is greater than zero.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int Compare(string strA, int indexA, string strB, int indexB, int length)
    {
      int length1 = length;
      int length2 = length;
      if (strA != null && strA.Length - indexA < length1)
        length1 = strA.Length - indexA;
      if (strB != null && strB.Length - indexB < length2)
        length2 = strB.Length - indexB;
      return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, CompareOptions.None);
    }

    /// <summary>
    /// Compares substrings of two specified <see cref="T:System.String"/> objects, ignoring or honoring their case, and returns an integer that indicates their relative position in the sort order.
    /// </summary>
    /// 
    /// <returns>
    /// A 32-bit signed integer that indicates the lexical relationship between the two comparands.ValueCondition Less than zero The substring in <paramref name="strA"/> is less than the substring in <paramref name="strB"/>. Zero The substrings are equal, or <paramref name="length"/> is zero. Greater than zero The substring in <paramref name="strA"/> is greater than the substring in <paramref name="strB"/>.
    /// </returns>
    /// <param name="strA">The first string to use in the comparison. </param><param name="indexA">The position of the substring within <paramref name="strA"/>. </param><param name="strB">The second string to use in the comparison. </param><param name="indexB">The position of the substring within <paramref name="strB"/>. </param><param name="length">The maximum number of characters in the substrings to compare. </param><param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="indexA"/> is greater than <paramref name="strA"/>.<see cref="P:System.String.Length"/>.-or- <paramref name="indexB"/> is greater than <paramref name="strB"/>.<see cref="P:System.String.Length"/>.-or- <paramref name="indexA"/>, <paramref name="indexB"/>, or <paramref name="length"/> is negative. -or-Either <paramref name="indexA"/> or <paramref name="indexB"/> is null, and <paramref name="length"/> is greater than zero.</exception><filterpriority>1</filterpriority>
    public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase)
    {
      int length1 = length;
      int length2 = length;
      if (strA != null && strA.Length - indexA < length1)
        length1 = strA.Length - indexA;
      if (strB != null && strB.Length - indexB < length2)
        length2 = strB.Length - indexB;
      if (ignoreCase)
        return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, CompareOptions.IgnoreCase);
      else
        return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, CompareOptions.None);
    }

    /// <summary>
    /// Compares substrings of two specified <see cref="T:System.String"/> objects, ignoring or honoring their case and using culture-specific information to influence the comparison, and returns an integer that indicates their relative position in the sort order.
    /// </summary>
    /// 
    /// <returns>
    /// An integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero The substring in <paramref name="strA"/> is less than the substring in <paramref name="strB"/>. Zero The substrings are equal, or <paramref name="length"/> is zero. Greater than zero The substring in <paramref name="strA"/> is greater than the substring in <paramref name="strB"/>.
    /// </returns>
    /// <param name="strA">The first string to use in the comparison. </param><param name="indexA">The position of the substring within <paramref name="strA"/>. </param><param name="strB">The second string to use in the comparison. </param><param name="indexB">The position of the substring within <paramref name="strB"/>. </param><param name="length">The maximum number of characters in the substrings to compare. </param><param name="ignoreCase">true to ignore case during the comparison; otherwise, false. </param><param name="culture">An object that supplies culture-specific comparison information. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="indexA"/> is greater than <paramref name="strA"/>.<see cref="P:System.String.Length"/>.-or- <paramref name="indexB"/> is greater than <paramref name="strB"/>.<see cref="P:System.String.Length"/>.-or- <paramref name="indexA"/>, <paramref name="indexB"/>, or <paramref name="length"/> is negative. -or-Either <paramref name="strA"/> or <paramref name="strB"/> is null, and <paramref name="length"/> is greater than zero.</exception><exception cref="T:System.ArgumentNullException"><paramref name="culture"/> is null. </exception><filterpriority>1</filterpriority>
    public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase, CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      int length1 = length;
      int length2 = length;
      if (strA != null && strA.Length - indexA < length1)
        length1 = strA.Length - indexA;
      if (strB != null && strB.Length - indexB < length2)
        length2 = strB.Length - indexB;
      if (ignoreCase)
        return culture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, CompareOptions.IgnoreCase);
      else
        return culture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, CompareOptions.None);
    }

    /// <summary>
    /// Compares substrings of two specified <see cref="T:System.String"/> objects using the specified comparison options and culture-specific information to influence the comparison, and returns an integer that indicates the relationship of the two substrings to each other in the sort order.
    /// </summary>
    /// 
    /// <returns>
    /// An integer that indicates the lexical relationship between the two substrings, as shown in the following table.ValueConditionLess than zeroThe substring in <paramref name="strA"/> is less than the substring in <paramref name="strB"/>.ZeroThe substrings are equal or <paramref name="length"/> is zero.Greater than zeroThe substring in <paramref name="strA"/> is greater than the substring in <paramref name="strB"/>.
    /// </returns>
    /// <param name="strA">The first string to use in the comparison.   </param><param name="indexA">The starting position of the substring within <paramref name="strA"/>.</param><param name="strB">The second string to use in the comparison.</param><param name="indexB">The starting position of the substring within <paramref name="strB"/>.</param><param name="length">The maximum number of characters in the substrings to compare.</param><param name="culture">An object that supplies culture-specific comparison information.</param><param name="options">Options to use when performing the comparison (such as ignoring case or symbols).  </param><exception cref="T:System.ArgumentException"><paramref name="options"/> is not a <see cref="T:System.Globalization.CompareOptions"/> value.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="indexA"/> is greater than <paramref name="strA"/>.Length.-or-<paramref name="indexB"/> is greater than <paramref name="strB"/>.Length.-or-<paramref name="indexA"/>, <paramref name="indexB"/>, or <paramref name="length"/> is negative.-or-Either <paramref name="strA"/> or <paramref name="strB"/> is null, and <paramref name="length"/> is greater than zero.</exception><exception cref="T:System.ArgumentNullException"><paramref name="culture"/> is null.</exception>
    public static int Compare(string strA, int indexA, string strB, int indexB, int length, CultureInfo culture, CompareOptions options)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      int length1 = length;
      int length2 = length;
      if (strA != null && strA.Length - indexA < length1)
        length1 = strA.Length - indexA;
      if (strB != null && strB.Length - indexB < length2)
        length2 = strB.Length - indexB;
      return culture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, options);
    }

    /// <summary>
    /// Compares substrings of two specified <see cref="T:System.String"/> objects using the specified rules, and returns an integer that indicates their relative position in the sort order.
    /// </summary>
    /// 
    /// <returns>
    /// A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero The substring in the <paramref name="strA"/> parameter is less than the substring in the <paramref name="strB"/> parameter.Zero The substrings are equal, or the <paramref name="length"/> parameter is zero. Greater than zero The substring in <paramref name="strA"/> is greater than the substring in <paramref name="strB"/>.
    /// </returns>
    /// <param name="strA">The first string to use in the comparison. </param><param name="indexA">The position of the substring within <paramref name="strA"/>. </param><param name="strB">The second string to use in the comparison.</param><param name="indexB">The position of the substring within <paramref name="strB"/>. </param><param name="length">The maximum number of characters in the substrings to compare. </param><param name="comparisonType">One of the enumeration values that specifies the rules to use in the comparison. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="indexA"/> is greater than <paramref name="strA"/>.<see cref="P:System.String.Length"/>.-or- <paramref name="indexB"/> is greater than <paramref name="strB"/>.<see cref="P:System.String.Length"/>.-or- <paramref name="indexA"/>, <paramref name="indexB"/>, or <paramref name="length"/> is negative. -or-Either <paramref name="indexA"/> or <paramref name="indexB"/> is null, and <paramref name="length"/> is greater than zero.</exception><exception cref="T:System.ArgumentException"><paramref name="comparisonType"/> is not a <see cref="T:System.StringComparison"/> value. </exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static int Compare(string strA, int indexA, string strB, int indexB, int length, StringComparison comparisonType)
    {
      if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      if (strA == null || strB == null)
      {
        if (strA == strB)
          return 0;
        return strA != null ? 1 : -1;
      }
      else
      {
        if (length < 0)
          throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
        if (indexA < 0)
          throw new ArgumentOutOfRangeException("indexA", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (indexB < 0)
          throw new ArgumentOutOfRangeException("indexB", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (strA.Length - indexA < 0)
          throw new ArgumentOutOfRangeException("indexA", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (strB.Length - indexB < 0)
          throw new ArgumentOutOfRangeException("indexB", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (length == 0 || strA == strB && indexA == indexB)
          return 0;
        int num1 = length;
        int num2 = length;
        if (strA != null && strA.Length - indexA < num1)
          num1 = strA.Length - indexA;
        if (strB != null && strB.Length - indexB < num2)
          num2 = strB.Length - indexB;
        switch (comparisonType)
        {
          case StringComparison.CurrentCulture:
            return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num1, strB, indexB, num2, CompareOptions.None);
          case StringComparison.CurrentCultureIgnoreCase:
            return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num1, strB, indexB, num2, CompareOptions.IgnoreCase);
          case StringComparison.InvariantCulture:
            return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, indexA, num1, strB, indexB, num2, CompareOptions.None);
          case StringComparison.InvariantCultureIgnoreCase:
            return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, indexA, num1, strB, indexB, num2, CompareOptions.IgnoreCase);
          case StringComparison.Ordinal:
            return string.nativeCompareOrdinalEx(strA, indexA, strB, indexB, length);
          case StringComparison.OrdinalIgnoreCase:
            return TextInfo.CompareOrdinalIgnoreCaseEx(strA, indexA, strB, indexB, num1, num2);
          default:
            throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"));
        }
      }
    }

    /// <summary>
    /// Compares this instance with a specified <see cref="T:System.Object"/> and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.Object"/>.
    /// </summary>
    /// 
    /// <returns>
    /// A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="value"/> parameter.Value Condition Less than zero This instance precedes <paramref name="value"/>. Zero This instance has the same position in the sort order as <paramref name="value"/>. Greater than zero This instance follows <paramref name="value"/>.-or- <paramref name="value"/> is null.
    /// </returns>
    /// <param name="value">An object that evaluates to a <see cref="T:System.String"/>. </param><exception cref="T:System.ArgumentException"><paramref name="value"/> is not a <see cref="T:System.String"/>. </exception><filterpriority>2</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is string))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeString"));
      else
        return string.Compare(this, (string) value, StringComparison.CurrentCulture);
    }

    /// <summary>
    /// Compares this instance with a specified <see cref="T:System.String"/> object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.String"/>.
    /// </summary>
    /// 
    /// <returns>
    /// A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="value"/> parameter.Value Condition Less than zero This instance precedes <paramref name="strB"/>. Zero This instance has the same position in the sort order as <paramref name="strB"/>. Greater than zero This instance follows <paramref name="strB"/>.-or- <paramref name="strB"/> is null.
    /// </returns>
    /// <param name="strB">The string to compare with this instance. </param><filterpriority>2</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public int CompareTo(string strB)
    {
      if (strB == null)
        return 1;
      else
        return CultureInfo.CurrentCulture.CompareInfo.Compare(this, strB, CompareOptions.None);
    }

    /// <summary>
    /// Compares two specified <see cref="T:System.String"/> objects by evaluating the numeric values of the corresponding <see cref="T:System.Char"/> objects in each string.
    /// </summary>
    /// 
    /// <returns>
    /// An integer that indicates the lexical relationship between the two comparands.ValueCondition Less than zero <paramref name="strA"/> is less than <paramref name="strB"/>. Zero <paramref name="strA"/> and <paramref name="strB"/> are equal. Greater than zero <paramref name="strA"/> is greater than <paramref name="strB"/>.
    /// </returns>
    /// <param name="strA">The first string to compare. </param><param name="strB">The second string to compare. </param><filterpriority>2</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public static int CompareOrdinal(string strA, string strB)
    {
      if (strA == strB)
        return 0;
      if (strA == null)
        return -1;
      if (strB == null)
        return 1;
      if ((int) strA.m_firstChar - (int) strB.m_firstChar != 0)
        return (int) strA.m_firstChar - (int) strB.m_firstChar;
      else
        return string.CompareOrdinalHelper(strA, strB);
    }

    /// <summary>
    /// Compares substrings of two specified <see cref="T:System.String"/> objects by evaluating the numeric values of the corresponding <see cref="T:System.Char"/> objects in each substring.
    /// </summary>
    /// 
    /// <returns>
    /// A 32-bit signed integer that indicates the lexical relationship between the two comparands.ValueCondition Less than zero The substring in <paramref name="strA"/> is less than the substring in <paramref name="strB"/>. Zero The substrings are equal, or <paramref name="length"/> is zero. Greater than zero The substring in <paramref name="strA"/> is greater than the substring in <paramref name="strB"/>.
    /// </returns>
    /// <param name="strA">The first string to use in the comparison. </param><param name="indexA">The starting index of the substring in <paramref name="strA"/>. </param><param name="strB">The second string to use in the comparison. </param><param name="indexB">The starting index of the substring in <paramref name="strB"/>. </param><param name="length">The maximum number of characters in the substrings to compare. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="strA"/> is not null and <paramref name="indexA"/> is greater than <paramref name="strA"/>.<see cref="P:System.String.Length"/>.-or- <paramref name="strB"/> is not null and<paramref name="indexB"/> is greater than <paramref name="strB"/>.<see cref="P:System.String.Length"/>.-or- <paramref name="indexA"/>, <paramref name="indexB"/>, or <paramref name="length"/> is negative. </exception><filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public static int CompareOrdinal(string strA, int indexA, string strB, int indexB, int length)
    {
      if (strA != null && strB != null)
        return string.nativeCompareOrdinalEx(strA, indexA, strB, indexB, length);
      if (strA == strB)
        return 0;
      return strA != null ? 1 : -1;
    }

    /// <summary>
    /// Returns a value indicating whether the specified <see cref="T:System.String"/> object occurs within this string.
    /// </summary>
    /// 
    /// <returns>
    /// true if the <paramref name="value"/> parameter occurs within this string, or if <paramref name="value"/> is the empty string (""); otherwise, false.
    /// </returns>
    /// <param name="value">The string to seek. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public bool Contains(string value)
    {
      return this.IndexOf(value, StringComparison.Ordinal) >= 0;
    }

    /// <summary>
    /// Determines whether the end of this string instance matches the specified string.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="value"/> matches the end of this instance; otherwise, false.
    /// </returns>
    /// <param name="value">The string to compare to the substring at the end of this instance. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public bool EndsWith(string value)
    {
      return this.EndsWith(value, string.LegacyMode ? StringComparison.Ordinal : StringComparison.CurrentCulture);
    }

    /// <summary>
    /// Determines whether the end of this string instance matches the specified string when compared using the specified comparison option.
    /// </summary>
    /// 
    /// <returns>
    /// true if the <paramref name="value"/> parameter matches the end of this string; otherwise, false.
    /// </returns>
    /// <param name="value">The string to compare to the substring at the end of this instance. </param><param name="comparisonType">One of the enumeration values that determines how this string and <paramref name="value"/> are compared. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentException"><paramref name="comparisonType"/> is not a <see cref="T:System.StringComparison"/> value.</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public bool EndsWith(string value, StringComparison comparisonType)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      if (this == value || value.Length == 0)
        return true;
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.IgnoreCase);
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.IsSuffix(this, value, CompareOptions.IgnoreCase);
        case StringComparison.Ordinal:
          if (this.Length >= value.Length)
            return string.nativeCompareOrdinalEx(this, this.Length - value.Length, value, 0, value.Length) == 0;
          else
            return false;
        case StringComparison.OrdinalIgnoreCase:
          if (this.Length >= value.Length)
            return TextInfo.CompareOrdinalIgnoreCaseEx(this, this.Length - value.Length, value, 0, value.Length, value.Length) == 0;
          else
            return false;
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      }
    }

    /// <summary>
    /// Determines whether the end of this string instance matches the specified string when compared using the specified culture.
    /// </summary>
    /// 
    /// <returns>
    /// true if the <paramref name="value"/> parameter matches the end of this string; otherwise, false.
    /// </returns>
    /// <param name="value">The string to compare to the substring at the end of this instance. </param><param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param><param name="culture">Cultural information that determines how this instance and <paramref name="value"/> are compared. If <paramref name="culture"/> is null, the current culture is used.</param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><filterpriority>1</filterpriority>
    public bool EndsWith(string value, bool ignoreCase, CultureInfo culture)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (this == value)
        return true;
      else
        return (culture != null ? culture : (string.LegacyMode ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture)).CompareInfo.IsSuffix(this, value, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
    }

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    internal bool EndsWith(char value)
    {
      int length = this.Length;
      return length != 0 && (int) this[length - 1] == (int) value;
    }

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified Unicode character in this string.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of <paramref name="value"/> if that character is found, or -1 if it is not.
    /// </returns>
    /// <param name="value">A Unicode character to seek. </param><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public int IndexOf(char value)
    {
      return this.IndexOf(value, 0, this.Length);
    }

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified Unicode character in this string. The search starts at a specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of <paramref name="value"/> if that character is found, or -1 if it is not.
    /// </returns>
    /// <param name="value">A Unicode character to seek. </param><param name="startIndex">The search starting position. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than 0 (zero) or greater than the length of the string. </exception><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public int IndexOf(char value, int startIndex)
    {
      return this.IndexOf(value, startIndex, this.Length - startIndex);
    }

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified character in this instance. The search starts at a specified character position and examines a specified number of character positions.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of <paramref name="value"/> if that character is found, or -1 if it is not.
    /// </returns>
    /// <param name="value">A Unicode character to seek. </param><param name="startIndex">The search starting position. </param><param name="count">The number of character positions to examine. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="count"/> or <paramref name="startIndex"/> is negative.-or- <paramref name="startIndex"/> is greater than the length of this string.-or-<paramref name="count"/> is greater than the length of this string minus <paramref name="startIndex"/>. </exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public int IndexOf(char value, int startIndex, int count);

    /// <summary>
    /// Reports the zero-based index of the first occurrence in this instance of any character in a specified array of Unicode characters.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of the first occurrence in this instance where any character in <paramref name="anyOf"/> was found; -1 if no character in <paramref name="anyOf"/> was found.
    /// </returns>
    /// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param><exception cref="T:System.ArgumentNullException"><paramref name="anyOf"/> is null. </exception><filterpriority>2</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public int IndexOfAny(char[] anyOf)
    {
      return this.IndexOfAny(anyOf, 0, this.Length);
    }

    /// <summary>
    /// Reports the zero-based index of the first occurrence in this instance of any character in a specified array of Unicode characters. The search starts at a specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of the first occurrence in this instance where any character in <paramref name="anyOf"/> was found; -1 if no character in <paramref name="anyOf"/> was found.
    /// </returns>
    /// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param><param name="startIndex">The search starting position. </param><exception cref="T:System.ArgumentNullException"><paramref name="anyOf"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is negative.-or- <paramref name="startIndex"/> is greater than the number of characters in this instance. </exception><filterpriority>2</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public int IndexOfAny(char[] anyOf, int startIndex)
    {
      return this.IndexOfAny(anyOf, startIndex, this.Length - startIndex);
    }

    /// <summary>
    /// Reports the zero-based index of the first occurrence in this instance of any character in a specified array of Unicode characters. The search starts at a specified character position and examines a specified number of character positions.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of the first occurrence in this instance where any character in <paramref name="anyOf"/> was found; -1 if no character in <paramref name="anyOf"/> was found.
    /// </returns>
    /// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param><param name="startIndex">The search starting position. </param><param name="count">The number of character positions to examine. </param><exception cref="T:System.ArgumentNullException"><paramref name="anyOf"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="count"/> or <paramref name="startIndex"/> is negative.-or- <paramref name="count"/> + <paramref name="startIndex"/> is greater than the number of characters in this instance. </exception><filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public int IndexOfAny(char[] anyOf, int startIndex, int count);

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified string in this instance.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of <paramref name="value"/> if that string is found, or -1 if it is not. If <paramref name="value"/> is <see cref="F:System.String.Empty"/>, the return value is 0.
    /// </returns>
    /// <param name="value">The string to seek. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int IndexOf(string value)
    {
      return this.IndexOf(value, string.LegacyMode ? StringComparison.Ordinal : StringComparison.CurrentCulture);
    }

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified string in this instance. The search starts at a specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of <paramref name="value"/> if that string is found, or -1 if it is not. If <paramref name="value"/> is <see cref="F:System.String.Empty"/>, the return value is <paramref name="startIndex"/>.
    /// </returns>
    /// <param name="value">The string to seek. </param><param name="startIndex">The search starting position. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than 0 (zero) or greater than the length of this string.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int IndexOf(string value, int startIndex)
    {
      return this.IndexOf(value, startIndex, string.LegacyMode ? StringComparison.Ordinal : StringComparison.CurrentCulture);
    }

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified string in this instance. The search starts at a specified character position and examines a specified number of character positions.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of <paramref name="value"/> if that string is found, or -1 if it is not. If <paramref name="value"/> is <see cref="F:System.String.Empty"/>, the return value is <paramref name="startIndex"/>.
    /// </returns>
    /// <param name="value">The string to seek. </param><param name="startIndex">The search starting position. </param><param name="count">The number of character positions to examine. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="count"/> or <paramref name="startIndex"/> is negative.-or- <paramref name="startIndex"/> is greater than the length of this string.-or-<paramref name="count"/> is greater than the length of this string minus <paramref name="startIndex"/>. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int IndexOf(string value, int startIndex, int count)
    {
      if (startIndex < 0 || startIndex > this.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || count > this.Length - startIndex)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      else
        return this.IndexOf(value, startIndex, count, string.LegacyMode ? StringComparison.Ordinal : StringComparison.CurrentCulture);
    }

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified string in the current <see cref="T:System.String"/> object. A parameter specifies the type of search to use for the specified string.
    /// </summary>
    /// 
    /// <returns>
    /// The index position of the <paramref name="value"/> parameter if that string is found, or -1 if it is not. If <paramref name="value"/> is <see cref="F:System.String.Empty"/>, the return value is 0.
    /// </returns>
    /// <param name="value">The string to seek. </param><param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentException"><paramref name="comparisonType"/> is not a valid <see cref="T:System.StringComparison"/> value.</exception>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public int IndexOf(string value, StringComparison comparisonType)
    {
      return this.IndexOf(value, 0, this.Length, comparisonType);
    }

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified string in the current <see cref="T:System.String"/> object. Parameters specify the starting search position in the current string and the type of search to use for the specified string.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of the <paramref name="value"/> parameter if that string is found, or -1 if it is not. If <paramref name="value"/> is <see cref="F:System.String.Empty"/>, the return value is <paramref name="startIndex"/>.
    /// </returns>
    /// <param name="value">The string to seek. </param><param name="startIndex">The search starting position. </param><param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than 0 (zero) or greater than the length of this string. </exception><exception cref="T:System.ArgumentException"><paramref name="comparisonType"/> is not a valid <see cref="T:System.StringComparison"/> value.</exception>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public int IndexOf(string value, int startIndex, StringComparison comparisonType)
    {
      return this.IndexOf(value, startIndex, this.Length - startIndex, comparisonType);
    }

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified string in the current <see cref="T:System.String"/> object. Parameters specify the starting search position in the current string, the number of characters in the current string to search, and the type of search to use for the specified string.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of the <paramref name="value"/> parameter if that string is found, or -1 if it is not. If <paramref name="value"/> is <see cref="F:System.String.Empty"/>, the return value is <paramref name="startIndex"/>.
    /// </returns>
    /// <param name="value">The string to seek. </param><param name="startIndex">The search starting position. </param><param name="count">The number of character positions to examine. </param><param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="count"/> or <paramref name="startIndex"/> is negative.-or- <paramref name="startIndex"/> is greater than the length of this instance.-or-<paramref name="count"/> is greater than the length of this string minus <paramref name="startIndex"/>.</exception><exception cref="T:System.ArgumentException"><paramref name="comparisonType"/> is not a valid <see cref="T:System.StringComparison"/> value.</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public int IndexOf(string value, int startIndex, int count, StringComparison comparisonType)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (startIndex < 0 || startIndex > this.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > this.Length - count)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.None);
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.None);
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
        case StringComparison.Ordinal:
          return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.Ordinal);
        case StringComparison.OrdinalIgnoreCase:
          if (value.IsAscii() && this.IsAscii())
            return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
          else
            return TextInfo.IndexOfStringOrdinalIgnoreCase(this, value, startIndex, count);
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      }
    }

    /// <summary>
    /// Reports the zero-based index position of the last occurrence of a specified Unicode character within this instance.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of <paramref name="value"/> if that character is found, or -1 if it is not.
    /// </returns>
    /// <param name="value">The Unicode character to seek. </param><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public int LastIndexOf(char value)
    {
      return this.LastIndexOf(value, this.Length - 1, this.Length);
    }

    /// <summary>
    /// Reports the zero-based index position of the last occurrence of a specified Unicode character within this instance. The search starts at a specified character position and proceeds backward toward the beginning of the string.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of <paramref name="value"/> if that character is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty"/>.
    /// </returns>
    /// <param name="value">The Unicode character to seek. </param><param name="startIndex">The starting position of the search. The search proceeds from <paramref name="startIndex"/> toward the beginning of this instance.</param><exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty"/>, and <paramref name="startIndex"/> is less than zero or greater than or equal to the length of this instance.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int LastIndexOf(char value, int startIndex)
    {
      return this.LastIndexOf(value, startIndex, startIndex + 1);
    }

    /// <summary>
    /// Reports the zero-based index position of the last occurrence of the specified Unicode character in a substring within this instance. The search starts at a specified character position and proceeds backward toward the beginning of the string for a specified number of character positions.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of <paramref name="value"/> if that character is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty"/>.
    /// </returns>
    /// <param name="value">The Unicode character to seek. </param><param name="startIndex">The starting position of the search. The search proceeds from <paramref name="startIndex"/> toward the beginning of this instance.</param><param name="count">The number of character positions to examine. </param><exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty"/>, and <paramref name="startIndex"/> is less than zero or greater than or equal to the length of this instance.-or-The current instance does not equal <see cref="F:System.String.Empty"/>, and <paramref name="startIndex"/> - <paramref name="count"/> + 1 is less than zero.</exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public int LastIndexOf(char value, int startIndex, int count);

    /// <summary>
    /// Reports the zero-based index position of the last occurrence in this instance of one or more characters specified in a Unicode array.
    /// </summary>
    /// 
    /// <returns>
    /// The index position of the last occurrence in this instance where any character in <paramref name="anyOf"/> was found; -1 if no character in <paramref name="anyOf"/> was found.
    /// </returns>
    /// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param><exception cref="T:System.ArgumentNullException"><paramref name="anyOf"/> is null. </exception><filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int LastIndexOfAny(char[] anyOf)
    {
      return this.LastIndexOfAny(anyOf, this.Length - 1, this.Length);
    }

    /// <summary>
    /// Reports the zero-based index position of the last occurrence in this instance of one or more characters specified in a Unicode array. The search starts at a specified character position and proceeds backward toward the beginning of the string.
    /// </summary>
    /// 
    /// <returns>
    /// The index position of the last occurrence in this instance where any character in <paramref name="anyOf"/> was found; -1 if no character in <paramref name="anyOf"/> was found or if the current instance equals <see cref="F:System.String.Empty"/>.
    /// </returns>
    /// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param><param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex"/> toward the beginning of this instance.</param><exception cref="T:System.ArgumentNullException"><paramref name="anyOf"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty"/>, and <paramref name="startIndex"/> specifies a position that is not within this instance. </exception><filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int LastIndexOfAny(char[] anyOf, int startIndex)
    {
      return this.LastIndexOfAny(anyOf, startIndex, startIndex + 1);
    }

    /// <summary>
    /// Reports the zero-based index position of the last occurrence in this instance of one or more characters specified in a Unicode array. The search starts at a specified character position and proceeds backward toward the beginning of the string for a specified number of character positions.
    /// </summary>
    /// 
    /// <returns>
    /// The index position of the last occurrence in this instance where any character in <paramref name="anyOf"/> was found; -1 if no character in <paramref name="anyOf"/> was found or if the current instance equals <see cref="F:System.String.Empty"/>.
    /// </returns>
    /// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param><param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex"/> toward the beginning of this instance.</param><param name="count">The number of character positions to examine. </param><exception cref="T:System.ArgumentNullException"><paramref name="anyOf"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty"/>, and <paramref name="count"/> or <paramref name="startIndex"/> is negative.-or- The current instance does not equal <see cref="F:System.String.Empty"/>, and <paramref name="startIndex"/> minus <paramref name="count"/> specifies a position that is not within this instance. </exception><filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public int LastIndexOfAny(char[] anyOf, int startIndex, int count);

    /// <summary>
    /// Reports the zero-based index position of the last occurrence of a specified string within this instance.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of <paramref name="value"/> if that string is found, or -1 if it is not. If <paramref name="value"/> is <see cref="F:System.String.Empty"/>, the return value is the last index position in this instance.
    /// </returns>
    /// <param name="value">The string to seek. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int LastIndexOf(string value)
    {
      return this.LastIndexOf(value, this.Length - 1, this.Length, string.LegacyMode ? StringComparison.Ordinal : StringComparison.CurrentCulture);
    }

    /// <summary>
    /// Reports the zero-based index position of the last occurrence of a specified string within this instance. The search starts at a specified character position and proceeds backward toward the beginning of the string.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of <paramref name="value"/> if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty"/>. If <paramref name="value"/> is <see cref="F:System.String.Empty"/>, the return value is the smaller of <paramref name="startIndex"/> and the last index position in this instance.
    /// </returns>
    /// <param name="value">The string to seek. </param><param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex"/> toward the beginning of this instance.</param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty"/>, and <paramref name="startIndex"/> is less than zero or greater than the length of the current instance. -or-The current instance equals <see cref="F:System.String.Empty"/>, and <paramref name="startIndex"/> is greater than zero.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int LastIndexOf(string value, int startIndex)
    {
      return this.LastIndexOf(value, startIndex, startIndex + 1, string.LegacyMode ? StringComparison.Ordinal : StringComparison.CurrentCulture);
    }

    /// <summary>
    /// Reports the zero-based index position of the last occurrence of a specified string within this instance. The search starts at a specified character position and proceeds backward toward the beginning of the string for a specified number of character positions.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index position of <paramref name="value"/> if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty"/>. If <paramref name="value"/> is <see cref="F:System.String.Empty"/>, the return value is the smaller of <paramref name="startIndex"/> and the last index position in this instance.
    /// </returns>
    /// <param name="value">The string to seek. </param><param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex"/> toward the beginning of this instance.</param><param name="count">The number of character positions to examine. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="count"/> is negative.-or-The current instance does not equal <see cref="F:System.String.Empty"/>, and  <paramref name="startIndex"/> is negative.-or- The current instance does not equal <see cref="F:System.String.Empty"/>, and <paramref name="startIndex"/> is greater than the length of this instance.-or-The current instance does not equal <see cref="F:System.String.Empty"/>, and <paramref name="startIndex"/> - <paramref name="count"/> + 1 specifies a position that is not within this instance. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int LastIndexOf(string value, int startIndex, int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      else
        return this.LastIndexOf(value, startIndex, count, string.LegacyMode ? StringComparison.Ordinal : StringComparison.CurrentCulture);
    }

    /// <summary>
    /// Reports the zero-based index of the last occurrence of a specified string within the current <see cref="T:System.String"/> object. A parameter specifies the type of search to use for the specified string.
    /// </summary>
    /// 
    /// <returns>
    /// The index position of the <paramref name="value"/> parameter if that string is found, or -1 if it is not. If <paramref name="value"/> is <see cref="F:System.String.Empty"/>, the return value is the last index position in this instance.
    /// </returns>
    /// <param name="value">The string to seek. </param><param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentException"><paramref name="comparisonType"/> is not a valid <see cref="T:System.StringComparison"/> value.</exception>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public int LastIndexOf(string value, StringComparison comparisonType)
    {
      return this.LastIndexOf(value, this.Length - 1, this.Length, comparisonType);
    }

    /// <summary>
    /// Reports the zero-based index of the last occurrence of a specified string within the current <see cref="T:System.String"/> object. The search starts at a specified character position and proceeds backward toward the beginning of the string. A parameter specifies the type of comparison to perform when searching for the specified string.
    /// </summary>
    /// 
    /// <returns>
    /// The index position of the <paramref name="value"/> parameter if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty"/>. If <paramref name="value"/> is <see cref="F:System.String.Empty"/>, the return value is the smaller of <paramref name="startIndex"/> and the last index position in this instance.
    /// </returns>
    /// <param name="value">The string to seek. </param><param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex"/> toward the beginning of this instance.</param><param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty"/>, and <paramref name="startIndex"/> is less than zero or greater than the length of the current instance. -or-The current instance equals <see cref="F:System.String.Empty"/>, and <paramref name="startIndex"/> is greater than zero.</exception><exception cref="T:System.ArgumentException"><paramref name="comparisonType"/> is not a valid <see cref="T:System.StringComparison"/> value.</exception>
    [__DynamicallyInvokable]
    public int LastIndexOf(string value, int startIndex, StringComparison comparisonType)
    {
      return this.LastIndexOf(value, startIndex, startIndex + 1, comparisonType);
    }

    /// <summary>
    /// Reports the zero-based index position of the last occurrence of a specified string within this instance. The search starts at a specified character position and proceeds backward toward the beginning of the string for the specified number of character positions. A parameter specifies the type of comparison to perform when searching for the specified string.
    /// </summary>
    /// 
    /// <returns>
    /// The index position of the <paramref name="value"/> parameter if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty"/>. If <paramref name="value"/> is <see cref="F:System.String.Empty"/>, the return value is the smaller of <paramref name="startIndex"/> and the last index position in this instance.
    /// </returns>
    /// <param name="value">The string to seek. </param><param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex"/> toward the beginning of this instance.</param><param name="count">The number of character positions to examine. </param><param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="count"/> is negative.-or-The current instance does not equal <see cref="F:System.String.Empty"/>, and <paramref name="startIndex"/> is negative.-or- The current instance does not equal <see cref="F:System.String.Empty"/>, and <paramref name="startIndex"/> is greater than the length of this instance.-or-The current instance does not equal <see cref="F:System.String.Empty"/>, and <paramref name="startIndex"/> + 1 - <paramref name="count"/> specifies a position that is not within this instance. </exception><exception cref="T:System.ArgumentException"><paramref name="comparisonType"/> is not a valid <see cref="T:System.StringComparison"/> value.</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public int LastIndexOf(string value, int startIndex, int count, StringComparison comparisonType)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (this.Length == 0 && (startIndex == -1 || startIndex == 0))
      {
        return value.Length != 0 ? -1 : 0;
      }
      else
      {
        if (startIndex < 0 || startIndex > this.Length)
          throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (startIndex == this.Length)
        {
          --startIndex;
          if (count > 0)
            --count;
          if (value.Length == 0 && count >= 0 && startIndex - count + 1 >= 0)
            return startIndex;
        }
        if (count < 0 || startIndex - count + 1 < 0)
          throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
        switch (comparisonType)
        {
          case StringComparison.CurrentCulture:
            return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
          case StringComparison.CurrentCultureIgnoreCase:
            return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
          case StringComparison.InvariantCulture:
            return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
          case StringComparison.InvariantCultureIgnoreCase:
            return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
          case StringComparison.Ordinal:
            return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.Ordinal);
          case StringComparison.OrdinalIgnoreCase:
            if (value.IsAscii() && this.IsAscii())
              return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
            else
              return TextInfo.LastIndexOfStringOrdinalIgnoreCase(this, value, startIndex, count);
          default:
            throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
        }
      }
    }

    /// <summary>
    /// Returns a new string that right-aligns the characters in this instance by padding them with spaces on the left, for a specified total length.
    /// </summary>
    /// 
    /// <returns>
    /// A new string that is equivalent to this instance, but right-aligned and padded on the left with as many spaces as needed to create a length of <paramref name="totalWidth"/>. However, if <paramref name="totalWidth"/> is less than the length of this instance, the method returns a reference to the existing instance. If <paramref name="totalWidth"/> is equal to the length of this instance, the method returns a new string that is identical to this instance.
    /// </returns>
    /// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="totalWidth"/> is less than zero. </exception><filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public string PadLeft(int totalWidth)
    {
      return this.PadHelper(totalWidth, ' ', false);
    }

    /// <summary>
    /// Returns a new string that right-aligns the characters in this instance by padding them on the left with a specified Unicode character, for a specified total length.
    /// </summary>
    /// 
    /// <returns>
    /// A new string that is equivalent to this instance, but right-aligned and padded on the left with as many <paramref name="paddingChar"/> characters as needed to create a length of <paramref name="totalWidth"/>. However, if <paramref name="totalWidth"/> is less than the length of this instance, the method returns a reference to the existing instance. If <paramref name="totalWidth"/> is equal to the length of this instance, the method returns a new string that is identical to this instance.
    /// </returns>
    /// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param><param name="paddingChar">A Unicode padding character. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="totalWidth"/> is less than zero. </exception><filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public string PadLeft(int totalWidth, char paddingChar)
    {
      return this.PadHelper(totalWidth, paddingChar, false);
    }

    /// <summary>
    /// Returns a new string that left-aligns the characters in this string by padding them with spaces on the right, for a specified total length.
    /// </summary>
    /// 
    /// <returns>
    /// A new string that is equivalent to this instance, but left-aligned and padded on the right with as many spaces as needed to create a length of <paramref name="totalWidth"/>. However, if <paramref name="totalWidth"/> is less than the length of this instance, the method returns a reference to the existing instance. If <paramref name="totalWidth"/> is equal to the length of this instance, the method returns a new string that is identical to this instance.
    /// </returns>
    /// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="totalWidth"/> is less than zero. </exception><filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public string PadRight(int totalWidth)
    {
      return this.PadHelper(totalWidth, ' ', true);
    }

    /// <summary>
    /// Returns a new string that left-aligns the characters in this string by padding them on the right with a specified Unicode character, for a specified total length.
    /// </summary>
    /// 
    /// <returns>
    /// A new string that is equivalent to this instance, but left-aligned and padded on the right with as many <paramref name="paddingChar"/> characters as needed to create a length of <paramref name="totalWidth"/>.  However, if <paramref name="totalWidth"/> is less than the length of this instance, the method returns a reference to the existing instance. If <paramref name="totalWidth"/> is equal to the length of this instance, the method returns a new string that is identical to this instance.
    /// </returns>
    /// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param><param name="paddingChar">A Unicode padding character. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="totalWidth"/> is less than zero. </exception><filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public string PadRight(int totalWidth, char paddingChar)
    {
      return this.PadHelper(totalWidth, paddingChar, true);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private string PadHelper(int totalWidth, char paddingChar, bool isRightPadded);

    /// <summary>
    /// Determines whether the beginning of this string instance matches the specified string.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="value"/> matches the beginning of this string; otherwise, false.
    /// </returns>
    /// <param name="value">The string to compare. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public bool StartsWith(string value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      else
        return this.StartsWith(value, string.LegacyMode ? StringComparison.Ordinal : StringComparison.CurrentCulture);
    }

    /// <summary>
    /// Determines whether the beginning of this string instance matches the specified string when compared using the specified comparison option.
    /// </summary>
    /// 
    /// <returns>
    /// true if this instance begins with <paramref name="value"/>; otherwise, false.
    /// </returns>
    /// <param name="value">The string to compare. </param><param name="comparisonType">One of the enumeration values that determines how this string and <paramref name="value"/> are compared. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentException"><paramref name="comparisonType"/> is not a <see cref="T:System.StringComparison"/> value.</exception>
    [ComVisible(false)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool StartsWith(string value, StringComparison comparisonType)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      if (this == value || value.Length == 0)
        return true;
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.IgnoreCase);
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this, value, CompareOptions.IgnoreCase);
        case StringComparison.Ordinal:
          if (this.Length < value.Length)
            return false;
          else
            return string.nativeCompareOrdinalEx(this, 0, value, 0, value.Length) == 0;
        case StringComparison.OrdinalIgnoreCase:
          if (this.Length < value.Length)
            return false;
          else
            return TextInfo.CompareOrdinalIgnoreCaseEx(this, 0, value, 0, value.Length, value.Length) == 0;
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      }
    }

    /// <summary>
    /// Determines whether the beginning of this string instance matches the specified string when compared using the specified culture.
    /// </summary>
    /// 
    /// <returns>
    /// true if the <paramref name="value"/> parameter matches the beginning of this string; otherwise, false.
    /// </returns>
    /// <param name="value">The string to compare. </param><param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param><param name="culture">Cultural information that determines how this string and <paramref name="value"/> are compared. If <paramref name="culture"/> is null, the current culture is used.</param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><filterpriority>1</filterpriority>
    public bool StartsWith(string value, bool ignoreCase, CultureInfo culture)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (this == value)
        return true;
      else
        return (culture != null ? culture : (string.LegacyMode ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture)).CompareInfo.IsPrefix(this, value, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
    }

    /// <summary>
    /// Returns a copy of this string converted to lowercase.
    /// </summary>
    /// 
    /// <returns>
    /// A string in lowercase.
    /// </returns>
    /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
    [__DynamicallyInvokable]
    public string ToLower()
    {
      return this.ToLower(string.LegacyMode ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns a copy of this string converted to lowercase, using the casing rules of the specified culture.
    /// </summary>
    /// 
    /// <returns>
    /// The lowercase equivalent of the current string.
    /// </returns>
    /// <param name="culture">An object that supplies culture-specific casing rules. </param><exception cref="T:System.ArgumentNullException"><paramref name="culture"/> is null. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public string ToLower(CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      else
        return culture.TextInfo.ToLower(this);
    }

    /// <summary>
    /// Returns a copy of this <see cref="T:System.String"/> object converted to lowercase using the casing rules of the invariant culture.
    /// </summary>
    /// 
    /// <returns>
    /// The lowercase equivalent of the current string.
    /// </returns>
    /// <filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public string ToLowerInvariant()
    {
      return this.ToLower(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Returns a copy of this string converted to uppercase.
    /// </summary>
    /// 
    /// <returns>
    /// The uppercase equivalent of the current string.
    /// </returns>
    /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
    [__DynamicallyInvokable]
    public string ToUpper()
    {
      return this.ToUpper(string.LegacyMode ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns a copy of this string converted to uppercase, using the casing rules of the specified culture.
    /// </summary>
    /// 
    /// <returns>
    /// The uppercase equivalent of the current string.
    /// </returns>
    /// <param name="culture">An object that supplies culture-specific casing rules. </param><exception cref="T:System.ArgumentNullException"><paramref name="culture"/> is null. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public string ToUpper(CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      else
        return culture.TextInfo.ToUpper(this);
    }

    /// <summary>
    /// Returns a copy of this <see cref="T:System.String"/> object converted to uppercase using the casing rules of the invariant culture.
    /// </summary>
    /// 
    /// <returns>
    /// The uppercase equivalent of the current string.
    /// </returns>
    /// <filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public string ToUpperInvariant()
    {
      return this.ToUpper(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Returns this instance of <see cref="T:System.String"/>; no actual conversion is performed.
    /// </summary>
    /// 
    /// <returns>
    /// The current string.
    /// </returns>
    /// <filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this;
    }

    /// <summary>
    /// Returns this instance of <see cref="T:System.String"/>; no actual conversion is performed.
    /// </summary>
    /// 
    /// <returns>
    /// The current string.
    /// </returns>
    /// <param name="provider">(Reserved) An object that supplies culture-specific formatting information. </param><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public string ToString(IFormatProvider provider)
    {
      return this;
    }

    /// <summary>
    /// Returns a reference to this instance of <see cref="T:System.String"/>.
    /// </summary>
    /// 
    /// <returns>
    /// This instance of <see cref="T:System.String"/>.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    public object Clone()
    {
      return (object) this;
    }

    /// <summary>
    /// Removes all leading and trailing white-space characters from the current <see cref="T:System.String"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// The string that remains after all white-space characters are removed from the start and end of the current string.
    /// </returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public string Trim()
    {
      return this.TrimHelper(2);
    }

    [SecuritySafeCritical]
    private string TrimHelper(int trimType)
    {
      int end = this.Length - 1;
      int start = 0;
      if (trimType != 1)
      {
        start = 0;
        while (start < this.Length && char.IsWhiteSpace(this[start]))
          ++start;
      }
      if (trimType != 0)
      {
        end = this.Length - 1;
        while (end >= start && char.IsWhiteSpace(this[end]))
          --end;
      }
      return this.CreateTrimmedString(start, end);
    }

    [SecuritySafeCritical]
    private string TrimHelper(char[] trimChars, int trimType)
    {
      int end = this.Length - 1;
      int start = 0;
      if (trimType != 1)
      {
        for (start = 0; start < this.Length; ++start)
        {
          char ch = this[start];
          int index = 0;
          while (index < trimChars.Length && (int) trimChars[index] != (int) ch)
            ++index;
          if (index == trimChars.Length)
            break;
        }
      }
      if (trimType != 0)
      {
        for (end = this.Length - 1; end >= start; --end)
        {
          char ch = this[end];
          int index = 0;
          while (index < trimChars.Length && (int) trimChars[index] != (int) ch)
            ++index;
          if (index == trimChars.Length)
            break;
        }
      }
      return this.CreateTrimmedString(start, end);
    }

    /// <summary>
    /// Returns a new string in which a specified string is inserted at a specified index position in this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A new string that is equivalent to this instance, but with <paramref name="value"/> inserted at position <paramref name="startIndex"/>.
    /// </returns>
    /// <param name="startIndex">The zero-based index position of the insertion. </param><param name="value">The string to insert. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is negative or greater than the length of this instance. </exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string Insert(int startIndex, string value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (startIndex < 0 || startIndex > this.Length)
        throw new ArgumentOutOfRangeException("startIndex");
      else
        return this.InsertInternal(startIndex, value);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private string ReplaceInternal(char oldChar, char newChar);

    /// <summary>
    /// Returns a new string in which all occurrences of a specified Unicode character in this instance are replaced with another specified Unicode character.
    /// </summary>
    /// 
    /// <returns>
    /// A string that is equivalent to this instance except that all instances of <paramref name="oldChar"/> are replaced with <paramref name="newChar"/>.
    /// </returns>
    /// <param name="oldChar">The Unicode character to be replaced. </param><param name="newChar">The Unicode character to replace all occurrences of <paramref name="oldChar"/>. </param><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public string Replace(char oldChar, char newChar)
    {
      return this.ReplaceInternal(oldChar, newChar);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private string ReplaceInternal(string oldValue, string newValue);

    /// <summary>
    /// Returns a new string in which all occurrences of a specified string in the current instance are replaced with another specified string.
    /// </summary>
    /// 
    /// <returns>
    /// A string that is equivalent to the current string except that all instances of <paramref name="oldValue"/> are replaced with <paramref name="newValue"/>.
    /// </returns>
    /// <param name="oldValue">The string to be replaced. </param><param name="newValue">The string to replace all occurrences of <paramref name="oldValue"/>. </param><exception cref="T:System.ArgumentNullException"><paramref name="oldValue"/> is null. </exception><exception cref="T:System.ArgumentException"><paramref name="oldValue"/> is the empty string (""). </exception><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public string Replace(string oldValue, string newValue)
    {
      if (oldValue == null)
        throw new ArgumentNullException("oldValue");
      else
        return this.ReplaceInternal(oldValue, newValue);
    }

    /// <summary>
    /// Returns a new string in which a specified number of characters in the current this instance beginning at a specified position have been deleted.
    /// </summary>
    /// 
    /// <returns>
    /// A new string that is equivalent to this instance except for the removed characters.
    /// </returns>
    /// <param name="startIndex">The zero-based position to begin deleting characters. </param><param name="count">The number of characters to delete. </param><exception cref="T:System.ArgumentOutOfRangeException">Either <paramref name="startIndex"/> or <paramref name="count"/> is less than zero.-or- <paramref name="startIndex"/> plus <paramref name="count"/> specify a position outside this instance. </exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string Remove(int startIndex, int count)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      else
        return this.RemoveInternal(startIndex, count);
    }

    /// <summary>
    /// Returns a new string in which all the characters in the current instance, beginning at a specified position and continuing through the last position, have been deleted.
    /// </summary>
    /// 
    /// <returns>
    /// A new string that is equivalent to this string except for the removed characters.
    /// </returns>
    /// <param name="startIndex">The zero-based position to begin deleting characters. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero.-or- <paramref name="startIndex"/> specifies a position that is not within this string. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public string Remove(int startIndex)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (startIndex >= this.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndexLessThanLength"));
      else
        return this.Substring(0, startIndex);
    }

    /// <summary>
    /// Replaces one or more format items in a specified string with the string representation of a specified object.
    /// </summary>
    /// 
    /// <returns>
    /// A copy of <paramref name="format"/> in which any format items are replaced by the string representation of <paramref name="arg0"/>.
    /// </returns>
    /// <param name="format">A composite format string (see Remarks). </param><param name="arg0">The object to format. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> is null. </exception><exception cref="T:System.FormatException">The format item in <paramref name="format"/> is invalid.-or- The index of a format item is not zero. </exception><filterpriority>1</filterpriority>
    public static string Format(string format, object arg0)
    {
      if (format == null)
        throw new ArgumentNullException("format");
      return string.Format((IFormatProvider) null, format, new object[1]
      {
        arg0
      });
    }

    /// <summary>
    /// Replaces the format items in a specified string with the string representation of two specified objects.
    /// </summary>
    /// 
    /// <returns>
    /// A copy of <paramref name="format"/> in which format items are replaced by the string representations of <paramref name="arg0"/> and <paramref name="arg1"/>.
    /// </returns>
    /// <param name="format">A composite format string (see Remarks). </param><param name="arg0">The first object to format. </param><param name="arg1">The second object to format. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="format"/> is invalid.-or- The index of a format item is not zero or one. </exception><filterpriority>1</filterpriority>
    public static string Format(string format, object arg0, object arg1)
    {
      if (format == null)
        throw new ArgumentNullException("format");
      return string.Format((IFormatProvider) null, format, new object[2]
      {
        arg0,
        arg1
      });
    }

    /// <summary>
    /// Replaces the format items in a specified string with the string representation of three specified objects.
    /// </summary>
    /// 
    /// <returns>
    /// A copy of <paramref name="format"/> in which the format items have been replaced by the string representations of <paramref name="arg0"/>, <paramref name="arg1"/>, and <paramref name="arg2"/>.
    /// </returns>
    /// <param name="format">A composite format string (see Remarks).</param><param name="arg0">The first object to format. </param><param name="arg1">The second object to format. </param><param name="arg2">The third object to format. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="format"/> is invalid.-or- The index of a format item is less than zero, or greater than two. </exception><filterpriority>1</filterpriority>
    public static string Format(string format, object arg0, object arg1, object arg2)
    {
      if (format == null)
        throw new ArgumentNullException("format");
      return string.Format((IFormatProvider) null, format, arg0, arg1, arg2);
    }

    /// <summary>
    /// Replaces the format item in a specified string with the string representation of a corresponding object in a specified array.
    /// </summary>
    /// 
    /// <returns>
    /// A copy of <paramref name="format"/> in which the format items have been replaced by the string representation of the corresponding objects in <paramref name="args"/>.
    /// </returns>
    /// <param name="format">A composite format string (see Remarks).</param><param name="args">An object array that contains zero or more objects to format. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> or <paramref name="args"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="format"/> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Format(string format, params object[] args)
    {
      if (format == null || args == null)
        throw new ArgumentNullException(format == null ? "format" : "args");
      else
        return string.Format((IFormatProvider) null, format, args);
    }

    /// <summary>
    /// Replaces the format item in a specified string with the string representation of a corresponding object in a specified array. A specified parameter supplies culture-specific formatting information.
    /// </summary>
    /// 
    /// <returns>
    /// A copy of <paramref name="format"/> in which the format items have been replaced by the string representation of the corresponding objects in <paramref name="args"/>.
    /// </returns>
    /// <param name="provider">An object that supplies culture-specific formatting information. </param><param name="format">A composite format string (see Remarks). </param><param name="args">An object array that contains zero or more objects to format. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> or <paramref name="args"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="format"/> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Format(IFormatProvider provider, string format, params object[] args)
    {
      if (format == null || args == null)
        throw new ArgumentNullException(format == null ? "format" : "args");
      StringBuilder sb = StringBuilderCache.Acquire(format.Length + args.Length * 8);
      sb.AppendFormat(provider, format, args);
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>
    /// Creates a new instance of <see cref="T:System.String"/> with the same value as a specified <see cref="T:System.String"/>.
    /// </summary>
    /// 
    /// <returns>
    /// A new string with the same value as <paramref name="str"/>.
    /// </returns>
    /// <param name="str">The string to copy. </param><exception cref="T:System.ArgumentNullException"><paramref name="str"/> is null. </exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static unsafe string Copy(string str)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      int length = str.Length;
      string str1 = string.FastAllocateString(length);
      fixed (char* dmem = &str1.m_firstChar)
        fixed (char* smem = &str.m_firstChar)
          string.wstrcpyPtrAligned(dmem, smem, length);
      return str1;
    }

    /// <summary>
    /// Creates the string  representation of a specified object.
    /// </summary>
    /// 
    /// <returns>
    /// The string representation of the value of <paramref name="arg0"/>, or <see cref="F:System.String.Empty"/> if <paramref name="arg0"/> is null.
    /// </returns>
    /// <param name="arg0">The object to represent, or null. </param><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Concat(object arg0)
    {
      if (arg0 == null)
        return string.Empty;
      else
        return arg0.ToString();
    }

    /// <summary>
    /// Concatenates the string representations of two specified objects.
    /// </summary>
    /// 
    /// <returns>
    /// The concatenated string representations of the values of <paramref name="arg0"/> and <paramref name="arg1"/>.
    /// </returns>
    /// <param name="arg0">The first object to concatenate. </param><param name="arg1">The second object to concatenate. </param><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Concat(object arg0, object arg1)
    {
      if (arg0 == null)
        arg0 = (object) string.Empty;
      if (arg1 == null)
        arg1 = (object) string.Empty;
      return arg0.ToString() + arg1.ToString();
    }

    /// <summary>
    /// Concatenates the string representations of three specified objects.
    /// </summary>
    /// 
    /// <returns>
    /// The concatenated string representations of the values of <paramref name="arg0"/>, <paramref name="arg1"/>, and <paramref name="arg2"/>.
    /// </returns>
    /// <param name="arg0">The first object to concatenate. </param><param name="arg1">The second object to concatenate. </param><param name="arg2">The third object to concatenate. </param><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Concat(object arg0, object arg1, object arg2)
    {
      if (arg0 == null)
        arg0 = (object) string.Empty;
      if (arg1 == null)
        arg1 = (object) string.Empty;
      if (arg2 == null)
        arg2 = (object) string.Empty;
      return arg0.ToString() + arg1.ToString() + arg2.ToString();
    }

    [CLSCompliant(false)]
    public static string Concat(object arg0, object arg1, object arg2, object arg3, __arglist)
    {
      ArgIterator argIterator = new ArgIterator(__arglist);
      int length = argIterator.GetRemainingCount() + 4;
      object[] objArray = new object[length];
      objArray[0] = arg0;
      objArray[1] = arg1;
      objArray[2] = arg2;
      objArray[3] = arg3;
      for (int index = 4; index < length; ++index)
        objArray[index] = TypedReference.ToObject(argIterator.GetNextArg());
      return string.Concat(objArray);
    }

    /// <summary>
    /// Concatenates the string representations of the elements in a specified <see cref="T:System.Object"/> array.
    /// </summary>
    /// 
    /// <returns>
    /// The concatenated string representations of the values of the elements in <paramref name="args"/>.
    /// </returns>
    /// <param name="args">An object array that contains the elements to concatenate. </param><exception cref="T:System.ArgumentNullException"><paramref name="args"/> is null. </exception><exception cref="T:System.OutOfMemoryException">Out of memory.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Concat(params object[] args)
    {
      if (args == null)
        throw new ArgumentNullException("args");
      string[] values = new string[args.Length];
      int totalLength = 0;
      for (int index = 0; index < args.Length; ++index)
      {
        object obj = args[index];
        values[index] = obj == null ? string.Empty : obj.ToString();
        if (values[index] == null)
          values[index] = string.Empty;
        totalLength += values[index].Length;
        if (totalLength < 0)
          throw new OutOfMemoryException();
      }
      return string.ConcatArray(values, totalLength);
    }

    /// <summary>
    /// Concatenates the members of an <see cref="T:System.Collections.Generic.IEnumerable`1"/> implementation.
    /// </summary>
    /// 
    /// <returns>
    /// The concatenated members in <paramref name="values"/>.
    /// </returns>
    /// <param name="values">A collection object that implements the <see cref="T:System.Collections.Generic.IEnumerable`1"/> interface.</param><typeparam name="T">The type of the members of <paramref name="values"/>.</typeparam><exception cref="T:System.ArgumentNullException"><paramref name="values"/> is null. </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static string Concat<T>(IEnumerable<T> values)
    {
      if (values == null)
        throw new ArgumentNullException("values");
      StringBuilder sb = StringBuilderCache.Acquire(16);
      using (IEnumerator<T> enumerator = values.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if ((object) enumerator.Current != null)
          {
            string str = enumerator.Current.ToString();
            if (str != null)
              sb.Append(str);
          }
        }
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>
    /// Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable`1"/> collection of type <see cref="T:System.String"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The concatenated strings in <paramref name="values"/>.
    /// </returns>
    /// <param name="values">A collection object that implements <see cref="T:System.Collections.Generic.IEnumerable`1"/> and whose generic type argument is <see cref="T:System.String"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="values"/> is null. </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static string Concat(IEnumerable<string> values)
    {
      if (values == null)
        throw new ArgumentNullException("values");
      StringBuilder sb = StringBuilderCache.Acquire(16);
      using (IEnumerator<string> enumerator = values.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if (enumerator.Current != null)
            sb.Append(enumerator.Current);
        }
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>
    /// Concatenates two specified instances of <see cref="T:System.String"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The concatenation of <paramref name="str0"/> and <paramref name="str1"/>.
    /// </returns>
    /// <param name="str0">The first string to concatenate. </param><param name="str1">The second string to concatenate. </param><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string Concat(string str0, string str1)
    {
      if (string.IsNullOrEmpty(str0))
      {
        if (string.IsNullOrEmpty(str1))
          return string.Empty;
        else
          return str1;
      }
      else
      {
        if (string.IsNullOrEmpty(str1))
          return str0;
        int length = str0.Length;
        string dest = string.FastAllocateString(length + str1.Length);
        string.FillStringChecked(dest, 0, str0);
        string.FillStringChecked(dest, length, str1);
        return dest;
      }
    }

    /// <summary>
    /// Concatenates three specified instances of <see cref="T:System.String"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The concatenation of <paramref name="str0"/>, <paramref name="str1"/>, and <paramref name="str2"/>.
    /// </returns>
    /// <param name="str0">The first string to concatenate. </param><param name="str1">The second string to concatenate. </param><param name="str2">The third string to concatenate. </param><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string Concat(string str0, string str1, string str2)
    {
      if (str0 == null && str1 == null && str2 == null)
        return string.Empty;
      if (str0 == null)
        str0 = string.Empty;
      if (str1 == null)
        str1 = string.Empty;
      if (str2 == null)
        str2 = string.Empty;
      string dest = string.FastAllocateString(str0.Length + str1.Length + str2.Length);
      string.FillStringChecked(dest, 0, str0);
      string.FillStringChecked(dest, str0.Length, str1);
      string.FillStringChecked(dest, str0.Length + str1.Length, str2);
      return dest;
    }

    /// <summary>
    /// Concatenates four specified instances of <see cref="T:System.String"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The concatenation of <paramref name="str0"/>, <paramref name="str1"/>, <paramref name="str2"/>, and <paramref name="str3"/>.
    /// </returns>
    /// <param name="str0">The first string to concatenate. </param><param name="str1">The second string to concatenate. </param><param name="str2">The third string to concatenate. </param><param name="str3">The fourth string to concatenate. </param><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string Concat(string str0, string str1, string str2, string str3)
    {
      if (str0 == null && str1 == null && (str2 == null && str3 == null))
        return string.Empty;
      if (str0 == null)
        str0 = string.Empty;
      if (str1 == null)
        str1 = string.Empty;
      if (str2 == null)
        str2 = string.Empty;
      if (str3 == null)
        str3 = string.Empty;
      string dest = string.FastAllocateString(str0.Length + str1.Length + str2.Length + str3.Length);
      string.FillStringChecked(dest, 0, str0);
      string.FillStringChecked(dest, str0.Length, str1);
      string.FillStringChecked(dest, str0.Length + str1.Length, str2);
      string.FillStringChecked(dest, str0.Length + str1.Length + str2.Length, str3);
      return dest;
    }

    /// <summary>
    /// Concatenates the elements of a specified <see cref="T:System.String"/> array.
    /// </summary>
    /// 
    /// <returns>
    /// The concatenated elements of <paramref name="values"/>.
    /// </returns>
    /// <param name="values">An array of string instances. </param><exception cref="T:System.ArgumentNullException"><paramref name="values"/> is null. </exception><exception cref="T:System.OutOfMemoryException">Out of memory.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Concat(params string[] values)
    {
      if (values == null)
        throw new ArgumentNullException("values");
      int totalLength = 0;
      string[] values1 = new string[values.Length];
      for (int index = 0; index < values.Length; ++index)
      {
        string str = values[index];
        values1[index] = str == null ? string.Empty : str;
        totalLength += values1[index].Length;
        if (totalLength < 0)
          throw new OutOfMemoryException();
      }
      return string.ConcatArray(values1, totalLength);
    }

    /// <summary>
    /// Retrieves the system's reference to the specified <see cref="T:System.String"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The system's reference to <paramref name="str"/>, if it is interned; otherwise, a new reference to a string with the value of <paramref name="str"/>.
    /// </returns>
    /// <param name="str">A string to search for in the intern pool. </param><exception cref="T:System.ArgumentNullException"><paramref name="str"/> is null. </exception><filterpriority>2</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [SecuritySafeCritical]
    public static string Intern(string str)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      else
        return Thread.GetDomain().GetOrInternString(str);
    }

    /// <summary>
    /// Retrieves a reference to a specified <see cref="T:System.String"/>.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to <paramref name="str"/> if it is in the common language runtime intern pool; otherwise, null.
    /// </returns>
    /// <param name="str">The string to search for in the intern pool. </param><exception cref="T:System.ArgumentNullException"><paramref name="str"/> is null. </exception><filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public static string IsInterned(string str)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      else
        return Thread.GetDomain().IsStringInterned(str);
    }

    /// <summary>
    /// Returns the <see cref="T:System.TypeCode"/> for class <see cref="T:System.String"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The enumerated constant, <see cref="F:System.TypeCode.String"/>.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public TypeCode GetTypeCode()
    {
      return TypeCode.String;
    }

    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      return Convert.ToBoolean(this, provider);
    }

    char IConvertible.ToChar(IFormatProvider provider)
    {
      return Convert.ToChar(this, provider);
    }

    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      return Convert.ToSByte(this, provider);
    }

    byte IConvertible.ToByte(IFormatProvider provider)
    {
      return Convert.ToByte(this, provider);
    }

    short IConvertible.ToInt16(IFormatProvider provider)
    {
      return Convert.ToInt16(this, provider);
    }

    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      return Convert.ToUInt16(this, provider);
    }

    int IConvertible.ToInt32(IFormatProvider provider)
    {
      return Convert.ToInt32(this, provider);
    }

    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      return Convert.ToUInt32(this, provider);
    }

    long IConvertible.ToInt64(IFormatProvider provider)
    {
      return Convert.ToInt64(this, provider);
    }

    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      return Convert.ToUInt64(this, provider);
    }

    float IConvertible.ToSingle(IFormatProvider provider)
    {
      return Convert.ToSingle(this, provider);
    }

    double IConvertible.ToDouble(IFormatProvider provider)
    {
      return Convert.ToDouble(this, provider);
    }

    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      return Convert.ToDecimal(this, provider);
    }

    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      return Convert.ToDateTime(this, provider);
    }

    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal bool IsFastSort();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal bool IsAscii();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal void SetTrailByte(byte data);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal bool TryGetTrailByte(out byte data);

    /// <summary>
    /// Retrieves an object that can iterate through the individual characters in this string.
    /// </summary>
    /// 
    /// <returns>
    /// An enumerator object.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    public CharEnumerator GetEnumerator()
    {
      return new CharEnumerator(this);
    }

    IEnumerator<char> IEnumerable<char>.GetEnumerator()
    {
      return (IEnumerator<char>) new CharEnumerator(this);
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new CharEnumerator(this);
    }

    [ForceTokenStabilization]
    [SecurityCritical]
    internal static unsafe void InternalCopy(string src, IntPtr dest, int len)
    {
      if (len == 0)
        return;
      fixed (char* chPtr = &src.m_firstChar)
        Buffer.Memcpy((byte*) dest.ToPointer(), (byte*) chPtr, len);
    }

    [SecuritySafeCritical]
    private static unsafe int CompareOrdinalIgnoreCaseHelper(string strA, string strB)
    {
      int num1 = Math.Min(strA.Length, strB.Length);
      fixed (char* chPtr1 = &strA.m_firstChar)
        fixed (char* chPtr2 = &strB.m_firstChar)
        {
          char* chPtr3 = chPtr1;
          char* chPtr4 = chPtr2;
          for (; num1 != 0; --num1)
          {
            int num2 = (int) *chPtr3;
            int num3 = (int) *chPtr4;
            if ((uint) (num2 - 97) <= 25U)
              num2 -= 32;
            if ((uint) (num3 - 97) <= 25U)
              num3 -= 32;
            if (num2 != num3)
              return num2 - num3;
            ++chPtr3;
            ++chPtr4;
          }
          return strA.Length - strB.Length;
        }
    }

    private string[] InternalSplitKeepEmptyEntries(int[] sepList, int[] lengthList, int numReplaces, int count)
    {
      int startIndex = 0;
      int index1 = 0;
      --count;
      int num = numReplaces < count ? numReplaces : count;
      string[] strArray = new string[num + 1];
      for (int index2 = 0; index2 < num && startIndex < this.Length; ++index2)
      {
        strArray[index1++] = this.Substring(startIndex, sepList[index2] - startIndex);
        startIndex = sepList[index2] + (lengthList == null ? 1 : lengthList[index2]);
      }
      if (startIndex < this.Length && num >= 0)
        strArray[index1] = this.Substring(startIndex);
      else if (index1 == num)
        strArray[index1] = string.Empty;
      return strArray;
    }

    private string[] InternalSplitOmitEmptyEntries(int[] sepList, int[] lengthList, int numReplaces, int count)
    {
      int length1 = numReplaces < count ? numReplaces + 1 : count;
      string[] strArray1 = new string[length1];
      int startIndex = 0;
      int length2 = 0;
      for (int index = 0; index < numReplaces && startIndex < this.Length; ++index)
      {
        if (sepList[index] - startIndex > 0)
          strArray1[length2++] = this.Substring(startIndex, sepList[index] - startIndex);
        startIndex = sepList[index] + (lengthList == null ? 1 : lengthList[index]);
        if (length2 == count - 1)
        {
          while (index < numReplaces - 1 && startIndex == sepList[++index])
            startIndex += lengthList == null ? 1 : lengthList[index];
          break;
        }
      }
      if (startIndex < this.Length)
        strArray1[length2++] = this.Substring(startIndex);
      string[] strArray2 = strArray1;
      if (length2 != length1)
      {
        strArray2 = new string[length2];
        for (int index = 0; index < length2; ++index)
          strArray2[index] = strArray1[index];
      }
      return strArray2;
    }

    [SecuritySafeCritical]
    private unsafe int MakeSeparatorList(char[] separator, ref int[] sepList)
    {
      int num1 = 0;
      if (separator == null || separator.Length == 0)
      {
        fixed (char* chPtr = &this.m_firstChar)
        {
          for (int index = 0; index < this.Length && num1 < sepList.Length; ++index)
          {
            if (char.IsWhiteSpace(chPtr[index]))
              sepList[num1++] = index;
          }
        }
      }
      else
      {
        int length1 = sepList.Length;
        int length2 = separator.Length;
        fixed (char* chPtr1 = &this.m_firstChar)
          fixed (char* chPtr2 = separator)
          {
            for (int index = 0; index < this.Length && num1 < length1; ++index)
            {
              char* chPtr3 = chPtr2;
              int num2 = 0;
              while (num2 < length2)
              {
                if ((int) chPtr1[index] == (int) *chPtr3)
                {
                  sepList[num1++] = index;
                  break;
                }
                else
                {
                  ++num2;
                  ++chPtr3;
                }
              }
            }
          }
      }
      return num1;
    }

    [SecuritySafeCritical]
    private unsafe int MakeSeparatorList(string[] separators, ref int[] sepList, ref int[] lengthList)
    {
      int index1 = 0;
      int length1 = sepList.Length;
      fixed (char* chPtr = &this.m_firstChar)
      {
        for (int indexA = 0; indexA < this.Length && index1 < length1; ++indexA)
        {
          for (int index2 = 0; index2 < separators.Length; ++index2)
          {
            string strB = separators[index2];
            if (!string.IsNullOrEmpty(strB))
            {
              int length2 = strB.Length;
              if ((int) chPtr[indexA] == (int) strB[0] && length2 <= this.Length - indexA && (length2 == 1 || string.CompareOrdinal(this, indexA, strB, 0, length2) == 0))
              {
                sepList[index1] = indexA;
                lengthList[index1] = length2;
                ++index1;
                indexA += length2 - 1;
                break;
              }
            }
          }
        }
      }
      return index1;
    }

    [SecurityCritical]
    private unsafe string InternalSubString(int startIndex, int length, bool fAlwaysCopy)
    {
      if (startIndex == 0 && length == this.Length && !fAlwaysCopy)
        return this;
      string str = string.FastAllocateString(length);
      fixed (char* dmem = &str.m_firstChar)
        fixed (char* chPtr = &this.m_firstChar)
          string.wstrcpy(dmem, chPtr + startIndex, length);
      return str;
    }

    [SecurityCritical]
    private static unsafe string CreateString(sbyte* value, int startIndex, int length, Encoding enc)
    {
      if (enc == null)
        return new string(value, startIndex, length);
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (value + startIndex < value)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
      byte[] numArray = new byte[length];
      try
      {
        Buffer.Memcpy(numArray, 0, (byte*) value, startIndex, length);
      }
      catch (NullReferenceException ex)
      {
        throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
      }
      return enc.GetString(numArray);
    }

    [SecuritySafeCritical]
    private static unsafe void FillStringChecked(string dest, int destPos, string src)
    {
      if (src.Length > dest.Length - destPos)
        throw new IndexOutOfRangeException();
      fixed (char* chPtr = &dest.m_firstChar)
        fixed (char* smem = &src.m_firstChar)
          string.wstrcpy(chPtr + destPos, smem, src.Length);
    }

    [SecurityCritical]
    private static unsafe void wstrcpyPtrAligned(char* dmem, char* smem, int charCount)
    {
      while (charCount >= 8)
      {
        *(int*) dmem = (int) *(uint*) smem;
        *(int*) (dmem + 2) = (int) *(uint*) (smem + 2);
        *(int*) (dmem + 4) = (int) *(uint*) (smem + 4);
        *(int*) (dmem + 6) = (int) *(uint*) (smem + 6);
        dmem += 8;
        smem += 8;
        charCount -= 8;
      }
      if ((charCount & 4) != 0)
      {
        *(int*) dmem = (int) *(uint*) smem;
        *(int*) (dmem + 2) = (int) *(uint*) (smem + 2);
        dmem += 4;
        smem += 4;
      }
      if ((charCount & 2) != 0)
      {
        *(int*) dmem = (int) *(uint*) smem;
        dmem += 2;
        smem += 2;
      }
      if ((charCount & 1) == 0)
        return;
      *dmem = *smem;
    }

    [SecuritySafeCritical]
    private unsafe string CtorCharArray(char[] value)
    {
      if (value == null || value.Length == 0)
        return string.Empty;
      string str = string.FastAllocateString(value.Length);
      fixed (char* dmem = str)
        fixed (char* smem = value)
          string.wstrcpyPtrAligned(dmem, smem, value.Length);
      return str;
    }

    [SecuritySafeCritical]
    private unsafe string CtorCharArrayStartLength(char[] value, int startIndex, int length)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (startIndex > value.Length - length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (length <= 0)
        return string.Empty;
      string str = string.FastAllocateString(length);
      fixed (char* dmem = str)
        fixed (char* chPtr = value)
          string.wstrcpy(dmem, chPtr + startIndex, length);
      return str;
    }

    [SecuritySafeCritical]
    private unsafe string CtorCharCount(char c, int count)
    {
      if (count > 0)
      {
        string str = string.FastAllocateString(count);
        fixed (char* chPtr1 = str)
        {
          char* chPtr2;
          for (chPtr2 = chPtr1; ((int) (uint) chPtr2 & 3) != 0 && count > 0; --count)
            *chPtr2++ = c;
          uint num = (uint) c << 16 | (uint) c;
          if (count >= 4)
          {
            count -= 4;
            do
            {
              *(int*) chPtr2 = (int) num;
              *(int*) (chPtr2 + 2) = (int) num;
              chPtr2 += 4;
              count -= 4;
            }
            while (count >= 0);
          }
          if ((count & 2) != 0)
          {
            *(int*) chPtr2 = (int) num;
            chPtr2 += 2;
          }
          if ((count & 1) != 0)
            *chPtr2 = c;
        }
        return str;
      }
      else
      {
        if (count == 0)
          return string.Empty;
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", new object[1]
        {
          (object) "count"
        }));
      }
    }

    [SecurityCritical]
    private unsafe string CtorCharPtr(char* ptr)
    {
      if ((IntPtr) ptr == IntPtr.Zero)
        return string.Empty;
      if ((UIntPtr) ptr < UIntPtr(64000))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeStringPtrNotAtom"));
      try
      {
        int num = string.wcslen(ptr);
        if (num == 0)
          return string.Empty;
        string str = string.FastAllocateString(num);
        fixed (char* dmem = str)
          string.wstrcpy(dmem, ptr, num);
        return str;
      }
      catch (NullReferenceException ex)
      {
        throw new ArgumentOutOfRangeException("ptr", Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
      }
    }

    [SecurityCritical]
    private string CreateTrimmedString(int start, int end)
    {
      int length = end - start + 1;
      if (length == this.Length)
        return this;
      if (length == 0)
        return string.Empty;
      else
        return this.InternalSubString(start, length, false);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private string InsertInternal(int startIndex, string value);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private string RemoveInternal(int startIndex, int count);

    [SecuritySafeCritical]
    private static string ConcatArray(string[] values, int totalLength)
    {
      string dest = string.FastAllocateString(totalLength);
      int destPos = 0;
      for (int index = 0; index < values.Length; ++index)
      {
        string.FillStringChecked(dest, destPos, values[index]);
        destPos += values[index].Length;
      }
      return dest;
    }
  }
}
