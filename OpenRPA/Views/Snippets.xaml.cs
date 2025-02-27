﻿using OpenRPA.Interfaces;
using System;
using System.Activities.Presentation.Toolbox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenRPA.Views
{
    /// <summary>
    /// Interaction logic for Snippets.xaml
    /// </summary>
    public partial class Snippets : UserControl
    {
        public Snippets()
        {
            InitializeComponent();
            DataContext = this;
            toolborder.Child = InitializeSnippets();
        }

        public   static DynamicActivityGenerator dag = new DynamicActivityGenerator("Snippets");
        public ToolboxControl InitializeSnippets()
        {
            try
            {
                var Toolbox = new ToolboxControl();
                var cs = new Dictionary<string, ToolboxCategory>();
                foreach(var s in Plugins.Snippets)
                {
                    try
                    {
                        if (!cs.ContainsKey(s.Category)) cs.Add(s.Category, new ToolboxCategory(s.Category));
                        ToolboxCategory cat = cs[s.Category];
                        var t = dag.AppendSubWorkflowTemplate(s.Name, s.Xaml);
                        cat.Add(new ToolboxItemWrapper(t, s.Name));
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.ToString());
                    }
                }
                try
                {
                    dag.Save();
                }
                catch (Exception)
                {
                }
                foreach (var c in cs)
                {
                    try
                    {
                        Toolbox.Categories.Add(c.Value);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.ToString());
                    }
                }

                return Toolbox;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "");
                MessageBox.Show("InitializeSnippets: " + ex.Message);
                return null;
            }
        }


    }
}
