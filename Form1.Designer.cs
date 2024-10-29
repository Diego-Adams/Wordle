namespace Wordle01
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
            Wordle = new Label();
            SuspendLayout();
            // 
            // Wordle
            // 
            Wordle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Wordle.AutoSize = true;
            Wordle.Font = new Font("Segoe Print", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Wordle.Location = new Point(124, -2);
            Wordle.Name = "Wordle";
            Wordle.Size = new Size(121, 50);
            Wordle.TabIndex = 0;
            Wordle.Text = "Wordle";
            Wordle.TextAlign = ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(378, 444);
            Controls.Add(Wordle);
            MaximumSize = new Size(400, 500);
            MinimumSize = new Size(400, 500);
            Name = "Form1";
            Text = "Wordle";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Wordle;


        

            }

     

    }




