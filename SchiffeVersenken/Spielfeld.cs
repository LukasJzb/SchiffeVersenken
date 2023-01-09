using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchiffeVersenken
{

    public partial class Spielfeld : Form
    {
        // 2D Array des Boards
        // 0 für nichts 1 für schiff 2 für getroffen 3 für daneben
        int[,] spieler1board;
        int[,] spieler2board;
        int[,] spieler3board;
        int[,] spieler4board;
        int y, x = 0;
        TaskCompletionSource<bool> fertig = null;

        public Spielfeld(int spielerAnzahl, int schiffAnzahl, int feldzeile, int feldspalte, Color[] farbArray, int[] schiffAnzahlArray)
        {
            InitializeComponent();


            // Init der Spielerboards mit Dimensionen des Spielfelds
            spieler1board = new int[feldzeile, feldspalte];
            spieler2board = new int[feldzeile, feldspalte];
            if (spielerAnzahl >= 3)
                spieler3board = new int[feldzeile, feldspalte];
            if (spielerAnzahl == 4)
                spieler4board = new int[feldzeile, feldspalte];

            for (int i = 0; i < feldzeile; i++)
            {

                for (int j = 0; j < feldspalte; j++)
                {
                    spieler1board[i,j] = 0;
                    spieler2board[i,j] = 0;
                    if (spielerAnzahl >= 3)
                        spieler3board[i,j] = 0;
                    if (spielerAnzahl == 4)
                        spieler4board[i,j] = 0;
                }

            }



            TableLayoutPanel spielfeld = new TableLayoutPanel();
            
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
                    btn1.Click += btn_Clicked;
                }
                letter++;
            }


            //Switchcasr um hintergrund des spieleravatars und scores zu setzen
            switch (spielerAnzahl)
            {
                case 4:
                    groupBoxspieler4.BackColor = farbArray[3];
                    groupBoxspieler4.Enabled = true;
                    groupBoxScore4.BackColor = farbArray[3];
                    groupBoxScore4.Enabled = true;
                    goto case 3;
                case 3:
                    groupBoxspieler3.BackColor = farbArray[2];
                    groupBoxspieler3.Enabled = true;
                    groupBoxScore3.BackColor = farbArray[2];
                    groupBoxScore3.Enabled = true;
                    goto case 2;
                case 2:
                    groupBoxspieler1.BackColor = farbArray[0];
                    groupBoxspieler2.BackColor = farbArray[1];
                    groupBoxScore1.BackColor = farbArray[0];
                    groupBoxScore2.BackColor = farbArray[1];
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

        }

        //eventhandler der generierten Buttons
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
            fertig.TrySetResult(true);
        }

        private void Spielfeld_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private async void placeschiff1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(string.Format("Schifflänge {0}", schifflaenge1.Text));
            int lenght = Int32.Parse(schifflaenge1.Text);
            if (placeschiff1.Text == "platzieren")
            {
                if (lenght == 1)
                {
 

                    fertig = new TaskCompletionSource<bool>();

                    MessageBox.Show("Wähle eine Position aus.", "Schiff1 Platzieren");
                    await fertig.Task;
                    spieler1board[x - 1, y - 1] = 1;
                    placeschiff1.Text = "neu platzieren";

                }
                else
                {
                    int startx, starty = 0;
                    fertig = new TaskCompletionSource<bool>();

                    MessageBox.Show("Wähle die erste Position aus.", "Schiff1 Platzieren");
                    await fertig.Task;
                    spieler1board[x - 1, y - 1] = 1;
                    startx = x;
                    starty = y;
                    MessageBox.Show("Wähle die zweite Position aus.", "Schiff1 Platzieren");
                    await fertig.Task;
                    if (x + 4 == startx) { spieler1board[x - 1, y - 1] = 1; }
                    else if (y + 4 == startx) { spieler1board[x - 1, y - 1] = 1; }
                    else if (x - 4 == startx) { spieler1board[x - 1, y - 1] = 1; }
                    else if (y - 4 == startx) { spieler1board[x - 1, y - 1] = 1; }
                    else { MessageBox.Show("das schiff ist nicht in der richtigen größe.", "Schiff1 Platzieren"); }
                }
            }
            else 
            {
            
            }
        }
        
    }
    
}


//string msg = "";
//for (int i = 0; i < spieler1board.GetLength(1); i++)
//{
//    for (int j = 0; j < spieler1board.GetLength(0); j++)
//    {
//        msg += String.Format("{0}   ", spieler1board[i, j]);
//    }
//    msg += String.Format("\n");
//}
//MessageBox.Show(msg, "Table");
//MessageBox.Show(string.Format("länge: {0} Breite: {1} ", spieler1board.GetLength(0).ToString(), spieler1board.GetLength(1).ToString(), "tabelle"));