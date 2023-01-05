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
        int[,] spieler1board;
        int[,] spieler2board;
        int[,] spieler3board;
        int[,] spieler4board;

        //List<Button> positions = new List<Button> {};

    public Spielfeld(int spielerAnzahl, int schiffAnzahl, int feldHoehe, int feldTiefe, Color[] farbArray, int[] schiffAnzahlArray)
        {
            InitializeComponent();


            // Init der Spielerboards mit Dimensionen des Spielfelds
            spieler1board = new int[feldHoehe, feldTiefe];
            spieler2board = new int[feldHoehe, feldTiefe];
            if (spielerAnzahl >= 3)
                spieler3board = new int[feldHoehe, feldTiefe];
            if (spielerAnzahl == 4)
                spieler4board = new int[feldHoehe, feldTiefe];


            TableLayoutPanel spielfeld = new TableLayoutPanel();
            
            spielfeld.RowCount = feldHoehe + 1;
            spielfeld.ColumnCount = feldTiefe + 1;

            spielfeld.Dock = DockStyle.Fill;
            spielfeld.RowStyles.Clear();
            spielfeld.ColumnStyles.Clear();



            this.mainGrid.Controls.Add(spielfeld, 0, 0);


            float horizontalProzent = 100 / feldTiefe;
            float verticalProzent = 100 / feldHoehe;

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
            letter = 'A';
            for (int i = 0; i < feldTiefe; ++i)
            {

                for (int j = 0; j < feldHoehe; ++j)
                {
                    Button btn1 = new Button();
                    btn1.Dock = DockStyle.Fill;
                    spielfeld.Controls.Add(btn1, i + 1, j + 1);
                    btn1.Text = (j+1).ToString() + letter;
                    btn1.Name = (j + 1).ToString() + letter;
                    //positions.Add(btn1);
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

            //var message = string.Join(Environment.NewLine, positions);
            //MessageBox.Show(message);


           
        }



        private void Spielfeld_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void placeschiff1_Click(object sender, EventArgs e)
        {
            //if(schifflaenge1.Text == "1")
            //{
            //    MessageBox.Show("Wähle einen platz zum platzieren aus", "Schiff1 platzieren");
            //}
            //else 
            //{
            //    MessageBox.Show("Wähle die erste Koordinate aus und dann die zweite", "Schiff1 platzieren");
            //}

            //for(int i = 0; i < positions.Count; i++)
            //{

            //}
            
            //if(placeschiff1.Text == "platzieren")
            //{
            //    placeschiff1.Text = "neu Platzieren";
            //}
            //else 
            //{ 
            //    placeschiff1.Text = "platzieren"; 
            //}
        }
    }
}
