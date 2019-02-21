using System;
using Capstone;

namespace capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            ProjectCLI cli = new ProjectCLI();
            cli.RunCli();
        }
    }
}
