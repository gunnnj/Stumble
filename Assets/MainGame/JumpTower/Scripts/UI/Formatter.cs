using UnityEngine;

public class Formatter
{
    public static string FormatMoney(float amount)
    {
        if (amount < 1000)
            return amount.ToString("N0");
        else if (amount < 1_000_000)
            return (amount / 1000).ToString("N2") + "K";
        else if (amount < 1_000_000_000)
            return (amount / 1_000_000).ToString("N2") + "M";
        else
            return (amount / 1_000_000_000).ToString("N2") + "B";
    }
}
