using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.ViewModels;
using SushiBarBusinessLogic.BusinessLogics;
using Unity;
namespace SushiBarView
{
    public partial class FormKitchen : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly KitchenLogic kitchenLogic;

        private int? id;

        private Dictionary<int, (string, int)> kitchenIngredients;

        public FormKitchen(KitchenLogic kitchenLogic)
        {
            InitializeComponent();
            this.kitchenLogic = kitchenLogic;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Название не указано", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPerson.Text))
            {
                MessageBox.Show("Исполнитель не указан", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                kitchenLogic.CreateOrUpdate(new KitchenBindingModel
                {
                    Id = id,
                    KitchenName = textBoxName.Text,
                    ResponsiblePersonFullName = textBoxPerson.Text,
                    KitchenIngredients = kitchenIngredients
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void LoadData()
        {
            try
            {
                if (kitchenIngredients != null)
                {
                    DataGridView.Rows.Clear();
                    foreach (var sc in kitchenIngredients)
                    {
                        DataGridView.Rows.Add(new object[] { sc.Key, sc.Value.Item1, sc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FormKitchen_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    KitchenViewModel view = kitchenLogic.Read(new KitchenBindingModel { Id = id.Value })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.KitchenName;
                        textBoxPerson.Text = view.ResponsiblePersonFullName;
                        kitchenIngredients = view.KitchenIngredients;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                kitchenIngredients = new Dictionary<int, (string, int)>();
            }
        }
    }
}
