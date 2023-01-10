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
    public partial class Spielerauswahl : Form
    {
        int spielerAnzahl;
        int schiffAnzahl;
        int feldHoehe;
        int feldTiefe;
        Button[] farbButtons = new Button[4];
        Color[] farbArray = new Color[4];
        ComboBox[] schiffComboArray = new ComboBox[5];
        int[] schiffAnzahlArray = new int[5];


        public Spielerauswahl(int spielerAnzahl, int schiffAnzahl, int feldHoehe, int feldTiefe)
        {
            InitializeComponent();
            schiff1.SelectedIndex = 0;
            schiff2.SelectedIndex = 1;
            schiff3.SelectedIndex = 2;
            schiff4.SelectedIndex = 3;
            schiff5.SelectedIndex = 4;

            this.spielerAnzahl = spielerAnzahl;
            this.schiffAnzahl = schiffAnzahl;
            this.feldHoehe = feldHoehe;
            this.feldTiefe = feldTiefe;



            // Spieler Auswahl Boxen ausgrauen
            switch (spielerAnzahl) {
                case 4:
                    groupSpieler4.Enabled = true;
                    farbButtons[3] = spieler4farbe;
                    goto case 3;
                case 3:
                    groupSpieler3.Enabled = true;
                    farbButtons[2] = spieler3farbe;
                    goto case 2;
                case 2:
                    farbButtons[1] = spieler2farbe;
                    farbButtons[0] = spieler1farbe;
                    break;
                default:
                    MessageBox.Show("Bei der Anzahl der Spieler gab es einen Fehler!","Fehler Spieleranzahl");
                    break;
            }


            // Schiffauswahl aktivieren anhand der Schiffanzahl
            switch (schiffAnzahl)
            {
                case 5:
                    groupSchiff5.Enabled = true;
                    schiffComboArray[4] = schiff5;
                    goto case 4;
                case 4:
                    groupSchiff4.Enabled = true;
                    schiffComboArray[3] = schiff4;
                    goto case 3;
                case 3:
                    groupSchiff3.Enabled = true;
                    schiffComboArray[2] = schiff3;
                    goto case 2;
                case 2:
                    groupSchiff2.Enabled = true;
                    schiffComboArray[1] = schiff2;
                    goto case 1;
                case 1:
                    groupSchiff1.Enabled = true;
                    schiffComboArray[0] = schiff1;
                    break;
                default:
                    MessageBox.Show("Bei der Anzahl der Schiffe ist ein Fehler aufgetretten!", "Fehler Schiffanzahl");
                    break;
            }
        }

        //Auswahl der Farbe
        private void farbeWechselnDialog(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = (sender as Button).BackColor;

            // Update the text box color if the user clicks OK 
            while (MyDialog.ShowDialog() == DialogResult.OK)
            {
                if (checkiftaken(MyDialog.Color))
                {
                    MessageBox.Show("Farbe vergeben! Bitte wähle eine andere Farbe", "Farbe vergeben");

                }
                else if (MyDialog.Color == Color.Black)
                {
                    MessageBox.Show("Du darfst die Farbe schwarz nicht wählen!", "Falsche Farbe");
                }
                else
                {
                    (sender as Button).BackColor = MyDialog.Color;
                    return;
                }
            }
        }
        //verhindert doppelte farben
        private Boolean checkiftaken(Color color) {
            return (color == spieler1farbe.BackColor || color == spieler2farbe.BackColor || color == spieler3farbe.BackColor || color == spieler4farbe.BackColor);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //speichert die farben und schifflängen in arrays
        private void button1_Click(object sender, EventArgs e)
        {   
            for (int i = 0; i < spielerAnzahl; i++) {
                farbArray[i] = farbButtons[i].BackColor;
            }

            for (int i = 0; i < schiffAnzahl; i++)
            {
                schiffAnzahlArray[i] = Int32.Parse(schiffComboArray[i].SelectedItem.ToString());
            }


            Spielfeld spielfeldForm = new Spielfeld(spielerAnzahl, schiffAnzahl, feldHoehe, feldTiefe, farbArray, schiffAnzahlArray);
            this.Hide();
            spielfeldForm.Show();
        }


    }
}
