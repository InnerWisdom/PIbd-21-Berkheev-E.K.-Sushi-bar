using SushiBarBusinessLogic.ViewModels;
using SushiBarBusinessLogic.BusinessLogics;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace SushiBarView
{
    public partial class FormSushiIngredient : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id
        {
            get { return Convert.ToInt32(comboBoxIngredient.SelectedValue); }
            set { comboBoxIngredient.SelectedValue = value; }
        }
        public string IngredientName { get { return comboBoxIngredient.Text; } }
        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }
        public FormSushiIngredient(IngredientLogic logic)
        {
            InitializeComponent();
            List<IngredientViewModel> list = logic.Read(null);
            if (list != null)
            {
                comboBoxIngredient.DisplayMember = "IngredientName";
                comboBoxIngredient.ValueMember = "Id";
                comboBoxIngredient.DataSource = list;
                comboBoxIngredient.SelectedItem = null;
            }
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxIngredient.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();

        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
