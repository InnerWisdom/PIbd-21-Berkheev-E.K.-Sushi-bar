using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.BusinessLogics;
using System;
using System.Windows.Forms;
using Unity;
namespace SushiBarView
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly OrderLogic _orderLogic;
        private readonly ReportLogic _report;
        private readonly BackUpAbstractLogic _backUpAbstractLogic;
        public FormMain(OrderLogic orderLogic, ReportLogic report,
            BackUpAbstractLogic _backUpAbstractLogic)
        {
            InitializeComponent();
            this._orderLogic = orderLogic;
            this._report = report;
            this._backUpAbstractLogic = _backUpAbstractLogic;
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


                    Program.ConfigGrid(_orderLogic.Read(null), DataGridView);

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

        private void списокКомпонентовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _report.SaveIngredientsToWordFile(new ReportBindingModel
                    {
                        FileName =
                   dialog.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                }
            }

        }

        private void компонентыПоИзделиямToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportSushiIngredients>();
            form.ShowDialog();
        }

        private void списокЗаказовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormViewReport>();
            form.ShowDialog();
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormClients>();
            form.ShowDialog();
        }

        private void исполнителиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCooks>();
            form.ShowDialog();
        }

        private void toolStripButtonStartWork_Click(object sender, EventArgs e)
        {
            var workModeling = Container.Resolve<WorkModeling>();
            workModeling.DoWork();
            MessageBox.Show("Работы запущены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }

        private void письмаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormMessages>();
            form.ShowDialog();
        }

        private void createBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_backUpAbstractLogic != null)
                {
                    var fbd = new FolderBrowserDialog();
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        _backUpAbstractLogic.CreateArchive(fbd.SelectedPath);
                        MessageBox.Show("Backup created", "Message",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }
}
