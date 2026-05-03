using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM_Sistemi
{
    public class Musteri
    {
        [DisplayName("Müşteri ID")]
        public int musteri_id { get; set; }

        [DisplayName("Müşteri Adı Soyadı")]
        public string ad { get; set; }

        [DisplayName("Telefon Numarası")]
        public string telefon { get; set; }

        public void musteri_ekle(List<Musteri> musteriListesi)
        {
            musteriListesi.Add(this);
        }

        public override string ToString() => ad;
    }

    public class Satis
    {
        [DisplayName("Satış ID")]
        public int satis_id { get; set; }

        [DisplayName("Müşteri Adı")]
        public string musteri_adi { get; set; }

        [DisplayName("Satılan Ürün")]
        public string urun { get; set; }

        [DisplayName("Satış Fiyatı (TL)")]
        public double fiyat { get; set; }
    }

    public class DestekTalebi
    {
        [DisplayName("Talep ID")]
        public int talep_id { get; set; }

        [DisplayName("Müşteri Adı")]
        public string musteri_adi { get; set; }

        [DisplayName("Talep Açıklaması")]
        public string aciklama { get; set; }
    }

    public partial class Form1 : Form
    {
        List<Musteri> musteriler = new List<Musteri>();
        List<Satis> satislar = new List<Satis>();
        List<DestekTalebi> talepler = new List<DestekTalebi>();

        int musteriSayac = 101;
        int satisSayac = 10001;
        int talepSayac = 50001;

        TabControl sekmeler;
        TabPage sekmeMusteri, sekmeSatis, sekmeDestek;
        DataGridView dgvMusteriler, dgvSatislar, dgvTalepler;
        ComboBox cmbSatisMusteri, cmbTalepMusteri;
        TextBox txtMusteriAd, txtTelefon, txtSatisUrun, txtSatisFiyat, txtTalepAciklama;

        public Form1()
        {
            this.Text = "Müşteri İlişkileri Yönetimi (CRM) - 2300005412 Fırat Diricanlı";
            this.Size = new Size(1150, 750);
            this.StartPosition = FormStartPosition.CenterScreen;

            SistemVerileriniHazirla();
            ArayuzuInsaEt();
        }

        private void SistemVerileriniHazirla()
        {
            Musteri m1 = new Musteri { musteri_id = musteriSayac++, ad = "Fırat Diricanlı", telefon = "0555 123 45 67" };
            Musteri m2 = new Musteri { musteri_id = musteriSayac++, ad = "Ali Yılmaz", telefon = "0532 987 65 43" };
            Musteri m3 = new Musteri { musteri_id = musteriSayac++, ad = "Ayşe Demir", telefon = "0505 456 78 90" };
            Musteri m4 = new Musteri { musteri_id = musteriSayac++, ad = "Kemal Sunal", telefon = "0544 111 22 33" };
            Musteri m5 = new Musteri { musteri_id = musteriSayac++, ad = "Zeynep Çelik", telefon = "0533 444 55 66" };

            m1.musteri_ekle(musteriler);
            m2.musteri_ekle(musteriler);
            m3.musteri_ekle(musteriler);
            m4.musteri_ekle(musteriler);
            m5.musteri_ekle(musteriler);

            satislar.Add(new Satis { satis_id = satisSayac++, musteri_adi = m1.ad, urun = "Kurumsal Web Sitesi", fiyat = 15000 });
            satislar.Add(new Satis { satis_id = satisSayac++, musteri_adi = m2.ad, urun = "SEO Danışmanlığı", fiyat = 5000 });
            satislar.Add(new Satis { satis_id = satisSayac++, musteri_adi = m3.ad, urun = "E-Ticaret Paketi", fiyat = 25000 });

            talepler.Add(new DestekTalebi { talep_id = talepSayac++, musteri_adi = m1.ad, aciklama = "Sunucu bağlantı hatası alıyorum." });
            talepler.Add(new DestekTalebi { talep_id = talepSayac++, musteri_adi = m3.ad, aciklama = "Ödeme sistemi entegrasyonu güncellenmeli." });
        }

        private void ArayuzuInsaEt()
        {
            sekmeler = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            sekmeMusteri = new TabPage("Müşteri Yönetimi");
            sekmeSatis = new TabPage("Satış İşlemleri");
            sekmeDestek = new TabPage("Destek Talepleri");

            Panel pnlMusteri = new Panel { Dock = DockStyle.Top, Height = 90, BackColor = Color.WhiteSmoke };
            txtMusteriAd = new TextBox { Location = new Point(20, 30), Width = 200, PlaceholderText = "Ad Soyad" };
            txtTelefon = new TextBox { Location = new Point(240, 30), Width = 150, PlaceholderText = "Telefon Numarası" };
            Button btnMusteriEkle = new Button { Text = "MÜŞTERİ KAYDET", Location = new Point(410, 28), Size = new Size(180, 32), BackColor = Color.SteelBlue, ForeColor = Color.White };
            btnMusteriEkle.Click += (s, e) => MusteriSistemeKaydet();

            dgvMusteriler = TabloOlustur();
            pnlMusteri.Controls.AddRange(new Control[] { txtMusteriAd, txtTelefon, btnMusteriEkle });
            sekmeMusteri.Controls.Add(dgvMusteriler);
            sekmeMusteri.Controls.Add(pnlMusteri);

            Panel pnlSatis = new Panel { Dock = DockStyle.Top, Height = 90, BackColor = Color.WhiteSmoke };
            cmbSatisMusteri = new ComboBox { Location = new Point(20, 30), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            txtSatisUrun = new TextBox { Location = new Point(240, 30), Width = 200, PlaceholderText = "Satılan Ürün / Hizmet" };
            txtSatisFiyat = new TextBox { Location = new Point(460, 30), Width = 120, PlaceholderText = "Tutar (TL)" };
            Button btnSatisKaydet = new Button { Text = "SATIŞI ONAYLA", Location = new Point(600, 28), Size = new Size(180, 32), BackColor = Color.SeaGreen, ForeColor = Color.White };
            btnSatisKaydet.Click += (s, e) => SatisKaydiniOlustur();

            dgvSatislar = TabloOlustur();
            pnlSatis.Controls.AddRange(new Control[] { cmbSatisMusteri, txtSatisUrun, txtSatisFiyat, btnSatisKaydet });
            sekmeSatis.Controls.Add(dgvSatislar);
            sekmeSatis.Controls.Add(pnlSatis);

            Panel pnlTalep = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Color.WhiteSmoke };
            cmbTalepMusteri = new ComboBox { Location = new Point(20, 30), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            txtTalepAciklama = new TextBox { Location = new Point(240, 30), Width = 350, PlaceholderText = "Destek Talebi Açıklaması" };
            Button btnTalepOlustur = new Button { Text = "DESTEK TALEBİ AÇ", Location = new Point(610, 28), Size = new Size(180, 32), BackColor = Color.Indigo, ForeColor = Color.White };
            btnTalepOlustur.Click += (s, e) => DestekTalebiKaydet();

            dgvTalepler = TabloOlustur();
            pnlTalep.Controls.AddRange(new Control[] { cmbTalepMusteri, txtTalepAciklama, btnTalepOlustur });
            sekmeDestek.Controls.Add(dgvTalepler);
            sekmeDestek.Controls.Add(pnlTalep);

            sekmeler.TabPages.AddRange(new TabPage[] { sekmeMusteri, sekmeSatis, sekmeDestek });
            this.Controls.Add(sekmeler);

            TablolariGuncelle();
        }

        private DataGridView TabloOlustur()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
        }

        private void TablolariGuncelle()
        {
            dgvMusteriler.DataSource = null; dgvMusteriler.DataSource = musteriler.ToList();
            dgvSatislar.DataSource = null; dgvSatislar.DataSource = satislar.ToList();
            dgvTalepler.DataSource = null; dgvTalepler.DataSource = talepler.ToList();

            cmbSatisMusteri.Items.Clear();
            cmbTalepMusteri.Items.Clear();

            foreach (var m in musteriler)
            {
                cmbSatisMusteri.Items.Add(m);
                cmbTalepMusteri.Items.Add(m);
            }
        }

        private void MusteriSistemeKaydet()
        {
            if (!string.IsNullOrWhiteSpace(txtMusteriAd.Text) && !string.IsNullOrWhiteSpace(txtTelefon.Text))
            {
                Musteri yeniMusteri = new Musteri
                {
                    musteri_id = musteriSayac++,
                    ad = txtMusteriAd.Text,
                    telefon = txtTelefon.Text
                };

                yeniMusteri.musteri_ekle(musteriler);

                txtMusteriAd.Clear(); txtTelefon.Clear();
                TablolariGuncelle();
                MessageBox.Show("Müşteri kaydı başarıyla sisteme eklenmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen müşteri adını ve telefon numarasını eksiksiz giriniz.", "Eksik Veri", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SatisKaydiniOlustur()
        {
            if (cmbSatisMusteri.SelectedItem is Musteri seciliMusteri && !string.IsNullOrWhiteSpace(txtSatisUrun.Text) && double.TryParse(txtSatisFiyat.Text, out double islemFiyat))
            {
                satislar.Add(new Satis
                {
                    satis_id = satisSayac++,
                    musteri_adi = seciliMusteri.ad,
                    urun = txtSatisUrun.Text,
                    fiyat = islemFiyat
                });

                txtSatisUrun.Clear(); txtSatisFiyat.Clear();
                TablolariGuncelle();
                MessageBox.Show("Satış işlemi başarıyla kaydedilmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen müşteri seçimi yapınız, ürün bilgisini ve sayısal fiyat değerini eksiksiz giriniz.", "Eksik veya Hatalı Veri", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DestekTalebiKaydet()
        {
            if (cmbTalepMusteri.SelectedItem is Musteri seciliMusteri && !string.IsNullOrWhiteSpace(txtTalepAciklama.Text))
            {
                talepler.Add(new DestekTalebi
                {
                    talep_id = talepSayac++,
                    musteri_adi = seciliMusteri.ad,
                    aciklama = txtTalepAciklama.Text
                });

                txtTalepAciklama.Clear();
                TablolariGuncelle();
                MessageBox.Show("Destek talebi başarıyla oluşturulmuştur.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen müşteri seçimi yapınız ve talep açıklamasını giriniz.", "Eksik Veri", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}