using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;

namespace QUETE_MEF_PlusieursServices
{
    public class Program
    {
        [ImportMany(typeof(ILogger))]
        private Logger _logger;

        static void Main(string[] args)
        {
            Host host = new Host();
            host.Run();

            Console.Read();
        } 
    }

    internal class Host
    {

        [ImportMany(typeof(ILogger))]
        protected IEnumerable<ILogger> _logger = null;

        public void Run()
        {
            var container = new CompositionContainer();
            container.ComposeParts(this, new Logger(), new LoggerTxt());
            foreach (var logger in _logger)
            {
                logger.Write(" World !");
            }
        }
    }

    [Export(typeof(ILogger))]
    internal class LoggerTxt : ILogger
    {
        public void Write(string message)
        {
            TextWriter writeFile = new StreamWriter("textwriter.txt");
            writeFile.WriteLine("Hello " + message + " this is a file text !");
            writeFile.Flush();
            writeFile.Close();

        }
    }

    [Export(typeof(ILogger))]
    internal class Logger : ILogger
    {
        public void Write(string message)
        {
            Console.WriteLine("Hello" + message);
        }
    }
}
