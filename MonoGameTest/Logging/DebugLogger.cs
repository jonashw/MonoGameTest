using System.Diagnostics;

namespace MonoGameTest.Logging
{
    public class DebugLogger : ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
