using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;

namespace Muhtarlık
{
    public partial class Aileler : Form
    {
        public Aileler()
        {
            InitializeComponent();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Aileler_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            ekleme_panel.Visible = true;
            xmlload();
        }


   

        private void ekleme_button_Click(object sender, EventArgs e)
        {
            XmlInsert();
            xmlload();
            clearForm();

        }

      
        public void XmlInsert()
        {
            string aileadi = ad_ekleme_textbox.Text;
            string kapino = kapino_ekleme_textbox.Text;
            string bireyler=null;

            try
            {
                XDocument xDoc = XDocument.Load("aileler.xml");
                XElement rootelement = xDoc.Root;
                XElement newelement = new XElement("Aile",
                                new XElement("Aile_Adı", aileadi),
                                new XElement("Kapı_No", kapino),
                                new XElement("Bireyler",bireyler)
                            );
                rootelement.Add(newelement);

                xDoc.Save("aileler.xml");            
                        
            }
            catch {
                new XDocument(
                        new XElement("Aileler",
                            new XElement("Aile",
                                new XElement("Aile_Adı", aileadi),
                                new XElement("Kapı_No", kapino),
                                new XElement("Bireyler", bireyler)
                            )
                            )
                        ).Save("aileler.xml");
            }
        }

        public void xmlload()
        {
            try
            {
                //dataGridView1.DataSource = ds.Tables[0];
                XDocument xDoc = XDocument.Load("aileler.xml");
                DataTable dt = new DataTable();
                //dt.Columns.Add("Resim", typeof(Byte[]));
                dt.Columns.Add("Aile Adı", typeof(String));
                dt.Columns.Add("Kapı No", typeof(String));
                dt.Columns.Add("Çalışılan Toplam Gün", typeof(Int32));
                dt.Columns.Add("Verilen Toplam Miktar (TL)", typeof(Int32));
                dt.Columns.Add("Toplam Traktör Varlığı", typeof(Int32));
                dt.Columns.Add("Toplam Otomobil Varlığı", typeof(Int32));
                dt.Columns.Add("Toplam Hayvan Varlığı", typeof(Int32));

                XElement xelement = XElement.Load("aileler.xml");
                IEnumerable<XElement> aileler = xelement.Elements(); 
                foreach (var aile in aileler)
                {
                    /*Bitmap bmp = new Bitmap(aile.Element("Resim").Value);
                    MemoryStream ms = new MemoryStream();
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    byte[] buf = ms.ToArray();
                    dt.Rows.Add(buf);*/
                    IEnumerable<XElement> bireyler = aile.Descendants("Birey");
                    int toplamgun = 0;
                    int toplammiktar=  0;
                    int toplamtraktor = 0;
                    int toplamotomobil = 0;
                    int toplamhayvan = 0;
                    foreach(var birey in bireyler){
                        toplamgun += int.Parse(birey.Element("Toplam_Gün").Value);
                        toplammiktar += int.Parse(birey.Element("Ne_Kadar_Verdiği").Value);
                        toplamtraktor += int.Parse(birey.Element("Traktör_Varlığı").Value);
                        toplamotomobil += int.Parse(birey.Element("Otomobil_Varlığı").Value);
                        toplamhayvan += int.Parse(birey.Element("Hayvan_Varlığı").Value);

                    }

                    dt.Rows.Add(aile.Element("Aile_Adı").Value ,
                        aile.Element("Kapı_No").Value,
                        toplamgun,
                        toplammiktar,
                        toplamtraktor,
                        toplamotomobil,
                        toplamhayvan
                        );
                }
                dataGridView1.DataSource = dt;

                int koytoplamgun = 0;
                int koytoplammiktar = 0;
                int koytoplamtraktor = 0;
                int koytoplamotomobil = 0;
                int koytoplamhayvan = 0;

                for(int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    koytoplamgun += Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
                    koytoplammiktar += Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                    koytoplamtraktor += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                    koytoplamotomobil += Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
                    koytoplamhayvan += Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value);
                }
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = "Köy Toplam : ";
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = koytoplamgun.ToString();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = koytoplammiktar.ToString();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = koytoplamtraktor.ToString();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[5].Value = koytoplamotomobil.ToString();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[6].Value = koytoplamhayvan.ToString();

                int abp = 0;
                int abn = 0;
                int ap = 0;
                int an = 0;
                int bp = 0;
                int bn = 0;
                int sp = 0;
                int sn = 0;

                foreach (var kan in xelement.Descendants("Kan_Grubu"))
                {
                    if(kan.Value == "AB Rh+")
                    {
                        abp++;
                    }
                    if (kan.Value == "AB Rh-")
                    {
                        abn++;
                    }
                    if (kan.Value == "A Rh+")
                    {
                        ap++;
                    }
                    if (kan.Value == "A Rh-")
                    {
                        an++;
                    }
                    if (kan.Value == "B Rh+")
                    {
                        bp++;
                    }
                    if (kan.Value == "B Rh-")
                    {
                        bn++;
                    }
                    if (kan.Value == "0 Rh+")
                    {
                        sp++;
                    }
                    if (kan.Value == "0 Rh-")
                    {
                        sn++;
                    }
                }
                label10.Text = "Köyün Kan Grubu Toplamları : "
                                + "  AB Rh+ = " + abp 
                                + "      AB Rh- = " + abn 
                                + "      A Rh+ = " + ap 
                                + "      A Rh- = " + an 
                                + "      B Rh+ = " + bp 
                                + "      B Rh- = " + bn 
                                + "      0 Rh+ = " + sp 
                                + "      0 Rh- = " + sn ;
                dataGridView1.ClearSelection();
            }
            catch
            {

            }
            
        }
        public void xmldelete()

        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("aileler.xml");
            foreach (XmlNode xnode in xDoc.SelectNodes("Aileler/Aile"))
            {
                if (xnode.SelectSingleNode("Kapı_No").InnerText == kn ) xnode.ParentNode.RemoveChild(xnode);
                xDoc.Save("aileler.xml");
            }
        }

        private void kapino_arama_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        public static String kn;
        public static String aa;
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                kn = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                aa = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            catch
            {

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Ana_Sayfa anasayfa = new Ana_Sayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            xmldelete();
            xmlload();
        }

        public void clearForm()
        {
            ad_ekleme_textbox.Clear();
            kapino_ekleme_textbox.Clear();
        }

        private void Aileler_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        private void Aileler_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearForm();
            xmlload();
        }

    }
}
