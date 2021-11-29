namespace trabalho_grau_b
{
    public class QueueItem
    {
        public string tipoProc;
        public int pid;
        public dynamic process;

        public QueueItem(string tipoProc, int pid, dynamic process)
        {
            this.tipoProc = tipoProc;
            this.pid = pid;
            this.process = process;
        }
    }
}