using System;
using System.Data.SQLite;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;
namespace BIVT_21_15_Liamin_Borodin.D.A_LV_7_8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            string strokaNews = "https://news.yandex.ru/health.rss";
            HttpWebRequest request_resusrs = (HttpWebRequest)WebRequest.Create(strokaNews);
            using (HttpWebResponse response_resurs = (HttpWebResponse)request_resusrs.GetResponse())
            {
                using (Stream stream_resurs = response_resurs.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(stream_resurs))
                    {
                        using StreamReader reader_resurs = streamReader;
                        strokaNews = reader_resurs.ReadToEnd();
                    }
                    richTextBox1.Text = strokaNews;
                }
                response_resurs.Close();
            }
            button1.Text = "Готово";
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection BD = new SQLiteConnection("Data Source = TestMyBD.db;"))
            {
                BD.Open();
                SQLiteCommand command = new SQLiteCommand("PRAGMA synchronous = 1; CREATE TABLE IF NOT EXISTS NEWS(Id INTEGER PRIMARY KEY AUTOINCREMENT, PubDate, Title, Description, Link);", BD);
                command.ExecuteNonQuery();
                XmlDocument News = new XmlDocument();
                News.Load("https://news.yandex.ru/health.rss");
                textBox1.Text = "https://news.yandex.ru/health.rss";
                XmlNodeList nodeList = News.DocumentElement.SelectSingleNode("channel").SelectNodes("item");
                string titlle, link, decription, PublicDate;
                for (int i = 0; i < nodeList.Count; i++)
                {
                    XmlNode node = nodeList[i];
                    titlle = node.SelectSingleNode("title").InnerText;
                    link = node.SelectSingleNode("link").InnerText;
                    decription = node.SelectSingleNode("description").InnerText;
                    PublicDate = node.SelectSingleNode("pubDate").InnerText;
                    richTextBox2.Text += PublicDate + "    " + '\n' + '\n' + titlle + "     " + '\n' + '\n' + decription + "     " + '\n' + '\n' + link + '\n' + '\n';
                    command = new SQLiteCommand("INSERT INTO NEWS(PubDate, Title, Description, Link) VALUES(@pubDate, @title, @description, @link)", BD);
                    command.Parameters.AddWithValue("@pubDate", PublicDate);
                    command.Parameters.AddWithValue("@title", titlle);
                    command.Parameters.AddWithValue("@description", decription);
                    command.Parameters.AddWithValue("@link", link);
                    command.ExecuteNonQuery();
                }
            }
            button2.Text = "Готово";
        }
        public void DeleteInfo()
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            button1.Text = "Показать RSS";
            button2.Text = "Показать новости";
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            DeleteInfo();
            MessageBox.Show("Информация была удалена");
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection BD = new SQLiteConnection("Data Source = TestMyBD.db;"))
            {
                BD.Open();
                using SQLiteCommand command = new SQLiteCommand("DELETE FROM NEWS", BD);
                command.ExecuteNonQuery();
            }
            MessageBox.Show("Инормация из БД была удалена");
        }
        private void Label1_Click(object sender, EventArgs e)
        {
        }
        private void Label2_Click(object sender, EventArgs e)
        {
        }
        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void RichTextBox2_TextChanged(object sender, EventArgs e)
        {
        }
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}