namespace GezginZeplin
{
    partial class Menu
    {


        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelHead = new System.Windows.Forms.Label();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.buttonDrawRoad = new System.Windows.Forms.Button();
            this.textBoxCityStart = new System.Windows.Forms.TextBox();
            this.textBoxCityEnd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPassengers = new System.Windows.Forms.TextBox();
            this.buttonCalculateSol = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.mapImage = new System.Windows.Forms.PictureBox();
            this.programBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.AutoSize = true;
            this.labelHead.Font = new System.Drawing.Font("Georgia", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHead.Location = new System.Drawing.Point(1011, 9);
            this.labelHead.Name = "labelHead";
            this.labelHead.Size = new System.Drawing.Size(281, 43);
            this.labelHead.TabIndex = 1;
            this.labelHead.Text = "Gezgin Zeplin";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(993, 219);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ShortcutsEnabled = false;
            this.outputTextBox.Size = new System.Drawing.Size(315, 197);
            this.outputTextBox.TabIndex = 5;
            this.outputTextBox.TabStop = false;
            // 
            // buttonDrawRoad
            // 
            this.buttonDrawRoad.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDrawRoad.Location = new System.Drawing.Point(1179, 186);
            this.buttonDrawRoad.Name = "buttonDrawRoad";
            this.buttonDrawRoad.Size = new System.Drawing.Size(101, 27);
            this.buttonDrawRoad.TabIndex = 5;
            this.buttonDrawRoad.Text = "Draw Map";
            this.buttonDrawRoad.UseVisualStyleBackColor = true;
            this.buttonDrawRoad.Click += new System.EventHandler(this.buttonDrawRoad_Click);
            // 
            // textBoxCityStart
            // 
            this.textBoxCityStart.Location = new System.Drawing.Point(1221, 67);
            this.textBoxCityStart.MaxLength = 2;
            this.textBoxCityStart.Name = "textBoxCityStart";
            this.textBoxCityStart.Size = new System.Drawing.Size(46, 20);
            this.textBoxCityStart.TabIndex = 1;
            this.textBoxCityStart.Text = "01";
            this.textBoxCityStart.WordWrap = false;
            this.textBoxCityStart.Click += new System.EventHandler(this.textBoxCityStart_Click);
            this.textBoxCityStart.TextChanged += new System.EventHandler(this.textBoxCityStart_TextChanged);
            this.textBoxCityStart.Leave += new System.EventHandler(this.textBoxCityStart_Leave);
            // 
            // textBoxCityEnd
            // 
            this.textBoxCityEnd.Location = new System.Drawing.Point(1221, 105);
            this.textBoxCityEnd.MaxLength = 2;
            this.textBoxCityEnd.Name = "textBoxCityEnd";
            this.textBoxCityEnd.Size = new System.Drawing.Size(46, 20);
            this.textBoxCityEnd.TabIndex = 2;
            this.textBoxCityEnd.Text = "01";
            this.textBoxCityEnd.WordWrap = false;
            this.textBoxCityEnd.Click += new System.EventHandler(this.textBoxCityEnd_Click);
            this.textBoxCityEnd.TextChanged += new System.EventHandler(this.textBoxCityEnd_TextChanged);
            this.textBoxCityEnd.Leave += new System.EventHandler(this.textBoxCityEnd_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1055, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Plate of Starting City";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1055, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Plate of End City";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1055, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "Amount of Passengers";
            // 
            // textBoxPassengers
            // 
            this.textBoxPassengers.Location = new System.Drawing.Point(1221, 143);
            this.textBoxPassengers.MaxLength = 2;
            this.textBoxPassengers.Name = "textBoxPassengers";
            this.textBoxPassengers.Size = new System.Drawing.Size(46, 20);
            this.textBoxPassengers.TabIndex = 3;
            this.textBoxPassengers.Text = "05";
            this.textBoxPassengers.WordWrap = false;
            this.textBoxPassengers.Click += new System.EventHandler(this.textBoxPassengers_Click);
            this.textBoxPassengers.TextChanged += new System.EventHandler(this.textBoxPassengers_TextChanged);
            this.textBoxPassengers.Leave += new System.EventHandler(this.textBoxPassengers_Leave);
            // 
            // buttonCalculateSol
            // 
            this.buttonCalculateSol.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCalculateSol.Location = new System.Drawing.Point(1025, 186);
            this.buttonCalculateSol.Name = "buttonCalculateSol";
            this.buttonCalculateSol.Size = new System.Drawing.Size(134, 27);
            this.buttonCalculateSol.TabIndex = 4;
            this.buttonCalculateSol.Text = "Calculate Solutions";
            this.buttonCalculateSol.UseVisualStyleBackColor = true;
            this.buttonCalculateSol.Click += new System.EventHandler(this.buttonCalculateSol_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 48);
            this.contextMenuStrip1.Text = "Menu";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.resetToolStripMenuItem.Text = "Clear";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::GezginZeplin.Properties.Resources.pinLitE;
            this.pictureBox2.Location = new System.Drawing.Point(1025, 101);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 24);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GezginZeplin.Properties.Resources.pinLitS;
            this.pictureBox1.Location = new System.Drawing.Point(1025, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // mapImage
            // 
            this.mapImage.BackColor = System.Drawing.Color.Transparent;
            this.mapImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapImage.Image = global::GezginZeplin.Properties.Resources.map_2;
            this.mapImage.ImageLocation = "";
            this.mapImage.Location = new System.Drawing.Point(0, 0);
            this.mapImage.Name = "mapImage";
            this.mapImage.Size = new System.Drawing.Size(974, 427);
            this.mapImage.TabIndex = 0;
            this.mapImage.TabStop = false;
            // 
            // programBindingSource
            // 
            this.programBindingSource.DataSource = typeof(GezginZeplin.Program);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1327, 428);
            this.Controls.Add(this.buttonCalculateSol);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBoxPassengers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxCityEnd);
            this.Controls.Add(this.textBoxCityStart);
            this.Controls.Add(this.buttonDrawRoad);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.labelHead);
            this.Controls.Add(this.mapImage);
            this.Name = "Menu";
            this.Text = "Menu";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mapImage;
        private System.Windows.Forms.Label labelHead;
        private System.Windows.Forms.BindingSource programBindingSource;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Button buttonDrawRoad;
        private System.Windows.Forms.TextBox textBoxCityStart;
        private System.Windows.Forms.TextBox textBoxCityEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPassengers;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button buttonCalculateSol;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
    }
}