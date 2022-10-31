using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;




namespace MemoList
{//Global Var//
    public static class Global
    {
        public static int p;

    }
    //----------------
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BottoneAggiungiElementiToDo_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtElementi_ToDo.Text) && !Elementi_ToDo.Items.Contains(txtElementi_ToDo.Text))
            {
                Global.p += 1;
                Trace.Write(Global.p);
                Elementi_ToDo.Items.Add(Global.p + " ->" + txtElementi_ToDo.Text + "   DataIns:  " + DateAndTime.Now.ToShortTimeString());
                txtElementi_ToDo.Clear();
                TxtPath.Clear();
                string s;
                int num = Global.p;
                s = num.ToString();
                MessageBox.Show("Elemento Aggiunto OK!:" + s, " CONFERMA OPERAZIONE ");
                Trace.Write(Global.p);
                Trace.WriteLine("file aggiunti ok");//visualizzato sull'output ctrl+alt+o
                //System.Diagnostics.Trace.WriteLine("file aggiunti ok");//visualizzato sull'output ctrl+alt+o
            }
        }
        private void BottoneCancellaElementiToDo_Click(object sender, RoutedEventArgs e)
        {
            Elementi_ToDo.Items.Clear();
            txtElementi_ToDo.Clear();
            TxtPath.Clear();

            Trace.WriteLine("file cancellati ok");
            MessageBox.Show("file Cancellati ok");
        }
        private void BottoneEsportaElementiToDo_Click(object sender, RoutedEventArgs e)
        {
            string folderName = @"C:\Users\aless\Desktop\" + TxtPath.Text;
            string pathString = System.IO.Path.Combine(folderName, DateAndTime.DateString);

            System.IO.Directory.CreateDirectory(pathString);
            Trace.WriteLine("cartella creata ok");
            // Create a file name for the file you want to create.
            //string fileName = System.IO.Path.GetRandomFileName();
            string fileName = "MyToDoList_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + ".txt";

            System.IO.File.Create(@fileName);
            Trace.WriteLine("file creato ok");

            Trace.WriteLine("Path to my file: {0}\n", fileName);

            //---------------------------------
            try
            {
                StreamWriter sw = new(pathString + "\\" + fileName);
                string percorso = pathString + "\\" + fileName;
                Trace.WriteLine(percorso);
                //Write a second line of text
                sw.WriteLine("From the StreamWriter class");
                sw.Close();
                System.IO.File.WriteAllLines(@percorso, Elementi_ToDo.Items.Cast<string>().ToArray());
                MessageBox.Show("Programs saved!");
                Trace.WriteLine("Scrittura di file eseguita ok");
            }

            catch (Exception x)
            {
                Trace.WriteLine(x);
            }
            finally
            {
                Trace.WriteLine("Executing finally block.");
            }
        }

        private void TxtElementi_ToDo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void TxtPath_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Elementi_ToDo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            email_send();
        }


        public void email_send()
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("alessandrocilli@gmail.com");
            mail.To.Add("alessandrocilli@gmail.com");
            mail.Subject = "Test Mail - 1";
            mail.Body = "mail with attachment";

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment("c:/ok.txt");
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 465;
            SmtpServer.Credentials = new System.Net.NetworkCredential("alessandrocilli@gmail.com", "0311Rajin");
            SmtpServer.EnableSsl = true;
            Trace.Write("Invio mail in corso");

            SmtpServer.Send(mail);

            Trace.Write("Mail Inviata!");
        }
    }
}
