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
    public partial class Hauptmenu : Form
    {
        public Hauptmenu()
        {
            InitializeComponent();
            feldgroesseHoehe.SelectedIndex = 5;
            feldgroesseTiefe.SelectedIndex = 5;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Spielerauswahl spielerauswahl = new Spielerauswahl();
            this.Hide();
            spielerauswahl.Show();
            
        }
    }
}
