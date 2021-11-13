
namespace TP_03
{
    partial class PanelControlForm
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
            this.pictureBox_BackgroundFormPanelControl = new System.Windows.Forms.PictureBox();
            this.button_AdministracionPanelControlForm = new System.Windows.Forms.Button();
            this.button_ImpresionPanelControlForm = new System.Windows.Forms.Button();
            this.button_SalirPanelControlForm = new System.Windows.Forms.Button();
            this.button_BatallaPanelControlForm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_BackgroundFormPanelControl)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_BackgroundFormPanelControl
            // 
            this.pictureBox_BackgroundFormPanelControl.Location = new System.Drawing.Point(-6, 0);
            this.pictureBox_BackgroundFormPanelControl.Name = "pictureBox_BackgroundFormPanelControl";
            this.pictureBox_BackgroundFormPanelControl.Size = new System.Drawing.Size(520, 301);
            this.pictureBox_BackgroundFormPanelControl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_BackgroundFormPanelControl.TabIndex = 3;
            this.pictureBox_BackgroundFormPanelControl.TabStop = false;
            // 
            // button_AdministracionPanelControlForm
            // 
            this.button_AdministracionPanelControlForm.BackColor = System.Drawing.Color.Transparent;
            this.button_AdministracionPanelControlForm.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_AdministracionPanelControlForm.Location = new System.Drawing.Point(155, 134);
            this.button_AdministracionPanelControlForm.Name = "button_AdministracionPanelControlForm";
            this.button_AdministracionPanelControlForm.Size = new System.Drawing.Size(194, 25);
            this.button_AdministracionPanelControlForm.TabIndex = 1;
            this.button_AdministracionPanelControlForm.Text = "Administración";
            this.button_AdministracionPanelControlForm.UseVisualStyleBackColor = false;
            this.button_AdministracionPanelControlForm.Click += new System.EventHandler(this.button_AdministracionPjs_Click);
            // 
            // button_ImpresionPanelControlForm
            // 
            this.button_ImpresionPanelControlForm.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_ImpresionPanelControlForm.Location = new System.Drawing.Point(155, 176);
            this.button_ImpresionPanelControlForm.Name = "button_ImpresionPanelControlForm";
            this.button_ImpresionPanelControlForm.Size = new System.Drawing.Size(194, 23);
            this.button_ImpresionPanelControlForm.TabIndex = 2;
            this.button_ImpresionPanelControlForm.Text = "Impresión";
            this.button_ImpresionPanelControlForm.UseVisualStyleBackColor = true;
            this.button_ImpresionPanelControlForm.Click += new System.EventHandler(this.button_ImpresionPjs_Click);
            // 
            // button_SalirPanelControlForm
            // 
            this.button_SalirPanelControlForm.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_SalirPanelControlForm.Location = new System.Drawing.Point(155, 216);
            this.button_SalirPanelControlForm.Name = "button_SalirPanelControlForm";
            this.button_SalirPanelControlForm.Size = new System.Drawing.Size(194, 23);
            this.button_SalirPanelControlForm.TabIndex = 3;
            this.button_SalirPanelControlForm.Text = "Salir";
            this.button_SalirPanelControlForm.UseVisualStyleBackColor = true;
            this.button_SalirPanelControlForm.Click += new System.EventHandler(this.button_CerrarPrograma_Click);
            // 
            // button_BatallaPanelControlForm
            // 
            this.button_BatallaPanelControlForm.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_BatallaPanelControlForm.Location = new System.Drawing.Point(155, 90);
            this.button_BatallaPanelControlForm.Name = "button_BatallaPanelControlForm";
            this.button_BatallaPanelControlForm.Size = new System.Drawing.Size(194, 23);
            this.button_BatallaPanelControlForm.TabIndex = 0;
            this.button_BatallaPanelControlForm.Text = "Batalla";
            this.button_BatallaPanelControlForm.UseVisualStyleBackColor = true;
            this.button_BatallaPanelControlForm.Click += new System.EventHandler(this.button_BatallaPanelControlForm_Click);
            // 
            // PanelControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(511, 297);
            this.Controls.Add(this.button_BatallaPanelControlForm);
            this.Controls.Add(this.button_SalirPanelControlForm);
            this.Controls.Add(this.button_AdministracionPanelControlForm);
            this.Controls.Add(this.button_ImpresionPanelControlForm);
            this.Controls.Add(this.pictureBox_BackgroundFormPanelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PanelControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panel de control de personajes";
            this.Load += new System.EventHandler(this.PanelControlForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_BackgroundFormPanelControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_BackgroundFormPanelControl;
        private System.Windows.Forms.Button button_AdministracionPanelControlForm;
        private System.Windows.Forms.Button button_ImpresionPanelControlForm;
        private System.Windows.Forms.Button button_SalirPanelControlForm;
        private System.Windows.Forms.Button button_BatallaPanelControlForm;
    }
}

