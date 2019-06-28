using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
{
    class MagicSquareClass
    {
        private Random rnd;
        private StreamReader file_for_read;
        private static string path;
        private int line_number;

        public MagicSquareClass()
        {
            path = "magic.txt";
            file_for_read = new StreamReader(path);
            rnd = new Random();
        }
        /// <summary>
        /// Получить магический квадрат из файла
        /// </summary>
        public int[] MagicSquareChange()
        {
            int count = File.ReadAllLines(path).Length;
            int n = rnd.Next(0, count);
            string s = File.ReadAllLines(path).Skip(n).First();
            string[] splitString = s.Split(' ');
            int[] nums = new int[9];
            for (int i = 0; i < 9; i++)
            {
                int x;
                int.TryParse(splitString[i], out x);
                nums[i] = x;
            }
            return nums;
        }
        /// <summary>
        /// Магический квадрат с пустыми ячейками
        /// </summary>
        /// <param name="arr"></param>
        public int[] MagicSquareOnGrid(int[] arr)
        {
            int number_of_empty = rnd.Next(2,4);
            int[] arr_empty_index = new int[9];

            for (int i = 0; i < 9; i++)
                arr_empty_index[i] = 0;

            int interim = 0, count = 0;

            while (count < number_of_empty)
            {
                for (int i = 0; i < 9; i++)
                {
                    interim = rnd.Next(1, 3);
                    if (interim != 1)
                    {
                        if (arr_empty_index[i] == 0)
                        {
                            arr_empty_index[i] = 1;
                            count++;
                        }
                    }
                }
            }

            for (int i = 0; i < 9; i++)
            {
                if (arr_empty_index[i] == 1) arr[i] = 0;
            }
            return arr;
        }
    }
}

