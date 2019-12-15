using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using TooSharp.Models;

namespace App_FOR_DB
{
    public partial class AddEditVisits : MaterialForm
    {
        Doctor _Doctor = null;
        Servise _Servise = null;
        Visit _Visit = null;
        Treatment _Treatment = null;
        Patient _Patient = null;
        public AddEditVisits()
        {
            InitializeComponent();
            FillComboBoxPatients(Patients.Records().Get());
            FillComboBoxServises(Servises.Records().Get());
            FillComboBoxServises(Doctors.Records().Get());
            this.Text = "Добавление новой записи";
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue700, Primary.BlueGrey500, Accent.LightBlue100, TextShade.WHITE);
        }
        public AddEditVisits(Visit visit, Doctor doctor, Patient patient,Servise servise, Treatment treatment)
        {
            InitializeComponent();
            this.Text = "Редактировать запись";
            FillComboBoxPatients(Patients.Records().Get());
            FillComboBoxServises(Servises.Records().Get());
            FillComboBoxServises(Doctors.Records().Get());
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue700, Primary.BlueGrey500, Accent.LightBlue100, TextShade.WHITE);
            clientsNameComboBox.SelectedIndex = visit.Pasport_ID - 1;
            comboBoxServisesName.SelectedIndex = visit.Servise_number - 1;
            comboBoxDoctorsName.SelectedIndex = visit.Employee_number - 1;
            hours.Text = Convert.ToString(visit.Date.Hour);
            minutes.Text = Convert.ToString(visit.Date.Minute);
            seconds.Text = Convert.ToString(visit.Date.Second);
            dateTimePicker1.Value = visit.Date;
            button1.Text = "Редактировать";
             _Doctor = doctor;
             _Servise = servise;
             _Visit = visit;
             _Treatment = treatment;
             _Patient = patient;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int hour = int.Parse(hours.Text);
            int min = int.Parse(minutes.Text);
            int sec = int.Parse(seconds.Text);
            int clientId = int.Parse(clientComboBoxId.Items[clientsNameComboBox.SelectedIndex].ToString());
            int servise_number = int.Parse(comboBoxServises.Items[comboBoxServisesName.SelectedIndex].ToString());
            int employee_number = int.Parse(comboBoxDoctor.Items[comboBoxDoctorsName.SelectedIndex].ToString());
            if (_Doctor == null && _Servise == null && _Visit == null && _Patient == null && _Servise == null && _Treatment == null)
            {
                Treatment tret = new Treatment() { Diagnos = "Диагноз: ", Therapy = "Лечение: " };
                var res = tret.Save();
                var num_tret = tret.Therapy_number;
                Visit vis = new Visit()
                {
                    Therapy_number = num_tret,
                    Pasport_ID = clientId,
                    Servise_number = servise_number,
                    Employee_number = employee_number,
                    Date = new DateTime( dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, hour, min, sec)
                };
                vis.SetTreatment(tret);
                vis.onValidationError += Visit_onValidationError;
                if (vis.Save() > 0) MessageBox.Show("Новая запись добавлена!");
                else MessageBox.Show("Ошибка в добавлении");
            }
            else
            {
                _Visit.Pasport_ID = clientId;
                _Visit.Employee_number = employee_number;
                _Visit.Servise_number = servise_number;
                _Visit.Date = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, hour, min, sec);
                _Visit.onValidationError += Visit_onValidationError;
                if (_Visit.Save() > 0) MessageBox.Show("Запись обновлена!");
            }
            this.Close();
        }
        void ShowError(string Text)
        {
            label9.Text = Text;
            panel1.Visible = true;
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            panel1.Visible = false;
        }
        private void Visit_onValidationError(object sender, TooSharp.Core.TsExeptionArgs e)
        {
            ShowError(e.exception.Message);
        }
        void FillComboBoxServises(IEnumerable<Doctor> doctors)
        {
            foreach (var i in doctors)
            {
                comboBoxDoctorsName.Items.Add(i.Surname +" "+ i.Name + " " + i.Midle_name);
                comboBoxDoctor.Items.Add(i.Position_number);
            }
        }
        void FillComboBoxServises(IEnumerable<Servise> servises)
        {
            foreach (var i in servises)
            {
                comboBoxServisesName.Items.Add(i.Servise_name);
                comboBoxServises.Items.Add(i.Servise_number);
            }
            
        }
        void FillComboBoxPatients(IEnumerable<Patient> patients)
        {
            foreach (var i in patients)
            {
                clientsNameComboBox.Items.Add(i.Surname + " " + i.Name + " " +i.Midle_name + " " +i.Birthday);
                clientComboBoxId.Items.Add(i.Pasport_ID);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            comboBoxDoctorsName.Text = "";
            comboBoxServisesName.Text = "";
            clientsNameComboBox.Text = "";
            hours.Text = "";
            minutes.Text = "";
            seconds.Text = "";
        }
    }
}
