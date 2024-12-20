namespace HotelManagementSystem1
{
    partial class ReceptionistForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceptionistForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnManageRes = new Guna.UI2.WinForms.Guna2Button();
            this.btnLogOut = new Guna.UI2.WinForms.Guna2Button();
            this.BTN_ROOMASS = new Guna.UI2.WinForms.Guna2Button();
            this.MovingPanal = new System.Windows.Forms.Panel();
            this.btnCheckout = new Guna.UI2.WinForms.Guna2Button();
            this.btnExist2 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnMinimize = new Guna.UI2.WinForms.Guna2CircleButton();
            this.panalResp = new System.Windows.Forms.Panel();
            this.uC_RoomAssign1 = new HotelManagementSystem1.AllUserControls_Resptions.UC_RoomAssign();
            this.uC_ManageResidents1 = new HotelManagementSystem1.AllUserControls_Resptions.UC_ManageResidents();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.uC_ResCHECKOUT1 = new HotelManagementSystem1.AllUserControls_Resptions.UC_ResCHECKOUT();
            this.panel2.SuspendLayout();
            this.panalResp.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnManageRes);
            this.panel2.Controls.Add(this.btnLogOut);
            this.panel2.Controls.Add(this.BTN_ROOMASS);
            this.panel2.Controls.Add(this.MovingPanal);
            this.panel2.Controls.Add(this.btnCheckout);
            this.panel2.Location = new System.Drawing.Point(90, 4);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2673, 257);
            this.panel2.TabIndex = 7;
            // 
            // btnManageRes
            // 
            this.btnManageRes.BorderRadius = 26;
            this.btnManageRes.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnManageRes.CheckedState.BorderColor = System.Drawing.Color.White;
            this.btnManageRes.CheckedState.CustomBorderColor = System.Drawing.Color.White;
            this.btnManageRes.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnManageRes.CheckedState.ForeColor = System.Drawing.Color.Black;
            this.btnManageRes.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnManageRes.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnManageRes.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnManageRes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnManageRes.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            this.btnManageRes.Font = new System.Drawing.Font("Segoe UI", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageRes.ForeColor = System.Drawing.Color.White;
            this.btnManageRes.Image = ((System.Drawing.Image)(resources.GetObject("btnManageRes.Image")));
            this.btnManageRes.ImageSize = new System.Drawing.Size(40, 40);
            this.btnManageRes.Location = new System.Drawing.Point(34, 37);
            this.btnManageRes.Margin = new System.Windows.Forms.Padding(4);
            this.btnManageRes.Name = "btnManageRes";
            this.btnManageRes.Size = new System.Drawing.Size(436, 169);
            this.btnManageRes.TabIndex = 9;
            this.btnManageRes.Text = "Manage Residents";
            this.btnManageRes.Click += new System.EventHandler(this.btnAddRoom_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.BorderRadius = 26;
            this.btnLogOut.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnLogOut.CheckedState.BorderColor = System.Drawing.Color.White;
            this.btnLogOut.CheckedState.CustomBorderColor = System.Drawing.Color.White;
            this.btnLogOut.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnLogOut.CheckedState.ForeColor = System.Drawing.Color.Black;
            this.btnLogOut.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogOut.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogOut.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogOut.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogOut.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            this.btnLogOut.Font = new System.Drawing.Font("Segoe UI", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.ForeColor = System.Drawing.Color.White;
            this.btnLogOut.ImageSize = new System.Drawing.Size(40, 40);
            this.btnLogOut.Location = new System.Drawing.Point(2199, 37);
            this.btnLogOut.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(364, 169);
            this.btnLogOut.TabIndex = 8;
            this.btnLogOut.Text = "LogOut";
            this.btnLogOut.Click += new System.EventHandler(this.guna2Button6_Click);
            // 
            // BTN_ROOMASS
            // 
            this.BTN_ROOMASS.BorderRadius = 26;
            this.BTN_ROOMASS.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.BTN_ROOMASS.CheckedState.BorderColor = System.Drawing.Color.White;
            this.BTN_ROOMASS.CheckedState.CustomBorderColor = System.Drawing.Color.White;
            this.BTN_ROOMASS.CheckedState.FillColor = System.Drawing.Color.White;
            this.BTN_ROOMASS.CheckedState.ForeColor = System.Drawing.Color.Black;
            this.BTN_ROOMASS.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BTN_ROOMASS.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BTN_ROOMASS.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BTN_ROOMASS.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BTN_ROOMASS.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            this.BTN_ROOMASS.Font = new System.Drawing.Font("Segoe UI", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_ROOMASS.ForeColor = System.Drawing.Color.White;
            this.BTN_ROOMASS.Image = ((System.Drawing.Image)(resources.GetObject("BTN_ROOMASS.Image")));
            this.BTN_ROOMASS.ImageSize = new System.Drawing.Size(40, 40);
            this.BTN_ROOMASS.Location = new System.Drawing.Point(780, 37);
            this.BTN_ROOMASS.Margin = new System.Windows.Forms.Padding(4);
            this.BTN_ROOMASS.Name = "BTN_ROOMASS";
            this.BTN_ROOMASS.Size = new System.Drawing.Size(436, 169);
            this.BTN_ROOMASS.TabIndex = 7;
            this.BTN_ROOMASS.Text = "Assign Room ";
            this.BTN_ROOMASS.Click += new System.EventHandler(this.BTN_ROOMMonitor_Click);
            // 
            // MovingPanal
            // 
            this.MovingPanal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MovingPanal.Location = new System.Drawing.Point(34, 217);
            this.MovingPanal.Margin = new System.Windows.Forms.Padding(4);
            this.MovingPanal.Name = "MovingPanal";
            this.MovingPanal.Size = new System.Drawing.Size(420, 10);
            this.MovingPanal.TabIndex = 0;
            // 
            // btnCheckout
            // 
            this.btnCheckout.BorderRadius = 26;
            this.btnCheckout.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnCheckout.CheckedState.BorderColor = System.Drawing.Color.White;
            this.btnCheckout.CheckedState.CustomBorderColor = System.Drawing.Color.White;
            this.btnCheckout.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnCheckout.CheckedState.ForeColor = System.Drawing.Color.Black;
            this.btnCheckout.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCheckout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCheckout.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCheckout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCheckout.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Image = ((System.Drawing.Image)(resources.GetObject("btnCheckout.Image")));
            this.btnCheckout.ImageSize = new System.Drawing.Size(40, 40);
            this.btnCheckout.Location = new System.Drawing.Point(1479, 37);
            this.btnCheckout.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(436, 169);
            this.btnCheckout.TabIndex = 3;
            this.btnCheckout.Text = "Checkout";
            this.btnCheckout.Click += new System.EventHandler(this.btnTrackIncome_Click);
            // 
            // btnExist2
            // 
            this.btnExist2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExist2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExist2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExist2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExist2.FillColor = System.Drawing.Color.Empty;
            this.btnExist2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExist2.ForeColor = System.Drawing.Color.White;
            this.btnExist2.Image = ((System.Drawing.Image)(resources.GetObject("btnExist2.Image")));
            this.btnExist2.ImageSize = new System.Drawing.Size(30, 30);
            this.btnExist2.Location = new System.Drawing.Point(-36, -27);
            this.btnExist2.Margin = new System.Windows.Forms.Padding(4);
            this.btnExist2.Name = "btnExist2";
            this.btnExist2.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnExist2.Size = new System.Drawing.Size(118, 104);
            this.btnExist2.TabIndex = 8;
            this.btnExist2.Click += new System.EventHandler(this.btnExist2_Click_1);
            // 
            // btnMinimize
            // 
            this.btnMinimize.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMinimize.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMinimize.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMinimize.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMinimize.FillColor = System.Drawing.Color.Empty;
            this.btnMinimize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimize.Image")));
            this.btnMinimize.ImageSize = new System.Drawing.Size(30, 30);
            this.btnMinimize.Location = new System.Drawing.Point(-34, 69);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(4);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnMinimize.Size = new System.Drawing.Size(116, 79);
            this.btnMinimize.TabIndex = 9;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // panalResp
            // 
            this.panalResp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panalResp.Controls.Add(this.uC_ResCHECKOUT1);
            this.panalResp.Controls.Add(this.uC_RoomAssign1);
            this.panalResp.Controls.Add(this.uC_ManageResidents1);
            this.panalResp.Location = new System.Drawing.Point(90, 289);
            this.panalResp.Name = "panalResp";
            this.panalResp.Size = new System.Drawing.Size(2673, 1298);
            this.panalResp.TabIndex = 10;
            // 
            // uC_RoomAssign1
            // 
            this.uC_RoomAssign1.BackColor = System.Drawing.Color.White;
            this.uC_RoomAssign1.Location = new System.Drawing.Point(3, 3);
            this.uC_RoomAssign1.Name = "uC_RoomAssign1";
            this.uC_RoomAssign1.Size = new System.Drawing.Size(2670, 1595);
            this.uC_RoomAssign1.TabIndex = 1;
            // 
            // uC_ManageResidents1
            // 
            this.uC_ManageResidents1.BackColor = System.Drawing.Color.White;
            this.uC_ManageResidents1.Location = new System.Drawing.Point(3, 3);
            this.uC_ManageResidents1.Name = "uC_ManageResidents1";
            this.uC_ManageResidents1.Size = new System.Drawing.Size(2670, 1595);
            this.uC_ManageResidents1.TabIndex = 0;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 30;
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2Elipse2
            // 
            this.guna2Elipse2.BorderRadius = 30;
            this.guna2Elipse2.TargetControl = this;
            // 
            // guna2Elipse3
            // 
            this.guna2Elipse3.BorderRadius = 30;
            this.guna2Elipse3.TargetControl = this;
            // 
            // uC_ResCHECKOUT1
            // 
            this.uC_ResCHECKOUT1.BackColor = System.Drawing.Color.White;
            this.uC_ResCHECKOUT1.Location = new System.Drawing.Point(3, 3);
            this.uC_ResCHECKOUT1.Name = "uC_ResCHECKOUT1";
            this.uC_ResCHECKOUT1.Size = new System.Drawing.Size(2670, 1595);
            this.uC_ResCHECKOUT1.TabIndex = 2;
            // 
            // ReceptionistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(2886, 1520);
            this.Controls.Add(this.panalResp);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnExist2);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReceptionistForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "receptionistForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReceptionistForm_Load);
            this.panel2.ResumeLayout(false);
            this.panalResp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2Button btnManageRes;
        private Guna.UI2.WinForms.Guna2Button btnLogOut;
        private Guna.UI2.WinForms.Guna2Button BTN_ROOMASS;
        private System.Windows.Forms.Panel MovingPanal;
        private Guna.UI2.WinForms.Guna2Button btnCheckout;
        private Guna.UI2.WinForms.Guna2CircleButton btnExist2;
        private Guna.UI2.WinForms.Guna2CircleButton btnMinimize;
        private System.Windows.Forms.Panel panalResp;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private AllUserControls_Resptions.UC_ManageResidents uC_ManageResidents1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private AllUserControls_Resptions.UC_RoomAssign uC_RoomAssign1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse3;
        private AllUserControls_Resptions.UC_ResCHECKOUT uC_ResCHECKOUT1;
    }
}