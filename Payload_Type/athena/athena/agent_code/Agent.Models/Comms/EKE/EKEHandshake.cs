using System.Text.Json.Serialization;

namespace Agent.Models
{
    public class EKEHandshake
    {
        public string action { get; set; } = "staging_rsa";
        public string pub_key { get; set; }
        public string session_id { get; set; }
    }
    
    [JsonSerializable(typeof(EKEHandshake))]
    public partial class EKEHandshakeJsonContext : JsonSerializerContext
    {
    }
}

