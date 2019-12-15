using System;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using TooSharp.Models;

namespace App_FOR_DB
{
    public partial class AddEditPatientForm : MaterialForm
    {
        Patient _Patient = null;
        public AddEditPatientForm()
        {
            InitializeComponent();
            this.Text = "Добавление нового пациента";
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue700, Primary.BlueGrey500, Accent.LightBlue100, TextShade.WHITE);
        }
        public AddEditPatientForm(Patient patient)
        {
            InitializeComponent();
            textBox1.Text = patient.Birthday;
            textBox2.Text = patient.Surname;
            textBox3.Text = patient.Name;
            textBox4.Text = patient.Midle_name;
            textBox5.Text = patient.Sex;
            textBox6.Text = patient.Phone;
            textBox7.Text = patient.Address;
            button1.Text = "Редактировать";
            _Patient = patient;
            _Patient.onValidationError += Patient_onValidationError;
        }
        void ShowError(string Text)
        {
            label8.Text = Text;
            panel1.Visible = true;
            timer1.Start();
        }
        private void Patient_onValidationError(object sender, TooSharp.Core.TsExeptionArgs e)
        {
            ShowError(e.exception.Message);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            panel1.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (_Patient == null)
            {
                Patient_history patient_History = new Patient_history() { History = "H: " };
                patient_History.Save();
                Patient patient = new Patient()
                {
                    Birthday = textBox1.Text,
                    Surname = textBox2.Text,
                    Name = textBox3.Text,
                    Midle_name = textBox4.Text,
                    Sex = textBox5.Text,
                    Phone = textBox6.Text,
                    Address = textBox7.Text
                };
                patient.onValidationError += Patient_onValidationError;
                if (patient.Save() > 0) MessageBox.Show("Новый пациент добавлен");
                else MessageBox.Show("Ошибка в добавлении");
            }
            else
            {
                _Patient.Birthday = textBox1.Text;
                _Patient.Surname = textBox2.Text;
                _Patient.Name = textBox3.Text;
                _Patient.Midle_name = textBox4.Text;
                _Patient.Sex = textBox5.Text;
                _Patient.Phone = textBox6.Text;
                _Patient.Address = textBox7.Text;
                if (_Patient.Save() > 0) MessageBox.Show("Пациент обновлён");
            }
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        }
    }
}