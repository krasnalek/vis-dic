using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Wordnet
{
    sealed partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.btnDirectory1 = new System.Windows.Forms.Button();
            this.btnSearchLiteral = new System.Windows.Forms.Button();
            this.txtLiteral = new System.Windows.Forms.TextBox();
            this.listResults = new System.Windows.Forms.ListBox();
            this.bdInput = new System.Windows.Forms.OpenFileDialog();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.lblFileName1 = new System.Windows.Forms.Label();
            this.btnSearchNumber = new System.Windows.Forms.Button();
            this.lblFileName2 = new System.Windows.Forms.Label();
            this.btnDirectory2 = new System.Windows.Forms.Button();
            this.btnCombine = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDirectory1
            // 
            this.btnDirectory1.Location = new System.Drawing.Point(22, 33);
            this.btnDirectory1.Name = "btnDirectory1";
            this.btnDirectory1.Size = new System.Drawing.Size(164, 48);
            this.btnDirectory1.TabIndex = 0;
            this.btnDirectory1.Text = "Choose 1st file";
            this.btnDirectory1.UseVisualStyleBackColor = true;
            this.btnDirectory1.Click += new System.EventHandler(this.btnDirectory_Click);
            // 
            // btnSearchLiteral
            // 
            this.btnSearchLiteral.Location = new System.Drawing.Point(192, 153);
            this.btnSearchLiteral.Name = "btnSearchLiteral";
            this.btnSearchLiteral.Size = new System.Drawing.Size(133, 37);
            this.btnSearchLiteral.TabIndex = 1;
            this.btnSearchLiteral.Text = "Search word (1)";
            this.btnSearchLiteral.UseVisualStyleBackColor = true;
            this.btnSearchLiteral.Click += new System.EventHandler(this.btnSearchLiteral_Click);
            // 
            // txtLiteral
            // 
            this.txtLiteral.Location = new System.Drawing.Point(22, 160);
            this.txtLiteral.Name = "txtLiteral";
            this.txtLiteral.Size = new System.Drawing.Size(164, 22);
            this.txtLiteral.TabIndex = 2;
            // 
            // listResults
            // 
            this.listResults.FormattingEnabled = true;
            this.listResults.ItemHeight = 16;
            this.listResults.Location = new System.Drawing.Point(22, 203);
            this.listResults.Name = "listResults";
            this.listResults.Size = new System.Drawing.Size(769, 228);
            this.listResults.TabIndex = 3;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // lblFileName1
            // 
            this.lblFileName1.AutoSize = true;
            this.lblFileName1.Location = new System.Drawing.Point(202, 45);
            this.lblFileName1.Name = "lblFileName1";
            this.lblFileName1.Size = new System.Drawing.Size(0, 17);
            this.lblFileName1.TabIndex = 4;
            // 
            // btnSearchNumber
            // 
            this.btnSearchNumber.Location = new System.Drawing.Point(331, 153);
            this.btnSearchNumber.Name = "btnSearchNumber";
            this.btnSearchNumber.Size = new System.Drawing.Size(150, 37);
            this.btnSearchNumber.TabIndex = 5;
            this.btnSearchNumber.Text = "Search number (1)";
            this.btnSearchNumber.UseVisualStyleBackColor = true;
            this.btnSearchNumber.Click += new System.EventHandler(this.btnSearchNumber_Click);
            // 
            // lblFileName2
            // 
            this.lblFileName2.AutoSize = true;
            this.lblFileName2.Location = new System.Drawing.Point(202, 99);
            this.lblFileName2.Name = "lblFileName2";
            this.lblFileName2.Size = new System.Drawing.Size(0, 17);
            this.lblFileName2.TabIndex = 7;
            // 
            // btnDirectory2
            // 
            this.btnDirectory2.Location = new System.Drawing.Point(22, 87);
            this.btnDirectory2.Name = "btnDirectory2";
            this.btnDirectory2.Size = new System.Drawing.Size(164, 48);
            this.btnDirectory2.TabIndex = 6;
            this.btnDirectory2.Text = "Choose second file";
            this.btnDirectory2.UseVisualStyleBackColor = true;
            this.btnDirectory2.Click += new System.EventHandler(this.btnDirectory2_Click);
            // 
            // btnCombine
            // 
            this.btnCombine.Location = new System.Drawing.Point(514, 153);
            this.btnCombine.Name = "btnCombine";
            this.btnCombine.Size = new System.Drawing.Size(277, 37);
            this.btnCombine.TabIndex = 8;
            this.btnCombine.Text = "Combine";
            this.btnCombine.UseVisualStyleBackColor = true;
            this.btnCombine.Click += new System.EventHandler(this.btnCombine_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 513);
            this.Controls.Add(this.btnCombine);
            this.Controls.Add(this.lblFileName2);
            this.Controls.Add(this.btnDirectory2);
            this.Controls.Add(this.btnSearchNumber);
            this.Controls.Add(this.lblFileName1);
            this.Controls.Add(this.listResults);
            this.Controls.Add(this.txtLiteral);
            this.Controls.Add(this.btnSearchLiteral);
            this.Controls.Add(this.btnDirectory1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button btnDirectory1;
        private Button btnSearchLiteral;
        private TextBox txtLiteral;
        private ListBox listResults;
        private OpenFileDialog bdInput;
        private FileSystemWatcher fileSystemWatcher1;
        private Label lblFileName1;
        private Button btnSearchNumber;
        private Label lblFileName2;
        private Button btnDirectory2;
        private Button btnCombine;
    }
}

