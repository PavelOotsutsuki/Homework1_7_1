using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Homework1_7_1
{
    class Program
    {
        static void Main(string[] args)
        {
            SearchBureau searchBureau = new SearchBureau();
            searchBureau.Work();
        }
    }

    class Criminal
    {
        public Criminal (string fullName, bool isCaught, int height, float weight, string nationality) 
        {
            FullName = fullName;
            IsCaught = isCaught;
            Height = height;
            Weight = weight;
            Nationality = nationality;
        }

        public string FullName { get; private set; }
        public bool IsCaught { get; private set; }
        public int Height { get; private set; }
        public float Weight { get; private set; }
        public string Nationality { get; private set; }
    }

    class SearchBureau
    {
        private List<Criminal> _criminals;
        private IEnumerable<Criminal> _filtredCriminals;
        private bool _isSearched;

        public SearchBureau()
        {
            _criminals = new List<Criminal>()
            {
                new Criminal("Майкл Джордан Андреевич", false, 180, 78.2f, "Русский"),
                new Criminal("Жерар ДеПортье", true, 174, 98.1f, "Француз"),
                new Criminal("Георг Лейтерт", false, 180, 78.2f, "Румын"),
                new Criminal("Андрей Джордан Майклович", true, 180, 78.2f, "Русский"),
                new Criminal("Эрик Леншерр", false, 184, 74, "Немец"),
                new Criminal("Эмбер Херд", false, 170, 49.6f, "Американец")
            };

            _isSearched = true;
        }

        public void Work()
        {
            const string ResetFiltersCommand = "0";
            const string SearchByHeightCommand = "1";
            const string SearchByWeightCommand = "2";
            const string SearchByNationalityCommand = "3";
            const string ExitCommand = "4";

            bool isWork = true;
            ResetFilters();

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine("БЮРО ПОИСКА\n");
                Console.WriteLine($"{ResetFiltersCommand}. Сбросить все фильтры");
                Console.WriteLine($"{SearchByHeightCommand}. Поиск по росту");
                Console.WriteLine($"{SearchByWeightCommand}. Поиск по весу");
                Console.WriteLine($"{SearchByNationalityCommand}. Поиск по национальности");
                Console.WriteLine($"{ExitCommand}. Завершить работу");
                Console.Write("\nВведите номер команды: ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case ResetFiltersCommand:
                        ResetFilters();
                        break;

                    case SearchByHeightCommand:
                        SearchByHeight();
                        break;

                    case SearchByWeightCommand:
                        SearchByWeight();
                        break;

                    case SearchByNationalityCommand:
                        SearchByNationality();
                        break;

                    case ExitCommand:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Введена неверная команда");
                        break;
                }

                Console.ReadKey();
            }
        }

        private void SearchByHeight()
        {
            InputRange(out int startHeight, out int endHeight);

            _filtredCriminals = _filtredCriminals.Where(criminal => criminal.Height >= startHeight && criminal.Height <= endHeight && criminal.IsCaught == false);
            _isSearched = true;
            OutputFiltredCriminals();
        }

        private void SearchByWeight()
        {
            InputRange(out float startWeight, out float endWeight);

            _filtredCriminals = _filtredCriminals.Where(criminal => criminal.Weight >= startWeight && criminal.Weight <= endWeight && criminal.IsCaught == false);
            _isSearched = true;
            OutputFiltredCriminals();
        }

        private void SearchByNationality()
        {
            Console.Write("Введите национальность: ");
            string userInput = Console.ReadLine();

            _filtredCriminals = _filtredCriminals.Where(criminal => criminal.Nationality == userInput && criminal.IsCaught == false);
            _isSearched = true;
            OutputFiltredCriminals();
        }

        private void OutputFiltredCriminals()
        {
            foreach (var criminal in _filtredCriminals)
            {
                Console.WriteLine(criminal.FullName);
            }
        }

        private void ResetFilters()
        {
            if (_isSearched)
            {
                _filtredCriminals = _criminals.Select(criminal => criminal);
                _isSearched = false;
            }
            else
            {
                Console.WriteLine("Фильтров нет");
            }
        }

        private void InputRange(out int start, out int end)
        {
            Console.WriteLine("Введите диапозон включительно:");
            Console.Write("От: ");
            start= UserUtilits.GetIntInputWithErrorMessage("Введено некорректное значение");
            Console.Write("До: ");
            end = UserUtilits.GetIntInputWithErrorMessage("Введено некорректное значение");
        }

        private void InputRange(out float start, out float end)
        {
            Console.WriteLine("Введите диапозон включительно:");
            Console.Write("От: ");
            start = UserUtilits.GetFloatInputWithErrorMessage("Введено некорректное значение");
            Console.Write("До: ");
            end = UserUtilits.GetFloatInputWithErrorMessage("Введено некорректное значение");
        }
    }

    class UserUtilits
    {
        public static int GetIntInputWithErrorMessage(string errorMessage)
        {
            int leftPosition = Console.CursorLeft;
            int topPosition = Console.CursorTop;
            int input;

            while (int.TryParse(Console.ReadLine(), out input) == false)
            {
                Console.Write($"\n{errorMessage}");
                Console.ReadKey();
                Console.SetCursorPosition(0, Console.CursorTop + 1);

                ClearConsoleAfterPosition(leftPosition, topPosition);
            }

            return input;
        }

        public static float GetFloatInputWithErrorMessage(string errorMessage)
        {
            int leftPosition = Console.CursorLeft;
            int topPosition = Console.CursorTop;
            float input;

            while (float.TryParse(Console.ReadLine(), out input) == false)
            {
                Console.Write($"\n{errorMessage}");
                Console.ReadKey();
                Console.SetCursorPosition(0, Console.CursorTop + 1);

                ClearConsoleAfterPosition(leftPosition, topPosition);
            }

            return input;
        }

        private static void ClearConsoleAfterPosition(int leftPosition, int topPosition)
        {
            for (int j = Console.CursorTop; j > topPosition; j--)
            {
                for (int i = Console.CursorLeft; i >= 0; i--)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(" ");
                }

                Console.CursorLeft = Console.WindowWidth;
            }

            for (int i = Console.CursorLeft; i >= leftPosition; i--)
            {
                Console.SetCursorPosition(i, topPosition);
                Console.Write(" ");
            }

            Console.SetCursorPosition(leftPosition, topPosition);
        }
    }
}