using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace marketplace
{
    public partial class SellerForm : Form
    {
        public SellerForm()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Создание нового товара
            Product product = new Product
            {
                ID = txtID.Text,
                Name = txtName.Text,
                Price = int.Parse(txtPrice.Text),
                ImagePath = txtImagePath.Text,
                Warehouse = txtWarehouse.Text,
            Quantity = int.Parse(txtQuantity.Text)
            };

            // Сохранение товара в текстовый файл
            SaveProductToFile(product);
        }

        private void SaveProductToFile(Product product)
        {
            // Формат строки для записи в файл
            string productData = $"{product.ID},{product.Name},{product.Price},{product.ImagePath},{product.Warehouse},{product.Quantity}";

            // Запись в файл (например, products.txt)
            File.AppendAllText("products.txt", productData + Environment.NewLine);
        }
        private void LoadProducts()
        {
            // Чтение данных из текстового файла
            var products = File.ReadAllLines("products.txt")
                .Select(line => line.Split(','))
                .Select(parts => new Product
                {
                    ID = parts[0],
                    Name = parts[1],
                    Price = int.Parse(parts[2]),
                    ImagePath = parts[3],
                    Warehouse = parts[4],
                    Quantity = int.Parse(parts[5]) // Получаем количество товара
                }).ToList();

            // Привязка к DataGridView
            dataGridViewProducts.DataSource = products;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
