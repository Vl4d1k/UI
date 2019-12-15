using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using TooSharp.Models;

namespace App_FOR_DB
{
    public partial class Doctors_View : MaterialForm
    {
        public Doctors_View()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue700, Primary.BlueGrey500, Accent.LightBlue100, TextShade.WHITE);
        }
        void PopulateData(IEnumerable<Doctor> doctors, IEnumerable<Post> posts)
        {
            dataGridView1.Rows.Clear();
            foreach (var doc in doctors)
                foreach (var pos in posts)
                {
                    if (doc.Position_number == pos.Position_number)
                    {
                        dataGridView1.Rows.Add(new object[]
                        {
                        //doc.Employee_number,
                        doc.Surname,
                        doc.Name,
                        doc.Midle_name,
                        doc.Phone,
                        pos.Category,
                        pos.Specialty,
                        "Изменить",
                        "Удалить"
                        });
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Tag = doc;
                    }
                    
                }
        }
        void ReloadData()
        {
            if (textBox1.Text.Trim().Length > 0)
            {
                PopulateData(Doctors.Records()
                    .Where(Doctors.COLUMNS.Surname, "LIKE", "%" + textBox1.Text + "%")
                    .Get(), Posts.Records().Get()
                    );
            }
            else
            {
                PopulateData(Doctors.Records().Get()
                    ,Posts.Records().Get()
                    );
            }
        }
        private void Doctors_View_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            ReloadData();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ReloadData();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ReloadData();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Doctor doctor = (Doctor)dataGridView1.CurrentRow.Tag;
            
            if (e.ColumnIndex == 6)
            {
                Post post = doctor.GetPost();
                new AddEditDoctor(doctor,post).ShowDialog();
            }
            if (e.ColumnIndex == 7)
            {
                if (MessageBox.Show("Удалить сотрудника " + doctor.Surname + " " + doctor.Name + "?", "Подтвердить", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    doctor.Delete();
                    ReloadData();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            new AddEditDoctor().ShowDialog();
        }
    }
}
