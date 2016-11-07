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
            this.writtentxb = new System.Windows.Forms.TextBox();
            this.displaybox = new System.Windows.Forms.Label();
            this.btnSwitch = new System.Windows.Forms.CheckBox();
            this.btn_analyze = new System.Windows.Forms.Button();
            this.displayPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // writtentxb
            // 
            resources.ApplyResources(this.writtentxb, "writtentxb");
            this.writtentxb.Name = "writtentxb";
            this.writtentxb.TextChanged += new System.EventHandler(this.writtentxb_TextChanged);
            // 
            // displaybox
            // 
            this.displaybox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.displaybox, "displaybox");
            this.displaybox.Name = "displaybox";
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
            this.displayPanel.Name = "displayPanel";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.displayPanel);
            this.Controls.Add(this.btn_analyze);
            this.Controls.Add(this.btnSwitch);
            this.Controls.Add(this.displaybox);
            this.Controls.Add(this.writtentxb);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox writtentxb;
        private System.Windows.Forms.Label displaybox;
        private System.Windows.Forms.CheckBox btnSwitch;
        private System.Windows.Forms.Button btn_analyze;
        private System.Windows.Forms.FlowLayoutPanel displayPanel;
    }
}

