using TextEditor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

Console.WriteLine("Введите путь к файлу: ");
string filePath = Console.ReadLine();
FileHandler fileManager = new FileHandler(filePath);

Figure loadedFigure = fileManager.LoadFile();
if (loadedFigure != null)
{
    Console.WriteLine("Loaded Figure:");
    Console.WriteLine($"Name: {loadedFigure.Name}");
    Console.WriteLine($"Width: {loadedFigure.Width}");
    Console.WriteLine($"Height: {loadedFigure.Height}");
}
else
{
    Console.WriteLine("Ошибка загрузки файла.");
}

ConsoleKeyInfo keyInfo;
do
{
    Console.WriteLine("Нажмите F1, чтобы сохранить в новом формате, или Esc для выхода.");
    keyInfo = Console.ReadKey();
    if (keyInfo.Key == ConsoleKey.F1)
    {
        Console.WriteLine("Введите новый формат файла (txt, json, xml):");
        string newFormat = Console.ReadLine();
        fileManager.SaveFile(loadedFigure, newFormat);
        Console.WriteLine($"Файл сохранён в формате {newFormat}.");
    }
} while (keyInfo.Key != ConsoleKey.Escape);