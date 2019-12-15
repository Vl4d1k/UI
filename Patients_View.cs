using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using TooSharp.Models;

namespace App_FOR_DB
{
    public partial class Patients_View : MaterialForm
    {
        public Patients_View()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue700, Primary.BlueGrey500, Accent.LightBlue100, TextShade.WHITE);
        }
        void PopulateData(IEnumerable<Patient> patients)
        {
            dataGridView1.Rows.Clear();
            foreach (var c in patients)
            {
                dataGridView1.Rows.Add(new object[]
                {
                    //c.Pasport_ID,
                    c.Surname,
                    c.Name,
                    c.Midle_name,
                    c.Sex,
                    c.Phone,
                    c.Address,
                    c.Birthday,
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
                PopulateData(Patients.Records()
                    .Where(Patients.COLUMNS.Pasport_ID, "LIKE", "%" + textBox1.Text + "%")
                    .Get());
            }
            else
            {
                PopulateData(Patients.Records().Get());
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            ReloadData();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ReloadData();
        }
        private void Patients_View_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            ReloadData();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddEditPatientForm addPatientDial = new AddEditPatientForm();
            addPatientDial.ShowDialog();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Patient patient = (Patient)dataGridView1.CurrentRow.Tag;
            if (e.ColumnIndex == 8)
            {
                new AddEditPatientForm(patient).ShowDialog();
            }
            if (e.ColumnIndex == 7)
            {
                if (MessageBox.Show("Удалить запись " + patient.Surname + "?", "Подтвердить", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    patient.Delete();
                    ReloadData();
                }
            }
        }
    }
}
