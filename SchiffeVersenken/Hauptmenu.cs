﻿using System;
using System.Windows.Forms;

namespace SchiffeVersenken
{
    public partial class Hauptmenu : Form
    {
        public Hauptmenu()
        {
            InitializeComponent();
            feldgroesseHoehe.SelectedIndex = 5;
            feldgroesseTiefe.SelectedIndex = 5;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Anzahl der Spieler angegeben
            int spielerAnzahl = 0;
            if (spielerAnzahl2.Checked) spielerAnzahl = 2;
            else if (spielerAnzahl3.Checked) spielerAnzahl = 3;
            else if (spielerAnzahl4.Checked) spielerAnzahl = 4;

            // Anzahl der Schiffe
            int schiffAnzahl = 0;
            if (schiffAnzahl1.Checked) schiffAnzahl = 1;
            else if (schiffAnzahl2.Checked) schiffAnzahl = 2;
            else if (schiffAnzahl3.Checked) schiffAnzahl = 3;
            else if (schiffAnzahl4.Checked) schiffAnzahl = 4;
            else if (schiffAnzahl5.Checked) schiffAnzahl = 5;

            // Felddimensoinen
            int feldHoehe = Int32.Parse(feldgroesseHoehe.SelectedItem.ToString());
            int feldTiefe = Int32.Parse(feldgroesseTiefe.SelectedItem.ToString());

            Spielerauswahl spielerauswahl = new Spielerauswahl(spielerAnzahl, schiffAnzahl, feldHoehe, feldTiefe);
            this.Hide();
            spielerauswahl.Closed += (s, args) => this.Show();
            spielerauswahl.StartPosition = FormStartPosition.Manual;
            spielerauswahl.Location = this.Location;
            spielerauswahl.Show();
        }
    }
}
