namespace Aviant.DDD.Core.TransferObjects
{
    using Newtonsoft.Json;

    public interface ITransferObject
    {
        public string ToJson();

        public string ToJson(Formatting formatting);

        public string ToJson(JsonSerializerSettings settings);

        public string ToJson(Formatting formatting, JsonSerializerSettings settings);
    }
}