namespace TestSkyndar
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.exit = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.confirmation_btn = new System.Windows.Forms.Button();
            this.disponibilites_btn = new System.Windows.Forms.Button();
            this.Prestation_btn = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.dispo1 = new TestSkyndar.dispo();
            this.prestations1 = new TestSkyndar.Prestations();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(254)))));
            this.panel1.Controls.Add(this.exit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 35);
            this.panel1.TabIndex = 0;
            // 
            // exit
            // 
            this.exit.AutoSize = true;
            this.exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exit.ForeColor = System.Drawing.Color.Black;
            this.exit.Location = new System.Drawing.Point(1073, 9);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(15, 16);
            this.exit.TabIndex = 0;
            this.exit.Text = "X";
            this.exit.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(254)))));
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.confirmation_btn);
            this.panel2.Controls.Add(this.disponibilites_btn);
            this.panel2.Controls.Add(this.Prestation_btn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(225, 565);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Agency FB", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(83, 543);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 22);
            this.label2.TabIndex = 5;
            this.label2.Text = "Skyndar";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // confirmation_btn
            // 
            this.confirmation_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(254)))));
            this.confirmation_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.confirmation_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.confirmation_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(254)))));
            this.confirmation_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(113)))), ((int)(((byte)(255)))));
            this.confirmation_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.confirmation_btn.Font = new System.Drawing.Font("Baskerville Old Face", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmation_btn.Location = new System.Drawing.Point(0, 295);
            this.confirmation_btn.Name = "confirmation_btn";
            this.confirmation_btn.Size = new System.Drawing.Size(225, 73);
            this.confirmation_btn.TabIndex = 4;
            this.confirmation_btn.Text = "Confirmation";
            this.confirmation_btn.UseVisualStyleBackColor = false;
            this.confirmation_btn.Click += new System.EventHandler(this.button2_Click);
            // 
            // disponibilites_btn
            // 
            this.disponibilites_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(254)))));
            this.disponibilites_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.disponibilites_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.disponibilites_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(254)))));
            this.disponibilites_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(113)))), ((int)(((byte)(255)))));
            this.disponibilites_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.disponibilites_btn.Font = new System.Drawing.Font("Baskerville Old Face", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.disponibilites_btn.Location = new System.Drawing.Point(0, 216);
            this.disponibilites_btn.Name = "disponibilites_btn";
            this.disponibilites_btn.Size = new System.Drawing.Size(225, 73);
            this.disponibilites_btn.TabIndex = 3;
            this.disponibilites_btn.Text = "Disponibilités";
            this.disponibilites_btn.UseVisualStyleBackColor = false;
            this.disponibilites_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // Prestation_btn
            // 
            this.Prestation_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(254)))));
            this.Prestation_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Prestation_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Prestation_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(254)))));
            this.Prestation_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(113)))), ((int)(((byte)(255)))));
            this.Prestation_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Prestation_btn.Font = new System.Drawing.Font("Baskerville Old Face", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Prestation_btn.Location = new System.Drawing.Point(0, 137);
            this.Prestation_btn.Name = "Prestation_btn";
            this.Prestation_btn.Size = new System.Drawing.Size(225, 73);
            this.Prestation_btn.TabIndex = 2;
            this.Prestation_btn.Text = "Prestations";
            this.Prestation_btn.UseVisualStyleBackColor = false;
            this.Prestation_btn.Click += new System.EventHandler(this.Prestation_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.prestations1);
            this.panel3.Controls.Add(this.dispo1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(225, 35);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(875, 565);
            this.panel3.TabIndex = 2;
            // 
            // dispo1
            // 
            this.dispo1.Location = new System.Drawing.Point(0, -3);
            this.dispo1.Name = "dispo1";
            this.dispo1.Size = new System.Drawing.Size(875, 565);
            this.dispo1.TabIndex = 3;
            // 
            // prestations1
            // 
            this.prestations1.Location = new System.Drawing.Point(0, 0);
            this.prestations1.Name = "prestations1";
            this.prestations1.Size = new System.Drawing.Size(875, 565);
            this.prestations1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label exit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button Prestation_btn;
        private System.Windows.Forms.Button confirmation_btn;
        private System.Windows.Forms.Button disponibilites_btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private Prestations prestations1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private dispo dispo1;
    }
}

