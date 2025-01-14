using System;
using System.Drawing;
using System.Windows.Forms;



namespace ResimCerceveleme
{
    public partial class Form1 : Form
    {
        private Bitmap mainImage; // Ana resim
        private Bitmap frameImage; // �er�eve resmi
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form ayarlar�
            this.Text = "Hat Yaz�s� �er�eveleyici";
            this.Width = 600;
            this.Height = 500;

            // Resim Y�kleme PictureBox
            var pbMain = new PictureBox
            {
                Name = "pbMain",
                Size = new Size(250, 250),
                Location = new Point(10, 10),
                BorderStyle = BorderStyle.Fixed3D,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            this.Controls.Add(pbMain);

            // �er�eve Y�kleme PictureBox
            var pbFrame = new PictureBox
            {
                Name = "pbFrame",
                Size = new Size(250, 250),
                Location = new Point(270, 10),
                BorderStyle = BorderStyle.Fixed3D,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            this.Controls.Add(pbFrame);

            // Resim Y�kle Butonu
            var btnLoadImage = new Button
            {
                Text = "Resim Y�kle",
                Name = "btnLoadImage",
                Location = new Point(10, 280),
                Size = new Size(120, 30)
            };
            btnLoadImage.Click += BtnLoadImage_Click;
            this.Controls.Add(btnLoadImage);

            // �er�eve Y�kle Butonu
            var btnLoadFrame = new Button
            {
                Text = "�er�eve Y�kle",
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

            // ��lemi Tamamla ve �er�eve Ekle
            var btnProcess = new Button
            {
                Text = "�er�eve Ekle",
                Name = "btnProcess",
                Location = new Point(270, 320),
                Size = new Size(120, 30)
            };
            btnProcess.Click += BtnProcess_Click;
            this.Controls.Add(btnProcess);
        }

        // Resim Y�kle Butonu Click Olay�
        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog
            {
                Filter = "Resim Dosyalar�|.jpeg;.jpg;.png;.bmp;.gif|T�m Dosyalar|.*",
                Title = "Bir resim se�in",
                Multiselect = false
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(ofd.FileName))
                {
                    try
                    {
                        mainImage = new Bitmap(ofd.FileName); // Ana resmi y�kle
                        var pbMain = (PictureBox)this.Controls["pbMain"];
                        pbMain.Image = mainImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Resim y�klenirken bir hata olu�tu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // �er�eve Y�kle Butonu Click Olay�
        private void BtnLoadFrame_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog
            {
                Filter = "Resim Dosyalar�|.jpeg;.jpg;.png;.bmp;.gif|T�m Dosyalar|.*",
                Title = "Bir �er�eve resmi se�in",
                Multiselect = false
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(ofd.FileName))
                {
                    try
                    {
                        frameImage = new Bitmap(ofd.FileName); // �er�eve resmini y�kle
                        var pbFrame = (PictureBox)this.Controls["pbFrame"];
                        pbFrame.Image = frameImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"�er�eve y�klenirken bir hata olu�tu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Temizle Butonu Click Olay�
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

        // �er�eve Ekle Butonu Click Olay�
        private void BtnProcess_Click(object sender, EventArgs e)
        {
            if (mainImage == null || frameImage == null)
            {
                MessageBox.Show("L�tfen hem resim hem de �er�eve y�kleyin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (Graphics g = Graphics.FromImage(mainImage))
                {
                    // �er�eve boyutlar�n� ayarlama
                    g.DrawImage(frameImage, 0, 0, mainImage.Width, mainImage.Height); // �er�eveyi resmi �zerine �iz
                }

                var pbMain = (PictureBox)this.Controls["pbMain"];
                pbMain.Image = mainImage;

                MessageBox.Show("�er�eve ba�ar�yla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�er�eve eklenirken bir hata olu�tu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
 