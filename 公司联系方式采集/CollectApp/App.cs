using CollectApp.common;
using CollectApp.controls;
using CollectApp.services;
using HFSoft.Component.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CollectApp
{
    public partial class App : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public App()
        {
            InitializeComponent();
            init();
        }

        #region 0.变量
        //导出路径默认值
        string defaultfilePath = "";
        //加载的状态
        StringBuilder sb_loadingState = new StringBuilder();
        //共抓取多少条记录
        int totalRecord = 0;
        /// <summary>
        /// 正在加载中
        /// </summary>
        OpaqueCommand cmd = new OpaqueCommand();
        #endregion

        #region 1.基本方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void init()
        {

        }

        /// <summary>
        /// 把所有表格合并在一起
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private DataTable DataHandler(DataSet ds)
        {

            DataTable dt = ds.Tables[0].Clone();

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                dt.Merge(ds.Tables[i]);
            }
            return dt;
        }


        /// <summary>
        /// 设置按钮是否可用,定时器是否开启，loading是否显示
        /// </summary>
        /// <param name="p">是否可用</param>
        /// <param name="isSingleThread">是否为单线程采集</param>
        private void SetButtonEnable(bool p)
        {
            timer1.Enabled = !p;

            btn_start.Enabled = p;
            if (!p)
            {
                //cmd.ShowOpaqueLayer(panel1, 125, true);
            }
            else
            {
                //cmd.HideOpaqueLayer();
            }
        }

        #endregion

        #region 2.事件处理

        /// <summary>
        /// 文本域自动滚动到最下面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            txt_result.SelectionStart = txt_result.Text.Length;

            txt_result.ScrollToCaret();
        }

        private void App_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("是否停止采集并退出程序？", "温馨提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                System.Environment.Exit(0);
            }
        }

        #endregion

        /// <summary>
        /// 采集指定类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="caie"></param>
        private void btn_start_Click(object sender, EventArgs e)
        {
            if (txt_start.Text.Length != 0 & txt_end.Text.Length != 0)
            {
                Log.Info(String.Format("采集公司信息，范围{0}~{1}==>开始时间{2}", txt_start.Text, txt_end.Text, DateTime.Now.ToString()));
                SingleThreadCollect();
            }
            else
            {
                MessageBox.Show("必须设置采集的范围");
            }
        }

        /// <summary>
        /// 单线程抓取[用定时器来判断是否正在加载]
        /// </summary>
        /// <param name="floder"></param>
        private void SingleThreadCollect()
        {
            int start = Int32.Parse(txt_start.Text ?? "-1");
            int end = Int32.Parse(txt_end.Text ?? "0");

            if (start != -1 && end != 0)
            {
                //获取路径
                FolderBrowserDialog fbd = new FolderBrowserDialog();

                //导出路径，使用上次选中的值
                if (defaultfilePath != "")
                {
                    fbd.SelectedPath = defaultfilePath;
                }

                //开始抓取
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    defaultfilePath = fbd.SelectedPath;
                    SetButtonEnable(false);

                    S_SingleThreadConfig.floderPath = defaultfilePath;
                    S_SingleThreadConfig.Start = start;
                    S_SingleThreadConfig.End = end;
                    //开始遍历采集
                    ThreadStart threadStart = new ThreadStart(S_SingleThreadConfig.Collect2Export);
                    Thread thread = new Thread(threadStart);
                    thread.Start();

                }
            }
            else
            {
                MessageBox.Show("请选中需要采集的类别，或者点击全部采集！", "温馨提示:");
            }
        }

        /// <summary>
        /// 判断是否采集结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (S_SingleThreadConfig.CurrentIndex == S_SingleThreadConfig.End)
            {
                SetButtonEnable(true);

                sb_loadingState.AppendLine(String.Format("Time:{0}==>采集结束！采集到的数据{1}", DateTime.Now.ToString("HH:mm:ss"), S_SingleThreadConfig.CollectDt.Rows.Count));
                txt_result.Text = sb_loadingState.ToString();

                MessageBox.Show(String.Format("共抓取数据: {0} 条！",S_SingleThreadConfig.CollectDt.Rows.Count));

            }
            else
            {
                sb_loadingState.AppendLine(String.Format("Time:{0}==>采集页面:contact-us-{1}.html", DateTime.Now.ToString("HH:mm:ss"), S_SingleThreadConfig.CurrentIndex));
                if (sb_loadingState.Length > 1024) sb_loadingState = new StringBuilder();
                txt_result.Text = sb_loadingState.ToString();
            }
        }
    }
}