namespace SizeChecker
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTreeView = new System.Windows.Forms.TreeView();
            this.mainListView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // mainTreeView
            // 
            this.mainTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.mainTreeView.Location = new System.Drawing.Point(9, 10);
            this.mainTreeView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mainTreeView.Name = "mainTreeView";
            this.mainTreeView.Size = new System.Drawing.Size(164, 347);
            this.mainTreeView.TabIndex = 0;
            this.mainTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.mainTreeView_BeforeExpand);
            this.mainTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.mainTreeView_AfterSelect);
            // 
            // mainListView
            // 
            this.mainListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainListView.HideSelection = false;
            this.mainListView.Location = new System.Drawing.Point(176, 10);
            this.mainListView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mainListView.Name = "mainListView";
            this.mainListView.Size = new System.Drawing.Size(416, 347);
            this.mainListView.TabIndex = 1;
            this.mainListView.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.mainListView);
            this.Controls.Add(this.mainTreeView);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "Size Checker";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView mainTreeView;
        private System.Windows.Forms.ListView mainListView;
    }
}

