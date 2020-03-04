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
using System.Xml;

namespace Muhtarlık
{
    public partial class Ana_Sayfa : Form
    {
        public Ana_Sayfa()
        {
            InitializeComponent();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void Ana_Sayfa_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            xmlload();
            label15.Text = "Aile Adı : " + Aileler.aa + " /  Kapı No : " + Aileler.kn;


        }
        public void XmlInsert()
        {
            if (textBox1.Text != "")
            {
                string adsoyad = textBox1.Text;
                string tcno = textBox2.Text;
                string telefon = maskedTextBox1.Text;
                string adres = textBox4.Text;
                string cinsiyet = comboBox1.Text;
                string dogumtarihi = dateTimePicker1.Value.ToShortDateString();
                string kangrubu = comboBox2.Text;
                string ogrenimbilgisi = textBox5.Text;
                string toplamgun = calisilan_gun_textbox.Text;
                string verdigiurun = textBox7.Text;
                string nekadarverdigi = textBox8.Text;
                string traktorvarligi = textBox3.Text;
                string otomobilvarligi = textBox10.Text;
                string hayvanvarligi = textBox11.Text;
                if(dateTimePicker1.CustomFormat == " ")
                {
                    dogumtarihi = "";
                }
                if (calisilan_gun_textbox.Text == "")
                {
                    toplamgun = "0";
                }
                if(textBox8.Text == "")
                {
                    nekadarverdigi = "0";
                }
                if(textBox3.Text == "")
                {
                    traktorvarligi = "0";
                }
                if(textBox10.Text == "")
                {
                    otomobilvarligi = "0";
                }
                if(textBox11.Text == "")
                {
                    hayvanvarligi = "0";
                }
                


                XDocument xDoc = XDocument.Load("aileler.xml");
                XElement rootelement = xDoc.Root;
                int id = xDoc.Descendants("Id").Count();
                IEnumerable<XElement> aileler = rootelement.Elements();
                foreach (var aile in aileler)
                {
                    if (aile.Element("Kapı_No").Value == Aileler.kn)
                    {
                        XElement bireylerelement = aile.Element("Bireyler");
                        XElement newelement = new XElement("Birey",
                                                new XElement("Id", id),
                                                new XElement("Ad_Soyad", adsoyad),
                                                new XElement("TC_No", tcno),
                                                new XElement("Telefon", telefon),
                                                new XElement("Adres", adres),
                                                new XElement("Cinsiyet", cinsiyet),
                                                new XElement("Doğum_Tarihi", dogumtarihi),
                                                new XElement("Kan_Grubu", kangrubu),
                                                new XElement("Öğrenim_Bilgisi", ogrenimbilgisi),
                                                new XElement("Toplam_Gün", toplamgun),
                                                new XElement("Verdiği_Ürün", verdigiurun),
                                                new XElement("Ne_Kadar_Verdiği", nekadarverdigi),
                                                new XElement("Traktör_Varlığı", traktorvarligi),
                                                new XElement("Otomobil_Varlığı", otomobilvarligi),
                                                new XElement("Hayvan_Varlığı", hayvanvarligi)
                                );
                        bireylerelement.Add(newelement);
                        xDoc.Save("aileler.xml");
                    }
                }
            }
            else
            {
                MessageBox.Show("Ad Soyad boş bırakılamaz!");
            }


        }
        public void xmldelete()

        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("aileler.xml");
            foreach (XmlNode xnode in xDoc.SelectNodes("Aileler/Aile/Bireyler/Birey"))
            {
                if (xnode.SelectSingleNode("Id").InnerText == kisi) xnode.ParentNode.RemoveChild(xnode);
                xDoc.Save("aileler.xml");
            }
        }

        public void xmlload()
        {

            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(Int16));
                dt.Columns.Add("Ad Soyad", typeof(String));
                dt.Columns.Add("TC No", typeof(String));
                dt.Columns.Add("Telefon", typeof(String));
                dt.Columns.Add("Adres", typeof(String));
                dt.Columns.Add("Cinsiyet", typeof(String));
                dt.Columns.Add("Doğum Tarihi", typeof(String));
                dt.Columns.Add("Kan Grubu", typeof(String));
                dt.Columns.Add("Öğrenim Bilgisi", typeof(String));
                dt.Columns.Add("Çalışılan Gün", typeof(Int16));
                dt.Columns.Add("Verdiği Ürün", typeof(String));
                dt.Columns.Add("Ne Kadar Verdiği", typeof(String));
                dt.Columns.Add("Traktör Varlığı", typeof(String));
                dt.Columns.Add("Otomobil Varlığı", typeof(String));
                dt.Columns.Add("Hayvan Varlığı", typeof(String));

                XElement xelement = XElement.Load("aileler.xml");
                IEnumerable<XElement> aileler = xelement.Elements();
                foreach (var aile in aileler)
                {
                    if (aile.Element("Kapı_No").Value == Aileler.kn)
                    {
                        XElement bireylerelement = aile.Element("Bireyler");
                        IEnumerable<XElement> bireyler = bireylerelement.Elements();
                        foreach (var birey in bireyler)
                        {
                            dt.Rows.Add(birey.Element("Id").Value,
                                        birey.Element("Ad_Soyad").Value,
                                        birey.Element("TC_No").Value,
                                        birey.Element("Telefon").Value,
                                        birey.Element("Adres").Value,
                                        birey.Element("Cinsiyet").Value,
                                        birey.Element("Doğum_Tarihi").Value,
                                        birey.Element("Kan_Grubu").Value,
                                        birey.Element("Öğrenim_Bilgisi").Value,
                                        birey.Element("Toplam_Gün").Value,
                                        birey.Element("Verdiği_Ürün").Value,
                                        birey.Element("Ne_Kadar_Verdiği").Value,
                                        birey.Element("Traktör_Varlığı").Value,
                                        birey.Element("Otomobil_Varlığı").Value,
                                        birey.Element("Hayvan_Varlığı").Value);
                        }
                        dataGridView1.DataSource = dt;
                        dataGridView1.ClearSelection();
                    }
                }

            }
            catch
            {

            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmlInsert();
            xmlload();
            clearForm();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        public void clearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            maskedTextBox1.Clear();
            textBox4.Clear();
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.CustomFormat = " ";
            comboBox2.SelectedIndex = -1;
            textBox5.Clear();
            calisilan_gun_textbox.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox3.Clear();
            textBox10.Clear();
            textBox11.Clear();

        }

        private void Ana_Sayfa_FormClosed(object sender, FormClosedEventArgs e)
        {
            Aileler aileform = (Aileler)Application.OpenForms["Aileler"];
            aileform.xmlload();
            aileform.Show();
        }

        public static String kisi;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            xmldelete();
            xmlload();

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                kisi = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            catch
            {

            }
        }

        private void calisilan_gun_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void calisilan_gun_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd.MM.yyyy";
        }

        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            KisiBilgiler.id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            KisiBilgiler.adsoyad = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            KisiBilgiler.tcno = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            KisiBilgiler.telefon = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            KisiBilgiler.adres = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            KisiBilgiler.cinsiyet = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            KisiBilgiler.dogumtarihi = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            KisiBilgiler.kangrubu = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            KisiBilgiler.ogrenimbilgisi = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            KisiBilgiler.toplamgun = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            KisiBilgiler.verdigiurun = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            KisiBilgiler.nekadarverdigi = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            KisiBilgiler.traktorvarligi = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            KisiBilgiler.otomobilvarligi = dataGridView1.CurrentRow.Cells[13].Value.ToString();
            KisiBilgiler.hayvanvarligi = dataGridView1.CurrentRow.Cells[14].Value.ToString();

            KisiBilgiler kisiBilgiler = new KisiBilgiler();
            kisiBilgiler.Show();
        }
    }
}
