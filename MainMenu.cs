using System;
using MaterialSkin.Controls;
using MaterialSkin;

namespace App_FOR_DB
{
    public partial class Form1 : MaterialForm
    {
        string User;
        public Form1()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue800, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }
        public Form1(string user)
        {
            InitializeComponent();
            User = user;
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue800, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            if (user == "admin")
            {
                materialFlatButton5.Visible = false;
            }
            else
            {
                materialFlatButton1.Visible = false;
                materialFlatButton2.Visible = false;
                materialFlatButton3.Visible = false;
                materialFlatButton4.Visible = false;
            }
        }
        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            Patients_View patients_View = new Patients_View();
            patients_View.ShowDialog();
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            Servises_View servises_View = new Servises_View();
            servises_View.ShowDialog();
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            Doctors_View doctors_View = new Doctors_View();
            doctors_View.ShowDialog();
        }
        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            Visits_View visits_View = new Visits_View();
            visits_View.ShowDialog();
        }
        private void materialFlatButton5_Click(object sender, EventArgs e)
        {
            Visits_ViewForDoctor visits_ViewForDoctor = new Visits_ViewForDoctor(User);
            visits_ViewForDoctor.ShowDialog();
        }
    }
}
