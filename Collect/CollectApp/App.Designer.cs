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
            this.btn_one = new System.Windows.Forms.Button();
            this.btn_two = new System.Windows.Forms.Button();
            this.btn_three = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_url = new System.Windows.Forms.TextBox();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.dgv_Company = new System.Windows.Forms.DataGridView();
            this.cn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.en = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Company)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_one
            // 
            this.btn_one.Location = new System.Drawing.Point(20, 58);
            this.btn_one.Name = "btn_one";
            this.btn_one.Size = new System.Drawing.Size(75, 23);
            this.btn_one.TabIndex = 0;
            this.btn_one.Text = "第一期";
            this.btn_one.UseVisualStyleBackColor = true;
            this.btn_one.Click += new System.EventHandler(this.btn_one_Click);
            // 
            // btn_two
            // 
            this.btn_two.Location = new System.Drawing.Point(105, 58);
            this.btn_two.Name = "btn_two";
            this.btn_two.Size = new System.Drawing.Size(75, 23);
            this.btn_two.TabIndex = 1;
            this.btn_two.Text = "第二期";
            this.btn_two.UseVisualStyleBackColor = true;
            this.btn_two.Click += new System.EventHandler(this.btn_two_Click);
            // 
            // btn_three
            // 
            this.btn_three.Location = new System.Drawing.Point(197, 58);
            this.btn_three.Name = "btn_three";
            this.btn_three.Size = new System.Drawing.Size(75, 23);
            this.btn_three.TabIndex = 2;
            this.btn_three.Text = "第三期";
            this.btn_three.UseVisualStyleBackColor = true;
            this.btn_three.Click += new System.EventHandler(this.btn_three_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "采集地址";
            // 
            // txt_url
            // 
            this.txt_url.Location = new System.Drawing.Point(112, 12);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(334, 21);
            this.txt_url.TabIndex = 4;
            this.txt_url.Text = "http://i.cantonfair.org.cn/cn/expexhibitorlist.aspx?categoryno=402";
            // 
            // txt_result
            // 
            this.txt_result.Location = new System.Drawing.Point(20, 96);
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_result.Size = new System.Drawing.Size(499, 243);
            this.txt_result.TabIndex = 5;
            // 
            // dgv_Company
            // 
            this.dgv_Company.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Company.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cn,
            this.en});
            this.dgv_Company.Location = new System.Drawing.Point(20, 346);
            this.dgv_Company.Name = "dgv_Company";
            this.dgv_Company.RowTemplate.Height = 23;
            this.dgv_Company.Size = new System.Drawing.Size(499, 258);
            this.dgv_Company.TabIndex = 6;
            // 
            // cn
            // 
            this.cn.DataPropertyName = "cn";
            this.cn.HeaderText = "中文公司名";
            this.cn.Name = "cn";
            // 
            // en
            // 
            this.en.DataPropertyName = "en";
            this.en.HeaderText = "英文公司名";
            this.en.Name = "en";
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 616);
            this.Controls.Add(this.dgv_Company);
            this.Controls.Add(this.txt_result);
            this.Controls.Add(this.txt_url);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_three);
            this.Controls.Add(this.btn_two);
            this.Controls.Add(this.btn_one);
            this.Name = "App";
            this.Text = "采集程序";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Company)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_one;
        private System.Windows.Forms.Button btn_two;
        private System.Windows.Forms.Button btn_three;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_url;
        private System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.DataGridView dgv_Company;
        private System.Windows.Forms.DataGridViewTextBoxColumn cn;
        private System.Windows.Forms.DataGridViewTextBoxColumn en;
    }
}

