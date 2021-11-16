using System;
using System.IO.Ports;
using System.Net.Sockets;

namespace Valtec.scr
{
    public class Connect : SerialPort
    {
        public static bool SerialData { get; }

        private static string[] metrics =
        {
            Commands.Water_Of_Month,
            Commands.Water_Of_Today,
            Commands.Month_Max_insFlow,
            Commands.Water_Of_Today_Start,
            Commands.Water_Of_Start_Month,
            Commands.Water_Temperature,
            Commands.Collected_Active_Forward
        };

        private bool _tcpClientConnected;

        private void ValtecRequest()
        {
            foreach (var command in metrics)
                if (command == "00012500" || command == "006146")
                {
                    var bytes = CRC16.CommandBytes(command);
                    var portName = GetPortNames().GetValue(0)?.ToString();
                    var tcpClient = new TcpClient("127.0.0.1", 502);
                    _tcpClientConnected = tcpClient.Connected;
                    tcpClient.Client.Listen();
                    Console.Out.WriteLine(Events);
                    foreach (var comand in metrics)
                        if (command == "00012500" || command == "006146")
                        {
                            var cmd = comand;
                            cmd = cmd + $"{DateTime.Now:ddMMyy}" + "0";
                            for (var i = 0; i < 5; i++)
                            {
                                cmd = cmd + i.ToString();
                                var enbytes = CRC16.CommandBytes(cmd);
                                tcpClient.Client.Accept();
                                tcpClient.Client.Send(enbytes);
                            }

                            /*else
                            {
                                return;
                            }*/
                        }
                }
        }
    }
}