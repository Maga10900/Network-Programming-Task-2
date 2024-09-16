﻿namespace ConsoleApp1;

public class Command
{
    public const string processList = "PROCLIST";
    public const string kill = "KILL";
    public const string run = "RUN";

    public string? Text { get; set; }
    public string? Param { get; set; }
}
