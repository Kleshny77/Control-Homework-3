using System.IO;

namespace ClassLibary;
/// <summary>
/// Cодержит методы для чтения и записи данных в csv файл.
/// </summary>
public class CsvProcessing
{
    private static string fPath;
    /// <summary>
    /// Получение пути к файлу.
    /// </summary>
    /// <param name="path">Абсолютный путь к файлу.</param>
    public static void GetPath(string path)
    {
        fPath = path;
    }
    /// <summary>
    /// Считывает файл в массив строк. Если файла не существует, выбрасывает исключение.
    /// </summary>
    /// <returns>Данные таблицы в виде массива строк.</returns>
    /// <exception cref="ArgumentNullException">Файла не существует.</exception>
    public static string[] Read()
    {
        // Проверка существования файла.
        if (!File.Exists(fPath))
        {
            // Выбрасывание исключения.
            throw new ArgumentNullException("Файл не найден.");
        }
        // Считывание файла.
        string[] table = File.ReadAllLines(fPath);
        return table;
    }
    /// <summary>
    /// Метод записывает данные в новый файл.
    /// </summary>
    /// <param name="data">Данные, которые надо записать в файл.</param>
    /// <param name="nPath">Путь к новому файлу.</param>
    public static void Write(string data, string nPath)
    {
        try
        {
            // Проверка существования файла.
            if (File.Exists(nPath))
            {
                // Дописываем данные в конец файла.
                File.AppendAllText(nPath, data);
            }
            else
            {
                // Если файл не существует, создаем новый файл и записываем данные.
                File.WriteAllText(nPath, data);
            }
        }
        catch (UnauthorizedAccessException)
        {
            // Ошибка доступа к файлу из-за недостаточных прав доступа.
            Console.WriteLine("Ошибка доступа - недостаточно прав доступа к файлу.");
        }
        catch (PathTooLongException)
        {
            // Ошибка из-за слишком длинного пути к файлу.
            Console.WriteLine("Ошибка - слишком длинный путь к файлу.");
        }
        catch (DirectoryNotFoundException)
        {
            // Ошибка, если директория пути файла не существует.
            Console.WriteLine("Ошибка - директория пути файла не существует.");
        }
        catch (IOException)
        {
            // Ошибка ввода-вывода.
            Console.WriteLine("Ошибка ввода-вывода при работе с файлом.");
        }
        catch (NotSupportedException)
        {
            // Ошибка, если операция не поддерживается.
            Console.WriteLine("Ошибка - операция не поддерживается.");
        }
        catch (Exception ex)
        {
            // Любая другая необработанная ошибка.
            Console.WriteLine("Ошибка записи данных в файл: " + ex.Message);
        }
    }
    /// <summary>
    /// Метод записывает новые данные в уже существующий файл, стирая старые данные в нём.
    /// </summary>
    /// <param name="data">Данные, которые надо перезаписать в старый файл.</param>
    /// <param name="fPath">Путь к существующему файлу</param>
    public static void Write(string[] data, string fPath)
    {
        try
        {
            File.WriteAllLines(fPath, data);
        }
        catch (UnauthorizedAccessException)
        {
            // Ошибка доступа к файлу из-за недостаточных прав доступа.
            Console.WriteLine("Ошибка доступа - недостаточно прав доступа к файлу.");
        }
        catch (PathTooLongException)
        {
            // Ошибка из-за слишком длинного пути к файлу.
            Console.WriteLine("Ошибка - слишком длинный путь к файлу.");
        }
        catch (DirectoryNotFoundException)
        {
            // Ошибка, если директория пути файла не существует.
            Console.WriteLine("Ошибка - директория пути файла не существует.");
        }
        catch (IOException)
        {
            // Ошибка ввода-вывода.
            Console.WriteLine("Ошибка ввода-вывода при работе с файлом.");
        }
        catch (NotSupportedException)
        {
            // Ошибка, если операция не поддерживается.
            Console.WriteLine("Ошибка - операция не поддерживается.");
        }
        catch (Exception ex)
        {
            // Любая другая необработанная ошибка.
            Console.WriteLine("Ошибка записи данных в файл: " + ex.Message);
        }
    }
}
/// <summary>
/// Содержит методы для работы с данными, полученными из файла.
/// </summary>
public class DataProcessing
{
    /// <summary>
    /// Делает выборку по определенным строкам определенных столбцов.
    /// </summary>
    public static string[] Selection(int choice)
    {
        // Получение значения полей от пользователя
        Console.Write("Значение поля Adm Area: ");
        string admArea = Console.ReadLine();
        Console.Write("Значение поля Carcapacity: ");
        string carCapacity = Console.ReadLine();
        Console.Write("Значение поля District: ");
        string district = Console.ReadLine();
        Console.Write("Значение поля Mode: ");
        string mode = Console.ReadLine();
        string[] table = new string[0];
        try
        {
            // Получение таблицы из файла.
            table = CsvProcessing.Read();
            string[] result = new string[0];
            for (int i = 0; i < table.Length; i++)
            {
                // Разбиваем строку таблицы на поля.
                string[] rowFields = table[i].Split(new string[] { "\";\"", "\";", "\"", ";" }, StringSplitOptions.None);
                bool flag = false;
                switch (choice)
                {
                    case 1:
                        flag = rowFields[4] == admArea;
                        break;
                    case 2:
                        flag = rowFields[10] == carCapacity;
                        break;
                    case 3:
                        flag = rowFields[5] == district && rowFields[11] == mode;
                        break;
                    case 4:
                        flag = rowFields[4] == admArea && rowFields[10] == carCapacity && rowFields[5] == district && rowFields[11] == mode;
                        break;
                }
                // Проверяем соответствие каждого поля критерию поиска.
                if (flag)
                {
                    Array.Resize(ref result, result.Length + 1);
                    result[result.Length - 1] = table[i];
                }
            }
            // Вывод результата на экран.
            if (result.Length > 0)
            {
                foreach (string row in result)
                {
                    string[] rowFields = row.Split(new string[] { "\";\"", " "}, StringSplitOptions.None);
                    for (int i = 0; i < rowFields.Length; i++)
                    {
                        string field = rowFields[i].Trim('"');
                        Console.Write(field.PadRight(5));
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Результат выборки пуст.");
            }
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine("Ошибка при чтении файла: " + ex.Message);
        }
        return table;
    }
    /// <summary>
    /// Cортировка по значениям CarCapacity по уменьшению ёмкости.
    /// </summary>
    public static string[] SortedLow()
    {
        // Получение таблицы из файла.
        string[] table = CsvProcessing.Read();
        int columnIndex = 9;
        ///<summary>
        /// Метод для корректной сортировки значений.
        /// </summary>
        /// <returns>
        /// 0 - если значения равны либо меньшее из двух значений.
        /// </returns>
        int CompareByColumn(string x, string y)
        {
            string[] xFields = x.Split(',');
            string[] yFields = y.Split(',');

            if (xFields.Length > columnIndex && yFields.Length > columnIndex)
            {
                int xValue = int.Parse(xFields[columnIndex]);
                int yValue = int.Parse(yFields[columnIndex]);

                // Сравнение в обратном порядке (убывание).
                return yValue.CompareTo(xValue);
            }
            else
            {
                return 0;
            }
        }
        Array.Sort(table, CompareByColumn);
        // Вывод таблицы в консоль.
        foreach (string row in table)
        {
            string[] rowFields = row.Split(';');
            foreach (string field in rowFields)
            {
                Console.Write(field + "\t");
            }
            Console.WriteLine();
        }
        return table;
    }
    /// <summary>
    /// Cортировка по значениям CarCapacity по увеличению ёмкости.
    /// </summary>
    public static string[] SortedUp()
    {
        // Получение таблицы из файла.
        string[] table = CsvProcessing.Read();
        int columnIndex = 10;
        Array.Sort(table, (x, y) =>
        {
            string[] xColumns = x.Split(new string[] { "\";\"", " " }, StringSplitOptions.None);
            string[] yColumns = y.Split(new string[] { "\";\"", " " }, StringSplitOptions.None);
            if (xColumns.Length > columnIndex && yColumns.Length > columnIndex)
            {
                return string.Compare(xColumns[columnIndex],yColumns[columnIndex]);
            }
            else
            {
                return 0;
            }
        });
        // Вывод отсортированных данных на экран.
        foreach (string row in table)
        {
            string[] rowFields = row.Split(';');
            foreach (string field in rowFields)
            {
                Console.Write(field + "\t");
            }
            Console.WriteLine();
        }
        return table;
    }
}