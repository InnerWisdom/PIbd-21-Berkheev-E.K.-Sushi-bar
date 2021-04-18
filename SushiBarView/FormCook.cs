using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.BusinessLogics;
using SushiBarBusinessLogic.ViewModels;
using Unity;

namespace SushiBarView
{
    public partial class FormCook : Form
    {
        public FormCook()
        {
            InitializeComponent();
        }

        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly CookLogic logic;
        private int? id;
        private Dictionary<int, (string, int)> cookComponents;
        public FormCook(CookLogic service)
        {
            InitializeComponent();
            logic = service;
        }
        private void FormCook_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CookViewModel view = logic.Read(new CookBindingModel { Id = id.Value })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.CookFIO;
                        textBoxWork.Text = view.WorkingTime.ToString();
                        textBoxRest.Text = view.PauseTime.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxWork.Text))
            {
                MessageBox.Show("Заполните время работы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxRest.Text))
            {
                MessageBox.Show("Заполните время отдыха", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new CookBindingModel
                {
                    Id = id,
                    CookFIO = textBoxName.Text,
                    WorkingTime = Convert.ToInt32(textBoxWork.Text),
                    PauseTime = Convert.ToInt32(textBoxRest.Text),
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
    }
}
