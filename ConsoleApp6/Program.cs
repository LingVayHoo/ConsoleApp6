using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 - запись || 2 - вывод данных");
            Console.Write("Ваш выбор: ");
            int key = Convert.ToInt32(Console.ReadLine());

            if (key == 1) WriteToFile();
            else if (key == 2) ReadFromFile();
            else Console.WriteLine("Неверный ввод!");
            Console.ReadKey();
        }

        static void WriteToFile()
        {
            int ID = CheckID();
            
            using (StreamWriter sw = new StreamWriter("Staff.txt", true, Encoding.Unicode))
            {
                
                string note = ID.ToString() + '#'+ DateAndTime() + '#';
                note += AddInfo("ФИО");
                note += AddInfo("возраст");
                note += AddInfo("рост");
                note += AddInfo("дату рождения в формате ##.##.####");
                note += AddInfo("место рождения");
                sw.WriteLine(note);

            }                      
        }

        static void ReadFromFile()
        {
            using (StreamReader sr = new StreamReader("Staff.txt", Encoding.Unicode))
            {
                string line;

                Console.WriteLine(
                $"{"ID |", 4}" +
                $"{"Дата и время записи |", 23}" +
                $"{"Фамилия Имя Отчество |", 33}" +
                $"{"Возраст |", 9}" +
                $"{"Рост |", 5}" +
                $"{"Дата рождения |", 16}" +
                $"{"Место рождения |", 18}");
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split('#');
                    Console.WriteLine(
                $"{data[0],3}" +
                $"{data[1],23}" +
                $"{data[2],33}" +
                $"{data[3],8}" +
                $"{data[4],6}" +
                $"{data[5],16}" +
                $"{data[6],18}");
                }
            }
        }

        /// <summary>
        /// Метод для определения ID
        /// </summary>
        /// <returns></returns>
        static int CheckID()
        {
            int count = 1;
            if (CheckFileExists())
            {
                using (StreamReader sr = new StreamReader("Staff.txt", Encoding.Unicode))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        count++;
                    }
                }
            }            
            return count;
        }

        /// <summary>
        /// Метод для добавления информации в переменную
        /// </summary>
        /// <param name="inputName">название добавляемого поля с маленькой буквы в родительном падеже</param>
        /// <returns></returns>
        static string AddInfo(string inputName)
        {
            Console.WriteLine($"Введите {inputName}:");
            return Console.ReadLine() + '#';
        }

        static string DateAndTime()
        {
            return DateTime.Now.ToShortDateString() + ' ' + DateTime.Now.ToShortTimeString();
        }

        /// <summary>
        /// Метод для проверки существования файла
        /// </summary>
        /// <returns></returns>
        static bool CheckFileExists()
        {
            FileInfo file = new FileInfo(@"Staff.txt");
            return file.Exists;
        }

    }

    
}
