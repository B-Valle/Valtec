using System;

namespace Valtec.scr
{
    public static class CRC16
    {
        private static string CRC16ModBus(string cmd)
        {
            var buffer = ToBytes(cmd);

            ushort crc = 0xFFFF;

            for (var pos = 0; pos < buffer.Length; pos++)
            {
                crc ^= (ushort) buffer[pos];

                for (var i = 8; i != 0; i--)
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
            }

            var crc16 = (ushort) ((crc >> 8) | (crc << 8));
            return crc16.ToString("X4");
        }

        private static byte[] ToBytes(string text)
        {
            var bytes = new byte[text.Length / 2];
            for (var i = 0; i < text.Length; i += 2) bytes[i / 2] = Convert.ToByte(text.Substring(i, 2), 16);

            return bytes;
        }

        public static byte[] CommandBytes(string cmd)
        {
            var command = cmd + CRC16ModBus(cmd);
            return ToBytes(command);
        }
    }
}