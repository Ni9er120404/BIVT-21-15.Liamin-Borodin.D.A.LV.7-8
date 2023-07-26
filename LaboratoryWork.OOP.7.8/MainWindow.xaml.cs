using Microsoft.Data.Sqlite;
using System.IO;
using System.Net;
using System.Windows;

namespace LaboratoryWork.OOP._7._8
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
		}

		private void Button1_Click(object sender, RoutedEventArgs e)
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
			button1.Content = "Готово";
		}

		private void Button2_Click(object sender, RoutedEventArgs e)
		{
			// Code to fetch news data and save it to the database using Entity Framework Core
		}

		private void Button3_Click(object sender, RoutedEventArgs e)
		{
			DeleteInfo();
			_ = MessageBox.Show("Информация была удалена");
		}

		private void Button4_Click(object sender, RoutedEventArgs e)
		{
			using (SqliteConnection BD = new SqliteConnection("Data Source = TestMyBD.db;"))
			{
				BD.Open();
				using SqliteCommand command = new SqliteCommand("DELETE FROM NEWS", BD);
				_ = command.ExecuteNonQuery();
			}
			_ = MessageBox.Show("Инормация из БД была удалена");
		}

		public void DeleteInfo()
		{
			richTextBox1.Text = "";
			richTextBox2.Text = "";
			button1.Content = "Показать RSS";
			button2.Content = "Показать новости";
		}
	}
}
