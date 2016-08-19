using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteImposto
{
    public partial class SplashScree : Form
    {
        //feito o delegate pois o processo não ira rodar na main thread
        private delegate void ProgressDelegate(int progress);
        private ProgressDelegate del;
        public SplashScree()
        {
            InitializeComponent();
            this.PrgProcess.Maximum = 50;
            del = this.UpdateProgressInternal;
        }

        private void UpdateProgressInternal(int progress)
        {
            if (this.Handle == null)
            {
                return;
            }
            this.PrgProcess.Value = progress;
        }
        public void UpdateProgress(int progress)
        {
            this.Invoke(del, progress);
        }
        
    }
}
