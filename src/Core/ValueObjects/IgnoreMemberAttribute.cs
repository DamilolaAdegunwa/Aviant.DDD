namespace Aviant.DDD.Core.ValueObjects
{
    #region

    using System;

    #endregion

    // source: https://github.com/jhewlett/ValueObject
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    { }
}