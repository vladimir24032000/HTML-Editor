using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HtmlAgilityPack;

namespace HTML_EDITOR
{
    class HTMLEditorClass
    {
        MainWindow mainWindow;
        Label statusBar;
        string htmlPath;
        HtmlDocument index;

       public HTMLEditorClass(Label statusBar, MainWindow mainWindow)
        {
            this.statusBar = statusBar;
            this.mainWindow = mainWindow;
        }
        public void downlodFile(string url)
        {
            WebClient myWebClient = new WebClient();
            statusBar.Content = "Загрузка..";
            // Concatenate the domain with the Web resource filename.
           
            // Download the Web resource and save it into the current filesystem folder.
            myWebClient.DownloadFile(url, "index.html");
            



        }
        public void setPath(string htmlPath)
        {
            this.htmlPath = htmlPath;
        }
        public void openHTML()
        {
           
            index = new HtmlDocument();
            index.Load(htmlPath);
            parsing();
        }
        public void parsing()
        {
            var imgs = index.DocumentNode.SelectNodes("//img");
            for(int i = 0; i< imgs.Count(); i++)
            {
                
                mainWindow.addNewEditForm(i + 1, 1, imgs[i].GetAttributeValue("src", "NULL"));

            }
            var links = index.DocumentNode.SelectNodes("//a");
            for (int i = 0; i < links.Count(); i++)
            {
                mainWindow.addNewEditForm(i + 1, 2, links[i].GetAttributeValue("href", "NULL"));
            }
            var texts = index.DocumentNode.SelectNodes("//p");
            for (int i = 0; i < texts.Count(); i++)
            {
                mainWindow.addNewEditForm(i + 1, 0, texts[i].InnerText);
            }
        }
        public void changeFile(int tagNumber, int number, string text)
        {
            var imgs = index.DocumentNode.SelectNodes("//img");
            var links = index.DocumentNode.SelectNodes("//a");
            var texts = index.DocumentNode.SelectNodes("//p");
            switch (tagNumber)
            {
                case 0:
                    {
                        

                        texts[number].ParentNode.ReplaceChild(HtmlTextNode.CreateNode("<p>"+ text + "</p>"), texts[number]);


                        break;
                    }
                case 1:
                    {
                        imgs[number].SetAttributeValue("src", text);
                        break;
                    }
                case 2:
                    {
                        links[number].SetAttributeValue("href", text);
                        break;
                    }

            }
        }
        public void saveFile()
        {
            index.Save("index.html");

        }

    }
}
