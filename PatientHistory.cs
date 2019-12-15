using System;
using MaterialSkin.Controls;
using MaterialSkin;
using TooSharp.Models;

namespace App_FOR_DB
{
    public partial class PatientHistory : MaterialForm
    {
        public PatientHistory(Patient patient, Patient_history patient_History) 
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue700, Primary.BlueGrey500, Accent.LightBlue100, TextShade.WHITE);
            if (patient.Pasport_ID == patient_History.Client_ID)
            {
                materialLabel2.Text = patient.Surname + " " + patient.Name + " " + patient.Midle_name;
                richTextBox1.Text = patient_History.History;
            }
        }
    }
}
