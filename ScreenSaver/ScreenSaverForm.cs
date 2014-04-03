using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ScreenSaver {
    //
    public partial class ScreenSaverForm : Form {
        //
        private Point mouseLocation;
        private bool previewMode = false;
        private Random rand = new Random ( );
        //
        protected int BASE_UNIT = 10;
        protected int GENERAL_PEN_SIZE = 1;
        protected int MAP_HEIGHT;
        protected int MAP_WIDTH;
        protected int GRID_WIDTH;
        protected int GRID_HEIGHT;
        protected int TIMER = 5;
        //
        protected static Color colorBackground = ColorTranslator.FromHtml ( "#09419E" );
        protected static Color colorLine = ColorTranslator.FromHtml ( "#1955AD" );
        protected static Color colorCell = ColorTranslator.FromHtml ( "#EFFFFF" );
        //
        protected SolidBrush brushCell;
        protected SolidBrush brushBackground;
        protected Boolean firstTime = true;
        //
        Grid theGrid;
        //
        protected Pen penLine;
        //
        public ScreenSaverForm ( ) {
            InitializeComponent ( );
        }

        public ScreenSaverForm ( Rectangle Bounds ) {
            InitializeComponent ( );
            this.Bounds = Bounds;
        }

        public ScreenSaverForm ( IntPtr PreviewWndHandle ) {
            InitializeComponent ( );
            previewMode = false;
        }
        //
        private void ScreenSaverForm_Load ( object sender, EventArgs e ) {
            Cursor.Hide ( );
            this.init ( );
            this.Refresh ( );
            this.SetStyle ( System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true );
            this.SetStyle ( System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true );
        }
        //
        private void init ( ) {
            //
            Graphics g = this.CreateGraphics ( );
            //
            this.MAP_HEIGHT = this.Height - ( 2 * BASE_UNIT );
            this.MAP_WIDTH = this.Width - ( 2 * BASE_UNIT );
            this.GRID_HEIGHT = this.MAP_HEIGHT / BASE_UNIT;
            this.GRID_WIDTH = this.MAP_WIDTH / BASE_UNIT;
            //
            //GRID_WIDTH = GRID_HEIGHT = 50;
            //
            theGrid = new Grid ( this.GRID_WIDTH, this.GRID_HEIGHT );
            //
            this.penLine = new Pen ( colorLine );
            this.penLine.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.brushCell = new SolidBrush ( colorCell );
            this.brushBackground = new SolidBrush ( colorBackground );
            //
            this.Paint += new PaintEventHandler ( this.paint );
            evolutionTimer.Interval = TIMER;
            evolutionTimer.Tick += new EventHandler ( evolutionTimer_Tick );
            evolutionTimer.Start ( );

        }

        public void paint ( object sender, PaintEventArgs e ) {
            if ( firstTime ) {
                //
                e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                //
                e.Graphics.Clear ( colorBackground );
                e.Graphics.DrawRectangle ( penLine, 10, 10, this.Width - 20, this.Height - 20 );
                this.drawGrid ( e.Graphics );
                this.refreshGrid ( e.Graphics );
                //firstTime = false;
            } else {
                //this.refreshGrid ( e.Graphics);
            }
        }
        //
        private void refreshGrid ( Graphics g ) {
            int x, y;
            for ( x = 0 ; x < theGrid.UniverseWidth ; x++ ) {
                for ( y = 0 ; y < theGrid.UniverseHeight ; y++ ) {
                    if ( theGrid.isAlive ( x, y ) == Cell.ALIVE ) {
                        this.fillRectangle ( x, y, g );
                    } else {
                        this.clearRectangle ( x, y, g );
                    }

                }
            }
        }
        //
        private void drawGrid ( Graphics g ) {
            //MessageBox.Show ( "" + theGrid.howManyAlive ( ) );
            int i, j;
            i = j = BASE_UNIT;
            //
            while ( i <= this.MAP_HEIGHT ) {
                //this.placeLine( BASE_UNIT + GENERAL_PEN_SIZE, i, MAP_SIZE + ( BASE_UNIT - GENERAL_PEN_SIZE ), i, this.penLine );
                g.DrawLine ( this.penLine, BASE_UNIT + GENERAL_PEN_SIZE, i, MAP_WIDTH + ( BASE_UNIT - GENERAL_PEN_SIZE ), i );
                i += BASE_UNIT;
            }
            while ( j <= this.MAP_WIDTH ) {
                //this.placeLine( j, BASE_UNIT + GENERAL_PEN_SIZE, j, MAP_SIZE + ( BASE_UNIT - GENERAL_PEN_SIZE), this.penLine );
                g.DrawLine ( this.penLine, j, BASE_UNIT + GENERAL_PEN_SIZE, j, MAP_HEIGHT + ( BASE_UNIT - GENERAL_PEN_SIZE ) );
                j += BASE_UNIT;
            }
            //
            /*for ( x = 0 ; x < theGrid.UniverseWidth ; x++ ) {
                for ( y = 0 ; y < theGrid.UniverseHeight ; y++ ) {
                    if ( theGrid.isAlive ( x, y ) == Cell.ALIVE ) {
                        this.fillRectangle ( x, y, g );
                    } else {
                        this.clearRectangle ( x, y, g );
                    }
                    
                }
            }*/
            //
            //this.fillRectangle ( 1, 6, g );
            //this.clearRectangle ( 1, 6, g );
        }
        //
        private void fillRectangle ( int x, int y, Graphics g, Color color ) {
            int posX, posY;
            SolidBrush brush = new SolidBrush ( color );
            //
            //
            posX = posY = BASE_UNIT + ( GENERAL_PEN_SIZE * 2 );
            //
            posX += x * BASE_UNIT;
            posY += y * BASE_UNIT;
            //
            g.FillRectangle ( brush, posX, posY, BASE_UNIT - ( 3 * GENERAL_PEN_SIZE ), BASE_UNIT - ( 3 * GENERAL_PEN_SIZE ) );
        }
        //
        private void fillRectangle ( int x, int y, Graphics g ) {
            int posX, posY;
            posX = posY = BASE_UNIT + ( GENERAL_PEN_SIZE * 2 );
            //
            posX += x * BASE_UNIT;
            posY += y * BASE_UNIT;
            //
            g.FillRectangle ( this.brushCell, posX, posY, BASE_UNIT - ( 3 * GENERAL_PEN_SIZE ), BASE_UNIT - ( 3 * GENERAL_PEN_SIZE ) );
        }
        //
        private void clearRectangle ( int x, int y, Graphics g ) {
            int posX, posY;
            posX = posY = BASE_UNIT + ( GENERAL_PEN_SIZE * 2 );
            //
            posX += x * BASE_UNIT;
            posY += y * BASE_UNIT;
            //
            g.FillRectangle ( this.brushBackground, posX, posY, BASE_UNIT - ( 3 * GENERAL_PEN_SIZE ), BASE_UNIT - ( 3 * GENERAL_PEN_SIZE ) );
        }
        //
        private void LoadSettings ( ) {
        }

        private void ScreenSaverForm_MouseMove ( object sender, MouseEventArgs e ) {
            if ( !previewMode ) {
                if ( !mouseLocation.IsEmpty ) {
                    // Terminate if mouse is moved a significant distance
                    if ( Math.Abs ( mouseLocation.X - e.X ) > 5 ||
                        Math.Abs ( mouseLocation.Y - e.Y ) > 5 )
                        Application.Exit ( );
                }

                // Update current mouse location
                mouseLocation = e.Location;
            }
        }

        private void ScreenSaverForm_KeyPress ( object sender, KeyPressEventArgs e ) {
            if ( !previewMode )
                Application.Exit ( );
        }

        private void ScreenSaverForm_MouseClick ( object sender, MouseEventArgs e ) {
            if ( !previewMode )
                Application.Exit ( );
        }

        private void evolutionTimer_Tick ( object sender, EventArgs e ) {
            evolutionTimer.Stop ( );
            theGrid.refreshGrid ( );
            theGrid.nextStep ( );
            this.Refresh ( );
            evolutionTimer.Start ( );
        }
    }
}
