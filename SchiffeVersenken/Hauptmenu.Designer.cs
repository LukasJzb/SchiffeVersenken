
namespace SchiffeVersenken
{
    partial class Hauptmenu
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hauptmenu));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.spielerAnzahl4 = new System.Windows.Forms.RadioButton();
            this.spielerAnzahl3 = new System.Windows.Forms.RadioButton();
            this.spielerAnzahl2 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.schiffAnzahl5 = new System.Windows.Forms.RadioButton();
            this.schiffAnzahl4 = new System.Windows.Forms.RadioButton();
            this.schiffAnzahl3 = new System.Windows.Forms.RadioButton();
            this.schiffAnzahl2 = new System.Windows.Forms.RadioButton();
            this.schiffAnzahl1 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.feldgroesseTiefe = new System.Windows.Forms.ComboBox();
            this.feldgroesseHoehe = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.button2, 4, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.51643F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.24883F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(944, 501);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 3);
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(352, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Schiffe Versenken";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(379, 281);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(182, 90);
            this.button1.TabIndex = 1;
            this.button1.Text = "Spiel Starten";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.spielerAnzahl4);
            this.groupBox1.Controls.Add(this.spielerAnzahl3);
            this.groupBox1.Controls.Add(this.spielerAnzahl2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(191, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 147);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spieleranzahl (2-4)";
            // 
            // spielerAnzahl4
            // 
            this.spielerAnzahl4.AutoSize = true;
            this.spielerAnzahl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.spielerAnzahl4.Location = new System.Drawing.Point(3, 50);
            this.spielerAnzahl4.Name = "spielerAnzahl4";
            this.spielerAnzahl4.Size = new System.Drawing.Size(176, 17);
            this.spielerAnzahl4.TabIndex = 2;
            this.spielerAnzahl4.Text = "4 Spieler";
            this.spielerAnzahl4.UseVisualStyleBackColor = true;
            // 
            // spielerAnzahl3
            // 
            this.spielerAnzahl3.AutoSize = true;
            this.spielerAnzahl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.spielerAnzahl3.Location = new System.Drawing.Point(3, 33);
            this.spielerAnzahl3.Name = "spielerAnzahl3";
            this.spielerAnzahl3.Size = new System.Drawing.Size(176, 17);
            this.spielerAnzahl3.TabIndex = 1;
            this.spielerAnzahl3.Text = "3 Spieler";
            this.spielerAnzahl3.UseVisualStyleBackColor = true;
            // 
            // spielerAnzahl2
            // 
            this.spielerAnzahl2.AutoSize = true;
            this.spielerAnzahl2.Checked = true;
            this.spielerAnzahl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.spielerAnzahl2.Location = new System.Drawing.Point(3, 16);
            this.spielerAnzahl2.Name = "spielerAnzahl2";
            this.spielerAnzahl2.Size = new System.Drawing.Size(176, 17);
            this.spielerAnzahl2.TabIndex = 0;
            this.spielerAnzahl2.TabStop = true;
            this.spielerAnzahl2.Text = "2 Spieler";
            this.spielerAnzahl2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.schiffAnzahl5);
            this.groupBox2.Controls.Add(this.schiffAnzahl4);
            this.groupBox2.Controls.Add(this.schiffAnzahl3);
            this.groupBox2.Controls.Add(this.schiffAnzahl2);
            this.groupBox2.Controls.Add(this.schiffAnzahl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(379, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(182, 147);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Anzahl der Schiffe";
            // 
            // schiffAnzahl5
            // 
            this.schiffAnzahl5.AutoSize = true;
            this.schiffAnzahl5.Checked = true;
            this.schiffAnzahl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.schiffAnzahl5.Location = new System.Drawing.Point(3, 84);
            this.schiffAnzahl5.Name = "schiffAnzahl5";
            this.schiffAnzahl5.Size = new System.Drawing.Size(176, 17);
            this.schiffAnzahl5.TabIndex = 4;
            this.schiffAnzahl5.TabStop = true;
            this.schiffAnzahl5.Text = "5 Schiffe";
            this.schiffAnzahl5.UseVisualStyleBackColor = true;
            // 
            // schiffAnzahl4
            // 
            this.schiffAnzahl4.AutoSize = true;
            this.schiffAnzahl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.schiffAnzahl4.Location = new System.Drawing.Point(3, 67);
            this.schiffAnzahl4.Name = "schiffAnzahl4";
            this.schiffAnzahl4.Size = new System.Drawing.Size(176, 17);
            this.schiffAnzahl4.TabIndex = 3;
            this.schiffAnzahl4.Text = "4 Schiffe";
            this.schiffAnzahl4.UseVisualStyleBackColor = true;
            // 
            // schiffAnzahl3
            // 
            this.schiffAnzahl3.AutoSize = true;
            this.schiffAnzahl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.schiffAnzahl3.Location = new System.Drawing.Point(3, 50);
            this.schiffAnzahl3.Name = "schiffAnzahl3";
            this.schiffAnzahl3.Size = new System.Drawing.Size(176, 17);
            this.schiffAnzahl3.TabIndex = 2;
            this.schiffAnzahl3.Text = "3 Schiffe";
            this.schiffAnzahl3.UseVisualStyleBackColor = true;
            // 
            // schiffAnzahl2
            // 
            this.schiffAnzahl2.AutoSize = true;
            this.schiffAnzahl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.schiffAnzahl2.Location = new System.Drawing.Point(3, 33);
            this.schiffAnzahl2.Name = "schiffAnzahl2";
            this.schiffAnzahl2.Size = new System.Drawing.Size(176, 17);
            this.schiffAnzahl2.TabIndex = 1;
            this.schiffAnzahl2.Text = "2 Schiffe";
            this.schiffAnzahl2.UseVisualStyleBackColor = true;
            // 
            // schiffAnzahl1
            // 
            this.schiffAnzahl1.AutoSize = true;
            this.schiffAnzahl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.schiffAnzahl1.Location = new System.Drawing.Point(3, 16);
            this.schiffAnzahl1.Name = "schiffAnzahl1";
            this.schiffAnzahl1.Size = new System.Drawing.Size(176, 17);
            this.schiffAnzahl1.TabIndex = 0;
            this.schiffAnzahl1.Text = "1 Schiff";
            this.schiffAnzahl1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.feldgroesseTiefe);
            this.groupBox3.Controls.Add(this.feldgroesseHoehe);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(567, 128);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(182, 147);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Feldgröße";
            // 
            // feldgroesseTiefe
            // 
            this.feldgroesseTiefe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.feldgroesseTiefe.FormattingEnabled = true;
            this.feldgroesseTiefe.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.feldgroesseTiefe.Location = new System.Drawing.Point(136, 47);
            this.feldgroesseTiefe.Name = "feldgroesseTiefe";
            this.feldgroesseTiefe.Size = new System.Drawing.Size(43, 21);
            this.feldgroesseTiefe.TabIndex = 5;
            // 
            // feldgroesseHoehe
            // 
            this.feldgroesseHoehe.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.feldgroesseHoehe.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.feldgroesseHoehe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.feldgroesseHoehe.FormattingEnabled = true;
            this.feldgroesseHoehe.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.feldgroesseHoehe.Location = new System.Drawing.Point(136, 21);
            this.feldgroesseHoehe.Name = "feldgroesseHoehe";
            this.feldgroesseHoehe.Size = new System.Drawing.Size(43, 21);
            this.feldgroesseHoehe.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Spalten";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Zeilen";
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(755, 377);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(186, 121);
            this.button2.TabIndex = 2;
            this.button2.Text = "Spiel Beenden";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Hauptmenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::SchiffeVersenken.Properties.Resources.Ocean_Background_1_RF_RMPL_011;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(960, 540);
            this.Name = "Hauptmenu";
            this.Text = "Schiffe Versenken";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton spielerAnzahl4;
        private System.Windows.Forms.RadioButton spielerAnzahl3;
        private System.Windows.Forms.RadioButton spielerAnzahl2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton schiffAnzahl5;
        private System.Windows.Forms.RadioButton schiffAnzahl4;
        private System.Windows.Forms.RadioButton schiffAnzahl3;
        private System.Windows.Forms.RadioButton schiffAnzahl2;
        private System.Windows.Forms.RadioButton schiffAnzahl1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox feldgroesseTiefe;
        public System.Windows.Forms.ComboBox feldgroesseHoehe;
    }
}

