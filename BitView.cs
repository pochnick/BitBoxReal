using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitBox
{
    public partial class BitView : UserControl
    {
        ViewingData data;
        
        private const int BIT_SIZE = 10;
        private readonly Color BIT_ONE_COLOR = Color.Blue;
        private readonly Color BIT_ZERO_COLOR = Color.White;
        private readonly Color BIT_ONE_START_SEG_COLOR = Color.DarkBlue;
        private readonly Color BIT_ZERO_START_SEG_COLOR = Color.Pink;
        private readonly Color BACKGROUND_COLOR = Color.Black;
        private readonly Color BETWEEN_BITS_COLOR = Color.Gray;
        private readonly Color GRID_COLOR = Color.Red;
        private ulong xPosition;
        private ulong yPosition;
        private uint width1;
        private uint width2;
        private uint gridSizeX = 8;
        private uint gridSizeY = 8;


        public BitView()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            xPosition = 0;
            yPosition = 0;
            width1 = 8;
            width2 = 32;
        }

        public new void Invalidate()
        {
            bitsArea.Invalidate();
            base.Invalidate();
        }

        #region parameters

        public ViewingData Data
        {
            get
            {
                return data;
            }
        }

        public void SetData(ViewingData data)
        {
            this.data = data;
            bitsArea.Invalidate();
        }

        public uint BitsWidth1
        {
            get
            {
                return width1;
            }
        }
        public uint BitsWidth2
        {
            get
            {
                return width2;
            }
        }

        public uint GridSizeX
        {
            get
            {
                return gridSizeX;
            }
        }
        public uint GridSizeY
        {
            get
            {
                return gridSizeY;
            }
        }

        #endregion

        public void ChangeBitWidth(uint width1, uint width2)
        {
            if (width1 == 0)
                width1 = 1;
            if (width2 == 0)
                width2 = 1;
            this.width1 = width1;
            this.width2 = width2;
            data.UpdateSegsLineView(width1 * width2);
            data.findSegAtLine(0, 0);
            bitsArea.Invalidate();
        }

        public void SetMaxWidth()
        {
            width1 = (uint)data.Longest;
            width2 = 1;
            bitsArea.Invalidate();
        }

        public void ChangeGridSize(uint gridSizeX, uint gridSizeY)
        {
            this.gridSizeX = gridSizeX;
            this.gridSizeY = gridSizeY;
            bitsArea.Invalidate();
        }

        public void SetDefaultGridSize()
        {
            gridSizeX = 8;
            gridSizeY = 0;
            bitsArea.Invalidate();
        }

        private void bitsArea_Paint(object sender, PaintEventArgs e)
        {
            // initilize
            SolidBrush backgroundBrush = new SolidBrush(BACKGROUND_COLOR);
            SolidBrush bitOneBrush = new SolidBrush(BIT_ONE_COLOR);
            SolidBrush bitZeroBrush = new SolidBrush(BIT_ZERO_COLOR);
            SolidBrush bitOneStartSegBrush = new SolidBrush(BIT_ONE_START_SEG_COLOR);
            SolidBrush bitZeroStartSegBrush = new SolidBrush(BIT_ZERO_START_SEG_COLOR);
            SolidBrush betweenBitsBrush = new SolidBrush(BETWEEN_BITS_COLOR);
            Pen gridPen = new Pen(GRID_COLOR);

            // draw background
            e.Graphics.FillRectangle(backgroundBrush, 0, 0, bitsArea.Width, bitsArea.Height);

            #region draw bits
            if (data == null)
                return;

            uint maxBitsInLine = width1 * width2;
            //int maxBitsInLine = bitsArea.Width / BIT_SIZE;
            uint maxLinesInView = (uint)bitsArea.Height / BIT_SIZE;
            uint lineInView = 0;
            uint bitInLine = 0;
            ulong segmentOffset = 0;
            ulong bitOffset = 0;
            while (lineInView < maxLinesInView)
            {
                if (bitOffset >= data[segmentOffset].Length)
                {
                    lineInView++;
                    bitInLine = 0;
                    bitOffset = 0;
                    segmentOffset++;
                }
                if (bitInLine >= maxBitsInLine)
                {
                    bitInLine = 0;
                    lineInView++;
                }
                if (segmentOffset >= data.Length)
                    break;
                //DrawBit(bitInLine * BIT_SIZE, lineInView * BIT_SIZE, BIT_SIZE, e.Graphics, parser.ParsedData[segmentOffset][bitOffset]);
                e.Graphics.FillRectangle(betweenBitsBrush, bitInLine * BIT_SIZE, lineInView * BIT_SIZE, BIT_SIZE, BIT_SIZE);

                // Choose relevant brush 
                SolidBrush choosenBrush;
                if (data[segmentOffset][bitOffset])
                {
                    if (bitOffset == 0)
                        choosenBrush = bitOneStartSegBrush;
                    else
                        choosenBrush = bitOneBrush;
                }
                else
                {
                    if (bitOffset == 0)
                        choosenBrush = bitZeroStartSegBrush;
                    else
                        choosenBrush = bitZeroBrush;
                }
                e.Graphics.FillRectangle(choosenBrush, bitInLine * BIT_SIZE + 1, lineInView * BIT_SIZE + 1, BIT_SIZE - 2, BIT_SIZE - 2);
                bitOffset++;
                bitInLine++;
            }
            #endregion

            #region draw grid
            if (gridSizeX > 0)
                for (int i = 0; i * gridSizeX * BIT_SIZE <= bitsArea.Width; i++)
                    e.Graphics.DrawLine(gridPen, new Point((int)(i * gridSizeX * BIT_SIZE), 0), new Point((int)(i * gridSizeX * BIT_SIZE), bitsArea.Height));
            if (gridSizeY > 0)
                for (int i = 0; i * gridSizeY * BIT_SIZE <= bitsArea.Height; i++)
                    e.Graphics.DrawLine(gridPen, new Point(0, (int)(i * gridSizeY * BIT_SIZE)), new Point(bitsArea.Width, (int)(i * gridSizeY * BIT_SIZE)));
            #endregion
        }

        private void bitsArea_Resize(object sender, EventArgs e)
        {
            bitsArea.Invalidate();
        }
    }
}
