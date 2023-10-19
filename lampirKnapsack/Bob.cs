namespace lampirKnapsack;

public class Bob
{
    public static void Logic()
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