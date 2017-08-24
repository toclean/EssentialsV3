using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErrorHandling
{
    public partial class ErrorPrompt : Form
    {
        public ErrorPrompt()
        {
            InitializeComponent();
        }

        public void CreateError(Exception e)
        {
            exceptionNameTb.Text = e.ToString();
            exceptionContentRtb.Text = e.Message;
            Show();
        }
    }
}
