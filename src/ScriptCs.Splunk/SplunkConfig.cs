namespace Splunk.Client.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class SplunkConfig
    {
        public SplunkConfig()
        {
            // set everything to the default
            Scheme = Scheme.Https;
            Host = "localhost";
            Port = 8089;
            Username = "admin";
            Password = "changeme";
        }

        /// <summary>
        /// The scheme
        /// </summary>
        public Scheme Scheme { get; internal set; } 

        /// <summary>
        /// The host
        /// </summary>
        public string Host { get; internal set; }

        /// <summary>
        /// The port
        /// </summary>
        public int Port { get; internal set; }

        /// <summary>
        /// The username
        /// </summary>
        public string Username { get; internal set; }

        /// <summary>
        /// The password
        /// </summary>
        public string Password { get; internal set; }

    }
}
