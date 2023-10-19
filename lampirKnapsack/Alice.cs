namespace lampirKnapsack;

public class Alice
{
    static ulong vMinus(ulong v, ulong u)
    {
        for (ulong i = 1; i < u - 1; i++)
            if (v * i % u == 1)
                return i;
        return 0;
    }

    static List<ulong> GenerateSuperIncreasingSequence(ulong length)
    {
        ulong randomAdd = (ulong)new Random().NextInt64(1, 21);
        List<ulong> finalOutput = new List<ulong>();
        for (ulong i = 1 + randomAdd; i < length + 1 + randomAdd; i++)
        {
            ulong addNumber = 0;
            for (ulong j = 0; j < (ulong)finalOutput.Count; j++)
            {
                addNumber += finalOutput[(int)j];
            }

            finalOutput.Add(i + addNumber);
        }

        return finalOutput;
    }

    static ulong MaxSieveOfEratosthenes(ulong n)
    {
        bool[] prime = new bool[n + 1];

        for (ulong i = 0; i <= n; i++)
            prime[i] = true;

        for (ulong p = 2; p * p <= n; p++)
        {
            if (prime[p])
            {
                for (ulong i = p * p; i <= n; i += p)
                    prime[i] = false;
            }
        }

        List<ulong> primes = new List<ulong>();

        for (ulong i = 2; i <= n; i++)
        {
            if (prime[i])
                primes.Add(i);
        }

        return primes.Last();
    }

    static ulong GCD(ulong a, ulong b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }
    public static void Logic()
    {
        Console.WriteLine("Hello Alice.\n");

        const int spSize = 16;
        var AlicePrivate = GenerateSuperIncreasingSequence(spSize);

        ulong sumOfAllA = AlicePrivate.Aggregate((a, c) => a + c);
        //Console.WriteLine($"AlicePrivate: [{string.Join(", ", AlicePrivate)}] | Sum: {sumOfAllA}");

        var u = MaxSieveOfEratosthenes((ulong)new Random().NextInt64((long)sumOfAllA, (long)sumOfAllA + (long)AlicePrivate[4]));
        var v = MaxSieveOfEratosthenes((ulong)new Random().NextInt64(1, (long)u));
        var vm1 = vMinus(v, u);
        //Console.WriteLine($"u = {u} | v = {v} -> GCD(u, v) = {Utils.GCD(u, v)}");

        List<ulong> AlicePublic = new List<ulong>();

        for (int i = 0; i < spSize; i++)
        {
            var count = AlicePrivate[i] * v;
            if (count > u) AlicePublic.Add(count % u);
            else AlicePublic.Add(count);
        }

        Console.WriteLine($"Alice's Public Key:\n[{string.Join(", ", AlicePublic)}]");

        Console.WriteLine("Enter Bob's encrypted message: ");
        string? input = Console.ReadLine();

        var Dt = new List<ulong>();
        foreach (var sum in input.Split(","))
        {
            var Et = ulong.Parse(sum);
            Dt.Add(Et * vm1 % u);
        }

        AlicePrivate.Reverse();

        var binaryList = new List<string>();

        foreach (var d in Dt)
        {
            var temp = "";
            var current = d;
            foreach (var ap in AlicePrivate)
            {
                if (current >= ap)
                {
                    temp += "1";
                    current -= ap;
                }
                else if (current < ap)
                {
                    temp += "0";
                }
            }

            char[] stringArray = temp.ToCharArray();
            Array.Reverse(stringArray);
            string reversedStr = new string(stringArray);
            binaryList.Add(reversedStr);
        }

        string result = "";
        //Console.WriteLine($"binary: [{string.Join(", ", binaryList)}]");

        var eightbitBinary = new List<string>();

        foreach (var sixteenbitbinary in binaryList)
        {
            eightbitBinary.Add(sixteenbitbinary.Substring(0, 8));
            eightbitBinary.Add(sixteenbitbinary.Substring(8));
        }

        foreach (var binary in eightbitBinary)
        {
            int num = Convert.ToInt32(binary, 2);
            char character = Convert.ToChar(num);
            result += character;
        }

        Console.WriteLine($"Decrypted message:\n{result}");
    }
}