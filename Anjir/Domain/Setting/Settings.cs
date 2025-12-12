namespace Domain.Setting;

public static class Settings
{
    public static class SmsConfig
    {
        public static string Originator { get; set; } = null!;
        public static string Login { get; set; } = null!;
        public static string Password { get; set; } = null!;
        public static string SendUrl { get; set; } = null!;
    }
    public static HashSet<string> SearchEngineBots { get; set; } = null!;
    public static int IpBlockTimeMinutes { get; set; } = 60;
    public static HashSet<string> TruistIpSet { get; set; } = null!;
}
