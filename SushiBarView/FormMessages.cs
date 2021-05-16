using System;
using System.Linq;
using System.Windows.Forms;
using SushiBarBusinessLogic.BusinessLogics;
using SushiBarBusinessLogic.ViewModels;
using SushiBarBusinessLogic.BindingModels;
using System.Reflection;
using Unity;

namespace SushiBarView
{
    public partial class FormMessages : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly MailLogic logic;
        private bool hasNext = false;

        private readonly int mailsOnPage = 2;

        private int currentPage = 0;
        public FormMessages(MailLogic logic)
        {
            InitializeComponent();
            if (mailsOnPage < 1) { mailsOnPage = 5; }
            this.logic = logic;
        }
        private void FormMessages_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = logic.Read(new MessageInfoBindingModel { ToSkip = currentPage * mailsOnPage, ToTake = mailsOnPage + 1 });
                var method = typeof(Program).GetMethod("ConfigGrid");
                MethodInfo generic = method.MakeGenericMethod(typeof(MessageInfoViewModel));
                hasNext = !(list.Count() <= mailsOnPage);
                if (hasNext)
                {
                    buttonNext.Text = "Next " + (currentPage + 2);
                    buttonNext.Enabled = true;
                }
                else
                {
                    buttonNext.Text = "Next";
                    buttonNext.Enabled = false;
                }
                generic.Invoke(this, new object[] { list.Take(mailsOnPage).ToList(), dataGridView });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
            }
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if ((currentPage - 1) >= 0)
            {
                currentPage--;
                textBoxPage.Text = (currentPage + 1).ToString();
                buttonNext.Enabled = true;
                buttonNext.Text = "Next " + (currentPage + 2);
                if (currentPage == 0)
                {
                    buttonPrev.Enabled = false;
                    buttonPrev.Text = "Prev";
                }
                else
                {
                    buttonPrev.Text = "Prev " + (currentPage);
                }
                LoadData();
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (hasNext)
            {
                currentPage++;
                textBoxPage.Text = (currentPage + 1).ToString();
                buttonPrev.Enabled = true;
                buttonPrev.Text = "Prev " + (currentPage);
                LoadData();
            }
        }
    }
}
