// Type: System.Text.StringBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  /// <summary>
  /// Represents a mutable string of characters. This class cannot be inherited.
  /// </summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class StringBuilder : ISerializable
  {
    internal const int DefaultCapacity = 16;
    internal const int MaxChunkSize = 8000;
    internal char[] m_ChunkChars;
    internal StringBuilder m_ChunkPrevious;
    internal int m_ChunkLength;
    internal int m_ChunkOffset;
    internal int m_MaxCapacity;
    private const string CapacityField = "Capacity";
    private const string MaxCapacityField = "m_MaxCapacity";
    private const string StringValueField = "m_StringValue";
    private const string ThreadIDField = "m_currentThread";

    /// <summary>
    /// Gets or sets the maximum number of characters that can be contained in the memory allocated by the current instance.
    /// </summary>
    /// 
    /// <returns>
    /// The maximum number of characters that can be contained in the memory allocated by the current instance.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than the current length of this instance.-or- The value specified for a set operation is greater than the maximum capacity. </exception><filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int Capacity
    {
      [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"), __DynamicallyInvokable] get
      {
        return this.m_ChunkChars.Length + this.m_ChunkOffset;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NegativeCapacity"));
        if (value > this.MaxCapacity)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_Capacity"));
        if (value < this.Length)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        if (this.Capacity == value)
          return;
        char[] chArray = new char[value - this.m_ChunkOffset];
        Array.Copy((Array) this.m_ChunkChars, (Array) chArray, this.m_ChunkLength);
        this.m_ChunkChars = chArray;
      }
    }

    /// <summary>
    /// Gets the maximum capacity of this instance.
    /// </summary>
    /// 
    /// <returns>
    /// The maximum number of characters this instance can hold.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int MaxCapacity
    {
      [__DynamicallyInvokable, TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.m_MaxCapacity;
      }
    }

    /// <summary>
    /// Gets or sets the length of the current <see cref="T:System.Text.StringBuilder"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// The length of this instance.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than zero or greater than <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Length
    {
      [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"), __DynamicallyInvokable] get
      {
        return this.m_ChunkOffset + this.m_ChunkLength;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
        if (value > this.MaxCapacity)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        int capacity = this.Capacity;
        if (value == 0 && this.m_ChunkPrevious == null)
        {
          this.m_ChunkLength = 0;
          this.m_ChunkOffset = 0;
        }
        else
        {
          int repeatCount = value - this.Length;
          if (repeatCount > 0)
          {
            this.Append(char.MinValue, repeatCount);
          }
          else
          {
            StringBuilder chunkForIndex = this.FindChunkForIndex(value);
            if (chunkForIndex != this)
            {
              char[] chArray = new char[capacity - chunkForIndex.m_ChunkOffset];
              Array.Copy((Array) chunkForIndex.m_ChunkChars, (Array) chArray, chunkForIndex.m_ChunkLength);
              this.m_ChunkChars = chArray;
              this.m_ChunkPrevious = chunkForIndex.m_ChunkPrevious;
              this.m_ChunkOffset = chunkForIndex.m_ChunkOffset;
            }
            this.m_ChunkLength = value - chunkForIndex.m_ChunkOffset;
          }
        }
      }
    }

    /// <summary>
    /// Gets or sets the character at the specified character position in this instance.
    /// </summary>
    /// 
    /// <returns>
    /// The Unicode character at position <paramref name="index"/>.
    /// </returns>
    /// <param name="index">The position of the character. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the bounds of this instance while setting a character. </exception><exception cref="T:System.IndexOutOfRangeException"><paramref name="index"/> is outside the bounds of this instance while getting a character. </exception><filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    [IndexerName("Chars")]
    public char this[int index]
    {
      [__DynamicallyInvokable] get
      {
        StringBuilder stringBuilder = this;
        do
        {
          int index1 = index - stringBuilder.m_ChunkOffset;
          if (index1 >= 0)
          {
            if (index1 >= stringBuilder.m_ChunkLength)
              throw new IndexOutOfRangeException();
            else
              return stringBuilder.m_ChunkChars[index1];
          }
          else
            stringBuilder = stringBuilder.m_ChunkPrevious;
        }
        while (stringBuilder != null);
        throw new IndexOutOfRangeException();
      }
      [__DynamicallyInvokable] set
      {
        StringBuilder stringBuilder = this;
        do
        {
          int index1 = index - stringBuilder.m_ChunkOffset;
          if (index1 >= 0)
          {
            if (index1 >= stringBuilder.m_ChunkLength)
              throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
            stringBuilder.m_ChunkChars[index1] = value;
            return;
          }
          else
            stringBuilder = stringBuilder.m_ChunkPrevious;
        }
        while (stringBuilder != null);
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Text.StringBuilder"/> class.
    /// </summary>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public StringBuilder()
      : this(16)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Text.StringBuilder"/> class using the specified capacity.
    /// </summary>
    /// <param name="capacity">The suggested starting size of this instance. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity"/> is less than zero. </exception>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public StringBuilder(int capacity)
      : this(string.Empty, capacity)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Text.StringBuilder"/> class using the specified string.
    /// </summary>
    /// <param name="value">The string used to initialize the value of the instance. If <paramref name="value"/> is null, the new <see cref="T:System.Text.StringBuilder"/> will contain the empty string (that is, it contains <see cref="F:System.String.Empty"/>). </param>
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public StringBuilder(string value)
      : this(value, 16)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Text.StringBuilder"/> class using the specified string and capacity.
    /// </summary>
    /// <param name="value">The string used to initialize the value of the instance. If <paramref name="value"/> is null, the new <see cref="T:System.Text.StringBuilder"/> will contain the empty string (that is, it contains <see cref="F:System.String.Empty"/>). </param><param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder"/>. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity"/> is less than zero. </exception>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public StringBuilder(string value, int capacity)
      : this(value, 0, value != null ? value.Length : 0, capacity)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Text.StringBuilder"/> class from the specified substring and capacity.
    /// </summary>
    /// <param name="value">The string that contains the substring used to initialize the value of this instance. If <paramref name="value"/> is null, the new <see cref="T:System.Text.StringBuilder"/> will contain the empty string (that is, it contains <see cref="F:System.String.Empty"/>). </param><param name="startIndex">The position within <paramref name="value"/> where the substring begins. </param><param name="length">The number of characters in the substring. </param><param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder"/>. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity"/> is less than zero.-or- <paramref name="startIndex"/> plus <paramref name="length"/> is not a position within <paramref name="value"/>. </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder(string value, int startIndex, int length, int capacity)
    {
      if (capacity < 0)
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", new object[1]
        {
          (object) "capacity"
        }));
      else if (length < 0)
      {
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", new object[1]
        {
          (object) "length"
        }));
      }
      else
      {
        if (startIndex < 0)
          throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
        if (value == null)
          value = string.Empty;
        if (startIndex > value.Length - length)
          throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_IndexLength"));
        this.m_MaxCapacity = int.MaxValue;
        if (capacity == 0)
          capacity = 16;
        if (capacity < length)
          capacity = length;
        this.m_ChunkChars = new char[capacity];
        this.m_ChunkLength = length;
        fixed (char* chPtr = value)
          StringBuilder.ThreadSafeCopy((char*) ((IntPtr) chPtr + (IntPtr) startIndex * 2), this.m_ChunkChars, 0, length);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Text.StringBuilder"/> class that starts with a specified capacity and can grow to a specified maximum.
    /// </summary>
    /// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder"/>. </param><param name="maxCapacity">The maximum number of characters the current string can contain. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="maxCapacity"/> is less than one, <paramref name="capacity"/> is less than zero, or <paramref name="capacity"/> is greater than <paramref name="maxCapacity"/>. </exception>
    [__DynamicallyInvokable]
    public StringBuilder(int capacity, int maxCapacity)
    {
      if (capacity > maxCapacity)
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_Capacity"));
      if (maxCapacity < 1)
        throw new ArgumentOutOfRangeException("maxCapacity", Environment.GetResourceString("ArgumentOutOfRange_SmallMaxCapacity"));
      if (capacity < 0)
      {
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", new object[1]
        {
          (object) "capacity"
        }));
      }
      else
      {
        if (capacity == 0)
          capacity = Math.Min(16, maxCapacity);
        this.m_MaxCapacity = maxCapacity;
        this.m_ChunkChars = new char[capacity];
      }
    }

    [SecurityCritical]
    private StringBuilder(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      int length = 0;
      string str = (string) null;
      int num = int.MaxValue;
      bool flag = false;
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        switch (enumerator.Name)
        {
          case "m_MaxCapacity":
            num = info.GetInt32("m_MaxCapacity");
            continue;
          case "m_StringValue":
            str = info.GetString("m_StringValue");
            continue;
          case "Capacity":
            length = info.GetInt32("Capacity");
            flag = true;
            continue;
          default:
            continue;
        }
      }
      if (str == null)
        str = string.Empty;
      if (num < 1 || str.Length > num)
        throw new SerializationException(Environment.GetResourceString("Serialization_StringBuilderMaxCapacity"));
      if (!flag)
      {
        length = 16;
        if (length < str.Length)
          length = str.Length;
        if (length > num)
          length = num;
      }
      if (length < 0 || length < str.Length || length > num)
        throw new SerializationException(Environment.GetResourceString("Serialization_StringBuilderCapacity"));
      this.m_MaxCapacity = num;
      this.m_ChunkChars = new char[length];
      str.CopyTo(0, this.m_ChunkChars, 0, str.Length);
      this.m_ChunkLength = str.Length;
      this.m_ChunkPrevious = (StringBuilder) null;
    }

    private StringBuilder(StringBuilder from)
    {
      this.m_ChunkLength = from.m_ChunkLength;
      this.m_ChunkOffset = from.m_ChunkOffset;
      this.m_ChunkChars = from.m_ChunkChars;
      this.m_ChunkPrevious = from.m_ChunkPrevious;
      this.m_MaxCapacity = from.m_MaxCapacity;
    }

    private StringBuilder(int size, int maxCapacity, StringBuilder previousBlock)
    {
      this.m_ChunkChars = new char[size];
      this.m_MaxCapacity = maxCapacity;
      this.m_ChunkPrevious = previousBlock;
      if (previousBlock == null)
        return;
      this.m_ChunkOffset = previousBlock.m_ChunkOffset + previousBlock.m_ChunkLength;
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      info.AddValue("m_MaxCapacity", this.m_MaxCapacity);
      info.AddValue("Capacity", this.Capacity);
      info.AddValue("m_StringValue", (object) base.ToString());
      info.AddValue("m_currentThread", 0);
    }

    /// <summary>
    /// Ensures that the capacity of this instance of <see cref="T:System.Text.StringBuilder"/> is at least the specified value.
    /// </summary>
    /// 
    /// <returns>
    /// The new capacity of this instance.
    /// </returns>
    /// <param name="capacity">The minimum capacity to ensure. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity"/> is less than zero.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int EnsureCapacity(int capacity)
    {
      if (capacity < 0)
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_NegativeCapacity"));
      if (this.Capacity < capacity)
        this.Capacity = capacity;
      return this.Capacity;
    }

    /// <summary>
    /// Converts the value of this instance to a <see cref="T:System.String"/>.
    /// </summary>
    /// 
    /// <returns>
    /// A string whose value is the same as this instance.
    /// </returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe string ToString()
    {
      if (this.Length == 0)
        return string.Empty;
      string str = string.FastAllocateString(this.Length);
      StringBuilder stringBuilder = this;
      fixed (char* chPtr = str)
      {
        do
        {
          if (stringBuilder.m_ChunkLength > 0)
          {
            char[] chArray = stringBuilder.m_ChunkChars;
            int num = stringBuilder.m_ChunkOffset;
            int charCount = stringBuilder.m_ChunkLength;
            if ((long) (uint) (charCount + num) > (long) str.Length || (uint) charCount > (uint) chArray.Length)
              throw new ArgumentOutOfRangeException("chunkLength", Environment.GetResourceString("ArgumentOutOfRange_Index"));
            fixed (char* smem = chArray)
              string.wstrcpy(chPtr + num, smem, charCount);
          }
          stringBuilder = stringBuilder.m_ChunkPrevious;
        }
        while (stringBuilder != null);
      }
      return str;
    }

    /// <summary>
    /// Converts the value of a substring of this instance to a <see cref="T:System.String"/>.
    /// </summary>
    /// 
    /// <returns>
    /// A string whose value is the same as the specified substring of this instance.
    /// </returns>
    /// <param name="startIndex">The starting position of the substring in this instance. </param><param name="length">The length of the substring. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> or <paramref name="length"/> is less than zero.-or- The sum of <paramref name="startIndex"/> and <paramref name="length"/> is greater than the length of the current instance. </exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe string ToString(int startIndex, int length)
    {
      int length1 = this.Length;
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (startIndex > length1)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndexLargerThanLength"));
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (startIndex > length1 - length)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_IndexLength"));
      StringBuilder stringBuilder = this;
      int num1 = startIndex + length;
      string str = string.FastAllocateString(length);
      int num2 = length;
      fixed (char* chPtr = str)
      {
        while (num2 > 0)
        {
          int num3 = num1 - stringBuilder.m_ChunkOffset;
          if (num3 >= 0)
          {
            if (num3 > stringBuilder.m_ChunkLength)
              num3 = stringBuilder.m_ChunkLength;
            int num4 = num2;
            int charCount = num4;
            int index = num3 - num4;
            if (index < 0)
            {
              charCount += index;
              index = 0;
            }
            num2 -= charCount;
            if (charCount > 0)
            {
              char[] chArray = stringBuilder.m_ChunkChars;
              if ((long) (uint) (charCount + num2) > (long) length || (uint) (charCount + index) > (uint) chArray.Length)
                throw new ArgumentOutOfRangeException("chunkCount", Environment.GetResourceString("ArgumentOutOfRange_Index"));
              fixed (char* smem = &chArray[index])
                string.wstrcpy(chPtr + num2, smem, charCount);
            }
          }
          stringBuilder = stringBuilder.m_ChunkPrevious;
        }
      }
      return str;
    }

    /// <summary>
    /// Removes all characters from the current <see cref="T:System.Text.StringBuilder"/> instance.
    /// </summary>
    /// 
    /// <returns>
    /// An object whose <see cref="P:System.Text.StringBuilder.Length"/> is 0 (zero).
    /// </returns>
    [__DynamicallyInvokable]
    public StringBuilder Clear()
    {
      this.Length = 0;
      return this;
    }

    /// <summary>
    /// Appends a specified number of copies of the string representation of a Unicode character to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The character to append. </param><param name="repeatCount">The number of times to append <paramref name="value"/>. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="repeatCount"/> is less than zero.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><exception cref="T:System.OutOfMemoryException">Out of memory.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(char value, int repeatCount)
    {
      if (repeatCount < 0)
        throw new ArgumentOutOfRangeException("repeatCount", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      if (repeatCount == 0)
        return this;
      int num = this.m_ChunkLength;
      while (repeatCount > 0)
      {
        if (num < this.m_ChunkChars.Length)
        {
          this.m_ChunkChars[num++] = value;
          --repeatCount;
        }
        else
        {
          this.m_ChunkLength = num;
          this.ExpandByABlock(repeatCount);
          num = 0;
        }
      }
      this.m_ChunkLength = num;
      return this;
    }

    /// <summary>
    /// Appends the string representation of a specified subarray of Unicode characters to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">A character array. </param><param name="startIndex">The starting position in <paramref name="value"/>. </param><param name="charCount">The number of characters to append. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null, and <paramref name="startIndex"/> and <paramref name="charCount"/> are not zero. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="charCount"/> is less than zero.-or- <paramref name="startIndex"/> is less than zero.-or- <paramref name="startIndex"/> + <paramref name="charCount"/> is greater than the length of <paramref name="value"/>.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Append(char[] value, int startIndex, int charCount)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (charCount < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (value == null)
      {
        if (startIndex == 0 && charCount == 0)
          return this;
        else
          throw new ArgumentNullException("value");
      }
      else
      {
        if (charCount > value.Length - startIndex)
          throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (charCount == 0)
          return this;
        fixed (char* chPtr = &value[startIndex])
          this.Append(chPtr, charCount);
        return this;
      }
    }

    /// <summary>
    /// Appends a copy of the specified string to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The string to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Append(string value)
    {
      if (value != null)
      {
        char[] chArray = this.m_ChunkChars;
        int index = this.m_ChunkLength;
        int length = value.Length;
        int num = index + length;
        if (num < chArray.Length)
        {
          if (length <= 2)
          {
            if (length > 0)
              chArray[index] = value[0];
            if (length > 1)
              chArray[index + 1] = value[1];
          }
          else
          {
            fixed (char* smem = value)
              fixed (char* dmem = &chArray[index])
                string.wstrcpy(dmem, smem, length);
          }
          this.m_ChunkLength = num;
        }
        else
          this.AppendHelper(value);
      }
      return this;
    }

    [SecurityCritical]
    [ForceTokenStabilization]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal void ReplaceBufferInternal(char* newBuffer, int newLength);

    [SecurityCritical]
    [ForceTokenStabilization]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal void ReplaceBufferAnsiInternal(sbyte* newBuffer, int newLength);

    /// <summary>
    /// Appends a copy of a specified substring to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The string that contains the substring to append. </param><param name="startIndex">The starting position of the substring within <paramref name="value"/>. </param><param name="count">The number of characters in <paramref name="value"/> to append. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null, and <paramref name="startIndex"/> and <paramref name="count"/> are not zero. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="count"/> less than zero.-or- <paramref name="startIndex"/> less than zero.-or- <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of <paramref name="value"/>.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Append(string value, int startIndex, int count)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (value == null)
      {
        if (startIndex == 0 && count == 0)
          return this;
        else
          throw new ArgumentNullException("value");
      }
      else
      {
        if (count == 0)
          return this;
        if (startIndex > value.Length - count)
          throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        fixed (char* chPtr = value)
          this.Append((char*) ((IntPtr) chPtr + (IntPtr) startIndex * 2), count);
        return this;
      }
    }

    /// <summary>
    /// Appends the default line terminator to the end of the current <see cref="T:System.Text.StringBuilder"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public StringBuilder AppendLine()
    {
      return this.Append(Environment.NewLine);
    }

    /// <summary>
    /// Appends a copy of the specified string followed by the default line terminator to the end of the current <see cref="T:System.Text.StringBuilder"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The string to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public StringBuilder AppendLine(string value)
    {
      this.Append(value);
      return this.Append(Environment.NewLine);
    }

    /// <summary>
    /// Copies the characters from a specified segment of this instance to a specified segment of a destination <see cref="T:System.Char"/> array.
    /// </summary>
    /// <param name="sourceIndex">The starting position in this instance where characters will be copied from. The index is zero-based.</param><param name="destination">The array where characters will be copied.</param><param name="destinationIndex">The starting position in <paramref name="destination"/> where characters will be copied. The index is zero-based.</param><param name="count">The number of characters to be copied.</param><exception cref="T:System.ArgumentNullException"><paramref name="destination"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="sourceIndex"/>, <paramref name="destinationIndex"/>, or <paramref name="count"/>, is less than zero.-or-<paramref name="sourceIndex"/> is greater than the length of this instance.</exception><exception cref="T:System.ArgumentException"><paramref name="sourceIndex"/> + <paramref name="count"/> is greater than the length of this instance.-or-<paramref name="destinationIndex"/> + <paramref name="count"/> is greater than the length of <paramref name="destination"/>.</exception><filterpriority>1</filterpriority>
    [ComVisible(false)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
    {
      if (destination == null)
        throw new ArgumentNullException("destination");
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Arg_NegativeArgCount"));
      if (destinationIndex < 0)
      {
        throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", new object[1]
        {
          (object) "destinationIndex"
        }));
      }
      else
      {
        if (destinationIndex > destination.Length - count)
          throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_OffsetOut"));
        if ((uint) sourceIndex > (uint) this.Length)
          throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (sourceIndex > this.Length - count)
          throw new ArgumentException(Environment.GetResourceString("Arg_LongerThanSrcString"));
        StringBuilder stringBuilder = this;
        int num1 = sourceIndex + count;
        int destinationIndex1 = destinationIndex + count;
        while (count > 0)
        {
          int num2 = num1 - stringBuilder.m_ChunkOffset;
          if (num2 >= 0)
          {
            if (num2 > stringBuilder.m_ChunkLength)
              num2 = stringBuilder.m_ChunkLength;
            int count1 = count;
            int sourceIndex1 = num2 - count;
            if (sourceIndex1 < 0)
            {
              count1 += sourceIndex1;
              sourceIndex1 = 0;
            }
            destinationIndex1 -= count1;
            count -= count1;
            StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, sourceIndex1, destination, destinationIndex1, count1);
          }
          stringBuilder = stringBuilder.m_ChunkPrevious;
        }
      }
    }

    /// <summary>
    /// Inserts one or more copies of a specified string into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after insertion has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The string to insert. </param><param name="count">The number of times to insert <paramref name="value"/>. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the current length of this instance.-or- <paramref name="count"/> is less than zero. </exception><exception cref="T:System.OutOfMemoryException">The current length of this <see cref="T:System.Text.StringBuilder"/> object plus the length of <paramref name="value"/> times <paramref name="count"/> exceeds <see cref="P:System.Text.StringBuilder.MaxCapacity"/>.</exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Insert(int index, string value, int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      int length = this.Length;
      if ((uint) index > (uint) length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (value == null || value.Length == 0 || count == 0)
        return this;
      long num = (long) value.Length * (long) count;
      if (num > (long) (this.MaxCapacity - this.Length))
        throw new OutOfMemoryException();
      StringBuilder chunk;
      int indexInChunk;
      this.MakeRoom(index, (int) num, out chunk, out indexInChunk, false);
      fixed (char* chPtr = value)
      {
        for (; count > 0; --count)
          this.ReplaceInPlaceAtChunk(ref chunk, ref indexInChunk, chPtr, value.Length);
      }
      return this;
    }

    /// <summary>
    /// Removes the specified range of characters from this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the excise operation has completed.
    /// </returns>
    /// <param name="startIndex">The zero-based position in this instance where removal begins. </param><param name="length">The number of characters to remove. </param><exception cref="T:System.ArgumentOutOfRangeException">If <paramref name="startIndex"/> or <paramref name="length"/> is less than zero, or <paramref name="startIndex"/> + <paramref name="length"/> is greater than the length of this instance. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Remove(int startIndex, int length)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (length > this.Length - startIndex)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (this.Length == length && startIndex == 0)
      {
        this.Length = 0;
        return this;
      }
      else
      {
        if (length > 0)
        {
          StringBuilder chunk;
          int indexInChunk;
          this.Remove(startIndex, length, out chunk, out indexInChunk);
        }
        return this;
      }
    }

    /// <summary>
    /// Appends the string representation of a specified Boolean value to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The Boolean value to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(bool value)
    {
      return this.Append(value.ToString());
    }

    /// <summary>
    /// Appends the string representation of a specified 8-bit signed integer to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The value to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Append(sbyte value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    /// Appends the string representation of a specified 8-bit unsigned integer to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The value to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(byte value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    /// Appends the string representation of a specified Unicode character to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The Unicode character to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(char value)
    {
      if (this.m_ChunkLength < this.m_ChunkChars.Length)
        this.m_ChunkChars[this.m_ChunkLength++] = value;
      else
        this.Append(value, 1);
      return this;
    }

    /// <summary>
    /// Appends the string representation of a specified 16-bit signed integer to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The value to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(short value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    /// Appends the string representation of a specified 32-bit signed integer to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The value to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public StringBuilder Append(int value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    /// Appends the string representation of a specified 64-bit signed integer to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The value to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(long value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    /// Appends the string representation of a specified single-precision floating-point number to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The value to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(float value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    /// Appends the string representation of a specified double-precision floating-point number to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The value to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(double value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    /// Appends the string representation of a specified decimal number to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The value to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(Decimal value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    /// Appends the string representation of a specified 16-bit unsigned integer to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The value to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Append(ushort value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    /// Appends the string representation of a specified 32-bit unsigned integer to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The value to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Append(uint value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    /// Appends the string representation of a specified 64-bit unsigned integer to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The value to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Append(ulong value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    /// Appends the string representation of a specified object to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The object to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public StringBuilder Append(object value)
    {
      if (value == null)
        return this;
      else
        return this.Append(value.ToString());
    }

    /// <summary>
    /// Appends the string representation of the Unicode characters in a specified array to this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed.
    /// </returns>
    /// <param name="value">The array of characters to append. </param><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Append(char[] value)
    {
      if (value != null && value.Length > 0)
      {
        fixed (char* chPtr = &value[0])
          this.Append(chPtr, value.Length);
      }
      return this;
    }

    /// <summary>
    /// Inserts a string into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The string to insert. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the current length of this instance. -or-The current length of this <see cref="T:System.Text.StringBuilder"/> object plus the length of <paramref name="value"/> exceeds <see cref="P:System.Text.StringBuilder.MaxCapacity"/>.</exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Insert(int index, string value)
    {
      if ((uint) index > (uint) this.Length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (value != null)
      {
        fixed (char* chPtr = value)
          this.Insert(index, chPtr, value.Length);
      }
      return this;
    }

    /// <summary>
    /// Inserts the string representation of a Boolean value into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The value to insert. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the length of this instance.</exception><exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, bool value)
    {
      return this.Insert(index, value.ToString(), 1);
    }

    /// <summary>
    /// Inserts the string representation of a specified 8-bit signed integer into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The value to insert. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the length of this instance. </exception><exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>.</exception><filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, sbyte value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    /// Inserts the string representation of a specified 8-bit unsigned integer into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The value to insert. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the length of this instance. </exception><exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, byte value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    /// Inserts the string representation of a specified 16-bit signed integer into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The value to insert. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the length of this instance. </exception><exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, short value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    /// Inserts the string representation of a specified Unicode character into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The value to insert. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Insert(int index, char value)
    {
      this.Insert(index, &value, 1);
      return this;
    }

    /// <summary>
    /// Inserts the string representation of a specified array of Unicode characters into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The character array to insert. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, char[] value)
    {
      if ((uint) index > (uint) this.Length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (value != null)
        this.Insert(index, value, 0, value.Length);
      return this;
    }

    /// <summary>
    /// Inserts the string representation of a specified subarray of Unicode characters into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">A character array. </param><param name="startIndex">The starting index within <paramref name="value"/>. </param><param name="charCount">The number of characters to insert. </param><exception cref="T:System.ArgumentNullException"><paramref name="value"/> is null, and <paramref name="startIndex"/> and <paramref name="charCount"/> are not zero. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/>, <paramref name="startIndex"/>, or <paramref name="charCount"/> is less than zero.-or- <paramref name="index"/> is greater than the length of this instance.-or- <paramref name="startIndex"/> plus <paramref name="charCount"/> is not a position within <paramref name="value"/>.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Insert(int index, char[] value, int startIndex, int charCount)
    {
      int length = this.Length;
      if ((uint) index > (uint) length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (value == null)
      {
        if (startIndex == 0 && charCount == 0)
          return this;
        else
          throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_String"));
      }
      else
      {
        if (startIndex < 0)
          throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
        if (charCount < 0)
          throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
        if (startIndex > value.Length - charCount)
          throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (charCount > 0)
        {
          fixed (char* chPtr = &value[startIndex])
            this.Insert(index, chPtr, charCount);
        }
        return this;
      }
    }

    /// <summary>
    /// Inserts the string representation of a specified 32-bit signed integer into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The value to insert. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the length of this instance. </exception><exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, int value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    /// Inserts the string representation of a 64-bit signed integer into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The value to insert. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the length of this instance. </exception><exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, long value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    /// Inserts the string representation of a single-precision floating point number into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The value to insert. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the length of this instance. </exception><exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, float value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    /// Inserts the string representation of a double-precision floating-point number into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The value to insert. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the length of this instance. </exception><exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, double value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    /// Inserts the string representation of a decimal number into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The value to insert. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the length of this instance. </exception><exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, Decimal value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    /// Inserts the string representation of a 16-bit unsigned integer into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The value to insert. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the length of this instance. </exception><exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>.</exception><filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, ushort value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    /// Inserts the string representation of a 32-bit unsigned integer into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The value to insert. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the length of this instance. </exception><exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>.</exception><filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, uint value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    /// Inserts the string representation of a 64-bit unsigned integer into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The value to insert. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the length of this instance. </exception><exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>.</exception><filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, ulong value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    /// Inserts the string representation of an object into this instance at the specified character position.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the insert operation has completed.
    /// </returns>
    /// <param name="index">The position in this instance where insertion begins. </param><param name="value">The object to insert, or null. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the length of this instance. </exception><exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>.</exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, object value)
    {
      if (value == null)
        return this;
      else
        return this.Insert(index, value.ToString(), 1);
    }

    /// <summary>
    /// Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a single argument.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance with <paramref name="format"/> appended. Each format item in <paramref name="format"/> is replaced by the string representation of <paramref name="arg0"/>.
    /// </returns>
    /// <param name="format">A composite format string (see Remarks). </param><param name="arg0">An object to format. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="format"/> is invalid. -or-The index of a format item is less than 0 (zero), or greater than or equal to 1.</exception><exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>2</filterpriority>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public StringBuilder AppendFormat(string format, object arg0)
    {
      return this.AppendFormat((IFormatProvider) null, format, new object[1]
      {
        arg0
      });
    }

    /// <summary>
    /// Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of two arguments.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance with <paramref name="format"/> appended. Each format item in <paramref name="format"/> is replaced by the string representation of the corresponding object argument.
    /// </returns>
    /// <param name="format">A composite format string (see Remarks). </param><param name="arg0">The first object to format. </param><param name="arg1">The second object to format. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="format"/> is invalid.-or-The index of a format item is less than 0 (zero), or greater than or equal to 2. </exception><exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>2</filterpriority>
    public StringBuilder AppendFormat(string format, object arg0, object arg1)
    {
      return this.AppendFormat((IFormatProvider) null, format, new object[2]
      {
        arg0,
        arg1
      });
    }

    /// <summary>
    /// Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of three arguments.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance with <paramref name="format"/> appended. Each format item in <paramref name="format"/> is replaced by the string representation of the corresponding object argument.
    /// </returns>
    /// <param name="format">A composite format string (see Remarks). </param><param name="arg0">The first object to format. </param><param name="arg1">The second object to format. </param><param name="arg2">The third object to format. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="format"/> is invalid.-or-The index of a format item is less than 0 (zero), or greater than or equal to 3.</exception><exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>2</filterpriority>
    public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
    {
      return this.AppendFormat((IFormatProvider) null, format, arg0, arg1, arg2);
    }

    /// <summary>
    /// Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a corresponding argument in a parameter array.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance with <paramref name="format"/> appended. Each format item in <paramref name="format"/> is replaced by the string representation of the corresponding object argument.
    /// </returns>
    /// <param name="format">A composite format string (see Remarks). </param><param name="args">An array of objects to format. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> or <paramref name="args"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="format"/> is invalid. -or-The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="args"/> array.</exception><exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public StringBuilder AppendFormat(string format, params object[] args)
    {
      return this.AppendFormat((IFormatProvider) null, format, args);
    }

    /// <summary>
    /// Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a corresponding argument in a parameter array using a specified format provider.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance after the append operation has completed. After the append operation, this instance contains any data that existed before the operation, suffixed by a copy of <paramref name="format"/> where any format specification is replaced by the string representation of the corresponding object argument.
    /// </returns>
    /// <param name="provider">An object that supplies culture-specific formatting information. </param><param name="format">A composite format string (see Remarks). </param><param name="args">An array of objects to format.</param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="format"/> is invalid. -or-The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="args"/> array.</exception><exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
    {
      if (format == null || args == null)
        throw new ArgumentNullException(format == null ? "format" : "args");
      int index1 = 0;
      int length = format.Length;
      char ch = char.MinValue;
      ICustomFormatter customFormatter = (ICustomFormatter) null;
      if (provider != null)
        customFormatter = (ICustomFormatter) provider.GetFormat(typeof (ICustomFormatter));
      while (true)
      {
        bool flag;
        int repeatCount;
        do
        {
          if (index1 < length)
          {
            ch = format[index1];
            ++index1;
            if ((int) ch == 125)
            {
              if (index1 < length && (int) format[index1] == 125)
                ++index1;
              else
                StringBuilder.FormatError();
            }
            if ((int) ch == 123)
            {
              if (index1 >= length || (int) format[index1] != 123)
                --index1;
              else
                goto label_10;
            }
            else
              goto label_12;
          }
          if (index1 != length)
          {
            int index2 = index1 + 1;
            if (index2 == length || (int) (ch = format[index2]) < 48 || (int) ch > 57)
              StringBuilder.FormatError();
            int index3 = 0;
            do
            {
              index3 = index3 * 10 + (int) ch - 48;
              ++index2;
              if (index2 == length)
                StringBuilder.FormatError();
              ch = format[index2];
            }
            while ((int) ch >= 48 && (int) ch <= 57 && index3 < 1000000);
            if (index3 >= args.Length)
              throw new FormatException(Environment.GetResourceString("Format_IndexOutOfRange"));
            while (index2 < length && (int) (ch = format[index2]) == 32)
              ++index2;
            flag = false;
            int num = 0;
            if ((int) ch == 44)
            {
              ++index2;
              while (index2 < length && (int) format[index2] == 32)
                ++index2;
              if (index2 == length)
                StringBuilder.FormatError();
              ch = format[index2];
              if ((int) ch == 45)
              {
                flag = true;
                ++index2;
                if (index2 == length)
                  StringBuilder.FormatError();
                ch = format[index2];
              }
              if ((int) ch < 48 || (int) ch > 57)
                StringBuilder.FormatError();
              do
              {
                num = num * 10 + (int) ch - 48;
                ++index2;
                if (index2 == length)
                  StringBuilder.FormatError();
                ch = format[index2];
              }
              while ((int) ch >= 48 && (int) ch <= 57 && num < 1000000);
            }
            while (index2 < length && (int) (ch = format[index2]) == 32)
              ++index2;
            object obj = args[index3];
            StringBuilder stringBuilder = (StringBuilder) null;
            if ((int) ch == 58)
            {
              int index4 = index2 + 1;
              while (true)
              {
                if (index4 == length)
                  StringBuilder.FormatError();
                ch = format[index4];
                ++index4;
                if ((int) ch == 123)
                {
                  if (index4 < length && (int) format[index4] == 123)
                    ++index4;
                  else
                    StringBuilder.FormatError();
                }
                else if ((int) ch == 125)
                {
                  if (index4 < length && (int) format[index4] == 125)
                    ++index4;
                  else
                    break;
                }
                if (stringBuilder == null)
                  stringBuilder = new StringBuilder();
                stringBuilder.Append(ch);
              }
              index2 = index4 - 1;
            }
            if ((int) ch != 125)
              StringBuilder.FormatError();
            index1 = index2 + 1;
            string format1 = (string) null;
            string str = (string) null;
            if (customFormatter != null)
            {
              if (stringBuilder != null)
                format1 = ((object) stringBuilder).ToString();
              str = customFormatter.Format(format1, obj, provider);
            }
            if (str == null)
            {
              IFormattable formattable = obj as IFormattable;
              if (formattable != null)
              {
                if (format1 == null && stringBuilder != null)
                  format1 = ((object) stringBuilder).ToString();
                str = formattable.ToString(format1, provider);
              }
              else if (obj != null)
                str = obj.ToString();
            }
            if (str == null)
              str = string.Empty;
            repeatCount = num - str.Length;
            if (!flag && repeatCount > 0)
              this.Append(' ', repeatCount);
            this.Append(str);
          }
          else
            goto label_76;
        }
        while (!flag || repeatCount <= 0);
        goto label_75;
label_10:
        ++index1;
label_12:
        this.Append(ch);
        continue;
label_75:
        this.Append(' ', repeatCount);
      }
label_76:
      return this;
    }

    /// <summary>
    /// Replaces all occurrences of a specified string in this instance with another specified string.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance with all instances of <paramref name="oldValue"/> replaced by <paramref name="newValue"/>.
    /// </returns>
    /// <param name="oldValue">The string to replace. </param><param name="newValue">The string that replaces <paramref name="oldValue"/>, or null. </param><exception cref="T:System.ArgumentNullException"><paramref name="oldValue"/> is null. </exception><exception cref="T:System.ArgumentException">The length of <paramref name="oldValue"/> is zero. </exception><exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Replace(string oldValue, string newValue)
    {
      return this.Replace(oldValue, newValue, 0, this.Length);
    }

    /// <summary>
    /// Returns a value indicating whether this instance is equal to a specified object.
    /// </summary>
    /// 
    /// <returns>
    /// true if this instance and <paramref name="sb"/> have equal string, <see cref="P:System.Text.StringBuilder.Capacity"/>, and <see cref="P:System.Text.StringBuilder.MaxCapacity"/> values; otherwise, false.
    /// </returns>
    /// <param name="sb">An object to compare with this instance, or null. </param><filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool Equals(StringBuilder sb)
    {
      if (sb == null || this.Capacity != sb.Capacity || (this.MaxCapacity != sb.MaxCapacity || this.Length != sb.Length))
        return false;
      if (sb == this)
        return true;
      StringBuilder stringBuilder1 = this;
      int index1 = stringBuilder1.m_ChunkLength;
      StringBuilder stringBuilder2 = sb;
      int index2 = stringBuilder2.m_ChunkLength;
      do
      {
        --index1;
        --index2;
        for (; index1 < 0; index1 = stringBuilder1.m_ChunkLength + index1)
        {
          stringBuilder1 = stringBuilder1.m_ChunkPrevious;
          if (stringBuilder1 == null)
            break;
        }
        for (; index2 < 0; index2 = stringBuilder2.m_ChunkLength + index2)
        {
          stringBuilder2 = stringBuilder2.m_ChunkPrevious;
          if (stringBuilder2 == null)
            break;
        }
        if (index1 < 0)
          return index2 < 0;
      }
      while (index2 >= 0 && (int) stringBuilder1.m_ChunkChars[index1] == (int) stringBuilder2.m_ChunkChars[index2]);
      return false;
    }

    /// <summary>
    /// Replaces, within a substring of this instance, all occurrences of a specified string with another specified string.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance with all instances of <paramref name="oldValue"/> replaced by <paramref name="newValue"/> in the range from <paramref name="startIndex"/> to <paramref name="startIndex"/> + <paramref name="count"/> - 1.
    /// </returns>
    /// <param name="oldValue">The string to replace. </param><param name="newValue">The string that replaces <paramref name="oldValue"/>, or null. </param><param name="startIndex">The position in this instance where the substring begins. </param><param name="count">The length of the substring. </param><exception cref="T:System.ArgumentNullException"><paramref name="oldValue"/> is null. </exception><exception cref="T:System.ArgumentException">The length of <paramref name="oldValue"/> is zero. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> or <paramref name="count"/> is less than zero.-or- <paramref name="startIndex"/> plus <paramref name="count"/> indicates a character position not within this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity"/>. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count)
    {
      int length1 = this.Length;
      if ((uint) startIndex > (uint) length1)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > length1 - count)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (oldValue == null)
        throw new ArgumentNullException("oldValue");
      if (oldValue.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "oldValue");
      if (newValue == null)
        newValue = "";
      int length2 = newValue.Length;
      int length3 = oldValue.Length;
      int[] replacements = (int[]) null;
      int replacementsCount = 0;
      StringBuilder chunkForIndex = this.FindChunkForIndex(startIndex);
      int indexInChunk = startIndex - chunkForIndex.m_ChunkOffset;
      while (count > 0)
      {
        if (this.StartsWith(chunkForIndex, indexInChunk, count, oldValue))
        {
          if (replacements == null)
            replacements = new int[5];
          else if (replacementsCount >= replacements.Length)
          {
            int[] numArray = new int[replacements.Length * 3 / 2 + 4];
            Array.Copy((Array) replacements, (Array) numArray, replacements.Length);
            replacements = numArray;
          }
          replacements[replacementsCount++] = indexInChunk;
          indexInChunk += oldValue.Length;
          count -= oldValue.Length;
        }
        else
        {
          ++indexInChunk;
          --count;
        }
        if (indexInChunk >= chunkForIndex.m_ChunkLength || count == 0)
        {
          int num = indexInChunk + chunkForIndex.m_ChunkOffset;
          this.ReplaceAllInChunk(replacements, replacementsCount, chunkForIndex, oldValue.Length, newValue);
          int index = num + (newValue.Length - oldValue.Length) * replacementsCount;
          replacementsCount = 0;
          chunkForIndex = this.FindChunkForIndex(index);
          indexInChunk = index - chunkForIndex.m_ChunkOffset;
        }
      }
      return this;
    }

    /// <summary>
    /// Replaces all occurrences of a specified character in this instance with another specified character.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance with <paramref name="oldChar"/> replaced by <paramref name="newChar"/>.
    /// </returns>
    /// <param name="oldChar">The character to replace. </param><param name="newChar">The character that replaces <paramref name="oldChar"/>. </param><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Replace(char oldChar, char newChar)
    {
      return this.Replace(oldChar, newChar, 0, this.Length);
    }

    /// <summary>
    /// Replaces, within a substring of this instance, all occurrences of a specified character with another specified character.
    /// </summary>
    /// 
    /// <returns>
    /// A reference to this instance with <paramref name="oldChar"/> replaced by <paramref name="newChar"/> in the range from <paramref name="startIndex"/> to <paramref name="startIndex"/> + <paramref name="count"/> -1.
    /// </returns>
    /// <param name="oldChar">The character to replace. </param><param name="newChar">The character that replaces <paramref name="oldChar"/>. </param><param name="startIndex">The position in this instance where the substring begins. </param><param name="count">The length of the substring. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the value of this instance.-or- <paramref name="startIndex"/> or <paramref name="count"/> is less than zero. </exception><filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
    {
      int length = this.Length;
      if ((uint) startIndex > (uint) length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > length - count)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      int num = startIndex + count;
      StringBuilder stringBuilder = this;
      while (true)
      {
        int val2 = num - stringBuilder.m_ChunkOffset;
        int val1 = startIndex - stringBuilder.m_ChunkOffset;
        if (val2 >= 0)
        {
          int index1 = Math.Max(val1, 0);
          for (int index2 = Math.Min(stringBuilder.m_ChunkLength, val2); index1 < index2; ++index1)
          {
            if ((int) stringBuilder.m_ChunkChars[index1] == (int) oldChar)
              stringBuilder.m_ChunkChars[index1] = newChar;
          }
        }
        if (val1 < 0)
          stringBuilder = stringBuilder.m_ChunkPrevious;
        else
          break;
      }
      return this;
    }

    [SecurityCritical]
    internal unsafe StringBuilder Append(char* value, int valueCount)
    {
      int num1 = valueCount + this.m_ChunkLength;
      if (num1 <= this.m_ChunkChars.Length)
      {
        StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, valueCount);
        this.m_ChunkLength = num1;
      }
      else
      {
        int count = this.m_ChunkChars.Length - this.m_ChunkLength;
        if (count > 0)
        {
          StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, count);
          this.m_ChunkLength = this.m_ChunkChars.Length;
        }
        int num2 = valueCount - count;
        this.ExpandByABlock(num2);
        StringBuilder.ThreadSafeCopy(value + count, this.m_ChunkChars, 0, num2);
        this.m_ChunkLength = num2;
      }
      return this;
    }

    [ForceTokenStabilization]
    [SecurityCritical]
    internal unsafe void InternalCopy(IntPtr dest, int len)
    {
      if (len == 0)
        return;
      bool flag = true;
      byte* numPtr = (byte*) dest.ToPointer();
      StringBuilder stringBuilder = this.FindChunkForByte(len);
      do
      {
        int num = stringBuilder.m_ChunkOffset * 2;
        int len1 = stringBuilder.m_ChunkLength * 2;
        fixed (char* chPtr = &stringBuilder.m_ChunkChars[0])
        {
          if (flag)
          {
            flag = false;
            Buffer.Memcpy(numPtr + num, (byte*) chPtr, len - num);
          }
          else
            Buffer.Memcpy(numPtr + num, (byte*) chPtr, len1);
        }
        stringBuilder = stringBuilder.m_ChunkPrevious;
      }
      while (stringBuilder != null);
    }

    [Conditional("_DEBUG")]
    private void VerifyClassInvariant()
    {
      StringBuilder stringBuilder1 = this;
      while (true)
      {
        StringBuilder stringBuilder2 = stringBuilder1.m_ChunkPrevious;
        if (stringBuilder2 != null)
          stringBuilder1 = stringBuilder2;
        else
          break;
      }
    }

    [SecuritySafeCritical]
    private unsafe void AppendHelper(string value)
    {
      fixed (char* chPtr = value)
        this.Append(chPtr, value.Length);
    }

    private static void FormatError()
    {
      throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
    }

    [SecurityCritical]
    private unsafe void Insert(int index, char* value, int valueCount)
    {
      if ((uint) index > (uint) this.Length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (valueCount <= 0)
        return;
      StringBuilder chunk;
      int indexInChunk;
      this.MakeRoom(index, valueCount, out chunk, out indexInChunk, false);
      this.ReplaceInPlaceAtChunk(ref chunk, ref indexInChunk, value, valueCount);
    }

    [SecuritySafeCritical]
    private unsafe void ReplaceAllInChunk(int[] replacements, int replacementsCount, StringBuilder sourceChunk, int removeCount, string value)
    {
      if (replacementsCount <= 0)
        return;
      fixed (char* chPtr1 = value)
      {
        int count = (value.Length - removeCount) * replacementsCount;
        StringBuilder chunk = sourceChunk;
        int indexInChunk = replacements[0];
        if (count > 0)
          this.MakeRoom(chunk.m_ChunkOffset + indexInChunk, count, out chunk, out indexInChunk, true);
        int index1 = 0;
        while (true)
        {
          this.ReplaceInPlaceAtChunk(ref chunk, ref indexInChunk, chPtr1, value.Length);
          int index2 = replacements[index1] + removeCount;
          ++index1;
          if (index1 < replacementsCount)
          {
            int num = replacements[index1];
            if (count != 0)
            {
              fixed (char* chPtr2 = &sourceChunk.m_ChunkChars[index2])
                this.ReplaceInPlaceAtChunk(ref chunk, ref indexInChunk, chPtr2, num - index2);
            }
            else
              indexInChunk += num - index2;
          }
          else
            break;
        }
        if (count < 0)
          this.Remove(chunk.m_ChunkOffset + indexInChunk, -count, out chunk, out indexInChunk);
      }
    }

    private bool StartsWith(StringBuilder chunk, int indexInChunk, int count, string value)
    {
      for (int index = 0; index < value.Length; ++index)
      {
        if (count == 0)
          return false;
        if (indexInChunk >= chunk.m_ChunkLength)
        {
          chunk = this.Next(chunk);
          if (chunk == null)
            return false;
          indexInChunk = 0;
        }
        if ((int) value[index] != (int) chunk.m_ChunkChars[indexInChunk])
          return false;
        ++indexInChunk;
        --count;
      }
      return true;
    }

    [SecurityCritical]
    private unsafe void ReplaceInPlaceAtChunk(ref StringBuilder chunk, ref int indexInChunk, char* value, int count)
    {
      if (count == 0)
        return;
      while (true)
      {
        int count1 = Math.Min(chunk.m_ChunkLength - indexInChunk, count);
        StringBuilder.ThreadSafeCopy(value, chunk.m_ChunkChars, indexInChunk, count1);
        indexInChunk += count1;
        if (indexInChunk >= chunk.m_ChunkLength)
        {
          chunk = this.Next(chunk);
          indexInChunk = 0;
        }
        count -= count1;
        if (count != 0)
          value += count1;
        else
          break;
      }
    }

    [SecurityCritical]
    private static unsafe void ThreadSafeCopy(char* sourcePtr, char[] destination, int destinationIndex, int count)
    {
      if (count <= 0)
        return;
      if ((uint) destinationIndex > (uint) destination.Length || destinationIndex + count > destination.Length)
        throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      fixed (char* dmem = &destination[destinationIndex])
        string.wstrcpy(dmem, sourcePtr, count);
    }

    [SecurityCritical]
    private static unsafe void ThreadSafeCopy(char[] source, int sourceIndex, char[] destination, int destinationIndex, int count)
    {
      if (count <= 0)
        return;
      if ((uint) sourceIndex > (uint) source.Length || sourceIndex + count > source.Length)
        throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      fixed (char* sourcePtr = &source[sourceIndex])
        StringBuilder.ThreadSafeCopy(sourcePtr, destination, destinationIndex, count);
    }

    private StringBuilder FindChunkForIndex(int index)
    {
      StringBuilder stringBuilder = this;
      while (stringBuilder.m_ChunkOffset > index)
        stringBuilder = stringBuilder.m_ChunkPrevious;
      return stringBuilder;
    }

    private StringBuilder FindChunkForByte(int byteIndex)
    {
      StringBuilder stringBuilder = this;
      while (stringBuilder.m_ChunkOffset * 2 > byteIndex)
        stringBuilder = stringBuilder.m_ChunkPrevious;
      return stringBuilder;
    }

    private StringBuilder Next(StringBuilder chunk)
    {
      if (chunk == this)
        return (StringBuilder) null;
      else
        return this.FindChunkForIndex(chunk.m_ChunkOffset + chunk.m_ChunkLength);
    }

    private void ExpandByABlock(int minBlockCharCount)
    {
      if (minBlockCharCount + this.Length > this.m_MaxCapacity)
        throw new ArgumentOutOfRangeException("requiredLength", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
      int length = Math.Max(minBlockCharCount, Math.Min(this.Length, 8000));
      this.m_ChunkPrevious = new StringBuilder(this);
      this.m_ChunkOffset += this.m_ChunkLength;
      this.m_ChunkLength = 0;
      if (this.m_ChunkOffset + length < length)
      {
        this.m_ChunkChars = (char[]) null;
        throw new OutOfMemoryException();
      }
      else
        this.m_ChunkChars = new char[length];
    }

    [SecuritySafeCritical]
    private unsafe void MakeRoom(int index, int count, out StringBuilder chunk, out int indexInChunk, bool doneMoveFollowingChars)
    {
      if (count + this.Length > this.m_MaxCapacity)
        throw new ArgumentOutOfRangeException("requiredLength", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
      chunk = this;
      while (chunk.m_ChunkOffset > index)
      {
        chunk.m_ChunkOffset += count;
        chunk = chunk.m_ChunkPrevious;
      }
      indexInChunk = index - chunk.m_ChunkOffset;
      if (!doneMoveFollowingChars && chunk.m_ChunkLength <= 32 && chunk.m_ChunkChars.Length - chunk.m_ChunkLength >= count)
      {
        int index1 = chunk.m_ChunkLength;
        while (index1 > indexInChunk)
        {
          --index1;
          chunk.m_ChunkChars[index1 + count] = chunk.m_ChunkChars[index1];
        }
        chunk.m_ChunkLength += count;
      }
      else
      {
        StringBuilder stringBuilder = new StringBuilder(Math.Max(count, 16), chunk.m_MaxCapacity, chunk.m_ChunkPrevious);
        stringBuilder.m_ChunkLength = count;
        int count1 = Math.Min(count, indexInChunk);
        if (count1 > 0)
        {
          fixed (char* sourcePtr = chunk.m_ChunkChars)
          {
            StringBuilder.ThreadSafeCopy(sourcePtr, stringBuilder.m_ChunkChars, 0, count1);
            int count2 = indexInChunk - count1;
            if (count2 >= 0)
            {
              StringBuilder.ThreadSafeCopy(sourcePtr + count1, chunk.m_ChunkChars, 0, count2);
              indexInChunk = count2;
            }
          }
        }
        chunk.m_ChunkPrevious = stringBuilder;
        chunk.m_ChunkOffset += count;
        if (count1 >= count)
          return;
        chunk = stringBuilder;
        indexInChunk = count1;
      }
    }

    [SecuritySafeCritical]
    private void Remove(int startIndex, int count, out StringBuilder chunk, out int indexInChunk)
    {
      int num = startIndex + count;
      chunk = this;
      StringBuilder stringBuilder = (StringBuilder) null;
      int sourceIndex = 0;
      while (true)
      {
        if (num - chunk.m_ChunkOffset >= 0)
        {
          if (stringBuilder == null)
          {
            stringBuilder = chunk;
            sourceIndex = num - stringBuilder.m_ChunkOffset;
          }
          if (startIndex - chunk.m_ChunkOffset >= 0)
            break;
        }
        else
          chunk.m_ChunkOffset -= count;
        chunk = chunk.m_ChunkPrevious;
      }
      indexInChunk = startIndex - chunk.m_ChunkOffset;
      int destinationIndex = indexInChunk;
      int count1 = stringBuilder.m_ChunkLength - sourceIndex;
      if (stringBuilder != chunk)
      {
        destinationIndex = 0;
        chunk.m_ChunkLength = indexInChunk;
        stringBuilder.m_ChunkPrevious = chunk;
        stringBuilder.m_ChunkOffset = chunk.m_ChunkOffset + chunk.m_ChunkLength;
        if (indexInChunk == 0)
        {
          stringBuilder.m_ChunkPrevious = chunk.m_ChunkPrevious;
          chunk = stringBuilder;
        }
      }
      stringBuilder.m_ChunkLength -= sourceIndex - destinationIndex;
      if (destinationIndex == sourceIndex)
        return;
      StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, sourceIndex, stringBuilder.m_ChunkChars, destinationIndex, count1);
    }
  }
}
