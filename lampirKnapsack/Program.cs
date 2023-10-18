using System.Text;

namespace lampirKnapsack;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Lampir Alice and Bob HW:");
        Console.WriteLine("Are you Alice(A) or Bob(B)?");
        var correctChoice = false;
        while (!correctChoice)
        {
            Console.Write("> Enter A or B:");
            string? choice = Console.ReadLine();
            if (choice?.ToLower() is "a")
            {
                Alice();
                correctChoice = true;
            }

            if (choice?.ToLower() is "b")
            {
                Bob();
                correctChoice = true;
            }
        }
    }

    static ulong vMinus(ulong v, ulong u)
    {
        for (ulong i = 1; i < u - 1; i++)
            if (v * i % u == 1)
                return i;
        return 0;
    }

    static void Alice()
    {
        Console.WriteLine("Hello Alice.\n");

        const int spSize = 16;
        var AlicePrivate = Utils.GenerateSuperIncreasingSequence(spSize);

        ulong sumOfAllA = AlicePrivate.Aggregate((a, c) => a + c);
        //Console.WriteLine($"AlicePrivate: [{string.Join(", ", AlicePrivate)}] | Sum: {sumOfAllA}");

        var u = Utils.MaxSieveOfEratosthenes((ulong)new Random().NextInt64((long)sumOfAllA, (long)sumOfAllA + (long)AlicePrivate[4]));
        var v = Utils.MaxSieveOfEratosthenes((ulong)new Random().NextInt64(1, (long)u));
        var vm1 = vMinus(v, u);
        //Console.WriteLine($"u = {u} | v = {v} -> GCD(u, v) = {Utils.GCD(u, v)}");

        List<ulong> AlicePublic = new List<ulong>();

        for (int i = 0; i < spSize; i++)
        {
            var count = AlicePrivate[i] * v;
            if (count > u) AlicePublic.Add(count % u);
            else AlicePublic.Add(count);
        }

        Console.WriteLine($"AlicePublic:\n[{string.Join(", ", AlicePublic)}]");

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
                } else if (current < ap)
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
            eightbitBinary.Add(sixteenbitbinary.Substring(0,8));
            eightbitBinary.Add(sixteenbitbinary.Substring(8));
        }
        
        foreach (var binary in eightbitBinary)
        {
            int num = Convert.ToInt32(binary, 2);
            char character = Convert.ToChar(num);
            result += character;
        }

        Console.WriteLine($"Decrypted message:\n${result}");
    }

    static void Bob()
    {
        Console.WriteLine("Hello Bob.\n");

        Console.Write("> Enter Alice's Public Key: ");
        var AlicePublic = Console.ReadLine();
        if (AlicePublic == null) return;

        Console.Write("> Enter your message: ");
        var input = Console.ReadLine();
        if (input == null) return;

        var asciiList = new List<long>();

        // example: "o" -> 111
        input.ToCharArray().ToList().ForEach(x => asciiList.Add(x));

        //Console.WriteLine($"ascii: [{string.Join(", ", asciiList)}]");

        var bitsList = new List<string>();
        asciiList.ForEach(x => bitsList.Add(Convert
            .ToString(x, 2)
            .PadLeft(8, '0')));

        // TWO LETTERS IN ONE 16BIT BINARY!
        //Console.WriteLine($"binary: [{string.Join(", ", bitsList)}]");

        var sixteenBitsList = new List<string>();

        for (int i = 0; i < bitsList.Count; i += 2)
        {
            if (i < bitsList.Count - 1) sixteenBitsList.Add(bitsList[i] + bitsList[i + 1]);
            else if (i == bitsList.Count - 1) sixteenBitsList.Add("00000000" + bitsList[i]);
        }

        //Console.WriteLine($"16bit: [{string.Join(", ", sixteenBitsList)}]");


        var Et = new List<ulong>();
        var counting = new List<ulong>();
        var aliceList = new List<ulong>();

        foreach (var s in AlicePublic.Split(","))
        {
            aliceList.Add(ulong.Parse(s));
        }

        foreach (var bits in sixteenBitsList)
        {
            for (int i = 0; i < bits.Length; i++)
            {
                counting.Add(aliceList[i] * ulong.Parse(bits[i].ToString()));
            }

            Et.Add(counting.Aggregate((a, c) => a + c));
            counting = new List<ulong>();
        }

        Console.WriteLine($"Encrypted message:\n[{string.Join(", ", Et)}]");
    }
}