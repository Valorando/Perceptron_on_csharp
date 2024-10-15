using System.Text;

class Perceptron
{
    private double[] weights;  // Ваги для кожного входу
    private double bias;       // Зсув
    private double learningRate = 0.1;  // Швидкість навчання

    // Конструктор, який ініціалізує ваги та зсув випадковими значеннями
    public Perceptron()
    {
        weights = new double[3]; // Три входи
        Random rand = new Random();
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = rand.NextDouble();
        }
        bias = rand.NextDouble();
    }

    // Активаційна функція (сходинкова)
    private int ActivationFunction(double input)
    {
        return input >= 0 ? 1 : 0;
    }

    // Функція передбачення (обчислення виходу)
    public int Predict(int x1, int x2, int x3)
    {
        double totalInput = x1 * weights[0] + x2 * weights[1] + x3 * weights[2] + bias;
        return ActivationFunction(totalInput);
    }

    // Навчання перцептрона на одному кроці
    public void Train(int x1, int x2, int x3, int expected)
    {
        int prediction = Predict(x1, x2, x3);
        int error = expected - prediction;

        // Оновлення ваг та зсуву за правилом навчання
        weights[0] += learningRate * error * x1;
        weights[1] += learningRate * error * x2;
        weights[2] += learningRate * error * x3;
        bias += learningRate * error;
    }

    // Функція для перевірки входів
    public static bool CheckInput(int value)
    {
        if (value != 0 && value != 1)
        {
            Console.WriteLine("Програма завершена.");
            return false;
        }
        return true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Установка кодировки для поддержки украинских букв
        Console.OutputEncoding = Encoding.UTF8;

        Perceptron perceptron = new Perceptron();

        // Навчальна вибірка для операції OR з трьома входами
        int[][] inputs =
        {
            new int[] { 0, 0, 0 },
            new int[] { 0, 0, 1 },
            new int[] { 0, 1, 0 },
            new int[] { 1, 0, 0 },
            new int[] { 0, 1, 1 },
            new int[] { 1, 0, 1 },
            new int[] { 1, 1, 0 },
            new int[] { 1, 1, 1 }
        };

        // Очікувані результати для OR з трьома входами
        int[] outputs = { 0, 1, 1, 1, 1, 1, 1, 1 };

        // Навчання перцептрона
        for (int epoch = 0; epoch < 1000; epoch++) // 1000 епох навчання
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                perceptron.Train(inputs[i][0], inputs[i][1], inputs[i][2], outputs[i]);
            }
        }

        // Тестування
        Console.WriteLine("Тестування перцептрона для операції OR з трьома входами:");
        foreach (var userInput in inputs)
        {
            int result = perceptron.Predict(userInput[0], userInput[1], userInput[2]);
            Console.WriteLine($"Вхід: {userInput[0]}, {userInput[1]}, {userInput[2]} -> Вихід: {result}");
        }

        while (true)
        {
            Console.WriteLine("\nВведіть нові значення для прогнозу (0 або 1). Для виходу введіть будь-яке інше значення.");

            try
            {
                Console.Write("Введіть перше значення (x1): ");
                int x1 = Convert.ToInt32(Console.ReadLine());
                if (!Perceptron.CheckInput(x1)) break;

                Console.Write("Введіть друге значення (x2): ");
                int x2 = Convert.ToInt32(Console.ReadLine());
                if (!Perceptron.CheckInput(x2)) break;

                Console.Write("Введіть третє значення (x3): ");
                int x3 = Convert.ToInt32(Console.ReadLine());
                if (!Perceptron.CheckInput(x3)) break;

                // Прогнозування для введених користувачем значень
                int predictedResult = perceptron.Predict(x1, x2, x3);
                Console.WriteLine($"Прогнозоване значення для [{x1}, {x2}, {x3}]: {predictedResult}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Програма завершена.");
                break;
            }
        }
    }
}