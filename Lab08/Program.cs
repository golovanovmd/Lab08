using System.IO;
using System;
using System.Xml.Serialization;
using AnimalLibrary;

class AnimalConsoleApp
{
    static void Main(string[] args)
    {
        //Сериализация представляет собой процесс преобразования состояния объекта в форму, пригодную для сохранения или передачи.
        
        Animal animal = new Cow("USA", "Bessie", "A friendly cow", false);
        //экземпляр создается с использованием конструктора Cow, который принимает аргументы USA, Bessie, A friendly cow и false.
        //Создается экземпляр класса XmlSerializer, который будет использоваться для сериализации и десериализации объектов типа Animal
        XmlSerializer serializer = new XmlSerializer(typeof(Animal));
        //для создания экземпляра класса StreamWriter, который будет использоваться для записи сериализованного объекта в файл с именем "animal.xml"
        using (TextWriter writer = new StreamWriter("animal.xml"))
        {//Вызывается метод Serialize объекта serializer, чтобы сериализовать объект animal и записать его в файл с использованием объекта writer
            serializer.Serialize(writer, animal);
        }

        Console.WriteLine("Animal object has been serialized to animal.xml");

        using (TextReader reader = new StreamReader("animal.xml"))
        {//для создания экземпляра класса StreamReader, который будет использоваться для чтения сериализованного объекта из файла "animal.xml"
            Animal deserializedAnimal = (Animal)serializer.Deserialize(reader);
            //Вызывается метод Deserialize объекта serializer для десериализации объекта из файла с использованием объекта reader
            Console.WriteLine("\nDeserialized Animal Object:");
            Console.WriteLine($"Name: {deserializedAnimal.Name}");
            Console.WriteLine($"Country: {deserializedAnimal.Country}");
            Console.WriteLine($"Description: {deserializedAnimal.Description}");
            Console.WriteLine($"HideFromOtherAnimals: {deserializedAnimal.HideFromOtherAnimals}");
            //Вызывается метод SayHello() на десериализованном объекте deserializedAnimal, чтобы он вывел приветствие в консоль
            deserializedAnimal.SayHello();
            Console.WriteLine($"Favorite Food: {deserializedAnimal.GetFavouriteFood()}");
        }
    }
}