namespace Aviant.DDD.Application.Services
{
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <inheritdoc />
    /// <summary>
    ///     https://www.mking.net/blog/working-with-private-setters-in-json-net
    /// </summary>
    internal sealed class PrivateSetterContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);

            if (jsonProperty.Writable)
                return jsonProperty;

            if (member is PropertyInfo propertyInfo)
            {
                var setter = propertyInfo.GetSetMethod(true);
                jsonProperty.Writable = setter != null;
            }

            return jsonProperty;
        }
    }
}