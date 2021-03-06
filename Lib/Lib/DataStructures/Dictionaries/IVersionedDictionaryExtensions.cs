﻿/*
 * IVersionedDictionaryExtensions
 * (c) Achim Friedland, 2009 - 2010
 */

#region Usings

using System;
using System.Collections.Generic;

using sones.Lib.DataStructures;

#endregion

namespace sones.Lib.DataStructures.Dictionaries
{

    /// <summary>
    /// Extensions to the IVersionedDictionaryInterface interface
    /// </summary>

    public static class IVersionedDictionaryExtensions
    {


        #region ContainsKeys(myKeys, myVersion)

        public static Trinary ContainsKeys<TKey, TValue>(this IVersionedDictionaryInterface<TKey, TValue> myIVersionedDictionaryInterface, IEnumerable<TKey> myKeys, Int64 myVersion)
            where TKey : IComparable
        {

            var _Success = Trinary.TRUE;

            foreach (var _Key in myKeys)
                _Success &= myIVersionedDictionaryInterface.ContainsKey(_Key, myVersion);

            return _Success;

        }

        #endregion

        #region ContainsValues(myValues, myVersion)

        public static Trinary ContainsValues<TKey, TValue>(this IVersionedDictionaryInterface<TKey, TValue> myIVersionedDictionaryInterface, IEnumerable<TValue> myValues, Int64 myVersion)
            where TKey : IComparable
        {

            var _Success = Trinary.TRUE;

            foreach (var _Value in myValues)
                _Success &= myIVersionedDictionaryInterface.ContainsValue(_Value, myVersion);

            return _Success;

        }

        #endregion

        #region Contains(myKeyValuePair, myVersion)

        public static Trinary Contains<TKey, TValue>(this IVersionedDictionaryInterface<TKey, TValue> myIVersionedDictionaryInterface, KeyValuePair<TKey, TValue> myKeyValuePair, Int64 myVersion)
            where TKey : IComparable
        {
            return myIVersionedDictionaryInterface.Contains(myKeyValuePair.Key, myKeyValuePair.Value, myVersion);
        }

        #endregion

        #region Contains(myDictionary, myVersion)

        public static Trinary Contains<TKey, TValue>(this IVersionedDictionaryInterface<TKey, TValue> myIVersionedDictionaryInterface, Dictionary<TKey, TValue> myDictionary, Int64 myVersion)
            where TKey : IComparable
        {

            var _Success = Trinary.TRUE;

            foreach (var _KeyValuePair in myDictionary)
                _Success &= myIVersionedDictionaryInterface.Contains(_KeyValuePair, myVersion);

            return _Success;

        }

        #endregion


        #region Keys(myMinKey, myMaxKey, myVersion)

        public static IEnumerable<TKey> Keys<TKey, TValue>(this IVersionedDictionaryInterface<TKey, TValue> myIVersionedDictionaryInterface, TKey myMinKey, TKey myMaxKey, Int64 myVersion)
            where TKey : IComparable
        {
            return myIVersionedDictionaryInterface.Keys(item => item.Key.CompareTo(myMinKey) >= 0 && item.Key.CompareTo(myMaxKey) <= 0, myVersion);
        }

        #endregion

        #region KeyCount(myMinKey, myMaxKey, myVersion)

        public static UInt64 KeyCount<TKey, TValue>(this IVersionedDictionaryInterface<TKey, TValue> myIVersionedDictionaryInterface, TKey myMinKey, TKey myMaxKey, Int64 myVersion)
            where TKey : IComparable
        {
            return myIVersionedDictionaryInterface.KeyCount(item => item.Key.CompareTo(myMinKey) >= 0 && item.Key.CompareTo(myMaxKey) <= 0, myVersion);
        }

        #endregion

        #region Values(myMinKey, myMaxKey, myVersion)

        public static IEnumerable<TValue> Values<TKey, TValue>(this IVersionedDictionaryInterface<TKey, TValue> myIVersionedDictionaryInterface, TKey myMinKey, TKey myMaxKey, Int64 myVersion)
            where TKey : IComparable
        {
            return myIVersionedDictionaryInterface.Values(item => item.Key.CompareTo(myMinKey) >= 0 && item.Key.CompareTo(myMaxKey) <= 0, myVersion);
        }

        #endregion

        #region ValueCount(myMinKey, myMaxKey, myVersion)

        public static UInt64 ValueCount<TKey, TValue>(this IVersionedDictionaryInterface<TKey, TValue> myIVersionedDictionaryInterface, TKey myMinKey, TKey myMaxKey, Int64 myVersion)
            where TKey : IComparable
        {
            return myIVersionedDictionaryInterface.ValueCount(item => item.Key.CompareTo(myMinKey) >= 0 && item.Key.CompareTo(myMaxKey) <= 0, myVersion);
        }

        #endregion

        #region GetDictionary(myMinKey, myMaxKey, myVersion)

        public static IDictionary<TKey, TValue> GetIDictionary<TKey, TValue>(this IVersionedDictionaryInterface<TKey, TValue> myIVersionedDictionaryInterface, TKey myMinKey, TKey myMaxKey, Int64 myVersion)
            where TKey : IComparable
        {
            return myIVersionedDictionaryInterface.GetIDictionary(item => item.Key.CompareTo(myMinKey) >= 0 && item.Key.CompareTo(myMaxKey) <= 0, myVersion);
        }

        #endregion

        #region GetEnumerator(myMinKey, myMaxKey, myVersion)

        public static IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator<TKey, TValue>(this IVersionedDictionaryInterface<TKey, TValue> myIVersionedDictionaryInterface, TKey myMinKey, TKey myMaxKey, Int64 myVersion)
            where TKey : IComparable
        {
            return myIVersionedDictionaryInterface.GetEnumerator(item => item.Key.CompareTo(myMinKey) >= 0 && item.Key.CompareTo(myMaxKey) <= 0, myVersion);
        }

        #endregion


    }

}