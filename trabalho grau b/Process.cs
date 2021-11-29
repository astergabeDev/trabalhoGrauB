namespace trabalho_grau_b
{
    public abstract class Process : ProcessoInterface
    {
        protected int pid;
        public abstract void execute();

        public Process(int pid)
        {
            this.pid = pid;
        }
    }
}