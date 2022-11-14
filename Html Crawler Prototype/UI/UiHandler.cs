namespace HTML_Crawler_Prototype;

public class UiHandler
{
    //HARDCODED REMOVE LATER
    private static string _Path = "/home/spasmilenkov/Documents/SAA-uni/medium-test.txt";

    //HTML Parsing instance
    private static Parser _parser = new Parser();

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
                    PrintNode();
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
            using (StreamReader sr = new StreamReader(_Path))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    _parser.Html = _parser.Html += line;
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
        _parser.ParseHtml();
    }

    public void PrintNode()
    {
    }

    public void SaveDocument()
    {
    }
}