using ConsoleApp1;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

var ip = IPAddress.Loopback;
var port = 27001;
var clinet = new TcpClient();
clinet.Connect(ip, port);

var stream = clinet.GetStream();

var br = new BinaryReader(stream);
var bw = new BinaryWriter(stream);

Command command = null;

string responce = null;
string str = null;

while (true)
{
    Console.WriteLine("Write any command or HELP");
    str = Console.ReadLine()!.ToUpper();
    if (str == "HELP")
    {
        Console.WriteLine();
        Console.WriteLine("Command List");
        Console.WriteLine(Command.processList);
        Console.WriteLine($"{Command.run} <process_name>");
        Console.WriteLine($"{Command.kill} <process_name>");
        Console.WriteLine($"HELP");
        Console.ReadLine();
        Console.Clear();
    }
    var input = str.Split(' ');
    switch (input[0])
    {
        case Command.processList:
            command = new Command() { Text = input[0] };
            bw.Write(JsonSerializer.Serialize(command));
            responce = br.ReadString();
            var processList = JsonSerializer.Deserialize<string[]>(responce);
            foreach (var proc in processList)
            {
                Console.WriteLine($"            {proc}");
            }
            break;

        case Command.run:
            command = new Command() { Text = input[0],Param = input[1] };
            bw.Write(JsonSerializer.Serialize(command));
            responce = br.ReadString();
            string pr = JsonSerializer.Deserialize<string>(responce);
            Process.Start(pr);
            break;

        case Command.kill:
            command = new Command() { Text = input[0], Param = input[1] };
            bw.Write(JsonSerializer.Serialize(command));
            responce = br.ReadString();
            string fm = JsonSerializer.Deserialize<string>(responce);
            foreach (var item in Process.GetProcesses())
            {
                if(fm.ToLower() == item.ProcessName.ToLower())
                {
                    item.Kill();
                }
            };
            break;
        default:
            break;
    }
}