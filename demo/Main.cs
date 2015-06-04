using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using csmatio.types;
using csmatio.io;

namespace CSMatIOTest
{
    public partial class Main : Form
    {
		private bool toggleCheck = false;

        public Main()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            DialogResult dRes = openFileDialog.ShowDialog();
            if (dRes == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                txtOutput.Text = txtOutput.Text + "Attempting to read the file '" + fileName + "'...";
                try
                {
                    MatFileReader mfr = new MatFileReader(fileName);
                    txtOutput.Text += "Done!\nMAT-file contains the following:\n";
                    txtOutput.Text += mfr.MatFileHeader.ToString() + "\n";
                    foreach (MLArray mla in mfr.Data)
                    {
                        txtOutput.Text = txtOutput.Text + mla.ContentToString() + "\n";
                    }
                }
                catch (System.IO.IOException)
                {
                    txtOutput.Text = txtOutput.Text + "Invalid MAT-file!\n";
                    MessageBox.Show("Invalid binary MAT-file! Please select a valid binary MAT-file.",
                        "Invalid MAT-file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                    
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            List<MLArray> mlList = new List<MLArray>();
            // Go through each of the options to add in the file
            if (chkCell.Checked)
            {
                mlList.Add(CreateCellArray());
            }
            if (chkStruct.Checked)
            {
                mlList.Add(CreateStructArray());
            }
            if (chkChar.Checked)
            {
                mlList.Add(CreateCharArray());
            }
            if (chkSparse.Checked)
            {
                mlList.Add(CreateSparseArray());
            }
            if (chkDouble.Checked)
            {
                mlList.Add(CreateDoubleArray());
            }
            if (chkSingle.Checked)
            {
                mlList.Add(CreateSingleArray());
            }
            if (chkInt8.Checked)
            {
                mlList.Add(CreateInt8Array());
            }
            if (chkUInt8.Checked)
            {
                mlList.Add(CreateUInt8Array());
            }
            if (chkInt16.Checked)
            {
                mlList.Add(CreateInt16Array());
            }
            if (chkUInt16.Checked)
            {
                mlList.Add(CreateUInt16Array());
            }
            if (chkInt32.Checked)
            {
                mlList.Add(CreateInt32Array());
            }
            if (chkUInt32.Checked)
            {
                mlList.Add(CreateUIn32Array());
            }
            if (chkInt64.Checked)
            {
                mlList.Add(CreateInt64Array());
            }
            if (chkUInt64.Checked)
            {
                mlList.Add(CreateUInt64Array());
            }
            if (chkImagMatrix.Checked)
            {
                mlList.Add(CreateImaginaryArray());
            }

            if (mlList.Count <= 0)
            {
                MessageBox.Show("You must select at least one MAT-file Creation Element in order to" +
                    " create a MAT-file.", "No MAT-file elements selected", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            // Get a filename name to write the file out to
            saveFileDialog.ShowDialog();
            string filename = saveFileDialog.FileName;

            txtOutput.Text += "Creating the MAT-file '" + filename + "'...";

            try
            {
                MatFileWriter mfw = new MatFileWriter(filename, mlList, chkCompress.Checked);
            }
            catch (Exception err)
            {
                MessageBox.Show("There was an error when creating the MAT-file: \n" + err.ToString(),
                    "MAT-File Creation Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtOutput.Text += "Failed!\n";
                return;
            }

            txtOutput.Text += "Done!\nMAT-File created with the following data: \n";
            foreach (MLArray mla in mlList)
                txtOutput.Text += mla.ContentToString() + "\n";    
        }

		private void btnCheckEmAll_Click(object sender, EventArgs e)
		{
			toggleCheck = !toggleCheck;

			chkCell.Checked =
			chkStruct.Checked =
			chkChar.Checked =
			chkSparse.Checked =
			chkDouble.Checked =
			chkSingle.Checked =
			chkInt8.Checked =
			chkUInt8.Checked =
			chkInt16.Checked =
			chkUInt16.Checked =
			chkInt32.Checked =
			chkUInt32.Checked =
			chkInt64.Checked =
			chkUInt64.Checked =
			chkImagMatrix.Checked =
			toggleCheck;
		}

        private static MLArray CreateCellArray()
        {
            string[] names = new string[] { "Hello", "World", "I am", "a", "MAT-file" };
            MLCell cell = new MLCell("Names", new int[] { names.Length, 1 });
            for (int i = 0; i < names.Length; i++)
                cell[i] = new MLChar(null, names[i]);
            return cell;
        }

        private static MLArray CreateStructArray()
        {
            MLStructure structure = new MLStructure("X", new int[] { 1, 1 });
            structure["w", 0] = new MLUInt8("", new byte[] { 1 }, 1);
            structure["y", 0] = new MLUInt8("", new byte[] { 2 }, 1);
            structure["z", 0] = new MLUInt8("", new byte[] { 3 }, 1);
            return structure;
        }

        private static MLArray CreateCharArray()
        {
#if NET20
            return new MLChar("AName", "Hello World v2.0!");
#endif
#if NET40
			return new MLChar("AName", "Hello World v4.0!");
#endif
        }

        private static MLArray CreateSparseArray()
        {
            MLSparse sparse = new MLSparse("S", new int[] { 3, 3 }, 0, 3);
            sparse.SetReal(1.5, 0, 0);
            sparse.SetReal(2.5, 1, 1);
            sparse.SetReal(3.5, 2, 2);
            return sparse;
        }

        private static MLArray CreateDoubleArray()
        {
            return new MLDouble("Double", new double[] { double.MaxValue, double.MinValue }, 1);
        }

        private static MLArray CreateSingleArray()
        {
            return new MLSingle("Single", new float[] { float.MinValue, float.MaxValue }, 1);
        }

        private static MLArray CreateInt8Array()
        {
            return new MLInt8("Int8", new sbyte[] { sbyte.MinValue, sbyte.MaxValue }, 1);
        }

        private static MLArray CreateUInt8Array()
        {
            return new MLUInt8("UInt8", new byte[] { byte.MinValue, byte.MaxValue }, 1);
        }

        private static MLArray CreateInt16Array()
        {
            return new MLInt16("Int16", new short[] { short.MinValue, short.MaxValue }, 1);
        }

        private static MLArray CreateUInt16Array()
        {
            return new MLUInt16("UInt16", new ushort[] { ushort.MinValue, ushort.MaxValue }, 1);
        }

        private static MLArray CreateInt32Array()
        {
            return new MLInt32("Int32", new int[] { int.MinValue, int.MaxValue }, 1);
        }

        private static MLArray CreateUIn32Array()
        {
            return new MLUInt32("UInt32", new uint[] { uint.MinValue, uint.MaxValue }, 1);
        }

        private static MLArray CreateInt64Array()
        {
            return new MLInt64("Int64", new long[] { long.MinValue, long.MaxValue }, 1);
        }

        private static MLArray CreateUInt64Array()
        {
            return new MLUInt64("UInt64", new ulong[] { ulong.MinValue, ulong.MaxValue }, 1);
        }

        private static MLArray CreateImaginaryArray()
        {
            // Create a large, randomaly generated imaginary array
            long[] myRealNums = new long[2000];
            long[] myImagNums = new long[2000];
            Random numGen = new Random();
            for (int i = 0; i < myRealNums.Length; i++)
            {
                myRealNums[i] = (long)numGen.Next(int.MinValue, int.MaxValue);
                myImagNums[i] = (long)numGen.Next(int.MinValue, int.MaxValue);
            }
            MLInt64 myImagArray =
                new MLInt64("IA", myRealNums, myImagNums, myRealNums.Length / 5);
            return myImagArray;
        }
    }
}