using BreakTheCodeFromSVAPSSystems.Interfaces;
using BreakTheCodeFromSVAPSSystems.Models;
using BreakTheCodeFromSVAPSSystems.Painters;
using BreakTheCodeFromSVAPSSystems.Readers;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace BreakTheCodeFromSVAPSSystems
{
    public partial class Form1 : Form
    {
        string programDirectory = Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName;
        MainModel model;
        OpenFileDialog openFileDialog;
        SaveFileDialog saveFileDialog;
        Bitmap bmp;
        IPainter painter;
        public Form1()
        {
            InitializeComponent();
            openFileDialog = new OpenFileDialog
            {
                InitialDirectory = programDirectory + @"\Data",
                FileName = "",
                Filter = "xml file|*.xml",
                DefaultExt = "xml"
            };
            saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = programDirectory,
                Filter = "jpg file|*.jpg",
                DefaultExt = "jpg"
            };
        }

        private void openXmlFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    IReader<MainModel> reader = new ReaderXML();
                    model = reader.Read(openFileDialog.FileName);
                    pictureBox1.Width = model.OriginalDocumentWidth;
                    pictureBox1.Height = model.OriginalDocumentHeight;
                    bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    painter = new Painter2D(model, Graphics.FromImage(bmp), new Pen(Color.Black, 3));
                    painter.Paint();
                    pictureBox1.Image = bmp;
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сould not read file!!! " + ex.Message);
            }
        }

        private void saveAsJPGFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);
                    bmp.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
                    bmp.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сould not save file!!! " + ex.Message);
            }
        }
    }
}
