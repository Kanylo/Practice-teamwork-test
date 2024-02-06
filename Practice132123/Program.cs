using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
class Program
{
    const char DisplayMathReferenceChoice = '1';

    static List<MathReferenceItem> mathReferenceList = new List<MathReferenceItem>();

    static void Main()
    {
        while (true)
        {
            LoadMathReference();
            Console.Clear();
            DisplayMenu();
            char choice = GetChoice();
            switch (choice)
            {
                case '1':
                    DisplayMathReference();
                    break;
                case '2':
                    AddMathReference();
                    break;
                case '3':
                    EditMathReference();
                    break;
                case '4':
                    DeleteMathReference();
                    break;
                case '5':
                    SortMathReference();
                    break;
                case '6':
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }

            Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }
    }


    static void LoadMathReference()
    {
        if (File.Exists("MathReference.json"))
        {
            string jsonData = File.ReadAllText("MathReference.json");
            mathReferenceList = JsonConvert.DeserializeObject<List<MathReferenceItem>>(jsonData);
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("1. Показати математичний довідник");
        Console.WriteLine("2. Додати новий запис");
        Console.WriteLine("3. Редагувати запис");
        Console.WriteLine("4. Видалити запис");
        Console.WriteLine("5. Сортувати");
        Console.WriteLine("6. Вийти");
    }

    static char GetChoice()
    {
        Console.Write("Введіть ваш вибір: ");
        char choice = Console.ReadKey().KeyChar;
        Console.WriteLine();
        return choice;
    }

    static void DisplayMathReference()
    {
        Console.Clear();
        Console.WriteLine("Математичний довідник:");

        if (mathReferenceList.Count == 0)
        {
            Console.WriteLine("Довідник порожній.");
        }
        else
        {
            int index = 1;
            foreach (var item in mathReferenceList)
            {
                Console.WriteLine($"{index}. {item.Topic}");
                index++;
            }

            Console.Write("Введіть номер запису для перегляду або натисніть Enter для повернення до меню: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int displayIndex) && displayIndex > 0 && displayIndex <= mathReferenceList.Count)
            {
                DisplayMathReferenceItem(displayIndex - 1);
            }
            else if (!string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Невірний номер запису.");
            }
        }
    }

    static void DisplayMathReferenceItem(int index)
    {
        var selectedMathReference = mathReferenceList[index];
        Console.WriteLine($"Тема: {selectedMathReference.Topic}");
        Console.WriteLine($"Опис: {selectedMathReference.Description}");
    }


    static void AddMathReference()
    {
        Console.Clear();
        Console.Write("Введіть тему: ");
        string topic = Console.ReadLine();
        Console.Write("Введіть опис: ");
        string description = Console.ReadLine();

        mathReferenceList.Add(new MathReferenceItem { Topic = topic, Description = description });
        Console.WriteLine("Запис успішно додано!");
        SaveMathReference();
    }
    static void SaveMathReference()
    {
        string jsonData = JsonConvert.SerializeObject(mathReferenceList);
        File.WriteAllText("MathReference.json", jsonData);
    }
    static void EditMathReference()
    {
        DisplayMathReference();
        Console.Write("Введіть номер запису для редагування: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= mathReferenceList.Count)
        {
            Console.Write("Введіть нову тему: ");
            string newTopic = Console.ReadLine();
            Console.Write("Введіть новий опис: ");
            string newDescription = Console.ReadLine();

            mathReferenceList[index - 1] = new MathReferenceItem { Topic = newTopic, Description = newDescription };
            Console.WriteLine("Запис успішно відредаговано!");
            SaveMathReference();  // Save changes 
        }
        else
        {
            Console.WriteLine("Невірний номер запису.");
        }
    }

    static void DeleteMathReference()
    {
        DisplayMathReference();
        Console.Write("Введіть номер запису для видалення: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= mathReferenceList.Count)
        {
            mathReferenceList.RemoveAt(index - 1);
            Console.WriteLine("Запис успішно видалено!");
            SaveMathReference();  // Save changes 
        }
        else
        {
            Console.WriteLine("Невірний номер запису.");
        }
    }

    static void SortMathReference()
    {
        mathReferenceList = mathReferenceList.OrderBy(item => item.Topic).ToList();
        Console.WriteLine("Довідник відсортовано за темою.");
        SaveMathReference();  // Save changes 
    }
    }

    class MathReferenceItem
{
    public string Topic { get; set; }
    public string Description { get; set; }
}
//какатест