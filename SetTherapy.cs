using System;
using MaterialSkin.Controls;
using MaterialSkin;
using TooSharp.Models;
using System.Collections.Generic;

namespace App_FOR_DB
{
    public partial class SetTherapy : MaterialForm
    {
        Visit _Visit = null;
        Treatment _Treatment = null;
        Patient _Patient = null;
        public SetTherapy()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue700, Primary.BlueGrey500, Accent.LightBlue100, TextShade.WHITE);
        }
        public SetTherapy(Patient patient, Treatment treatment, Visit visit)
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue700, Primary.BlueGrey500, Accent.LightBlue100, TextShade.WHITE);
            materialLabel3.Text = patient.Surname + " " + patient.Name + " " + patient.Midle_name;
            diagnozTextBox.Text = treatment.Diagnos;
            therapyTextBox.Text = treatment.Therapy;
            _Visit = visit;
            _Treatment = treatment;
            _Patient = patient;
        }
        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            _Treatment.Therapy = therapyTextBox.Text;
            _Treatment.Diagnos = diagnozTextBox.Text;
            _Treatment.Save();
            string history = "Дата: " + _Visit.Date.ToString() + " " + diagnozTextBox.Text + ";" + "\n";
            int num = GetPatientsByTherapyNumber(Visits.Records().Get(), _Treatment.Therapy_number);
            SaveHistory(Patient_histories.Records().Get(), num, history);
            this.Close();
        }
        int GetPatientsByTherapyNumber(IEnumerable<Visit> visits, int num)
        {
            foreach (var vis in visits)
            {
                if (vis.Therapy_number == num)
                    return vis.Pasport_ID;
            }
            return -1;
        }
        void SaveHistory(IEnumerable<Patient_history> histories, int id, string history)
        {
            foreach(var his in histories)
            {
                if(his.Client_ID == id)
                {
                    string str = his.History + history;
                    his.History = str;
                    his.Save();
                }
            }
        }
    }
}
