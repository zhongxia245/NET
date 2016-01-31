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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            this.dgv_Company = new System.Windows.Forms.DataGridView();
            this.cn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.en = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab_all = new System.Windows.Forms.TabControl();
            this.tab_two = new System.Windows.Forms.TabPage();
            this.btn_startCollect = new System.Windows.Forms.Button();
            this.tab_three = new System.Windows.Forms.TabPage();
            this.tab_one = new System.Windows.Forms.TabPage();
            this.btn_export = new System.Windows.Forms.Button();
            this.flp1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flp2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flp3 = new System.Windows.Forms.FlowLayoutPanel();
            this.cb_all = new System.Windows.Forms.CheckBox();
            this.btn_collect_export = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Company)).BeginInit();
            this.tab_all.SuspendLayout();
            this.tab_two.SuspendLayout();
            this.tab_three.SuspendLayout();
            this.tab_one.SuspendLayout();
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
            // tab_two
            // 
            this.tab_two.Controls.Add(this.flp2);
            resources.ApplyResources(this.tab_two, "tab_two");
            this.tab_two.Name = "tab_two";
            this.tab_two.UseVisualStyleBackColor = true;
            // 
            // btn_startCollect
            // 
            resources.ApplyResources(this.btn_startCollect, "btn_startCollect");
            this.btn_startCollect.Name = "btn_startCollect";
            this.btn_startCollect.UseVisualStyleBackColor = true;
            this.btn_startCollect.Click += new System.EventHandler(this.btn_startCollect_Click);
            // 
            // tab_three
            // 
            this.tab_three.Controls.Add(this.flp3);
            resources.ApplyResources(this.tab_three, "tab_three");
            this.tab_three.Name = "tab_three";
            this.tab_three.UseVisualStyleBackColor = true;
            // 
            // tab_one
            // 
            this.tab_one.Controls.Add(this.flp1);
            resources.ApplyResources(this.tab_one, "tab_one");
            this.tab_one.Name = "tab_one";
            this.tab_one.UseVisualStyleBackColor = true;
            // 
            // btn_export
            // 
            resources.ApplyResources(this.btn_export, "btn_export");
            this.btn_export.Name = "btn_export";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // flp1
            // 
            resources.ApplyResources(this.flp1, "flp1");
            this.flp1.Name = "flp1";
            // 
            // flp2
            // 
            resources.ApplyResources(this.flp2, "flp2");
            this.flp2.Name = "flp2";
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
            this.btn_collect_export.UseVisualStyleBackColor = true;
            this.btn_collect_export.Click += new System.EventHandler(this.btn_collect_export_Click);
            // 
            // App
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_collect_export);
            this.Controls.Add(this.cb_all);
            this.Controls.Add(this.btn_export);
            this.Controls.Add(this.btn_startCollect);
            this.Controls.Add(this.tab_all);
            this.Controls.Add(this.dgv_Company);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "App";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Company)).EndInit();
            this.tab_all.ResumeLayout(false);
            this.tab_two.ResumeLayout(false);
            this.tab_three.ResumeLayout(false);
            this.tab_one.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Company;
        private System.Windows.Forms.DataGridViewTextBoxColumn cn;
        private System.Windows.Forms.DataGridViewTextBoxColumn en;
        private System.Windows.Forms.TabControl tab_all;
        private System.Windows.Forms.TabPage tab_two;
        private System.Windows.Forms.Button btn_startCollect;
        private System.Windows.Forms.TabPage tab_three;
        private System.Windows.Forms.TabPage tab_one;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.FlowLayoutPanel flp1;
        private System.Windows.Forms.FlowLayoutPanel flp2;
        private System.Windows.Forms.FlowLayoutPanel flp3;
        private System.Windows.Forms.CheckBox cb_all;
        private System.Windows.Forms.Button btn_collect_export;
    }
}

