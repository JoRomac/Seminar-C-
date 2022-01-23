using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;


namespace PasswordHash
{
    public partial class Form1 : Form
    {
        private StreamWriter myFile;

        public Form1()
        {
            this.FormClosing += Form1_FormClosing;
            
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private string Encrypt(string value)
        {
            using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }
        private void SavePass()
        {
            myFile.WriteLine(Encrypt(textBox1.Text));
            myFile.Close();
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //if (!File.Exists("HashLozinke.txt"))
               // passFile = new StreamWriter("HashLozinke.txt");
           // else
            myFile = new("HashLozinke.txt", append:true);
            SavePass();
            textBox1.Clear();
        }
            

        private void button2_Click(object sender, EventArgs e)
        {
  
            if (!File.Exists("HashLozinke.txt"))
            {
                MessageBox.Show("Nepostoji datoteka lozinki");
            }
            else {
                bool flag = false;
                StreamReader sr = new StreamReader("HashLozinke.txt");
                string line = sr.ReadLine();
                while (line != null)
                {
                    if (line.Equals(Encrypt(textBox1.Text)))
                    {
                        MessageBox.Show("Lozinka je ispravna");
                        flag = true;
                    }
                    line = sr.ReadLine();
                }
                if (!flag)
                    MessageBox.Show("Lozinka ne postoji");
                sr.Close();
            }    
            textBox1.Clear();

        }
    }
}
