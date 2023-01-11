using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;


namespace SchiffeVersenken
{

    public partial class Spielfeld : Form
    {
        /* 2D Array des Boards
         * 0 für nichts (blau)
         * 1 für schiff (grau)
         * 2 für getroffen (grün)
         * 3 für daneben (rot)
         */
       
        int y, x = 0;
        int spielerAnzahl;
        int activePlayer = 0;
        int schiffAnzahl;
        int aktuelleRunde = 0;
        int activeSchiffanzahl = 0;
        int runden= 0;
        int modus = 0;
        Color[] playerFarbArray;
        TableLayoutPanel spielfeld;
        Spieler[] spielerArray;
        int spielerImSpiel = 0;
        int[] score;
        SoundPlayer attack = new System.Media.SoundPlayer(SchiffeVersenken.Properties.Resources.startRakete);
        SoundPlayer treffer = new System.Media.SoundPlayer(SchiffeVersenken.Properties.Resources.trefferSchiff);
        SoundPlayer miss = new System.Media.SoundPlayer(SchiffeVersenken.Properties.Resources.trefferWasser);
        SoundPlayer destroyed = new System.Media.SoundPlayer(SchiffeVersenken.Properties.Resources.Schiffversenkt);
        SoundPlayer sieg = new System.Media.SoundPlayer(SchiffeVersenken.Properties.Resources.gewinnerFenster);
        int[,] activeBoard;
        Button[,] buttonsBoard;
        TaskCompletionSource<bool> fertigTask = null;
        TaskCompletionSource<bool> angriffTask = null;

        public Spielfeld(int spielerAnzahl, int schiffAnzahl, int feldzeile, int feldspalte, Color[] playerFarbArray, int[] schiffAnzahlArray, int modus, int runden)
        {
            InitializeComponent();
            this.schiffAnzahl = schiffAnzahl;
            this.spielerAnzahl = spielerAnzahl;
            this.playerFarbArray = playerFarbArray;
            this.spielerArray = new Spieler[spielerAnzahl];
            this.modus = modus;
            this.runden = runden;
            score = new int[spielerAnzahl];
            
            buttonsBoard = new Button[feldzeile, feldspalte];

            // Init der Spielerboards mit Dimensionen des Spielfelds
            //spieler1board = new int[feldzeile, feldspalte];
            //spieler2board = new int[feldzeile, feldspalte];
            //if (spielerAnzahl >= 3)
            //    spieler3board = new int[feldzeile, feldspalte];
            //if (spielerAnzahl == 4)
            //    spieler4board = new int[feldzeile, feldspalte];

            for (int i = 0; i < spielerAnzahl; i++) score[i] = 0;

            for (int i = 0; i < spielerAnzahl; i++)
            {
                spielerArray[i] = new Spieler(i, playerFarbArray[i], new int[feldzeile, feldspalte], score[i]);
            }



            spielfeld = new TableLayoutPanel();
            
            spielfeld.RowCount = feldzeile + 1;
            spielfeld.ColumnCount = feldspalte + 1;
            spielfeld.Dock = DockStyle.Fill;
            spielfeld.RowStyles.Clear();
            spielfeld.ColumnStyles.Clear();

            this.mainGrid.Controls.Add(spielfeld, 0, 0);


            float horizontalProzent = 100 / feldspalte;
            float verticalProzent = 100 / feldzeile;

            //Beschreibung der Achsen

            char letter = 'A';
            spielfeld.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));
            for (int i = 1; i < spielfeld.ColumnCount; i++) {
                Label label = new Label();
                label.Text = letter++.ToString();
                label.TextAlign = ContentAlignment.MiddleCenter;
                spielfeld.Controls.Add(label, i, 0);
            }

            int numb = 1;
            spielfeld.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            for (int i = 1; i < spielfeld.RowCount; i++)
            {
                Label label = new Label();
                label.Text = numb++.ToString();
                label.Dock = DockStyle.Fill;
                label.TextAlign = ContentAlignment.MiddleCenter;
                spielfeld.Controls.Add(label, 0, i);
            }

            for (int i = 1; i < spielfeld.RowCount; i++)
            {
                spielfeld.RowStyles.Add(new RowStyle(SizeType.Percent, verticalProzent));
            }

            for (int i = 1; i < spielfeld.ColumnCount; i++)
            {
                spielfeld.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, horizontalProzent));
            }
            
            //änderung der namen und inhalt der spielfeld Buttons
            
            letter = 'A';
            for (int i = 1; i <= feldspalte; i++)
            {

                for (int j = 1; j <= feldzeile; j++)
                {
                    Button btn1 = new Button();
                    btn1.Dock = DockStyle.Fill;
                    spielfeld.Controls.Add(btn1, i, j);
                    btn1.Text = (j).ToString() + letter;
                    btn1.Name = (j).ToString() + letter;
                    buttonsBoard[j - 1, i - 1] = btn1;
                    btn1.Enabled = false;
                    btn1.Click += btn_Clicked;
                    btn1.BackColor = Color.LightBlue;   
                }
                letter++;
            }


            //Switchcasr um hintergrund des spieleravatars und scores zu setzen
            switch (spielerAnzahl)
            {
                case 4:
                    groupBoxspieler4.BackColor = spielerArray[3].getFarbe();
                    groupBoxspieler4.Enabled = true;
                    groupBoxScore4.BackColor = spielerArray[3].getFarbe();
                    groupBoxScore4.Enabled = true;
                    goto case 3;
                case 3:
                    groupBoxspieler3.BackColor = spielerArray[2].getFarbe();
                    groupBoxspieler3.Enabled = true;
                    groupBoxScore3.BackColor = spielerArray[2].getFarbe();
                    groupBoxScore3.Enabled = true;
                    goto case 2;
                case 2:
                    groupBoxspieler1.BackColor = spielerArray[0].getFarbe();
                    groupBoxspieler2.BackColor = spielerArray[1].getFarbe();
                    groupBoxScore1.BackColor = spielerArray[0].getFarbe();
                    groupBoxScore2.BackColor = spielerArray[1].getFarbe();
                    break;
                default:
                    MessageBox.Show("Bei dem erstellen des Spielfeldinterfac gab es ein fehler!", "Fehler Spielfeldinterface");
                    break;
            }

            //deaktivieren der Schiffe anhant der anzahl ausgewählter schiffe
            switch (schiffAnzahl)
            {
                case 5:
                    groupBoxSchiff5.Enabled = true;
                    schifflaenge5.Text = schiffAnzahlArray[4].ToString();
                    goto case 4;
                case 4:
                    groupBoxSchiff4.Enabled = true;
                    schifflaenge4.Text = schiffAnzahlArray[3].ToString();
                    goto case 3;
                case 3:
                    groupBoxSchiff3.Enabled = true;
                    schifflaenge3.Text = schiffAnzahlArray[2].ToString();
                    goto case 2;
                case 2:
                    groupBoxSchiff2.Enabled = true;
                    schifflaenge2.Text = schiffAnzahlArray[1].ToString();
                    goto case 1;
                case 1:
                    groupBoxSchiff1.Enabled = true;
                    schifflaenge1.Text = schiffAnzahlArray[0].ToString();
                    break;
                default:
                    MessageBox.Show("Bei der Anzahl der Schiffe gab es einen Fehler!", "Fehler Schiffanzahl");
                    break;
            }

            activeBoard = spielerArray[activePlayer].getSpielerBoard();
            activePlayerChanged(activePlayer);
        }
       
        /// <summary>
        /// eventhandler der generierten Buttons
        /// </summary>
        private void btn_Clicked(Object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            //MessageBox.Show(string.Format("Dies ist die koordinate {0} ", btn.Name), "Koordinaten" );
            string s = btn.Name;
            char[] position = s.ToCharArray();
            if (position.GetLength(0) == 2)
            {
                x = ((position[0] - '0'));
                y = ((position[1] - '@'));
            }
            else 
            {
                x = 10;
                y = ((position[2] - '@'));
            }
            //MessageBox.Show(string.Format("Dies ist die koordinate {0} {1} ", y,x), "Koordinaten");
            fertigTask.TrySetResult(true);
        }

        private void Spielfeld_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private async void placeschiff_Click(object sender, EventArgs e)
        {
            refreshButtons(true);

            Button btn = (Button)sender;
            string s = btn.Name;
            
            
            int length = 0;
            int schiffNr = 0;
            if (s == "placeschiff1") {
                length = Int32.Parse(schifflaenge1.Text);
                schiffNr = 1;
            }
            else if (s == "placeschiff2") {
                length = Int32.Parse(schifflaenge2.Text);
                schiffNr = 2;
            }
            else if (s == "placeschiff3") {
                length = Int32.Parse(schifflaenge3.Text);
                schiffNr = 3;
            }
            else if (s == "placeschiff4") {
                length = Int32.Parse(schifflaenge4.Text);
                schiffNr = 4;
            }
            else if (s == "placeschiff5") {
                length = Int32.Parse(schifflaenge5.Text);
                schiffNr = 5;
            }


            if (btn.Text == "neu platzieren")
            {
                for (int i = 0; i < spielerArray[activePlayer].getSpielerBoard().GetLength(0); i++)
                {
                    for (int j = 0; j < spielerArray[activePlayer].getSpielerBoard().GetLength(1); j++)
                    {
                        if (spielerArray[activePlayer].getSpielerBoardValue(i,j) == schiffNr) spielerArray[activePlayer].setSpielerBoardValue(i,j,0);
                    }
                }
                activeSchiffanzahl--;
                printBoard(true);
            }
            
            //Schifflänge ist 1, daher gesonderter Fall
            if (length == 1)
            {
                fertigTask = new TaskCompletionSource<bool>();

                MessageBox.Show("Wähle eine Position aus.", "Schiff Platzieren");
                await fertigTask.Task;
                if (spielerArray[activePlayer].getSpielerBoardValue(x - 1, y - 1) == 0) {
                    spielerArray[activePlayer].setSpielerBoardValue(x - 1, y - 1, schiffNr);
                    btn.Text = "neu platzieren";
                    activeSchiffanzahl++;
                }
                else
                {
                    MessageBox.Show("Hier ist schon ein Schiff!\nEs kann hier nicht platziert werden!", "Schiff vorhanden");
                }
            }
            //Schifflänge ist größer als 1
            else
            {
                int startx, starty = 0;
                fertigTask = new TaskCompletionSource<bool>();

                MessageBox.Show("Wähle die erste Position aus.", "Schiff Platzieren");
                await fertigTask.Task;

                startx = x;
                starty = y;
                fertigTask = new TaskCompletionSource<bool>();
                MessageBox.Show("Wähle die zweite Position aus.", "Schiff Platzieren");
                await fertigTask.Task;

                bool clear = true;

                //Fall: Schiff nach oben setzen
                if ((x + (length - 1) == startx) && (starty == y))
                {
                    for (int i = startx; i >= x; i--)
                    {
                        if (spielerArray[activePlayer].getSpielerBoardValue(i - 1, y - 1) > 0) clear = false;
                    }

                    if (clear)
                    {
                        for (int i = startx; i >= x; i--)
                        {
                            spielerArray[activePlayer].setSpielerBoardValue(i - 1, y - 1, schiffNr);
                        }
                    } 
                    else
                    {
                        MessageBox.Show("Hier ist schon ein Schiff!\nEs kann hier nicht platziert werden!", "Schiff vorhanden");
                    }
                }

                //Fall: Schiff nach links setzen
                else if ((y + (length - 1) == starty) && (startx == x))
                {
                    for (int i = starty; i >= y; i--)
                    {
                        if (spielerArray[activePlayer].getSpielerBoardValue(x - 1, i - 1) > 0) clear = false;
                    }

                    if (clear)
                    {
                        for (int i = starty; i >= y; i--)
                        {
                            spielerArray[activePlayer].setSpielerBoardValue(x - 1, i - 1, schiffNr);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hier ist schon ein Schiff!\nEs kann hier nicht platziert werden!", "Schiff vorhanden");
                    }
                }

                //Fall: Schiff nach unten setzen
                else if ((x - (length - 1) == startx) && (starty == y))
                {
                    for (int i = startx; i <= x; i++)
                    {
                        if (spielerArray[activePlayer].getSpielerBoardValue(i - 1, y - 1) > 0) clear = false;
                    }

                    if (clear)
                    {
                        for (int i = startx; i <= x; i++)
                        {
                            spielerArray[activePlayer].setSpielerBoardValue(i - 1, y - 1, schiffNr);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hier ist schon ein Schiff!\nEs kann hier nicht platziert werden!!", "Schiff vorhanden");
                    }
                }

                //Fall: Schiff nach rechts setzen
                else if ((y - (length - 1) == starty) && (startx == x))
                {
                    for (int i = starty; i <= y; i++)
                    {
                        if (spielerArray[activePlayer].getSpielerBoardValue(x - 1, i - 1) > 0) clear = false;
                    }

                    if (clear)
                    {
                        for (int i = starty; i <= y; i++)
                        {
                            spielerArray[activePlayer].setSpielerBoardValue(x - 1, i - 1, schiffNr);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hier ist schon ein Schiff!\nEs kann hier nicht platziert werden!", "Schiff vorhanden");
                    }
                }
                else {
                    
                    MessageBox.Show("Das Schiff wurde falsch platziert!", "Schiff Platzieren");
                    return;
                }

                if (clear)
                {
                    btn.Text = "neu platzieren";
                    activeSchiffanzahl++;
                }
            }

            printBoard(true);

            if (activeSchiffanzahl == schiffAnzahl)
            {
                if (activePlayer == spielerAnzahl-1)
                {
                    DialogResult result = MessageBox.Show("Spiel starten?", "Fertig!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {
                        gameLoop();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Nächster Spieler?", "Fertig?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {
                        activePlayerChanged(activePlayer++);
                        activeSchiffanzahl = 0;
                    }
                }
            }
            deaktivereAlleButtons();

            printBoard(true);
        }

        void printBoard(bool schiffeSichtbar) {

            // Im ganzen Board werden die Farben neu gesetzt
            for (int i = 0; i < spielerArray[activePlayer].getSpielerBoard().GetLength(0); i++)
            {
                for (int j = 0; j < spielerArray[activePlayer].getSpielerBoard().GetLength(1); j++)
                {
                    /* 0 für nichts (blau)
                     * 1 = Schiff 1 (braun)
                     * 2 = Schiff 2 (braun)
                     * 3 = Schiff 3 (braun)
                     * 4 = Schiff 4 (braun)
                     * 5 = Schiff 5 (braun)
                     * 6 für getroffen (grün)
                     * 7 für daneben (rot)
                     */

                    switch (spielerArray[activePlayer].getSpielerBoardValue(i, j))
                    {
                        case 0:
                            buttonsBoard[i, j].BackColor = Color.LightBlue;
                            break;
                        case 1:
                            if (schiffeSichtbar) buttonsBoard[i, j].BackColor = Color.SaddleBrown;
                            else buttonsBoard[i, j].BackColor = Color.LightBlue;
                            break;
                        case 2:
                            if (schiffeSichtbar) buttonsBoard[i, j].BackColor = Color.MediumTurquoise;
                            else buttonsBoard[i, j].BackColor = Color.LightBlue;
                            break;
                        case 3:
                            if (schiffeSichtbar) buttonsBoard[i, j].BackColor = Color.Thistle;
                            else buttonsBoard[i, j].BackColor = Color.LightBlue;
                            break;
                        case 4:
                            if (schiffeSichtbar) buttonsBoard[i, j].BackColor = Color.Salmon;
                            else buttonsBoard[i, j].BackColor = Color.LightBlue;
                            break;
                        case 5:
                            if (schiffeSichtbar) buttonsBoard[i, j].BackColor = Color.NavajoWhite;
                            else buttonsBoard[i, j].BackColor = Color.LightBlue;
                            break;
                        case 6:
                            buttonsBoard[i, j].BackColor = Color.Green;
                            break;
                        case 7:
                            buttonsBoard[i, j].BackColor = Color.Red;
                            break;
                    }
                }
            }

        }

        void activePlayerChanged(int playerNr) {
            placeschiff1.Text = "platzieren";
            placeschiff2.Text = "platzieren";
            placeschiff3.Text = "platzieren";
            placeschiff4.Text = "platzieren";
            placeschiff5.Text = "platzieren";

            // Textlabel des Hauptfensters ändern
            this.Text = "Spielfeld - Spieler: " + (activePlayer + 1).ToString();
            // Hintergrundfarbe des aktvien Spielers einstellen
            spielfeld.BackColor = spielerArray[activePlayer].getFarbe();
        }

        async void angreifenClick(object sender, EventArgs e) {

            Button btn = (Button)sender;
            int lastPlayer = activePlayer;

            switch (btn.Name)
            {
                case "spielerfeld1":
                    activePlayer = 0;
                    break;
                case "spielerfeld2":
                    activePlayer = 1;
                    break;
                case "spielerfeld3":
                    activePlayer = 2;
                    break;
                case "spielerfeld4":
                    activePlayer = 3;
                    break;
                default:
                    MessageBox.Show("Fehler beim Aufrufen des angegrieffen Spielfeld!", "Fehler Auswahl Spielfeld");
                    break;
            }

            activePlayerChanged(activePlayer);
            printBoard(false);
            refreshButtons(false);
            attack.Play();
            fertigTask = new TaskCompletionSource<bool>();

            await fertigTask.Task;

            int feld = spielerArray[activePlayer].getSpielerBoardValue(x - 1, y - 1);

            if ((feld >= 1) && (feld <= 5))
            {
                // HIT!!!
                treffer.Play();
                spielerArray[activePlayer].setSpielerBoardValue(x - 1, y - 1, 6);
                spielerArray[lastPlayer].addScore(6-feld);
                refreshscore();
                printBoard(false);

                if (!spielerArray[activePlayer].hatSchiffNr(feld))
                {
                    destroyed.Play();
                    MessageBox.Show("Schiff ist komplett zerstört");
                }

                if (!spielerArray[activePlayer].hatSchiffe())
                {
                    MessageBox.Show("Alle Schiffe wurden zerstört!","Spieler: " + (activePlayer+1) + " ausgeschieden!");
                    spielerImSpiel--;
                }
            }
            else {
                // WASSERTREFFER!!!
                spielerArray[activePlayer].setSpielerBoardValue(x - 1, y - 1, 7);
                spielerArray[lastPlayer].addScore(0);
                printBoard(false);
                miss.Play();
            }
            
            deaktivereAlleButtons();
            refreshscore();
            activePlayer = lastPlayer;
            angriffTask.TrySetResult(true);
        }

        void refreshButtons(bool schiffPlatzieren)
        {
            int[,] spielerBoard = spielerArray[activePlayer].getSpielerBoard();
            for (int i = 0; i < spielerBoard.GetLength(0); i++)
            {
                for (int j = 0; j < spielerBoard.GetLength(1); j++)
                {
                    if (schiffPlatzieren)
                    {
                        if (spielerBoard[i, j] == 0) buttonsBoard[i, j].Enabled = true;
                        else buttonsBoard[i, j].Enabled = false;
                    } 
                    else
                    {
                        if (spielerBoard[i, j] > 5) buttonsBoard[i, j].Enabled = false;
                        else buttonsBoard[i, j].Enabled = true;
                    }
                }
            }
        }

        void deaktivereAlleButtons()
        {
            foreach (Button btn in buttonsBoard) btn.Enabled = false;
        }
        

        void auswahlButtonsSetzen(int i)
        { 
            spielerfeld1.Enabled = true;
            spielerfeld2.Enabled = true;
            spielerfeld3.Enabled = true;
            spielerfeld4.Enabled = true;
            switch (i)
            {
                case 0:
                    spielerfeld1.Enabled = false;
                    break;
                case 1:
                    spielerfeld2.Enabled = false;
                    break;
                case 2:
                    spielerfeld3.Enabled = false;
                    break;
                case 3:
                    spielerfeld4.Enabled = false;
                    break;
            }
        }
        void refreshscore()
        {
            
            Score1.Text = spielerArray[0].getScore().ToString();
            Score2.Text = spielerArray[1].getScore().ToString();
            if (spielerAnzahl >= 3)
            { Score3.Text = spielerArray[2].getScore().ToString(); }
            if (spielerAnzahl == 4)
            { Score4.Text = spielerArray[3].getScore().ToString(); }
            
        }
        async void gameLoop()
        {
            MessageBox.Show("Spiel startet!");

            placeschiff1.Visible = false;
            placeschiff2.Visible = false;
            placeschiff3.Visible = false;
            placeschiff4.Visible = false;
            placeschiff5.Visible = false;

            spielerImSpiel = spielerAnzahl;
            activePlayer = 0;

            int letzterSpieler;
            int gewinnerscore = spielerArray[0].getScore();
            int gewinnerspieler = 0;
            List<int> gewinner = new List<int>();

            spielerfeld1.Enabled = false;
            switch (modus) 
            {
                case 1:
                    goto Normal;
                    break;
                case 2:
                    goto Einerraus;
                    break;
                case 3:
                    goto Rundenaus;
                    break;
                default:
                    MessageBox.Show("Es gab Probleme beim auswählen des modus", "Fehler modus");
                    break;
            }
            //normal mode
            Normal:
            while (true)
            {
                activePlayerChanged(activePlayer);
                printBoard(true);
                auswahlButtonsSetzen(activePlayer);

                
                angriffTask = new TaskCompletionSource<bool>();

                await angriffTask.Task;

                letzterSpieler = activePlayer;

                do {
                    activePlayer++;
                    if (activePlayer > spielerAnzahl - 1) {
                        activePlayer = 0;
                        rundenzahlStripbar.Text = (Int32.Parse(rundenzahlStripbar.Text)+1).ToString();
                    }
                } while (spielerArray[activePlayer].istEliminiert());

                if (letzterSpieler == activePlayer) break;
            }
            sieg.Play();
            MessageBox.Show("Spieler " + (activePlayer + 1) + " hat gewonnen!", "Spiel beendet!");
            Application.Exit();

        Einerraus:
            while (spielerImSpiel == spielerAnzahl)
            {
                activePlayerChanged(activePlayer);
                printBoard(true);
                refreshscore();
                auswahlButtonsSetzen(activePlayer);


                angriffTask = new TaskCompletionSource<bool>();

                await angriffTask.Task;

                do
                {
                    activePlayer++;
                    if (activePlayer > spielerAnzahl - 1)
                    {
                        activePlayer = 0;
                        rundenzahlStripbar.Text = (Int32.Parse(rundenzahlStripbar.Text) + 1).ToString();
                    }
                } while (spielerArray[activePlayer].istEliminiert());
  
            }
            refreshscore();
            gewinnerscore = spielerArray[0].getScore();
            gewinnerspieler = 0;

            for(int i = 0; i < spielerAnzahl;i++)
            {
                if(gewinnerscore < spielerArray[i].getScore()) 
                {
                    gewinnerspieler = i;
                    gewinnerscore = spielerArray[i].getScore();
                }
            }
            for (int i = 0; i < spielerAnzahl; i++)
            {
                if (gewinnerscore == spielerArray[i].getScore())
                {
                    gewinner.Add(i + 1);
                }
            }
            activePlayer = gewinnerspieler;
           
            if (gewinner.Count > 1) 
            {
                string ergebnis = string.Join(" , ", gewinner);
                sieg.Play();
                MessageBox.Show("Spieler " + ergebnis + " haben gewonnen!", "Spiel beendet!");
            }
            else {
                sieg.Play();
                MessageBox.Show("Spieler " + (activePlayer + 1) + " hat gewonnen!", "Spiel beendet!"); }
            Application.Exit();

        Rundenaus:
            while (aktuelleRunde < runden)
            {
                activePlayerChanged(activePlayer);
                printBoard(true);
                refreshscore();
                auswahlButtonsSetzen(activePlayer);


                angriffTask = new TaskCompletionSource<bool>();

                await angriffTask.Task;

                letzterSpieler = activePlayer;

                do
                {
                    activePlayer++;
                    if (activePlayer > spielerAnzahl - 1)
                    {
                        activePlayer = 0;
                        aktuelleRunde++;
                        rundenzahlStripbar.Text = (Int32.Parse(rundenzahlStripbar.Text) + 1).ToString();
                    }
                } while (spielerArray[activePlayer].istEliminiert());

                if (letzterSpieler == activePlayer) break;
            }
            refreshscore();
            gewinnerscore = spielerArray[0].getScore();
            gewinnerspieler = 0;
            for (int i = 0; i < spielerAnzahl; i++)
            {
                if (gewinnerscore < spielerArray[i].getScore())
                {
                    gewinnerspieler = i;
                    gewinnerscore = spielerArray[i].getScore();
                }
            }
            for (int i = 0; i < spielerAnzahl; i++)
            {
                if (gewinnerscore == spielerArray[i].getScore())
                {
                    gewinner.Add(i + 1);
                }
            }
            activePlayer = gewinnerspieler;
            if (gewinner.Count > 1)
            {
                string ergebnis = string.Join(" , ", gewinner);
                sieg.Play();
                MessageBox.Show("Spieler " + ergebnis + " haben gewonnen!", "Spiel beendet!");
            }
            else {
                sieg.Play();
                MessageBox.Show("Spieler " + (activePlayer + 1) + " hat gewonnen!", "Spiel beendet!"); }
            Application.Exit();


         
        }
    }
}