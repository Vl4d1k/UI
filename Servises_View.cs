using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using TooSharp.Models;

namespace App_FOR_DB
{
    public partial class Servises_View : MaterialForm
    {
        public Servises_View()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue700, Primary.BlueGrey500, Accent.LightBlue100, TextShade.WHITE);
        }
        void PopulateData(IEnumerable<Servise> servises)
        {
            dataGridView1.Rows.Clear();
            foreach (var c in servises)
            {
                dataGridView1.Rows.Add(new object[]
                {
                    c.Servise_name,
                    c.Cost,
                    "Изменить",
                    "Удалить"
                });
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Tag = c;
            }
        }
        void ReloadData()
        {
            if (textBox1.Text.Trim().Length > 0)
            {
                PopulateData(Servises.Records()
                    .Where(Servises.COLUMNS.Servise_name, "LIKE", "%" + textBox1.Text + "%")
                    .Get());
            }
            else
            {
                PopulateData(Servises.Records().Get());
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ReloadData();
        }
        private void Servises_View_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            ReloadData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ReloadData();
        }
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                var ser = (Servise)dataGridView1.CurrentRow.Tag;
                if (MessageBox.Show("Удалить запись " + ser.Servise_name + "?", "Подтвердить", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    ser.Delete();
                    ReloadData();
                }
            }
            if (e.ColumnIndex == 3)
            {
                new AddEditServises((Servise)dataGridView1.CurrentRow.Tag).ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AddEditServises().ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ReloadData();
        }
    }
}
