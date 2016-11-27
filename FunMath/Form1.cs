using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrimeFinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            array = new bool[Int32.Parse(largestNum.Text)];
            for (int i = 0; i < array.Length; i++)
                array[i] = false;
            Stopwatch sw = Stopwatch.StartNew();
            calcPrimes();
            sw.Stop();
            int count = 0;
            for (int i = 0; i < array.Length; i++)
                if (array[i] == false)
                    count++;
            Stopwatch sw2 = Stopwatch.StartNew();
            display();
            sw2.Stop();
            curCount = count;
            textBox1.Text = "Primes found: " + count + " in " + sw.ElapsedMilliseconds + "ms Rendered in " + sw2.ElapsedMilliseconds  + "ms\r\n";
        }
        private void display()
        {
            StringBuilder output = new StringBuilder();
            int pageNum = Int32.Parse(numPerPage.Text);
            output.Append(((page - 1) * pageNum) + " through " + ((page) * pageNum) + " numbers \r\n\r\n");
            for (int i = (page - 1) * pageNum; i < page * pageNum && i < array.Length; i++)
            {
                if(!array[i])
                    output.Append( (i + 1) + "\r\n");
            }
            gotoPageTb.Text = page + "";
            numOutput.Text = output.ToString();

        }
        private int page = 1;
        private int curCount = 0;
        private bool[] array;
        private void calcPrimesRec(Int64 start)
        {
            if (start >= array.Length)
                return;
            for(Int64 i = start * 2 - 1; i < array.Length; i += start)
            {
                if (array[i])
                    continue;
                array[i] = true;
                calcPrimesRec(i);
            }
            if(!array[start])
                calcPrimesRec(start + 1);
        }
        private void calcPrimes()
        {
            for (Int64 i = 2; i < array.Length; i++)
                if (!array[i - 1])
                    for (Int64 j = i * 2 - 1; j < array.Length; j += i)
                        array[j] = true;
        }

        private void nextPageBtn_Click(object sender, EventArgs e)
        {
            int pageNum = Int32.Parse(numPerPage.Text);
            if (array.Length > (page) * pageNum)
            {
                page++;
                display();
            }
            gotoPageTb.Text = page + "";
        }

        private void prevPageBtn_Click(object sender, EventArgs e)
        {
            if(page > 1)
                page--;
            display();
            gotoPageTb.Text = page + "";
        }

        private void numPerPage_TextChanged(object sender, EventArgs e)
        {
            display();
            page = 1;
            gotoPageTb.Text = page + "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            page = Int32.Parse(gotoPageTb.Text);
            Int32 pageNum = Int32.Parse(numPerPage.Text);
            if (array.Length < page * pageNum)
                page = array.Length / pageNum;
            display();
        }
       
    }
}
