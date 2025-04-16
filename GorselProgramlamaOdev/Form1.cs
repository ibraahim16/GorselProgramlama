using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GorselProgramlamaOdev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnKaydet.Click += btnKaydet_Click; // ← bu satır butonu bağlar
        }




        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Giriş verileri
            string ad = txtAd.Text;
            string soyad = txtSoyad.Text;
            int gun = Convert.ToInt32(cmbGun.SelectedItem);
            string ay = cmbAy.SelectedItem.ToString();
            int yil = Convert.ToInt32(cmbYil.SelectedItem);
            
            double boy = Convert.ToDouble(txtBoy.Text.Replace(",", "."),
                              System.Globalization.CultureInfo.InvariantCulture);
            double kilo = Convert.ToDouble(txtKilo.Text.Replace(",", "."),
                                           System.Globalization.CultureInfo.InvariantCulture);


            // VKİ hesaplama
            double vki = kilo / (boy * boy);
            string vkiYorum = "";

            if (vki < 18.5)
                vkiYorum = "Zayıf";
            else if (vki < 25)
                vkiYorum = "Normal";
            else if (vki < 30)
                vkiYorum = "Fazla kilolu";
            else
                vkiYorum = "Obez";

            // Burç hesaplama
            string burc = "";
            string burcYorum = "";
            string burcResmi = "";

            if ((ay == "Ocak" && gun >= 22) || (ay == "Şubat" && gun <= 19))
            {
                burc = "Kova";
                burcYorum = "Bağımsız ve yaratıcı.";
                burcResmi = "Images/kova.png";
            }
            else if ((ay == "Şubat" && gun >= 20) || (ay == "Mart" && gun <= 20))
            {
                burc = "Balık";
                burcYorum = "Hayalperest ve sezgiseldir.";
                burcResmi = "Images/balik.png";
            }
            else if ((ay == "Mart" && gun >= 21) || (ay == "Nisan" && gun <= 20))
            {
                burc = "Koç";
                burcYorum = "Lider ruhlu ve enerjik.";
                burcResmi = "Images/koc.png";
            }
            else if ((ay == "Nisan" && gun >= 21) || (ay == "Mayıs" && gun <= 21))
            {
                burc = "Boğa";
                burcYorum = "Sadık ve kararlıdır.";
                burcResmi = "Images/boga.png";
            }
            else if ((ay == "Mayıs" && gun >= 22) || (ay == "Haziran" && gun <= 22))
            {
                burc = "İkizler";
                burcYorum = "Meraklı ve konuşkandır.";
                burcResmi = "Images/ikizler.png";
            }
            else if ((ay == "Haziran" && gun >= 23) || (ay == "Temmuz" && gun <= 22))
            {
                burc = "Yengeç";
                burcYorum = "Duygusal ve koruyucudur.";
                burcResmi = "Images/yengec.png";
            }
            else if ((ay == "Temmuz" && gun >= 23) || (ay == "Ağustos" && gun <= 22))
            {
                burc = "Aslan";
                burcYorum = "Kendine güvenen ve lider ruhlu.";
                burcResmi = "Images/aslan.png";
            }
            else if ((ay == "Ağustos" && gun >= 23) || (ay == "Eylül" && gun <= 22))
            {
                burc = "Başak";
                burcYorum = "Titiz ve detaycıdır.";
                burcResmi = "Images/basak.png";
            }
            else if ((ay == "Eylül" && gun >= 23) || (ay == "Ekim" && gun <= 22))
            {
                burc = "Terazi";
                burcYorum = "Adil ve uyumlu.";
                burcResmi = "Images/terazi.png";
            }
            else if ((ay == "Ekim" && gun >= 23) || (ay == "Kasım" && gun <= 21))
            {
                burc = "Akrep";
                burcYorum = "Gizemli ve tutkulu.";
                burcResmi = "Images/akrep.png";
            }
            else if ((ay == "Kasım" && gun >= 22) || (ay == "Aralık" && gun <= 21))
            {
                burc = "Yay";
                burcYorum = "Gezmeyi ve özgürlüğü sever.";
                burcResmi = "Images/yay.png";
            }
            else if ((ay == "Aralık" && gun >= 22) || (ay == "Ocak" && gun <= 21))
            {
                burc = "Oğlak";
                burcYorum = "Çalışkan ve disiplinlidir.";
                burcResmi = "Images/oglak.png";
            }

            // Sonuçları göster
            lblVKI.Text = vki.ToString("F2");
            lblVKIYorum.Text = vkiYorum;
            lblBurc.Text = burc;
            lblBurcYorum.Text = burcYorum;
            pictureBox1.ImageLocation = burcResmi;





            // Veritabanına kayıt işlemi
            try
            {
                MessageBox.Show("Kayıt işlemi başladı...");
                string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GorselVeritabani;Integrated Security=True";
                SqlConnection con = new SqlConnection(connStr);


                con.Open();

                string sql = "INSERT INTO Kisiler (Ad, Soyad, Gun, Ay, Yil, Boy, Kilo, VKI, VKIYorum, Burc, BurcYorum, BurcResmi) " +
                             "VALUES (@Ad, @Soyad, @Gun, @Ay, @Yil, @Boy, @Kilo, @VKI, @VKIYorum, @Burc, @BurcYorum, @BurcResmi)";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Ad", ad);
                cmd.Parameters.AddWithValue("@Soyad", soyad);
                cmd.Parameters.AddWithValue("@Gun", gun);
                cmd.Parameters.AddWithValue("@Ay", ay);
                cmd.Parameters.AddWithValue("@Yil", yil);
                cmd.Parameters.AddWithValue("@Boy", boy);
                cmd.Parameters.AddWithValue("@Kilo", kilo);
                cmd.Parameters.AddWithValue("@VKI", vki);
                cmd.Parameters.AddWithValue("@VKIYorum", vkiYorum);
                cmd.Parameters.AddWithValue("@Burc", burc);
                cmd.Parameters.AddWithValue("@BurcYorum", burcYorum);
                cmd.Parameters.AddWithValue("@BurcResmi", burcResmi);

                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Kullanıcı veritabanına kaydedildi ✅");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı Hatası ❌: " + ex.Message);
            }

        }
        private void btnKaydet_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Günleri ekle (1–31)
            for (int i = 1; i <= 31; i++)
                cmbGun.Items.Add(i);

            // Ayları ekle
            string[] aylar = {
        "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
        "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"
    };
            cmbAy.Items.AddRange(aylar);

            // Yılları ekle (1960–2025)
            for (int i = 1960; i <= 2025; i++)
                cmbYil.Items.Add(i);
        }

       
    }
}
