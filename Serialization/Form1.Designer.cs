namespace Serialization
{
    partial class Form1
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
            this.TypeSelector = new System.Windows.Forms.ComboBox();
            this.ForPrintDeserializedInfo = new System.Windows.Forms.ListBox();
            this.SerializedButton = new System.Windows.Forms.Button();
            this.DeserializedButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TypeSelector
            // 
            this.TypeSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.TypeSelector.FormattingEnabled = true;
            this.TypeSelector.Items.AddRange(new object[] {
            "Binary",
            "JSON",
            "XML"});
            this.TypeSelector.Location = new System.Drawing.Point(584, 70);
            this.TypeSelector.Name = "TypeSelector";
            this.TypeSelector.Size = new System.Drawing.Size(147, 37);
            this.TypeSelector.TabIndex = 0;
            // 
            // ForPrintDeserializedInfo
            // 
            this.ForPrintDeserializedInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ForPrintDeserializedInfo.FormattingEnabled = true;
            this.ForPrintDeserializedInfo.ItemHeight = 20;
            this.ForPrintDeserializedInfo.Location = new System.Drawing.Point(12, 44);
            this.ForPrintDeserializedInfo.Name = "ForPrintDeserializedInfo";
            this.ForPrintDeserializedInfo.Size = new System.Drawing.Size(539, 364);
            this.ForPrintDeserializedInfo.TabIndex = 1;
            // 
            // SerializedButton
            // 
            this.SerializedButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.SerializedButton.Location = new System.Drawing.Point(584, 232);
            this.SerializedButton.Name = "SerializedButton";
            this.SerializedButton.Size = new System.Drawing.Size(147, 37);
            this.SerializedButton.TabIndex = 2;
            this.SerializedButton.Text = "Serialized";
            this.SerializedButton.UseVisualStyleBackColor = true;
            this.SerializedButton.Click += new System.EventHandler(this.SerializedButton_Click);
            // 
            // DeserializedButton
            // 
            this.DeserializedButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.DeserializedButton.Location = new System.Drawing.Point(576, 342);
            this.DeserializedButton.Name = "DeserializedButton";
            this.DeserializedButton.Size = new System.Drawing.Size(175, 37);
            this.DeserializedButton.TabIndex = 3;
            this.DeserializedButton.Text = "Deserialized";
            this.DeserializedButton.UseVisualStyleBackColor = true;
            this.DeserializedButton.Click += new System.EventHandler(this.DeserializedButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DeserializedButton);
            this.Controls.Add(this.SerializedButton);
            this.Controls.Add(this.ForPrintDeserializedInfo);
            this.Controls.Add(this.TypeSelector);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox TypeSelector;
        private System.Windows.Forms.ListBox ForPrintDeserializedInfo;
        private System.Windows.Forms.Button SerializedButton;
        private System.Windows.Forms.Button DeserializedButton;
    }
}

