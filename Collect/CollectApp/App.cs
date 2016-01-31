using CollectApp.common;
using CollectApp.config;
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
using System.Windows.Forms;

namespace CollectApp
{
    public partial class App : Form
    {   
        //导出路径默认值
        string defaultfilePath = "";  
        public App()
        {
            InitializeComponent();
            //不显示默认的字段
            dgv_Company.AutoGenerateColumns = false;
            init();
        }

        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_startCollect_Click(object sender, EventArgs e)
        {
            List<M_Category> list = new List<M_Category>();

            //选中第几期
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
                    if (cb.Checked)
                    {
                        M_Category category = new M_Category();
                        category.Title = cb.Text;
                        category.Param = cb.Tag.ToString();
                        category.TimePhase = tab_all.SelectedIndex + 1;
                        list.Add(category);
                    }
                }
            }
            foreach (M_Category item in list)
            {
                string url_cn_base = item.Param.IndexOf("areano") == -1 ? Config.BaseURL_CN : Config.BaseURL_CN_Imp;
                string url_en_base = item.Param.IndexOf("areano") == -1 ? Config.BaseURL_EN : Config.BaseURL_EN_Imp;
                var url_cn = String.Format("{0}?{1}", url_cn_base, item.Param);
                var url_en = String.Format("{0}?{1}", url_en_base, item.Param);
                LoadingHandler.Show(this, args =>
                {
                    DataTable result_cn = Core.GetAllCompanyName_DataTable(url_cn);
                    DataTable result_en = Core.GetAllCompanyName_DataTable(url_en);
                    DataTable dt = S_Core.MergeTableColumn(result_cn, result_en);

                    args.Execute(ex =>
                    {
                        dgv_Company.DataSource = dt;
                    });

                    MessageBox.Show(String.Format("总条数：{0}", dt.Rows.Count + 1));
                });
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_export_Click(object sender, EventArgs e)
        {
            string localFilePath = String.Empty;


            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            //设置文件类型  
            saveFileDialog1.Filter = " xls files(*.xls)|*.txt|All files(*.*)|*.*";
            //设置文件名称：
            saveFileDialog1.FileName = String.Format("{0}_采集结果.xls", DateTime.Now.ToString("yyyyMMddHH"));
            //保存对话框是否记忆上次打开的目录  
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //获得文件路径  
                localFilePath = saveFileDialog1.FileName.ToString();
                DataTable dt = (DataTable)dgv_Company.DataSource;

                bool flag = new ExcelHelper(localFilePath, "").DatatableToExcel(dt);
                if (flag)
                {
                    MessageBox.Show("导出成功!,导出数据条数: " + dt.Rows.Count + " 条");
                }
                else
                {
                    MessageBox.Show("导出失败！");
                }
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
                cb.Text = ht1[key].ToString();
                flp1.Controls.Add(cb);
            }

            foreach (var key in ht2.Keys)
            {
                CheckBox cb = new CheckBox();
                cb.Width = 140;
                cb.Tag = key;
                cb.Text = ht2[key].ToString();
                flp2.Controls.Add(cb);
            }

            foreach (var key in ht3.Keys)
            {
                CheckBox cb = new CheckBox();
                cb.Width = 140;
                cb.Tag = key;
                cb.Text = ht3[key].ToString();
                flp3.Controls.Add(cb);
            }

        }

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

        /// <summary>
        /// 采集并导出
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
            
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                defaultfilePath = fbd.SelectedPath;

                collectAndExport(fbd.SelectedPath);
            }
        }

        /// <summary>
        /// 采集并导出
        /// </summary>
        /// <param name="floder"></param>
        private void collectAndExport(string floder) {

            DataSet ds = new DataSet();
            List<M_Category> list = new List<M_Category>();

            //选中第几期
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
                    if (cb.Checked)
                    {
                        M_Category category = new M_Category();
                        category.Title = cb.Text;
                        category.Param = cb.Tag.ToString();
                        category.TimePhase = tab_all.SelectedIndex + 1;
                        list.Add(category);
                    }
                }
            }

            //遍历抓取数据
            foreach (M_Category item in list)
            {
                //区分进出口
                string url_cn_base = item.Param.IndexOf("areano") == -1 ? Config.BaseURL_CN : Config.BaseURL_CN_Imp;
                string url_en_base = item.Param.IndexOf("areano") == -1 ? Config.BaseURL_EN : Config.BaseURL_EN_Imp;
                var url_cn = String.Format("{0}?{1}", url_cn_base, item.Param);
                var url_en = String.Format("{0}?{1}", url_en_base, item.Param);
                
                LoadingHandler.Show(this, args =>
                {
                    DataTable result_cn = Core.GetAllCompanyName_DataTable(url_cn);
                    DataTable result_en = Core.GetAllCompanyName_DataTable(url_en);
                    DataTable dt = S_Core.MergeTableColumn(result_cn, result_en);

                    //添加到dataSet中
                    dt.TableName = item.Title;
                    ds.Tables.Add(dt);
                });
            }

            //导出 DataSet
            bool flag = new ExcelHelper(String.Format("{0}/第{1}期.xls", floder, tab_all.SelectedIndex + 1), "").DataSetToExcel(ds);
            if (!flag)
            {
                MessageBox.Show("导出失败！");
            }
        }
    }
}
