using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Proyecto_Integrador
{
    public class BmpManager
    {
        public Bitmap bmpOrg, bmpAnalyse, bmpRoad, bmpTree;
        public Brush whiteBrsh, blackBrsh, orangeBrsh, purpleBrsh;
        public PictureBox pbImg;
        public Graphics gBmp;
        public Color c;

        public BmpManager()
        {
            bmpOrg = null;
            bmpAnalyse = null;
            bmpRoad = null;
            bmpTree = null;

            blackBrsh = new SolidBrush(Color.Black);
            whiteBrsh = new SolidBrush(Color.White);
            orangeBrsh = new SolidBrush(Color.Orange);
            purpleBrsh = new SolidBrush(Color.Purple);
        }

    }
}
