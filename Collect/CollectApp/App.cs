using CollectApp.common;
using CollectApp.config;
using CollectApp.controls;
using CollectApp.model;
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

        public App()
        {
            InitializeComponent();
            //不显示默认的字段
            dgv_Company.AutoGenerateColumns = false;
            init();
        }


        /// <summary>
        /// 定时器，判断是否采集结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (S_CollectThread.DS.Tables.Count == S_CollectThread.DataTableCount)
            {
                SetButtonEnable(true);

                sb_loadingState.AppendLine(String.Format("Time:{2}==>共采集{1}个类别，已经采集 {0} 个类别,采集结束！", S_CollectThread.DS.Tables.Count, S_CollectThread.DataTableCount, DateTime.Now.ToString("HH:mm:ss")));
                txt_result.Text = sb_loadingState.ToString();


                DataTable dt = DataHandler(S_CollectThread.DS);
                string flag = new ExcelHelper(String.Format("{0}/本次采集数据汇总表.xls", defaultfilePath), "").DatatableToExcel(dt);
                if (flag != "")
                {
                    MessageBox.Show("导出失败！" + flag);
                }

                MessageBox.Show(String.Format("共抓取数据: {0} 条！", dt.Rows.Count));

                //清空表格集合
                S_CollectThread.DS = new DataSet();

            }
            else
            {
                sb_loadingState.AppendLine(String.Format("Time:{2}==>共采集{1}个类别，已经采集 {0} 个类别", S_CollectThread.DS.Tables.Count, S_CollectThread.DataTableCount, DateTime.Now.ToString("HH:mm:ss")));
                if (sb_loadingState.Length > 1024) sb_loadingState = new StringBuilder();
                txt_result.Text = sb_loadingState.ToString();

            }
        }

        /// <summary>
        /// 初始化类别
        /// </summary>
        private void init()
        {
            Hashtable ht = Core.GetAll(Config.BaseURL_CN);
            Hashtable ht1 = (Hashtable)ht[1];
            Hashtable ht2 = (Hashtable)ht[2];
            Hashtable ht3 = (Hashtable)ht[3];

            foreach (var key in ht1.Keys)
            {
                CheckBox cb = new CheckBox();
                cb.Width = 140;
                cb.Tag = key;
                cb.Text = ht1[key].ToString().Replace("&nbsp;", "").Trim().Replace("、", "_");
                flp1.Controls.Add(cb);
            }

            foreach (var key in ht2.Keys)
            {
                CheckBox cb = new CheckBox();
                cb.Width = 140;
                cb.Tag = key;
                cb.Text = ht2[key].ToString().Replace("&nbsp;", "").Trim().Replace("、", "_");
                flp2.Controls.Add(cb);
            }

            foreach (var key in ht3.Keys)
            {
                CheckBox cb = new CheckBox();
                cb.Width = 140;
                cb.Tag = key;
                cb.Text = ht3[key].ToString().Replace("&nbsp;", "").Trim().Replace("、", "_");
                flp3.Controls.Add(cb);
            }
        }

        /// <summary>
        /// 全选或者反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            Control ctl = flp1;
            switch (tab_all.SelectedIndex)
            {
                case 0: ctl = flp1; break;
                case 1: ctl = flp2; break;
                case 2: ctl = flp3; break;
            }
            //获取所有选中的类别
            foreach (Control ct in ctl.Controls)
            {
                if (ct.GetType().ToString().Equals("System.Windows.Forms.CheckBox"))
                {
                    CheckBox cb = (CheckBox)ct;
                    cb.Checked = cb_all.Checked;
                }
            }
        }

        #region 单线程

        /// <summary>
        /// 采集并导出[方法]
        /// </summary>
        /// <param name="floder"></param>
        private void CollectAndExport(string floder)
        {

            DataSet ds = new DataSet();
            List<M_Category> list = GetCategory(tab_all.SelectedIndex);

            //遍历抓取数据
            foreach (M_Category item in list)
            {
                //区分进出口
                string url_cn_base = item.Param.IndexOf("areano") == -1 ? Config.BaseURL_CN : Config.BaseURL_CN_Imp;
                string url_en_base = item.Param.IndexOf("areano") == -1 ? Config.BaseURL_EN : Config.BaseURL_EN_Imp;
                var url_cn = String.Format("{0}?{1}", url_cn_base, item.Param);
                var url_en = String.Format("{0}?{1}", url_en_base, item.Param);

                LoadingHandler.Show(this, LoadingStyle.None, args =>
                {
                    args.Execute(ex =>
                    {
                        sb_loadingState.AppendLine(String.Format("正在抓取 {0} ... ", item.Title));
                        txt_result.Text = sb_loadingState.ToString();
                    });

                    DataTable result_cn = Core.GetAllCompanyName_DataTable(url_cn, item.TimePhase, item.Title);
                    DataTable result_en = Core.GetAllCompanyName_DataTable(url_en, item.TimePhase, item.Title);
                    DataTable dt = S_Core.MergeTableColumn(result_cn, result_en);

                    //添加到dataSet中
                    dt.TableName = item.Title;

                    totalRecord += dt.Rows.Count;

                    ds.Tables.Add(dt);

                    args.Execute(ex =>
                    {
                        sb_loadingState.AppendLine(String.Format("抓取 {0} 结束！ ", item.Title));
                        txt_result.Text = sb_loadingState.ToString();
                    });
                });
            }

            //导出 DataSet
            string flag = new ExcelHelper(String.Format("{0}/第{1}期.xls", floder, tab_all.SelectedIndex + 1), "").DataSetToExcel(ds);
            if (flag != "")
            {
                MessageBox.Show("导出失败！" + flag);
            }
            MessageBox.Show(String.Format("共抓取数据: {0} 条！", totalRecord));
        }


        /// <summary>
        /// 采集并导出[点击事件]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="caie"></param>
        private void btn_collect_export_Click(object sender, EventArgs e)
        {
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

                CollectAndExport(fbd.SelectedPath);
            }
        }

        #endregion

        #region 多线程

        /// <summary>
        /// 多线程采集数据，单个单个文件生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_single_Click(object sender, EventArgs e)
        {
            ThreadCollect(false);
        }

        /// <summary>
        /// 全部采集[多线程]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_all_Click(object sender, EventArgs e)
        {
            ThreadCollect(true);
        }

        /// <summary>
        /// 多线程抓取
        /// </summary>
        /// <param name="floder"></param>
        private void ThreadCollect(bool isExportAll = false)
        {
            //获取采集类别
            List<M_Category> list;
            if (isExportAll)
                list = GetAllCategory();
            else
                list = GetCategory(tab_all.SelectedIndex);

            if (list.Count > 0)
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

                    S_CollectThread.DataTableCount = list.Count;

                    //开始遍历采集
                    foreach (M_Category item in list)
                    {
                        S_CollectThread collect = new S_CollectThread(item, defaultfilePath);
                        ThreadStart threadStart = new ThreadStart(collect.run);
                        Thread thread = new Thread(threadStart);
                        thread.Start();

                    }
                }
            }
            else
            {
                MessageBox.Show("请选中需要采集的类别，或者点击全部采集！","温馨提示:");
            }

            #region 死循环判断是否全部加在完成[目前不用]

            //StringBuilder sb = new StringBuilder();
            //LoadingHandler.Show(this, args =>
            //{
            //    //判断所有线程是否全部完成
            //    while (true)
            //    {
            //        args.Execute(ex =>
            //        {
            //            sb.AppendLine(String.Format("Time:{2}==>共采集{1}个类别，已经采集 {0} 个类别", S_CollectThread.DS.Tables.Count, S_CollectThread.DataTableCount, DateTime.Now.ToString("HH:mm:ss")));
            //            if (sb.Length > 1024) sb = new StringBuilder();
            //            txt_result.Text = sb.ToString();
            //        });
            //        Thread.Sleep(1000);
            //        if (S_CollectThread.DS.Tables.Count == S_CollectThread.DataTableCount)
            //        {
            //            DataTable dt = DataHandler(S_CollectThread.DS);
            //            string flag = new ExcelHelper(String.Format("{0}/所有类别数据.xls", floder), "").DatatableToExcel(dt);
            //            if (flag != "")
            //            {
            //                MessageBox.Show("导出失败！" + flag);
            //            }

            //            MessageBox.Show(String.Format("共抓取数据: {0} 条！", dt.Rows.Count));

            //            //清空表格集合
            //            S_CollectThread.DS = new DataSet();

            //            args.Execute(ex =>
            //            {
            //                sb.AppendLine(String.Format("Time:{2}==>共采集{1}个类别，采集结束！", S_CollectThread.DS.Tables.Count, S_CollectThread.DataTableCount, DateTime.Now.ToString("HH:mm:ss")));
            //                if (sb.Length > 1024) sb = new StringBuilder();
            //                txt_result.Text = sb.ToString();
            //            });

            //            break;
            //        }
            //    }
            //});

            #endregion

        }




        #endregion

        #region 基本方法

        /// <summary>
        /// 获取类别
        /// </summary>
        /// <returns></returns>
        private List<M_Category> GetCategory(int selectedIndex, bool isAll = false)
        {
            List<M_Category> list = new List<M_Category>();

            //选中第几期
            Control ctl = flp1;
            switch (selectedIndex)
            {
                case 0: ctl = flp1; break;
                case 1: ctl = flp2; break;
                case 2: ctl = flp3; break;
            }

            //获取所有选中的类别
            foreach (Control ct in ctl.Controls)
            {
                if (ct.GetType().ToString().Equals("System.Windows.Forms.CheckBox"))
                {
                    CheckBox cb = (CheckBox)ct;
                    if (isAll)
                    {
                        M_Category category = new M_Category();
                        category.Title = cb.Text;
                        category.Param = cb.Tag.ToString();
                        category.TimePhase = selectedIndex + 1;
                        list.Add(category);
                    }
                    else
                    {
                        if (cb.Checked)
                        {
                            M_Category category = new M_Category();
                            category.Title = cb.Text;
                            category.Param = cb.Tag.ToString();
                            category.TimePhase = selectedIndex + 1;
                            list.Add(category);
                        }
                    }

                }
            }
            return list;
        }

        /// <summary>
        /// 所有类别
        /// </summary>
        /// <returns></returns>
        private List<M_Category> GetAllCategory()
        {
            List<M_Category> list = new List<M_Category>();
            list.AddRange(GetCategory(0, true));
            list.AddRange(GetCategory(1, true));
            list.AddRange(GetCategory(2, true));
            return list;
        }

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
        /// <param name="p"></param>
        private void SetButtonEnable(bool p)
        {
            timer1.Enabled = !p;
            btn_single.Enabled = p;
            btn_all.Enabled = p;
            if (!p)
            {
                cmd.ShowOpaqueLayer(panel1, 125, true);
            }
            else
            {
                cmd.HideOpaqueLayer();
            }

        }

        #endregion

        private void App_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("是否停止采集并退出程序？", "温馨提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                System.Environment.Exit(0);
            }
        }
    }
}