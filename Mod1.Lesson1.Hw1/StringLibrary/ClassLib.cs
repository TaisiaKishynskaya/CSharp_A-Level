namespace UtilityLibraries;

public static class CalculatorLib
{
    public static int GetSum(int firstValue, int secondValue) 
    {
        return firstValue + secondValue;
    }

    public static int GetDifference(int firstValue, int secondValue) 
    {
        return firstValue - secondValue;
    }

    public static int GetProduct(int firstValue, int secondValue) 
    { 
        return firstValue * secondValue;
    }

    public static float GetQuotient(float firstValue, float secondValue) 
    {
        return firstValue / secondValue;
    }
}