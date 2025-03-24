using UnityEngine;

public enum DiceType
{
    D4,
    D6,
    D8,
    D10,
    D12,
    D20
}
public static class DiceController
{
    public static int RollDice(DiceType dice, int bonus = 0)
    {
        switch (dice)
        {
            case DiceType.D4:
                return GenerateRandomNumber(4);
            case DiceType.D6:
                return GenerateRandomNumber(6);
            case DiceType.D8:
                return GenerateRandomNumber(8);
            case DiceType.D10:
                return GenerateRandomNumber(10);
            case DiceType.D12:
                return GenerateRandomNumber(12);
            default:
                return GenerateRandomNumber(20);
        }
    }

    private static int GenerateRandomNumber(int inclusiveMaxValue)
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(1, inclusiveMaxValue + 1);
        return randomNumber;
    }
}
