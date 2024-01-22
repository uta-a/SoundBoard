namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            inputDevices = new ComboBox();
            outputDevices = new ComboBox();
            Buffa = new NumericUpDown();
            MusicFileCombo = new ComboBox();
            Paths = new TextBox();
            MusicFileList = new ListBox();
            Volume = new TrackBar();
            start = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            groupBox1 = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            Status = new Label();
            groupBox2 = new GroupBox();
            button6 = new Button();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            groupBox5 = new GroupBox();
            groupBox6 = new GroupBox();
            button5 = new Button();
            mediaPos = new TrackBar();
            currentTime = new System.Windows.Forms.Timer(components);
            groupBox7 = new GroupBox();
            time = new Label();
            lastTime = new System.Windows.Forms.Timer(components);
            Menu = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            ReadMe = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)Buffa).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Volume).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mediaPos).BeginInit();
            groupBox7.SuspendLayout();
            Menu.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // inputDevices
            // 
            inputDevices.DropDownStyle = ComboBoxStyle.DropDownList;
            inputDevices.FormattingEnabled = true;
            resources.ApplyResources(inputDevices, "inputDevices");
            inputDevices.Name = "inputDevices";
            // 
            // outputDevices
            // 
            outputDevices.DropDownStyle = ComboBoxStyle.DropDownList;
            outputDevices.FormattingEnabled = true;
            resources.ApplyResources(outputDevices, "outputDevices");
            outputDevices.Name = "outputDevices";
            // 
            // Buffa
            // 
            resources.ApplyResources(Buffa, "Buffa");
            Buffa.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            Buffa.Minimum = new decimal(new int[] { 10000, 0, 0, 0 });
            Buffa.Name = "Buffa";
            Buffa.Value = new decimal(new int[] { 20000, 0, 0, 0 });
            // 
            // MusicFileCombo
            // 
            MusicFileCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            MusicFileCombo.FormattingEnabled = true;
            resources.ApplyResources(MusicFileCombo, "MusicFileCombo");
            MusicFileCombo.Name = "MusicFileCombo";
            MusicFileCombo.SelectedIndexChanged += MusicFileCombo_SelectedIndexChanged;
            // 
            // Paths
            // 
            resources.ApplyResources(Paths, "Paths");
            Paths.Name = "Paths";
            Paths.ReadOnly = true;
            // 
            // MusicFileList
            // 
            MusicFileList.FormattingEnabled = true;
            resources.ApplyResources(MusicFileList, "MusicFileList");
            MusicFileList.Name = "MusicFileList";
            // 
            // Volume
            // 
            Volume.BackColor = Color.WhiteSmoke;
            resources.ApplyResources(Volume, "Volume");
            Volume.Maximum = 100;
            Volume.Name = "Volume";
            Volume.TickFrequency = 10;
            Volume.TickStyle = TickStyle.Both;
            Volume.Value = 100;
            Volume.Scroll += Volume_Scroll;
            // 
            // start
            // 
            resources.ApplyResources(start, "start");
            start.Name = "start";
            start.UseVisualStyleBackColor = true;
            start.Click += start_Click;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += stop_Click;
            // 
            // button2
            // 
            resources.ApplyResources(button2, "button2");
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += play_Click;
            // 
            // button3
            // 
            resources.ApplyResources(button3, "button3");
            button3.Name = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += stopSound_Click;
            // 
            // button4
            // 
            resources.ApplyResources(button4, "button4");
            button4.Name = "button4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += OpenFolder_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(outputDevices);
            groupBox1.Controls.Add(inputDevices);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // Status
            // 
            resources.ApplyResources(Status, "Status");
            Status.ForeColor = Color.Red;
            Status.Name = "Status";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button6);
            groupBox2.Controls.Add(button4);
            groupBox2.Controls.Add(MusicFileList);
            groupBox2.Controls.Add(Paths);
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // button6
            // 
            resources.ApplyResources(button6, "button6");
            button6.Name = "button6";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(groupBox4);
            groupBox3.Controls.Add(button1);
            groupBox3.Controls.Add(start);
            groupBox3.Controls.Add(Status);
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(Buffa);
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            // 
            // groupBox5
            // 
            groupBox5.BackColor = Color.WhiteSmoke;
            groupBox5.Controls.Add(Volume);
            resources.ApplyResources(groupBox5, "groupBox5");
            groupBox5.Name = "groupBox5";
            groupBox5.TabStop = false;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(button5);
            groupBox6.Controls.Add(MusicFileCombo);
            groupBox6.Controls.Add(button3);
            groupBox6.Controls.Add(button2);
            resources.ApplyResources(groupBox6, "groupBox6");
            groupBox6.Name = "groupBox6";
            groupBox6.TabStop = false;
            // 
            // button5
            // 
            resources.ApplyResources(button5, "button5");
            button5.Name = "button5";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // mediaPos
            // 
            mediaPos.BackColor = Color.WhiteSmoke;
            resources.ApplyResources(mediaPos, "mediaPos");
            mediaPos.Maximum = 100;
            mediaPos.Name = "mediaPos";
            mediaPos.TickStyle = TickStyle.None;
            mediaPos.Scroll += mediaPos_Scroll;
            // 
            // currentTime
            // 
            currentTime.Interval = 10;
            currentTime.Tick += timer1_Tick;
            // 
            // groupBox7
            // 
            groupBox7.BackColor = Color.WhiteSmoke;
            groupBox7.Controls.Add(time);
            groupBox7.Controls.Add(mediaPos);
            resources.ApplyResources(groupBox7, "groupBox7");
            groupBox7.Name = "groupBox7";
            groupBox7.TabStop = false;
            // 
            // time
            // 
            resources.ApplyResources(time, "time");
            time.BackColor = Color.Transparent;
            time.Name = "time";
            // 
            // lastTime
            // 
            lastTime.Interval = 10;
            lastTime.Tick += lastTime_Tick;
            // 
            // Menu
            // 
            Menu.Controls.Add(tabPage1);
            Menu.Controls.Add(tabPage2);
            resources.ApplyResources(Menu, "Menu");
            Menu.Name = "Menu";
            Menu.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Controls.Add(groupBox7);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupBox6);
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Controls.Add(groupBox5);
            resources.ApplyResources(tabPage1, "tabPage1");
            tabPage1.Name = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(ReadMe);
            resources.ApplyResources(tabPage2, "tabPage2");
            tabPage2.Name = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // ReadMe
            // 
            resources.ApplyResources(ReadMe, "ReadMe");
            ReadMe.Name = "ReadMe";
            ReadMe.ReadOnly = true;
            ReadMe.LinkClicked += richTextBox1_LinkClicked;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Menu);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)Buffa).EndInit();
            ((System.ComponentModel.ISupportInitialize)Volume).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mediaPos).EndInit();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            Menu.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ComboBox inputDevices;
        private ComboBox outputDevices;
        private NumericUpDown Buffa;
        private ComboBox MusicFileCombo;
        private TextBox Paths;
        private ListBox MusicFileList;
        private TrackBar Volume;
        private Button start;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private GroupBox groupBox1;
        private Label label2;
        private Label label1;
        private Label Status;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private Button button5;
        private TrackBar mediaPos;
        private System.Windows.Forms.Timer currentTime;
        private GroupBox groupBox7;
        private Label time;
        private System.Windows.Forms.Timer lastTime;
        private Button button6;
        private TabControl Menu;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private RichTextBox ReadMe;
    }
}
