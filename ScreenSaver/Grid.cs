using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScreenSaver {
    class Grid {
        //
        protected int universeWidth;
        public int UniverseWidth {
            get { return universeWidth; }
            set { universeWidth = value; }
        }
        protected int universeHeight;
        public int UniverseHeight {
            get { return universeHeight; }
            set { universeHeight = value; }
        }
        protected int iteration;
        protected Cell[,] theGrid;
        public Cell[,] TheGrid {
            get { return theGrid; }
            set { theGrid = value; }
        }
        protected Random randomValue;
        public Random RandomValue {
            get { return randomValue; }
        }
        //
        public Grid ( int width, int height ) {
            int i, j;
            //
            this.universeWidth = width;
            this.universeHeight = height;
            this.iteration = 0;
            this.randomValue = new Random ( );
            theGrid = new Cell[width, height];
            //
            for ( i = 0 ; i < width ; i++ ) {
                for ( j = 0 ; j < height ; j++ ) {
                    theGrid[i, j] = new Cell ( i, j, Cell.RANDOM, this );
                }
            }
        }
        //
        public int isAlive ( int x, int y ) {
            if ( x >= 0 && x < this.universeWidth && y >= 0 && y < this.universeHeight ) {
                return theGrid[x, y].State;
            }
            return 0;
        }
        //
        public void refreshGrid ( ) {
            int i, j;
            for ( i = 0 ; i < this.universeWidth ; i++ ) {
                for ( j = 0 ; j < this.universeHeight ; j++ ) {
                    theGrid[i, j].recalculateNeighbor ( );
                }
            }
        }
        //
        public void nextStep ( ) {
            int i, j;
            for ( i = 0 ; i < this.universeWidth ; i++ ) {
                for ( j = 0 ; j < this.universeHeight ; j++ ) {
                    theGrid[i, j].State = theGrid[i, j].NextState;
                    theGrid[i, j].NextState = Cell.DEAD;
                }
            }
            this.iteration++;
            this.refreshGrid ( );
        }
        //
        public int howManyAlive ( ) {
            int i, j, res = 0;
            for ( i = 0 ; i < this.universeWidth ; i++ ) {
                for ( j = 0 ; j < this.universeHeight ; j++ ) {
                    res += theGrid[i, j].State;
                }
            }
            return res;
        }
    }
}
