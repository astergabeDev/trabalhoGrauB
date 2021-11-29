using System.IO;
namespace trabalho_grau_b
{
    public class ReadingProcess : Process
    {
        
        public ReadingProcess(int pid) : base(pid)
        {
            
        }
        public override void execute()
        {
            string[] cLines = System.IO.File.ReadAllLines("computition.txt");

            ComputingProcess[] computingProcesses = new ComputingProcess[cLines.Length];
            
            for(int i = 0; i < cLines.Length; i++)
            {
                string[] expStr = cLines[i].Split(" ");
                computingProcesses[i] = new ComputingProcess(this.pid,float.Parse(expStr[0]),float.Parse(expStr[2]),char.Parse(expStr[1]));
                computingProcesses[i].execute();
            }
            StreamWriter arquivoText;
            arquivoText = File.CreateText("computition.txt");
            for (int i = 0; i < cLines.Length; i++)
            {
                arquivoText.WriteLine();
            }
            arquivoText.Close();
        }
    }
}