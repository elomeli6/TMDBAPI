using System;
using System.Globalization;

public class PresentationFormatter
{
    public string TransformRuntime(int runtimeInMinutes)
    {
      

        int hours = runtimeInMinutes / 60;
        int minutes = runtimeInMinutes % 60;

        if (hours > 0 && minutes > 0)
        {
            return $"{hours} hour{(hours > 1 ? "s" : "")} {minutes} minute{(minutes > 1 ? "s" : "")}";
        }
        else if (hours > 0)
        {
            return $"{hours} hour{(hours > 1 ? "s" : "")}";
        }
        else
        {
            return $"{minutes} minute{(minutes > 1 ? "s" : "")}";
        }
    }

    
}