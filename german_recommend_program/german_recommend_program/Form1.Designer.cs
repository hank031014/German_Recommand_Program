namespace german_recommend_program
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnSwitch = new System.Windows.Forms.CheckBox();
            this.btn_analyze = new System.Windows.Forms.Button();
            this.displayPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_big_ae = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_small_ae = new System.Windows.Forms.Button();
            this.btn_big_oe = new System.Windows.Forms.Button();
            this.btn_small_oe = new System.Windows.Forms.Button();
            this.btn_big_ue = new System.Windows.Forms.Button();
            this.btn_small_ue = new System.Windows.Forms.Button();
            this.btn_ss = new System.Windows.Forms.Button();
            this.lb_de = new System.Windows.Forms.Label();
            this.btn_dict = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.writtentxb = new System.Windows.Forms.RichTextBox();
            this.btn_about = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSwitch
            // 
            resources.ApplyResources(this.btnSwitch, "btnSwitch");
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // btn_analyze
            // 
            resources.ApplyResources(this.btn_analyze, "btn_analyze");
            this.btn_analyze.Name = "btn_analyze";
            this.btn_analyze.UseVisualStyleBackColor = true;
            this.btn_analyze.Click += new System.EventHandler(this.btn_analyze_Click);
            // 
            // displayPanel
            // 
            resources.ApplyResources(this.displayPanel, "displayPanel");
            this.displayPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.displayPanel.Name = "displayPanel";
            // 
            // btn_big_ae
            // 
            resources.ApplyResources(this.btn_big_ae, "btn_big_ae");
            this.btn_big_ae.Name = "btn_big_ae";
            this.btn_big_ae.UseVisualStyleBackColor = true;
            this.btn_big_ae.Click += new System.EventHandler(this.btn_big_ae_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.btn_big_ae);
            this.flowLayoutPanel1.Controls.Add(this.btn_small_ae);
            this.flowLayoutPanel1.Controls.Add(this.btn_big_oe);
            this.flowLayoutPanel1.Controls.Add(this.btn_small_oe);
            this.flowLayoutPanel1.Controls.Add(this.btn_big_ue);
            this.flowLayoutPanel1.Controls.Add(this.btn_small_ue);
            this.flowLayoutPanel1.Controls.Add(this.btn_ss);
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Tag = "";
            // 
            // btn_small_ae
            // 
            resources.ApplyResources(this.btn_small_ae, "btn_small_ae");
            this.btn_small_ae.Name = "btn_small_ae";
            this.btn_small_ae.UseVisualStyleBackColor = true;
            this.btn_small_ae.Click += new System.EventHandler(this.btn_small_ae_Click);
            // 
            // btn_big_oe
            // 
            resources.ApplyResources(this.btn_big_oe, "btn_big_oe");
            this.btn_big_oe.Name = "btn_big_oe";
            this.btn_big_oe.UseVisualStyleBackColor = true;
            this.btn_big_oe.Click += new System.EventHandler(this.btn_big_oe_Click);
            // 
            // btn_small_oe
            // 
            resources.ApplyResources(this.btn_small_oe, "btn_small_oe");
            this.btn_small_oe.Name = "btn_small_oe";
            this.btn_small_oe.UseVisualStyleBackColor = true;
            this.btn_small_oe.Click += new System.EventHandler(this.btn_small_oe_Click);
            // 
            // btn_big_ue
            // 
            resources.ApplyResources(this.btn_big_ue, "btn_big_ue");
            this.btn_big_ue.Name = "btn_big_ue";
            this.btn_big_ue.UseVisualStyleBackColor = true;
            this.btn_big_ue.Click += new System.EventHandler(this.btn_big_ue_Click);
            // 
            // btn_small_ue
            // 
            resources.ApplyResources(this.btn_small_ue, "btn_small_ue");
            this.btn_small_ue.Name = "btn_small_ue";
            this.btn_small_ue.UseVisualStyleBackColor = true;
            this.btn_small_ue.Click += new System.EventHandler(this.btn_small_ue_Click);
            // 
            // btn_ss
            // 
            resources.ApplyResources(this.btn_ss, "btn_ss");
            this.btn_ss.Name = "btn_ss";
            this.btn_ss.UseVisualStyleBackColor = true;
            this.btn_ss.Click += new System.EventHandler(this.btn_ss_Click);
            // 
            // lb_de
            // 
            this.lb_de.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lb_de, "lb_de");
            this.lb_de.Name = "lb_de";
            // 
            // btn_dict
            // 
            resources.ApplyResources(this.btn_dict, "btn_dict");
            this.btn_dict.Name = "btn_dict";
            this.btn_dict.UseVisualStyleBackColor = true;
            this.btn_dict.Click += new System.EventHandler(this.btn_dict_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            resources.ApplyResources(this.listBox1, "listBox1");
            this.listBox1.Name = "listBox1";
            this.listBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyUp);
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // writtentxb
            // 
            resources.ApplyResources(this.writtentxb, "writtentxb");
            this.writtentxb.Name = "writtentxb";
            this.writtentxb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.writtentxb_KeyUp);
            // 
            // btn_about
            // 
            resources.ApplyResources(this.btn_about, "btn_about");
            this.btn_about.Name = "btn_about";
            this.btn_about.UseVisualStyleBackColor = true;
            this.btn_about.Click += new System.EventHandler(this.btn_about_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_about);
            this.Controls.Add(this.writtentxb);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btn_dict);
            this.Controls.Add(this.lb_de);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.displayPanel);
            this.Controls.Add(this.btn_analyze);
            this.Controls.Add(this.btnSwitch);
            this.Name = "Form1";
            this.Tag = "";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox btnSwitch;
        private System.Windows.Forms.Button btn_analyze;
        private System.Windows.Forms.FlowLayoutPanel displayPanel;
        private System.Windows.Forms.Button btn_big_ae;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn_small_ae;
        private System.Windows.Forms.Button btn_big_oe;
        private System.Windows.Forms.Button btn_small_oe;
        private System.Windows.Forms.Button btn_big_ue;
        private System.Windows.Forms.Button btn_small_ue;
        private System.Windows.Forms.Button btn_ss;
        private System.Windows.Forms.Label lb_de;
        private System.Windows.Forms.Button btn_dict;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.RichTextBox writtentxb;
        private System.Windows.Forms.Button btn_about;
    }
}

