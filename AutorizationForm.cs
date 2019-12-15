using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using TooSharp.Models;

namespace App_FOR_DB
{
    public partial class AutorizationForm : MaterialForm
    {
        public AutorizationForm()
        {
            InitializeComponent();
            this.Text = "Вход в личный кабинет";
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue700, Primary.BlueGrey500, Accent.LightBlue100, TextShade.WHITE);
            textBox2.PasswordChar = '*';
        }
        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            var user = CheckPas(Doctors.Records().Get());
            if (user != null)
            {
                var form = new Form1(user);
                form.Show();
            }
            //this.Close();
            this.WindowState = FormWindowState.Minimized;
        }
        string CheckPas(IEnumerable<Doctor> users)
        {
            if ("admin" == textBox1.Text && "admin" == textBox2.Text) return "admin";
            foreach (var user in users)
            {
                if (Convert.ToString(user.Position_number) == textBox1.Text && user.Password == textBox2.Text) return Convert.ToString(user.Position_number);
            }
            return null;
        }
    }
}
