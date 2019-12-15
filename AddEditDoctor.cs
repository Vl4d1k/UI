using System;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using TooSharp.Models;

namespace App_FOR_DB
{
    public partial class AddEditDoctor : MaterialForm
    {
        Doctor _Doctor = null;
        Post _Post = null;
        public AddEditDoctor()
        {
            InitializeComponent();
            this.Text = "Добавление нового сотрудника";
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue700, Primary.BlueGrey500, Accent.LightBlue100, TextShade.WHITE);
        }
        public AddEditDoctor(Doctor doctor,Post post)
        {
            InitializeComponent();
            textBoxFam.Text = doctor.Surname;
            textBoxName.Text = doctor.Name;
            textBoxMN.Text = doctor.Midle_name;
            textBoxPhone.Text = doctor.Phone;
            textBoxCat.Text = post.Category;
            textBoxSpez.Text = post.Specialty;
            button1.Text = "Редактировать";
            _Doctor = doctor;
            _Post = post;
            _Doctor.onValidationError += Doctor_onValidationError;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (_Doctor == null && _Post == null)
            {
                Post pos = new Post()
                {
                    Category = textBoxCat.Text,
                    Specialty = textBoxSpez.Text
                };
                pos.Save();
                int number = pos.Position_number;
                Doctor doct = new Doctor()
                {
                    Surname = textBoxFam.Text,
                    Name = textBoxName.Text,
                    Midle_name = textBoxMN.Text,
                    Phone = textBoxPhone.Text,
                    Position_number = number
                };
                doct.onValidationError += Doctor_onValidationError;
                if ( doct.Save() > 0) MessageBox.Show("Новый сотрудник добавлен");
                else MessageBox.Show("Ошибка в добавлении");
            }
            else
            {
                _Doctor.Surname = textBoxFam.Text;
                _Doctor.Name = textBoxName.Text;
                _Doctor.Midle_name = textBoxMN.Text;
                _Doctor.Phone = textBoxPhone.Text;
                _Post.Category = textBoxCat.Text;
                _Post.Specialty = textBoxSpez.Text;
                _Post.Save();
                if (_Doctor.Save() > 0) MessageBox.Show("Пациент обновлён");
            }
            this.Close();
        }
        void ShowError(string Text)
        {
            label8.Text = Text;
            panel1.Visible = true;
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            panel1.Visible = false;
        }
        private void Doctor_onValidationError(object sender, TooSharp.Core.TsExeptionArgs e)
        {
            ShowError(e.exception.Message);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxFam.Text = "";
            textBoxName.Text = "";
            textBoxMN.Text = "";
            textBoxPhone.Text = "";
            textBoxCat.Text = "";
            textBoxSpez.Text = "";
        }
    }
}
