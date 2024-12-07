using System;
using System.IO;
using System.IO.Compression;

class lab082


{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите путь к папке, в которой нужно выполнить поиск:");
        string searchDirectory = Console.ReadLine();

        Console.WriteLine("Введите имя файла для поиска (с расширением, например, example.txt):");
        string targetFileName = Console.ReadLine();
        //использует метод GetFiles из класса Directory для поиска файлов в указанной папке(searchDirectory)
        //Он использует значение targetFileName в качестве шаблона имени файла и SearchOption.AllDirectories для поиска 
        //во всех подпапках. Результаты поиска сохраняются в массиве строк files
        string[] files = Directory.GetFiles(searchDirectory, targetFileName, SearchOption.AllDirectories);

        if (files.Length == 0)
        {
            Console.WriteLine("Файл не найден.");
        }
        else
        {
            foreach (string filePath in files)
            {
                Console.WriteLine($"Найден файл: {filePath}");
                //Этот код открывает файл на чтение, используя класс `File` и метод `OpenRead()`, и создает объект `FileStream`,
                //который представляет поток для чтения файла
                using (FileStream fileStream = File.OpenRead(filePath))
                {//Этот код создает объект `StreamReader`, который обеспечивает чтение текста из объекта `FileStream` (`fileStream`)
                    using (StreamReader reader = new StreamReader(fileStream))
                    {//Этот код использует метод `ReadToEnd()` объекта `StreamReader` для чтения содержимого файла и присваивает
                     //его значение переменной `fileContent`
                        string fileContent = reader.ReadToEnd();
                        Console.WriteLine($"Содержимое файла:\n{fileContent}");
                    }
                }
                //тот код использует метод `ChangeExtension()` из класса `Path` для изменения расширения файла (`filePath`)
                //на ".gz" и сохраняет новый путь к файлу в переменной `compressedFilePath`
                string compressedFilePath = Path.ChangeExtension(filePath, ".gz");
                //открывает исходный файл на чтение, используя метод `OpenRead()` из класса `File`, и создает объект `FileStream` (`originalFileStream`)
                using (FileStream originalFileStream = File.OpenRead(filePath))
                {//создает файл для записи, используя метод `Create()` из класса `File`, и создает объект `FileStream`
                 //(`compressedFileStream`), который представляет поток для записи сжатого файла
                    using (FileStream compressedFileStream = File.Create(compressedFilePath))
                    {//создает объект `GZipStream`, который обеспечивает сжатие данных при их записи в объект `compressedFileStream`.
                     //Он использует режим сжатия `CompressionMode.Compress`
                        using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                        {
                            originalFileStream.CopyTo(compressionStream);
                            Console.WriteLine($"Файл сжат и сохранен как: {compressedFilePath}");
                        }
                    }
                }
            }
        }
    }
}