using System;
using System.Drawing;
using System.Windows.Forms;



namespace ResimCerceveleme
{
    public partial class Form1 : Form
    {
        private Bitmap mainImage; // Ana resim
        private Bitmap frameImage; // Çerçeve resmi
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form ayarlarý
            this.Text = "Hat Yazýsý Çerçeveleyici";
            this.Width = 600;
            this.Height = 500;

            // Resim Yükleme PictureBox
            var pbMain = new PictureBox
            {
                Name = "pbMain",
                Size = new Size(250, 250),
                Location = new Point(10, 10),
                BorderStyle = BorderStyle.Fixed3D,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            this.Controls.Add(pbMain);

            // Çerçeve Yükleme PictureBox
            var pbFrame = new PictureBox
            {
                Name = "pbFrame",
                Size = new Size(250, 250),
                Location = new Point(270, 10),
                BorderStyle = BorderStyle.Fixed3D,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            this.Controls.Add(pbFrame);

            // Resim Yükle Butonu
            var btnLoadImage = new Button
            {
                Text = "Resim Yükle",
                Name = "btnLoadImage",
                Location = new Point(10, 280),
                Size = new Size(120, 30)
            };
            btnLoadImage.Click += BtnLoadImage_Click;
            this.Controls.Add(btnLoadImage);

            // Çerçeve Yükle Butonu
            var btnLoadFrame = new Button
            {
                Text = "Çerçeve Yükle",
                Name = "btnLoadFrame",
                Location = new Point(270, 280),
                Size = new Size(120, 30)
            };
            btnLoadFrame.Click += BtnLoadFrame_Click;
            this.Controls.Add(btnLoadFrame);

            // Temizle Butonu
            var btnClear = new Button
            {
                Text = "Temizle",
                Name = "btnClear",
                Location = new Point(10, 320),
                Size = new Size(120, 30)
            };
            btnClear.Click += BtnClear_Click;
            this.Controls.Add(btnClear);

            // Ýþlemi Tamamla ve Çerçeve Ekle
            var btnProcess = new Button
            {
                Text = "Çerçeve Ekle",
                Name = "btnProcess",
                Location = new Point(270, 320),
                Size = new Size(120, 30)
            };
            btnProcess.Click += BtnProcess_Click;
            this.Controls.Add(btnProcess);
        }

        // Resim Yükle Butonu Click Olayý
        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog
            {
                Filter = "Resim Dosyalarý|.jpeg;.jpg;.png;.bmp;.gif|Tüm Dosyalar|.*",
                Title = "Bir resim seçin",
                Multiselect = false
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(ofd.FileName))
                {
                    try
                    {
                        mainImage = new Bitmap(ofd.FileName); // Ana resmi yükle
                        var pbMain = (PictureBox)this.Controls["pbMain"];
                        pbMain.Image = mainImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Resim yüklenirken bir hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Çerçeve Yükle Butonu Click Olayý
        private void BtnLoadFrame_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog
            {
                Filter = "Resim Dosyalarý|.jpeg;.jpg;.png;.bmp;.gif|Tüm Dosyalar|.*",
                Title = "Bir çerçeve resmi seçin",
                Multiselect = false
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(ofd.FileName))
                {
                    try
                    {
                        frameImage = new Bitmap(ofd.FileName); // Çerçeve resmini yükle
                        var pbFrame = (PictureBox)this.Controls["pbFrame"];
                        pbFrame.Image = frameImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Çerçeve yüklenirken bir hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Temizle Butonu Click Olayý
        private void BtnClear_Click(object sender, EventArgs e)
        {
            mainImage = null;
            frameImage = null;

            var pbMain = (PictureBox)this.Controls["pbMain"];
            pbMain.Image = null;

            var pbFrame = (PictureBox)this.Controls["pbFrame"];
            pbFrame.Image = null;

            MessageBox.Show("Form temizlendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Çerçeve Ekle Butonu Click Olayý
        private void BtnProcess_Click(object sender, EventArgs e)
        {
            if (mainImage == null || frameImage == null)
            {
                MessageBox.Show("Lütfen hem resim hem de çerçeve yükleyin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (Graphics g = Graphics.FromImage(mainImage))
                {
                    // Çerçeve boyutlarýný ayarlama
                    g.DrawImage(frameImage, 0, 0, mainImage.Width, mainImage.Height); // Çerçeveyi resmi üzerine çiz
                }

                var pbMain = (PictureBox)this.Controls["pbMain"];
                pbMain.Image = mainImage;

                MessageBox.Show("Çerçeve baþarýyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Çerçeve eklenirken bir hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
 