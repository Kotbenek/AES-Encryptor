using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AES_Encryptor
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Input type RadioButtons handling
        /// </summary>
        private void Input_type_CheckedChanged(object sender, EventArgs e)
        {
            //Get sender object
            RadioButton rdb = (RadioButton)sender;

            //Handle only the checked RadioButton
            if (!rdb.Checked) return;

            if (rdb == rdbFile)
            {
                //File GUI
                txtInput.Multiline = false;
                txtOutput.Multiline = false;

                txtInput.Height = 20;
                txtOutput.Height = 20;

                this.Height = 270;

                lblOutput.Location = new Point(lblOutput.Location.X, lblOutput.Location.Y - 60);
                txtOutput.Location = new Point(txtOutput.Location.X, txtOutput.Location.Y - 60);

                lblKey.Location = new Point(lblKey.Location.X, lblKey.Location.Y - 120);
                txtKey.Location = new Point(txtKey.Location.X, txtKey.Location.Y - 120);

                lblIV.Location = new Point(lblIV.Location.X, lblIV.Location.Y - 120);
                txtIV.Location = new Point(txtIV.Location.X, txtIV.Location.Y - 120);

                btnEncrypt.Location = new Point(btnEncrypt.Location.X, btnEncrypt.Location.Y - 120);
                btnDecrypt.Location = new Point(btnDecrypt.Location.X, btnDecrypt.Location.Y - 120);
                pbProgress.Location = new Point(pbProgress.Location.X, pbProgress.Location.Y - 120);

                btnBrowseInput.Visible = true;
                btnBrowseOutput.Visible = true;
            }
            else if (rdb == rdbText)
            {
                //Text GUI
                txtInput.Multiline = true;
                txtOutput.Multiline = true;

                txtInput.Height = 80;
                txtOutput.Height = 80;

                this.Height = 390;

                lblOutput.Location = new Point(lblOutput.Location.X, lblOutput.Location.Y + 60);
                txtOutput.Location = new Point(txtOutput.Location.X, txtOutput.Location.Y + 60);

                lblKey.Location = new Point(lblKey.Location.X, lblKey.Location.Y + 120);
                txtKey.Location = new Point(txtKey.Location.X, txtKey.Location.Y + 120);

                lblIV.Location = new Point(lblIV.Location.X, lblIV.Location.Y + 120);
                txtIV.Location = new Point(txtIV.Location.X, txtIV.Location.Y + 120);

                btnEncrypt.Location = new Point(btnEncrypt.Location.X, btnEncrypt.Location.Y + 120);
                btnDecrypt.Location = new Point(btnDecrypt.Location.X, btnDecrypt.Location.Y + 120);
                pbProgress.Location = new Point(pbProgress.Location.X, pbProgress.Location.Y + 120);

                btnBrowseInput.Visible = false;
                btnBrowseOutput.Visible = false;
            }
            else
            {
                throw new Exception("Wrong sender object");
            }

            //Clear input and output textboxes
            txtInput.Text = string.Empty;
            txtOutput.Text = string.Empty;
        }

        /// <summary>
        /// Browsing for an input file to encrypt/decrypt
        /// </summary>
        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*|Encrypted files (*.encrypted)|*.encrypted";

            //If the file was selected successfully
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Set input file path
                txtInput.Text = ofd.FileName;

                //Set default output file path depending on the file extension
                if (Path.GetExtension(ofd.FileName) == ".encrypted")
                {
                    txtOutput.Text = ofd.FileName.Substring(0, ofd.FileName.Length - Path.GetExtension(ofd.FileName).Length);
                }
                else
                {
                    txtOutput.Text = ofd.FileName + ".encrypted";
                }                
            }
        }

        /// <summary>
        /// Browsing for an output file to encrypt/decrypt to
        /// </summary>
        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //SaveFileDialog filter depending on input file extension
            sfd.Filter = (Path.GetExtension(txtInput.Text) == ".encrypted") ? "All files (*.*)|*.*" : "Encrypted files (*.encrypted)|*.encrypted|All files (*.*)|*.*";

            //Set output file path
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                txtOutput.Text = sfd.FileName;
            }
        }
    }
}
