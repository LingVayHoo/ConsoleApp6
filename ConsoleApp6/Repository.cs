using System;
using System.IO;
using System.Text;

namespace ConsoleApp6
{
    internal class Repository
    {        
        private string _fileName = "Staff.txt";
        public Worker[] GetAllWorkers()
        {
            if (IsFileExists())
            {
                Worker[] workers = new Worker[NumberOfLines()];
                using (StreamReader sr = new StreamReader(_fileName, Encoding.Unicode))
                {
                    string line;
                    int i = 0;
                     
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] data = line.Split('#');

                        workers[i] = StringToWorker(data);
                        i++;
                    }
                }
                return workers;
            }         
            else
            {
                Console.WriteLine("Файл не найден!");
                return null;
            }
        }

        public Worker GetWorkerByID(int ID)
        {
            Worker[] allWorkers = GetAllWorkers();
            
            if (allWorkers != null && allWorkers.Length > 0 && allWorkers.Length >= ID)
            {
                Worker targetWorker = new Worker();
                for (int i = 0; i < allWorkers.Length; i++)
                {
                    if (allWorkers[i].Id == ID)
                    {
                        targetWorker = allWorkers[i];
                        break;
                    }
                }
                return targetWorker;
            }  
            else 
            {
                return DefaultWorker(); 
            }
        }

        public bool DeleteWorker(int ID)
        {
            Worker[] allWorkers = GetAllWorkers();
            int i = 1;
            bool isFounded = false;
            if (allWorkers != null && allWorkers.Length > 0 && allWorkers.Length >= ID)
            {
                using (StreamWriter sw = new StreamWriter(_fileName, false, Encoding.Unicode))
                {
                    foreach (Worker worker in allWorkers)
                    {
                        if(worker.Id == ID)
                        {
                            isFounded = true;
                        }
                        if (worker.Id != ID)
                        {
                            sw.WriteLine(WorkerToString(worker, i));
                            i++;
                        }
                    }
                }
            }

            return isFounded;
        }

        public void AddWorker(Worker worker)
        {
            string line = WorkerToString(worker, NumberOfLines() + 1);
            using (StreamWriter sw = new StreamWriter(_fileName, true, Encoding.Unicode))
            {
                sw.WriteLine(line);
            }
        }

        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            Worker[] allWorkers = GetAllWorkers();

            // Не знаю, какой метод здесь нужно было применить. Я давно использую в играх 
            // такой метод фильтрации, буду признателен за подсказку более удачного. 
            // Плюсом такого метода на мой взгляд является возможность искать даже непоследовательные элементы
            int count = 0;
            foreach (var item in allWorkers)
            {
                if (item.CreateDateTime >= dateFrom && item.CreateDateTime <= dateTo)
                {
                    count++;
                }
            }
            Worker[] targetWorkers = new Worker[count];
            int j = 0;
            for (int i = 0; i < allWorkers.Length; i++)
            {
                if (allWorkers[i].CreateDateTime >= dateFrom && allWorkers[i].CreateDateTime <= dateTo)
                {
                    Console.WriteLine($"{targetWorkers.Length} дебаг");
                    targetWorkers[j] = allWorkers[i];
                    j++;
                }
            }
            return targetWorkers;
        }

        public string WorkerToString(Worker w, int newID)
        {
            string line;
            line = newID.ToString() + '#' +
                w.CreateDateTime.ToString() + '#' +
                w.FullName + '#' +
                w.Age.ToString() + '#' +
                w.Height.ToString() + '#' +
                w.Birthday.ToShortDateString() + '#' +
                w.Birthplace;
            return line;
        }

        private Worker StringToWorker(string[] data)
        {
            Worker targetWorker =  new Worker(
                            int.Parse(data[0]),
                            DateTime.Parse(data[1]),
                            data[2],
                            int.Parse(data[3]),
                            int.Parse(data[4]),
                            DateTime.Parse(data[5]),
                            data[6]);
            return targetWorker;
        }

        /// <summary>
        /// Метод для определения ID
        /// </summary>
        /// <returns></returns>
        private int NumberOfLines()
        {
            int count = 0;
            if (IsFileExists())
            {
                using (StreamReader sr = new StreamReader(_fileName, Encoding.Unicode))
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
        /// Метод для проверки существования файла
        /// </summary>
        /// <returns></returns>
        public bool IsFileExists()
        {
            FileInfo file = new FileInfo(@_fileName);
            return file.Exists;
        }

        private Worker DefaultWorker()
        {
            Worker worker = new Worker(-1, "Error");
            return worker;
        }

    }
}
