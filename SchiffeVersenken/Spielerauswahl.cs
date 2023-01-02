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
        public Spielerauswahl()
        {
            InitializeComponent();
            schiff1.SelectedIndex = 0;
            schiff2.SelectedIndex = 1;
            schiff3.SelectedIndex = 2;
            schiff4.SelectedIndex = 3;
            schiff5.SelectedIndex = 4;
        }

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
                if (checkiftaken(MyDialog.Color))
                {
                    MessageBox.Show("Farbe vergeben! Bitte wähle eine andere Farbe", "Farbe vergeben");
                    
                } else { 
                    (sender as Button).BackColor = MyDialog.Color;
                    return;
                }
        }

        private Boolean checkiftaken(Color color) {
            return (color == spieler1farbe.BackColor || color == spieler2farbe.BackColor || color == spieler3farbe.BackColor || color == spieler4farbe.BackColor);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
