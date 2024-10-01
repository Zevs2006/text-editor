using System;
using System.IO;
using System.Windows.Forms;

namespace text_editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // ���������� ���������
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null)
            {
                // ������ ��� ���������� �����
                SaveFile(tabControl1.SelectedTab);
            }
            else
            {
                MessageBox.Show("��� �������� ������� ��� ����������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ��������� ��� (� ������� ������ �����)
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null)
            {
                RichTextBox rtb = tabControl1.SelectedTab.Controls[0] as RichTextBox;
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "��������� ����� (*.txt)|*.txt|��� ����� (*.*)|*.*"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, rtb.Text);
                    tabControl1.SelectedTab.Text = Path.GetFileName(saveFileDialog.FileName);
                    tabControl1.SelectedTab.Tag = saveFileDialog.FileName; // ��������� ���� � ����� � Tag
                }
            }
            else
            {
                MessageBox.Show("��� �������� ������� ��� ����������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ����� ��� ���������� �����
        private void SaveFile(TabPage tabPage)
        {
            RichTextBox rtb = tabPage.Controls[0] as RichTextBox;

            if (tabPage.Tag != null) // ���� ���� ��� �������� ������
            {
                string filePath = tabPage.Tag.ToString();
                File.WriteAllText(filePath, rtb.Text);
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "��������� ����� (*.txt)|*.txt|��� ����� (*.*)|*.*"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, rtb.Text);
                    tabPage.Text = Path.GetFileName(saveFileDialog.FileName);
                    tabPage.Tag = saveFileDialog.FileName; // ��������� ���� � ����� � Tag
                }
            }
        }

        // �������� ����� �������
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage newTab = new TabPage("����� ��������");
            RichTextBox rtb = new RichTextBox
            {
                Dock = DockStyle.Fill
            };
            newTab.Controls.Add(rtb);
            tabControl1.TabPages.Add(newTab);
            tabControl1.SelectedTab = newTab;
        }

        // �������� �����
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "��������� ����� (*.txt)|*.txt|��� ����� (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileContent = File.ReadAllText(openFileDialog.FileName);
                TabPage newTab = new TabPage(Path.GetFileName(openFileDialog.FileName));
                RichTextBox rtb = new RichTextBox
                {
                    Dock = DockStyle.Fill,
                    Text = fileContent
                };
                newTab.Controls.Add(rtb);
                newTab.Tag = openFileDialog.FileName; // ��������� ���� � ��������� �����
                tabControl1.TabPages.Add(newTab);
                tabControl1.SelectedTab = newTab;
            }
        }
    }
}
