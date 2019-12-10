using System;
using System.ComponentModel;

namespace SoftCube.Asserts
{
    /// <summary>
    /// アサート。
    /// </summary>
    public static partial class Assert
    {
        /// <summary>Do not call this method.</summary>
        [Obsolete("This is an override of Object.Equals(). Call Assert.Equal() instead.", true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new static bool Equals(object a, object b)
        {
            throw new InvalidOperationException("Assert.Equals should not be used");
        }

        /// <summary>Do not call this method.</summary>
        [Obsolete("This is an override of Object.ReferenceEquals(). Call Assert.Same() instead.", true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new static bool ReferenceEquals(object a, object b)
        {
            throw new InvalidOperationException("Assert.ReferenceEquals should not be used");
        }
    }
}
