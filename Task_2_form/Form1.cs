using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Task_2_form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClearMirroredFolder();


            LoadImageFiles();
        }

        private void LoadImageFiles()
        {
            string[] fileEntries = Directory.GetFiles("images");

            comboBox1.Items.Clear();

            Regex regexExtForImage = new Regex("^((.bmp)|(.gif)|(.tiff?)|(.jpe?g)|(.png))$", RegexOptions.IgnoreCase);

            foreach (string fileName in fileEntries)
            {
                if (regexExtForImage.IsMatch(Path.GetExtension(fileName)))
                {
                    try
                    {
                        comboBox1.Items.Add(Path.GetFileName(fileName));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"‘айл {fileName} не м≥стить картинки, хоча, суд€чи з розширенн€, повинен.");
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedImage = comboBox1.SelectedItem as string;

            if (selectedImage != null)
            {
                string imagePath = Path.Combine("images", selectedImage);
                string newFileName =selectedImage + "-mirrored.gif";
                string outputPath = Path.Combine(@"mirrored", newFileName);

               
                pictureBox1.Image = Image.FromFile(imagePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; 

                
                Image image = Image.FromFile(imagePath);
                image.RotateFlip(RotateFlipType.RotateNoneFlipX);

                image.Save(outputPath, ImageFormat.Gif);

                pictureBox2.Image = image;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom; 
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void ClearMirroredFolder()
        {
            string mirroredFolderPath = "mirrored";

            if (Directory.Exists(mirroredFolderPath))
            {

                string[] files = Directory.GetFiles(mirroredFolderPath);
                foreach (string file in files)
                {
                    File.Delete(file);
                }


                string[] subdirectories = Directory.GetDirectories(mirroredFolderPath);
                foreach (string subdirectory in subdirectories)
                {
                    Directory.Delete(subdirectory, true);
                }
            }
            else
            {

                Directory.CreateDirectory(mirroredFolderPath);
            }
        }
    }
}

        