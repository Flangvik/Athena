namespace Agent.Interfaces
{
    //This interface controls the encryption and decryption of mythic messages
    public interface ICryptoManager
    {
        public abstract string Encrypt(string data);
        public abstract string Decrypt(string data);
        
        // Default implementations for backward compatibility
        // These allow dynamic key/UUID updates during EKE (Encrypted Key Exchange)
        public void UpdateKey(string newKey) { }
        public void UpdateUUID(string newUuid) { }
    }
}
