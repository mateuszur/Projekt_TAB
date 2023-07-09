using System.IO;

namespace Gym_Aplication
{

    class ParametryFileManager
    {
        private string filePath;

        public ParametryFileManager()
        {
            this.filePath = ".\\DataBaseConnection\\ConnectionString.txt";
        }
        public void ZapiszParametry(string parametry)
        {
            File.WriteAllText(filePath, parametry);
        }

        public string OdczytajParametry()
        {
            return File.ReadAllText(filePath);
        }

    }
}
