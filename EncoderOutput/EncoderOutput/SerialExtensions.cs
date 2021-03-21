using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderOutput
{
    public static class SerialExtensions
    {
        public static int ReadShort(this SerialPort port)
        {
            var first = (byte)port.ReadByte();
            var second = (byte)port.ReadByte();
            return BitConverter.ToInt16(new[] { second, first }, 0);
        }
    }
}
