using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace marketplace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ShowLoginPanel();
            pictureBox1.Image = Image.FromFile("D:\\zadacki\\marketplace\\marketplace\\bin\\Debug\\kk.jpg");

            txtLoginPassword.Enter += txtRegPassword_Enter;
            txtLoginPassword.Leave += txtRegPassword_Leave;
            txtLoginUsername.Enter += txtLoginUsername_Enter;
            txtLoginUsername.Leave += txtLoginUsername_Leave;
            SoundPlayer sndPlayer = new SoundPlayer(@"C:\Users\HUAWEI\Desktop\eb.wav");
            sndPlayer.Play();
        }
        private bool isRegistered = false; // Флаг для проверки регистрации
        private string userDataFilePath = "enter.txt";
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void LoadMarketplaceImage()
        {
           

        }
        private void loginPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void loginPanel_VisibleChanged(object sender, EventArgs e)
        {

        }
        private void ShowLoginPanel()
        {
            // Скрываем панель регистрации и показываем панель входа
            registrationPanel.Visible = false;
            loginPanel.Visible = true;
        }

        private void ShowRegistrationPanel()
        {
            // Скрываем панель входа и показываем панель регистрации
            loginPanel.Visible = false;
            registrationPanel.Visible = true;
            registrationPanel.Location = new Point(0, 0) ;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtLoginUsername.Text.Trim();
            string password = txtLoginPassword.Text.Trim();

            // Проверка существования пользователя
            string role = IsUserRegistered(username, password);
            if (role != null)
            {
                MessageBox.Show("Вход успешен!");
                this.Hide(); // Скрываем текущую форму

                if (role == "Продавец")
                {
                    SellerForm sellerForm = new SellerForm();
                    sellerForm.Show(); // Открываем форму продавца
                }
                else if (role == "Покупатель")
                {
                    ProductsForm buyerForm = new ProductsForm();
                    buyerForm.Show(); // Открываем форму покупателя
                }
            }
            else
            {
                MessageBox.Show("Данные введены некорректно либо пользователь не зарегистрирован!!!");
            }
        }
        private string IsUserRegistered(string username, string password)
        {
            

            // Чтение всех строк из файла
            var lines = File.ReadAllLines(userDataFilePath);

            // Перебор строк для поиска пользователя
            foreach (var line in lines)
            {
                var parts = line.Split(',');

                // Проверка, что строка содержит нужное количество элементов
                if (parts.Length == 4)
                {
                    string fileUsername = parts[0].Trim();
                    string filePassword = parts[1].Trim();
                    string fileMail = parts[2].Trim();
                    string role = parts[3].Trim();

                    // Проверка совпадения имени пользователя и пароля
                    if (fileUsername.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                        filePassword.Equals(password))
                    {
                        return role; // Возвращаем роль пользователя
                    }
                }
            }

            return null; // Пользователь не найден
        }

        private bool IsUserRegistered2(string username, string password)
        {
            // Проверка наличия пользователя в файле
            string[] lines = File.ReadAllLines(userDataFilePath);
            foreach (string line in lines)
            {
                string[] userData = line.Split(',');
                if (userData[0] == username && userData[1] == password)
                    return true;
            }
            return false;
        }

        private bool IsValidUsername(string username)
        {
            // Проверка на соответствие имени пользователя регулярному выражению
            return Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$");
        }
        private void SaveUserData(string username, string password, string email, string roles)
        {
            // Сохранение данных пользователя в файл
            File.AppendAllText(userDataFilePath, $"{username},{password},{email},{roles}{Environment.NewLine}");
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtRegUsername.Text.Trim();
            string password = txtRegPassword.Text.Trim();
            string email = txtRegEmail.Text.Trim();
            string roles = "";

            // Проверка имени пользователя
            if (!IsValidUsername(username))
            {
                MessageBox.Show("Имя пользователя может содержать только буквы, цифры и нижнее подчеркивание.");
                return;
            }

            // Проверка пароля
            if (password.Length < 8)
            {
                MessageBox.Show("Пароль должен быть не менее 8 символов.");
                return;
            }

            // Проверка существования пользователя
            if (IsUserRegistered2(username, email))
            {
                MessageBox.Show("Пользователь с таким именем пользователя или электронной почтой уже существует.");
                return;
            }

            if (chkSeller.Checked)
                roles += "Продавец ";
            if (chkBuyer.Checked)
                roles += "Покупатель ";

            // Сохранение данных пользователя в файл
            SaveUserData(username, password, email, roles.Trim());
            MessageBox.Show($"Регистрация успешна! Выбранные роли: {roles.Trim()}");
            ShowLoginPanel();
        }

        private void linkToRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowRegistrationPanel();

        }

        private void registrationPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtLoginPassword_Leave(object sender, EventArgs e)
        {

        }

        private void txtRegPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLoginPassword.Text))
            {
                txtLoginPassword.Text = "Пароль";
                /*    txtRegPassword.UseSystemPasswordChar = false;*/
                txtLoginPassword.ForeColor = Color.Black;
            }
        }

        private void txtRegPassword_Enter(object sender, EventArgs e)
        {
            if (txtLoginPassword.Text == "Пароль")
            {
                txtLoginPassword.Text = "";
                /* txtRegPassword.UseSystemPasswordChar = true;*/
                txtLoginPassword.ForeColor = Color.Black;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtLoginUsername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLoginUsername.Text))
            {
                txtLoginUsername.Text = "Логин";
                txtLoginUsername.ForeColor = Color.Black; // Устанавливаем цвет подсказки
            }
        }

        private void txtLoginUsername_Enter(object sender, EventArgs e)
        {
            
             if (txtLoginUsername.Text == "Логин")
            {
                txtLoginUsername.Text = "";
                txtLoginUsername.ForeColor = Color.Black; // Устанавливаем цвет текста
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Переключение видимости пароля
            if (txtLoginPassword.UseSystemPasswordChar==true)
            {
                // Показываем пароль
                txtLoginPassword.UseSystemPasswordChar = false;
               
            }
            else
            {
                // Скрываем пароль
                txtLoginPassword.UseSystemPasswordChar = true;
               
            }

        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }
    }
}
