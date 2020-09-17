namespace Aviant.DDD.Core.ValueObjects
{
    using System;

    // source: https://github.com/jhewlett/ValueObject
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    { }
}