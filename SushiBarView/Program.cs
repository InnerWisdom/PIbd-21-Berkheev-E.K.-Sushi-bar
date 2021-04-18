using SushiBarBusinessLogic.BusinessLogics;
using SushiBarBusinessLogic.Interfaces;
using SushiBarDatabaseImplement.Implements;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;


namespace SushiBarView
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IIngredientStorage, IngredientStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderStorage, OrderStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISushiStorage, SushiStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientStorage, ClientStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICookStorage, CookStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IngredientLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<OrderLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<SushiLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReportLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ClientLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<CookLogic>(new 
           HierarchicalLifetimeManager());
            return currentContainer;
        }

    }
}
