using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HTML_EDITOR
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HTMLEditorClass hTMLEditorClass;
        List<TextBox> textsBoxes = new List<TextBox>();
        List<TextBox> imgsBoxes = new List<TextBox>();
        List<TextBox> linksBoxes = new List<TextBox>();
        public MainWindow()
        {
            InitializeComponent();
            hTMLEditorClass = new HTMLEditorClass(statusBar, this);
        }

        private void downlodButton_Click(object sender, RoutedEventArgs e)
        {
            downlodButton.IsEnabled = false;
            try
            {
                hTMLEditorClass.downlodFile(urlTextBox.Text);
                statusBar.Content = "Файл загружен в " + System.AppDomain.CurrentDomain.BaseDirectory;
                hTMLEditorClass.setPath(System.AppDomain.CurrentDomain.BaseDirectory + "index.html");
               if( MessageBox.Show("Открыть файл?", "HTML Editor", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        textsEditStack.Children.Clear();
                        imgEditStack.Children.Clear();
                        linksEditStack.Children.Clear();
                        linksBoxes.Clear();
                        imgsBoxes.Clear();
                        textsBoxes.Clear();
                        hTMLEditorClass.openHTML();
                    }
                    catch
                    {
                        statusBar.Content = "Ошибка открытия";

                    }
                }
                else
                {
                    
                }
            }
            catch
            {
                statusBar.Content = "Файл не загружен. Попробуйте снова";
            }
            downlodButton.IsEnabled = true;

        }
        public void addNewEditForm(int number, int tagNumber, string curText)
        {
            DockPanel dockPanel = new DockPanel();
            TextBlock numberTextBlock = new TextBlock();
            numberTextBlock.Text = number.ToString();
            TextBox textBox = new TextBox();
            textBox.HorizontalAlignment = HorizontalAlignment.Left;
            textBox.Width = 300;
            textBox.Margin = new Thickness(10, 10, 10, 10);
            textBox.TextChanged += editTextBox_TextChanged;
            TextBlock textBlock = new TextBlock();
            textBlock.Text = curText;
            textBlock.TextWrapping = TextWrapping.Wrap;
            dockPanel.Children.Add(numberTextBlock);
            dockPanel.Children.Add(textBox);
            dockPanel.Children.Add(textBlock);
            switch (tagNumber)
            {
                case 0:
                    {
                        textsEditStack.Children.Add(dockPanel);
                        textsBoxes.Add(textBox);
                        break;
                    }
                case 1:
                    {
                        imgEditStack.Children.Add(dockPanel);
                        imgsBoxes.Add(textBox);
                        break;
                    }
                case 2:
                    {
                        linksEditStack.Children.Add(dockPanel);
                        linksBoxes.Add(textBox);
                        break;
                    }
            }

            //< Label Content = "Изменить текст" ></ Label >
 
            //         < DockPanel Margin = "10" >
  
            //              < TextBlock Text = "1." ></ TextBlock >
   
            //               < TextBox HorizontalAlignment = "Left" Width = "300" Margin = "10,0,10,0" ></ TextBox >
        
            //                    < TextBlock Text = "Текущий текст" >
         
            //                     </ TextBlock >
         
            //                 </ DockPanel >
        }

       
        private void editTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            saveButton.IsEnabled = true;
            TextBox changedBox = (TextBox)sender;
            for(int i = 0; i < textsBoxes.Count(); i++)
            {
                if(changedBox == textsBoxes[i])
                {
                    hTMLEditorClass.changeFile(0, i, changedBox.Text);
                    return;
                }
            }
            for (int i = 0; i < imgsBoxes.Count(); i++)
            {
                if (changedBox == imgsBoxes[i])
                {
                    hTMLEditorClass.changeFile(1, i, changedBox.Text);
                    return;
                }
            }
            for (int i = 0; i < linksBoxes.Count(); i++)
            {
                if (changedBox == linksBoxes[i])
                {
                    hTMLEditorClass.changeFile(2, i, changedBox.Text);
                    return;
                }
            }

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            saveButton.IsEnabled = false;
            hTMLEditorClass.saveFile();
            statusBar.Content = "Сохранено!";
        }
    }
}
