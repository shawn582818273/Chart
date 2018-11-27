using DryDump;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace chart
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null; //每次打开清空内容
            DataTable dt = getData().Tables[0];
            dataGridView1.DataSource = dt;
            FormRelated formr = new FormRelated();
            FormatRelated formatr = new FormatRelated();
            ArrayList yDataList = formr.DataTableToArrayList(dt);
            formr.DataToChart(chart1, chart1.Series[0], (object[])yDataList[6], (object[])yDataList[2], SeriesChartType.Line);
            formr.DataToChart(chart1, chart1.Series[1], (object[])yDataList[6], (object[])yDataList[3], SeriesChartType.Line);
            formr.DataToChart(chart1, chart1.Series[2], (object[])yDataList[6], (object[])yDataList[4], SeriesChartType.Line);
            formr.DataToChart(chart1, chart1.Series[3], (object[])yDataList[6], (object[])yDataList[5], SeriesChartType.Line);
        }
        public DataSet getData()
        {
            //打开文件
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls";
            file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.Cancel)
                return null;
            //判断文件后缀
            var path = file.FileName;
            string fileSuffix = System.IO.Path.GetExtension(path);
            if (string.IsNullOrEmpty(fileSuffix))
                return null;
            using (DataSet ds = new DataSet())
            {
                //判断Excel文件是2003版本还是2007版本
                string connString = "";
                if (fileSuffix == ".xls")
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
                else
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
                //读取文件
                string sql_select = "SELECT * FROM [parameters$]";
                using (OleDbConnection conn = new OleDbConnection(connString))
                using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql_select, conn))
                {
                    conn.Open();
                    cmd.Fill(ds);
                }
                if (ds == null || ds.Tables.Count <= 0) return null;
                return ds;
            }
        }

    }
}
