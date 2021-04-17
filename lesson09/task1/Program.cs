using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    // Создайте свой класс, объекты которого будут занимать много места в памяти (например, в
    // коде класса будет присутствовать большой массив) и реализуйте для этого класса,
    // формализованный шаблон очистки
    public class MyResourceWrapper : IDisposable
    {
        // Используется для выяснения того, вызывался ли уже метод Dispose()
        private bool disposed = false;
        int[] bigArray;

        public MyResourceWrapper()
        {
            bigArray = new int[1000];
            for (int i = 0; i < bigArray.Length; i++)
            {
                bigArray[i] = i;
            }
        }


        public void Dispose()
        {
            // Вызов вспомогательного метода.
            // Значение true указывает на то, что очистка
            // была инициирована пользователем объекта.
            Cleanup(true);
            // Подавление финализации.
            GC.SuppressFinalize(this);
        }

        private void Cleanup(bool disposing)
        {
            // Проверка, выполнялась ли очистка,
            if (!this.disposed)
            {
                // Если disposing равно true, должно осуществляться
                // освобождение всех управляемых ресурсов,
                if (disposing)
                {
                    // Здесь осуществляется освобождение управляемых ресурсов.
                    bigArray = null;
                    Console.WriteLine("Освобождаем управляемые ресурсы.");
                }
                // Очистка неуправляемых ресурсов.
                Console.WriteLine("Закрываем соединение с базой.");
            }
            disposed = true;
        }

        ~MyResourceWrapper()
        {
            // Вызов вспомогательного метода.
            // Значение false указывает на то, что
            // очистка была инициирована сборщиком мусора.
            Cleanup(false);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Проверка работы конструкции using:");
            using (var myResourceWrapper = new MyResourceWrapper())
            {
                Console.WriteLine("Используем экземпляр нашего класса");
            }

            Console.WriteLine(new string('-', 40));

            Console.WriteLine("Проверка ручного вызова метода Dispose:");
            var wrapper = new MyResourceWrapper();

            wrapper.Dispose();
            wrapper.Dispose();

            Console.WriteLine(new string('-', 40));

            Console.WriteLine("Проверка работы деструктора:");

            var wrapper4Finalizing = new MyResourceWrapper();

            //Задержка
            Console.ReadKey();
        }
    }
}
