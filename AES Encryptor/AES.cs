using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES_Encryptor
{
    public class AES
    {
        //Variables

        //Cipher key
        byte[] K;
        //Round key
        byte[] Round_key;
        //Number of 32-bit words comprising the Cipher key
        int Nk;
        //Number of rounds
        int Nr;
        //Initialization vector
        byte[] IV;


        //Constants

        //Number of columns (32-bit words) comprising the State
        const int Nb = 4;
        //Round constant
        readonly byte[] Rcon = new byte[]{ 0x8D, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1B, 0x36 };
        //Substitution table
        readonly byte[] Sbox = new byte[]{
            0x63, 0x7C, 0x77, 0x7B, 0xF2, 0x6B, 0x6F, 0xC5, 0x30, 0x01, 0x67, 0x2B, 0xFE, 0xD7, 0xAB, 0x76,
            0xCA, 0x82, 0xC9, 0x7D, 0xFA, 0x59, 0x47, 0xF0, 0xAD, 0xD4, 0xA2, 0xAF, 0x9C, 0xA4, 0x72, 0xC0,
            0xB7, 0xFD, 0x93, 0x26, 0x36, 0x3F, 0xF7, 0xCC, 0x34, 0xA5, 0xE5, 0xF1, 0x71, 0xD8, 0x31, 0x15,
            0x04, 0xC7, 0x23, 0xC3, 0x18, 0x96, 0x05, 0x9A, 0x07, 0x12, 0x80, 0xE2, 0xEB, 0x27, 0xB2, 0x75,
            0x09, 0x83, 0x2C, 0x1A, 0x1B, 0x6E, 0x5A, 0xA0, 0x52, 0x3B, 0xD6, 0xB3, 0x29, 0xE3, 0x2F, 0x84,
            0x53, 0xD1, 0x00, 0xED, 0x20, 0xFC, 0xB1, 0x5B, 0x6A, 0xCB, 0xBE, 0x39, 0x4A, 0x4C, 0x58, 0xCF,
            0xD0, 0xEF, 0xAA, 0xFB, 0x43, 0x4D, 0x33, 0x85, 0x45, 0xF9, 0x02, 0x7F, 0x50, 0x3C, 0x9F, 0xA8,
            0x51, 0xA3, 0x40, 0x8F, 0x92, 0x9D, 0x38, 0xF5, 0xBC, 0xB6, 0xDA, 0x21, 0x10, 0xFF, 0xF3, 0xD2,
            0xCD, 0x0C, 0x13, 0xEC, 0x5F, 0x97, 0x44, 0x17, 0xC4, 0xA7, 0x7E, 0x3D, 0x64, 0x5D, 0x19, 0x73,
            0x60, 0x81, 0x4F, 0xDC, 0x22, 0x2A, 0x90, 0x88, 0x46, 0xEE, 0xB8, 0x14, 0xDE, 0x5E, 0x0B, 0xDB,
            0xE0, 0x32, 0x3A, 0x0A, 0x49, 0x06, 0x24, 0x5C, 0xC2, 0xD3, 0xAC, 0x62, 0x91, 0x95, 0xE4, 0x79,
            0xE7, 0xC8, 0x37, 0x6D, 0x8D, 0xD5, 0x4E, 0xA9, 0x6C, 0x56, 0xF4, 0xEA, 0x65, 0x7A, 0xAE, 0x08,
            0xBA, 0x78, 0x25, 0x2E, 0x1C, 0xA6, 0xB4, 0xC6, 0xE8, 0xDD, 0x74, 0x1F, 0x4B, 0xBD, 0x8B, 0x8A,
            0x70, 0x3E, 0xB5, 0x66, 0x48, 0x03, 0xF6, 0x0E, 0x61, 0x35, 0x57, 0xB9, 0x86, 0xC1, 0x1D, 0x9E,
            0xE1, 0xF8, 0x98, 0x11, 0x69, 0xD9, 0x8E, 0x94, 0x9B, 0x1E, 0x87, 0xE9, 0xCE, 0x55, 0x28, 0xDF,
            0x8C, 0xA1, 0x89, 0x0D, 0xBF, 0xE6, 0x42, 0x68, 0x41, 0x99, 0x2D, 0x0F, 0xB0, 0x54, 0xBB, 0x16 };
        //Inverse substitution table
        readonly byte[] Inv_Sbox = new byte[]{
            0x52, 0x09, 0x6A, 0xD5, 0x30, 0x36, 0xA5, 0x38, 0xBF, 0x40, 0xA3, 0x9E, 0x81, 0xF3, 0xD7, 0xFB,
            0x7C, 0xE3, 0x39, 0x82, 0x9B, 0x2F, 0xFF, 0x87, 0x34, 0x8E, 0x43, 0x44, 0xC4, 0xDE, 0xE9, 0xCB,
            0x54, 0x7B, 0x94, 0x32, 0xA6, 0xC2, 0x23, 0x3D, 0xEE, 0x4C, 0x95, 0x0B, 0x42, 0xFA, 0xC3, 0x4E,
            0x08, 0x2E, 0xA1, 0x66, 0x28, 0xD9, 0x24, 0xB2, 0x76, 0x5B, 0xA2, 0x49, 0x6D, 0x8B, 0xD1, 0x25,
            0x72, 0xF8, 0xF6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xD4, 0xA4, 0x5C, 0xCC, 0x5D, 0x65, 0xB6, 0x92,
            0x6C, 0x70, 0x48, 0x50, 0xFD, 0xED, 0xB9, 0xDA, 0x5E, 0x15, 0x46, 0x57, 0xA7, 0x8D, 0x9D, 0x84,
            0x90, 0xD8, 0xAB, 0x00, 0x8C, 0xBC, 0xD3, 0x0A, 0xF7, 0xE4, 0x58, 0x05, 0xB8, 0xB3, 0x45, 0x06,
            0xD0, 0x2C, 0x1E, 0x8F, 0xCA, 0x3F, 0x0F, 0x02, 0xC1, 0xAF, 0xBD, 0x03, 0x01, 0x13, 0x8A, 0x6B,
            0x3A, 0x91, 0x11, 0x41, 0x4F, 0x67, 0xDC, 0xEA, 0x97, 0xF2, 0xCF, 0xCE, 0xF0, 0xB4, 0xE6, 0x73,
            0x96, 0xAC, 0x74, 0x22, 0xE7, 0xAD, 0x35, 0x85, 0xE2, 0xF9, 0x37, 0xE8, 0x1C, 0x75, 0xDF, 0x6E,
            0x47, 0xF1, 0x1A, 0x71, 0x1D, 0x29, 0xC5, 0x89, 0x6F, 0xB7, 0x62, 0x0E, 0xAA, 0x18, 0xBE, 0x1B,
            0xFC, 0x56, 0x3E, 0x4B, 0xC6, 0xD2, 0x79, 0x20, 0x9A, 0xDB, 0xC0, 0xFE, 0x78, 0xCD, 0x5A, 0xF4,
            0x1F, 0xDD, 0xA8, 0x33, 0x88, 0x07, 0xC7, 0x31, 0xB1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xEC, 0x5F,
            0x60, 0x51, 0x7F, 0xA9, 0x19, 0xB5, 0x4A, 0x0D, 0x2D, 0xE5, 0x7A, 0x9F, 0x93, 0xC9, 0x9C, 0xEF,
            0xA0, 0xE0, 0x3B, 0x4D, 0xAE, 0x2A, 0xF5, 0xB0, 0xC8, 0xEB, 0xBB, 0x3C, 0x83, 0x53, 0x99, 0x61,
            0x17, 0x2B, 0x04, 0x7E, 0xBA, 0x77, 0xD6, 0x26, 0xE1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0C, 0x7D };

        /// <summary>
        /// Key length in bits
        /// </summary>
        public enum KeyLength
        {
            AES128,
            AES192,
            AES256
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key_length">AES key length</param>
        public AES(KeyLength key_length)
        {
            if (key_length == KeyLength.AES128)
            {
                Nk = 4;
                Nr = 10;
                Round_key = new byte[176];
            }
            else if (key_length == KeyLength.AES192)
            {
                Nk = 6;
                Nr = 12;
                Round_key = new byte[208];
            }
            else if (key_length == KeyLength.AES256)
            {
                Nk = 8;
                Nr = 14;
                Round_key = new byte[240];
            }
        }

        /// <summary>
        /// Function setting the AES key
        /// </summary>
        /// <param name="key">AES key</param>
        public void Set_Key(byte[] key)
        {
            //AES128
            if (Nk == 4)
            {
                if (key.Length != 16) throw new ArgumentException("AES128 requires a 16-byte key", "key");

                K = new byte[16];
                for (int i = 0; i < 16; i++)
                {
                    K[i] = key[i];
                }
            }
            //AES192
            else if (Nk == 6)
            {
                if (key.Length != 24) throw new ArgumentException("AES192 requires a 24-byte key", "key");

                K = new byte[24];
                for (int i = 0; i < 24; i++)
                {
                    K[i] = key[i];
                }
            }
            //AES256
            else if (Nk == 8)
            {
                if (key.Length != 32) throw new ArgumentException("AES256 requires a 32-byte key", "key");

                K = new byte[32];
                for (int i = 0; i < 32; i++)
                {
                    K[i] = key[i];
                }
            }

            KeyExpansion();
        }

        /// <summary>
        /// Function setting the AES initialization vector
        /// </summary>
        /// <param name="iv">AES initialization vector</param>
        public void Set_IV(byte[] iv)
        {
            if (iv.Length != 16) throw new ArgumentException("IV must be 16 bytes long", "iv");

            IV = new byte[4 * Nb];
            for (int i = 0; i < 4 * Nb; i++)
            {
                IV[i] = iv[i];
            }
        }

        /// <summary>
        /// Function encrypting the data using AES in CBC mode
        /// </summary>
        /// <param name="data">Data to encrypt</param>
        void Encrypt_CBC(byte[] data)
        {
            if (K == null) throw new Exception("Key must be set");
            if (IV == null) throw new Exception("IV must be set");

            byte[] state = new byte[4 * Nb];

            for (int i = 0; i < data.Length; i += 4 * Nb)
            {
                //Add IV
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < Nb; k++)
                    {
                        data[k + j * Nb + i] ^= IV[k + j * Nb];
                    }
                }
                
                //Prepare data
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < Nb; k++)
                    {
                        state[k + j * Nb] = data[j + k * 4 + i];
                    }
                }

                //Encrypt data
                Cipher(state);

                //Unload data
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < Nb; k++)
                    {
                        data[j + k * 4 + i] = state[k + j * Nb];
                    }
                }

                //Save IV for next block
                for (int j = 0; j < 4 * Nb; j++)
                {
                    IV[j] = data[j + i];
                }
            }
        }

        /// <summary>
        /// Function decrypting the data using AES in CBC mode
        /// </summary>
        /// <param name="data">Data to decrypt</param>
        void Decrypt_CBC(byte[] data)
        {
            if (K == null) throw new Exception("Key must be set");
            if (IV == null) throw new Exception("IV must be set");

            byte[] state = new byte[4 * Nb];
            byte[] next_IV = new byte[4 * Nb];

            for (int i = 0; i < data.Length; i += 4 * Nb)
            {
                //Store IV for next block
                for (int j = 0; j < 4 * Nb; j++)
                {
                    next_IV[j] = data[j + i];
                }

                //Prepare data
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < Nb; k++)
                    {
                        state[k + j * Nb] = data[j + k * 4 + i];
                    }
                }

                //Decrypt data
                InvCipher(state);

                //Unload data
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < Nb; k++)
                    {
                        data[j + k * 4 + i] = state[k + j * Nb];
                    }
                }

                //Add IV
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < Nb; k++)
                    {
                        data[k + j * Nb + i] ^= IV[k + j * Nb];
                    }
                }

                //Save IV for next block
                for (int j = 0; j < 4 * Nb; j++)
                {
                    IV[j] = next_IV[j];
                }
            }
        }


        /// <summary>
        /// AES AddRoundKey transformation
        /// </summary>
        /// <param name="state">State array (usage: state[column + row * Nb])</param>
        /// <param name="round_number">Number of the current round</param>
        void AddRoundKey(byte[] state, int round_number)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < Nb; j++)
                {
                    state[j + i * Nb] ^= Round_key[i + j * 4 + round_number * Nb * 4];
                }
            }
        }

        /// <summary>
        /// AES Cipher function
        /// </summary>
        /// <param name="data">Data to encrypt, formatted as the State (size: 4 * Nb)</param>
        void Cipher(byte[] data)
        {
            AddRoundKey(data, 0);

            for (int i = 1; i < Nr; i++)
            {
                SubBytes(data);
                ShiftRows(data);
                MixColumns(data);
                AddRoundKey(data, i);
            }

            SubBytes(data);
            ShiftRows(data);
            AddRoundKey(data, Nr);
        }

        /// <summary>
        /// AES InvCipher function
        /// </summary>
        /// <param name="data">Data to decrypt, formatted as the State (size: 4 * Nb)</param>
        void InvCipher(byte[] data)
        {
            AddRoundKey(data, Nr);

            for (int i = Nr - 1; i > 0; i--)
            {
                InvShiftRows(data);
                InvSubBytes(data);
                AddRoundKey(data, i);
                InvMixColumns(data);
            }

            InvShiftRows(data);
            InvSubBytes(data);
            AddRoundKey(data, 0);
        }

        /// <summary>
        /// AES InvMixColumns transformation
        /// </summary>
        /// <param name="state">State array (usage: state[column + row * Nb])</param>
        void InvMixColumns(byte[] state)
        {
            //Create the temporary state
            byte[] temp_state = new byte[4 * Nb];

            //Mix each column
            for (int i = 0; i < Nb; i++)
            {
                temp_state[i + 0 * Nb] = (byte)(Multiply(0x0E, state[i + 0 * Nb]) ^ Multiply(0x0B, state[i + 1 * Nb]) ^ Multiply(0x0D, state[i + 2 * Nb]) ^ Multiply(0x09, state[i + 3 * Nb]));
                temp_state[i + 1 * Nb] = (byte)(Multiply(0x09, state[i + 0 * Nb]) ^ Multiply(0x0E, state[i + 1 * Nb]) ^ Multiply(0x0B, state[i + 2 * Nb]) ^ Multiply(0x0D, state[i + 3 * Nb]));
                temp_state[i + 2 * Nb] = (byte)(Multiply(0x0D, state[i + 0 * Nb]) ^ Multiply(0x09, state[i + 1 * Nb]) ^ Multiply(0x0E, state[i + 2 * Nb]) ^ Multiply(0x0B, state[i + 3 * Nb]));
                temp_state[i + 3 * Nb] = (byte)(Multiply(0x0B, state[i + 0 * Nb]) ^ Multiply(0x0D, state[i + 1 * Nb]) ^ Multiply(0x09, state[i + 2 * Nb]) ^ Multiply(0x0E, state[i + 3 * Nb]));
            }

            //Copy temporary state to state
            for (int i = 0; i < 4 * Nb; i++) state[i] = temp_state[i];
        }

        /// <summary>
        /// AES InvShiftRows transformation
        /// </summary>
        /// <param name="state">State array (usage: state[column + row * Nb])</param>
        void InvShiftRows(byte[] state)
        {
            byte temp;

            //Shift row 0 - 0 columns to the right
            //It is already in this state, no need to shift

            //Shift row 1 - 1 column to the right
            temp = state[3 + 1 * Nb];
            state[3 + 1 * Nb] = state[2 + 1 * Nb];
            state[2 + 1 * Nb] = state[1 + 1 * Nb];
            state[1 + 1 * Nb] = state[0 + 1 * Nb];
            state[0 + 1 * Nb] = temp;

            //Shift row 2 - 2 columns to the right
            temp = state[0 + 2 * Nb];
            state[0 + 2 * Nb] = state[2 + 2 * Nb];
            state[2 + 2 * Nb] = temp;

            temp = state[1 + 2 * Nb];
            state[1 + 2 * Nb] = state[3 + 2 * Nb];
            state[3 + 2 * Nb] = temp;

            //Shift row 3 - 3 columns to the right
            temp = state[0 + 3 * Nb];
            state[0 + 3 * Nb] = state[1 + 3 * Nb];
            state[1 + 3 * Nb] = state[2 + 3 * Nb];
            state[2 + 3 * Nb] = state[3 + 3 * Nb];
            state[3 + 3 * Nb] = temp;
        }

        /// <summary>
        /// AES InvSubBytes transformation
        /// </summary>
        /// <param name="state">State array (usage: state[column + row * Nb])</param>
        void InvSubBytes(byte[] state)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < Nb; j++)
                {
                    state[j + i * Nb] = Inv_Sbox[state[j + i * Nb]];
                }
            }
        }

        /// <summary>
        /// AES KeyExpansion function
        /// </summary>
        void KeyExpansion()
        {
            byte[] temp = new byte[4];

            for (int i = 0; i < Nk; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Round_key[4 * i + j] = K[4 * i + j];
                }
            }

            for (int i = Nk; i < Nb * (Nr + 1); i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    temp[j] = Round_key[4 * (i - 1) + j];
                }

                if (i % Nk == 0)
                {
                    RotWord(temp);
                    SubWord(temp);
                    temp[0] ^= Rcon[i / Nk];
                }
                else if (Nk > 6 && i % Nk == 4)
                {
                    SubWord(temp);
                }

                for (int j = 0; j < 4; j++)
                {
                    Round_key[4 * i + j] = (byte)(Round_key[4 * (i - Nk) + j] ^ temp[j]);
                }
            }
        }

        /// <summary>
        /// AES MixColumns transformation
        /// </summary>
        /// <param name="state">State array (usage: state[column + row * Nb])</param>
        void MixColumns(byte[] state)
        {
            //Create the temporary state
            byte[] temp_state = new byte[4 * Nb];
            
            //Mix each column
            for (int i = 0; i < Nb; i++)
            {
                temp_state[i + 0 * Nb] = (byte)(Multiply(0x02, state[i + 0 * Nb]) ^ Multiply(0x03, state[i + 1 * Nb]) ^ state[i + 2 * Nb] ^ state[i + 3 * Nb]);
                temp_state[i + 1 * Nb] = (byte)(state[i + 0 * Nb] ^ Multiply(0x02, state[i + 1 * Nb]) ^ Multiply(0x03, state[i + 2 * Nb]) ^ state[i + 3 * Nb]);
                temp_state[i + 2 * Nb] = (byte)(state[i + 0 * Nb] ^ state[i + 1 * Nb] ^ Multiply(0x02, state[i + 2 * Nb]) ^ Multiply(0x03, state[i + 3 * Nb]));
                temp_state[i + 3 * Nb] = (byte)(Multiply(0x03, state[i + 0 * Nb]) ^ state[i + 1 * Nb] ^ state[i + 2 * Nb] ^ Multiply(0x02, state[i + 3 * Nb]));
            }
            
            //Copy temporary state to state
            for (int i = 0; i < 4 * Nb; i++) state[i] = temp_state[i];
        }

        /// <summary>
        /// Function multiplying two binary polynomials modulo (x^8 + x^4 + x^3 + x + 1)
        /// </summary>
        /// <param name="a">Binary polynomial</param>
        /// <param name="b">Binary polynomial</param>
        /// <returns>Multiplied binary polynomials modulo (x^8 + x^4 + x^3 + x + 1)</returns>
        byte Multiply(byte a, byte b)
        {
            byte r = 0;            
            byte ax = a;            
            byte y = b;
            for (int i = 0; i < 8; i++)
            {
                //Check the least significant bit
                //If (y & 0x01) == 0, r ^= 0 (r remains unchanged)
                //If (y & 0x01) == 1, r ^= ax
                r ^= (byte)(ax * (y & 0x01));

                //Multiply ax by the polynomial x modulo (x^8 + x^4 + x^3 + x + 1)
                ax = xtime(ax);

                //Right shift y (prepare data in the least significant bit)
                y >>= 1;
            }
            return r;
        }

        /// <summary>
        /// AES RotWord function
        /// </summary>
        /// <param name="word">A word to rotate</param>
        void RotWord(byte[] word)
        {
            byte temp;

            temp = word[0];
            word[0] = word[1];
            word[1] = word[2];
            word[2] = word[3];
            word[3] = temp;
        }

        /// <summary>
        /// AES ShiftRows transformation
        /// </summary>
        /// <param name="state">State array (usage: state[column + row * Nb])</param>
        void ShiftRows(byte[] state)
        {
            byte temp;

            //Shift row 0 - 0 columns to the left
            //It is already in this state, no need to shift

            //Shift row 1 - 1 column to the left
            temp = state[0 + 1 * Nb];
            state[0 + 1 * Nb] = state[1 + 1 * Nb];
            state[1 + 1 * Nb] = state[2 + 1 * Nb];
            state[2 + 1 * Nb] = state[3 + 1 * Nb];
            state[3 + 1 * Nb] = temp;

            //Shift row 2 - 2 columns to the left
            temp = state[0 + 2 * Nb];
            state[0 + 2 * Nb] = state[2 + 2 * Nb];
            state[2 + 2 * Nb] = temp;
            
            temp = state[1 + 2 * Nb];
            state[1 + 2 * Nb] = state[3 + 2 * Nb];
            state[3 + 2 * Nb] = temp;

            //Shift row 3 - 3 columns to the left
            temp = state[3 + 3 * Nb];
            state[3 + 3 * Nb] = state[2 + 3 * Nb];
            state[2 + 3 * Nb] = state[1 + 3 * Nb];
            state[1 + 3 * Nb] = state[0 + 3 * Nb];
            state[0 + 3 * Nb] = temp;
        }

        /// <summary>
        /// AES SubBytes transformation
        /// </summary>
        /// <param name="state">State array (usage: state[column + row * Nb])</param>
        void SubBytes(byte[] state)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < Nb; j++)
                {
                    state[j + i * Nb] = Sbox[state[j + i * Nb]];
                }
            }
        }

        /// <summary>
        /// AES SubWord function
        /// </summary>
        /// <param name="word">A word to substitute</param>
        void SubWord(byte[] word)
        {
            for (int i = 0; i < 4; i++)
            {
                word[i] = Sbox[word[i]];
            }
        }

        /// <summary>
        /// Function multiplying the binary polynomial by the polynomial x modulo (x^8 + x^4 + x^3 + x + 1)
        /// </summary>
        /// <param name="b">Binary polynomial</param>
        /// <returns>Binary polynomial multiplied by the polynomial x modulo (x^8 + x^4 + x^3 + x + 1)</returns>
        byte xtime(byte b)
        {
            //If (b >> 7) == 0, return (b << 1)
            //If (b >> 7) == 1, return (b << 1) ^ 0x1B
            return (byte)((b << 1) ^ (((b >> 7) & 0x01)) * 0x1B);
        }
    }
}
