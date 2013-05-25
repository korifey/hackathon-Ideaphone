// Type: System.Collections.Generic.List`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\mscorlib.dll

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime;

namespace System.Collections.Generic
{
  /// <summary>
  /// Represents a strongly typed list of objects that can be accessed by index. Provides methods to search, sort, and manipulate lists.
  /// </summary>
  /// <typeparam name="T">The type of elements in the list.</typeparam><filterpriority>1</filterpriority>
  [DebuggerTypeProxy(typeof (Mscorlib_CollectionDebugView<>))]
  [DebuggerDisplay("Count = {Count}")]
  [Serializable]
  public class List<T> : IList<T>, ICollection<T>, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
  {
    /// <summary>
    /// Gets or sets the total number of elements the internal data structure can hold without resizing.
    /// </summary>
    /// 
    /// <returns>
    /// The number of elements that the <see cref="T:System.Collections.Generic.List`1"/> can contain before resizing is required.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException"><see cref="P:System.Collections.Generic.List`1.Capacity"/> is set to a value that is less than <see cref="P:System.Collections.Generic.List`1.Count"/>. </exception><exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
    public int Capacity { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")] get; set; }

    /// <summary>
    /// Gets the number of elements actually contained in the <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The number of elements actually contained in the <see cref="T:System.Collections.Generic.List`1"/>.
    /// </returns>
    public int Count { get; }

    bool IList.IsFixedSize { get; }

    bool ICollection<T>.IsReadOnly { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")] get; }

    bool IList.IsReadOnly { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")] get; }

    bool ICollection.IsSynchronized { get; }

    object ICollection.SyncRoot { get; }

    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// 
    /// <returns>
    /// The element at the specified index.
    /// </returns>
    /// <param name="index">The zero-based index of the element to get or set.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="index"/> is equal to or greater than <see cref="P:System.Collections.Generic.List`1.Count"/>. </exception>
    public T this[int index] { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")] set; }

    static List();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Collections.Generic.List`1"/> class that is empty and has the default initial capacity.
    /// </summary>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public List();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Collections.Generic.List`1"/> class that is empty and has the specified initial capacity.
    /// </summary>
    /// <param name="capacity">The number of elements that the new list can initially store.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity"/> is less than 0. </exception>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public List(int capacity);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Collections.Generic.List`1"/> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new list.</param><exception cref="T:System.ArgumentNullException"><paramref name="collection"/> is null.</exception>
    public List(IEnumerable<T> collection);

    object IList.get_Item(int index);

    void IList.set_Item(int index, object value);

    /// <summary>
    /// Adds an object to the end of the <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// <param name="item">The object to be added to the end of the <see cref="T:System.Collections.Generic.List`1"/>. The value can be null for reference types.</param>
    public void Add(T item);

    int IList.Add(object item);

    /// <summary>
    /// Adds the elements of the specified collection to the end of the <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// <param name="collection">The collection whose elements should be added to the end of the <see cref="T:System.Collections.Generic.List`1"/>. The collection itself cannot be null, but it can contain elements that are null, if type <paramref name="T"/> is a reference type.</param><exception cref="T:System.ArgumentNullException"><paramref name="collection"/> is null.</exception>
    public void AddRange(IEnumerable<T> collection);

    /// <summary>
    /// Returns a read-only <see cref="T:System.Collections.Generic.IList`1"/> wrapper for the current collection.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"/> that acts as a read-only wrapper around the current <see cref="T:System.Collections.Generic.List`1"/>.
    /// </returns>
    public ReadOnlyCollection<T> AsReadOnly();

    /// <summary>
    /// Searches a range of elements in the sorted <see cref="T:System.Collections.Generic.List`1"/> for an element using the specified comparer and returns the zero-based index of the element.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of <paramref name="item"/> in the sorted <see cref="T:System.Collections.Generic.List`1"/>, if <paramref name="item"/> is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than <paramref name="item"/> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.Generic.List`1.Count"/>.
    /// </returns>
    /// <param name="index">The zero-based starting index of the range to search.</param><param name="count">The length of the range to search.</param><param name="item">The object to locate. The value can be null for reference types.</param><param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1"/> implementation to use when comparing elements, or null to use the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default"/>.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="count"/> is less than 0. </exception><exception cref="T:System.ArgumentException"><paramref name="index"/> and <paramref name="count"/> do not denote a valid range in the <see cref="T:System.Collections.Generic.List`1"/>.</exception><exception cref="T:System.InvalidOperationException"><paramref name="comparer"/> is null, and the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default"/> cannot find an implementation of the <see cref="T:System.IComparable`1"/> generic interface or the <see cref="T:System.IComparable"/> interface for type <paramref name="T"/>.</exception>
    public int BinarySearch(int index, int count, T item, IComparer<T> comparer);

    /// <summary>
    /// Searches the entire sorted <see cref="T:System.Collections.Generic.List`1"/> for an element using the default comparer and returns the zero-based index of the element.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of <paramref name="item"/> in the sorted <see cref="T:System.Collections.Generic.List`1"/>, if <paramref name="item"/> is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than <paramref name="item"/> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.Generic.List`1.Count"/>.
    /// </returns>
    /// <param name="item">The object to locate. The value can be null for reference types.</param><exception cref="T:System.InvalidOperationException">The default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default"/> cannot find an implementation of the <see cref="T:System.IComparable`1"/> generic interface or the <see cref="T:System.IComparable"/> interface for type <paramref name="T"/>.</exception>
    public int BinarySearch(T item);

    /// <summary>
    /// Searches the entire sorted <see cref="T:System.Collections.Generic.List`1"/> for an element using the specified comparer and returns the zero-based index of the element.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of <paramref name="item"/> in the sorted <see cref="T:System.Collections.Generic.List`1"/>, if <paramref name="item"/> is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than <paramref name="item"/> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.Generic.List`1.Count"/>.
    /// </returns>
    /// <param name="item">The object to locate. The value can be null for reference types.</param><param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1"/> implementation to use when comparing elements.-or-null to use the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default"/>.</param><exception cref="T:System.InvalidOperationException"><paramref name="comparer"/> is null, and the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default"/> cannot find an implementation of the <see cref="T:System.IComparable`1"/> generic interface or the <see cref="T:System.IComparable"/> interface for type <paramref name="T"/>.</exception>
    public int BinarySearch(T item, IComparer<T> comparer);

    /// <summary>
    /// Removes all elements from the <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    public void Clear();

    /// <summary>
    /// Determines whether an element is in the <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.List`1"/>; otherwise, false.
    /// </returns>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1"/>. The value can be null for reference types.</param>
    public bool Contains(T item);

    bool IList.Contains(object item);

    /// <summary>
    /// Converts the elements in the current <see cref="T:System.Collections.Generic.List`1"/> to another type, and returns a list containing the converted elements.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.List`1"/> of the target type containing the converted elements from the current <see cref="T:System.Collections.Generic.List`1"/>.
    /// </returns>
    /// <param name="converter">A <see cref="T:System.Converter`2"/> delegate that converts each element from one type to another type.</param><typeparam name="TOutput">The type of the elements of the target array.</typeparam><exception cref="T:System.ArgumentNullException"><paramref name="converter"/> is null.</exception>
    public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter);

    /// <summary>
    /// Copies the entire <see cref="T:System.Collections.Generic.List`1"/> to a compatible one-dimensional array, starting at the beginning of the target array.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.List`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception><exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.List`1"/> is greater than the number of elements that the destination <paramref name="array"/> can contain.</exception>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public void CopyTo(T[] array);

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    void ICollection.CopyTo(Array array, int arrayIndex);

    /// <summary>
    /// Copies a range of elements from the <see cref="T:System.Collections.Generic.List`1"/> to a compatible one-dimensional array, starting at the specified index of the target array.
    /// </summary>
    /// <param name="index">The zero-based index in the source <see cref="T:System.Collections.Generic.List`1"/> at which copying begins.</param><param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.List`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param><param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param><param name="count">The number of elements to copy.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="arrayIndex"/> is less than 0.-or-<paramref name="count"/> is less than 0. </exception><exception cref="T:System.ArgumentException"><paramref name="index"/> is equal to or greater than the <see cref="P:System.Collections.Generic.List`1.Count"/> of the source <see cref="T:System.Collections.Generic.List`1"/>.-or-The number of elements from <paramref name="index"/> to the end of the source <see cref="T:System.Collections.Generic.List`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>. </exception>
    public void CopyTo(int index, T[] array, int arrayIndex, int count);

    /// <summary>
    /// Copies the entire <see cref="T:System.Collections.Generic.List`1"/> to a compatible one-dimensional array, starting at the specified index of the target array.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.List`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param><param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception><exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.List`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.</exception>
    public void CopyTo(T[] array, int arrayIndex);

    /// <summary>
    /// Determines whether the <see cref="T:System.Collections.Generic.List`1"/> contains elements that match the conditions defined by the specified predicate.
    /// </summary>
    /// 
    /// <returns>
    /// true if the <see cref="T:System.Collections.Generic.List`1"/> contains one or more elements that match the conditions defined by the specified predicate; otherwise, false.
    /// </returns>
    /// <param name="match">The <see cref="T:System.Predicate`1"/> delegate that defines the conditions of the elements to search for.</param><exception cref="T:System.ArgumentNullException"><paramref name="match"/> is null.</exception>
    public bool Exists(Predicate<T> match);

    /// <summary>
    /// Searches for an element that matches the conditions defined by the specified predicate, and returns the first occurrence within the entire <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The first element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type <paramref name="T"/>.
    /// </returns>
    /// <param name="match">The <see cref="T:System.Predicate`1"/> delegate that defines the conditions of the element to search for.</param><exception cref="T:System.ArgumentNullException"><paramref name="match"/> is null.</exception>
    public T Find(Predicate<T> match);

    /// <summary>
    /// Retrieves all the elements that match the conditions defined by the specified predicate.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.List`1"/> containing all the elements that match the conditions defined by the specified predicate, if found; otherwise, an empty <see cref="T:System.Collections.Generic.List`1"/>.
    /// </returns>
    /// <param name="match">The <see cref="T:System.Predicate`1"/> delegate that defines the conditions of the elements to search for.</param><exception cref="T:System.ArgumentNullException"><paramref name="match"/> is null.</exception>
    public List<T> FindAll(Predicate<T> match);

    /// <summary>
    /// Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match"/>, if found; otherwise, –1.
    /// </returns>
    /// <param name="match">The <see cref="T:System.Predicate`1"/> delegate that defines the conditions of the element to search for.</param><exception cref="T:System.ArgumentNullException"><paramref name="match"/> is null.</exception>
    public int FindIndex(Predicate<T> match);

    /// <summary>
    /// Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1"/> that extends from the specified index to the last element.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match"/>, if found; otherwise, –1.
    /// </returns>
    /// <param name="startIndex">The zero-based starting index of the search.</param><param name="match">The <see cref="T:System.Predicate`1"/> delegate that defines the conditions of the element to search for.</param><exception cref="T:System.ArgumentNullException"><paramref name="match"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1"/>.</exception>
    public int FindIndex(int startIndex, Predicate<T> match);

    /// <summary>
    /// Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1"/> that starts at the specified index and contains the specified number of elements.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match"/>, if found; otherwise, –1.
    /// </returns>
    /// <param name="startIndex">The zero-based starting index of the search.</param><param name="count">The number of elements in the section to search.</param><param name="match">The <see cref="T:System.Predicate`1"/> delegate that defines the conditions of the element to search for.</param><exception cref="T:System.ArgumentNullException"><paramref name="match"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1"/>.-or-<paramref name="count"/> is less than 0.-or-<paramref name="startIndex"/> and <paramref name="count"/> do not specify a valid section in the <see cref="T:System.Collections.Generic.List`1"/>.</exception>
    public int FindIndex(int startIndex, int count, Predicate<T> match);

    /// <summary>
    /// Searches for an element that matches the conditions defined by the specified predicate, and returns the last occurrence within the entire <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The last element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type <paramref name="T"/>.
    /// </returns>
    /// <param name="match">The <see cref="T:System.Predicate`1"/> delegate that defines the conditions of the element to search for.</param><exception cref="T:System.ArgumentNullException"><paramref name="match"/> is null.</exception>
    public T FindLast(Predicate<T> match);

    /// <summary>
    /// Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the entire <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match"/>, if found; otherwise, –1.
    /// </returns>
    /// <param name="match">The <see cref="T:System.Predicate`1"/> delegate that defines the conditions of the element to search for.</param><exception cref="T:System.ArgumentNullException"><paramref name="match"/> is null.</exception>
    public int FindLastIndex(Predicate<T> match);

    /// <summary>
    /// Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1"/> that extends from the first element to the specified index.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match"/>, if found; otherwise, –1.
    /// </returns>
    /// <param name="startIndex">The zero-based starting index of the backward search.</param><param name="match">The <see cref="T:System.Predicate`1"/> delegate that defines the conditions of the element to search for.</param><exception cref="T:System.ArgumentNullException"><paramref name="match"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1"/>.</exception>
    public int FindLastIndex(int startIndex, Predicate<T> match);

    /// <summary>
    /// Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1"/> that contains the specified number of elements and ends at the specified index.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match"/>, if found; otherwise, –1.
    /// </returns>
    /// <param name="startIndex">The zero-based starting index of the backward search.</param><param name="count">The number of elements in the section to search.</param><param name="match">The <see cref="T:System.Predicate`1"/> delegate that defines the conditions of the element to search for.</param><exception cref="T:System.ArgumentNullException"><paramref name="match"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1"/>.-or-<paramref name="count"/> is less than 0.-or-<paramref name="startIndex"/> and <paramref name="count"/> do not specify a valid section in the <see cref="T:System.Collections.Generic.List`1"/>.</exception>
    public int FindLastIndex(int startIndex, int count, Predicate<T> match);

    /// <summary>
    /// Performs the specified action on each element of the <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// <param name="action">The <see cref="T:System.Action`1"/> delegate to perform on each element of the <see cref="T:System.Collections.Generic.List`1"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="action"/> is null.</exception>
    public void ForEach(Action<T> action);

    /// <summary>
    /// Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.List`1.Enumerator"/> for the <see cref="T:System.Collections.Generic.List`1"/>.
    /// </returns>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public List<T>.Enumerator GetEnumerator();

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    IEnumerator<T> IEnumerable<T>.GetEnumerator();

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    IEnumerator IEnumerable.GetEnumerator();

    /// <summary>
    /// Creates a shallow copy of a range of elements in the source <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// A shallow copy of a range of elements in the source <see cref="T:System.Collections.Generic.List`1"/>.
    /// </returns>
    /// <param name="index">The zero-based <see cref="T:System.Collections.Generic.List`1"/> index at which the range starts.</param><param name="count">The number of elements in the range.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="count"/> is less than 0.</exception><exception cref="T:System.ArgumentException"><paramref name="index"/> and <paramref name="count"/> do not denote a valid range of elements in the <see cref="T:System.Collections.Generic.List`1"/>.</exception>
    public List<T> GetRange(int index, int count);

    /// <summary>
    /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of the first occurrence of <paramref name="item"/> within the entire <see cref="T:System.Collections.Generic.List`1"/>, if found; otherwise, –1.
    /// </returns>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1"/>. The value can be null for reference types.</param>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public int IndexOf(T item);

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    int IList.IndexOf(object item);

    /// <summary>
    /// Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1"/> that extends from the specified index to the last element.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of the first occurrence of <paramref name="item"/> within the range of elements in the <see cref="T:System.Collections.Generic.List`1"/> that extends from <paramref name="index"/> to the last element, if found; otherwise, –1.
    /// </returns>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1"/>. The value can be null for reference types.</param><param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1"/>.</exception>
    public int IndexOf(T item, int index);

    /// <summary>
    /// Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1"/> that starts at the specified index and contains the specified number of elements.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of the first occurrence of <paramref name="item"/> within the range of elements in the <see cref="T:System.Collections.Generic.List`1"/> that starts at <paramref name="index"/> and contains <paramref name="count"/> number of elements, if found; otherwise, –1.
    /// </returns>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1"/>. The value can be null for reference types.</param><param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param><param name="count">The number of elements in the section to search.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1"/>.-or-<paramref name="count"/> is less than 0.-or-<paramref name="index"/> and <paramref name="count"/> do not specify a valid section in the <see cref="T:System.Collections.Generic.List`1"/>.</exception>
    public int IndexOf(T item, int index, int count);

    /// <summary>
    /// Inserts an element into the <see cref="T:System.Collections.Generic.List`1"/> at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param><param name="item">The object to insert. The value can be null for reference types.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="index"/> is greater than <see cref="P:System.Collections.Generic.List`1.Count"/>.</exception>
    public void Insert(int index, T item);

    void IList.Insert(int index, object item);

    /// <summary>
    /// Inserts the elements of a collection into the <see cref="T:System.Collections.Generic.List`1"/> at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which the new elements should be inserted.</param><param name="collection">The collection whose elements should be inserted into the <see cref="T:System.Collections.Generic.List`1"/>. The collection itself cannot be null, but it can contain elements that are null, if type <paramref name="T"/> is a reference type.</param><exception cref="T:System.ArgumentNullException"><paramref name="collection"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="index"/> is greater than <see cref="P:System.Collections.Generic.List`1.Count"/>.</exception>
    public void InsertRange(int index, IEnumerable<T> collection);

    /// <summary>
    /// Searches for the specified object and returns the zero-based index of the last occurrence within the entire <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of the last occurrence of <paramref name="item"/> within the entire the <see cref="T:System.Collections.Generic.List`1"/>, if found; otherwise, –1.
    /// </returns>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1"/>. The value can be null for reference types.</param>
    public int LastIndexOf(T item);

    /// <summary>
    /// Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1"/> that extends from the first element to the specified index.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of the last occurrence of <paramref name="item"/> within the range of elements in the <see cref="T:System.Collections.Generic.List`1"/> that extends from the first element to <paramref name="index"/>, if found; otherwise, –1.
    /// </returns>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1"/>. The value can be null for reference types.</param><param name="index">The zero-based starting index of the backward search.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1"/>. </exception>
    public int LastIndexOf(T item, int index);

    /// <summary>
    /// Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1"/> that contains the specified number of elements and ends at the specified index.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of the last occurrence of <paramref name="item"/> within the range of elements in the <see cref="T:System.Collections.Generic.List`1"/> that contains <paramref name="count"/> number of elements and ends at <paramref name="index"/>, if found; otherwise, –1.
    /// </returns>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1"/>. The value can be null for reference types.</param><param name="index">The zero-based starting index of the backward search.</param><param name="count">The number of elements in the section to search.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1"/>.-or-<paramref name="count"/> is less than 0.-or-<paramref name="index"/> and <paramref name="count"/> do not specify a valid section in the <see cref="T:System.Collections.Generic.List`1"/>. </exception>
    public int LastIndexOf(T item, int index, int count);

    /// <summary>
    /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="item"/> is successfully removed; otherwise, false.  This method also returns false if <paramref name="item"/> was not found in the <see cref="T:System.Collections.Generic.List`1"/>.
    /// </returns>
    /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.List`1"/>. The value can be null for reference types.</param>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public bool Remove(T item);

    void IList.Remove(object item);

    /// <summary>
    /// Removes all the elements that match the conditions defined by the specified predicate.
    /// </summary>
    /// 
    /// <returns>
    /// The number of elements removed from the <see cref="T:System.Collections.Generic.List`1"/> .
    /// </returns>
    /// <param name="match">The <see cref="T:System.Predicate`1"/> delegate that defines the conditions of the elements to remove.</param><exception cref="T:System.ArgumentNullException"><paramref name="match"/> is null.</exception>
    public int RemoveAll(Predicate<T> match);

    /// <summary>
    /// Removes the element at the specified index of the <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// <param name="index">The zero-based index of the element to remove.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="index"/> is equal to or greater than <see cref="P:System.Collections.Generic.List`1.Count"/>.</exception>
    public void RemoveAt(int index);

    /// <summary>
    /// Removes a range of elements from the <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    /// <param name="index">The zero-based starting index of the range of elements to remove.</param><param name="count">The number of elements to remove.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="count"/> is less than 0.</exception><exception cref="T:System.ArgumentException"><paramref name="index"/> and <paramref name="count"/> do not denote a valid range of elements in the <see cref="T:System.Collections.Generic.List`1"/>.</exception>
    public void RemoveRange(int index, int count);

    /// <summary>
    /// Reverses the order of the elements in the entire <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    public void Reverse();

    /// <summary>
    /// Reverses the order of the elements in the specified range.
    /// </summary>
    /// <param name="index">The zero-based starting index of the range to reverse.</param><param name="count">The number of elements in the range to reverse.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="count"/> is less than 0. </exception><exception cref="T:System.ArgumentException"><paramref name="index"/> and <paramref name="count"/> do not denote a valid range of elements in the <see cref="T:System.Collections.Generic.List`1"/>. </exception>
    public void Reverse(int index, int count);

    /// <summary>
    /// Sorts the elements in the entire <see cref="T:System.Collections.Generic.List`1"/> using the default comparer.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">The default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default"/> cannot find an implementation of the <see cref="T:System.IComparable`1"/> generic interface or the <see cref="T:System.IComparable"/> interface for type <paramref name="T"/>.</exception>
    public void Sort();

    /// <summary>
    /// Sorts the elements in the entire <see cref="T:System.Collections.Generic.List`1"/> using the specified comparer.
    /// </summary>
    /// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1"/> implementation to use when comparing elements, or null to use the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default"/>.</param><exception cref="T:System.InvalidOperationException"><paramref name="comparer"/> is null, and the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default"/> cannot find implementation of the <see cref="T:System.IComparable`1"/> generic interface or the <see cref="T:System.IComparable"/> interface for type <paramref name="T"/>.</exception><exception cref="T:System.ArgumentException">The implementation of <paramref name="comparer"/> caused an error during the sort. For example, <paramref name="comparer"/> might not return 0 when comparing an item with itself.</exception>
    public void Sort(IComparer<T> comparer);

    /// <summary>
    /// Sorts the elements in a range of elements in <see cref="T:System.Collections.Generic.List`1"/> using the specified comparer.
    /// </summary>
    /// <param name="index">The zero-based starting index of the range to sort.</param><param name="count">The length of the range to sort.</param><param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1"/> implementation to use when comparing elements, or null to use the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default"/>.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="count"/> is less than 0.</exception><exception cref="T:System.ArgumentException"><paramref name="index"/> and <paramref name="count"/> do not specify a valid range in the <see cref="T:System.Collections.Generic.List`1"/>.-or-The implementation of <paramref name="comparer"/> caused an error during the sort. For example, <paramref name="comparer"/> might not return 0 when comparing an item with itself.</exception><exception cref="T:System.InvalidOperationException"><paramref name="comparer"/> is null, and the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default"/> cannot find implementation of the <see cref="T:System.IComparable`1"/> generic interface or the <see cref="T:System.IComparable"/> interface for type <paramref name="T"/>.</exception>
    public void Sort(int index, int count, IComparer<T> comparer);

    /// <summary>
    /// Sorts the elements in the entire <see cref="T:System.Collections.Generic.List`1"/> using the specified <see cref="T:System.Comparison`1"/>.
    /// </summary>
    /// <param name="comparison">The <see cref="T:System.Comparison`1"/> to use when comparing elements.</param><exception cref="T:System.ArgumentNullException"><paramref name="comparison"/> is null.</exception><exception cref="T:System.ArgumentException">The implementation of <paramref name="comparison"/> caused an error during the sort. For example, <paramref name="comparison"/> might not return 0 when comparing an item with itself.</exception>
    public void Sort(Comparison<T> comparison);

    /// <summary>
    /// Copies the elements of the <see cref="T:System.Collections.Generic.List`1"/> to a new array.
    /// </summary>
    /// 
    /// <returns>
    /// An array containing copies of the elements of the <see cref="T:System.Collections.Generic.List`1"/>.
    /// </returns>
    public T[] ToArray();

    /// <summary>
    /// Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Generic.List`1"/>, if that number is less than a threshold value.
    /// </summary>
    public void TrimExcess();

    /// <summary>
    /// Determines whether every element in the <see cref="T:System.Collections.Generic.List`1"/> matches the conditions defined by the specified predicate.
    /// </summary>
    /// 
    /// <returns>
    /// true if every element in the <see cref="T:System.Collections.Generic.List`1"/> matches the conditions defined by the specified predicate; otherwise, false. If the list has no elements, the return value is true.
    /// </returns>
    /// <param name="match">The <see cref="T:System.Predicate`1"/> delegate that defines the conditions to check against the elements.</param><exception cref="T:System.ArgumentNullException"><paramref name="match"/> is null.</exception>
    public bool TrueForAll(Predicate<T> match);

    internal static IList<T> Synchronized(List<T> list);

    [Serializable]
    internal class SynchronizedList : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
      public int Count { get; }

      public bool IsReadOnly { get; }

      public T this[int index] { get; set; }

      internal SynchronizedList(List<T> list);

      public void Add(T item);

      public void Clear();

      public bool Contains(T item);

      public void CopyTo(T[] array, int arrayIndex);

      public bool Remove(T item);

      IEnumerator IEnumerable.GetEnumerator();

      IEnumerator<T> IEnumerable<T>.GetEnumerator();

      public int IndexOf(T item);

      public void Insert(int index, T item);

      public void RemoveAt(int index);
    }

    /// <summary>
    /// Enumerates the elements of a <see cref="T:System.Collections.Generic.List`1"/>.
    /// </summary>
    [Serializable]
    public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
    {
      private List<T> list;
      private int index;
      private int version;
      private T current;

      /// <summary>
      /// Gets the element at the current position of the enumerator.
      /// </summary>
      /// 
      /// <returns>
      /// The element in the <see cref="T:System.Collections.Generic.List`1"/> at the current position of the enumerator.
      /// </returns>
      public T Current { get; }

      object IEnumerator.Current { get; }

      internal Enumerator(List<T> list);

      /// <summary>
      /// Releases all resources used by the <see cref="T:System.Collections.Generic.List`1.Enumerator"/>.
      /// </summary>
      [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
      public void Dispose();

      /// <summary>
      /// Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.List`1"/>.
      /// </summary>
      /// 
      /// <returns>
      /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
      /// </returns>
      /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
      public bool MoveNext();

      void IEnumerator.Reset();
    }
  }
}
