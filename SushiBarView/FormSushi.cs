using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.BusinessLogics;
using SushiBarBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
namespace SushiBarView
{
    public partial class FormSushi : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly SushiLogic logic;
        private int? id;
        private Dictionary<int, (string, int)> sushiComponents;
        public FormSushi(SushiLogic service)
        {
            InitializeComponent();
            this.logic = service;
        }
        private void FormProduct_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SushiViewModel view = logic.Read(new SushiBindingModel
                    {
                        Id =id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.SushiName;
                        textBoxPrice.Text = view.Price.ToString();
                        sushiComponents = view.SushiComponents;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                sushiComponents = new Dictionary<int, (string, int)>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (sushiComponents != null)
                {
                    DataGridView.Rows.Clear();
                    foreach (var pc in sushiComponents)
                    {
                        DataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1,
pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSushiIngredient>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (sushiComponents.ContainsKey(form.Id))
                {
                    sushiComponents[form.Id] = (form.IngredientName, form.Count);
                }
                else
                {
                    sushiComponents.Add(form.Id, (form.IngredientName, form.Count));
                }
                LoadData();
            }
        }
        private void ButtonUpd_Click(object sender, EventArgs e)
        {
            if (DataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormSushiIngredient>();
                int id = Convert.ToInt32(DataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = sushiComponents[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    sushiComponents[form.Id] = (form.IngredientName, form.Count);
                    LoadData();
                }
            }
        }
        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (DataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        sushiComponents.Remove(Convert.ToInt32(DataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (sushiComponents == null || sushiComponents.Count == 0)
            {
                MessageBox.Show("Заполните ингредиенты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new SushiBindingModel
                {
                    Id = id,
                    SushiName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    SushiIngredients = sushiComponents
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}