    using TextEditor;

    static class Program
    {
        static void Main()
        {
            FileHandler fileManager = ObtainPathAndLoadFile();
            Figure loadedFigure = fileManager?.LoadFile();

            if (loadedFigure != null)
            {
                DisplayLoadedFigure(loadedFigure);
                PerformFileOperationsLoop(fileManager, loadedFigure);
            }
            else
            {
                Console.WriteLine("Ошибка загрузки файла.");
            }
        }

        static FileHandler ObtainPathAndLoadFile()
        {
            Console.Write("Введите путь к файлу: ");
        string filePath = Console.ReadLine();

        return new FileHandler(filePath);
    }

    static void DisplayLoadedFigure(Figure loadedFigure)
    {
        Console.WriteLine("Loaded Figure:");
        Console.WriteLine($"Name: {loadedFigure.Name}");
        Console.WriteLine($"Width: {loadedFigure.Width}");
        Console.WriteLine($"Height: {loadedFigure.Height}");
    }

    static void PerformFileOperationsLoop(FileHandler fileManager, Figure loadedFigure)
    {
        ConsoleKeyInfo keyInfo;
        do
        {
            Console.WriteLine("Нажмите F1, чтобы сохранить в новом формате, или Esc для выхода.");
            keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.F1)
            {
                string newFormat = RequestNewFileFormat();
                if (!string.IsNullOrEmpty(newFormat))
                {
                    fileManager.SaveFile(loadedFigure, newFormat);
                    Console.WriteLine($"Файл сохранён в формате {newFormat}.");
                }
            }
        } while (keyInfo.Key != ConsoleKey.Escape);
    }

    static string RequestNewFileFormat()
    {
        Console.WriteLine("Введите новый формат файла (txt, json, xml):");
        string newFormat = Console.ReadLine();
        if (newFormat.Equals("txt", StringComparison.OrdinalIgnoreCase) ||
            newFormat.Equals("json", StringComparison.OrdinalIgnoreCase) ||
            newFormat.Equals("xml", StringComparison.OrdinalIgnoreCase))
        {
            return newFormat;
        }

        Console.WriteLine("Введён недопустимый формат файла.");
        return null;
    }
}
