using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        public static int LevenshteinDistance(string string1, string string2)
        {
            if (string1 == null) throw new ArgumentNullException("string1");
            if (string2 == null) throw new ArgumentNullException("string2");
            int diff;
            int[,] m = new int[string1.Length + 1, string2.Length + 1];

            for (int i = 0; i <= string1.Length; i++) { m[i, 0] = i; }
            for (int j = 0; j <= string2.Length; j++) { m[0, j] = j; }

            for (int i = 1; i <= string1.Length; i++)
            {
                for (int j = 1; j <= string2.Length; j++)
                {
                    diff = (string1[i - 1] == string2[j - 1]) ? 0 : 1;

                    m[i, j] = Math.Min(Math.Min(m[i - 1, j] + 1,
                                             m[i, j - 1] + 1),
                                             m[i - 1, j - 1] + diff);
                }
            }
            return m[string1.Length, string2.Length];
        }
        List<String> dictionary;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Visible = false;
            textBox1.Visible = false;
            listBox1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ChooseFile = new OpenFileDialog();
            ChooseFile.Filter = "Text Files|*.txt";
            ChooseFile.Title = "Select text File";
            dictionary = new List<string>();
            String text;
            if (ChooseFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                text = File.ReadAllText(ChooseFile.FileName);
                foreach(String word in text.Split())
                    {
                        if (dictionary.Contains(word) == false) { dictionary.Add(word); };
                    };
                watch.Stop();
                listBox1.Items.Clear();
                label1.Text = watch.Elapsed.ToString();
                button2.Visible = true;
                textBox1.Visible = true;
                // StreamReader sr = new
                //System.IO.StreamReader(ChooseFile.FileName);
               // MessageBox.Show(sr.ReadToEnd());
                //sr.Close();
                
                
            }
            

        }
        private void label1_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            listBox1.Items.Clear();
           /* if (dictionary.Contains(textBox1.Text))
            {
                listBox1.BeginUpdate();
                if(listBox1.Items.Contains(textBox1.Text)==false){listBox1.Items.Add(textBox1.Text);}
                listBox1.EndUpdate();
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Слово не обнаружено");
                textBox1.Text = "";
            } */
            foreach (string element in dictionary)
            {
                if ((int.Parse(textBox2.Text)) >= (LevenshteinDistance(element, textBox1.Text)))
                {
                    listBox1.BeginUpdate();
                    listBox1.Items.Add(element);
                    listBox1.EndUpdate();                   

                }
           }
            textBox1.Text = "";
            watch.Stop();
            label2.Text = watch.Elapsed.ToString();
            listBox1.Visible = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }



    }
}
