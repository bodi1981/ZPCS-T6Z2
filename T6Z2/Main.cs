using System;
using System.Drawing;
using System.Windows.Forms;
using T6Z2.Properties;

namespace T6Z2
{
    public partial class Main : Form
    {
        public string PicturePath
        {
            get
            {
                return Settings.Default.PicturePath;
            }
            set
            {
                Settings.Default.PicturePath = value;
            }
        }

        public Main()
        {
            InitializeComponent();

            if (PicturePath != "")
            {
                pbPicture.Image = new Bitmap(PicturePath);
                pbPicture.SizeMode = PictureBoxSizeMode.Zoom;
                btnRemove.Visible = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files|*.gif;*.jpg;*.jpeg;*.png";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var path = dialog.FileName;                     //zmienna path powstala w celu aby w przypadku zaczytania 
                        pbPicture.Image = new Bitmap(path);             //nieprawidlowego pliku nie przypisac blednej sciezki do 
                        pbPicture.SizeMode = PictureBoxSizeMode.Zoom;   //zmiennej PicturePath
                        PicturePath = path;
                        btnRemove.Visible = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Wybrany plik nie jest prawidłowym plikiem graficznym", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            pbPicture.Image = null;
            btnRemove.Visible = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (pbPicture == null || pbPicture.Image == null)
                PicturePath = "";

            Settings.Default.Save();
        }
    }
}
