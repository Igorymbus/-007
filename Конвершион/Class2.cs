using Newtonsoft.Json;
using System.Xml.Serialization;
using TextEditor;

class FileHandler
{
    private string filePath;

    public FileHandler(string filePath)
    {
        this.filePath = filePath;
    }

    public Figure LoadFile()
    {
        try
        {
            string extension = Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(extension))
            {
                Console.WriteLine("Неверное расширение файла.");
                return null;
            }

            switch (extension.ToLower())
            {
                case ".txt":
                    return LoadTxtFile();
                case ".json":
                    return LoadJsonFile();
                case ".xml":
                    return LoadXmlFile();
                default:
                    Console.WriteLine("Неподдерживаемый формат файла.");
                    return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при чтении файла: {ex.Message}");
            return null;
        }
    }

    private Figure LoadTxtFile()
    {
        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length < 3)
        {
            Console.WriteLine("Неверный формат файла.");
            return null;
        }

        string name = lines[0];
        if (int.TryParse(lines[1], out int width) && int.TryParse(lines[2], out int height))
        {
            return new Figure(name, width, height);
        }
        else
        {
            Console.WriteLine("Неверные значения ширины и высоты в файле.");
            return null;
        }
    }

    private Figure LoadJsonFile()
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<Figure>(json);
    }

    private Figure LoadXmlFile()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Figure));
        using (StringReader stringReader = new StringReader(File.ReadAllText(filePath)))
        {
            return (Figure)xmlSerializer.Deserialize(stringReader);
        }
    }

    public void SaveFile(Figure figure)
    {
        try
        {
            string extension = Path.GetExtension(filePath);

            if (string.IsNullOrEmpty(extension))
            {
                Console.WriteLine("Неверное расширение файла.");
                return;
            }

            switch (extension.ToLower())
            {
                case ".txt":
                    SaveTxtFile(figure);
                    break;
                case ".json":
                    SaveJsonFile(figure);
                    break;
                case ".xml":
                    SaveXmlFile(figure);
                    break;
                default:
                    Console.WriteLine("Неподдерживаемый формат файла.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при записи файла: {ex.Message}");
        }
    }

    private void SaveTxtFile(Figure figure)
    {
        string[] lines = { figure.Name, figure.Width.ToString(), figure.Height.ToString() };
        File.WriteAllLines(filePath, lines);
    }

    private void SaveJsonFile(Figure figure)
    {
        string json = JsonConvert.SerializeObject(figure, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    private void SaveXmlFile(Figure figure)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Figure));
        using (StringWriter stringWriter = new StringWriter())
        {
            xmlSerializer.Serialize(stringWriter, figure);
            File.WriteAllText(filePath, stringWriter.ToString());
        }
    }

    internal void SaveFile(Figure? loadedFigure, string? newFormat)
    {
       
        if (loadedFigure == null || string.IsNullOrEmpty(newFormat))
        {
            Console.WriteLine("Фигура или формат файла не указаны.");
            return;
        }

       
        string newFilePath = Path.ChangeExtension(filePath, newFormat);

        
        FileHandler newFileHandler = new FileHandler(newFilePath);

        try
        {
            
            newFileHandler.SaveFile(loadedFigure);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при сохранении файла: {ex.Message}");
        }
    }
}
