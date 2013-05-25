// Type: System.Collections.Generic.LinkedList`1
// Assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.dll

using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Generic
{
  /// <summary>
  /// Represents a doubly linked list.
  /// </summary>
  /// <typeparam name="T">Specifies the element type of the linked list.</typeparam><filterpriority>1</filterpriority>
  [ComVisible(false)]
  [DebuggerTypeProxy(typeof (System_CollectionDebugView<>))]
  [DebuggerDisplay("Count = {Count}")]
  [__DynamicallyInvokable]
  [Serializable]
  public class LinkedList<T> : ICollection<T>, IEnumerable<T>, ICollection, IEnumerable, ISerializable, IDeserializationCallback
  {
    internal LinkedListNode<T> head;
    internal int count;
    internal int version;
    private object _syncRoot;
    private SerializationInfo siInfo;
    private const string VersionName = "Version";
    private const string CountName = "Count";
    private const string ValuesName = "Data";

    /// <summary>
    /// Gets the number of nodes actually contained in the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The number of nodes actually contained in the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable, TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.count;
      }
    }

    /// <summary>
    /// Gets the first node of the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The first <see cref="T:System.Collections.Generic.LinkedListNode`1"/> of the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </returns>
    [__DynamicallyInvokable]
    public LinkedListNode<T> First
    {
      [__DynamicallyInvokable, TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.head;
      }
    }

    /// <summary>
    /// Gets the last node of the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The last <see cref="T:System.Collections.Generic.LinkedListNode`1"/> of the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </returns>
    [__DynamicallyInvokable]
    public LinkedListNode<T> Last
    {
      [__DynamicallyInvokable] get
      {
        if (this.head != null)
          return this.head.prev;
        else
          return (LinkedListNode<T>) null;
      }
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    bool ICollection.IsSynchronized
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    object ICollection.SyncRoot
    {
      [__DynamicallyInvokable] get
      {
        if (this._syncRoot == null)
          Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
        return this._syncRoot;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedList`1"/> class that is empty.
    /// </summary>
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public LinkedList()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedList`1"/> class that contains elements copied from the specified <see cref="T:System.Collections.IEnumerable"/> and has sufficient capacity to accommodate the number of elements copied.
    /// </summary>
    /// <param name="collection">The <see cref="T:System.Collections.IEnumerable"/> whose elements are copied to the new <see cref="T:System.Collections.Generic.LinkedList`1"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="collection"/> is null.</exception>
    [__DynamicallyInvokable]
    public LinkedList(IEnumerable<T> collection)
    {
      if (collection == null)
        throw new ArgumentNullException("collection");
      foreach (T obj in collection)
        this.AddLast(obj);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedList`1"/> class that is serializable with the specified <see cref="T:System.Runtime.Serialization.SerializationInfo"/> and <see cref="T:System.Runtime.Serialization.StreamingContext"/>.
    /// </summary>
    /// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo"/> object containing the information required to serialize the <see cref="T:System.Collections.Generic.LinkedList`1"/>.</param><param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext"/> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.LinkedList`1"/>.</param>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    protected LinkedList(SerializationInfo info, StreamingContext context)
    {
      this.siInfo = info;
    }

    [__DynamicallyInvokable]
    void ICollection<T>.Add(T value)
    {
      this.AddLast(value);
    }

    /// <summary>
    /// Adds a new node containing the specified value after the specified existing node in the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The new <see cref="T:System.Collections.Generic.LinkedListNode`1"/> containing <paramref name="value"/>.
    /// </returns>
    /// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1"/> after which to insert a new <see cref="T:System.Collections.Generic.LinkedListNode`1"/> containing <paramref name="value"/>.</param><param name="value">The value to add to the <see cref="T:System.Collections.Generic.LinkedList`1"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="node"/> is null.</exception><exception cref="T:System.InvalidOperationException"><paramref name="node"/> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1"/>.</exception>
    [__DynamicallyInvokable]
    public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
    {
      this.ValidateNode(node);
      LinkedListNode<T> newNode = new LinkedListNode<T>(node.list, value);
      this.InternalInsertNodeBefore(node.next, newNode);
      return newNode;
    }

    /// <summary>
    /// Adds the specified new node after the specified existing node in the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1"/> after which to insert <paramref name="newNode"/>.</param><param name="newNode">The new <see cref="T:System.Collections.Generic.LinkedListNode`1"/> to add to the <see cref="T:System.Collections.Generic.LinkedList`1"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="node"/> is null.-or-<paramref name="newNode"/> is null.</exception><exception cref="T:System.InvalidOperationException"><paramref name="node"/> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1"/>.-or-<paramref name="newNode"/> belongs to another <see cref="T:System.Collections.Generic.LinkedList`1"/>.</exception>
    [__DynamicallyInvokable]
    public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
    {
      this.ValidateNode(node);
      this.ValidateNewNode(newNode);
      this.InternalInsertNodeBefore(node.next, newNode);
      newNode.list = this;
    }

    /// <summary>
    /// Adds a new node containing the specified value before the specified existing node in the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The new <see cref="T:System.Collections.Generic.LinkedListNode`1"/> containing <paramref name="value"/>.
    /// </returns>
    /// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1"/> before which to insert a new <see cref="T:System.Collections.Generic.LinkedListNode`1"/> containing <paramref name="value"/>.</param><param name="value">The value to add to the <see cref="T:System.Collections.Generic.LinkedList`1"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="node"/> is null.</exception><exception cref="T:System.InvalidOperationException"><paramref name="node"/> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1"/>.</exception>
    [__DynamicallyInvokable]
    public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
    {
      this.ValidateNode(node);
      LinkedListNode<T> newNode = new LinkedListNode<T>(node.list, value);
      this.InternalInsertNodeBefore(node, newNode);
      if (node == this.head)
        this.head = newNode;
      return newNode;
    }

    /// <summary>
    /// Adds the specified new node before the specified existing node in the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1"/> before which to insert <paramref name="newNode"/>.</param><param name="newNode">The new <see cref="T:System.Collections.Generic.LinkedListNode`1"/> to add to the <see cref="T:System.Collections.Generic.LinkedList`1"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="node"/> is null.-or-<paramref name="newNode"/> is null.</exception><exception cref="T:System.InvalidOperationException"><paramref name="node"/> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1"/>.-or-<paramref name="newNode"/> belongs to another <see cref="T:System.Collections.Generic.LinkedList`1"/>.</exception>
    [__DynamicallyInvokable]
    public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
    {
      this.ValidateNode(node);
      this.ValidateNewNode(newNode);
      this.InternalInsertNodeBefore(node, newNode);
      newNode.list = this;
      if (node != this.head)
        return;
      this.head = newNode;
    }

    /// <summary>
    /// Adds a new node containing the specified value at the start of the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The new <see cref="T:System.Collections.Generic.LinkedListNode`1"/> containing <paramref name="value"/>.
    /// </returns>
    /// <param name="value">The value to add at the start of the <see cref="T:System.Collections.Generic.LinkedList`1"/>.</param>
    [__DynamicallyInvokable]
    public LinkedListNode<T> AddFirst(T value)
    {
      LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
      if (this.head == null)
      {
        this.InternalInsertNodeToEmptyList(newNode);
      }
      else
      {
        this.InternalInsertNodeBefore(this.head, newNode);
        this.head = newNode;
      }
      return newNode;
    }

    /// <summary>
    /// Adds the specified new node at the start of the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// <param name="node">The new <see cref="T:System.Collections.Generic.LinkedListNode`1"/> to add at the start of the <see cref="T:System.Collections.Generic.LinkedList`1"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="node"/> is null.</exception><exception cref="T:System.InvalidOperationException"><paramref name="node"/> belongs to another <see cref="T:System.Collections.Generic.LinkedList`1"/>.</exception>
    [__DynamicallyInvokable]
    public void AddFirst(LinkedListNode<T> node)
    {
      this.ValidateNewNode(node);
      if (this.head == null)
      {
        this.InternalInsertNodeToEmptyList(node);
      }
      else
      {
        this.InternalInsertNodeBefore(this.head, node);
        this.head = node;
      }
      node.list = this;
    }

    /// <summary>
    /// Adds a new node containing the specified value at the end of the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The new <see cref="T:System.Collections.Generic.LinkedListNode`1"/> containing <paramref name="value"/>.
    /// </returns>
    /// <param name="value">The value to add at the end of the <see cref="T:System.Collections.Generic.LinkedList`1"/>.</param>
    [__DynamicallyInvokable]
    public LinkedListNode<T> AddLast(T value)
    {
      LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
      if (this.head == null)
        this.InternalInsertNodeToEmptyList(newNode);
      else
        this.InternalInsertNodeBefore(this.head, newNode);
      return newNode;
    }

    /// <summary>
    /// Adds the specified new node at the end of the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// <param name="node">The new <see cref="T:System.Collections.Generic.LinkedListNode`1"/> to add at the end of the <see cref="T:System.Collections.Generic.LinkedList`1"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="node"/> is null.</exception><exception cref="T:System.InvalidOperationException"><paramref name="node"/> belongs to another <see cref="T:System.Collections.Generic.LinkedList`1"/>.</exception>
    [__DynamicallyInvokable]
    public void AddLast(LinkedListNode<T> node)
    {
      this.ValidateNewNode(node);
      if (this.head == null)
        this.InternalInsertNodeToEmptyList(node);
      else
        this.InternalInsertNodeBefore(this.head, node);
      node.list = this;
    }

    /// <summary>
    /// Removes all nodes from the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    [__DynamicallyInvokable]
    public void Clear()
    {
      LinkedListNode<T> linkedListNode1 = this.head;
      while (linkedListNode1 != null)
      {
        LinkedListNode<T> linkedListNode2 = linkedListNode1;
        linkedListNode1 = linkedListNode1.Next;
        linkedListNode2.Invalidate();
      }
      this.head = (LinkedListNode<T>) null;
      this.count = 0;
      ++this.version;
    }

    /// <summary>
    /// Determines whether a value is in the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="value"/> is found in the <see cref="T:System.Collections.Generic.LinkedList`1"/>; otherwise, false.
    /// </returns>
    /// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.LinkedList`1"/>. The value can be null for reference types.</param>
    [__DynamicallyInvokable]
    public bool Contains(T value)
    {
      return this.Find(value) != null;
    }

    /// <summary>
    /// Copies the entire <see cref="T:System.Collections.Generic.LinkedList`1"/> to a compatible one-dimensional <see cref="T:System.Array"/>, starting at the specified index of the target array.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.LinkedList`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.</exception><exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.LinkedList`1"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.</exception>
    [__DynamicallyInvokable]
    public void CopyTo(T[] array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (index < 0 || index > array.Length)
      {
        throw new ArgumentOutOfRangeException("index", SR.GetString("IndexOutOfRange", new object[1]
        {
          (object) index
        }));
      }
      else
      {
        if (array.Length - index < this.Count)
          throw new ArgumentException(SR.GetString("Arg_InsufficientSpace"));
        LinkedListNode<T> linkedListNode = this.head;
        if (linkedListNode == null)
          return;
        do
        {
          array[index++] = linkedListNode.item;
          linkedListNode = linkedListNode.next;
        }
        while (linkedListNode != this.head);
      }
    }

    /// <summary>
    /// Finds the first node that contains the specified value.
    /// </summary>
    /// 
    /// <returns>
    /// The first <see cref="T:System.Collections.Generic.LinkedListNode`1"/> that contains the specified value, if found; otherwise, null.
    /// </returns>
    /// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.LinkedList`1"/>.</param>
    [__DynamicallyInvokable]
    public LinkedListNode<T> Find(T value)
    {
      LinkedListNode<T> linkedListNode = this.head;
      EqualityComparer<T> @default = EqualityComparer<T>.Default;
      if (linkedListNode != null)
      {
        if ((object) value != null)
        {
          while (!@default.Equals(linkedListNode.item, value))
          {
            linkedListNode = linkedListNode.next;
            if (linkedListNode == this.head)
              goto label_8;
          }
          return linkedListNode;
        }
        else
        {
          while ((object) linkedListNode.item != null)
          {
            linkedListNode = linkedListNode.next;
            if (linkedListNode == this.head)
              goto label_8;
          }
          return linkedListNode;
        }
      }
label_8:
      return (LinkedListNode<T>) null;
    }

    /// <summary>
    /// Finds the last node that contains the specified value.
    /// </summary>
    /// 
    /// <returns>
    /// The last <see cref="T:System.Collections.Generic.LinkedListNode`1"/> that contains the specified value, if found; otherwise, null.
    /// </returns>
    /// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.LinkedList`1"/>.</param>
    [__DynamicallyInvokable]
    public LinkedListNode<T> FindLast(T value)
    {
      if (this.head == null)
        return (LinkedListNode<T>) null;
      LinkedListNode<T> linkedListNode1 = this.head.prev;
      LinkedListNode<T> linkedListNode2 = linkedListNode1;
      EqualityComparer<T> @default = EqualityComparer<T>.Default;
      if (linkedListNode2 != null)
      {
        if ((object) value != null)
        {
          while (!@default.Equals(linkedListNode2.item, value))
          {
            linkedListNode2 = linkedListNode2.prev;
            if (linkedListNode2 == linkedListNode1)
              goto label_10;
          }
          return linkedListNode2;
        }
        else
        {
          while ((object) linkedListNode2.item != null)
          {
            linkedListNode2 = linkedListNode2.prev;
            if (linkedListNode2 == linkedListNode1)
              goto label_10;
          }
          return linkedListNode2;
        }
      }
label_10:
      return (LinkedListNode<T>) null;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// An <see cref="T:System.Collections.Generic.LinkedList`1.Enumerator"/> for the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </returns>
    [__DynamicallyInvokable]
    public LinkedList<T>.Enumerator GetEnumerator()
    {
      return new LinkedList<T>.Enumerator(this);
    }

    [__DynamicallyInvokable]
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      return (IEnumerator<T>) this.GetEnumerator();
    }

    /// <summary>
    /// Removes the first occurrence of the specified value from the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// true if the element containing <paramref name="value"/> is successfully removed; otherwise, false.  This method also returns false if <paramref name="value"/> was not found in the original <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </returns>
    /// <param name="value">The value to remove from the <see cref="T:System.Collections.Generic.LinkedList`1"/>.</param>
    [__DynamicallyInvokable]
    public bool Remove(T value)
    {
      LinkedListNode<T> node = this.Find(value);
      if (node == null)
        return false;
      this.InternalRemoveNode(node);
      return true;
    }

    /// <summary>
    /// Removes the specified node from the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1"/> to remove from the <see cref="T:System.Collections.Generic.LinkedList`1"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="node"/> is null.</exception><exception cref="T:System.InvalidOperationException"><paramref name="node"/> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1"/>.</exception>
    [__DynamicallyInvokable]
    public void Remove(LinkedListNode<T> node)
    {
      this.ValidateNode(node);
      this.InternalRemoveNode(node);
    }

    /// <summary>
    /// Removes the node at the start of the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.LinkedList`1"/> is empty.</exception>
    [__DynamicallyInvokable]
    public void RemoveFirst()
    {
      if (this.head == null)
        throw new InvalidOperationException(SR.GetString("LinkedListEmpty"));
      this.InternalRemoveNode(this.head);
    }

    /// <summary>
    /// Removes the node at the end of the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.LinkedList`1"/> is empty.</exception>
    [__DynamicallyInvokable]
    public void RemoveLast()
    {
      if (this.head == null)
        throw new InvalidOperationException(SR.GetString("LinkedListEmpty"));
      this.InternalRemoveNode(this.head.prev);
    }

    /// <summary>
    /// Implements the <see cref="T:System.Runtime.Serialization.ISerializable"/> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.LinkedList`1"/> instance.
    /// </summary>
    /// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo"/> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.LinkedList`1"/> instance.</param><param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext"/> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.LinkedList`1"/> instance.</param><exception cref="T:System.ArgumentNullException"><paramref name="info"/> is null.</exception>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      info.AddValue("Version", this.version);
      info.AddValue("Count", this.count);
      if (this.count == 0)
        return;
      T[] array = new T[this.Count];
      this.CopyTo(array, 0);
      info.AddValue("Data", (object) array, typeof (T[]));
    }

    /// <summary>
    /// Implements the <see cref="T:System.Runtime.Serialization.ISerializable"/> interface and raises the deserialization event when the deserialization is complete.
    /// </summary>
    /// <param name="sender">The source of the deserialization event.</param><exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> object associated with the current <see cref="T:System.Collections.Generic.LinkedList`1"/> instance is invalid.</exception>
    public virtual void OnDeserialization(object sender)
    {
      if (this.siInfo == null)
        return;
      int int32 = this.siInfo.GetInt32("Version");
      if (this.siInfo.GetInt32("Count") != 0)
      {
        T[] objArray = (T[]) this.siInfo.GetValue("Data", typeof (T[]));
        if (objArray == null)
          throw new SerializationException(SR.GetString("Serialization_MissingValues"));
        for (int index = 0; index < objArray.Length; ++index)
          this.AddLast(objArray[index]);
      }
      else
        this.head = (LinkedListNode<T>) null;
      this.version = int32;
      this.siInfo = (SerializationInfo) null;
    }

    private void InternalInsertNodeBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
    {
      newNode.next = node;
      newNode.prev = node.prev;
      node.prev.next = newNode;
      node.prev = newNode;
      ++this.version;
      ++this.count;
    }

    private void InternalInsertNodeToEmptyList(LinkedListNode<T> newNode)
    {
      newNode.next = newNode;
      newNode.prev = newNode;
      this.head = newNode;
      ++this.version;
      ++this.count;
    }

    internal void InternalRemoveNode(LinkedListNode<T> node)
    {
      if (node.next == node)
      {
        this.head = (LinkedListNode<T>) null;
      }
      else
      {
        node.next.prev = node.prev;
        node.prev.next = node.next;
        if (this.head == node)
          this.head = node.next;
      }
      node.Invalidate();
      --this.count;
      ++this.version;
    }

    internal void ValidateNewNode(LinkedListNode<T> node)
    {
      if (node == null)
        throw new ArgumentNullException("node");
      if (node.list != null)
        throw new InvalidOperationException(SR.GetString("LinkedListNodeIsAttached"));
    }

    internal void ValidateNode(LinkedListNode<T> node)
    {
      if (node == null)
        throw new ArgumentNullException("node");
      if (node.list != this)
        throw new InvalidOperationException(SR.GetString("ExternalLinkedListNode"));
    }

    [__DynamicallyInvokable]
    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (array.Rank != 1)
        throw new ArgumentException(SR.GetString("Arg_MultiRank"));
      if (array.GetLowerBound(0) != 0)
        throw new ArgumentException(SR.GetString("Arg_NonZeroLowerBound"));
      if (index < 0)
      {
        throw new ArgumentOutOfRangeException("index", SR.GetString("IndexOutOfRange", new object[1]
        {
          (object) index
        }));
      }
      else
      {
        if (array.Length - index < this.Count)
          throw new ArgumentException(SR.GetString("Arg_InsufficientSpace"));
        T[] array1 = array as T[];
        if (array1 != null)
        {
          this.CopyTo(array1, index);
        }
        else
        {
          Type elementType = array.GetType().GetElementType();
          Type c = typeof (T);
          if (!elementType.IsAssignableFrom(c) && !c.IsAssignableFrom(elementType))
            throw new ArgumentException(SR.GetString("Invalid_Array_Type"));
          object[] objArray = array as object[];
          if (objArray == null)
            throw new ArgumentException(SR.GetString("Invalid_Array_Type"));
          LinkedListNode<T> linkedListNode = this.head;
          try
          {
            if (linkedListNode == null)
              return;
            do
            {
              objArray[index++] = (object) linkedListNode.item;
              linkedListNode = linkedListNode.next;
            }
            while (linkedListNode != this.head);
          }
          catch (ArrayTypeMismatchException ex)
          {
            throw new ArgumentException(SR.GetString("Invalid_Array_Type"));
          }
        }
      }
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    /// <summary>
    /// Enumerates the elements of a <see cref="T:System.Collections.Generic.LinkedList`1"/>.
    /// </summary>
    [__DynamicallyInvokable]
    [Serializable]
    public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator, ISerializable, IDeserializationCallback
    {
      private LinkedList<T> list;
      private LinkedListNode<T> node;
      private int version;
      private T current;
      private int index;
      private SerializationInfo siInfo;
      private const string LinkedListName = "LinkedList";
      private const string CurrentValueName = "Current";
      private const string VersionName = "Version";
      private const string IndexName = "Index";

      /// <summary>
      /// Gets the element at the current position of the enumerator.
      /// </summary>
      /// 
      /// <returns>
      /// The element in the <see cref="T:System.Collections.Generic.LinkedList`1"/> at the current position of the enumerator.
      /// </returns>
      [__DynamicallyInvokable]
      public T Current
      {
        [__DynamicallyInvokable, TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
        {
          return this.current;
        }
      }

      [__DynamicallyInvokable]
      object IEnumerator.Current
      {
        [__DynamicallyInvokable] get
        {
          if (this.index == 0 || this.index == this.list.Count + 1)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
          return (object) this.current;
        }
      }

      internal Enumerator(LinkedList<T> list)
      {
        this.list = list;
        this.version = list.version;
        this.node = list.head;
        this.current = default (T);
        this.index = 0;
        this.siInfo = (SerializationInfo) null;
      }

      internal Enumerator(SerializationInfo info, StreamingContext context)
      {
        this.siInfo = info;
        this.list = (LinkedList<T>) null;
        this.version = 0;
        this.node = (LinkedListNode<T>) null;
        this.current = default (T);
        this.index = 0;
      }

      /// <summary>
      /// Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.LinkedList`1"/>.
      /// </summary>
      /// 
      /// <returns>
      /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
      /// </returns>
      /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
      [__DynamicallyInvokable]
      public bool MoveNext()
      {
        if (this.version != this.list.version)
          throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
        if (this.node == null)
        {
          this.index = this.list.Count + 1;
          return false;
        }
        else
        {
          ++this.index;
          this.current = this.node.item;
          this.node = this.node.next;
          if (this.node == this.list.head)
            this.node = (LinkedListNode<T>) null;
          return true;
        }
      }

      [__DynamicallyInvokable]
      void IEnumerator.Reset()
      {
        if (this.version != this.list.version)
          throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
        this.current = default (T);
        this.node = this.list.head;
        this.index = 0;
      }

      /// <summary>
      /// Releases all resources used by the <see cref="T:System.Collections.Generic.LinkedList`1.Enumerator"/>.
      /// </summary>
      [__DynamicallyInvokable]
      public void Dispose()
      {
      }

      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        info.AddValue("LinkedList", (object) this.list);
        info.AddValue("Version", this.version);
        info.AddValue("Current", (object) this.current);
        info.AddValue("Index", this.index);
      }

      void IDeserializationCallback.OnDeserialization(object sender)
      {
        if (this.list != null)
          return;
        if (this.siInfo == null)
          throw new SerializationException(SR.GetString("Serialization_InvalidOnDeser"));
        this.list = (LinkedList<T>) this.siInfo.GetValue("LinkedList", typeof (LinkedList<T>));
        this.version = this.siInfo.GetInt32("Version");
        this.current = (T) this.siInfo.GetValue("Current", typeof (T));
        this.index = this.siInfo.GetInt32("Index");
        if (this.list.siInfo != null)
          this.list.OnDeserialization(sender);
        if (this.index == this.list.Count + 1)
        {
          this.node = (LinkedListNode<T>) null;
        }
        else
        {
          this.node = this.list.First;
          if (this.node != null && this.index != 0)
          {
            for (int index = 0; index < this.index; ++index)
              this.node = this.node.next;
            if (this.node == this.list.First)
              this.node = (LinkedListNode<T>) null;
          }
        }
        this.siInfo = (SerializationInfo) null;
      }
    }
  }
}
