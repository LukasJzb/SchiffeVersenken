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
        Color farbe;
        int[,] spielerBoard;
        bool eliminiert = false;
        int score;

        /// <summary>
        /// Allgemeine Spielerklasse mit Farbe, Spielerboard und Score
        /// </summary>
        /// <param name="farbe"></param>
        /// <param name="spielerBoard"></param>
        /// <param name="score"></param>
        public Spieler(Color farbe, int[,] spielerBoard, int score)
        {
            this.farbe = farbe;
            this.spielerBoard = spielerBoard;


            // Spielerboard mit 0 füllen
            for (int i = 0; i < spielerBoard.GetLength(0); i++)
            {
                for (int j = 0; j < spielerBoard.GetLength(1); j++)
                {
                    spielerBoard[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Gibt den aktuellen Score des Spielers zurück
        /// </summary>
        /// <returns>Integer Score</returns>
        public int getScore()
        {
            return score;
        }

        /// <summary>
        /// Summiert den übergebenen Score dem Aktuellen dazu
        /// </summary>
        /// <param name="länge"></param>
        public void addScore(int länge)
        {
            score = score + länge;
        }

        /// <summary>
        /// Gibt die Farbe des Spielers zurück
        /// </summary>
        /// <returns>Color Farbe</returns>
        public Color getFarbe()
        {
            return farbe;
        }

        /// <summary>
        /// Gibt das Spielerboard des Spielers als 2D-Array zurück
        /// </summary>
        /// <returns>int[,] spielerboard</returns>
        public int[,] getSpielerBoard()
        {
            return spielerBoard;
        }

        /// <summary>
        /// Setzt das komplette Board was übergeben wurde als Spielerboard
        /// </summary>
        /// <param name="newSpielerBoard"></param>
        public void setSpielerBoard(int[,] newSpielerBoard)
        {
            spielerBoard = newSpielerBoard;
        }

        /// <summary>
        /// Gibt den Wert des Boards an der Stelle x, y zurück
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>int boardvalue</returns>
        public int getSpielerBoardValue(int x, int y)
        {
            return spielerBoard[x, y];
        }

        /// <summary>
        /// Setzt den Wert des Boards an der Stelle x, y fest
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void setSpielerBoardValue(int x, int y, int value) {
            spielerBoard[x, y] = value;
        }

        /// <summary>
        /// Gibt einen Bool-Wert zurück, true wenn SchiffNr vorhanden. False wenn Schiff zerstört
        /// </summary>
        /// <param name="schiffNr"></param>
        /// <returns>bool schiffVorhanden</returns>
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
        /// <returns>bool minEinSchiffVorhanden</returns>
        public bool hatSchiffe() {
            foreach (int feld in spielerBoard) {
                if ((feld >= 1) && (feld <= 5)) return true;
            }
            eliminiert = true;
            return false;
        }

        /// <summary>
        /// Gibt einen Bool-Wert zurück ob, alle Schiffe zerstört sind
        /// </summary>
        /// <returns>bool eliminiert</returns>
        public bool istEliminiert()
        {
            return eliminiert;
        }
    }
}
