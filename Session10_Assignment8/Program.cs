using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Session10_Assignment8
{
    // ==========================================
    // 1. Optimized Bubble Sort Algorithm
    // ==========================================
    public static class SortingHelper
    {
        public static void OptimizedBubbleSort<T>(T[] array) where T : IComparable<T>
        {
            int n = array.Length;
            bool swapped;

            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;

                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        // Swap
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        swapped = true;
                    }
                }

                // If no two elements were swapped by inner loop, the array is sorted
                if (!swapped)
                    break;
            }
        }
    }

    // ==========================================
    // 2. Generic Range<T> Class
    // ==========================================
    public class Range<T> where T : IComparable<T>
    {
        public T Min { get; }
        public T Max { get; }

        public Range(T min, T max)
        {
            if (min.CompareTo(max) > 0)
                throw new ArgumentException("Minimum value cannot be greater than maximum value.");

            Min = min;
            Max = max;
        }

        public bool IsInRange(T value)
        {
            return value.CompareTo(Min) >= 0 && value.CompareTo(Max) <= 0;
        }

        public dynamic Length()
        {
            try
            {
                return (dynamic)Max - (dynamic)Min;
            }
            catch
            {
                throw new InvalidOperationException("Length calculation is not supported for this type T.");
            }
        }
    }

    // ==========================================
    // 3. ArrayList In-Place Reverse Helper
    // ==========================================
    public static class ArrayListHelper
    {
        public static void ReverseArrayList(ArrayList list)
        {
            if (list == null || list.Count <= 1) return;

            int left = 0;
            int right = list.Count - 1;

            while (left < right)
            {
                object temp = list[left];
                list[left] = list[right];
                list[right] = temp;

                left++;
                right--;
            }
        }
    }

    // ==========================================
    // 4. Even Numbers Filter Helper
    // ==========================================
    public static class ListHelper
    {
        public static List<int> GetEvenNumbers(List<int> numbers)
        {
            return numbers.Where(n => n % 2 == 0).ToList();
        }
    }

    // ==========================================
    // 5. Custom FixedSizeList<T> Class
    // ==========================================
    public class FixedSizeList<T>
    {
        private readonly List<T> _items;
        public int Capacity { get; }
        public int Count => _items.Count;

        public FixedSizeList(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero.");

            Capacity = capacity;
            _items = new List<T>(capacity);
        }

        public void Add(T item)
        {
            if (_items.Count >= Capacity)
            {
                throw new InvalidOperationException("Error: Cannot add element. The list has reached its maximum capacity.");
            }
            _items.Add(item);
        }

        public T Get(int index)
        {
            if (index < 0 || index >= _items.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Error: Invalid index or index out of range.");
            }
            return _items[index];
        }
    }

    // ==========================================
    // 6. First Non-Repeated Character Helper
    // ==========================================
    public static class StringHelper
    {
        public static int FirstNonRepeatedCharacterIndex(string str)
        {
            if (string.IsNullOrEmpty(str)) return -1;

            Dictionary<char, int> charCounts = new Dictionary<char, int>();

            foreach (char c in str)
            {
                if (charCounts.ContainsKey(c))
                    charCounts[c]++;
                else
                    charCounts[c] = 1;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (charCounts[str[i]] == 1)
                {
                    return i;
                }
            }

            return -1;
        }
    }

    // ==========================================
    // Program Execution & Testing
    // ==========================================
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Testing Optimized Bubble Sort
            Console.WriteLine("=== 1. Optimized Bubble Sort ===");
            int[] numbers = { 64, 34, 25, 12, 22, 11, 90 };
            SortingHelper.OptimizedBubbleSort(numbers);
            Console.WriteLine($"Sorted Array: {string.Join(", ", numbers)}\n");

            // 2. Testing Range<T>
            Console.WriteLine("=== 2. Range<T> ===");
            Range<int> intRange = new Range<int>(10, 50);
            Console.WriteLine($"Is 30 in range [10, 50]? {intRange.IsInRange(30)}");
            Console.WriteLine($"Range Length: {intRange.Length()}\n");

            // 3. Testing ArrayList In-Place Reverse
            Console.WriteLine("=== 3. ArrayList Reverse ===");
            ArrayList arrayList = new ArrayList() { 10, 20, 30, 40, 50 };
            ArrayListHelper.ReverseArrayList(arrayList);
            Console.Write("Reversed ArrayList: ");
            foreach (var item in arrayList) Console.Write($"{item} ");
            Console.WriteLine("\n");

            // 4. Testing Even Numbers Filter
            Console.WriteLine("=== 4. Even Numbers ===");
            List<int> intList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var evenNumbers = ListHelper.GetEvenNumbers(intList);
            Console.WriteLine($"Even Numbers: {string.Join(", ", evenNumbers)}\n");

            // 5. Testing FixedSizeList<T>
            Console.WriteLine("=== 5. FixedSizeList<T> ===");
            FixedSizeList<string> fixedList = new FixedSizeList<string>(3);
            fixedList.Add("C#");
            fixedList.Add("Advanced");
            fixedList.Add("DEPI");
            Console.WriteLine($"Element at index 1: {fixedList.Get(1)}");

            // Testing capacity exception handling
            try
            {
                fixedList.Add("Overflow");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught Expected Exception: {ex.Message}");
            }
            Console.WriteLine();

            // 6. Testing First Non-Repeated Character
            Console.WriteLine("=== 6. First Non-Repeated Character ===");
            string testStr = "swiss";
            int index = StringHelper.FirstNonRepeatedCharacterIndex(testStr);
            Console.WriteLine($"First non-repeated character in '{testStr}' is at index: {index} (Character: '{testStr[index]}')");
        }
    }
}