using System;

namespace GtkSharp3.Sandbox.Services
{
    public class TestService : ITestService
    {
        public void Print()
        {
            Console.WriteLine("TestService");
        }
    }
}
