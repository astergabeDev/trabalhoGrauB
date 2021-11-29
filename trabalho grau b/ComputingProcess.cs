namespace trabalho_grau_b
{
    public class ComputingProcess : Process
    {
        public float op1;
        public float op2;
        public char operacao;
        float result;
        public ComputingProcess(int pid, float op1, float op2, char operacao) : base(pid)
        {
            this.op1 = op1;
            this.op2 = op2;
            this.operacao = operacao;
        }
        public override void execute()
        {
            switch (operacao)
            {
                case '+':
                    result = op1 + op2;
                    break;
                case '-':
                    result = op1 - op2;
                    break;
                case '*':
                    result = op1 * op2;
                    break;
                case '/':
                    if(op2 != 0)
                    {
                        result = op1 / op2;
                    }
                    break;
                default:
                    break;
            }
            System.Console.WriteLine($"Processo PID: {pid} teve o resultado: {result}");
        }

    }
}