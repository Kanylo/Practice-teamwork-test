﻿using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<MathReferenceItem> mathReferenceList = new List<MathReferenceItem>();

    static void Main()
    {
        while (true)
        {
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
        return Console.ReadKey().KeyChar;
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
            foreach (var item in mathReferenceList)
            {
                Console.WriteLine($"{item.Topic}: {item.Description}");
            }
        }
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
    }
}

class MathReferenceItem
{
    public string Topic { get; set; }
    public string Description { get; set; }
}