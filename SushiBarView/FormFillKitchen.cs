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
using SushiBarBusinessLogic.BusinessLogics;
using SushiBarBusinessLogic.ViewModels;
using Unity;

namespace SushiBarView
{
    public partial class FormFillKitchen : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly KitchenLogic kitchenLogic;

        public int ComponentId
        {
            get { return Convert.ToInt32(comboBoxIngredient.SelectedValue); }
            set { comboBoxIngredient.SelectedValue = value; }
        }

        public int StoreHouse
        {
            get { return Convert.ToInt32(comboBoxIngredient.SelectedValue); }
            set { comboBoxIngredient.SelectedValue = value; }
        }

        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set { textBoxCount.Text = value.ToString(); }
        }

        public FormFillKitchen(IngredientLogic logicIngredient, KitchenLogic logicKitchen)
        {
            InitializeComponent();
            kitchenLogic = logicKitchen;
            List<KitchenViewModel> listKitchen = logicKitchen.Read(null);
            if (listKitchen != null)
            {
                comboBoxKitchen.DisplayMember = "KitchenName";
                comboBoxKitchen.ValueMember = "Id";
                comboBoxKitchen.DataSource = listKitchen;
                comboBoxKitchen.SelectedItem = null;
            }
            List<IngredientViewModel> listIngredient = logicIngredient.Read(null);
            if (listIngredient != null)
            {
                comboBoxIngredient.DisplayMember = "IngredientName";
                comboBoxIngredient.ValueMember = "Id";
                comboBoxIngredient.DataSource = listIngredient;
                comboBoxIngredient.SelectedItem = null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Количество не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxKitchen.SelectedValue == null)
            {
                MessageBox.Show("Кухня не выбрана", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxIngredient.SelectedValue == null)
            {
                MessageBox.Show("Выберите ингредиент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                kitchenLogic.Refill(new KitchenFillBindingModel
                {
                    IngredientId = Convert.ToInt32(comboBoxIngredient.SelectedValue),
                    KitchenId = Convert.ToInt32(comboBoxKitchen.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
