using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SoftCube.Asserts
{
    /// <summary>
    /// アサート用のデフォルト同値比較。
    /// </summary>
    /// <typeparam name="T">比較対象のオブジェクトの型</typeparam>
    internal class AssertEqualityComparer<T> : IEqualityComparer<T>
    {
        #region 定数

        /// <summary>
        /// デフォルトの要素の同値比較。
        /// </summary>
        private static readonly IEqualityComparer DefaultElementEqualityComparer = new AssertEqualityComparerAdapter<object>(new AssertEqualityComparer<object>());

        /// <summary>
        /// Nullable<>の型情報。
        /// </summary>
        private static readonly TypeInfo NullableTypeInfo = typeof(Nullable<>).GetTypeInfo();

        #endregion

        #region プロパティ

        /// <summary>
        /// 要素の同値比較ファクトリー。
        /// </summary>
        /// <remarks>
        /// 要素の同値比較は、比較対象のオブジェクトが反復子である場合、各要素の同値比較に使用される。
        /// </remarks>
        private Func<IEqualityComparer> ElementEqualityComparerFactory { get; }

        /// <summary>
        /// CompareTypedSetsのメソッド情報。
        /// </summary>
        private MethodInfo CompareTypedSetsMethod
        {
            get
            {
                if (compareTypedSetsMethod is null)
                {
                    compareTypedSetsMethod = GetType().GetTypeInfo().GetDeclaredMethod(nameof(CompareTypedSets));
                }
                return compareTypedSetsMethod;
            }
        }
        private static MethodInfo compareTypedSetsMethod;

        #endregion

        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="elementEaualityComparer">要素の同値比較（比較対象のオブジェクトが反復子である場合、各要素の同値比較に使用される）</param>
        public AssertEqualityComparer(IEqualityComparer elementEaualityComparer = null)
        {
            ElementEqualityComparerFactory = () => elementEaualityComparer ?? DefaultElementEqualityComparer;
        }

        #endregion

        #region メソッド

        #region 同値比較

        /// <summary>
        /// 指定したオブジェクトが等しいかを判断する。
        /// </summary>
        /// <param name="x">比較対象のオブジェクト</param>
        /// <param name="y">比較対象のオブジェクト</param>
        /// <returns>指定したオブジェクトが等しいか</returns>
        public bool Equals(T x, T y)
        {
            var typeInfo = typeof(T).GetTypeInfo();

            // Nullableである場合、null比較による判断を試みる。
            if (!typeInfo.IsValueType || (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition().GetTypeInfo().IsAssignableFrom(NullableTypeInfo)))
            {
                if (object.Equals(x, default(T)))
                {
                    return object.Equals(y, default(T));
                }

                if (object.Equals(y, default(T)))
                {
                    return false;
                }
            }

            // IEquatable<T>である場合、IEquatable<T>.Equalsで判断する。
            if (x is IEquatable<T> equatable)
            {
                return equatable.Equals(y);
            }

            // IComparable<T>である場合、IComparable<T>.CompareToで判断する。
            if (x is IComparable<T> comparableGeneric)
            {
                try
                {
                    return comparableGeneric.CompareTo(y) == 0;
                }
                catch
                {
                    // IComparable<T>.CompareToは、状況によって例外を投げる可能性がある。
                    // 例外が投げられた場合、例外を無視して比較を続ける。
                }
            }

            // IComparableである場合、IComparable<T>.CompareToで判断する。
            if (x is IComparable comparable)
            {
                try
                {
                    return comparable.CompareTo(y) == 0;
                }
                catch
                {
                    // IComparable.CompareToは、状況によって例外を投げる可能性がある。
                    // 例外が投げられた場合、例外を無視して比較を続ける。
                }
            }

            // 辞書である場合、各要素が等しいかを判断する。
            var dictionariesEqual = CheckIfDictionariesAreEqual(x, y);
            if (dictionariesEqual.HasValue)
            {
                return dictionariesEqual.GetValueOrDefault();
            }

            // 集合である場合、各要素が等しいかを判断する。
            var setsEqual = CheckIfSetsAreEqual(x, y, typeInfo);
            if (setsEqual.HasValue)
            {
                return setsEqual.GetValueOrDefault();
            }

            // 反復子である場合、各要素が等しいかを判断する。
            var enumerablesEqual = CheckIfEnumerablesAreEqual(x, y);
            if (enumerablesEqual.HasValue)
            {
                return enumerablesEqual.GetValueOrDefault();
            }

            // IStructuralEquatableである場合、IStructuralEquatable.Equalsで判断する。
            if (x is IStructuralEquatable structuralEquatable && structuralEquatable.Equals(y, new StructuralEqualityComparer(ElementEqualityComparerFactory())))
            {
                return true;
            }

            // IEquatable<typeof(y)>である場合、IEquatable<typeof(y)>.Equalsで判断する。
            var iequatableY = typeof(IEquatable<>).MakeGenericType(y.GetType()).GetTypeInfo();
            if (iequatableY.IsAssignableFrom(x.GetType().GetTypeInfo()))
            {
                var equalsMethod = iequatableY.GetDeclaredMethod(nameof(IEquatable<T>.Equals));
                return (bool)equalsMethod.Invoke(x, new object[] { y });
            }

            // IComparable<typeof(y)>である場合、IComparable<typeof(y)>.Equalsで判断する。
            var icomparableY = typeof(IComparable<>).MakeGenericType(y.GetType()).GetTypeInfo();
            if (icomparableY.IsAssignableFrom(x.GetType().GetTypeInfo()))
            {
                var compareToMethod = icomparableY.GetDeclaredMethod(nameof(IComparable<T>.CompareTo));
                try
                {
                    return (int)compareToMethod.Invoke(x, new object[] { y }) == 0;
                }
                catch
                {
                    // IComparable<typeof(y)>.CompareToは、状況によって例外を投げる可能性がある。
                    // 例外が投げられた場合、例外を無視して比較を続ける。
                }
            }

            // 上記のいずれでもない場合、object.Equalsで判断する。
            return object.Equals(x, y);
        }

        #region 反復子の比較

        /// <summary>
        /// 指定したオブジェクトが反復子である場合、等しいかを判断する。
        /// </summary>
        /// <param name="x">比較対象のオブジェクト</param>
        /// <param name="y">比較対象のオブジェクト</param>
        /// <returns>指定したオブジェクトが反復子である場合、等しいか（反復子ではない場合、null）</returns>
        bool? CheckIfEnumerablesAreEqual(T x, T y)
        {
            if (x is IEnumerable enumerableX && y is IEnumerable enumerableY)
            {
                IEnumerator enumeratorX = null;
                IEnumerator enumeratorY = null;
                try
                {
                    enumeratorX = enumerableX.GetEnumerator();
                    enumeratorY = enumerableY.GetEnumerator();
                    var equalityComparer = ElementEqualityComparerFactory();

                    while (true)
                    {
                        var hasNextX = enumeratorX.MoveNext();
                        var hasNextY = enumeratorY.MoveNext();

                        if (!hasNextX || !hasNextY)
                        {
                            if (hasNextX == hasNextY)
                            {
                                // Array.GetEnumerator()は配列を平坦化し、配列の次元と長さを無視する。
                                // 配列の次元と長さが等しいかを判断する。
                                if (enumerableX is Array xArray && enumerableY is Array yArray)
                                {
                                    // new object[2,1] != new object[2]
                                    if (xArray.Rank != yArray.Rank)
                                    {
                                        return false;
                                    }

                                    // new object[2,1] != new object[1,2]
                                    for (int i = 0; i < xArray.Rank; i++)
                                    {
                                        if (xArray.GetLength(i) != yArray.GetLength(i))
                                        {
                                            return false;
                                        }
                                    }
                                }
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }

                        if (!equalityComparer.Equals(enumeratorX.Current, enumeratorY.Current))
                        {
                            return false;
                        }
                    }
                }
                finally
                {
                    if (enumeratorX is IDisposable disposableX)
                    {
                        disposableX.Dispose();
                    }

                    if (enumeratorY is IDisposable disposableY)
                    {
                        disposableY.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region 辞書の比較

        /// <summary>
        /// 指定したオブジェクトが辞書である場合、等しいかを判断する。
        /// </summary>
        /// <param name="x">比較対象のオブジェクト</param>
        /// <param name="y">比較対象のオブジェクト</param>
        /// <returns>指定したオブジェクトが辞書である場合、等しいか（辞書ではない場合、null）</returns>
        bool? CheckIfDictionariesAreEqual(T x, T y)
        {
            if (x is IDictionary dictionaryX && y is IDictionary dictionaryY)
            {
                if (dictionaryX.Count != dictionaryY.Count)
                {
                    return false;
                }

                var equalityComparer = ElementEqualityComparerFactory();
                var dictionaryYKeys = new HashSet<object>(dictionaryY.Keys.Cast<object>());

                foreach (var key in dictionaryX.Keys)
                {
                    if (!dictionaryYKeys.Contains(key))
                    {
                        return false;
                    }

                    var valueX = dictionaryX[key];
                    var valueY = dictionaryY[key];

                    if (!equalityComparer.Equals(valueX, valueY))
                    {
                        return false;
                    }

                    dictionaryYKeys.Remove(key);
                }

                return dictionaryYKeys.Count == 0;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region 集合の比較

        /// <summary>
        /// 指定したオブジェクトが集合である場合、等しいかを判断する。
        /// </summary>
        /// <param name="x">比較対象のオブジェクト</param>
        /// <param name="y">比較対象のオブジェクト</param>
        /// <param name="typeInfo">Tの型情報</param>
        /// <returns>指定したオブジェクトが集合である場合、等しいか（集合ではない場合、null）</returns>
        bool? CheckIfSetsAreEqual(T x, T y, TypeInfo typeInfo)
        {
            if (!IsSet(typeInfo))
            {
                return null;
            }

            if (x is IEnumerable enumX && y is IEnumerable enumY)
            {
                Type elementType;
                if (typeof(T).GenericTypeArguments.Length != 1)
                {
                    elementType = typeof(object);
                }
                else
                {
                    elementType = typeof(T).GenericTypeArguments[0];
                }

                var method = CompareTypedSetsMethod.MakeGenericMethod(new Type[] { elementType });
                return (bool)method.Invoke(this, new object[] { enumX, enumY });
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 指定した反復子（集合）が等しいかを判断する。
        /// </summary>
        /// <typeparam name="TElement">反復子（集合）の要素の型</typeparam>
        /// <param name="enumX">比較対象の反復子</param>
        /// <param name="enumY">比較対象の反復子</param>
        /// <returns>指定した反復子（集合）が等しいか</returns>
        bool CompareTypedSets<TElement>(IEnumerable enumX, IEnumerable enumY)
        {
            var setX = new HashSet<TElement>(enumX.Cast<TElement>());
            var setY = new HashSet<TElement>(enumY.Cast<TElement>());
            return setX.SetEquals(setY);
        }

        /// <summary>
        /// 指定した型情報が集合か、を判断する。
        /// </summary>
        /// <param name="typeInfo">型情報</param>
        /// <returns>指定した型情報が集合か</returns>
        bool IsSet(TypeInfo typeInfo)
        {
            return typeInfo.ImplementedInterfaces
                .Select(i => i.GetTypeInfo())
                .Where(ti => ti.IsGenericType)
                .Select(ti => ti.GetGenericTypeDefinition())
                .Contains(typeof(ISet<>).GetGenericTypeDefinition());
        }

        #endregion

        #endregion

        #region ハッシュコード

        /// <summary>
        /// ハッシュコードを取得する。
        /// </summary>
        /// <param name="obj">比較対象のオブジェクト</param>
        /// <returns>ハッシュコード</returns>
        /// <remarks>
        /// このクラスはGetHashCodeを実装しない。
        /// このクラスをハッシュコンテナーに使用しないこと。
        /// </remarks>
        public int GetHashCode(T obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region 内部クラス

        /// <summary>
        /// IStructuralEquatable用の同値比較。
        /// </summary>
        private class StructuralEqualityComparer : IEqualityComparer
        {
            #region プロパティ

            /// <summary>
            /// 要素の同値比較。
            /// </summary>
            /// <remarks>
            /// 要素の同値比較は、比較対象のオブジェクトが反復子である場合、各要素の同値比較に使用される。
            /// </remarks>
            private IEqualityComparer ElementEqualityComparer { get; }

            /// <summary>
            /// EqualsGenericのメソッド情報。
            /// </summary>
            private MethodInfo EqualsGenericMethod
            {
                get
                {
                    if (equalsGenericMethod == null)
                    {
                        equalsGenericMethod = typeof(StructuralEqualityComparer).GetTypeInfo().GetDeclaredMethod(nameof(EqualsGeneric));
                    }

                    return equalsGenericMethod;
                }
            }
            private static MethodInfo equalsGenericMethod;

            #endregion

            #region コンストラクター

            /// <summary>
            /// コンストラクター。
            /// </summary>
            /// <param name="elementEqualityComparer">要素の同値比較（比較対象のオブジェクトが反復子である場合、各要素の同値比較に使用される）</param>
            public StructuralEqualityComparer(IEqualityComparer elementEqualityComparer)
            {
                ElementEqualityComparer = elementEqualityComparer;
            }

            #endregion

            #region メソッド

            #region 同値比較

            /// <summary>
            /// 指定したオブジェクトが等しいかを判断する。
            /// </summary>
            /// <param name="x">比較対象のオブジェクト</param>
            /// <param name="y">比較対象のオブジェクト</param>
            /// <returns>指定したオブジェクトが等しいか</returns>
            public new bool Equals(object x, object y)
            {
                if (x == null)
                {
                    return y == null;
                }
                if (y == null)
                {
                    return false;
                }

                // AssertEqualityComparerから最高の結果を得るために、比較対象のオブジェクトの型を特定する。
                // 比較対象のオブジェクトの型が同じではない場合、System.Objectであると想定する。
                // これはインターフェイスなどを共有しているかどうかを確認しようとするC＃コンパイラよりも単純だが、
                // AssertEqualityComparer<System.Object>が十分に賢いため、ここではやりすぎになる可能性がある。
                var objectType = x.GetType() == y.GetType() ? x.GetType() : typeof(object);
                return (bool)EqualsGenericMethod.MakeGenericMethod(objectType).Invoke(this, new object[] { x, y });
            }

            /// <summary>
            /// 指定したオブジェクトが等しいかを判断する。
            /// </summary>
            /// <typeparam name="U"></typeparam>
            /// <param name="x">比較対象のオブジェクト</param>
            /// <param name="y">比較対象のオブジェクト</param>
            /// <returns>指定したオブジェクトが等しいか</returns>
            private bool EqualsGeneric<U>(U x, U y)
            {
                return new AssertEqualityComparer<U>(ElementEqualityComparer).Equals(x, y);
            }

            #endregion

            #region ハッシュコード

            /// <summary>
            /// ハッシュコードを取得する。
            /// </summary>
            /// <param name="obj">比較対象のオブジェクト</param>
            /// <returns>ハッシュコード</returns>
            /// <remarks>
            /// このクラスはGetHashCodeを実装しない。
            /// このクラスをハッシュコンテナーに使用しないこと。
            /// </remarks>
            public int GetHashCode(object obj)
            {
                throw new NotImplementedException();
            }

            #endregion

            #endregion
        }

        #endregion
    }
}