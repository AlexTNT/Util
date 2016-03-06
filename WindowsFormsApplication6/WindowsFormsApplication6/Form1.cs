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

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool validate(){        //проверки полей на пустые значения
            int valid=0;

            if (textBox1.Text == "")
                textBox1.BackColor = Color.LightCoral;
            else
            {
                textBox1.BackColor = Color.White;
                valid++;
            }
            if(textBox2.Text=="")
                textBox2.BackColor=Color.LightCoral;
            else
            {
                textBox2.BackColor = Color.White;
                valid++;
            }
            if(textBox3.Text=="")
                textBox3.BackColor = Color.LightCoral;
            else
            {
                textBox3.BackColor = Color.White;
                valid++;
            }
            if (textBox4.Text == "")
                textBox4.BackColor = Color.LightCoral;
            else
            {
                textBox4.BackColor = Color.White;
                valid++;
            }

            if (valid == 4) return true;
            else return false;
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                //Form2 listForm = new Form2();
                //listForm.Show();

                int countOfCopied = 0;
                int countOfProceed = 0;
                string sourceDir = textBox2.Text;
                string destDir = textBox3.Text;
                string fileName = "";
                string[] dirs = Directory.GetFiles(textBox2.Text, textBox4.Text);
                SourceElemCount.Text = dirs.Length.ToString();
                foreach (string filePath in dirs)
                {
                    countOfProceed++;
                    CountOfProceedElem.Text = countOfProceed.ToString();
                    fileName = filePath.Substring(sourceDir.Length + 1);
                    listBox1.Items.Add(fileName);
                   // listForm.addItem(fileName);
                    using (FileStream fs = File.Open(filePath, FileMode.Open))
                    {
                        byte[] b = new byte[1024];
                        UTF8Encoding temp = new UTF8Encoding(true);

                        while (fs.Read(b, 0, b.Length) > 0)
                        {
                            if (temp.GetString(b).Contains(textBox1.Text))
                            {

                                fs.Close();
                                countOfCopied++;
                                DestinationElemCount.Text = countOfCopied.ToString();
                                listBox2.Items.Add(fileName);
                                try { File.Copy(filePath, (destDir + "\\" + fileName), true); }
                                catch (Exception se) { Console.WriteLine(se); }
                                break;
                            }
                        }
                    }
                }
            }
            else
                MessageBox.Show("Заполнить поля");
        }
    }

    
}
