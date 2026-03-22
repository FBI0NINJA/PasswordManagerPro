using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PasswordManagerPro
{
    public partial class Form1 : Form
    {
        List<PasswordItem> items = new List<PasswordItem>();
        string filePath = "data.json";
        bool isVisible = false;

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
        }

        void LoadData()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                items = JsonConvert.DeserializeObject<List<PasswordItem>>(json) ?? new List<PasswordItem>();

                listBox1.Items.Clear();

                foreach (var item in items)
                    listBox1.Items.Add($"{item.Site} | {item.Email} | {item.Password}");
            }
        }


        //json
        void SaveData()
        {
            string json = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }


        // save

        private void buttonSave_Click(object sender, EventArgs e)
        {
            PasswordItem item = new PasswordItem()
            {
                Site = textBoxSite.Text,
                Email = textBoxEmail.Text,
                Password = textBoxPassword.Text
            };

            items.Add(item);
            SaveData();
            LoadData();

            textBoxSite.Clear();
            textBoxEmail.Clear();
            textBoxPassword.Clear();
        }


        // list box
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;

            if (index >= 0)
            {
                textBoxSite.Text = items[index].Site;
                textBoxEmail.Text = items[index].Email;
                textBoxPassword.Text = items[index].Password;
            }
        }


        // delete
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;

            if (index >= 0)
            {
                DialogResult result = MessageBox.Show(
                    "هل انت متأكد من حذف هذا العنصر؟",
                    "تأكيد الحذف",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    items.RemoveAt(index);
                    SaveData();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("اختار عنصر الأول");
            }
        }


        //copy
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxPassword.Text);
            MessageBox.Show("Copied!");
        }

        //اظهار-اخفاء كلمة السر
        private void buttonShow_Click(object sender, EventArgs e)
        {
            isVisible = !isVisible;
            textBoxPassword.UseSystemPasswordChar = !isVisible;
        }


        //زرار فتح الفيس بوك
        private void buttonFacebook_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/ninjafbi1");
        }


        //زرار فتح جيتهاب
        private void buttonGithub_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/FBI0NINJA");

        }

        private void buttonFacebook_MouseEnter(object sender, EventArgs e)
        {
            buttonFacebook.BackColor = Color.LightGray;
        }

        private void buttonFacebook_MouseLeave(object sender, EventArgs e)
        {
            buttonFacebook.BackColor = Color.Transparent;
        }

        //فتح موقع عند الخروج

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.facebook.com/ninjafbi1",
                UseShellExecute = true
            });
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSite_TextChanged(object sender, EventArgs e)
        {

        }


        // edit
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;

            if (index >= 0)
            {
               
                items[index].Site = textBoxSite.Text;
                items[index].Email = textBoxEmail.Text;
                items[index].Password = textBoxPassword.Text;

                SaveData();
                LoadData();

                MessageBox.Show("تم التعديل بنجاح ✅");
            }
            else
            {
                MessageBox.Show("اختار عنصر الأول");
            }
        }
    }
}