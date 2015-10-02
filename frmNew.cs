using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DOSBox_Launcher
{
    public partial class frmNew : Form
    {
        public string NewFileName = "";
        private string NewFilePath = "";
        public string Template = "";
        private char[] InvalidPathChars = Path.GetInvalidPathChars();

        public frmNew()
        {
            InitializeComponent();
        }

        private void frmNew_Load(object sender, System.EventArgs e)
        {
            var info = new FileInfo(NewFileName);
            txtLauncherName.Text = info.Name.Substring(0, info.Name.LastIndexOf("."));
            NewFilePath = info.Directory.FullName + "\\";
        }

        private void txtFilename_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog1.InitialDirectory = NewFilePath;
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (OpenFileDialog1.FileName.ToLower().IndexOf(NewFilePath.ToLower()) == 0)
                {
                    txtFilename.Text = OpenFileDialog1.FileName.Substring(NewFilePath.Length);
                    if (OpenFileDialog1.SafeFileName.LastIndexOf(".") > 0)
                    {
                        txtLauncherName.Text = OpenFileDialog1.SafeFileName.Substring(0, OpenFileDialog1.SafeFileName.LastIndexOf("."));
                    }
                    else
                    {
                        if (OpenFileDialog1.SafeFileName.Length > 0)
                        {
                            txtLauncherName.Text = OpenFileDialog1.SafeFileName;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The file must be within the folder that the configuration file was created in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void txtLauncherName_TextChanged(object sender, System.EventArgs e)
        {
            TextBox unboxedSender = (TextBox)sender;

            foreach (char invalidChar in InvalidPathChars)
            {
                if (unboxedSender.Text.Contains(invalidChar.ToString()))
                    unboxedSender.Text = unboxedSender.Text.Replace(invalidChar.ToString(), "");
            }
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            List<string> template = new List<string>();

            template.Add(this.Template);

            if (txtFilename.Text.Length == 0)
            {
                MessageBox.Show("Select a file to launch.");
                return;
            }

            if (txtLauncherName.Text.Length == 0)
            {
                MessageBox.Show("Enter a launcher name.");
                return;
            }

            string cdPath = "";

            if (txtFilename.Text.Contains("\\"))
            {
                var info = new FileInfo(NewFilePath + txtFilename.Text);
                cdPath = info.Directory.FullName.Substring(NewFilePath.Length);
                template.Add($"cd c:\\{cdPath}");
            }

            if (txtFilename.Text.Substring(txtFilename.Text.Length - 4).ToLower() == ".bat")
            {
                template.Add($"call \"{(txtFilename.Text.Contains("\\") ? txtFilename.Text.Substring(cdPath.Length + 1) : txtFilename.Text)}\"");
            }
            else
            {
                template.Add(txtFilename.Text);
            }

            if (chkExit.Checked)
                template.Add("exit");

            var finalName = Path.Combine(NewFilePath, txtLauncherName.Text + ".dosbox");

            if (File.Exists(finalName) && MessageBox.Show($"The file {finalName} already exists, do you want to overwrite?", "File Exists", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            File.WriteAllText(finalName, string.Join("\r\n", template.ToArray()));
            Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
