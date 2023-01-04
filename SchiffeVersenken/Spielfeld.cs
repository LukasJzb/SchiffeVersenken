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
        public Spielfeld(int spielerAnzahl, int schiffAnzahl, int feldHoehe, int feldTiefe,Color[] farbArray,int[] schiffAnzahlArray)
        {
            InitializeComponent();

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

            for (int i = 0; i < feldTiefe; ++i)
            {
                for (int j = 0; j < feldHoehe; ++j)
                {
                    Button btn1 = new Button();
                    btn1.Dock = DockStyle.Fill;
                    spielfeld.Controls.Add(btn1, i+1, j+1);
                }
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
           
          
           
        }
    }
}
