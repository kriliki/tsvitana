using System;
using System.IO;

// Власний клас виключення для випадку, коли файл не знайдено
public class FileNotFoundException : Exception
{
    public FileNotFoundException(string message) : base(message) { }
}

// Власний клас виключення для випадку, коли файл не доступний
public class FileNotAccessibleException : Exception
{
    public FileNotAccessibleException(string message) : base(message) { }
}

// Власний клас виключення для випадку, коли файл має бiльше 100 рядкiв
public class TooManyLinesException : Exception
{
    public TooManyLinesException(string message) : base(message) { }
}

// Клас для читання та виводу вмiсту файлу
public class FileReader
{
    private string fileName;

    public FileReader(string fileName)
    {
        this.fileName = fileName;
    }

    public void ReadFile()
    {
        // Перевiрка iснування файлу
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException($"Файл {fileName} не знайдено");
        }

        // Перевiрка доступностi файлу для читання
        if (!IsFileAccessible(fileName))
        {
            throw new FileNotAccessibleException($"Файл {fileName} не доступний для читання");
        }

        string[] lines = File.ReadAllLines(fileName);

        // Перевiрка на максимальну кiлькiсть рядкiв (100)
        if (lines.Length > 100)
        {
            throw new TooManyLinesException($"Файл мiстить {lines.Length} рядкiв, максимум 100");
        }

        DisplayContent(lines);
    }

    // Метод перевiряє, чи можна вiдкрити файл для читання
    private bool IsFileAccessible(string fileName)
    {
        try
        {
            using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
    }

    private void DisplayContent(string[] lines)
    {
        Console.WriteLine("=== Вмiст файлу ===");
        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введiть iм'я файлу: ");
        string fileName = Console.ReadLine();

        try
        {
            FileReader reader = new FileReader(fileName);
            reader.ReadFile();
        }
        // Обробка власних виключень у порядку вiд бiльш специфiчних до загальних
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        catch (FileNotAccessibleException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        catch (TooManyLinesException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Невiдома помилка: {ex.Message}");
        }
    }
}
