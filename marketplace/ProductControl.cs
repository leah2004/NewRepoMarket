using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace marketplace
{
    public partial class ProductControl : UserControl
    {
        public ProductControl( )
        {
            InitializeComponent();
            AddToCart.Click += (s, e) => AddToCartClicked?.Invoke(this, EventArgs.Empty);
        }
        public string ProductNname
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public int ProductPrice
        {
            get
            {
                int price;
                if (int.TryParse(label2.Text, out price))
                {
                    return price;
                }
                else
                {
                    // Обработка ошибки: например, можно вернуть 0 или выбросить исключение
                    throw new FormatException("Неверный формат цены.");
                }
            }
            set
            {
                label2.Text = value.ToString("F0"); // Форматируем как целое число
            }
            /*get { return int.TryParse(label2.Text); }
            set { label2.Text = value.ToString("F2"); }*/
        }
        public string ImageName { get; set; } // Свойство для хранения имени изображения
        public void LoadImage()
        {
            string imagePath = System.IO.Path.Combine("D:\\zadacki\\marketplace\\marketplace\\bin\\Debug", ImageName); // Предполагаем, что изображения находятся в папке "Images"
            if (System.IO.File.Exists(imagePath))
            {
                pictureBox1.Image = Image.FromFile(imagePath);
            }
            else
            {
                pictureBox1.Image = null; // Или установить изображение по умолчанию
            }
        }

        public event EventHandler AddToCartClicked;
        private void ProductControl_Load(object sender, EventArgs e)
        {

        }

        private void AddToCart_Click(object sender, EventArgs e)
        {

        }
    }
}
