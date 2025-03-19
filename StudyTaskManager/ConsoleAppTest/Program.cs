namespace ConsoleAppTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime __timeStart = DateTime.Now; Console.WriteLine($"Тестовый проект просто для проверки реализации.\nНачало работы: {__timeStart}\n------------------------\n");





            DateTime __timeEnd = DateTime.Now; Console.WriteLine($"\n------------------------\nКонец работы: {__timeEnd}\nВремя работы: {__timeEnd - __timeStart}");
        }
    }
}
