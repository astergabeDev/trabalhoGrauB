using System.Collections;
using System.Collections.Generic;

namespace trabalho_grau_b
{
    public class PrintingProcess : Process
    {
        Queue<QueueItem> pool;
        public PrintingProcess(int pid, Queue<QueueItem> pool) : base(pid)
        {
            this.pool = pool;
        }
        public override void execute()
        {
            foreach (QueueItem processo in pool)
            {
                System.Console.WriteLine($"PID: {processo.pid} ({processo.tipoProc})");
            }
        } 
    }
}