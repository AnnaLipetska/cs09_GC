using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task2
{
    class Program
    {
        // Создайте класс, который позволит выполнять мониторинг ресурсов, используемых программой.
        // Используйте его в целях наблюдения за работой программы, а именно: пользователь может
        // указать приемлемые уровни потребления ресурсов(памяти), а методы класса позволят выдать
        // предупреждение, когда количество реально используемых ресурсов приблизиться к
        // максимально допустимому уровню.

        class Watcher
        {
            int checksCounter = 0;
            readonly int acceptableMemorySize;

            public Watcher(int memorySize)
            {
                acceptableMemorySize = memorySize;
            }

            public void CheckMemory(object o)
            {
                Console.WriteLine(++checksCounter);
                if (GC.GetTotalMemory(false) > acceptableMemorySize)
                {
                    Console.WriteLine("Houston, we have a problem");
                    Thread.Sleep(1000);
                }
            }
        }

        static void Main(string[] args)
        {
            var watcher = new Watcher(40000000);
            var timer = new Timer(watcher.CheckMemory, new object(), 0, 100);

            ArrayWrapper[] arrayOfArrays = new ArrayWrapper[10000];

            for (int i = 0; i < arrayOfArrays.Length; i++)
            {
                var arr = new ArrayWrapper();
            }
        }
    }

    class ArrayWrapper
    {
        int[] array = new int[1000000];
        public ArrayWrapper()
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }
        }

        public int this[int index]
        {
            get { return array[index]; }
            set { array[index] = value; }
        }

        public int Length 
        {
            get { return array.Length; }
        }
    }
}
