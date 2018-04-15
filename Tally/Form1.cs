using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Tally : Form
    {
        private int CurrentNumber=0;
        public event System.EventHandler NumChanged;
        public int Num {
            get {
                return CurrentNumber;
            }

            set {
                //#3
                CurrentNumber = value;
                UpdateNumber();
            }
        }
        private string CurOpenFile; //so we can Save after Saving as
        public Tally()
        {
            InitializeComponent();
            Num = 0;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Num++;
        }

        private void TakeAway_Click(object sender, EventArgs e)
        {
            if (Num > 0)
            {
                Num--;
            }
        }

        private void UpdateNumber()
        {
            //Update the display
            NumberDisplay.Text = Num.ToString();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            //Reset number back to 0
            Num=0;
        }

        //#2
        protected virtual void OnNumberChanged()
        {
            //create an event for when 'Num' is changed (so we don't have to explicitly call the function in Minus_Click, button1_click and Reset_Click
            if (NumChanged != null) NumChanged(this, EventArgs.Empty);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();

            save.FileName = "NewTally.txt";

            save.Filter = "Text File | *.txt";
            
            if (save.ShowDialog() == DialogResult.OK)

            {

                StreamWriter writer = new StreamWriter(save.OpenFile());

                writer.WriteLine(Num.ToString());
                writer.Dispose();

                writer.Close();
                CurOpenFile = Path.GetFullPath(save.FileName);
                Console.WriteLine(CurOpenFile);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(CurOpenFile))
            {
                string newTally = Num.ToString();

                File.WriteAllText(CurOpenFile, newTally);
            }    
        }

        private void Reset_Click_1(object sender, EventArgs e)
        {
            Num = 0;
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text File | *.txt";
            openFileDialog1.Title = "Select a Tally File";

            // Show the Dialog.  
            // If the user clicked OK in the dialog and  
            // a .CUR file was selected, open it.  
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Assign the cursor in the Stream to the Form's Cursor property.  
                string newTally = File.ReadAllText(openFileDialog1.FileName);
                Num = int.Parse(newTally);
                CurOpenFile = Path.GetFullPath(openFileDialog1.FileName);
            }
        }
    }
}


