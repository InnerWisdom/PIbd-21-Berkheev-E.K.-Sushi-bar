using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.BusinessLogics;
using SushiBarBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Unity;
namespace SushiBarView
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly OrderLogic _orderLogic;

        public FormMain(OrderLogic orderLogic)
        {
            InitializeComponent();
            this._orderLogic = orderLogic;
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {

                var list = _orderLogic.Read(null);
                if (list!=null)
                {

                    DataGridView.DataSource = list;
                    DataGridView.Columns[0].Visible = false;

                    DataGridView.Columns[1].Visible = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void КомпонентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormIngredients>();
            form.ShowDialog();
        }
        private void ИзделияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSushis>();
            form.ShowDialog();
        }
        private void ButtonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCreateOrder>();
            form.ShowDialog();
            LoadData();
        }
        private void ButtonTakeOrderInWork_Click(object sender, EventArgs e)
        {
            if (DataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(DataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel
                    {
                        OrderId =
                   id
                    });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void ButtonOrderReady_Click(object sender, EventArgs e)
        {
            if (DataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(DataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    _orderLogic.FinishOrder(new ChangeStatusBindingModel
                    {
                        OrderId = id
                    });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void ButtonPayOrder_Click(object sender, EventArgs e)
        {
            if (DataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(DataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    _orderLogic.PayOrder(new ChangeStatusBindingModel { OrderId = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void кухниToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormKitchens>();
            form.ShowDialog();
        }

        private void заполнениеКухниToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormFillKitchen>();
            form.ShowDialog();
        }

        private void toolStripDropDownButtonFillKitchen_Click(object sender, EventArgs e)
        {

            var form = Container.Resolve<FormFillKitchen>();
            form.ShowDialog();
        }

        private void toolStripButtonFillKitchen_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormFillKitchen>();
            form.ShowDialog();
        }
    }
}
