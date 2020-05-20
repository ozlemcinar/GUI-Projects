using System;

using System.Linq;

using System.Collections.Generic;

using System.IO;

using System.Windows;

using System.Windows.Documents;

using System.Windows.Input;

using System.Windows.Media;

using Microsoft.Win32;

using System.Windows.Controls;

using System.Runtime.InteropServices;

using System.Text.RegularExpressions;

using System.Collections.ObjectModel;
using System.Text;


namespace WpfApplication3
{

    public partial class MainWindow : Window
    {
        String path;

        int numOfLine;

        int currentLine;

        int lineLength;

        string copyyy;
        bool savedornot;

        ObservableCollection<MainWindow> rowss = new ObservableCollection<MainWindow>();

        public MainWindow()
        {
            InitializeComponent();

            savedornot = false;

            numOfLine = 1;

            currentLine = 1;

            copyyy = "\n";

            TextPointer caretPos = rtbEditor.CaretPosition;

            caretPos = caretPos.DocumentStart;

            rtbEditor.CaretPosition = caretPos;

            String ppath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            leftButton.Click += LeftButton_Click;

            leftMenu.Click += LeftButton_Click;

            rightButton.Click += RightButton_Click;

            rightMenu.Click += RightButton_Click;

            upButton.Click += UpButton_Click;

            upMenu.Click += UpButton_Click;

            downButton.Click += DownButton_Click;

            downMenu.Click += DownButton_Click;

            helpMenu.Click += Help_Click;

            rtbEditor.Document.PageWidth = 100000;

            suffix.Document.PageWidth = 300;

            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\untitled.rtb";

            lblFilePath.Text = "  " + path + "  ";

            suffix.AppendText("=====");

            lineLength = Convert.ToInt32(rtbEditor.ViewportHeight) / 26;
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {

            TextPointer tp1 = rtbEditor.Selection.Start.GetLineStartPosition(0);

            TextPointer tp2 = rtbEditor.Selection.Start;

            int column = tp1.GetOffsetToPosition(tp2);
           
            int someBigNumber = int.MaxValue;

            int lineMoved, currentLineNumber;

            rtbEditor.Selection.Start.GetLineStartPosition(-someBigNumber, out lineMoved);

            currentLineNumber = -lineMoved;

            lblPosition.Text = "  Line: " + (currentLineNumber + 1).ToString() + " Column: " + column.ToString() + "  ";

            int num = Lines();

            if (numOfLine != num)
            {
                lineNumChanged(num);

                numOfLine = num;
            }
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "(*.txt)|*.txt|All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);

                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);

                range.Load(fileStream, DataFormats.Rtf);
            }
        }

        private void lineNumChanged(int num)
        {
            suffix.Document.Blocks.Clear();

            suffix.AppendText("=====\n\n");

            for (int i = 1; i < num; i++)
            {
                suffix.AppendText("=====\r\n");
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!savedornot)
            {
             //   StreamWriter file = new StreamWriter(dialo);
             //   for (int i = 0; i < Lines(); i++)
               // {
                 //   File
                //}

                OpenFileDialog dlg = new OpenFileDialog();
                
           
          
                SaveFileDialog dialog = new SaveFileDialog()
                {
                    
                   
                   // Filter = "Text Files(*.txt)|*.txt|All(*.*)|*",

                    FileName = dlg.FileName,
                             //  OpenFileDialog dlg = new OpenFileDialog();

                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                };

                if (dialog.ShowDialog() == true)
                {
                    FileStream fileStream = new FileStream(dialog.FileName, FileMode.Create);

                    TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);

                    range.Save(fileStream, DataFormats.Rtf);
                }

                savedornot = true;

                path = dialog.FileName;

                lblFilePath.Text = path;
            }

            else
            {

                FileStream fileStream = new FileStream(path, FileMode.Open);

                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);

                range.Save(fileStream, DataFormats.Rtf);

                lblFilePath.Text = path;

            }
        }

        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {

                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);

                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);

                range.Save(fileStream, DataFormats.Rtf);

            }
        }

        private void Find_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            rtbEditor.SelectAll();

            TextPointer start = rtbEditor.Document.ContentStart;

            TextPointer end = rtbEditor.Document.ContentEnd;

            TextRange range = new TextRange(start, end);

            Regex reg = new Regex(searchbox.Text, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            while (start != null && start.CompareTo(end) < 0)
            {

                if (start.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {

                    var match = reg.Match(start.GetTextInRun(LogicalDirection.Forward));

                    var textrange = new TextRange(start.GetPositionAtOffset(match.Index, LogicalDirection.Forward), start.GetPositionAtOffset(match.Index + match.Length, LogicalDirection.Backward));

                    textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));

                    start = textrange.End;

                }

                start = start.GetNextContextPosition(LogicalDirection.Forward);

            }
        }

        private static TextPointer GetPoint(TextPointer start, int x)
        {

            var ret = start;

            var i = 0;

            while (i < x && ret != null)
            {

                if (ret.GetPointerContext(LogicalDirection.Backward) == TextPointerContext.Text || ret.GetPointerContext(LogicalDirection.Backward) == TextPointerContext.None)

                    i++;

                if (ret.GetPositionAtOffset(1, LogicalDirection.Forward) == null)

                    return ret;

                ret = ret.GetPositionAtOffset(1, LogicalDirection.Forward);

            }

            return ret;
        }
        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            rtbEditor.LineUp();
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            rtbEditor.LineDown();
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            rtbEditor.LineLeft();
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            rtbEditor.LineRight();
        }
        private void forwardButton_Click(object sender, RoutedEventArgs e)
        {
            rtbEditor.LineDown();

        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            rtbEditor.LineUp();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("-save: Overwrites to the original file. If no file exists, prompt for a path and name.\n"

                                 + "-save as: Saves the file as specified on the cmdline.\n"

                                 + "-open: Opens the specified file reads it into memory, closes it unchanged, but ready for editing.\n"

                                 + "-find: Allows the user to search for a string. See rules below these tables for how a user can specify the search.\n"

                                 + "-#: Skip to line# of the file. Brings line to the top of the view if there are enough lines under it.\n"

                                 + "-up #: Scroll up n lines. If no number is provided, or wrong entery follows up, goes up 1 line.\n"

                                 + "-down #: Scroll downn lines. If no number is provided, or wrong entery follows down, goes down 1 line.\n"

                                 + "-left #: Scrolls left # times (shows more data on the left if any exists) If no # provided, assumes 1.\n"

                                 + "-right #: Scrolls right # times (shows more data on the right if any exists) If no # provided, assumes 1.\n"

                                 + "-forward: Scrolls down all page."
                                 + "backward: Scrolls up all page.\n"

                                 + "-change: Modifies a searched-for string, starting at the current line e.g.; c/abc/ABC/n1 n2 changes abc to ABC on n1 lines    n2 times per line \n"

                                 + "-setcl #: Sets the number of the line on the screen which is to act as the current line until further notice");

            commandLine.Text = "";

        }

        private void commandEnterPressed(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {

                TextRange range1 = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);

                range1.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));

                string key = commandLine.Text;

                string[] k = key.Split(null);

                lblAction.Text = k[0];

                if (k[0].ToLower() == "save")
                {

                    if (k[1] != null)
                    {

                        if (k[1].ToLower() == "as")
                        {

                            FileStream fileStream = new FileStream(k[2], FileMode.Create);

                            TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);

                            range.Save(fileStream, DataFormats.Rtf);

                        }

                    }

                    else

                        Save_Executed(sender, null);

                    commandLine.Text = "";

                }

                else if (k[0].ToLower() == "open")
                {

                    if (k[1] != null)
                    {

                        FileStream fileStream = new FileStream(k[1], FileMode.Open);

                        TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);

                        range.Load(fileStream, DataFormats.Rtf);

                    }

                    commandLine.Text = "";

                }

                else if (k[0].ToLower() == "find")
                {

                    int n = 0;

                    try
                    {

                        int h = Int32.Parse(k[2]);

                        if (h > 0) { n = h; }

                    }

                    catch (Exception a) { }

                    var str = k[1].Split('/');

                    int num = 0;

                    rtbEditor.SelectAll();

                    TextPointer start = rtbEditor.Selection.Start.GetLineStartPosition(currentLine - 1 + n);

                    TextPointer end = rtbEditor.Selection.Start.GetLineStartPosition(currentLine + num - 1);

                    TextRange range = new TextRange(start, end);

                    Regex reg = new Regex(str[1], RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    while (start != null && start.CompareTo(end) < 0)
                    {

                        if (start.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                        {

                            var match = reg.Match(start.GetTextInRun(LogicalDirection.Forward));

                            var textrange = new TextRange(start.GetPositionAtOffset(match.Index, LogicalDirection.Forward), start.GetPositionAtOffset(match.Index + match.Length, LogicalDirection.Backward));

                            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));

                            start = textrange.End;

                        }

                        start = start.GetNextContextPosition(LogicalDirection.Forward);

                    }

                    commandLine.Text = "Press enter to turn text back to black";

                }

                else if (k[0].ToLower() == "up")
                {

                    int num = 1;

                    if (k[1] != null)
                    {

                        bool isNumeric = int.TryParse(k[1], out num);

                        if (!isNumeric)

                            num = 1;

                    }

                    for (int i = 0; i < num; i++)
                    {

                        UpButton_Click(sender, null);

                    }

                    commandLine.Text = "";

                }

                else if (k[0].ToLower() == "down")
                {

                    int num = 1;

                    if (k[1] != null)
                    {

                        bool isNumeric = int.TryParse(k[1], out num);

                        if (!isNumeric)

                            num = 1;

                    }

                    for (int i = 0; i < num; i++)
                    {

                        DownButton_Click(sender, null);

                    }

                    commandLine.Text = "";

                }
                else if (k[0].ToLower() == "i")
                {
                    int num = 1;
                    if (k[1] != null){
                        bool isNumeric = int.TryParse(k[1], out num);
                        if (!isNumeric)
                            num = 1;
                    }
                    for (int i = 0; i < num; i++)
                    {
                        
                       // File.AppendText("\n");
                        //Lines();
                      

                    }

                    commandLine.Text = "";

                }
                else if (k[0].ToLower() == "forward")
                {

                    for (int i = 0; i < Lines()  ; i++)
                    {
                        forwardButton_Click(sender, null);
                    }
                    commandLine.Text = "";
                }

                else if (k[0].ToLower() == "backward")
                {

                    for (int i = 0; i < Lines(); i++)
                    {
                        backButton_Click(sender, null);
                    }
                    commandLine.Text = "";
                }

                else if (k[0].ToLower() == "left")
                {

                    int num = 1;

                    if (k[1] != null)
                    {

                        bool isNumeric = int.TryParse(k[1], out num);

                        if (!isNumeric)

                            num = 1;

                    }

                    for (int i = 0; i < num; i++)
                    {

                        LeftButton_Click(sender, null);

                    }

                    commandLine.Text = "";

                }

                else if (k[0].ToLower() == "right")
                {

                    int num = 1;

                    if (k[1] != null)
                    {

                        bool isNumeric = int.TryParse(k[1], out num);

                        if (!isNumeric)

                            num = 1;

                    }

                    for (int i = 0; i < num; i++)
                    {

                        RightButton_Click(sender, null);

                    }

                    commandLine.Text = "";

                }

                else if (k[0].ToLower() == "change")
                {

                    var str = k[1].Split('/');

                    int num = 0;

                    bool isNumeric = int.TryParse(str[3], out num);

                    int n = 0;

                    int h = Int32.Parse(str[3]);

                    if (h > 0) { n = h; }

                    rtbEditor.SelectAll();

                    TextPointer start = rtbEditor.Selection.Start.GetLineStartPosition(currentLine - 1 + n);

                    TextPointer end = rtbEditor.Selection.Start.GetLineStartPosition(currentLine + num - 1 + n);

                    TextRange range = new TextRange(start, end);

                    String txt = new TextRange(start, end).Text;

                    txt = txt.Replace(str[1], str[2]);

                    if (txt != new TextRange(start, end).Text)
                    {

                        new TextRange(start, end).Text = txt; // Change the current text to _Text

                    }

                }

                else if (k[0].ToLower() == "setcl")
                {
                    int num=1;
                    if (k[1] != null) { 
                        bool isNumeric = int.TryParse(k[1], out num);
                      //  int deneme = Convert.ToInt16(k[1]);//str[0]; 
                    
                      //  num = int.Parse(k[1]);
                        if (!isNumeric){
                            num=1;
                        }
                      //  currentLine = num;

                    }
                  //  Console.WriteLine(deneme);

                    /*
                     * 
                     * TextPointer start = rtbEditor.Selection.Start.GetLineStartPosition(currentLine - 1);

                        TextPointer end = rtbEditor.Selection.Start.GetLineStartPosition(currentLine + num - 1);
                     int someBigNumber = int.MaxValue;

                 int lineMoved, currentLineNumber;

                 suffix.Selection.Start.GetLineStartPosition(-someBigNumber, out lineMoved);

                 currentLineNumber = -lineMoved;
                    
                    
                     */
                    var textToSync = (sender == rtbEditor) ? suffix : rtbEditor;

                    textToSync.ScrollToVerticalOffset(num);

                    textToSync.ScrollToHorizontalOffset(num);

                 //   TextPointer start = rtbEditor.Selection.Start.GetLineStartPosition(num);

                   // TextPointer end = rtbEditor.Selection.Start.GetLineStartPosition(num);
                    //currentLine = num;
                    commandLine.Text = "";


                }

                else if (k[0].ToLower() == "help")
                {

                    Help_Click(e, null);

                }

                else
                {

                    int num;

                    bool isNumeric = int.TryParse(k[0], out num);

                    if (isNumeric)
                    {

                        rtbEditor.ScrollToHome();

                        for (int i = 0; i < num - 1; i++)

                            DownButton_Click(sender, null);

                        rtbEditor.SelectAll();

                        TextPointer start = rtbEditor.Selection.Start.GetLineStartPosition(num - 1);

                        TextPointer end = rtbEditor.Selection.Start.GetLineStartPosition(num);

                        var textrange = new TextRange(start, end);

                        textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));

                    }

                    else
                    {

                        commandLine.Text = "Type help for options";

                    }
                }
            }
        }

        public int Lines()
        {

            TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);

            String s = range.Text;

            int count = 0;

            int position = 0;

            while ((position = s.IndexOf('\n', position)) != -1)
            {

                count++;

                position++;       

            }

            return count;

        }

        private void mouse_clicked(object sender, MouseButtonEventArgs e)
        {
            commandLine.Text = "";
        }

        private void RichTextBox_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

            var textToSync = (sender == rtbEditor) ? suffix : rtbEditor;

            textToSync.ScrollToVerticalOffset(e.VerticalOffset);

            textToSync.ScrollToHorizontalOffset(e.HorizontalOffset);

        }

        private void suffixEnterPressed(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {

                TextPointer tp1 = suffix.Selection.Start.GetLineStartPosition(0);

                TextPointer tp2 = suffix.Selection.Start;

                int column = tp1.GetOffsetToPosition(tp2);

                int someBigNumber = int.MaxValue;

                int lineMoved, currentLineNumber;

                suffix.Selection.Start.GetLineStartPosition(-someBigNumber, out lineMoved);

                currentLineNumber = -lineMoved;

                suffix.SelectAll();

                TextPointer start = suffix.Selection.Start.GetLineStartPosition(currentLineNumber);

                TextPointer end = suffix.Selection.Start.GetLineStartPosition(currentLineNumber + 1);

                var textrange = new TextRange(start, end);

                lblAction.Text = currentLineNumber.ToString();

                string key = textrange.Text;

                string str = Regex.Replace(key, "=", "");

                str = str.ToLower();

                var s = str.ToCharArray();

                if (str[0] == 'm')
                {

                    String lineText;

                    rtbEditor.SelectAll();

                    TextPointer start1 = rtbEditor.Selection.Start.GetLineStartPosition(currentLineNumber + 1);

                    TextPointer end1 = rtbEditor.Selection.Start.GetLineStartPosition(currentLineNumber + 2);

                    String txt = new TextRange(start1, end1).Text;

                    lineText = txt;

                    txt = "";

                    if (txt != new TextRange(start1, end1).Text)
                    {

                        new TextRange(start1, end1).Text = txt; // Change the current text to _Text

                    }

                    rtbEditor.SelectAll();

                    start1 = rtbEditor.Selection.Start.GetLineStartPosition(currentLineNumber);

                    end1 = rtbEditor.Selection.Start.GetLineStartPosition(currentLineNumber + 1);

                    txt = new TextRange(start1, end1).Text;

                    txt = lineText + txt;

                    if (txt != new TextRange(start1, end1).Text)
                    {

                        new TextRange(start1, end1).Text = txt; // Change the current text to _Text

                    }

                }

                if (str[0] == 'c')
                {

                    rtbEditor.SelectAll();

                    TextPointer start1 = rtbEditor.Selection.Start.GetLineStartPosition(currentLineNumber);

                    TextPointer end1 = rtbEditor.Selection.Start.GetLineStartPosition(currentLineNumber + 1);

                    copyyy = new TextRange(start1, end1).Text;

                }

                if (str[0] == 'a')
                {

                    rtbEditor.SelectAll();

                    TextPointer start1 = rtbEditor.Selection.Start.GetLineStartPosition(currentLineNumber);

                    TextPointer end1 = rtbEditor.Selection.Start.GetLineStartPosition(currentLineNumber);

                    String txt = new TextRange(start1, end1).Text;
                    string a = txt;
                    string xx = "";

                    // string a = txt;
                    //   int deneme = Convert.ToInt16(str[1])-48;//str[0]; 
                    // Console.WriteLine(deneme);
                    //  for (int i = 0; i < deneme; i++) {                       
                    for (int i = 1; i < str.Length; i++)
                    {
                        xx = xx + str[i];
                    }
                    //    Console.WriteLine(xx);
                    string replacement = Regex.Replace(txt,@"\t\n\r", "");
                    replacement =  replacement + xx;

                  //  if (txt != new TextRange(start1, end1).Text)
                    //{

                        new TextRange(start1, end1).Text = replacement; 

                    //}

                }

                if (str[0] == 'b')
                {

                    rtbEditor.SelectAll();

                    TextPointer start1 = rtbEditor.Selection.Start.GetLineStartPosition(currentLineNumber );

                    TextPointer end1 = rtbEditor.Selection.Start.GetLineStartPosition(currentLineNumber);

                    String txt = new TextRange(start1, end1).Text;
                    string xx = "";
                    
                   // string a = txt;
                 //   int deneme = Convert.ToInt16(str[1])-48;//str[0]; 
                   // Console.WriteLine(deneme);
                  //  for (int i = 0; i < deneme; i++) {                       
                    for (int i = 1; i < str.Length; i++)
                    {
                        xx = str[i] + xx;
                    }
                //    Console.WriteLine(xx);
                    //txt = xx + txt ;
                    string replacement = Regex.Replace(txt, @"\t\n\r", "");
                    replacement = xx + replacement ;
                    //}
                     

                  //  txt = txt + copyyy + Environment.NewLine;

                   // if (txt != new TextRange(start1, end1).Text)
                    //{

                        new TextRange(start1, end1).Text = replacement; // Change the current text to _Text

                   // }

                }

                if (str[0] == 'i'){
                    rtbEditor.SelectAll();
                    TextPointer start1 = rtbEditor.Selection.Start.GetLineStartPosition(currentLineNumber - 1);
                    TextPointer end1 = rtbEditor.Selection.Start.GetLineStartPosition(currentLineNumber);
                    String txt = new TextRange(start1, end1).Text;
                    string a = txt;
                    int deneme = Convert.ToInt16(str[1])-48;//str[0]; 
                   // Console.WriteLine(deneme);
                    for (int i = 0; i < deneme; i++) {                       
                        txt =  txt+ copyyy;
                    }
                    if (txt != new TextRange(start1, end1).Text){
                         new TextRange(start1, end1).Text = txt; // Change the current text to _Text
                    }
                }

                if (str[0] == '"')
                {

                    rtbEditor.SelectAll();

                    TextPointer start1 = rtbEditor.Selection.Start.GetLineStartPosition(currentLineNumber);

                    TextPointer end1 = rtbEditor.Selection.Start.GetLineStartPosition(currentLineNumber + 1);

                    String txt = new TextRange(start1, end1).Text;

                    string a = txt;
                    int deneme = Convert.ToInt16(str[1])-48;//str[0]; 
                   //Console.WriteLine(deneme);
                    for (int i = 0; i < deneme; i++) {                       
                        txt =  txt + a ;
                       // txt = a ;
                    }
                  //  txt = txt + txt;

                    if (txt != new TextRange(start1, end1).Text)
                    {

                        new TextRange(start1, end1).Text = txt; // Change the current text to _Text

                    }
                }
            }
        }
    }
}