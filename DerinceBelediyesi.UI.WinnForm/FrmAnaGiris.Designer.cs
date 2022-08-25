
namespace DerinceBelediyesi.UI.WinnForm
{
    partial class FrmAnaGiris
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
            this.btnstok = new System.Windows.Forms.Button();
            this.btnpersonelislemleri = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnstok
            // 
            this.btnstok.Location = new System.Drawing.Point(139, 47);
            this.btnstok.Name = "btnstok";
            this.btnstok.Size = new System.Drawing.Size(164, 42);
            this.btnstok.TabIndex = 0;
            this.btnstok.Text = "Stok İşlemleri";
            this.btnstok.UseVisualStyleBackColor = true;
            this.btnstok.Click += new System.EventHandler(this.btnstok_Click);
            // 
            // btnpersonelislemleri
            // 
            this.btnpersonelislemleri.Location = new System.Drawing.Point(139, 114);
            this.btnpersonelislemleri.Name = "btnpersonelislemleri";
            this.btnpersonelislemleri.Size = new System.Drawing.Size(164, 44);
            this.btnpersonelislemleri.TabIndex = 1;
            this.btnpersonelislemleri.Text = "Personel İşlemleri";
            this.btnpersonelislemleri.UseVisualStyleBackColor = true;
            this.btnpersonelislemleri.Click += new System.EventHandler(this.btnpersonelislemleri_Click);
            // 
            // FrmAnaGiris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 236);
            this.Controls.Add(this.btnpersonelislemleri);
            this.Controls.Add(this.btnstok);
            this.Name = "FrmAnaGiris";
            this.Text = "Derince Belediyesi";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAnaGiris_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnstok;
        private System.Windows.Forms.Button btnpersonelislemleri;
    }
}