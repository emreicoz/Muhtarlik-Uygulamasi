using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Muhtarlık
{

    public partial class KisiBilgiler : Form
    {
        public static string id;
        public static string adsoyad;
        public static string tcno;
        public static string telefon;
        public static string adres;
        public static string cinsiyet;
        public static string dogumtarihi;
        public static string kangrubu;
        public static string ogrenimbilgisi;
        public static string toplamgun;
        public static string verdigiurun;
        public static string nekadarverdigi;
        public static string traktorvarligi;
        public static string otomobilvarligi;
        public static string hayvanvarligi;
        public KisiBilgiler()
        {
            InitializeComponent();
        }

        private void KisiBilgiler_Load(object sender, EventArgs e)
        {
            textBox1.Text = adsoyad;
            textBox2.Text = tcno;
            maskedTextBox1.Text = telefon;
            textBox4.Text = adres;
            comboBox1.Text = cinsiyet;
            if(dogumtarihi == "")
            {
                dateTimePicker1.CustomFormat = " ";
            }
            else
            {
                dateTimePicker1.Value = DateTime.Parse(dogumtarihi);
            }
            
            comboBox2.Text = kangrubu;
            textBox5.Text = ogrenimbilgisi;
            calisilan_gun_textbox.Text = toplamgun;
            textBox7.Text = verdigiurun;
            textBox8.Text = nekadarverdigi;
            textBox3.Text = traktorvarligi;
            textBox10.Text = otomobilvarligi;
            textBox11.Text = hayvanvarligi;
        }

        public bool xmlUpdate()
        {
            if (textBox1.Text != "")
            {
                XDocument xDoc = XDocument.Load("aileler.xml");
                XElement xElement = xDoc.Root;
                IEnumerable<XElement> bireyler = xElement.Descendants("Birey");


                if (calisilan_gun_textbox.Text == "")
                {
                    calisilan_gun_textbox.Text = "0";
                }
                if (textBox8.Text == "")
                {
                    textBox8.Text = "0";
                }
                if (textBox3.Text == "")
                {
                    textBox3.Text = "0";
                }
                if (textBox10.Text == "")
                {
                    textBox10.Text = "0";
                }
                if (textBox11.Text == "")
                {
                    textBox11.Text = "0";
                }

                foreach (var birey in bireyler)
                {
                    if (birey.Element("Id").Value == id)
                    {
                        birey.Element("Ad_Soyad").Value = textBox1.Text;
                        birey.Element("TC_No").Value = textBox2.Text;
                        birey.Element("Telefon").Value = maskedTextBox1.Text;
                        birey.Element("Adres").Value = textBox4.Text;
                        birey.Element("Cinsiyet").Value = comboBox1.Text;
                        birey.Element("Doğum_Tarihi").Value = dateTimePicker1.Value.ToShortDateString();
                        birey.Element("Kan_Grubu").Value = comboBox2.Text;
                        birey.Element("Öğrenim_Bilgisi").Value = textBox5.Text;
                        birey.Element("Toplam_Gün").Value = calisilan_gun_textbox.Text;
                        birey.Element("Verdiği_Ürün").Value = textBox7.Text;
                        birey.Element("Ne_Kadar_Verdiği").Value = textBox8.Text;
                        birey.Element("Traktör_Varlığı").Value = textBox3.Text;
                        birey.Element("Otomobil_Varlığı").Value = textBox10.Text;
                        birey.Element("Hayvan_Varlığı").Value = textBox11.Text;
                    }
                    xDoc.Save("aileler.xml");
                }
                return true;
            }
            else
            {
                MessageBox.Show("Ad Soyad alanı boş bırakılamaz!");
                return false;
            }

            

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(xmlUpdate() == true)
            {
                xmlUpdate();
                Ana_Sayfa ana_Sayfa = (Ana_Sayfa)Application.OpenForms["Ana_Sayfa"];
                ana_Sayfa.xmlload();
                this.Close();
            }
            else
            {

            }
            
            
        }

        private void calisilan_gun_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
