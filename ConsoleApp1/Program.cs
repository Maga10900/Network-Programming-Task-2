using ConsoleApp1;
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
            command = new Command() { Text = input[1] };
            bw.Write(JsonSerializer.Serialize(command));
            responce = br.ReadString();
            var processList = JsonSerializer.Deserialize<string[]>(responce);
            foreach (var proc in processList)
            {
                Console.WriteLine($"            {proc}");
            }
            break;

        case Command.run:
            break;

        case Command.kill:
            break;
        default:
            break;
    }
}