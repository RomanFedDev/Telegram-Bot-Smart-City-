namespace TelegramBot
{
    internal class Program
    {
        static bool cycle = true;
        static string userName = "";
        static string commands = "/start, /help, /info, /echo, /addtask, /showtasks, /removetask, /exit";

        static List<string> taskList = new();

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать! Доступные команды: " + commands);

            while (cycle)
            {
                Console.WriteLine();
                var name = userName != "" ? $"{userName}, в" : "В";
                Console.WriteLine(name + "ведите команду:");
                Console.Write("/");

                CheckEnteredCommand("/" + Console.ReadLine() ?? string.Empty);
            }
        }

        static void CheckEnteredCommand(string command)
        {
            string[] commandParts = command.Split(' ', 2);
            string argument = commandParts.Length > 1 ? commandParts[1].Trim() : string.Empty;

            string commanSplit = commandParts[0].ToLower();
            var nameDefault = userName != "" ? $"{userName}, в" : "В";

            switch (commanSplit)
            {
                case "/start":
                    Console.WriteLine("Введите ваше имя:");
                    userName = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine($"Привет, {userName}! Чем могу помочь?");
                    break;
                case "/info":
                    var nameInfo = userName != "" ? $"{userName}, п" : "П";
                    Console.WriteLine(nameInfo + "рограмма v 0.2, дата создания: 20.10.2026");
                    break;
                case "/help":
                    Console.WriteLine(nameDefault + "ведите нужную команду для начала работы." +
                        "\nЕсли пользователь вводит команду /start, программа просит его ввести своё имя." +
                        "\nПосле ввода имени становится доступной команда /echo." +
                        "\nКоманда /echo возвращает введенный текст." +
                        "\nКоманда /help Отображает краткую справочную информацию о том, как пользоваться программой." +
                        "\nКоманда /info Предоставляет информацию о версии программы и дате её создания." +
                        "\nПрограмма продолжает ожидать ввод команды от пользователя, пока не будет введена команда /exit." +
                        "\n..." +
                        "\nКоманда /addtask позволяет добавлять задачи в список." +
                        "\nКоманда /showtasks показывает список добавленных задач." +
                        "\nКоманда /removetask удаляет задачу из списка по номеру.");
                    break;
                case "/exit":
                    Console.WriteLine("Программа завершена.");
                    cycle = false;
                    break;
                case "/echo":
                    if (userName == "")
                    {
                        Console.WriteLine(@"Для активации команды /echo введите команду ""/start""");
                        return;
                    }
                    if (argument.Length <= 0)
                    {
                        Console.WriteLine("Введите текст после команды /echo ");
                    }
                    else
                        Console.WriteLine(argument);
                    break;

                case "/addtask":
                    AddTaskToArray();
                    break;
                case "/showtasks":
                    ShowTasks();
                    break;
                case "/removetask":
                    RemoveTask();
                    break;

                default:
                    Console.WriteLine(nameDefault + "ведена неверная команда!");
                    break;
            }
        }

        static void AddTaskToArray()
        {
            Console.WriteLine("Введите описание задачи:");
            var task = Console.ReadLine() ?? string.Empty;

            if (taskList.Contains(task))
            {
                Console.WriteLine($@"Задача ""{task}"" уже в списке. Введите новую.");
                return;
            }

            taskList.Add(task);
            Console.WriteLine(@$"Задача ""{task}"" добавлена.");
        }

        static void ShowTasks()
        {
            if (taskList.Count == 0)
            {
                Console.WriteLine("Задачи не добавлены!");
                return;
            }

            Console.WriteLine("Вот ваш список задач:");
            for (int i = 0; i < taskList.Count; i++)
                Console.WriteLine($"{i + 1}. {taskList[i]}");
        }

        static void RemoveTask()
        {
            if (taskList.Count == 0)
            {
                Console.WriteLine("Список задач пуст!");
                return;
            }

            ShowTasks();
            Console.WriteLine("Введите номер задачи для удаления:");
            var task = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(task, out int taskID))
            {
                if (taskID > taskList.Count || taskID < 1)
                {
                    Console.WriteLine("Неверный номер задачи! Введите корректный номер.");
                    return;
                }

                if (taskList.Contains(taskList[taskID - 1]))
                {
                    Console.WriteLine($@"Задача ""{taskList[taskID - 1]}"" удалена.");
                    taskList.Remove(taskList[taskID - 1]);
                }
            }
            else
                Console.WriteLine("Неверный номер задачи! Введите корректный номер.");
        }
    }
}
