//using System;

//namespace Xunit.Sdk
//{
//    /// <summary>
//    /// Exception thrown when the value is unexpectedly of the exact given type.
//    /// </summary>
//#if XUNIT_VISIBILITY_INTERNAL
//    internal
//#else
//    public
//#endif
//    class IsNotTypeException : AssertActualExpectedException
//    {
//        /// <summary>
//        /// Creates a new instance of the <see cref="IsNotTypeException"/> class.
//        /// </summary>
//        /// <param name="expected">The expected type</param>
//        /// <param name="actual">The actual object value</param>
//        public IsNotTypeException(Type expected, object actual)
//            : base(expected, actual == null ? null : actual.GetType(), "Assert.IsNotType() Failure")
//        { }
//    }
//}