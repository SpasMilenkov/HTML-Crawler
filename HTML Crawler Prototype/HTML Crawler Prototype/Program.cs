
//dear programmer as of writing this program only i and God knew how the thing worked
//with the passing of time however now only God knows
//after you are done bumping your head in the wall trying to fix this 
//increment the counter below so the next dreaded individual knows what awaits him 
//hours wasted on this: 19

using System.Diagnostics;

namespace HTML_Crawler_Prototype
{
    public class Program 
    {
        //the simple command line interface rendering
        private static UiHandler _menu = new UiHandler();
        public static void Main(string[] args)
        {
            _menu.LoadUi();
        }
    }
}