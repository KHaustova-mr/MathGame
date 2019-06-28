using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MathGame
{
    class Class_Attempt
    {
        public int attempt;
        private static string path;
        private string today_date;
        private StreamReader file_for_read;
        private StreamWriter file_for_write;

        public Class_Attempt()
        {
                       
        }
        /// <summary>
        /// Вычисляем сегодняшнюю попытку
        /// </summary>
        /// <param name="p">Путь к файлу с попытками</param>
        public int Attempt(string p)
        {
            path = p;
            today_date = DateTime.Today.ToString();
            file_for_read = new StreamReader(path);
            string date = file_for_read.ReadLine();
            attempt = Convert.ToInt32(file_for_read.ReadLine());
            file_for_read.Close();
            File.WriteAllText(path, string.Empty);
            file_for_write = new StreamWriter(path);
            file_for_write.WriteLine(DateTime.Today.ToString());
            if (today_date == date)
            {
                attempt++;               
                file_for_write.WriteLine(attempt);
            }
            else
            {
                attempt = 1;
                file_for_write.WriteLine(0);
            }
            file_for_write.Close();
            return attempt;
        }
    }
}
