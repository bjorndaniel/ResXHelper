namespace ResXHelper2022.Options
{
    partial class SettingsForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.BtnRemove = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.LBSelectedLanguages = new System.Windows.Forms.ListBox();
            this.LBAllLanguages = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(222, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select default languages";
            // 
            // BtnRemove
            // 
            this.BtnRemove.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRemove.Location = new System.Drawing.Point(345, 271);
            this.BtnRemove.Name = "BtnRemove";
            this.BtnRemove.Size = new System.Drawing.Size(134, 50);
            this.BtnRemove.TabIndex = 8;
            this.BtnRemove.Text = "<-";
            this.BtnRemove.UseVisualStyleBackColor = true;
            this.BtnRemove.Click += new System.EventHandler(this.BtnRemoveOnClick);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.Location = new System.Drawing.Point(345, 201);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(134, 50);
            this.BtnAdd.TabIndex = 7;
            this.BtnAdd.Text = "->";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAddOnClick);
            // 
            // LBSelectedLanguages
            // 
            this.LBSelectedLanguages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBSelectedLanguages.FormattingEnabled = true;
            this.LBSelectedLanguages.ItemHeight = 20;
            this.LBSelectedLanguages.Location = new System.Drawing.Point(485, 57);
            this.LBSelectedLanguages.Name = "LBSelectedLanguages";
            this.LBSelectedLanguages.Size = new System.Drawing.Size(325, 464);
            this.LBSelectedLanguages.Sorted = true;
            this.LBSelectedLanguages.TabIndex = 6;
            this.LBSelectedLanguages.DoubleClick += new System.EventHandler(this.LBSelectedLanguages_DoubleClick);
            // 
            // LBAllLanguages
            // 
            this.LBAllLanguages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBAllLanguages.FormattingEnabled = true;
            this.LBAllLanguages.ItemHeight = 20;
            this.LBAllLanguages.Location = new System.Drawing.Point(14, 57);
            this.LBAllLanguages.Name = "LBAllLanguages";
            this.LBAllLanguages.Size = new System.Drawing.Size(325, 464);
            this.LBAllLanguages.Sorted = true;
            this.LBAllLanguages.TabIndex = 5;
            this.LBAllLanguages.DoubleClick += new System.EventHandler(this.LBAllLanguages_DoubleClick);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnRemove);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.LBSelectedLanguages);
            this.Controls.Add(this.LBAllLanguages);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(825, 578);
            this.MinimumSize = new System.Drawing.Size(825, 578);
            this.Name = "SettingsForm";
            this.Size = new System.Drawing.Size(825, 578);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnRemove;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.ListBox LBSelectedLanguages;
        private System.Windows.Forms.ListBox LBAllLanguages;
    }
}
