using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Sozluk
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ingkelimegetir()//////////metot////////////////////////////////////////////////////////////////////////////////////
        {
            baglantim.Open();
            OleDbCommand ing_vericek = new OleDbCommand("select ingilizce from ingturkce", baglantim);
            OleDbDataReader oku = ing_vericek.ExecuteReader();
            while (oku.Read())
            {
                listBox1.Items.Add(oku["ingilizce"].ToString());
            }
            baglantim.Close();
        }
        private void liste1_yazdir()/////////metot////////////////////////////////////////////////////////////////////////////////////
        {
            baglantim.Open();
            OleDbCommand ing_vericek = new OleDbCommand("select ingilizce from ingturkce", baglantim);
            OleDbDataReader oku = ing_vericek.ExecuteReader();
            while (oku.Read())
            {
                listBox1.Items.Add(oku["ingilizce"].ToString());
            }
            baglantim.Close();
        }

        ///////////////////// BAĞLANTIMIZIN YAPILDIĞI KISIM//////////////////////////////////////////////////////////////////////////////////////////////
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source="+Application.StartupPath+ "\\vt_sozluk.accdb");
        private void Form1_Load(object sender, EventArgs e)
        {
            //FORM LOAD
            ingkelimegetir();
        }
        int puanskor = 0;
        
        ////////////////////////// kelime ekleme kısmı /////////////////////////////////////////////////////////////////////////
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Boş metin kaydedilemez!\n\nLütfen Kontrol Edip Tekrar Deneyin!", "Uyarı");
            }
            else
            {
                try
                {
                    

                    baglantim.Open();
                    OleDbCommand eklekomut = new OleDbCommand("insert into ingturkce(ingilizce,turkce)values('" + textBox2.Text + "','" + textBox3.Text + "')", baglantim);
                    eklekomut.ExecuteNonQuery();
                    baglantim.Close();
                    MessageBox.Show("Başarıyla Kaydedildi.", "Bilgi");
                   
                    textBox2.Clear();
                    textBox3.Clear();
                    listBox1.Items.Clear();
                    ingkelimegetir();

                }
                catch (Exception mesaj)
                {

                    MessageBox.Show(mesaj.Message, "Uyarı");
                    baglantim.Close();
                }
            }
            
        }
        //////////////////////BAŞLAT BUTONU ÖZELLİKLER EVENTLERİ/////////////////////////////////////////////////////////////////////////////////    
        private void button1_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            puanlabel.Enabled = true;
            label1.Enabled = true;
            label2.Enabled = true;
            label3.Enabled = true;
            
            dogru_yanlis.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            button3.Enabled = true;
            randomkelime.Enabled = true;
            textBox1.Enabled = true;
            button1.Enabled = false;
            button1.BackColor = Color.Gray;
            button4.BackColor = Color.OrangeRed;
            tabPage1.BackColor = Color.Khaki;

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            
            radioButton2.Checked = true; //ipucu kapalı olsun.

            ingkelimegetir();//yukarıdaki metodu çağırdık..21.satırdaki

            Random rnd = new Random();
            int rndsayi = rnd.Next(listBox1.Items.Count);
            randomkelime.Text = Convert.ToString(listBox1.Items[rndsayi]);
            baglantim.Open();
            OleDbCommand kiyasla = new OleDbCommand("select ingilizce,turkce from ingturkce where ingilizce ='" + randomkelime.Text + "'", baglantim);
            OleDbDataReader bak = kiyasla.ExecuteReader();
            while (bak.Read())
            {
                listBox2.Items.Add(bak["turkce"].ToString());
            }
            baglantim.Close();
        }
        //////// İPUCU AÇ KAPAT  ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked ==true)//göster
            {
                listBox2.Visible = true;
            }
            if (radioButton2.Checked==true)
            {
                listBox2.Visible = false;
            }
        }
        ///////////  KONTROL ET BUTONU   ///////////////////////////////////////////////////////////////////////////////////////////
        private void button3_Click(object sender, EventArgs e)
        {
            //-------------------DOĞRU YANLIŞ KISMI---------------------
            if (textBox1.Text == listBox2.Items[0].ToString())
            {
                puanskor += 10;
                dogru_yanlis.ForeColor = Color.Green;
                dogru_yanlis.Text = "Doğru!";

            }
            else if (textBox1.Text != listBox2.Items[0].ToString())
            {
                puanskor -= 10;
                dogru_yanlis.ForeColor = Color.Red;
                dogru_yanlis.Text = "Yanlış!";
            }

            ///--------------------------------------------------------
            try
            {
               

                puanlabel.Text = Convert.ToString(puanskor);
                listBox2.Items.Clear();
                

                radioButton2.Checked = true; //ipucu kapalı olsun.
               
                Random rnd = new Random();
                int rndsayi = rnd.Next(listBox1.Items.Count );
                randomkelime.Text = Convert.ToString(listBox1.Items[rndsayi]);
                textBox1.Clear();
                
                ///+++++++++++++++++ KIYASLAMA ++++++++++++++++++++++++++++++++++
                baglantim.Open();
                OleDbCommand kiyasla = new OleDbCommand("select ingilizce,turkce from ingturkce where ingilizce ='" + randomkelime.Text + "'", baglantim);
                OleDbDataReader bak = kiyasla.ExecuteReader();
                while (bak.Read())
                {
                    listBox2.Items.Add(bak["turkce"].ToString());
                }
                baglantim.Close();
                ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            }
            catch (Exception mesaj)
            {

                MessageBox.Show(mesaj.Message, "Uyarı");
                baglantim.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            puanlabel.Enabled = false;
            label2.Enabled = false;
            button4.Enabled = false;
          
            dogru_yanlis.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            button3.Enabled = false;
            randomkelime.Enabled = false;
            textBox1.Enabled = false;
            button1.Enabled = true;
            
            puanskor = 0;
            puanlabel.Text = "0";
            dogru_yanlis.Text = "";
            button1.BackColor = Color.DeepSkyBlue;
            button4.BackColor = Color.Gray;
            tabPage1.BackColor = Color.Transparent;
        }

        private void button5_Click(object sender, EventArgs e) //güncelle buton
        {
            try
            {

                if (textBox2.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("Güncelleme Başarısız Oldu!\n\nLütfen Kontrol Edip Tekrar Deneyin!\n\nSatırlar boş kalamaz!", "Uyarı");
                }
                else
                {
                    bool bulundu = false;
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        if (Convert.ToString(listBox1.Items[i]) == textBox2.Text)
                        {
                            baglantim.Open();
                            OleDbCommand guncellekomut = new OleDbCommand("update ingturkce set turkce='" + textBox3.Text + "'where ingilizce='" + textBox2.Text + "'", baglantim);
                            guncellekomut.ExecuteNonQuery();
                            baglantim.Close();
                            MessageBox.Show("Başarıyla Güncellendi", "Bilgi");
                            textBox2.Clear(); textBox3.Clear();
                            bulundu = true;
                        }  
                    }
                    if (bulundu == false)
                    {
                        MessageBox.Show("Girilen kelime mevcut olmadığı için\nGüncelleme Başarısız Oldu.", "Uyarı");
                        textBox2.Clear(); textBox3.Clear();
                    }
                }
               


            }
            catch (Exception mesaj)
            {

                MessageBox.Show(mesaj.Message, "Uyarı");
                baglantim.Close();
            }


        }

        private void button6_Click(object sender, EventArgs e)//sil butonu
        {

            try
            {

                if (textBox2.Text == "" && textBox3.Text == "")
                {
                    MessageBox.Show("Silme İşlemi Başarısız Oldu!\n\nLütfen Kontrol Edip Tekrar Deneyin!\n\nSatırlar boş kalamaz!", "Uyarı");
                }
                else
                {
                   

                    bool bulundu = false;
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        if (Convert.ToString(listBox1.Items[i]) == textBox2.Text)
                        {
                            baglantim.Open();
                            OleDbCommand sillekomut = new OleDbCommand("delete from ingturkce where ingilizce='" + textBox2.Text + "'", baglantim);
                            sillekomut.ExecuteNonQuery();
                            baglantim.Close();
                            MessageBox.Show("Başarıyla Silindi", "Bilgi");
                            textBox2.Clear(); textBox3.Clear();
                            liste1_yazdir();
                            bulundu = true;
                        }
                    }
                    if (bulundu == false)
                    {
                        MessageBox.Show("Girilen kelime mevcut olmadığı için\nSilme İşlemi Başarısız Oldu.", "Uyarı");
                        textBox2.Clear(); textBox3.Clear();
                    }
                    listBox1.Items.Clear();
                    liste1_yazdir();

                }
                
            }
            catch (Exception mesaj)
            {

                MessageBox.Show(mesaj.Message,"Uyarı");
                baglantim.Close();
            }
            

        }
    }
    }

