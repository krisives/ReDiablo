using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReDiablo.Data;
using System.IO;
using System.Xml.Serialization;
using System.Security.Cryptography;

namespace ReDiablo
{
    public partial class GfxViewer : Form
    {
        bool debug = true;

        GfxLoader loader = new GfxLoader();
        Color[] palette = new Color[256];
        String fileName = null;
        byte[] data = null;
        Bitmap[] frames = null;
        String hash = null;
        MetaData meta = null;

        bool isOpening = false;
        bool handling = false;

        Brush bgBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
        Brush bgBrushSquare = new SolidBrush(Color.FromArgb(128, 128, 128, 128));
        Pen outlinePen = new Pen(Color.FromArgb(64, 64, 64));

        int zoom = 2;
        int fileVersion = 1;

        public GfxViewer()
        {
            InitializeComponent();
            createPalette();
            meta = new MetaData();
        }

        protected void createPalette()
        {
            byte[] data = File.ReadAllBytes("diablo.pal");

            for (int i = 0; i < 256; i++)
            {
                palette[i] = Color.FromArgb(data[i * 3], data[i * 3 + 1], data[i * 3 + 2]);
            }
        }

        protected void openFile(String fileName)
        {
            cleanupFile();

            isOpening = true;
            handling = true;

            this.fileName = fileName;

            int width = Decimal.ToInt32(currentWidth.Value);
            int height = Decimal.ToInt32(currentHeight.Value);
            if (width <= 0) { width = 96; }
            if (height <= 0) { height = 96; }

            fileVersion = fileName.Contains(".cl2") ? 2 : 1;
            data = File.ReadAllBytes(fileName);

            // Calculate file hash used to store meta data
            MD5 md5Hash = MD5.Create();
            hash = BitConverter.ToString(md5Hash.ComputeHash(data)).Replace("-", "");

            frames = new Bitmap[loader.readFrameCount(data)];

            for (int i = 0; i < frames.Length; i++ )
            {
                int customWidth = meta.getWidth(hash, i, width);
                int customHeight = meta.getHeight(hash, i, height);

                if (fileVersion == 2)
                {
                    frames[i] = loader.readFrame2(data, customWidth, customHeight, palette, i);
                }
                else
                {
                    frames[i] = loader.readFrame(data, customWidth, customHeight, palette, i);
                }
                
            }

            currentFrame.Value = 0;
            currentFrame.Maximum = Math.Max(0, frames.Length - 1);
            currentWidth.Value = frames[0].Width;
            currentHeight.Value = frames[0].Height;
            maxFrame.Text = "/ " + (frames.Length-1);

            closeToolStripMenuItem.Enabled = true;

            
            canvas.Refresh();
            isOpening = false;
            handling = false;
        }

        protected void cleanupFile()
        {
            if (data != null)
            {
                data = null;
            }

            if (frames != null)
            {
                foreach (Bitmap frame in frames)
                {
                    if (frame != null)
                    {
                        frame.Dispose();
                    }
                }

                frames = null;
            }
        }

        protected void closeFile()
        {
            cleanupFile();

            fileName = null;
            currentFrame.Value = 0;
            currentFrame.Maximum = 0;
            currentWidth.Value = 0;
            currentHeight.Value = 0;
            maxFrame.Text = "/ 0";
            hash = null;

            closeToolStripMenuItem.Enabled = false;
        }

        protected void reload()
        {
            if (isOpening)
            {
                return;
            }

            isOpening = true;

            String fileName = this.fileName;
            int frame = Decimal.ToInt32(currentFrame.Value);
            int width = Decimal.ToInt32(currentWidth.Value);
            int height = Decimal.ToInt32(currentHeight.Value);

            if (fileName == null)
            {
                return;
            }

            cleanupFile();
            currentWidth.Value = width;
            currentHeight.Value = height;
            openFile(fileName);
            currentFrame.Value = frame;

            canvas.Refresh();

            isOpening = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CEL/CL2 files (*.cel,*.cl2)|*.cel;*.cl2|CEL files (*.cel)|*.cel|CL2 files (*.cl2)|*.cl2|All files (*.*)|*.*";

            switch (dialog.ShowDialog())
            {
                case DialogResult.OK:
                    break;
                default:
                    return;
            }

            //try
            //{
                openFile(dialog.FileName);
            //}
            //catch (Exception error)
           // {
            //    Console.WriteLine("{0}", error);
            //    MessageBox.Show(String.Format("Error loading file: {0}", error.Message));
            //}
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            if (frames == null || frames.Length <= 0)
            {
                return;
            }

            if (currentFrame.Value > frames.Length)
            {
                return;
            }

            int frameIndex = Decimal.ToInt32(currentFrame.Value);
            Bitmap draw = frames[frameIndex];

            if (draw == null)
            {
                return;
            }

            int centerX = canvas.Width / 2 - (draw.Width / 2) * zoom;
            int centerY = canvas.Height / 2 - (draw.Height / 2) * zoom;
            int zwidth = draw.Width * zoom;
            int zheight = draw.Height * zoom;

            e.Graphics.FillRectangle(bgBrush, centerX, centerY, zwidth, zheight);
            int toggle = 0;

            RectangleF oldBounds = e.Graphics.ClipBounds;
            RectangleF clipBounds = new RectangleF(centerX, centerY, zwidth, zheight);
            e.Graphics.SetClip(clipBounds);

            for (int y = 0; y < zheight; y += 8*zoom)
            {
                for (int x = toggle*8*zoom; x < zwidth; x += 16*zoom)
                {
                    e.Graphics.FillRectangle(bgBrushSquare, centerX + x, centerY + y, 8*zoom, 8*zoom);
                }

                toggle = (toggle == 0) ? 1 : 0;
            }

            e.Graphics.SetClip(oldBounds);
            e.Graphics.DrawRectangle(outlinePen, clipBounds.X-1, clipBounds.Y-1, clipBounds.Width+2, clipBounds.Height+2);

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.DrawImage(draw, centerX, centerY, zwidth, zheight);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                closeFile();
            }
            catch (Exception error)
            {
                
            }

            Application.Exit();
        }

        private void currentFrame_ValueChanged(object sender, EventArgs e)
        {
            if (handling)
            {
                return;
            }

            int frame = Decimal.ToInt32(currentFrame.Value);

            if (frames == null || frame >= frames.Length || frames[frame] == null)
            {
                return;
            }

            handling = true;
            currentWidth.Value = frames[frame].Width;// customWidths[frame];
            currentHeight.Value = frames[frame].Height;// customHeights[frame];
            handling = false;

            canvas.Refresh();
        }

        private void currentWidth_ValueChanged(object sender, EventArgs e)
        {
            int frame = Decimal.ToInt32(currentFrame.Value);

            if (hash != null)
            {
                meta.setWidth(hash, frame, Decimal.ToInt32(currentWidth.Value));
            }

            if (!handling)
            {
                reload();
            }

            canvas.Refresh();
        }

        private void currentHeight_ValueChanged(object sender, EventArgs e)
        {
            int frame = Decimal.ToInt32(currentFrame.Value);

            if (hash != null)
            {
                meta.setHeight(hash, frame, Decimal.ToInt32(currentHeight.Value));
            }

            if (!handling)
            {
                reload();
            }

            canvas.Refresh();
        }

        private void nextFrameButton_Click(object sender, EventArgs e)
        {
            int frame = Decimal.ToInt32(currentFrame.Value);

            if (frame >= Decimal.ToInt32(currentFrame.Maximum)) {
                return;
            }

            int width = Decimal.ToInt32(currentWidth.Value);
            int height = Decimal.ToInt32(currentHeight.Value);
            
            if (hash != null)
            {
                if (!meta.hasMeta(hash, frame + 1))
                {
                    meta.setSize(hash, frame + 1, width, height);
                }
            }

            currentFrame.Value++;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                meta.save();
                MessageBox.Show("Meta data has been saved");
            }
            catch (Exception error)
            {
                MessageBox.Show("Failed to save meta data: " + error.Message);
            }
        }

        public void setZoom(int level)
        {
            level = Math.Max(level, 1);
            level = Math.Min(4, level);
            zoom = level;

            switch (zoom)
            {
                case 1:
                    zoomX1ToolStripMenuItem.Checked = true;
                    zoomX2ToolStripMenuItem.Checked = false;
                    zoomX4ToolStripMenuItem.Checked = false;
                    break;
                case 2:
                    zoomX1ToolStripMenuItem.Checked = false;
                    zoomX2ToolStripMenuItem.Checked = true;
                    zoomX4ToolStripMenuItem.Checked = false;
                    break;
                case 3:
                case 4:
                    zoomX1ToolStripMenuItem.Checked = false;
                    zoomX2ToolStripMenuItem.Checked = false;
                    zoomX4ToolStripMenuItem.Checked = true;
                    break;
            }

            canvas.Refresh();
        }

        private void zoomX2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setZoom(2);
        }

        private void zoomX1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setZoom(1);
        }

        private void zoomX4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setZoom(4);
        }

        private void GfxViewer_Resize(object sender, EventArgs e)
        {
            canvas.Refresh();
        }
    }

    class MetaData
    {
        Dictionary<String, Size> table = new Dictionary<String, Size>();

        public MetaData()
        {
            if (!File.Exists("metagfx.csv"))
            {
                return;
            }

            foreach (String lineData in File.ReadAllLines("metagfx.csv"))
            {
                String line = lineData.Trim();

                if (line.Length <= 0)
                {
                    continue;
                }

                String[] parts = line.Split(',');
                String hash = parts[0];
                int width = Int32.Parse(parts[1]);
                int height = Int32.Parse(parts[2]);
                    
                if (hash == "")
                {
                    continue;
                }

                if (width <= 0 && height <= 0)
                {
                    continue;
                }

                table[hash] = new Size(width, height);
            }
        }

        public void save()
        {
            String[] lines = new String[table.Count];
            int i = 0;

            foreach (String hash in table.Keys) 
            {
                Size size = table[hash];
                lines[i++] = String.Format("{0},{1},{2}", hash, size.Width, size.Height);
            }

            File.WriteAllLines("metagfx.csv", lines);
        }

        public bool hasMeta(String hash, int frame)
        {
            return table.ContainsKey(hash + ":" + frame);
        }

        public void setSize(String hash, int frame, int width, int height)
        {
            table[hash + ":" + frame] = new Size(width, height);
        }

        public int getWidth(String hash, int frame)
        {
            hash = hash + ":" + frame;
            return table.ContainsKey(hash) ? table[hash].Width : 0;
        }

        public int getWidth(String hash, int frame, int y)
        {
            int x = getWidth(hash, frame);
            return x > 0 ? x : y;
        }

        public int getHeight(String hash, int frame)
        {
            hash = hash + ":" + frame;
            return table.ContainsKey(hash) ? table[hash].Height : 0;
        }

        public int getHeight(String hash, int frame, int y)
        {
            int x = getHeight(hash, frame);
            return x > 0 ? x : y;
        }

        public void setWidth(String hash, int frame, int width)
        {
            setSize(hash, frame, width, getHeight(hash, frame));
        }

        public void setHeight(String hash, int frame, int height)
        {
            setSize(hash, frame, getWidth(hash, frame), height);
        }
    }
}
