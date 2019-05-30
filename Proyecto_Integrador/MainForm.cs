using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Proyecto_Integrador
{
    public partial class MainForm : Form
    {
        BmpProcessor myBmpProcess;
        Random rand;
        Algorithms alg;
        List<Agent> lAgents;
        List<Agent> agentL;

        Graph ARM_Prim, ARM_Kruskal;
        BmpManager drwAni;
        bool[] both = new bool[2];

        public MainForm()
        {
            InitializeComponent();
            DisableAllBttns();
            drwAni = new BmpManager();
        }

        private void DisableAllBttns()
        {
            both[0] = false;
            both[1] = false;
            bttnScanImg.Enabled = false;
            bttnAddAge.Enabled = false;
            cbAgents.Enabled = false;
            bttnCaMaAg.Enabled = false;
            bttnCaMaVer.Enabled = false;
            tbNumVer.Enabled = false;
            bttnPrim.Enabled = false;
            bttnKruskal.Enabled = false;
            bttnAnimatePrim.Enabled = false;
            bttnAnimateKruskal.Enabled = false;
            bttnBoth.Enabled = false;
            bttnAddHunter.Enabled = false;
            bttnAddPrey.Enabled = false;
        }
        private void UpdateTreeView()
        {
            List<Vertex> myLc;

            myLc = myBmpProcess.GetLC();
            treeViewCircles.Nodes.Clear();
            for(int i = 0; i < myLc.Count; i++)
            {
                treeViewCircles.Nodes.Add(myLc[i].ToString());
                foreach(Edge e in myLc[i].getLA())
                {
                    treeViewCircles.Nodes[i].Nodes.Add(e.ToString());
                }
            }
        }
        private void ScanImage()
        {
            agentL = new List<Agent>();//<-----
            drwAni.bmpAnalyse = new Bitmap(drwAni.bmpOrg);
            myBmpProcess = new BmpProcessor(drwAni.bmpAnalyse, pictureBoxImg);

            //Modifyes bmpAnalyse
            myBmpProcess.FindCircles();
            UpdateTreeView();

            pictureBoxImg.Image = drwAni.bmpAnalyse;
            tbNumVer.Maximum = myBmpProcess.GetLC().Count;
            tbHunters.Maximum = myBmpProcess.GetLC().Count;

            cbInitialVertex.Items.Clear();
            foreach(Vertex v in myBmpProcess.GetLC())
            {
                cbInitialVertex.Items.Add(v.GetId());
                cbDestinationVertex.Items.Add(v.GetId());
            }

            //___Enables bttns___
            if(myBmpProcess.GetLC().Count > 1)
            {
                tbNumVer.Enabled = true;
                bttnAddAge.Enabled = true;
                bttnPrim.Enabled = true;
                bttnKruskal.Enabled = true;
                bttnBoth.Enabled = true;
                bttnAddPrey.Enabled = true;
            }
        }

        private Vertex SelcetedOrgVer()
        {
            string obj = cbInitialVertex.Text;
            int ind = 0;
            if(obj == "")
            {
                return null;
            } else
            {
                Int32.TryParse(obj.ToString(), out ind);
                return myBmpProcess.GetLC()[ind - 1];
            }
        }
        private Vertex SelectedDesVer()
        {
            string obj = cbDestinationVertex.Text;
            int ind = 0;
            if(obj == "")
            {
                return null;
            } else
            {
                Int32.TryParse(obj.ToString(), out ind);
                return myBmpProcess.GetLC()[ind - 1];
            }
        }
        private int NewNum( List<int> rL, int end )
        {
            int MyNumber = 0;
            do
            {
                rand = new Random();
                MyNumber = rand.Next(0, end);
            } while(rL.Contains(MyNumber));
            rL.Add(MyNumber);
            return MyNumber;
        }

        //Act 1, 2
        private void BttnLoadImgClick( object sender, EventArgs e )
        {
            DisableAllBttns();
            openFileDialog1.ShowDialog();
            try
            {
                drwAni.bmpOrg = new Bitmap(openFileDialog1.FileName);
            } catch(Exception)
            {
                MessageBox.Show("Error: Archivo no existente");
                return;
            }
            //Fill picture box
            pictureBoxImg.Image = null;
            pictureBoxImg.Image = drwAni.bmpOrg;
            //___Enables bttns___
            bttnScanImg.Enabled = true;
        }
        private void BttnScanImgClick( object sender, EventArgs e )
        {
            ScanImage();
        }

        //Act 3
        private void BttnAddAgentClick( object sender, EventArgs e )
        {
            List<int> lNum;
            int uniqueNum;
            int numAgents;
            Vertex org;

            numAgents = tbNumVer.Value;
            lNum = new List<int>();
            lAgents = new List<Agent>();
            rand = new Random();

            if(numAgents > 0 && myBmpProcess.GetLC().Count > 1)
            {
                for(int i = 0; i < numAgents; i++)
                {
                    do
                    {
                        uniqueNum = rand.Next(myBmpProcess.GetNumCircles());
                    } while(lNum.Contains(uniqueNum));

                    lNum.Add(uniqueNum);
                    //NewNum(lNum, numAgents);
                    org = myBmpProcess.GetLC()[uniqueNum];
                    lAgents.Add(new Agent(org, i));
                }
                //Animate list of agents
                myBmpProcess.AnimateLAgents(lAgents);

                //___Enables bttns___
                bttnCaMaAg.Enabled = true;
                bttnCaMaVer.Enabled = true;
                cbAgents.Enabled = true;
                bttnAddAge.Enabled = false;
                tbNumVer.Enabled = false;

                //Fill combo box
                cbAgents.Items.Clear();
                foreach(Agent a in lAgents)
                {
                    cbAgents.Items.Add(a.GetId());
                }
            }
        }
        private void BttnCaMaVer_Click( object sender, EventArgs e )
        {
            drwAni.bmpRoad = new Bitmap(drwAni.bmpAnalyse);

            myBmpProcess.DrawAgentRoad(lAgents[0], drwAni.bmpRoad, treeViewCaMaLa);
            pictureBoxImg.Image = drwAni.bmpRoad;
        }
        private void BttnCaMaLa_Click( object sender, EventArgs e )
        {
            object obj = cbAgents.SelectedItem;
            int index;
            if(obj != null)
            {
                drwAni.bmpRoad = new Bitmap(drwAni.bmpAnalyse);
                Int32.TryParse(obj.ToString(), out index);

                try
                {
                    Agent agn = lAgents[index];
                    myBmpProcess.DrawAgentRoad(agn, drwAni.bmpRoad, treeViewCaMaLa);
                } catch(Exception)
                {
                    throw;
                }
                pictureBoxImg.Image = drwAni.bmpRoad;
            }
        }
        private void UpdateTreeARM( Graph ARM, String algStr, TreeView tree )
        {
            double aW = 0;
            tree.Nodes.Clear();
            tree.Nodes.Add(algStr);

            tree.Nodes[0].Nodes.Add("Orden de aceptacion");
            for(int i = 0; i < ARM.EdgL.Count; i++)
            {
                tree.Nodes[0].Nodes.Add(i + 1 + ")  " + ARM.EdgL[i].ToTree());
                aW += ARM.EdgL[i].GetPonderacion();
            }
            tree.Nodes[0].Nodes.Add("Peso acumulado: " + String.Format("{0:0.00}", aW));
        }

        //Act 4
        private void BttnPrim_Click( object sender, EventArgs e )
        {
            drwAni.bmpTree = new Bitmap(drwAni.bmpAnalyse);
            alg = new Algorithms(myBmpProcess.GetLC());
            Vertex v = SelcetedOrgVer();

            try
            {
                ARM_Prim = alg.Prim(v);
            } catch(Exception)
            {
                return;
            }

            if(ARM_Prim != null)
            {
                //Draw ARM
                myBmpProcess.DrawARM(drwAni.bmpTree, ARM_Prim, new SolidBrush(Color.Orange));
                pictureBoxImg.Image = drwAni.bmpTree;
                UpdateTreeARM(ARM_Prim, "PRIM: ", TreeViewPrim);
                //bttn Enabled
                bttnAnimatePrim.Enabled = true;
                both[0] = true;
            }
        }
        private void BttnKruskal_Click( object sender, EventArgs e )
        {
            //ScanImage();

            drwAni.bmpTree = new Bitmap(drwAni.bmpAnalyse);
            alg = new Algorithms(myBmpProcess.GetLC());

            ARM_Kruskal = alg.Kruskal();
            //Draw ARM 
            myBmpProcess.DrawARM(drwAni.bmpTree, ARM_Kruskal, new SolidBrush(Color.Orange));
            pictureBoxImg.Image = drwAni.bmpTree;
            UpdateTreeARM(ARM_Kruskal, "KRUSKAL: ", TreeViewKruskal);
            //bttn Enabled
            bttnAnimateKruskal.Enabled = true;
            both[1] = true;
        }
        private void BttnAnimateKruskal_Click( object sender, EventArgs e )
        {
            bttnAnimateKruskal.Enabled = false;
            new Thread(() => {
                lock(this)
                {
                    myBmpProcess.AnimateARM(drwAni.bmpTree, ARM_Kruskal);
                }
            }).Start();
        }
        private void BttnAnimatePrim_Click( object sender, EventArgs e )
        {
            bttnAnimatePrim.Enabled = false;

            new Thread(() => {
                lock(this)
                {
                    myBmpProcess.AnimateARM(drwAni.bmpTree, ARM_Prim);
                }
            }).Start();
        }
        private void BttnBoth_Click( object sender, EventArgs e )
        {
            if(both[0] && both[1])
            {
                try
                {
                    myBmpProcess.DrawARM(drwAni.bmpTree, ARM_Kruskal, new SolidBrush(Color.Red));
                    myBmpProcess.DrawARM(drwAni.bmpTree, ARM_Prim, new SolidBrush(Color.Purple));
                } catch(Exception)
                {
                    return;
                }
                pictureBoxImg.Image = drwAni.bmpTree;
            }
        }

        //Act 5
        private void BttnAddPrey_Click( object sender, EventArgs e )
        {
            Vertex vOrg, vDes;

            vOrg = SelcetedOrgVer();
            vDes = SelectedDesVer();

            //agentL.Add(new Agent(vOrg, vDes));//Dijkstra

            bttnAddHunter.Enabled = true;
        }
        private void BttnAddHunter_Click( object sender, EventArgs e )
        {
            List<int> usedRanVer = new List<int>();
            int totVer = myBmpProcess.GetLC().Count;
            int ranVer;

            for(int i = 0; i < tbHunters.Value; i++)
            {
                ranVer = NewNum(usedRanVer, totVer);
                agentL.Add(new Agent(myBmpProcess.GetLC()[ranVer]));//DFS
            }
            myBmpProcess.AnimateAllAgents(drwAni.bmpAnalyse, agentL);
        }

        //Extras
        private void CbInitialVertex_Click( object sender, EventArgs e )
        {
            cbInitialVertex.DroppedDown = true;
        }
        private void TreeViewCircles_DoubleClick( object sender, EventArgs e )
        {
            treeViewCircles.ExpandAll();
        }
        private void TreeViewARM_DoubleClick( object sender, EventArgs e )
        {
            TreeViewPrim.ExpandAll();
        }
        private void TrackBarNumVer_Scroll( object sender, EventArgs e )
        {
            labelNumVer.Text = tbNumVer.Value.ToString();
        }
        private void TbHunters_Scroll( object sender, EventArgs e )
        {
            labelHunters.Text = tbHunters.Value.ToString();
        }
    }
}
