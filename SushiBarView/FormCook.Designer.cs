
namespace SushiBarView
{
    partial class FormCook
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxRest = new System.Windows.Forms.TextBox();
            this.textBoxWork = new System.Windows.Forms.TextBox();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.labelWork = new System.Windows.Forms.Label();
            this.labelRest = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(107, 23);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(130, 20);
            this.textBoxName.TabIndex = 0;
            // 
            // textBoxRest
            // 
            this.textBoxRest.Location = new System.Drawing.Point(107, 110);
            this.textBoxRest.Name = "textBoxRest";
            this.textBoxRest.Size = new System.Drawing.Size(130, 20);
            this.textBoxRest.TabIndex = 1;
            // 
            // textBoxWork
            // 
            this.textBoxWork.Location = new System.Drawing.Point(107, 69);
            this.textBoxWork.Name = "textBoxWork";
            this.textBoxWork.Size = new System.Drawing.Size(130, 20);
            this.textBoxWork.TabIndex = 2;
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(10, 158);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(111, 32);
            this.ButtonSave.TabIndex = 3;
            this.ButtonSave.Text = "Сохранить";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(157, 159);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(99, 30);
            this.ButtonCancel.TabIndex = 4;
            this.ButtonCancel.Text = "Отмена";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(20, 23);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(34, 13);
            this.labelName.TabIndex = 5;
            this.labelName.Text = "ФИО";
            // 
            // labelWork
            // 
            this.labelWork.AutoSize = true;
            this.labelWork.Location = new System.Drawing.Point(19, 69);
            this.labelWork.Name = "labelWork";
            this.labelWork.Size = new System.Drawing.Size(43, 13);
            this.labelWork.TabIndex = 6;
            this.labelWork.Text = "Работа";
            // 
            // labelRest
            // 
            this.labelRest.AutoSize = true;
            this.labelRest.Location = new System.Drawing.Point(20, 110);
            this.labelRest.Name = "labelRest";
            this.labelRest.Size = new System.Drawing.Size(39, 13);
            this.labelRest.TabIndex = 7;
            this.labelRest.Text = "Отдых";
            // 
            // FormCook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 209);
            this.Controls.Add(this.labelRest);
            this.Controls.Add(this.labelWork);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.textBoxWork);
            this.Controls.Add(this.textBoxRest);
            this.Controls.Add(this.textBoxName);
            this.Name = "FormCook";
            this.Text = "Повар";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxRest;
        private System.Windows.Forms.TextBox textBoxWork;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelWork;
        private System.Windows.Forms.Label labelRest;
    }
}