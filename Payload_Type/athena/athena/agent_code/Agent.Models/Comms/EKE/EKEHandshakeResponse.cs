using System.Text.Json.Serialization;

namespace Agent.Models
{
    public class EKEHandshakeResponse
    {
        public string action { get; set; }
        public string uuid { get; set; }
        public string session_key { get; set; }
    }
    
    [JsonSerializable(typeof(EKEHandshakeResponse))]
    public partial class EKEHandshakeResponseJsonContext : JsonSerializerContext
    {
    }
}

