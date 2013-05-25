// Type: System.Threading.Timer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  /// Provides a mechanism for executing a method at specified intervals. This class cannot be inherited.
  /// </summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public sealed class Timer : MarshalByRefObject, IDisposable
  {
    private TimerHolder m_timer;
    private const uint MAX_SUPPORTED_TIMEOUT = 4294967294U;

    /// <summary>
    /// Initializes a new instance of the Timer class, using a 32-bit signed integer to specify the time interval.
    /// </summary>
    /// <param name="callback">A <see cref="T:System.Threading.TimerCallback"/> delegate representing a method to be executed. </param><param name="state">An object containing information to be used by the callback method, or null. </param><param name="dueTime">The amount of time to delay before <paramref name="callback"/> is invoked, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite"/> to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param><param name="period">The time interval between invocations of <paramref name="callback"/>, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite"/> to disable periodic signaling. </param><exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime"/> or <paramref name="period"/> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite"/>. </exception><exception cref="T:System.ArgumentNullException">The <paramref name="callback"/> parameter is null. </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Timer(TimerCallback callback, object state, int dueTime, int period)
    {
      if (dueTime < -1)
        throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (period < -1)
        throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.TimerSetup(callback, state, (uint) dueTime, (uint) period, ref stackMark);
    }

    /// <summary>
    /// Initializes a new instance of the Timer class, using <see cref="T:System.TimeSpan"/> values to measure time intervals.
    /// </summary>
    /// <param name="callback">A <see cref="T:System.Threading.TimerCallback"/> delegate representing a method to be executed. </param><param name="state">An object containing information to be used by the callback method, or null. </param><param name="dueTime">The <see cref="T:System.TimeSpan"/> representing the amount of time to delay before the <paramref name="callback"/> parameter invokes its methods. Specify negative one (-1) milliseconds to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param><param name="period">The time interval between invocations of the methods referenced by <paramref name="callback"/>. Specify negative one (-1) milliseconds to disable periodic signaling. </param><exception cref="T:System.ArgumentOutOfRangeException">The number of milliseconds in the value of <paramref name="dueTime"/> or <paramref name="period"/> is negative and not equal to <see cref="F:System.Threading.Timeout.Infinite"/>, or is greater than <see cref="F:System.Int32.MaxValue"/>. </exception><exception cref="T:System.ArgumentNullException">The <paramref name="callback"/> parameter is null. </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Timer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period)
    {
      long num1 = (long) dueTime.TotalMilliseconds;
      if (num1 < -1L)
        throw new ArgumentOutOfRangeException("dueTm", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (num1 > 4294967294L)
        throw new ArgumentOutOfRangeException("dueTm", Environment.GetResourceString("ArgumentOutOfRange_TimeoutTooLarge"));
      long num2 = (long) period.TotalMilliseconds;
      if (num2 < -1L)
        throw new ArgumentOutOfRangeException("periodTm", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (num2 > 4294967294L)
        throw new ArgumentOutOfRangeException("periodTm", Environment.GetResourceString("ArgumentOutOfRange_PeriodTooLarge"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.TimerSetup(callback, state, (uint) num1, (uint) num2, ref stackMark);
    }

    /// <summary>
    /// Initializes a new instance of the Timer class, using 32-bit unsigned integers to measure time intervals.
    /// </summary>
    /// <param name="callback">A <see cref="T:System.Threading.TimerCallback"/> delegate representing a method to be executed. </param><param name="state">An object containing information to be used by the callback method, or null. </param><param name="dueTime">The amount of time to delay before <paramref name="callback"/> is invoked, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite"/> to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param><param name="period">The time interval between invocations of <paramref name="callback"/>, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite"/> to disable periodic signaling. </param><exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime"/> or <paramref name="period"/> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite"/>. </exception><exception cref="T:System.ArgumentNullException">The <paramref name="callback"/> parameter is null. </exception>
    [CLSCompliant(false)]
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Timer(TimerCallback callback, object state, uint dueTime, uint period)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.TimerSetup(callback, state, dueTime, period, ref stackMark);
    }

    /// <summary>
    /// Initializes a new instance of the Timer class, using 64-bit signed integers to measure time intervals.
    /// </summary>
    /// <param name="callback">A <see cref="T:System.Threading.TimerCallback"/> delegate representing a method to be executed. </param><param name="state">An object containing information to be used by the callback method, or null. </param><param name="dueTime">The amount of time to delay before <paramref name="callback"/> is invoked, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite"/> to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param><param name="period">The time interval between invocations of <paramref name="callback"/>, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite"/> to disable periodic signaling. </param><exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime"/> or <paramref name="period"/> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite"/>. </exception><exception cref="T:System.NotSupportedException">The <paramref name="dueTime"/> or <paramref name="period"/> parameter is greater than 4294967294. </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Timer(TimerCallback callback, object state, long dueTime, long period)
    {
      if (dueTime < -1L)
        throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (period < -1L)
        throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (dueTime > 4294967294L)
        throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_TimeoutTooLarge"));
      if (period > 4294967294L)
        throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_PeriodTooLarge"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.TimerSetup(callback, state, (uint) dueTime, (uint) period, ref stackMark);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Threading.Timer"/> class with an infinite period and an infinite due time, using the newly created <see cref="T:System.Threading.Timer"/> object as the state object.
    /// </summary>
    /// <param name="callback">A <see cref="T:System.Threading.TimerCallback"/> delegate representing a method to be executed.</param>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Timer(TimerCallback callback)
    {
      int num1 = -1;
      int num2 = -1;
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.TimerSetup(callback, (object) this, (uint) num1, (uint) num2, ref stackMark);
    }

    /// <summary>
    /// Changes the start time and the interval between method invocations for a timer, using 32-bit signed integers to measure time intervals.
    /// </summary>
    /// 
    /// <returns>
    /// true if the timer was successfully updated; otherwise, false.
    /// </returns>
    /// <param name="dueTime">The amount of time to delay before the invoking the callback method specified when the <see cref="T:System.Threading.Timer"/> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite"/> to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param><param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer"/> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite"/> to disable periodic signaling. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer"/> has already been disposed. </exception><exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime"/> or <paramref name="period"/> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite"/>. </exception><filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool Change(int dueTime, int period)
    {
      if (dueTime < -1)
        throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (period < -1)
        throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      else
        return this.m_timer.m_timer.Change((uint) dueTime, (uint) period);
    }

    /// <summary>
    /// Changes the start time and the interval between method invocations for a timer, using <see cref="T:System.TimeSpan"/> values to measure time intervals.
    /// </summary>
    /// 
    /// <returns>
    /// true if the timer was successfully updated; otherwise, false.
    /// </returns>
    /// <param name="dueTime">A <see cref="T:System.TimeSpan"/> representing the amount of time to delay before invoking the callback method specified when the <see cref="T:System.Threading.Timer"/> was constructed. Specify negative one (-1) milliseconds to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param><param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer"/> was constructed. Specify negative one (-1) milliseconds to disable periodic signaling. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer"/> has already been disposed. </exception><exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime"/> or <paramref name="period"/> parameter, in milliseconds, is less than -1. </exception><exception cref="T:System.NotSupportedException">The <paramref name="dueTime"/> or <paramref name="period"/> parameter, in milliseconds, is greater than 4294967294. </exception><filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool Change(TimeSpan dueTime, TimeSpan period)
    {
      return this.Change((long) dueTime.TotalMilliseconds, (long) period.TotalMilliseconds);
    }

    /// <summary>
    /// Changes the start time and the interval between method invocations for a timer, using 32-bit unsigned integers to measure time intervals.
    /// </summary>
    /// 
    /// <returns>
    /// true if the timer was successfully updated; otherwise, false.
    /// </returns>
    /// <param name="dueTime">The amount of time to delay before the invoking the callback method specified when the <see cref="T:System.Threading.Timer"/> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite"/> to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param><param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer"/> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite"/> to disable periodic signaling. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer"/> has already been disposed. </exception><filterpriority>2</filterpriority>
    [CLSCompliant(false)]
    public bool Change(uint dueTime, uint period)
    {
      return this.m_timer.m_timer.Change(dueTime, period);
    }

    /// <summary>
    /// Changes the start time and the interval between method invocations for a timer, using 64-bit signed integers to measure time intervals.
    /// </summary>
    /// 
    /// <returns>
    /// true if the timer was successfully updated; otherwise, false.
    /// </returns>
    /// <param name="dueTime">The amount of time to delay before the invoking the callback method specified when the <see cref="T:System.Threading.Timer"/> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite"/> to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param><param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer"/> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite"/> to disable periodic signaling. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer"/> has already been disposed. </exception><exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime"/> or <paramref name="period"/> parameter is less than -1. </exception><exception cref="T:System.NotSupportedException">The <paramref name="dueTime"/> or <paramref name="period"/> parameter is greater than 4294967294. </exception><filterpriority>2</filterpriority>
    public bool Change(long dueTime, long period)
    {
      if (dueTime < -1L)
        throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (period < -1L)
        throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (dueTime > 4294967294L)
        throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_TimeoutTooLarge"));
      if (period > 4294967294L)
        throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_PeriodTooLarge"));
      else
        return this.m_timer.m_timer.Change((uint) dueTime, (uint) period);
    }

    /// <summary>
    /// Releases all resources used by the current instance of <see cref="T:System.Threading.Timer"/> and signals when the timer has been disposed of.
    /// </summary>
    /// 
    /// <returns>
    /// true if the function succeeds; otherwise, false.
    /// </returns>
    /// <param name="notifyObject">The <see cref="T:System.Threading.WaitHandle"/> to be signaled when the Timer has been disposed of. </param><exception cref="T:System.ArgumentNullException">The <paramref name="notifyObject"/> parameter is null. </exception><filterpriority>2</filterpriority>
    public bool Dispose(WaitHandle notifyObject)
    {
      if (notifyObject == null)
        throw new ArgumentNullException("notifyObject");
      else
        return this.m_timer.Close(notifyObject);
    }

    /// <summary>
    /// Releases all resources used by the current instance of <see cref="T:System.Threading.Timer"/>.
    /// </summary>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.m_timer.Close();
    }

    internal void KeepRootedWhileScheduled()
    {
      GC.SuppressFinalize((object) this.m_timer);
    }

    [SecurityCritical]
    private void TimerSetup(TimerCallback callback, object state, uint dueTime, uint period, ref StackCrawlMark stackMark)
    {
      if (callback == null)
        throw new ArgumentNullException("TimerCallback");
      this.m_timer = new TimerHolder(new TimerQueueTimer(callback, state, dueTime, period, ref stackMark));
    }
  }
}
