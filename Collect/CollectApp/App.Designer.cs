namespace CollectApp
{
    partial class App
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            this.dgv_Company = new System.Windows.Forms.DataGridView();
            this.cn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.en = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab_all = new System.Windows.Forms.TabControl();
            this.tab_one = new System.Windows.Forms.TabPage();
            this.flp1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tab_two = new System.Windows.Forms.TabPage();
            this.flp2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tab_three = new System.Windows.Forms.TabPage();
            this.flp3 = new System.Windows.Forms.FlowLayoutPanel();
            this.cb_all = new System.Windows.Forms.CheckBox();
            this.btn_collect_export = new System.Windows.Forms.Button();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.btn_single = new System.Windows.Forms.Button();
            this.btn_all = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btn_Single_All = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Company)).BeginInit();
            this.tab_all.SuspendLayout();
            this.tab_one.SuspendLayout();
            this.tab_two.SuspendLayout();
            this.tab_three.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_Company
            // 
            this.dgv_Company.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Company.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cn,
            this.en});
            resources.ApplyResources(this.dgv_Company, "dgv_Company");
            this.dgv_Company.Name = "dgv_Company";
            this.dgv_Company.RowTemplate.Height = 23;
            // 
            // cn
            // 
            this.cn.DataPropertyName = "cn";
            resources.ApplyResources(this.cn, "cn");
            this.cn.Name = "cn";
            // 
            // en
            // 
            this.en.DataPropertyName = "en";
            resources.ApplyResources(this.en, "en");
            this.en.Name = "en";
            // 
            // tab_all
            // 
            resources.ApplyResources(this.tab_all, "tab_all");
            this.tab_all.Controls.Add(this.tab_one);
            this.tab_all.Controls.Add(this.tab_two);
            this.tab_all.Controls.Add(this.tab_three);
            this.tab_all.Name = "tab_all";
            this.tab_all.SelectedIndex = 0;
            // 
            // tab_one
            // 
            this.tab_one.Controls.Add(this.flp1);
            resources.ApplyResources(this.tab_one, "tab_one");
            this.tab_one.Name = "tab_one";
            this.tab_one.UseVisualStyleBackColor = true;
            // 
            // flp1
            // 
            resources.ApplyResources(this.flp1, "flp1");
            this.flp1.Name = "flp1";
            // 
            // tab_two
            // 
            this.tab_two.Controls.Add(this.flp2);
            resources.ApplyResources(this.tab_two, "tab_two");
            this.tab_two.Name = "tab_two";
            this.tab_two.UseVisualStyleBackColor = true;
            // 
            // flp2
            // 
            resources.ApplyResources(this.flp2, "flp2");
            this.flp2.Name = "flp2";
            // 
            // tab_three
            // 
            this.tab_three.Controls.Add(this.flp3);
            resources.ApplyResources(this.tab_three, "tab_three");
            this.tab_three.Name = "tab_three";
            this.tab_three.UseVisualStyleBackColor = true;
            // 
            // flp3
            // 
            resources.ApplyResources(this.flp3, "flp3");
            this.flp3.Name = "flp3";
            // 
            // cb_all
            // 
            resources.ApplyResources(this.cb_all, "cb_all");
            this.cb_all.Name = "cb_all";
            this.cb_all.UseVisualStyleBackColor = true;
            this.cb_all.CheckedChanged += new System.EventHandler(this.cb_all_CheckedChanged);
            // 
            // btn_collect_export
            // 
            resources.ApplyResources(this.btn_collect_export, "btn_collect_export");
            this.btn_collect_export.Name = "btn_collect_export";
            this.btn_collect_export.UseVisualStyleBackColor = false;
            this.btn_collect_export.Click += new System.EventHandler(this.btn_collect_export_Click);
            // 
            // txt_result
            // 
            resources.ApplyResources(this.txt_result, "txt_result");
            this.txt_result.Name = "txt_result";
            this.txt_result.TextChanged += new System.EventHandler(this.txt_result_TextChanged);
            // 
            // btn_single
            // 
            resources.ApplyResources(this.btn_single, "btn_single");
            this.btn_single.Name = "btn_single";
            this.btn_single.UseVisualStyleBackColor = true;
            this.btn_single.Click += new System.EventHandler(this.btn_single_Click);
            // 
            // btn_all
            // 
            resources.ApplyResources(this.btn_all, "btn_all");
            this.btn_all.Name = "btn_all";
            this.btn_all.UseVisualStyleBackColor = true;
            this.btn_all.Click += new System.EventHandler(this.btn_all_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tab_all);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btn_Single_All
            // 
            resources.ApplyResources(this.btn_Single_All, "btn_Single_All");
            this.btn_Single_All.Name = "btn_Single_All";
            this.btn_Single_All.UseVisualStyleBackColor = false;
            this.btn_Single_All.Click += new System.EventHandler(this.btn_Single_All_Click);
            // 
            // App
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Single_All);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_all);
            this.Controls.Add(this.btn_single);
            this.Controls.Add(this.txt_result);
            this.Controls.Add(this.btn_collect_export);
            this.Controls.Add(this.cb_all);
            this.Controls.Add(this.dgv_Company);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "App";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.App_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Company)).EndInit();
            this.tab_all.ResumeLayout(false);
            this.tab_one.ResumeLayout(false);
            this.tab_two.ResumeLayout(false);
            this.tab_three.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Company;
        private System.Windows.Forms.DataGridViewTextBoxColumn cn;
        private System.Windows.Forms.DataGridViewTextBoxColumn en;
        private System.Windows.Forms.TabControl tab_all;
        private System.Windows.Forms.TabPage tab_two;
        private System.Windows.Forms.TabPage tab_three;
        private System.Windows.Forms.TabPage tab_one;
        private System.Windows.Forms.FlowLayoutPanel flp1;
        private System.Windows.Forms.FlowLayoutPanel flp2;
        private System.Windows.Forms.FlowLayoutPanel flp3;
        private System.Windows.Forms.CheckBox cb_all;
        private System.Windows.Forms.Button btn_collect_export;
        private System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.Button btn_single;
        private System.Windows.Forms.Button btn_all;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button btn_Single_All;
    }
}

