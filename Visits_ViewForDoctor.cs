using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using TooSharp.Models;

namespace App_FOR_DB
{
    public partial class Visits_ViewForDoctor : MaterialForm
    {
        string User;
        public Visits_ViewForDoctor(string user)
        {
            InitializeComponent();
            User = user;
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
                                        thr.Diagnos,
                                        thr.Therapy,
                                        "Назначить лечение",
                                        "Историю болезни"
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
                    case 1:
                        PopulateData(
                        Doctors.Records().Where(Doctors.COLUMNS.Employee_number, "LIKE", "%" + User + "%").Get(),
                        Patients.Records().Get(),
                        Servises.Records().Where(Servises.COLUMNS.Servise_name, "LIKE", "%" + textBox1.Text + "%").Get(),
                        Treatments.Records().Get(),
                        Visits.Records().Get()
                        );
                        break;
                    case 2:
                        PopulateData(
                        Doctors.Records().Where(Doctors.COLUMNS.Employee_number, "LIKE", "%" + User + "%").Get(),
                        Patients.Records().Get(),
                        Servises.Records().Get(),
                        Treatments.Records().Get(),
                        Visits.Records().Where(Visits.COLUMNS.Date, "LIKE", "%" + textBox1.Text + "%").Get()
                        );
                        break;
                    case 0:
                        PopulateData(
                        Doctors.Records().Where(Doctors.COLUMNS.Employee_number, "LIKE", "%" + User + "%").Get(),
                        Patients.Records().Where(Patients.COLUMNS.Surname, "LIKE", "%" + textBox1.Text + "%").Get(),
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
                    Doctors.Records().Where(Doctors.COLUMNS.Employee_number, "LIKE", "%" + User + "%").Get(),
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
            if (e.ColumnIndex == 6)
            {
                new SetTherapy(patient, treat, visit).ShowDialog();
            }
            if (e.ColumnIndex == 7)
            {
                var his = Patient_histories.Records().Get();
                foreach (var pat_his in his)
                {
                    if(pat_his.Client_ID == patient.Pasport_ID)
                    {
                        new PatientHistory(patient, pat_his).ShowDialog();
                    }
                }
                
            }
        }
        void FillSortTags()
        {
            string[] SortFilds = new string[] { "Фамилии", "Услуге", "Времени"};
            foreach(var tags in SortFilds) comboBox1.Items.Add(tags);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ReloadData();
        }
        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            ReloadData();
        }
    }
}
