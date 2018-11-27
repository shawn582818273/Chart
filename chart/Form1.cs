using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections;
using DryDump;

namespace chart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string constr = "server=localhost;User Id=root;password=123456;Database=pfh";
            MySqlConnection mycon = new MySqlConnection(constr);
            mycon.Open();
            string sql = "select * from parameters";
            MySqlDataAdapter mda = new MySqlDataAdapter(sql, mycon);
            DataSet ds = new DataSet();
            mda.Fill(ds, "table1");
            //MessageBox.Show("hello");
            DataTable dt = ds.Tables["table1"];
            //this.dataGridView1.DataSource = dt;
            //Console.ReadLine();
            mycon.Close();

            ArrayList a1 = new ArrayList();
            ArrayList a2 = new ArrayList();
            Series ser1 = new Series();
            Series ser2 = new Series();
            Series ser3 = new Series();
            //int[] yData = null;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    a1.Add(dr["id"]);
            //    a2.Add(dr["age"]);
            //}
            //Console.WriteLine(a1[0]);
            //yData = (int[])a1.ToArray(typeof(int));
            //double[] yData = new double[5]{ 1.2,2.2,6.77,9.90,12};;
            //object[] yData = (object[])a1.ToArray(typeof(object));
            //a1.CopyTo(yData);
            //Object[] yData2= (Object[])a2.ToArray(typeof(Object));
            //this.chart1.Series["ser1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            //this.chart1.Series["ser2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            //this.chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
            //this.chart1.Series[0]["PieLineColor"] = "RED";//绘制黑色的连线。
            //this.chart1.Series["Series1"].Points.DataBindXY(xData, yData);
            //chart1.Series["Series1"]["PieDrawingStyle"] = "Default";
            //this.chart1.DataSource = dt;//绑定数据
            //this.chart1.Series[0].Points.DataBindY(yData);
            //this.chart1.Series[1].Points.DataBindY(yData2);

            FormRelated formr = new FormRelated();
            FormatRelated formatr = new FormatRelated();
            ArrayList yDataList =formr.DataTableToArrayList(dt);           
            formr.DataToChart(chart1,chart1.Series[0], (object[])yDataList[6], (object[])yDataList[2], SeriesChartType.Line);
            formr.DataToChart(chart1,chart1.Series[1], (object[])yDataList[6], (object[])yDataList[3], SeriesChartType.Line);
            formr.DataToChart(chart1,chart1.Series[2], (object[])yDataList[6], (object[])yDataList[4], SeriesChartType.Line);

        }

    }
}
