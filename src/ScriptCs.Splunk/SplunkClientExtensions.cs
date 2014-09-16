using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splunk.Client;

namespace ScriptCs.Splunk
{
    public static class SplunkClientExtensions
    {
        public static async void SendEventAsync(this Service service, string splunkEvent, string index = null, TransmitterArgs args=null)
        {
            await service.Transmitter.SendAsync(splunkEvent, index, args);
        }

        public static async void StreamEventsAsync(this Service service, IEnumerable<string> splunkEvents,
            string index = null, TransmitterArgs args = null)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            foreach (var splunkEvent in splunkEvents)
            {
                await writer.WriteLineAsync(splunkEvent);
                await writer.FlushAsync();
            }
            stream.Position = 0;
            service.Transmitter.SendAsync(stream, index, args);
        }
    }
}
