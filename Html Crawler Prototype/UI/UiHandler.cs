using Html_Crawler_Prototype.Utilities;
namespace HTML_Crawler_Prototype;

public class UiHandler
{
    //HARDCODED REMOVE LATER
    private static string _Path = "/home/spasmilenkov/Documents/SAA-uni/html-test.txt";
    private const string _winPath = @"C:\Users\Spas Milenkov\Downloads\html-test.txt";

    //HTML Parsing instance
    private static HtmlParser _parser = new HtmlParser();
    public void LoadUi()
    {
        char command = ' ';

        do
        {
            Console.WriteLine(
                "---------------------------------------------------------------------------------------" +
                "\nWelcome to the HTML Crawler. Here are the commands:\n" +
                "0: Exit the program\n" +
                "1: Load a file from the file system\n" +
                "2: Print node\n" +
                "3: Save html file\n" +
                "Enter command");
            command = Console.ReadKey().KeyChar;
            Console.WriteLine(
                "\n---------------------------------------------------------------------------------------");
            switch (command)
            {
                case '1':
                    LoadFile();
                    break;
                case '2':
                    Console.WriteLine("Enter Xpath query:");
                    string input = Console.ReadLine();
                    _parser.ParseInput(input);
                    break;
                case '3':
                    SaveDocument();
                    break;
                case '0':
                    break;
                // case '1':
                //     break;
                default:
                    Console.WriteLine("Invalid Input!");
                    break;
            }
        } while (command != '0');
    }

    public void LoadFile()
    {
        try
        {
            using (StreamReader sr = new StreamReader(_winPath))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    // _parser.Html = _parser.Html += line;
                    Console.WriteLine(line);
                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine(_Path);
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        _parser.Html = File.ReadAllText(_winPath);
        _parser.ParseHtml();
        Console.WriteLine("Done");
    }

    public void PrintNode()
    {
    }

    public void SaveDocument()
    {
    }
}