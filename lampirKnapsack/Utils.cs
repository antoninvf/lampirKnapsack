namespace lampirKnapsack;

public static class Utils
{
    public static List<ulong> GenerateSuperIncreasingSequence(ulong length)
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

    public static ulong MaxSieveOfEratosthenes(ulong n)
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

    public static ulong GCD(ulong a, ulong b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }
}