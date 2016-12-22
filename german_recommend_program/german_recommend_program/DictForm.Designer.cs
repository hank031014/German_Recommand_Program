namespace german_recommend_program
{
    partial class DictForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_search = new System.Windows.Forms.Button();
            this.lb_result = new System.Windows.Forms.ListBox();
            this.lb_word = new System.Windows.Forms.Label();
            this.rtb_search = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lb_pron = new System.Windows.Forms.Label();
            this.lb_prep = new System.Windows.Forms.Label();
            this.lb_conj = new System.Windows.Forms.Label();
            this.lb_v_p = new System.Windows.Forms.Label();
            this.lb_v_pp = new System.Windows.Forms.Label();
            this.lb_v_ping = new System.Windows.Forms.Label();
            this.lb_v_pt = new System.Windows.Forms.Label();
            this.lb_v_part = new System.Windows.Forms.Label();
            this.lb_v_ppv = new System.Windows.Forms.Label();
            this.lb_v_sich = new System.Windows.Forms.Label();
            this.lb_v_give = new System.Windows.Forms.Label();
            this.lb_v_vt = new System.Windows.Forms.Label();
            this.lb_adj_super = new System.Windows.Forms.Label();
            this.lb_adj_comp = new System.Windows.Forms.Label();
            this.lb_n_gender = new System.Windows.Forms.Label();
            this.lb_n_pural = new System.Windows.Forms.Label();
            this.lb_english = new System.Windows.Forms.Label();
            this.lb_pos = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_id = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_search
            // 
            this.btn_search.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_search.Location = new System.Drawing.Point(186, 25);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(90, 32);
            this.btn_search.TabIndex = 2;
            this.btn_search.Text = "搜尋單字";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // lb_result
            // 
            this.lb_result.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_result.FormattingEnabled = true;
            this.lb_result.ItemHeight = 19;
            this.lb_result.Location = new System.Drawing.Point(22, 80);
            this.lb_result.Name = "lb_result";
            this.lb_result.Size = new System.Drawing.Size(136, 175);
            this.lb_result.TabIndex = 3;
            this.lb_result.SelectedIndexChanged += new System.EventHandler(this.lb_result_SelectedIndexChanged);
            // 
            // lb_word
            // 
            this.lb_word.AutoSize = true;
            this.lb_word.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_word.Location = new System.Drawing.Point(70, 18);
            this.lb_word.Name = "lb_word";
            this.lb_word.Size = new System.Drawing.Size(54, 20);
            this.lb_word.TabIndex = 4;
            this.lb_word.Text = "label1";
            // 
            // rtb_search
            // 
            this.rtb_search.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_search.Location = new System.Drawing.Point(22, 25);
            this.rtb_search.Multiline = false;
            this.rtb_search.Name = "rtb_search";
            this.rtb_search.Size = new System.Drawing.Size(136, 32);
            this.rtb_search.TabIndex = 1;
            this.rtb_search.Text = "";
            this.rtb_search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtb_search_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lb_pron);
            this.groupBox1.Controls.Add(this.lb_prep);
            this.groupBox1.Controls.Add(this.lb_conj);
            this.groupBox1.Controls.Add(this.lb_v_p);
            this.groupBox1.Controls.Add(this.lb_v_pp);
            this.groupBox1.Controls.Add(this.lb_v_ping);
            this.groupBox1.Controls.Add(this.lb_v_pt);
            this.groupBox1.Controls.Add(this.lb_v_part);
            this.groupBox1.Controls.Add(this.lb_v_ppv);
            this.groupBox1.Controls.Add(this.lb_v_sich);
            this.groupBox1.Controls.Add(this.lb_v_give);
            this.groupBox1.Controls.Add(this.lb_v_vt);
            this.groupBox1.Controls.Add(this.lb_adj_super);
            this.groupBox1.Controls.Add(this.lb_adj_comp);
            this.groupBox1.Controls.Add(this.lb_n_gender);
            this.groupBox1.Controls.Add(this.lb_n_pural);
            this.groupBox1.Controls.Add(this.lb_english);
            this.groupBox1.Controls.Add(this.lb_pos);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lb_id);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lb_word);
            this.groupBox1.Location = new System.Drawing.Point(186, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(705, 416);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // lb_pron
            // 
            this.lb_pron.AutoSize = true;
            this.lb_pron.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_pron.Location = new System.Drawing.Point(561, 385);
            this.lb_pron.Name = "lb_pron";
            this.lb_pron.Size = new System.Drawing.Size(54, 20);
            this.lb_pron.TabIndex = 44;
            this.lb_pron.Text = "label1";
            // 
            // lb_prep
            // 
            this.lb_prep.AutoSize = true;
            this.lb_prep.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_prep.Location = new System.Drawing.Point(341, 385);
            this.lb_prep.Name = "lb_prep";
            this.lb_prep.Size = new System.Drawing.Size(54, 20);
            this.lb_prep.TabIndex = 43;
            this.lb_prep.Text = "label1";
            // 
            // lb_conj
            // 
            this.lb_conj.AutoSize = true;
            this.lb_conj.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_conj.Location = new System.Drawing.Point(99, 385);
            this.lb_conj.Name = "lb_conj";
            this.lb_conj.Size = new System.Drawing.Size(54, 20);
            this.lb_conj.TabIndex = 42;
            this.lb_conj.Text = "label1";
            // 
            // lb_v_p
            // 
            this.lb_v_p.AutoSize = true;
            this.lb_v_p.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_v_p.Location = new System.Drawing.Point(427, 312);
            this.lb_v_p.Name = "lb_v_p";
            this.lb_v_p.Size = new System.Drawing.Size(54, 20);
            this.lb_v_p.TabIndex = 41;
            this.lb_v_p.Text = "label1";
            // 
            // lb_v_pp
            // 
            this.lb_v_pp.AutoSize = true;
            this.lb_v_pp.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_v_pp.Location = new System.Drawing.Point(134, 341);
            this.lb_v_pp.Name = "lb_v_pp";
            this.lb_v_pp.Size = new System.Drawing.Size(54, 20);
            this.lb_v_pp.TabIndex = 40;
            this.lb_v_pp.Text = "label1";
            // 
            // lb_v_ping
            // 
            this.lb_v_ping.AutoSize = true;
            this.lb_v_ping.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_v_ping.Location = new System.Drawing.Point(134, 312);
            this.lb_v_ping.Name = "lb_v_ping";
            this.lb_v_ping.Size = new System.Drawing.Size(54, 20);
            this.lb_v_ping.TabIndex = 39;
            this.lb_v_ping.Text = "label1";
            // 
            // lb_v_pt
            // 
            this.lb_v_pt.AutoSize = true;
            this.lb_v_pt.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_v_pt.Location = new System.Drawing.Point(457, 283);
            this.lb_v_pt.Name = "lb_v_pt";
            this.lb_v_pt.Size = new System.Drawing.Size(54, 20);
            this.lb_v_pt.TabIndex = 38;
            this.lb_v_pt.Text = "label1";
            // 
            // lb_v_part
            // 
            this.lb_v_part.AutoSize = true;
            this.lb_v_part.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_v_part.Location = new System.Drawing.Point(148, 283);
            this.lb_v_part.Name = "lb_v_part";
            this.lb_v_part.Size = new System.Drawing.Size(54, 20);
            this.lb_v_part.TabIndex = 37;
            this.lb_v_part.Text = "label1";
            // 
            // lb_v_ppv
            // 
            this.lb_v_ppv.AutoSize = true;
            this.lb_v_ppv.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_v_ppv.Location = new System.Drawing.Point(442, 254);
            this.lb_v_ppv.Name = "lb_v_ppv";
            this.lb_v_ppv.Size = new System.Drawing.Size(54, 20);
            this.lb_v_ppv.TabIndex = 36;
            this.lb_v_ppv.Text = "label1";
            // 
            // lb_v_sich
            // 
            this.lb_v_sich.AutoSize = true;
            this.lb_v_sich.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_v_sich.Location = new System.Drawing.Point(99, 254);
            this.lb_v_sich.Name = "lb_v_sich";
            this.lb_v_sich.Size = new System.Drawing.Size(54, 20);
            this.lb_v_sich.TabIndex = 35;
            this.lb_v_sich.Text = "label1";
            // 
            // lb_v_give
            // 
            this.lb_v_give.AutoSize = true;
            this.lb_v_give.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_v_give.Location = new System.Drawing.Point(408, 225);
            this.lb_v_give.Name = "lb_v_give";
            this.lb_v_give.Size = new System.Drawing.Size(54, 20);
            this.lb_v_give.TabIndex = 34;
            this.lb_v_give.Text = "label1";
            // 
            // lb_v_vt
            // 
            this.lb_v_vt.AutoSize = true;
            this.lb_v_vt.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_v_vt.Location = new System.Drawing.Point(99, 225);
            this.lb_v_vt.Name = "lb_v_vt";
            this.lb_v_vt.Size = new System.Drawing.Size(54, 20);
            this.lb_v_vt.TabIndex = 33;
            this.lb_v_vt.Text = "label1";
            // 
            // lb_adj_super
            // 
            this.lb_adj_super.AutoSize = true;
            this.lb_adj_super.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_adj_super.Location = new System.Drawing.Point(442, 174);
            this.lb_adj_super.Name = "lb_adj_super";
            this.lb_adj_super.Size = new System.Drawing.Size(54, 20);
            this.lb_adj_super.TabIndex = 32;
            this.lb_adj_super.Text = "label1";
            // 
            // lb_adj_comp
            // 
            this.lb_adj_comp.AutoSize = true;
            this.lb_adj_comp.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_adj_comp.Location = new System.Drawing.Point(134, 174);
            this.lb_adj_comp.Name = "lb_adj_comp";
            this.lb_adj_comp.Size = new System.Drawing.Size(54, 20);
            this.lb_adj_comp.TabIndex = 31;
            this.lb_adj_comp.Text = "label1";
            // 
            // lb_n_gender
            // 
            this.lb_n_gender.AutoSize = true;
            this.lb_n_gender.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_n_gender.Location = new System.Drawing.Point(99, 114);
            this.lb_n_gender.Name = "lb_n_gender";
            this.lb_n_gender.Size = new System.Drawing.Size(54, 20);
            this.lb_n_gender.TabIndex = 30;
            this.lb_n_gender.Text = "label1";
            // 
            // lb_n_pural
            // 
            this.lb_n_pural.AutoSize = true;
            this.lb_n_pural.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_n_pural.Location = new System.Drawing.Point(427, 114);
            this.lb_n_pural.Name = "lb_n_pural";
            this.lb_n_pural.Size = new System.Drawing.Size(54, 20);
            this.lb_n_pural.TabIndex = 29;
            this.lb_n_pural.Text = "label1";
            // 
            // lb_english
            // 
            this.lb_english.AutoSize = true;
            this.lb_english.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_english.Location = new System.Drawing.Point(99, 55);
            this.lb_english.MaximumSize = new System.Drawing.Size(600, 0);
            this.lb_english.Name = "lb_english";
            this.lb_english.Size = new System.Drawing.Size(54, 20);
            this.lb_english.TabIndex = 28;
            this.lb_english.Text = "label1";
            // 
            // lb_pos
            // 
            this.lb_pos.AutoSize = true;
            this.lb_pos.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_pos.Location = new System.Drawing.Point(530, 18);
            this.lb_pos.Name = "lb_pos";
            this.lb_pos.Size = new System.Drawing.Size(54, 20);
            this.lb_pos.TabIndex = 27;
            this.lb_pos.Text = "label1";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label21.Location = new System.Drawing.Point(477, 385);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(89, 20);
            this.label21.TabIndex = 26;
            this.label21.Text = "代詞類型：";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label20.Location = new System.Drawing.Point(257, 385);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(89, 20);
            this.label20.TabIndex = 25;
            this.label20.Text = "介詞類型：";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label19.Location = new System.Drawing.Point(16, 385);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(89, 20);
            this.label19.TabIndex = 24;
            this.label19.Text = "連詞類型：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label18.Location = new System.Drawing.Point(16, 341);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(121, 20);
            this.label18.TabIndex = 23;
            this.label18.Text = "動詞過去分詞：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label17.Location = new System.Drawing.Point(325, 283);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(137, 20);
            this.label17.TabIndex = 22;
            this.label17.Text = "動詞現在式變化：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label16.Location = new System.Drawing.Point(16, 312);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(121, 20);
            this.label16.TabIndex = 21;
            this.label16.Text = "動詞進行式式：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label15.Location = new System.Drawing.Point(325, 312);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(105, 20);
            this.label15.TabIndex = 20;
            this.label15.Text = "動詞過去式：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label14.Location = new System.Drawing.Point(16, 283);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(137, 20);
            this.label14.TabIndex = 19;
            this.label14.Text = "動詞可分離字根：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.Location = new System.Drawing.Point(325, 254);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(121, 20);
            this.label13.TabIndex = 18;
            this.label13.Text = "完成式助動詞：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(16, 254);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 20);
            this.label12.TabIndex = 17;
            this.label12.Text = "反身動詞：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(325, 225);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 20);
            this.label11.TabIndex = 16;
            this.label11.Text = "授予動詞：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(16, 225);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 20);
            this.label10.TabIndex = 15;
            this.label10.Text = "及物動詞：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(325, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "形容詞最高級：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(325, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 20);
            this.label9.TabIndex = 13;
            this.label9.Text = "名詞複數形：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(16, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 20);
            this.label8.TabIndex = 12;
            this.label8.Text = "名詞性別：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(16, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "英文翻譯：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(16, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "形容詞比較級：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(477, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "詞性：";
            // 
            // lb_id
            // 
            this.lb_id.AutoSize = true;
            this.lb_id.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_id.Location = new System.Drawing.Point(320, 18);
            this.lb_id.Name = "lb_id";
            this.lb_id.Size = new System.Drawing.Size(54, 20);
            this.lb_id.TabIndex = 7;
            this.lb_id.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(16, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "單字：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(257, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "編號：";
            // 
            // DictForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 504);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rtb_search);
            this.Controls.Add(this.lb_result);
            this.Controls.Add(this.btn_search);
            this.Name = "DictForm";
            this.Text = "字典";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DictForm_FormClosed);
            this.Load += new System.EventHandler(this.DictForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.ListBox lb_result;
        private System.Windows.Forms.Label lb_word;
        private System.Windows.Forms.RichTextBox rtb_search;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lb_pron;
        private System.Windows.Forms.Label lb_prep;
        private System.Windows.Forms.Label lb_conj;
        private System.Windows.Forms.Label lb_v_p;
        private System.Windows.Forms.Label lb_v_pp;
        private System.Windows.Forms.Label lb_v_ping;
        private System.Windows.Forms.Label lb_v_pt;
        private System.Windows.Forms.Label lb_v_part;
        private System.Windows.Forms.Label lb_v_ppv;
        private System.Windows.Forms.Label lb_v_sich;
        private System.Windows.Forms.Label lb_v_give;
        private System.Windows.Forms.Label lb_v_vt;
        private System.Windows.Forms.Label lb_adj_super;
        private System.Windows.Forms.Label lb_adj_comp;
        private System.Windows.Forms.Label lb_n_gender;
        private System.Windows.Forms.Label lb_n_pural;
        private System.Windows.Forms.Label lb_english;
        private System.Windows.Forms.Label lb_pos;
    }
}