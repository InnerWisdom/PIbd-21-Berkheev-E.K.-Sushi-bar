namespace SushiBarView
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.КомпонентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ИзделияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonReports = new System.Windows.Forms.ToolStripDropDownButton();
            this.списокКомпонентовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.компонентыПоИзделиямToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокЗаказовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.ButtonCreateOrder = new System.Windows.Forms.Button();
            this.ButtonTakeOrderInWork = new System.Windows.Forms.Button();
            this.ButtonOrderReady = new System.Windows.Forms.Button();
            this.ButtonPayOrder = new System.Windows.Forms.Button();
            this.ButtonRef = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton,
            this.toolStripDropDownButtonReports});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton
            // 
            this.toolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.КомпонентыToolStripMenuItem,
            this.ИзделияToolStripMenuItem});
            this.toolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton.Image")));
            this.toolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton.Name = "toolStripDropDownButton";
            this.toolStripDropDownButton.Size = new System.Drawing.Size(95, 22);
            this.toolStripDropDownButton.Text = "Справочники";
            // 
            // КомпонентыToolStripMenuItem
            // 
            this.КомпонентыToolStripMenuItem.Name = "КомпонентыToolStripMenuItem";
            this.КомпонентыToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.КомпонентыToolStripMenuItem.Text = "Ингредиенты";
            this.КомпонентыToolStripMenuItem.Click += new System.EventHandler(this.КомпонентыToolStripMenuItem_Click);
            // 
            // ИзделияToolStripMenuItem
            // 
            this.ИзделияToolStripMenuItem.Name = "ИзделияToolStripMenuItem";
            this.ИзделияToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ИзделияToolStripMenuItem.Text = "Суши";
            this.ИзделияToolStripMenuItem.Click += new System.EventHandler(this.ИзделияToolStripMenuItem_Click);
            // 
            // toolStripDropDownButtonReports
            // 
            this.toolStripDropDownButtonReports.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButtonReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.списокКомпонентовToolStripMenuItem,
            this.компонентыПоИзделиямToolStripMenuItem,
            this.списокЗаказовToolStripMenuItem});
            this.toolStripDropDownButtonReports.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonReports.Image")));
            this.toolStripDropDownButtonReports.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonReports.Name = "toolStripDropDownButtonReports";
            this.toolStripDropDownButtonReports.Size = new System.Drawing.Size(61, 22);
            this.toolStripDropDownButtonReports.Text = "Отчеты";
            // 
            // списокКомпонентовToolStripMenuItem
            // 
            this.списокКомпонентовToolStripMenuItem.Name = "списокКомпонентовToolStripMenuItem";
            this.списокКомпонентовToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.списокКомпонентовToolStripMenuItem.Text = "Список ингредиентов";
            this.списокКомпонентовToolStripMenuItem.Click += new System.EventHandler(this.списокКомпонентовToolStripMenuItem_Click);
            // 
            // компонентыПоИзделиямToolStripMenuItem
            // 
            this.компонентыПоИзделиямToolStripMenuItem.Name = "компонентыПоИзделиямToolStripMenuItem";
            this.компонентыПоИзделиямToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.компонентыПоИзделиямToolStripMenuItem.Text = "Ингредиенты по суши";
            this.компонентыПоИзделиямToolStripMenuItem.Click += new System.EventHandler(this.компонентыПоИзделиямToolStripMenuItem_Click);
            // 
            // списокЗаказовToolStripMenuItem
            // 
            this.списокЗаказовToolStripMenuItem.Name = "списокЗаказовToolStripMenuItem";
            this.списокЗаказовToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.списокЗаказовToolStripMenuItem.Text = "Список заказов";
            this.списокЗаказовToolStripMenuItem.Click += new System.EventHandler(this.списокЗаказовToolStripMenuItem_Click);
            // 
            // DataGridView
            // 
            this.DataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Location = new System.Drawing.Point(1, 32);
            this.DataGridView.MultiSelect = false;
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView.Size = new System.Drawing.Size(638, 337);
            this.DataGridView.TabIndex = 1;
            // 
            // ButtonCreateOrder
            // 
            this.ButtonCreateOrder.Location = new System.Drawing.Point(645, 43);
            this.ButtonCreateOrder.Name = "ButtonCreateOrder";
            this.ButtonCreateOrder.Size = new System.Drawing.Size(143, 31);
            this.ButtonCreateOrder.TabIndex = 2;
            this.ButtonCreateOrder.Text = "Создать заказ";
            this.ButtonCreateOrder.UseVisualStyleBackColor = true;
            this.ButtonCreateOrder.Click += new System.EventHandler(this.ButtonCreateOrder_Click);
            // 
            // ButtonTakeOrderInWork
            // 
            this.ButtonTakeOrderInWork.Location = new System.Drawing.Point(645, 100);
            this.ButtonTakeOrderInWork.Name = "ButtonTakeOrderInWork";
            this.ButtonTakeOrderInWork.Size = new System.Drawing.Size(143, 31);
            this.ButtonTakeOrderInWork.TabIndex = 3;
            this.ButtonTakeOrderInWork.Text = "Отдать на выполнение";
            this.ButtonTakeOrderInWork.UseVisualStyleBackColor = true;
            this.ButtonTakeOrderInWork.Click += new System.EventHandler(this.ButtonTakeOrderInWork_Click);
            // 
            // ButtonOrderReady
            // 
            this.ButtonOrderReady.Location = new System.Drawing.Point(645, 158);
            this.ButtonOrderReady.Name = "ButtonOrderReady";
            this.ButtonOrderReady.Size = new System.Drawing.Size(143, 31);
            this.ButtonOrderReady.TabIndex = 4;
            this.ButtonOrderReady.Text = "Заказ готов";
            this.ButtonOrderReady.UseVisualStyleBackColor = true;
            this.ButtonOrderReady.Click += new System.EventHandler(this.ButtonOrderReady_Click);
            // 
            // ButtonPayOrder
            // 
            this.ButtonPayOrder.Location = new System.Drawing.Point(645, 213);
            this.ButtonPayOrder.Name = "ButtonPayOrder";
            this.ButtonPayOrder.Size = new System.Drawing.Size(143, 31);
            this.ButtonPayOrder.TabIndex = 5;
            this.ButtonPayOrder.Text = "Заказ оплачен";
            this.ButtonPayOrder.UseVisualStyleBackColor = true;
            this.ButtonPayOrder.Click += new System.EventHandler(this.ButtonPayOrder_Click);
            // 
            // ButtonRef
            // 
            this.ButtonRef.Location = new System.Drawing.Point(645, 268);
            this.ButtonRef.Name = "ButtonRef";
            this.ButtonRef.Size = new System.Drawing.Size(143, 31);
            this.ButtonRef.TabIndex = 6;
            this.ButtonRef.Text = "Обновить список";
            this.ButtonRef.UseVisualStyleBackColor = true;
            this.ButtonRef.Click += new System.EventHandler(this.ButtonRef_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 384);
            this.Controls.Add(this.ButtonRef);
            this.Controls.Add(this.ButtonPayOrder);
            this.Controls.Add(this.ButtonOrderReady);
            this.Controls.Add(this.ButtonTakeOrderInWork);
            this.Controls.Add(this.ButtonCreateOrder);
            this.Controls.Add(this.DataGridView);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormMain";
            this.Text = "Суши бар";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem КомпонентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ИзделияToolStripMenuItem;
        private System.Windows.Forms.DataGridView DataGridView;
        private System.Windows.Forms.Button ButtonCreateOrder;
        private System.Windows.Forms.Button ButtonTakeOrderInWork;
        private System.Windows.Forms.Button ButtonOrderReady;
        private System.Windows.Forms.Button ButtonPayOrder;
        private System.Windows.Forms.Button ButtonRef;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonReports;
        private System.Windows.Forms.ToolStripMenuItem списокКомпонентовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem компонентыПоИзделиямToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокЗаказовToolStripMenuItem;
    }
}