using System;
using System.Collections.Generic;
using System.Linq;

class ParityAndBCCFunctions
{
    static List<int> SetParity(List<int> bits, string parityType = "Odd")
    {
        int countOnes = bits.Count(bit => bit == 1);
        int parityBit = (parityType == "Odd") ? (countOnes % 2 == 0 ? 1 : 0) : (countOnes % 2 != 0 ? 1 : 0);
        bits.Add(parityBit);
        return bits;
    }

    static bool CheckParity(List<int> bits, string parityType = "Odd")
    {
        int countOnes = bits.Count(bit => bit == 1);
        return (parityType == "Odd") ? (countOnes % 2 == 0) : (countOnes % 2 != 0);
    }

    static List<int> CalculateBCC(List<int> bits)
    {
        int bcc = bits.Aggregate(0, (current, bit) => current ^ bit);
        return new List<int> { bcc };
    }

    static bool CheckBCC(List<int> bits, List<int> bcc)
    {
        List<int> calculatedBcc = CalculateBCC(bits);
        return calculatedBcc.SequenceEqual(bcc);
    }

    static List<int> CalculateBCCWithParity(List<int> bits, string parityType = "Odd")
    {
        List<int> bcc = CalculateBCC(bits);
        return SetParity(bcc, parityType);
    }

    static bool CheckBCCWithParity(List<int> bits, List<int> bcc, string parityType = "even")
    {
        List<int> calculatedBcc = CalculateBCC(bits);
        return CheckParity(calculatedBcc.Concat(new[] { 0 }).ToList(), parityType) == CheckParity(bcc, parityType);
    }

    static void Main()
    {
        Console.WriteLine("Enter a sequence of bits (one line per bit, leave an empty line to end input):");

        List<int> bits = new List<int>();
        string userInput;

        while (true)
        {
            userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
                break;

            // Validate user input
            if (!userInput.All(char.IsDigit) || userInput.Any(c => c != '0' && c != '1'))
            {
                Console.WriteLine("Invalid input. Please enter a valid sequence of bits.");
                return;
            }

            bits.AddRange(userInput.Select(c => int.Parse(c.ToString())));
        }

        string evenParityType = "Odd";
        string oddParityType = "Even";

        // Set even parity
        List<int> bitsWithEvenParity = SetParity(new List<int>(bits), evenParityType);
        Console.WriteLine($"Bits with {evenParityType} Parity: " + string.Join(", ", bitsWithEvenParity));

        // Check even parity
        bool evenParityCheckResult = CheckParity(bitsWithEvenParity, evenParityType);
        Console.WriteLine($"{char.ToUpper(evenParityType[0]) + evenParityType.Substring(1)} Parity Check Result: " + evenParityCheckResult);

        // Calculate and check BCC with even parity
        List<int> bccWithEvenParity = CalculateBCCWithParity(new List<int>(bits), evenParityType);
        Console.WriteLine($"BCC with {evenParityType} Parity: " + string.Join(", ", bccWithEvenParity));
        bool bccWithEvenParityCheckResult = CheckBCCWithParity(bitsWithEvenParity, bccWithEvenParity, evenParityType);
        Console.WriteLine($"BCC with {evenParityType} Parity Check Result: " + bccWithEvenParityCheckResult);

        // Separate line for odd parity
        Console.WriteLine();

        // Set odd parity
        List<int> bitsWithOddParity = SetParity(new List<int>(bits), oddParityType);
        Console.WriteLine($"Bits with {oddParityType} Parity: " + string.Join(", ", bitsWithOddParity));

        // Check odd parity
        bool oddParityCheckResult = CheckParity(bitsWithOddParity, oddParityType);
        Console.WriteLine($"{char.ToUpper(oddParityType[0]) + oddParityType.Substring(1)} Parity Check Result: " + oddParityCheckResult);

        // Calculate and check BCC with odd parity
        List<int> bccWithOddParity = CalculateBCCWithParity(new List<int>(bits), oddParityType);
        Console.WriteLine($"BCC with {oddParityType} Parity: " + string.Join(", ", bccWithOddParity));
        bool bccWithOddParityCheckResult = CheckBCCWithParity(bitsWithOddParity, bccWithOddParity, oddParityType);
        Console.WriteLine($"BCC with {oddParityType} Parity Check Result: " + bccWithOddParityCheckResult);
    }
}
