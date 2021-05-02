using System;
using System.Windows.Forms;
using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.BusinessLogics;
using Unity;

namespace SushiBarView
{
    public partial class FormReportKitchenIngredients : Form
    {

        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ReportLogic logic;
        public FormReportKitchenIngredients(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void ButtonSaveToExcel_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveKitchenIngredientToExcelFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void FormReportKitchenIngredients_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = logic.GetKitchenIngredient();
                if (dict != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var elem in dict)
                    {
                        dataGridView.Rows.Add(new object[] { elem.KitchenName, "", "" });
                        foreach (var listElem in elem.Ingredients)
                        {
                            dataGridView.Rows.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                        }
                        dataGridView.Rows.Add(new object[] { "Итого", "", elem.TotalCount });
                        dataGridView.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
