
namespace ResXHelper
{
    partial class CustomOptionsPageForm
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
            this.LBAllLanguages = new System.Windows.Forms.ListBox();
            this.LBSelectedLanguages = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LBAllLanguages
            // 
            this.LBAllLanguages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBAllLanguages.FormattingEnabled = true;
            this.LBAllLanguages.ItemHeight = 20;
            this.LBAllLanguages.Location = new System.Drawing.Point(15, 79);
            this.LBAllLanguages.Name = "LBAllLanguages";
            this.LBAllLanguages.Size = new System.Drawing.Size(325, 464);
            this.LBAllLanguages.Sorted = true;
            this.LBAllLanguages.TabIndex = 0;
            this.LBAllLanguages.DoubleClick += new System.EventHandler(this.LBAllLanguages_DoubleClick);
            // 
            // LBSelectedLanguages
            // 
            this.LBSelectedLanguages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBSelectedLanguages.FormattingEnabled = true;
            this.LBSelectedLanguages.ItemHeight = 20;
            this.LBSelectedLanguages.Location = new System.Drawing.Point(486, 79);
            this.LBSelectedLanguages.Name = "LBSelectedLanguages";
            this.LBSelectedLanguages.Size = new System.Drawing.Size(325, 464);
            this.LBSelectedLanguages.Sorted = true;
            this.LBSelectedLanguages.TabIndex = 1;
            this.LBSelectedLanguages.DoubleClick += new System.EventHandler(this.LBSelectedLanguages_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(231, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(353, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select default languages";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(346, 223);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(134, 50);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "->";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(346, 293);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(134, 50);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "<-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // CustomOptionsPageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LBSelectedLanguages);
            this.Controls.Add(this.LBAllLanguages);
            this.MaximumSize = new System.Drawing.Size(825, 578);
            this.MinimumSize = new System.Drawing.Size(825, 578);
            this.Name = "CustomOptionsPageForm";
            this.Size = new System.Drawing.Size(825, 578);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LBAllLanguages;
        private System.Windows.Forms.ListBox LBSelectedLanguages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
    }
}
