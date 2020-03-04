using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Muhtarlık
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }
 

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public bool GirisYap()
        {
            try
            {
                string kullaniciadi = textBox1.Text;
                string sifre = textBox2.Text;

                if (kullaniciadi == "tavmab" && sifre == "1453")
                {
                    return true;
                }
                         
                
            }
            catch
            {

            }
            
            return false;
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (GirisYap() == true)
            {
                Aileler aileler = new Aileler();
                aileler.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre Yanlış");
                textBox1.Clear();
                textBox2.Clear();
            }
        }

       

    }
}
