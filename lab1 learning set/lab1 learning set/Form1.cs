using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace lab1_learning_set
{
    
    public partial class Form1 : Form
    {
        
        
        DataGridViewCell cel;
        DataGridViewColumn changecolumn;
        int i, j, k, index_symbol=0;
        int[] Arr_code_symbol = new int[156]; //12*13
        string code;
        string etalon;
        ArrayList code_symbol = new ArrayList();
        string filename;

        Symdol[] simvol = new Symdol[5];
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("Коды символов");
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;

            dataGridView2.AllowUserToResizeColumns = false;
            dataGridView2.AllowUserToResizeRows = false;


            for (i = 0; i < 12; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView2.Rows.Add();
            }

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;

            dataGridView2.RowHeadersVisible = false;
            dataGridView2.ColumnHeadersVisible = false;
            for (i = 0; i < dataGridView1.RowCount; i++)
            {
                for (j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    cel = dataGridView1.Rows[i].Cells[j];
                    changecolumn = dataGridView1.Columns[j];

                    changecolumn.Width = 15;
                    cel.Style.BackColor = System.Drawing.Color.Green;

                    cel = dataGridView2.Rows[i].Cells[j];
                    changecolumn = dataGridView2.Columns[j];

                    changecolumn.Width = 15;
                    cel.Style.BackColor = System.Drawing.Color.Green;
                }

            }
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            dataGridView1.ClearSelection();
            cel = dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells[dataGridView1.CurrentCellAddress.X];
            //cel.Value = dataGridView1.CurrentCellAddress.X;
            cel.Style.BackColor = System.Drawing.Color.Yellow;
            cel.Value = 0;



        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            dataGridView1.ClearSelection();
            cel = dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells[dataGridView1.CurrentCellAddress.X];
            cel.Style.BackColor = System.Drawing.Color.Red;
            cel.Value = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (i = 0; i < dataGridView1.RowCount; i++)
                for (j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    cel = dataGridView1.Rows[i].Cells[j];
                    if (cel.Value != null)
                        code = code + Convert.ToString(cel.Value);
                }

            label1.Text = Convert.ToString(code_symbol.Count);
            //textBox1.Text =Convert.ToString( code );
            listBox1.Items.Add(code);
            code = null;
        }
        public void PrintValues(IEnumerable myArrList)
        {
            System.Collections.IEnumerator myEnum = myArrList.GetEnumerator();
            while (myEnum.MoveNext()) ;
            textBox1.Text = Convert.ToString(myEnum.Current);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\test\Code_symbols.txt", false);
            //for (i = 1; i < listBox1.Items.Count; i++)
            //    file.WriteLine(listBox1.Items[i]);
            //file.Close();
        }

        void createNoise(char symb) //, int kol
        {
            Random rndm = new Random();
            Digit[] dig = new Digit[156];
            filename = Convert.ToString(symb);

            StreamReader sr = new StreamReader(@"C:\Users\HP\Desktop\3.5 kurs\PatRec\лаб1\" + filename + ".txt", Encoding.Default);
            etalon = sr.ReadToEnd();
            sr.Close();

            for (i = 0; i < etalon.Length; i++)
            {
                dig[i].priznak = int.Parse(Convert.ToString(etalon[i]));
                dig[i].noise = rndm.NextDouble();
                if (dig[i].noise < 0.1f)
                {
                    dig[i].priznak = Invert(dig[i].priznak);
                }
            }
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\HP\Desktop\3.5 kurs\PatRec\лаб1\Symbols with noise.txt", true);
            for (i = 0; i < dig.Length; i++)
            {
                file.Write(dig[i].priznak);
              //  file.WriteLine();
            }
            file.Write(symb);
            file.WriteLine();
            file.Close();

           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random rndm = new Random();            
            Digit[] dig = new Digit[156];
              for (i = 0; i < etalon.Length; i++)
              {
                  dig[i].priznak = int.Parse(Convert.ToString(etalon[i]));
                  dig[i].noise = rndm.NextDouble();
                  if (dig[i].noise < 0.1f)
                  {
                      dig[i].priznak = Invert(dig[i].priznak);
                  }
              }
              k = 0;
              for (i = 0; i < dataGridView2.RowCount; i++)
                  for (j = 0; j < dataGridView2.ColumnCount; j++)
                  {
                      cel = dataGridView2.Rows[i].Cells[j];
                      if ( (dig[k].priznak) == 1)
                      {
                          // cel.Value = Convert.ToString(etalon[k]);
                          cel.Style.BackColor = System.Drawing.Color.Red;
                      }
                      else
                      {
                          //cel.Value = Convert.ToString(etalon[k]);
                          cel.Style.BackColor = System.Drawing.Color.Yellow;

                      }
                      k++;
                      //code = code + Convert.ToString(cel.Value);
                  }
        }

        public struct Digit
        {
            public int priznak;
            public double noise;

            //Digit(int priznak, double noise)
            //{
            //    this.priznak = priznak;
            //    this.noise = noise;
            //}

        }
        public struct Symdol
        {

            public float count;
            public char name;
        }

        int Invert(int prizna)
        {
            if (prizna == 1)
             return 0; 
            else            
              return 1;        
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            
            
            simvol[index_symbol].name = Convert.ToChar(comboBox1.Text);
            simvol[index_symbol].count = Convert.ToSingle(comboBox2.Text);

            listBox1.Items.Add(Convert.ToString(simvol[index_symbol].name) +" "+ Convert.ToString(simvol[index_symbol].count));
            index_symbol++;
            filename = comboBox1.Text; //textBox2.Text;

            StreamReader sr = new StreamReader(@"C:\Users\HP\Desktop\3.5 kurs\PatRec\лаб1\" + filename + ".txt", Encoding.Default);
            etalon = sr.ReadToEnd();
            sr.Close();
            k = 0;
            for (i = 0; i < dataGridView1.RowCount; i++)
                for (j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    cel = dataGridView1.Rows[i].Cells[j];
                    if ((int.Parse(Convert.ToString(etalon[k])) == 1))
                    {
                        // cel.Value = Convert.ToString(etalon[k]);
                        cel.Style.BackColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        //cel.Value = Convert.ToString(etalon[k]);
                        cel.Style.BackColor = System.Drawing.Color.Yellow;

                    }
                    k++;
                    //code = code + Convert.ToString(cel.Value);
                }
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
          int  r=1;
          while (r <= simvol[3].count)//simvol[3].count
            {
                createNoise(simvol[3].name);
                r++;
            }

        }


    }
}

