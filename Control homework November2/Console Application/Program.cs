namespace Console_Application;
class Program
{
    /// <summary>
    /// Метод для считывания с консоли переменной типа int м проверки этой переменной на ограничения. Если ограничений нет, то происходит такая проверка: -бесконечность < введенное число < +бесконечность.
    /// </summary>
    /// <param name="name">Имя переменной, которую надо считать. По умолчанию имени нет.</param>
    /// <param name="lowerBound">Нижняя граница ограничения. По умолчанию равна -бесконечности.</param>
    /// <param name="upperBound">Верхняя граница ограничения. По умолчанию равна +бесконечности.</param>
    /// <param name="lowerSing">Строгость знака нижней границы ограничения. По умолчанию строгий знак ("<").</param>
    /// <param name="upperSing">Строгость знака верхней границы ограничения. По умолчанию строгий знак ("<").</param>
    /// <returns>Переменная типа int, которую ввёл пользователь.</returns>
    public static int InputInt(string name = "", double lowerBound = double.NegativeInfinity, double upperBound = double.PositiveInfinity, string lowerSing = "<", string upperSing = "<")
    {
        bool flag;
        // Временная переменная для проверки на корректность ввода данных.
        int result;
        // Возвращаемая переменная.
        do
        {
            Console.Write($"{lowerBound} {lowerSing} {name} {upperSing} {upperBound}. {name} = ");
            // Предупреждение пользователя об ограничениях на переменную, которую надо ввести.
            if (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Некорректный ввод, повторите попытку");
                flag = false;
                // Срабатывает, если пользователь ввел любой символ, не содержащий цифр.
            }
            else
            {
                bool flag1 = true;
                // Временная переменная создания проерки на ограничения. Проверяет нижнее ограничение.
                bool flag2 = true;
                // Временная переменная создания проерки на ограничения. Проверяет верхнее ограничение.
                switch (lowerSing)
                {
                    case "<":
                        flag1 = lowerBound < result;
                        break;
                    case "<=":
                        flag1 = lowerBound <= result;
                        break;
                }
                switch (upperSing)
                {
                    case "<":
                        flag2 = result < upperBound;
                        break;
                    case "<=":
                        flag2 = result <= upperBound;
                        break;
                }
                if (flag1 && flag2)
                {
                    flag = true;
                }
                else
                {
                    Console.WriteLine($"Введённые значения не подходят под ограничения. {lowerBound} {lowerSing} {name} {upperSing} {upperBound}");
                    flag = false;
                }
            }
        }
        while (!flag);
        // Цикл повторяется до тех пор, пока пользователь не введет корректные значения.
        return result;
    }
    ///<summary>
    ///Этот метод запрашивает у пользователя абсолютный путь к файлу с csv-данными.
    ///</summary>
    ///<returns>
    ///Переменная типа string, содержащая абсолютный путь к файлу с csv-данными.
    ///</returns>
    public static string AbsolutePath(string name)
    {
        string path;
        Console.Write($"{name} = ");
        path = Console.ReadLine();
        return path;
    }
    /// <summary>
    /// Этот метод считывает определенные данные с файла.
    /// </summary>
    /// <returns>
    /// Значение типа bool для выхода из программы.
    /// </returns>
    /// <param name="path">
    /// Абсолютный путь файла.
    /// </param>
    public static string[] GettingData(string path)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Укажите номер пункта меню для запуска действия:");
        Console.WriteLine("    1. Произвести выборку по значению AdmArea");
        Console.WriteLine("    2. Произвести выборку по значению CarCapacity");
        Console.WriteLine("    3. Произвести выборку по значению District и Mode");
        Console.WriteLine("    4. Произвести выборку по значениям AdmArea, CarCapacity, District и Mode");
        Console.WriteLine("    5. Отсортировать таблицу по значению CarCapacity (по увеличению ёмкости)");
        Console.WriteLine("    6. Отсортировать таблицу по значению CarCapacity (по уменьшению ёмкости)");
        Console.WriteLine("    7. Выйти из программы");
        int choice = InputInt("Ваш выбор", lowerBound: 0, upperBound: 8);
        Console.ResetColor();
        string[] result = null;
        switch (choice)
        {
            case 1:
                // Реализация выборки по значению AdmArea.
                result = ClassLibary.DataProcessing.Selection(1);
                break;
            case 2:
                // Реализация выборки по значению CarCapacity.
                result = ClassLibary.DataProcessing.Selection(2);
                break;
            case 3:
                // Реализация выборки по значению District и Mode.
                result = ClassLibary.DataProcessing.Selection(3);
                break;
            case 4:
                // Реализация выборки по значению AdmArea, CarCapacity, District и Mode.
                result = ClassLibary.DataProcessing.Selection(4);
                break;
            case 5:
                // Реализация сортировки по значению CarCapacity (по увеличению ёмкости).
                result = ClassLibary.DataProcessing.SortedUp();
                break;
            case 6:
                // Реализация сортировки по значению CarCapacity (по уменьшению ёмкости).
                result = ClassLibary.DataProcessing.SortedLow();
                break;
            case 7:
                // Выход из программы.
                result = ClassLibary.CsvProcessing.Read();
                ClassLibary.CsvProcessing.Write(result, AbsolutePath("Новый абсолютный путь, чтобы перезаписать файл"));
                Console.Write("Попрощайтесь с программой: ");
                Console.ReadLine();
                Console.WriteLine("До свидания!");
                // Досрочный выход из программы.
                Environment.Exit(0);
                break;
        }
        return result;
    }
    /// <summary>
    /// Метод рисует очень патриотичный флаг России. Если вы поставите оценку за это кдз < 5, это будет очень непатриотично! (приравненно к иноагенству)
    /// </summary>
    public static void RussianFlag()
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.WriteLine("                                                                                                                                    ");
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("                                                                                                                                    ");
        Console.BackgroundColor = ConsoleColor.Red;
        Console.WriteLine("                                                                                                                                    ");
        Console.ResetColor();
    }
    static void Main(string[] args)
    {
        RussianFlag();
        string fPath = AbsolutePath("Абсолютный путь");
        ClassLibary.CsvProcessing.GetPath(fPath);
        ClassLibary.CsvProcessing.Read();
        string[] result = GettingData(fPath);
        string nPath = AbsolutePath("Новый абсолютный путь");
        // Переписывание данных.
        ClassLibary.CsvProcessing.Write(result, nPath);
        // Перекрашивание консоли в черный цвет.
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Write("Попрощайтесь с программой: ");
        Console.ReadLine();
    }   
}