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

        // Сохранение изменений
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null)
            {
                // Логика для сохранения файла
                SaveFile(tabControl1.SelectedTab);
            }
            else
            {
                MessageBox.Show("Нет открытых вкладок для сохранения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Сохранить как (с выбором нового места)
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null)
            {
                RichTextBox rtb = tabControl1.SelectedTab.Controls[0] as RichTextBox;
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, rtb.Text);
                    tabControl1.SelectedTab.Text = Path.GetFileName(saveFileDialog.FileName);
                    tabControl1.SelectedTab.Tag = saveFileDialog.FileName; // Сохраняем путь к файлу в Tag
                }
            }
            else
            {
                MessageBox.Show("Нет открытых вкладок для сохранения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Метод для сохранения файла
        private void SaveFile(TabPage tabPage)
        {
            RichTextBox rtb = tabPage.Controls[0] as RichTextBox;

            if (tabPage.Tag != null) // Если файл уже сохранен раньше
            {
                string filePath = tabPage.Tag.ToString();
                File.WriteAllText(filePath, rtb.Text);
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, rtb.Text);
                    tabPage.Text = Path.GetFileName(saveFileDialog.FileName);
                    tabPage.Tag = saveFileDialog.FileName; // Сохраняем путь к файлу в Tag
                }
            }
        }

        // Создание новой вкладки
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage newTab = new TabPage("Новый документ");
            RichTextBox rtb = new RichTextBox
            {
                Dock = DockStyle.Fill
            };
            newTab.Controls.Add(rtb);
            tabControl1.TabPages.Add(newTab);
            tabControl1.SelectedTab = newTab;
        }

        // Открытие файла
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
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
                newTab.Tag = openFileDialog.FileName; // Сохраняем путь к открытому файлу
                tabControl1.TabPages.Add(newTab);
                tabControl1.SelectedTab = newTab;
            }
        }
    }
}
