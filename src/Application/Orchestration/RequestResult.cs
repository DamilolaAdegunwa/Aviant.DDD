namespace Aviant.DDD.Application.Orchestration
{
    #region

    using System;
    using System.Collections.Generic;

    #endregion

    public class RequestResult
    {
        private readonly object? _payload;

        public RequestResult()
        { }

        public RequestResult(object? payload)
        {
            _payload  = payload;
            Succeeded = true;
        }

        public RequestResult(object? payload, int? affectedRows)
            : this(payload) => AffectedRows = affectedRows;

        public RequestResult(List<string> messages)
        {
            Messages  = messages;
            Succeeded = false;
        }

        public bool Succeeded { get; set; }

        public List<string> Messages { get; set; } = new List<string>();

        private int? AffectedRows { get; }

        public object? Payload() => _payload;

        public T Payload<T>()
        {
            if (_payload is null)
                throw new Exception("Payload is null");

            if (typeof(T) != _payload.GetType())
                throw new Exception(
                    string.Format(
                        "Type \"{0}\" does not much payload type \"{1}\"",
                        typeof(T).FullName,
                        _payload.GetType().FullName));

            return (T) _payload;
        }
    }
}