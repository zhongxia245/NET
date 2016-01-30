using CollectApp.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace CollectApp
{
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();
            //不显示默认的字段
            dgv_Company.AutoGenerateColumns = false;
        }

        /// <summary>
        /// 第一期采集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_one_Click(object sender, EventArgs e)
        {
            DateTime startTime = DateTime.Now;
            string result = Core.GetAllCompanyName(txt_url.Text);
            txt_result.Text = result;

            DateTime endTime = DateTime.Now;
            TimeSpan ts = endTime.Subtract(startTime);
            double time = ts.TotalMilliseconds / 1000;
            MessageBox.Show(String.Format("采集总共时间：{0} 秒", time));
        }

        /// <summary>
        /// 第二期采集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_two_Click(object sender, EventArgs e)
        {
            DateTime startTime = DateTime.Now;
            DataTable result = Core.GetAllCompanyName_DataTable(txt_url.Text);
            dgv_Company.DataSource = result;

            DateTime endTime = DateTime.Now;
            TimeSpan ts = endTime.Subtract(startTime);
            double time = ts.TotalMilliseconds / 1000;
            MessageBox.Show(String.Format("采集总共时间：{0} 秒", time));
        }

        /// <summary>
        /// 第三期采集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_three_Click(object sender, EventArgs e)
        {

        }
    }
}
