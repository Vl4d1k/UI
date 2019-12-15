using System;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using TooSharp.Models;

namespace App_FOR_DB
{
    public partial class AddEditServises : MaterialForm
    {
        Servise _Servise = null;
        public AddEditServises()
        {
            InitializeComponent();
            this.Text = "Добавление новой услуги";
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue700, Primary.BlueGrey500, Accent.LightBlue100, TextShade.WHITE);
        }
        public AddEditServises(Servise servise)
        {
            InitializeComponent();
            textBox2.Text = Convert.ToString(servise.Cost);
            textBox3.Text = servise.Servise_name;
            button1.Text = "Обновление";
            _Servise = servise;
            _Servise.onValidationError += Servise_onValidationError;

        }
        private void Servise_onValidationError(object sender, TooSharp.Core.TsExeptionArgs e)
        {
            ShowError(e.exception.Message);
        }
        void ShowError(string Text)
        {
            label1.Text = Text;
            panel1.Visible = true;
            timer1.Start();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (_Servise == null)
            {
                Servise servise = new Servise()
                {
                    Cost = int.Parse(textBox2.Text),
                    Servise_name = textBox3.Text
                };
                servise.onValidationError += Servise_onValidationError;
                if (servise.Save() > 0) MessageBox.Show("Услуга добавлена");
                else MessageBox.Show("Ошибка в добавлении");
            }
            else
            {
                _Servise.Cost = int.Parse(textBox2.Text);
                _Servise.Servise_name = textBox3.Text;
                if (_Servise.Save() > 0) MessageBox.Show("Услуга изменена");
            }
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
        }
    }
}
