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
        int y, x = 0;
        int spielerAnzahl;
        int aktiverSpieler = 0;
        int schiffAnzahl;
        int aktuelleRunde = 0;
        int aktiveSchiffanzahl = 0;
        int runden= 0;
        int modus = 0;
        Color[] spielerFarbArray;
        TableLayoutPanel spielfeld;
        Spieler[] spielerArray;
        int spielerImSpiel = 0;
        int[] score;
        Button[] angriffButtons;

        //Sound-Import
        SoundPlayer attacke = new System.Media.SoundPlayer(SchiffeVersenken.Properties.Resources.startRakete);
        SoundPlayer treffer = new System.Media.SoundPlayer(SchiffeVersenken.Properties.Resources.trefferSchiff);
        SoundPlayer daneben = new System.Media.SoundPlayer(SchiffeVersenken.Properties.Resources.trefferWasser);
        SoundPlayer zerstört = new System.Media.SoundPlayer(SchiffeVersenken.Properties.Resources.Schiffversenkt);
        SoundPlayer sieg = new System.Media.SoundPlayer(SchiffeVersenken.Properties.Resources.gewinnerFenster);
   
        Button[,] buttonsBoard;
        TaskCompletionSource<bool> fertigTask = null;
        TaskCompletionSource<bool> angriffTask = null;

        public Spielfeld(int spielerAnzahl, int schiffAnzahl, int feldzeile, int feldspalte, Color[] playerFarbArray, int[] schiffAnzahlArray, int modus, int runden)
        {
            InitializeComponent();
            this.schiffAnzahl = schiffAnzahl;
            this.spielerAnzahl = spielerAnzahl;
            this.spielerFarbArray = playerFarbArray;
            this.spielerArray = new Spieler[spielerAnzahl];
            this.modus = modus;
            this.runden = runden;
            score = new int[spielerAnzahl];

            angriffButtons = new Button[4];
            angriffButtons[0] = spielerfeld1;
            angriffButtons[1] = spielerfeld2;
            angriffButtons[2] = spielerfeld3;
            angriffButtons[3] = spielerfeld4;

            buttonsBoard = new Button[feldzeile, feldspalte];

            //Setzt den Score aller Spieler auf 0
            for (int i = 0; i < spielerAnzahl; i++) score[i] = 0;

            //Erstellt alle nötigen Spielerklassen
            for (int i = 0; i < spielerAnzahl; i++)
            {
                spielerArray[i] = new Spieler(playerFarbArray[i], new int[feldzeile, feldspalte], score[i]);
            }

            //Erstellt alle Buttons als TableLayout in richtigen Dimensionen
            //Dabei wird eine Zeile und Spalte mehr erstellt für die Nummerierung am Rand
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

            //Die Größe der Reihen und Spalten wird festgelegt
            for (int i = 1; i < spielfeld.RowCount; i++)
            {
                spielfeld.RowStyles.Add(new RowStyle(SizeType.Percent, verticalProzent));
            }

            for (int i = 1; i < spielfeld.ColumnCount; i++)
            {
                spielfeld.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, horizontalProzent));
            }
            

            //Buttons werden initalisiert und benannt
            //Änderung der namen und inhalt der spielfeld Buttons
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


            //Switchcase um Hintergrund des Spieleravatars und Scores zu setzen
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
                    MessageBox.Show("Bei dem erstellen des Spielfeldinterfac gab es ein Fehler!", "Fehler Spielfeldinterface");
                    break;
            }

            //Deaktivieren der Schiffe anhand der Anzahl ausgewählter Schiffe
            //Und Schiffsgröße wird in Array gespeichert
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

            //activeBoard = spielerArray[aktiverSpieler].getSpielerBoard();
            activePlayerChanged(aktiverSpieler);
        }
       
        /// <summary>
        /// Eventhandler der generierten Buttons
        /// </summary>
        private void btn_Clicked(Object sender, EventArgs e)
        {
            Button btn = (Button)sender;
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
            //Button wurde 
            fertigTask.TrySetResult(true);
        }

        private void Spielfeld_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Spiel Beenden?", "Spiel Beenden?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
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
                for (int i = 0; i < spielerArray[aktiverSpieler].getSpielerBoard().GetLength(0); i++)
                {
                    for (int j = 0; j < spielerArray[aktiverSpieler].getSpielerBoard().GetLength(1); j++)
                    {
                        if (spielerArray[aktiverSpieler].getSpielerBoardValue(i,j) == schiffNr) spielerArray[aktiverSpieler].setSpielerBoardValue(i,j,0);
                    }
                }
                aktiveSchiffanzahl--;
                refreshButtons(true);
                printBoard(true);
            }
            
            //Schifflänge ist 1, daher gesonderter Fall
            if (length == 1)
            {
                fertigTask = new TaskCompletionSource<bool>();

                infoLabeländern("Wähle eine Position für Schiff " + schiffNr + " aus!");

                await fertigTask.Task;
                if (spielerArray[aktiverSpieler].getSpielerBoardValue(x - 1, y - 1) == 0) {
                    spielerArray[aktiverSpieler].setSpielerBoardValue(x - 1, y - 1, schiffNr);
                    infoLabeländern("Schiff erfolgreich platziert");
                    btn.Text = "neu platzieren";
                    aktiveSchiffanzahl++;
                }
                else
                {
                    MessageBox.Show("Hier ist schon ein Schiff!\nEs kann hier nicht platziert werden!", "Schiff vorhanden");
                    btn.Text = "platzieren";
                    infoLabeländern("Wähle eine Schiff zum Platzieren");
                }
            }
            //Schifflänge ist größer als 1
            else
            {
                int startx, starty = 0;
                fertigTask = new TaskCompletionSource<bool>();

                infoLabeländern("Wähle die erste Position für Schiff " + schiffNr + " aus!");
                
                await fertigTask.Task;

                startx = x;
                starty = y;
                fertigTask = new TaskCompletionSource<bool>();
                infoLabeländern("Wähle die End-Position für Schiff " + schiffNr + " aus!");
                await fertigTask.Task;

                bool clear = true;

                //Fall: Schiff nach oben setzen
                if ((x + (length - 1) == startx) && (starty == y))
                {
                    for (int i = startx; i >= x; i--)
                    {
                        if (spielerArray[aktiverSpieler].getSpielerBoardValue(i - 1, y - 1) > 0) clear = false;
                    }

                    if (clear)
                    {
                        for (int i = startx; i >= x; i--)
                        {
                            spielerArray[aktiverSpieler].setSpielerBoardValue(i - 1, y - 1, schiffNr);
                        }
                    } 
                    else
                    {
                        MessageBox.Show("Hier ist schon ein Schiff!\nEs kann hier nicht platziert werden!", "Schiff vorhanden");
                        btn.Text = "platzieren";
                        infoLabeländern("Wähle eine Schiff zum Platzieren");
                    }
                }

                //Fall: Schiff nach links setzen
                else if ((y + (length - 1) == starty) && (startx == x))
                {
                    for (int i = starty; i >= y; i--)
                    {
                        if (spielerArray[aktiverSpieler].getSpielerBoardValue(x - 1, i - 1) > 0) clear = false;
                    }

                    if (clear)
                    {
                        for (int i = starty; i >= y; i--)
                        {
                            spielerArray[aktiverSpieler].setSpielerBoardValue(x - 1, i - 1, schiffNr);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hier ist schon ein Schiff!\nEs kann hier nicht platziert werden!", "Schiff vorhanden");
                        btn.Text = "platzieren";
                        infoLabeländern("Wähle eine Schiff zum Platzieren");
                    }
                }

                //Fall: Schiff nach unten setzen
                else if ((x - (length - 1) == startx) && (starty == y))
                {
                    for (int i = startx; i <= x; i++)
                    {
                        if (spielerArray[aktiverSpieler].getSpielerBoardValue(i - 1, y - 1) > 0) clear = false;
                    }

                    if (clear)
                    {
                        for (int i = startx; i <= x; i++)
                        {
                            spielerArray[aktiverSpieler].setSpielerBoardValue(i - 1, y - 1, schiffNr);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hier ist schon ein Schiff!\nEs kann hier nicht platziert werden!!", "Schiff vorhanden");
                        btn.Text = "platzieren";
                        infoLabeländern("Wähle eine Schiff zum Platzieren");
                    }
                }

                //Fall: Schiff nach rechts setzen
                else if ((y - (length - 1) == starty) && (startx == x))
                {
                    for (int i = starty; i <= y; i++)
                    {
                        if (spielerArray[aktiverSpieler].getSpielerBoardValue(x - 1, i - 1) > 0) clear = false;
                    }

                    if (clear)
                    {
                        for (int i = starty; i <= y; i++)
                        {
                            spielerArray[aktiverSpieler].setSpielerBoardValue(x - 1, i - 1, schiffNr);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hier ist schon ein Schiff!\nEs kann hier nicht platziert werden!", "Schiff vorhanden");
                        btn.Text = "platzieren";
                        infoLabeländern("Wähle eine Schiff zum Platzieren");
                    }
                }
                else {
                    
                    MessageBox.Show("Das Schiff wurde falsch platziert!", "Schiff Platzieren");
                    btn.Text = "platzieren";
                    infoLabeländern("Wähle eine Schiff zum Platzieren");
                    return;
                }

                if (clear)
                {
                    infoLabeländern("Schiff erfolgreich platziert");
                    btn.Text = "neu platzieren";
                    aktiveSchiffanzahl++;
                }
            }

            printBoard(true);

            if (aktiveSchiffanzahl == schiffAnzahl)
            {
                if (aktiverSpieler == spielerAnzahl-1)
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
                        activePlayerChanged(aktiverSpieler++);
                        infoLabeländern("Wähle eine Schiff zum Platzieren");
                        aktiveSchiffanzahl = 0;
                    }
                }
            }
            deaktivereAlleButtons();

            printBoard(true);
        }

        void printBoard(bool schiffeSichtbar) {

            // Im ganzen Board werden die Farben neu gesetzt
            for (int i = 0; i < spielerArray[aktiverSpieler].getSpielerBoard().GetLength(0); i++)
            {
                for (int j = 0; j < spielerArray[aktiverSpieler].getSpielerBoard().GetLength(1); j++)
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

                    switch (spielerArray[aktiverSpieler].getSpielerBoardValue(i, j))
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
            this.Text = "Spielfeld - Spieler: " + (aktiverSpieler + 1).ToString();
            // Hintergrundfarbe des aktvien Spielers einstellen
            spielfeld.BackColor = spielerArray[aktiverSpieler].getFarbe();
        }

        async void angreifenClick(object sender, EventArgs e) {

            Button btn = (Button)sender;
            int lastPlayer = aktiverSpieler;

            switch (btn.Name)
            {
                case "spielerfeld1":
                    aktiverSpieler = 0;
                    break;
                case "spielerfeld2":
                    aktiverSpieler = 1;
                    break;
                case "spielerfeld3":
                    aktiverSpieler = 2;
                    break;
                case "spielerfeld4":
                    aktiverSpieler = 3;
                    break;
                default:
                    MessageBox.Show("Fehler beim Aufrufen des angegrieffen Spielfeld!", "Fehler Auswahl Spielfeld");
                    break;
            }


            // Spieler greift an und kann keinen anderen Spieler aktuell angreifen
            foreach (Button angriffBtn in angriffButtons) angriffBtn.Enabled = false;

            activePlayerChanged(aktiverSpieler);
            printBoard(false);
            refreshButtons(false);
            attacke.Play();
            infoLabeländern("Wähle eine Position zum Angreifen!", lastPlayer);
            fertigTask = new TaskCompletionSource<bool>();

            await fertigTask.Task;

            int feld = spielerArray[aktiverSpieler].getSpielerBoardValue(x - 1, y - 1);

            if ((feld >= 1) && (feld <= 5))
            {
                // HIT!!!
                treffer.Play();
                spielerArray[aktiverSpieler].setSpielerBoardValue(x - 1, y - 1, 6);
                spielerArray[lastPlayer].addScore(6-feld);
                refreshscore();
                printBoard(false);

                infoLabeländern("Treffer!", lastPlayer);

                if (!spielerArray[aktiverSpieler].hatSchiffNr(feld))
                {
                    zerstört.Play();
                    MessageBox.Show("Schiff ist komplett zerstört");
                }

                if (!spielerArray[aktiverSpieler].hatSchiffe())
                {
                    MessageBox.Show("Alle Schiffe wurden zerstört!","Spieler: " + (aktiverSpieler+1) + " ausgeschieden!");
                    spielerImSpiel--;
                }
            }
            else {
                // WASSERTREFFER!!!
                spielerArray[aktiverSpieler].setSpielerBoardValue(x - 1, y - 1, 7);
                spielerArray[lastPlayer].addScore(0);
                printBoard(false);

                infoLabeländern("Wassertreffer!", lastPlayer);

                daneben.Play();
            }
            
            deaktivereAlleButtons();
            refreshscore();
            aktiverSpieler = lastPlayer;
            angriffTask.TrySetResult(true);
        }

        void refreshButtons(bool schiffPlatzieren)
        {
            int[,] spielerBoard = spielerArray[aktiverSpieler].getSpielerBoard();
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
        

        void auswahlButtonsSetzen(int x)
        {
            for (int i = 0; i < spielerAnzahl; i++)
            {

                if (spielerArray[i].istEliminiert() || x == i)
                {
                    angriffButtons[i].Enabled = false;
                }
                else
                {
                    angriffButtons[i].Enabled = true;
                }
            }
            

            //switch (i)
            //{
            //    case 0:
            //        spielerfeld1.Enabled = false;
            //        break;
            //    case 1:
            //        spielerfeld2.Enabled = false;
            //        break;
            //    case 2:
            //        spielerfeld3.Enabled = false;
            //        break;
            //    case 3:
            //        spielerfeld4.Enabled = false;
            //        break;
            //}
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
            placeschiff1.Visible = false;
            placeschiff2.Visible = false;
            placeschiff3.Visible = false;
            placeschiff4.Visible = false;
            placeschiff5.Visible = false;

            spielerImSpiel = spielerAnzahl;
            aktiverSpieler = 0;

            int letzterSpieler;
            int gewinnerscore = spielerArray[0].getScore();
            int gewinnerspieler = 0;
            List<int> gewinner = new List<int>();

            spielerfeld1.Enabled = false;
            switch (modus) 
            {
                case 1:
                    goto Normal;
                case 2:
                    goto Einerraus;
                case 3:
                    goto Rundenaus;
                default:
                    MessageBox.Show("Es gab Probleme beim auswählen des modus", "Fehler modus");
                    break;
            }
            //normal mode
            Normal:
            while (true)
            {
                MessageBox.Show("Spieler " + (aktiverSpieler + 1) + " ist an der Reihe!", "Nächster Spieler");
                activePlayerChanged(aktiverSpieler);
                printBoard(true);
                auswahlButtonsSetzen(aktiverSpieler);
                infoLabeländern("Wähle einen Spieler zum Angreifen!");

                angriffTask = new TaskCompletionSource<bool>();

                await angriffTask.Task;

                letzterSpieler = aktiverSpieler;

                

                do {
                    aktiverSpieler++;
                    if (aktiverSpieler > spielerAnzahl - 1) {
                        aktiverSpieler = 0;
                        rundenzahlStripbar.Text = (Int32.Parse(rundenzahlStripbar.Text)+1).ToString();
                    }
                } while (spielerArray[aktiverSpieler].istEliminiert());

                

                if (letzterSpieler == aktiverSpieler) break;
            }
            sieg.Play();
            infoLabeländern("Du hast gewonnen!");
            MessageBox.Show("Spieler " + (aktiverSpieler + 1) + " hat gewonnen!", "Spiel beendet!");
            Environment.Exit(0);

        Einerraus:
            while (spielerImSpiel == spielerAnzahl)
            {
                MessageBox.Show("Spieler " + (aktiverSpieler + 1) + " ist an der Reihe!", "Nächster Spieler");
                activePlayerChanged(aktiverSpieler);
                printBoard(true);
                refreshscore();
                auswahlButtonsSetzen(aktiverSpieler);
                infoLabeländern("Wähle einen Spieler zum Angreifen!");


                angriffTask = new TaskCompletionSource<bool>();

                await angriffTask.Task;



                

                do
                {
                    aktiverSpieler++;
                    if (aktiverSpieler > spielerAnzahl - 1)
                    {
                        aktiverSpieler = 0;
                        rundenzahlStripbar.Text = (Int32.Parse(rundenzahlStripbar.Text) + 1).ToString();
                    }
                } while (spielerArray[aktiverSpieler].istEliminiert());

                

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
            aktiverSpieler = gewinnerspieler;
           
            if (gewinner.Count > 1) 
            {
                string ergebnis = string.Join(" , ", gewinner);
                sieg.Play();
                infoLabeländern("Ihr habt gewonnen!", ergebnis);
                MessageBox.Show("Spieler " + ergebnis + " haben gewonnen!", "Spiel beendet!");
            }
            else {
                sieg.Play();
                infoLabeländern("Du hast gewonnen!");
                MessageBox.Show("Spieler " + (aktiverSpieler + 1) + " hat gewonnen!", "Spiel beendet!"); }
            Environment.Exit(0);

        Rundenaus:
            while (aktuelleRunde < runden)
            {
                MessageBox.Show("Spieler " + (aktiverSpieler + 1) + " ist an der Reihe!", "Nächster Spieler");
                activePlayerChanged(aktiverSpieler);
                printBoard(true);
                refreshscore();
                auswahlButtonsSetzen(aktiverSpieler);
                infoLabeländern("Wähle einen Spieler zum Angreifen!");

                angriffTask = new TaskCompletionSource<bool>();
                await angriffTask.Task;

                letzterSpieler = aktiverSpieler;

                do
                {
                    aktiverSpieler++;
                    if (aktiverSpieler > spielerAnzahl - 1)
                    {
                        aktiverSpieler = 0;
                        aktuelleRunde++;
                        rundenzahlStripbar.Text = (Int32.Parse(rundenzahlStripbar.Text) + 1).ToString();
                    }
                } while (spielerArray[aktiverSpieler].istEliminiert());

                if (letzterSpieler == aktiverSpieler) break;
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
            aktiverSpieler = gewinnerspieler;
            if (gewinner.Count > 1)
            {
                string ergebnis = string.Join(" , ", gewinner);
                sieg.Play();
                infoLabeländern("Ihr habt gewonnen!", ergebnis);
                MessageBox.Show("Spieler " + ergebnis + " haben gewonnen!", "Spiel beendet!");
            }
            else {
                sieg.Play();
                infoLabeländern("Du hast gewonnen!");
                MessageBox.Show("Spieler " + (aktiverSpieler + 1) + " hat gewonnen!", "Spiel beendet!");
            }
            Environment.Exit(0);
        }

        void infoLabeländern(string neuerText) {
            infoLabel.Text = "Spieler " + (aktiverSpieler + 1) + " - " + neuerText;
        }

        void infoLabeländern(string neuerText, int aktuellerSpieler) {
            infoLabel.Text = "Spieler " + (aktuellerSpieler+1) + " - " + neuerText;
        }

        void infoLabeländern(string neuerText, string spielerListe)
        {
            infoLabel.Text = "Spieler " + spielerListe + " - " + neuerText;
        }

        void angreifButtonAktualisieren() { 
            
        }
    }
}