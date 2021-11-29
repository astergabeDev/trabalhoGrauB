using System.IO;

namespace trabalho_grau_b
{
    public class WritingProcess : Process
    {
        public string exp;
        public WritingProcess(int pid, string exp) : base(pid)
        {
            this.exp = exp;
        }
        public override void execute()
        {
            try
            {
                StreamWriter arquivoText;
                arquivoText = File.CreateText("computition.txt");

                arquivoText.WriteLine(exp);

                arquivoText.Close();  
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Deu erro");
            }
            
        }
    }
        
}