namespace spacexinterop.api._Common._Configs;

public class EncryptionKeyConfig
{
    public Dictionary<string, string> Keys { get; set; } = new(); 
    public string CurrentKeyId { get; set; } = "0";               
}