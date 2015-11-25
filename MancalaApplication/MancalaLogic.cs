using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MancalaApplication
{
    class MancalaLogic
    {

        private Button[] arrayHoles;
        private bool rule1;
        private int endPocket;

        public MancalaLogic(Button[] arrayPockets)
        {
            int arrayLength = arrayPockets.Length;
            arrayHoles = new Button[arrayLength];
            for (int i = 0; i < arrayLength; i++)
            {
                arrayHoles[i] = arrayPockets[i];            
            }

        }

        public int PocketPlace(Button pocket)
        {
            int hole = -1;
            for (int i = 0; i < arrayHoles.Length; i++)
            {
                if (pocket == arrayHoles[i])
                {
                    hole = i;
                }
            }
            return hole;
        }

        public void MoveBeads(int pocket, int beads)
        {
            Rule1 = false;
            int last;
            last = pocket + beads;
            if (last > 11)
            {
                last = last - 12;
                for (int i = pocket; i < arrayHoles.Length; i++)
                {
                    if (i == pocket)
                    {
                        ArrayHoles[i].Text = "0";
                    }
                    else
                    {
                        ArrayHoles[i].Text = Convert.ToString(Convert.ToInt16(ArrayHoles[i].Text) + 1);
                    }
                }
                for (int i = 0; i <= last; i++)
                {
                    ArrayHoles[i].Text = Convert.ToString(Convert.ToInt16(ArrayHoles[i].Text) + 1);
                    if (ArrayHoles[last].Text == "1")
                    {
                        Debug.WriteLine("last text: " + ArrayHoles[last].Text);
                        Debug.WriteLine("last: " + last);
                        if (last != 5 && last != 11)
                        {
                            Rule1 = true;
                        }
                    }
                }
            }
            else
            {
                for (int i = pocket; i <= last; i++)
                {
                    if (i == pocket)
                    {
                        ArrayHoles[i].Text = "0";
                    }
                    else
                    {
                        ArrayHoles[i].Text = Convert.ToString(Convert.ToInt16(ArrayHoles[i].Text) + 1);
                        if (ArrayHoles[last].Text == "1")
                        {
                            Debug.WriteLine("last text: " + ArrayHoles[last].Text);
                            Debug.WriteLine("last: " + last);
                            if (last != 5 && last != 11)
                            {
                                Rule1 = true;
                            }
                        }
                    }
                }
            }
            EndPocket = last;
        }

        public Button[] ArrayHoles
        {
            get { return arrayHoles; }
        }

        public bool Rule1
        {
            set { rule1 = value; }
            get { return rule1; }
        }

        public int EndPocket
        {
            set { endPocket = value; }
            get { return endPocket; }
        }

    }
}
