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
        private StreamWriter passFile;

        public Form1()
        {
            this.FormClosing += Form1_FormClosing;
         
            passFile = new StreamWriter("HashLozinke.txt");
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            passFile.Close();
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
            passFile.WriteLine(Encrypt(textBox1.Text));
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SavePass();
            textBox1.Clear();
        }
        
    }
}
