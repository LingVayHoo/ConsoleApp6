using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();
            MainMenu(repository);            
            Console.ReadKey();
        }

        /// <summary>
        /// Главное меню
        /// </summary>
        /// <param name="repository"></param>
        static void MainMenu(Repository repository)
        {
            Console.WriteLine("Главное меню:" +
                "\n" +
                "\n 1 - просмотр всех записей" +
                "\n 2 - найти запись по ID" +
                "\n 3 - добавить запись" +
                "\n 4 - удалить запись" +
                "\n 5 - найти все записи в диапазоне дат" +
                "\n");
            Console.Write("Ваш выбор: ");
            int key = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            ChoiceActivator(key, repository);
        }

        /// <summary>
        /// Метод для активации выбранного пункта меню
        /// </summary>
        /// <param name="key"></param>
        /// <param name="repository"></param>
        static void ChoiceActivator(int key, Repository repository)
        {
            switch (key)
            {
                case 1:
                    PrintAllLines(repository);
                    break;
                case 2:
                    PrintLineByID(repository);
                    break;
                case 3:
                    AddWorkerTorepository(repository);
                    break;   
                case 4:
                    DeleteWorkerFromRepository(repository);
                    break;
                case 5:
                    ChooseCustomLines(repository);
                    break;
                default:
                    Console.WriteLine("Неверный ввод!");
                    MainMenu(repository);
                    break;
            }
        }

        /// <summary>
        /// Метод для печати всех записей репозитория
        /// </summary>
        /// <param name="repository"></param>
        static void PrintAllLines(Repository repository)
        {
            Worker[] workers = repository.GetAllWorkers();
            PrintLines(workers, repository);
            SortMenu(workers, repository);
        } 

        /// <summary>
        /// Метод для печати определенного массива записей
        /// </summary>
        /// <param name="workers"></param>
        /// <param name="repository"></param>
        static void PrintLines(Worker[] workers, Repository repository)
        {
            if(workers != null)
            {
                string line;
                PrintTitle();
                for (int i = 0; i < workers.Length; i++)
                {
                    line = repository.WorkerToString(workers[i], workers[i].Id);
                    string[] data = line.Split('#');
                    PrintData(data);
                }
            }            
        }

        /// <summary>
        /// Меню сортировки
        /// </summary>
        /// <param name="repository"></param>
        static void SortMenu(Worker[] workers, Repository repository)
        {
            if (workers != null)
            {
                Console.WriteLine();
                Console.WriteLine("Отсортировать по:" +
                   "\n" +
                   "\n 1 - ID" +
                   "\n 2 - дате создания" +
                   "\n 3 - ФИО сотрудника" +
                   "\n 4 - возрасту" +
                   "\n 5 - росту" +
                   "\n 6 - дате рождения" +
                   "\n 7 - месту рождения" +
                   "\n");
                Console.Write("Ваш выбор: ");
                int key = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Sort(key, repository);
            }
            ReturnToMenu(repository);
        }

        /// <summary>
        /// Метод для сортировки данных
        /// </summary>
        /// <param name="key"></param>
        /// <param name="repository"></param>
        static void Sort(int key, Repository repository)
        {
            Worker[] workers = repository.GetAllWorkers();
            

            switch (key)
            {
                case 1:
                    workers.OrderBy(w => w.Id);
                    PrintLines(workers, repository);
                    break;
                case 2:
                    workers.OrderBy(w => w.CreateDateTime);
                    PrintLines(workers, repository);
                    break; 
                case 3:
                    workers.OrderBy(w => w.FullName);
                    PrintLines(workers, repository);
                    break; 
                case 4:
                    workers.OrderBy(w => w.Age);
                    PrintLines(workers, repository);
                    break;
                case 5:
                    workers.OrderBy(w => w.Height);
                    PrintLines(workers, repository);
                    break;
                case 6:
                    workers.OrderBy(w => w.Birthday);
                    PrintLines(workers, repository);
                    break;
                case 7:
                    workers.OrderBy(w => w.Birthplace);
                    PrintLines(workers, repository);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Метод для печати записи по ID
        /// </summary>
        /// <param name="repository"></param>
        static void PrintLineByID(Repository repository)
        {
            Console.WriteLine();
            Console.Write("Введите номер сотрудника:");
            int targetID = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Worker targetWorker = repository.GetWorkerByID(targetID);
            if(targetWorker.Id > -1)
            {
                string line = repository.WorkerToString(targetWorker, targetWorker.Id);
                string[] data = line.Split('#');
                PrintTitle();
                PrintData(data);
            }
            ReturnToMenu(repository);
        }

        /// <summary>
        /// Метод для создания записи в репозитории
        /// </summary>
        /// <param name="repository"></param>
        static void AddWorkerTorepository(Repository repository)
        {
            Worker createdWorker = new Worker();
            createdWorker.Id = 0;
            createdWorker.CreateDateTime = DateTime.Now;
            createdWorker.FullName = AddInfo("ФИО");
            createdWorker.Age = IsValidValueInt("возраст");
            createdWorker.Height = IsValidValueInt("рост");
            createdWorker.Birthday = IsValidValueDateTime("дату рождения в формате ##.##.####");
            createdWorker.Birthplace = AddInfo("место рождения");
            repository.AddWorker(createdWorker);
            Console.WriteLine("Запись успешно добавлена!");
            ReturnToMenu(repository);
        }


        /// <summary>
        /// Метод для удаления записи из репозитория
        /// </summary>
        /// <param name="repository"></param>
        static void DeleteWorkerFromRepository(Repository repository)
        {
            Console.WriteLine();
            Console.Write("Введите номер сотрудника, запись о котором нужно удалить:");
            int targetID = int.Parse(Console.ReadLine());
            Console.WriteLine();

            if(repository.DeleteWorker(targetID))
            {
                Console.WriteLine("Запись успешно удалена");
            }
            else
            {
                Console.WriteLine("Запись не найдена!");
            }
            ReturnToMenu(repository);
        }

        /// <summary>
        /// Метод для 
        /// </summary>
        /// <param name="repository"></param>
        static void ChooseCustomLines(Repository repository)
        {
            if (repository.IsFileExists())
            {
                DateTime startDate = IsValidValueDateTime("начальную дату:");
                DateTime finishDate = IsValidValueDateTime("конечную дату (не включительно):");
                Worker[] customWorkers = repository.GetWorkersBetweenTwoDates(startDate, finishDate);

                PrintLines(customWorkers, repository);
            }
            else
            {
                Console.WriteLine("Файл не найден!");
            }
            ReturnToMenu(repository);
        }

        /// <summary>
        /// Метод для печати заглавия
        /// </summary>
        static void PrintTitle()
        {
            Console.WriteLine(
                $"{"ID |",4}" +
                $"{"Дата и время записи |",23}" +
                $"{"Фамилия Имя Отчество |",33}" +
                $"{"Возраст |",9}" +
                $"{"Рост |",5}" +
                $"{"Дата рождения |",16}" +
                $"{"Место рождения |",18}");
        }

        /// <summary>
        /// Метод для печати массива string
        /// </summary>
        /// <param name="data"></param>
        static void PrintData(string[] data)
        {
            Console.WriteLine
                (
                   $"{data[0],3}" +
                   $"{data[1],23}" +
                   $"{data[2],33}" +
                   $"{data[3],8}" +
                   $"{data[4],6}" +
                   $"{data[5],16}" +
                   $"{data[6],18}"
                );
        }

        /// <summary>
        /// Проверка на неправильный ввод int
        /// </summary>
        /// <param name="titleOfValue"></param>
        /// <returns></returns>
        static int IsValidValueInt(string titleOfValue)
        {
            string value = AddInfo(titleOfValue);
            if (int.TryParse(value, out int validValue))
            {
                return validValue;
            }
            else 
            {
                Console.WriteLine("Неверный ввод!");
                return IsValidValueInt(titleOfValue); 
            }
        }


        /// <summary>
        /// Проверка на неправильный ввод даты
        /// </summary>
        /// <param name="titleOfValue"></param>
        /// <returns></returns>
        static DateTime IsValidValueDateTime(string titleOfValue)
        {
            string value = AddInfo(titleOfValue);
            if (DateTime.TryParse(value, out DateTime dateTime))
            {
                return dateTime;
            }
            else
            {
                Console.WriteLine("Неверный ввод!");
                return IsValidValueDateTime(titleOfValue);
            }
        }

        
        static void ReturnToMenu(Repository repository)
        {
            Console.WriteLine();
            Console.WriteLine("1 - Возврат в главное меню" +
                "\n2 - выход");
            Console.WriteLine();
            Console.Write("Ваш выбор:");
            int key = int.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (key)
            {
                case 1:
                    MainMenu(repository);
                    break;
                case 2:
                    break;
                default:
                    Console.WriteLine("Неверный ввод!");
                    ReturnToMenu(repository);
                    break;
            }
        }


        /// <summary>
        /// Метод для добавления информации в переменную
        /// </summary>
        /// <param name="inputName">название добавляемого поля с маленькой буквы в родительном падеже</param>
        /// <returns></returns>
        static string AddInfo(string inputName)
        {
            Console.WriteLine($"Введите {inputName}:");
            return Console.ReadLine();
        }
    }

    
}
