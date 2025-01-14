﻿using Microsoft.Win32;

namespace GW_Launcher.Forms;

public partial class AddAccountForm : Form
{
    public Account account;
    public bool finished;

    public AddAccountForm()
    {
        account = new Account();
        InitializeComponent();
    }

    private void ButtonDone_Click(object sender, EventArgs e)
    {
        account.title = textBoxTitle.Text;
        account.email = textBoxEmail.Text;
        account.password = textBoxPassword.Text;
        account.character = textBoxCharacter.Text;
        account.gwpath = textBoxPath.Text;
        account.elevated = checkBoxElevated.Checked;
        account.extraargs = textBoxExtraArguments.Text;

        finished = true;
        Close();
    }

    private void AddAccountForm_Load(object sender, EventArgs e)
    {
        textBoxTitle.Text = account.title;
        textBoxEmail.Text = account.email;
        textBoxPassword.Text = account.password;
        textBoxCharacter.Text = account.character;
        textBoxPath.Text = account.gwpath;
        checkBoxElevated.Checked = account.elevated;
        textBoxExtraArguments.Text = account.extraargs;
    }

    private void ButtonDialogPath_Click(object sender, EventArgs e)
    {
        var openFileDialog = new OpenFileDialog();

        var pathdefault =
            (string?) Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\ArenaNet\\Guild Wars", "Path", null);
        if (pathdefault == null)
        {
            pathdefault = (string?) Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\ArenaNet\\Guild Wars",
                "Path", null);
            if (pathdefault == null)
            {
                MessageBox.Show(@"pathdefault = null, gw not installed?");
            }
        }

        openFileDialog.InitialDirectory = pathdefault;
        openFileDialog.Filter = "Guild Wars|Gw.exe";
        openFileDialog.RestoreDirectory = true;

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            textBoxPath.Text = openFileDialog.FileName;
        }
    }

    private void ButtonTogglePasswordVisibility_Click(object sender, EventArgs e)
    {
        textBoxPassword.PasswordChar = textBoxPassword.PasswordChar == '\0' ? '*' : '\0';
    }

    private void ButtonMods_Click(object sender, EventArgs e)
    {
        Program.mutex.WaitOne();
        var modForm = new ModManager(account);
        modForm.Show();
        Program.mutex.ReleaseMutex();
    }
}
