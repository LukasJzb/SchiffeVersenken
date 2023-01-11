using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchiffeVersenken
{
    public class Spieler
    {
        int spielerid;
        Color farbe;
        int[,] spielerBoard;
        bool eliminated = false;

        public Spieler(int spielerid, Color farbe, int[,] spielerBoard)
        {
            this.spielerid = spielerid;
            this.farbe = farbe;
            this.spielerBoard = spielerBoard;



            for (int i = 0; i < spielerBoard.GetLength(1); i++)
            {
                for (int j = 0; j < spielerBoard.GetLength(0); j++)
                {
                    spielerBoard[j, i] = 0;
                }
            }
        }

        public Color getFarbe()
        {
            return farbe;
        }

        public int getSpielerID()
        {
            return spielerid;
        }

        public int[,] getSpielerBoard()
        {
            return spielerBoard;
        }

        public void setSpielerBoard(int[,] newSpielerBoard)
        {
            spielerBoard = newSpielerBoard;
        }

        public int getSpielerBoardValue(int x, int y)
        {
            return spielerBoard[x, y];
        }

        public void setSpielerBoardValue(int x, int y, int value) {
            spielerBoard[x, y] = value;
        }

        public bool hatSchiffNr(int schiffNr) {
            foreach (int feld in spielerBoard)
            {
                if (feld == schiffNr) return true;
            }
            return false;
        }

        /// <summary>
        /// Funktion um zu testen ob Schiffe vorhanden sind.
        /// </summary>
        /// <returns>Ob überhaupt noch Schiffe da sind (true/false)</returns>
        public bool hatSchiffe() {
            foreach (int feld in spielerBoard) {
                if ((feld >= 1) && (feld <= 5)) return true;
            }
            eliminated = true;
            return false;
        }

        public bool getStatus()
        {
            return eliminated;
        }
    }
}
