namespace AES_Encryptor
{
    partial class Form
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.lblInput = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnBrowseInput = new System.Windows.Forms.Button();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.grpInputType = new System.Windows.Forms.GroupBox();
            this.rdbText = new System.Windows.Forms.RadioButton();
            this.rdbFile = new System.Windows.Forms.RadioButton();
            this.lblKey = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.lblIV = new System.Windows.Forms.Label();
            this.txtIV = new System.Windows.Forms.TextBox();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.grpInputType.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(12, 9);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(34, 13);
            this.lblInput.TabIndex = 0;
            this.lblInput.Text = "Input:";
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(12, 48);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(42, 13);
            this.lblOutput.TabIndex = 1;
            this.lblOutput.Text = "Output:";
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtInput.Location = new System.Drawing.Point(12, 25);
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInput.Size = new System.Drawing.Size(479, 20);
            this.txtInput.TabIndex = 2;
            // 
            // txtOutput
            // 
            this.txtOutput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtOutput.Location = new System.Drawing.Point(12, 64);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(479, 20);
            this.txtOutput.TabIndex = 3;
            // 
            // btnBrowseInput
            // 
            this.btnBrowseInput.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseInput.Image")));
            this.btnBrowseInput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowseInput.Location = new System.Drawing.Point(497, 23);
            this.btnBrowseInput.Name = "btnBrowseInput";
            this.btnBrowseInput.Size = new System.Drawing.Size(75, 24);
            this.btnBrowseInput.TabIndex = 4;
            this.btnBrowseInput.Text = "Browse...";
            this.btnBrowseInput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBrowseInput.UseVisualStyleBackColor = true;
            this.btnBrowseInput.Click += new System.EventHandler(this.btnBrowseInput_Click);
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseOutput.Image")));
            this.btnBrowseOutput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowseOutput.Location = new System.Drawing.Point(497, 61);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(75, 24);
            this.btnBrowseOutput.TabIndex = 5;
            this.btnBrowseOutput.Text = "Browse...";
            this.btnBrowseOutput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBrowseOutput.UseVisualStyleBackColor = true;
            // 
            // grpInputType
            // 
            this.grpInputType.Controls.Add(this.rdbText);
            this.grpInputType.Controls.Add(this.rdbFile);
            this.grpInputType.Location = new System.Drawing.Point(497, 96);
            this.grpInputType.Name = "grpInputType";
            this.grpInputType.Size = new System.Drawing.Size(75, 68);
            this.grpInputType.TabIndex = 6;
            this.grpInputType.TabStop = false;
            this.grpInputType.Text = "Input type";
            // 
            // rdbText
            // 
            this.rdbText.AutoSize = true;
            this.rdbText.Location = new System.Drawing.Point(6, 42);
            this.rdbText.Name = "rdbText";
            this.rdbText.Size = new System.Drawing.Size(46, 17);
            this.rdbText.TabIndex = 1;
            this.rdbText.Text = "Text";
            this.rdbText.UseVisualStyleBackColor = true;
            this.rdbText.CheckedChanged += new System.EventHandler(this.Input_type_CheckedChanged);
            // 
            // rdbFile
            // 
            this.rdbFile.AutoSize = true;
            this.rdbFile.Checked = true;
            this.rdbFile.Location = new System.Drawing.Point(6, 19);
            this.rdbFile.Name = "rdbFile";
            this.rdbFile.Size = new System.Drawing.Size(41, 17);
            this.rdbFile.TabIndex = 0;
            this.rdbFile.TabStop = true;
            this.rdbFile.Text = "File";
            this.rdbFile.UseVisualStyleBackColor = true;
            this.rdbFile.CheckedChanged += new System.EventHandler(this.Input_type_CheckedChanged);
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(12, 87);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(28, 13);
            this.lblKey.TabIndex = 7;
            this.lblKey.Text = "Key:";
            // 
            // txtKey
            // 
            this.txtKey.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtKey.Location = new System.Drawing.Point(12, 103);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(479, 20);
            this.txtKey.TabIndex = 8;
            // 
            // lblIV
            // 
            this.lblIV.AutoSize = true;
            this.lblIV.Location = new System.Drawing.Point(12, 126);
            this.lblIV.Name = "lblIV";
            this.lblIV.Size = new System.Drawing.Size(20, 13);
            this.lblIV.TabIndex = 9;
            this.lblIV.Text = "IV:";
            // 
            // txtIV
            // 
            this.txtIV.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIV.Location = new System.Drawing.Point(12, 142);
            this.txtIV.Name = "txtIV";
            this.txtIV.Size = new System.Drawing.Size(479, 20);
            this.txtIV.TabIndex = 10;
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Image = ((System.Drawing.Image)(resources.GetObject("btnEncrypt.Image")));
            this.btnEncrypt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEncrypt.Location = new System.Drawing.Point(12, 168);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 11;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEncrypt.UseVisualStyleBackColor = true;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Image = ((System.Drawing.Image)(resources.GetObject("btnDecrypt.Image")));
            this.btnDecrypt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDecrypt.Location = new System.Drawing.Point(93, 168);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 23);
            this.btnDecrypt.TabIndex = 12;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDecrypt.UseVisualStyleBackColor = true;
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(12, 197);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(560, 23);
            this.pbProgress.TabIndex = 13;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 231);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.txtIV);
            this.Controls.Add(this.lblIV);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.grpInputType);
            this.Controls.Add(this.btnBrowseOutput);
            this.Controls.Add(this.btnBrowseInput);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.lblInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.ShowIcon = false;
            this.Text = "AES Encryptor";
            this.grpInputType.ResumeLayout(false);
            this.grpInputType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnBrowseInput;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.GroupBox grpInputType;
        private System.Windows.Forms.RadioButton rdbText;
        private System.Windows.Forms.RadioButton rdbFile;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label lblIV;
        private System.Windows.Forms.TextBox txtIV;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.ProgressBar pbProgress;
    }
}

