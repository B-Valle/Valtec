using System.IO.Ports;

namespace Valtec.scr
{
    public class Connect
    {
        public static void OpenCOMPort()
        {
            var comPort = new SerialPort();
            comPort.PortName = "COM4";
            comPort.BaudRate = 9600;
            comPort.DataBits = 8;
            comPort.Parity = Parity.None;
            comPort.StopBits = StopBits.One;
            comPort.Open();
            comPort.Close();
        }
    }
}