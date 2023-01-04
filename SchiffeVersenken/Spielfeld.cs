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


            float horizontalProzent = feldTiefe / 100;
            float verticalProzent = feldHoehe / 100;

            char letter = 'A';
            spielfeld.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20));
            for (int i = 1; i < spielfeld.ColumnCount; i++) {
                Label label = new Label();
                label.Text = letter++.ToString();
                label.TextAlign = ContentAlignment.MiddleCenter;
                spielfeld.Controls.Add(label, i, 0);
            }

            char numb = '0';
            spielfeld.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
            for (int i = 1; i < spielfeld.RowCount; i++)
            {
                Label label = new Label();
                label.Text = numb++.ToString();
                label.Dock = DockStyle.Fill;
                label.TextAlign = ContentAlignment.MiddleCenter;
                spielfeld.Controls.Add(label, 0, i);
            }

            for (int i = 1; i < spielfeld.RowCount + 1; i++)
            {
                spielfeld.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / spielfeld.RowCount));
            }

            for (int i = 1; i < spielfeld.ColumnCount + 1; i++)
            {
                spielfeld.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / spielfeld.ColumnCount));
            }

            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    Button btn1 = new Button();
                    btn1.Dock = DockStyle.Fill;
                    spielfeld.Controls.Add(btn1, i+1, j+1);
                }
            }
        }
    }
}
