using System;
namespace HTML_Crawler_Prototype;

public static class Helper
{
    public static string[] Split(string text, char separator)
    {
        if(text.Length == 1)
        {
            string[] split = new string[] {text};

            return split;
        }
        //hold the number of times the separator is met in the text
        int sepCount = 0;

        //use linear search (there is probably a better way for this) to get the number of the sep count
        foreach (char c in text)
            if (c == separator)
                sepCount++;
        
        //initialize the array with the sep count + 1
        //even if the separator is not present in the string we return the entire string in the array
        //length is 0+1
        string[] splitString = new string[sepCount + 1];
        
        //returns the entire string alone in an array
        //covers the case where the separator is not present
        if (sepCount == 0)
        {
            splitString[0] = text;
            return splitString;
        }

        int i = 0;
        //iterate through the string
        //create an empty string to hold the chars 
        string currentSplit = "";
        for (int j = 0; j < text.Length; j++)
        {
            if (text[j] == separator )
            {
                splitString[i] = currentSplit;
                currentSplit = "";
                i++;
                j++;
            }
            
            if (j == text.Length-1)
            {
                currentSplit += text[j];
                splitString[i] = currentSplit;
                return splitString;
            }

            currentSplit += text[j];
        }
        return splitString;
    }
    public static string Trim(string text)
    {
        return TrimEnd(TrimStart(text));
    }

    public static string TrimStart(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] != ' ' && text[i] != '\t' && text[i] != '\r')
                return Slice(text, i);
        }

        return "";
    }

    public static string TrimEnd(string text)
    {
        for (int i = text.Length - 1; i > 0; i--)
        {
            if (text[i] != ' ' && text[i] != '\t' && text[i] != '\r')
                return Slice(text, 0, i + 1);
        }

        return "";
    }
    public static string Slice(string text, int beginning, int ending)
    {
        string sliced = "";
        for (int i = beginning; i < ending; i++)
        {
            char c = text[i];
            sliced += c;
        }
        return sliced;
    }

    public static string Slice(string text, int beginning)
    {
        string sliced = "";
        for (int i = beginning; i < text.Length; i++)
        {
            char c = text[i];
            sliced += c;
        }

        return sliced;

    }
    public static string[] Slice(string[] arr, int beginning)
    {
        string[] sliced = new string[arr.Length - beginning];
        int k = 0;
        for (int i = beginning; i < arr.Length; i++, k++)
        {
            sliced[k] = arr[i];
        }

        return sliced;

    }
}