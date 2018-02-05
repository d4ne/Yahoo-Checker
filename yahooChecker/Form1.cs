using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yahooChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Text Files";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                dataGridView1.Rows.RemoveAt(0);

                int i = 0;

                foreach (var line in File.ReadAllLines(filePath))
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = line;
                    dataGridView1.Rows[i].Cells[1].Value = "Unchecked";
                    i++;
                }

                bunifuCustomLabel5.Text = dataGridView1.Rows.Count.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
            dataGridView1.Rows[0].Cells[0].Value = "No handle added";
            dataGridView1.Rows[0].Cells[1].Value = "Unchecked";
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount <= 1)
            {
                MessageBox.Show("Please upload a wordlist containing atleast 1 email address.", "Yahoo checker Error");
            }
            else
            {
                new Thread(new ThreadStart(check)) { IsBackground = true }.Start();
            }
        }

        private void check()
        {
            bool status = false;
            bool cancel = (bool)this.Invoke((Func<bool, bool>)DoCheapGuiAccess, status);

            yahoo yahoo = new yahoo();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string address = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string[] words = address.Split('@');

                string handle = words[0];
                string host = words[1];

                try
                {
                    string response = yahoo.checkYahoo(handle, host);

                    if (response == "true")
                    {
                        dataGridView1.Rows[i].Cells[1].Value = "Doesnt exist";
                    }
                    else if (response == "false")
                    {
                        dataGridView1.Rows[i].Cells[1].Value = "Exist";
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[1].Value = "Error";
                    }
                }
                catch
                {
                    dataGridView1.Rows[i].Cells[1].Value = "Error";
                }
            }

            bool status2 = true;
            bool cancel2 = (bool)this.Invoke((Func<bool, bool>)DoCheapGuiAccess, status2);

            MessageBox.Show("Mail check finished. You could export the result now.", "Yahoo checker info");
        }

        bool DoCheapGuiAccess(bool status)
        {
            if (status == false)
            {
                bunifuCustomLabel8.Text = "Running.";
                return true;
            }
            else
            {
                bunifuCustomLabel8.Text = "Finished.";
                return true;
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount <= 1)
            {
                MessageBox.Show("Please upload a wordlist containing atleast 1 email address.", "Yahoo checker Error");
            }
            else
            {
                new Thread(new ThreadStart(export)) { IsBackground = true }.Start();
            }
        }

        private void export()
        {
            string file_name = AppDomain.CurrentDomain.BaseDirectory + "/export.txt";
            TextWriter writer = new StreamWriter(file_name);
            string line = "";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if(dataGridView1.Rows[i].Cells[1].Value.ToString() == "Doesnt exist")
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        line += dataGridView1.Columns[j].HeaderText + ": " + dataGridView1.Rows[i].Cells[j].Value.ToString() + " | ";
                    }

                    line = line.Remove(line.Length - 3);
                    writer.Write(line);
                    line = "";
                    writer.WriteLine("");
                }
            }

            writer.Close();
            MessageBox.Show("Data exported to the following text file: " + file_name + "!", "Yahoo checker info");
        }
    }
}
