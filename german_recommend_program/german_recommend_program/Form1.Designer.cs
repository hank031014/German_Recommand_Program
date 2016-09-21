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
            this.writtentxb = new System.Windows.Forms.TextBox();
            this.displaybox = new System.Windows.Forms.Label();
            this.btnSwitch = new System.Windows.Forms.CheckBox();
            this.btn_analyze = new System.Windows.Forms.Button();
            this.displayPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // writtentxb
            // 
            this.writtentxb.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.writtentxb.Location = new System.Drawing.Point(179, 203);
            this.writtentxb.Multiline = true;
            this.writtentxb.Name = "writtentxb";
            this.writtentxb.Size = new System.Drawing.Size(675, 123);
            this.writtentxb.TabIndex = 0;
            this.writtentxb.TextChanged += new System.EventHandler(this.writtentxb_TextChanged);
            // 
            // displaybox
            // 
            this.displaybox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.displaybox.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.displaybox.Location = new System.Drawing.Point(565, 22);
            this.displaybox.Name = "displaybox";
            this.displaybox.Size = new System.Drawing.Size(291, 152);
            this.displaybox.TabIndex = 1;
            // 
            // btnSwitch
            // 
            this.btnSwitch.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnSwitch.AutoSize = true;
            this.btnSwitch.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSwitch.Location = new System.Drawing.Point(38, 164);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(83, 30);
            this.btnSwitch.TabIndex = 2;
            this.btnSwitch.Text = "一般模式";
            this.btnSwitch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // btn_analyze
            // 
            this.btn_analyze.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_analyze.Location = new System.Drawing.Point(38, 203);
            this.btn_analyze.Name = "btn_analyze";
            this.btn_analyze.Size = new System.Drawing.Size(83, 30);
            this.btn_analyze.TabIndex = 3;
            this.btn_analyze.Text = "分析";
            this.btn_analyze.UseVisualStyleBackColor = true;
            this.btn_analyze.Click += new System.EventHandler(this.btn_analyze_Click);
            // 
            // displayPanel
            // 
            this.displayPanel.Location = new System.Drawing.Point(179, 22);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(365, 152);
            this.displayPanel.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 356);
            this.Controls.Add(this.displayPanel);
            this.Controls.Add(this.btn_analyze);
            this.Controls.Add(this.btnSwitch);
            this.Controls.Add(this.displaybox);
            this.Controls.Add(this.writtentxb);
            this.Name = "Form1";
            this.Text = "Write in  German";
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

