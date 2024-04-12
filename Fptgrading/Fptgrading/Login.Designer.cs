namespace Fptgrading
{
    partial class Login
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.usertbx = new System.Windows.Forms.TextBox();
            this.passtbx = new System.Windows.Forms.TextBox();
            this.loginbutton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.errortbx = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(285, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(285, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // usertbx
            // 
            this.usertbx.Location = new System.Drawing.Point(382, 188);
            this.usertbx.Name = "usertbx";
            this.usertbx.Size = new System.Drawing.Size(115, 22);
            this.usertbx.TabIndex = 2;
            this.usertbx.TextChanged += new System.EventHandler(this.usertbx_TextChanged);
            // 
            // passtbx
            // 
            this.passtbx.Location = new System.Drawing.Point(382, 269);
            this.passtbx.Name = "passtbx";
            this.passtbx.Size = new System.Drawing.Size(115, 22);
            this.passtbx.TabIndex = 2;
            // 
            // loginbutton
            // 
            this.loginbutton.Location = new System.Drawing.Point(254, 364);
            this.loginbutton.Name = "loginbutton";
            this.loginbutton.Size = new System.Drawing.Size(75, 23);
            this.loginbutton.TabIndex = 3;
            this.loginbutton.Text = "Login";
            this.loginbutton.UseVisualStyleBackColor = true;
            this.loginbutton.Click += new System.EventHandler(this.loginbutton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(470, 364);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // errortbx
            // 
            this.errortbx.AutoSize = true;
            this.errortbx.ForeColor = System.Drawing.Color.Red;
            this.errortbx.Location = new System.Drawing.Point(379, 327);
            this.errortbx.Name = "errortbx";
            this.errortbx.Size = new System.Drawing.Size(0, 16);
            this.errortbx.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(385, 327);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 16);
            this.label3.TabIndex = 5;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.errortbx);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.loginbutton);
            this.Controls.Add(this.passtbx);
            this.Controls.Add(this.usertbx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox usertbx;
        private System.Windows.Forms.TextBox passtbx;
        private System.Windows.Forms.Button loginbutton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label errortbx;
        private System.Windows.Forms.Label label3;
    }
}

