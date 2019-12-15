using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using TooSharp.Models;

namespace App_FOR_DB
{
    public partial class Visits_View : MaterialForm
    {
        public Visits_View()
        {
            InitializeComponent();
            FillSortTags();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue700, Primary.BlueGrey500, Accent.LightBlue100, TextShade.WHITE);
        }
        void PopulateData(IEnumerable<Doctor> doctors, IEnumerable<Patient> patients, IEnumerable<Servise> servises, IEnumerable<Treatment> treatments, IEnumerable<Visit> visits)
        {
            dataGridView1.Rows.Clear();
            foreach (var vis in visits)
                foreach (var  pat in patients)
                    foreach (var doc in doctors)
                        foreach (var ser in servises)
                            foreach (var thr in treatments)
                            {
                                if (doc.Employee_number == vis.Employee_number && pat.Pasport_ID == vis.Pasport_ID && vis.Servise_number == ser.Servise_number && vis.Therapy_number == thr.Therapy_number)
                                {
                                    dataGridView1.Rows.Add(new object[]
                                    {
                                        pat.Name +" "+ pat.Surname +" "+ pat.Midle_name,
                                        ser.Servise_name,
                                        doc.Name +" "+ doc.Surname +" "+ doc.Midle_name,
                                        vis.Date,
                                        "Изменить",
                                        "Удалить"
                                    });
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Tag = vis;
                                }
                            }
        }

        void ReloadData()
        {
            if (textBox1.Text.Trim().Length > 0)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                            PopulateData(
                            Doctors.Records().Get(),
                            Patients.Records().Where(Patients.COLUMNS.Surname, "LIKE", "%" + textBox1.Text + "%").Get(),
                            Servises.Records().Get(),
                            Treatments.Records().Get(),
                            Visits.Records().Get()
                            );
                        break;
                    case 1:
                             PopulateData(
                             Doctors.Records().Get(),
                             Patients.Records().Get(),
                             Servises.Records().Where(Servises.COLUMNS.Servise_name, "LIKE", "%" + textBox1.Text + "%").Get(),
                             Treatments.Records().Get(),
                             Visits.Records().Get()
                             );
                        break;
                    case 2:
                            PopulateData(
                            Doctors.Records().Where(Doctors.COLUMNS.Surname, "LIKE", "%" + textBox1.Text + "%").Get(),
                            Patients.Records().Get(),
                            Servises.Records().Get(),
                            Treatments.Records().Get(),
                            Visits.Records().Get()
                            );
                        break;
                    default:
                        PopulateData(
                        Doctors.Records().Get(),
                        Patients.Records().Get(),
                        Servises.Records().Get(),
                        Treatments.Records().Get(),
                        Visits.Records().Get()
                        );
                        break;
                }
            }
            else
            {
                PopulateData(
                Doctors.Records().Get(),
                Patients.Records().Get(),
                Servises.Records().Get(),
                Treatments.Records().Get(),
                Visits.Records().Get()
                );
            }
        }
        private void Visits_View_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            ReloadData();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Visit visit = (Visit)dataGridView1.CurrentRow.Tag;
            var patient = visit.GetPatient();
            var doctor = visit.GetDoctor();
            var servis = visit.GetServise();
            var treat = visit.GetTreatment();
            if (e.ColumnIndex == 4)
            {
                new AddEditVisits(visit,doctor,patient,servis, treat).ShowDialog();
            }
            if (e.ColumnIndex == 5)
            {
                if (MessageBox.Show("Удалить записть пациента  " + patient.Name + " " + patient.Surname + "?", "Подтвердить", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    visit.Delete();
                    ReloadData();
                }
            }
        }
        void FillSortTags()
        {
            string[] SortFilds = new string[] { "Фамилии", "Услуге", "Врачу"};
            foreach(var tags in SortFilds) comboBox1.Items.Add(tags);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ReloadData();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ReloadData();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddEditVisits aDDEditVisit = new AddEditVisits();
            aDDEditVisit.ShowDialog();
        }
    }
}
