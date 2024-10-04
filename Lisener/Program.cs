using Lisener;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

var ip = IPAddress.Parse("127.0.0.1");
var port = 27001;

var lisener = new TcpListener(ip, port);
lisener.Start();

while (true)
{
    var client = lisener.AcceptTcpClient();

    var stream = client.GetStream();

    var br = new BinaryReader(stream);

    var bw = new BinaryWriter(stream);


    while (true)
    {
        var input = br.ReadString();
        var command = JsonSerializer.Deserialize<Command>(input);

        Console.WriteLine(command.Text);
        Console.WriteLine(command.Param);
        switch (command.Text)
        {
            case Command.processList:
                var processes = Process.GetProcesses();
                var processsNames = JsonSerializer.Serialize(processes.Select(p => p.ProcessName));
                bw.Write(processsNames);
                break;


            case Command.run:
                bw.Write(JsonSerializer.Serialize(command.Param));
                break;

            case Command.kill:
                bw.Write(JsonSerializer.Serialize(command.Param));
                break;

            default:
                break;
        }
    }
}