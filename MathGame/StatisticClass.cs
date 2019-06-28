using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
{
    class StatisticClass
    {
        public List<string> data_from_file;
        private StreamReader file_forRead;
        private StreamWriter file_for_write;

        public StatisticClass()
        {
            
        }
        /// <summary>
        /// Открыть статистику для чтения
        /// </summary>
        /// <param name="p">Путь к файлу со статистикой</param>
        public List<string> Conclusion(string p)
        {
            data_from_file = new List<string>();
            file_forRead = new StreamReader(p);            
            int size = File.ReadAllLines(p).Length;
            for (int i = 0; i < size; i++)
            {
                string s = file_forRead.ReadLine();
                data_from_file.Add(s);
            }
            data_from_file.Sort();
            data_from_file.Reverse();
            file_forRead.Close();
            return data_from_file;
        }
        /// <summary>
        /// Дополнить статистику новым рекордом
        /// </summary>
        /// <param name="p">Путь к файлу со статистикой</param>
        /// <param name="new_record">Новый рекорд</param>
        public List<string> Statistic(string p, string new_record)
        {
            data_from_file = new List<string>();
            file_forRead = new StreamReader(p);
            int size = File.ReadAllLines(p).Length;
            for (int i = 0; i < size; i++)
            {
                string s = file_forRead.ReadLine();
                data_from_file.Add(s);
            }
            data_from_file.Add(new_record);
            data_from_file.Sort();
            data_from_file.Reverse();
            if (size > 20) { data_from_file.Remove(data_from_file[size - 1]); size = 20; }
            else size++;
            file_forRead.Close();

            File.WriteAllText(p, string.Empty);
            file_for_write = new StreamWriter(p);
            for (int i = 0; i < size; i++)
            {
                file_for_write.WriteLine(data_from_file[i]);
            }
            file_for_write.Close();
            return data_from_file;
        }
    }
}
