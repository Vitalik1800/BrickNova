using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrickNova.UI;


public partial class PlayerNameForm : Form
{
    private readonly TextBox _nameTextBox;
    private readonly Button _okButton;
    private readonly Button _cancelButton;

    public string PlayerName =>
        _nameTextBox.Text.Trim();

    public PlayerNameForm()
    {
        InitializeComponent();

        Text = "Player Name";
        ClientSize = new Size(400, 200);
        StartPosition = FormStartPosition.CenterParent;

        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;

        Label titleLabel = new Label
        {
            Text = "Enter your name:",
            AutoSize = true,
            Font = new Font(
                "Arial",
                14,
                FontStyle.Bold
            ),
            Location = new Point(30, 30)
        };

        _nameTextBox = new TextBox
        {
            Name = "nameTextBox",
            Location = new Point(30, 70),
            Size = new Size(340, 25),
            MaxLength = 20
        };

        _okButton = new Button
        {
            Text = "OK",
            Size = new Size(80, 30),
            Location = new Point(200, 120)
        };

        _cancelButton = new Button
        {
            Text= "Cancel",
            Size = new Size(80, 30),
            Location = new Point(290, 120)
        };

        _okButton.Click += OnOkButtonClick;
        _cancelButton.Click += OnCancelButtonClick;

        Controls.Add(titleLabel);
        Controls.Add(_nameTextBox);
        Controls.Add(_okButton);
        Controls.Add(_cancelButton);

        AcceptButton = _okButton;
        CancelButton = _cancelButton;
    }

    private void OnOkButtonClick(
        object? sender,
        EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(
            _nameTextBox.Text))
        {
            MessageBox.Show(
                "Please enter your name.",
                "BrickNova",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            _nameTextBox.Focus();

            return;
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private void OnCancelButtonClick(
        object? sender,
        EventArgs e )
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

}
