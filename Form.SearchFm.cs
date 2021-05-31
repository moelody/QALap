using System;
using System.Windows.Forms;
using System.Drawing;

public partial class SearchFm
{
    private readonly System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }
    private void InitializeComponent()
    {
        this.panelBox = new PanelBox();
        this.searchText = new EditTextBox();
        this.mainButton = new FlatButton();
        this.panelBox.SuspendLayout();
        SuspendLayout();

        this.searchText.BorderStyle = BorderStyle.None;
        this.searchText.BackColor = Color.FromArgb(30, 30, 30);
        this.searchText.ForeColor = Color.FromArgb(255, 255, 255);
        this.searchText.Location = new Point(10, 9);
        this.searchText.Margin = new Padding(10, 10, 10, 10);
        this.searchText.Name = "searchText";
        this.searchText.Size = new Size(430, 100);
        this.searchText.TabIndex = 0;
        this.searchText.TabStop = true;
        this.searchText.Font = new Font("微软雅黑", 18, FontStyle.Regular);
        this.searchText.EditMousedoubleclick += this.searchText_DoubleClick;
        this.searchText.TextChanged += new EventHandler(this.searchText_TextChanged);
        this.searchText.KeyDown += new KeyEventHandler(this.searchText_KeyDown);
        this.searchText.KeyPress += new KeyPressEventHandler(this.searchText_KeyPress);
        this.searchText.KeyUp += new KeyEventHandler(this.searchText_KeyUp);

        this.mainButton.BackColor = Color.FromArgb(36, 36, 36); //Button color
        this.mainButton.ForeColor = Color.FromArgb(255, 255, 255);//The color of the button text
        this.mainButton.FlatAppearance.BorderColor = Color.FromArgb(36, 36, 36);//The color of the button's border
        this.mainButton.FlatAppearance.MouseDownBackColor = Color.White;
        this.mainButton.FlatAppearance.MouseOverBackColor = Color.White;
        this.mainButton.FlatStyle = FlatStyle.Flat;
        this.mainButton.Name = "searchButton";
        this.mainButton.Tag = "Search";
        this.mainButton.Text = "⚡";
        this.mainButton.Location = new Point(500, 6);
        this.mainButton.Font = new Font("微软雅黑", 16, FontStyle.Bold);
        this.mainButton.Size = new Size(40, 40);
        this.mainButton.TabIndex = 1;
        this.mainButton.UseVisualStyleBackColor = false;
        this.mainButton.Click += new EventHandler(this.mainButton_Click);

        this.Popup = new PopupWindow()
        {
            AutoSize = true,
            MinimumSize = new Size(540, 75),
            MaximumSize = new Size(540, 25 + 300),
            BackColor = Color.FromArgb(30, 30, 30),
            ForeColor = Color.FromArgb(255, 255, 255),
            Font = new Font("微软雅黑", 18, FontStyle.Regular),
            Margin = new Padding(0, 0, 0, 1),
            Padding = Padding.Empty
        };
        this.Popup.MouseWheel += new MouseEventHandler(handleDelta);
        this.Popup.ItemClicked += new ToolStripItemClickedEventHandler(popup_ItemClicked);
        this.Popup.MouseHover += new EventHandler(popup_MouseHover);

        this.panelBox.Margin = Padding.Empty;
        this.panelBox.Padding = Padding.Empty;
        this.panelBox.AutoSize = true;
        this.panelBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        this.panelBox.Controls.Add(searchText);
        this.panelBox.Controls.Add(mainButton);
        this.panelBox.Controls.Add(Popup);

        this.AutoSize = true;
        this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        this.AutoScaleDimensions = new SizeF(6F, 12F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.BackColor = Color.FromArgb(30, 30, 30);
        this.ClientSize = new Size(540, 50);
        this.Controls.Add(panelBox);

        this.FormBorderStyle = FormBorderStyle.None;
        this.Name = "SearchFm";
        this.Padding = Padding.Empty;
        this.ShowIcon = false;
        this.ShowInTaskbar = false;
        //this.StartPosition = FormStartPosition.CenterScreen;
        this.StartPosition = FormStartPosition.Manual;
        this.Location = Cursor.Position;
        if (Cursor.Position.X + 543 > Screen.PrimaryScreen.Bounds.Width)
        {
            this.Location = new Point(Cursor.Position.X - 543, Cursor.Position.Y);
        }
        if (Cursor.Position.Y + 377 > Screen.PrimaryScreen.Bounds.Height)
        {
            this.Location = new Point(Cursor.Position.X, Cursor.Position.Y - 377);
        }
        this.Text = "QuickerSearch";
        this.Deactivate += new EventHandler(SearchFm_Deactivate);
        this.Load += new EventHandler(Form1_Load);
        this.Shown += new EventHandler(Form1_Shown);
        this.MouseDown += new MouseEventHandler(Form1_MouseDown);

        this.panelBox.ResumeLayout(false);
        this.panelBox.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }
    private Panel panelBox;
    private FlatButton mainButton;
    private EditTextBox searchText;
    private PopupWindow Popup;
}
