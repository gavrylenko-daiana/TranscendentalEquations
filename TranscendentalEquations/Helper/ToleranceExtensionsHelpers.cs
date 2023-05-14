namespace TranscendentalEquations.Helper;

internal static class ToleranceExtensionsHelpers
{
    public static double GetTolerance(this double tolerance, double fx)
    {
        string fxStr = fx.ToString();
        int decimalIndex = fxStr.IndexOf(".");

        if (decimalIndex != -1 && fxStr[decimalIndex + 1] == '0')
        {
            fxStr = fxStr.Substring(0, decimalIndex + 1) + '1' + fxStr.Substring(decimalIndex + 2);
            tolerance = Convert.ToDouble(fxStr);
            return tolerance;
        }
        else
        {
            tolerance = Math.Ceiling(fx * 10) / 10;
            return tolerance;
        }
    }
}
