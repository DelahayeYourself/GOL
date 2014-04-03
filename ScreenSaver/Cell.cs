using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScreenSaver {
    class Cell {
        //
        private int x;
        public int X {
            get { return x; }
            set { x = value; }
        }
        private int y;
        public int Y {
            get { return y; }
            set { y = value; }
        }
        private int state;
        public int State {
            get { return state; }
            set { state = value; }
        }
        private int nextState;
        public int NextState {
            get { return nextState; }
            set { nextState = value; }
        }
        private int nbNeighbor;
        public int NbNeighbor {
            get { return nbNeighbor; }
            set { nbNeighbor = value; }
        }
        private Grid universe;
        public Grid Universe {
            get { return universe; }
            set { universe = value; }
        }
        //
        public static int ALIVE = 1;
        public static int DEAD = 0;
        public static int RANDOM = 42;
        public static int CONWAY = 1;
        public static int HIGHLIFE = 2;
        public static int USER_PREFERENCE = 3;
        public static int USED_ALGORITHM = CONWAY;
        //
        public static int[] ARRAY_SURVIVE = new int[] { 2, 3, 4, 5 };
        public static int[] ARRAY_BORN = new int[] { 3, 6 };
        //
        public static List<int> LIST_SURVIVE = new List<int> ( ARRAY_SURVIVE );
        public static List<int> LIST_BORN = new List<int> ( ARRAY_BORN );
        //
        public Cell ( int x, int y, int state, Grid grid ) {
            this.x = x;
            this.y = y;
            if ( state == Cell.RANDOM ) {
                if ( ( grid.RandomValue.Next ( 0, 100 ) % 2 == 0 ) ) {
                    this.state = Cell.ALIVE;
                } else {
                    this.state = Cell.DEAD;
                }
            } else {
                this.state = state;
            }
            this.universe = grid;
            this.nbNeighbor = 0;
        }
        //
        public void recalculateNeighbor ( ) {
            int number = 0;
            number += universe.isAlive ( this.x - 1, this.y - 1 );
            number += universe.isAlive ( this.x - 1, this.y );
            number += universe.isAlive ( this.x - 1, this.y + 1 );
            number += universe.isAlive ( this.x, this.y - 1 );
            number += universe.isAlive ( this.x, this.y + 1 );
            number += universe.isAlive ( this.x + 1, this.y - 1 );
            number += universe.isAlive ( this.x + 1, this.y );
            number += universe.isAlive ( this.x + 1, this.y + 1 );
            //
            this.nbNeighbor = number;
            this.nextState = Cell.DEAD;
            if ( USED_ALGORITHM == Cell.CONWAY ) {
                if ( ( this.state == Cell.ALIVE && ( this.nbNeighbor == 3 || this.nbNeighbor == 2 ) ) || ( this.state == Cell.DEAD && this.nbNeighbor == 3 ) ) {
                    this.nextState = Cell.ALIVE;
                }
            }
            if ( USED_ALGORITHM == Cell.HIGHLIFE ) {
                if ( ( this.state == Cell.ALIVE && ( this.nbNeighbor == 3 || this.nbNeighbor == 2 ) ) || ( this.state == Cell.DEAD && ( this.nbNeighbor == 3 || this.nbNeighbor == 6 ) ) ) {
                    this.nextState = Cell.ALIVE;
                }
            }
            if ( USED_ALGORITHM == Cell.USER_PREFERENCE ) {
                if ( ( this.state == Cell.ALIVE && ( LIST_SURVIVE.Contains ( this.nbNeighbor ) ) ) || ( this.state == Cell.DEAD && ( LIST_BORN.Contains ( this.nbNeighbor ) ) ) ) {
                    this.nextState = Cell.ALIVE;
                }
            }
        }
    }
}
