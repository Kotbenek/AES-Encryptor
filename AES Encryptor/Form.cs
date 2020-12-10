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
using System.Text.RegularExpressions;

namespace AES_Encryptor
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }
                
        private void Form_Load(object sender, EventArgs e)
        {
            chkInputHex.Visible = false;
            chkDecryptionOutputHex.Visible = false;
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

                chkInputHex.Visible = false;
                chkDecryptionOutputHex.Visible = false;

                chkDecryptionOutputHex.Location = new Point(chkDecryptionOutputHex.Location.X, chkDecryptionOutputHex.Location.Y - 60);
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

                chkInputHex.Visible = true;
                chkDecryptionOutputHex.Visible = true;

                chkDecryptionOutputHex.Location = new Point(chkDecryptionOutputHex.Location.X, chkDecryptionOutputHex.Location.Y + 60);
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

        /// <summary>
        /// Function converting a string containing hex characters to byte array
        /// </summary>
        /// <param name="s">String containing only hex characters with no whitespaces</param>
        /// <returns>Byte array with data from passed string</returns>
        byte[] HexString_to_ByteArray(string s)
        {
            if (s.Length % 2 == 1) throw new ArgumentException("Input string cannot have an odd number of characters", "s");

            byte[] array = new byte[s.Length / 2];
            
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (byte)((HexChar_to_Byte(s[2 * i]) << 4) + HexChar_to_Byte(s[2 * i + 1]));
            }

            return array;
        }

        /// <summary>
        /// Function converting a hex char to half byte representation
        /// </summary>
        /// <param name="c">Hex char</param>
        /// <returns>Half byte representation (0x00 - 0x0F)</returns>
        byte HexChar_to_Byte(char c)
        {
            return (byte)(c - (c <= '9' ? '0' : c <= 'F' ? 'A' - 10 : 'a' - 10));
        }

        /// <summary>
        /// Function converting a string containing hex characters (and possibly whitespaces) to string with only hex characters
        /// </summary>
        /// <param name="s">String containing hex characters (can contain whitespaces)</param>
        /// <returns>String containing only hex characters with no whitespaces</returns>
        string String_to_HexString(string s)
        {
            //Extract hex characters
            return Regex.Replace(s, "[^a-fA-F0-9]", string.Empty);
        }

        /// <summary>
        /// Function setting the AES key from string
        /// </summary>
        /// <param name="aes">AES object</param>
        /// <param name="key">Key string</param>
        void AES_Set_Key(AES aes, string key)
        {
            string k = String_to_HexString(key);

            if (k.Length != 64)
            {
                //Wrong key length error handling
                throw new NotImplementedException();
            }
            else
            {
                aes.Set_Key(HexString_to_ByteArray(k));
            }
        }

        /// <summary>
        /// Function setting the AES initialization vector from string
        /// </summary>
        /// <param name="aes">AES object</param>
        /// <param name="iv">IV string</param>
        void AES_Set_IV(AES aes, string iv)
        {
            string v = String_to_HexString(iv);

            if (v.Length != 32)
            {
                //Wrong IV length error handling
                throw new NotImplementedException();
            }
            else
            {
                aes.Set_IV(HexString_to_ByteArray(v));
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (rdbFile.Checked)
            {
                throw new NotImplementedException();
            }
            else if (rdbText.Checked)
            {
                //Create AES object
                AES aes = new AES(AES.KeyLength.AES256);
                AES_Set_Key(aes, txtKey.Text);
                AES_Set_IV(aes, txtIV.Text);
                
                //Prepare input and output streams
                MemoryStream ms_input = new MemoryStream(chkInputHex.Checked ? HexString_to_ByteArray(String_to_HexString(txtInput.Text)) : Encoding.UTF8.GetBytes(txtInput.Text));
                MemoryStream ms_output = new MemoryStream();

                //Encrypt data
                aes.Encrypt_CBC_PKCS7(ms_input, ms_output);

                //Get encrypted data
                byte[] output = ms_output.ToArray();

                //Convert to hex string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in output)
                {
                    sb.Append(b.ToString("X2"));
                    sb.Append(" ");
                }
                
                //Display encrypted data
                txtOutput.Text = sb.ToString(0, sb.Length - 1);
            }
            else
            {
                throw new Exception("No Input type was selected");
            }
        }        
    }
}
