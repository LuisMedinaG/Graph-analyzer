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
        BmpManager bmpMan;
        bool[] both = new bool[2];

        public MainForm()
        {
            InitializeComponent();
            DisableAllBttns();
            bmpMan = new BmpManager();
        }

        private void DisableAllBttns()
        {
            both[0] = false;
            both[1] = false;
            bttnScanImg.Enabled = false;
            bttnAddAge.Enabled = false;
            cbAgents.Enabled = false;
            bttnCaMaLaAg.Enabled = false;
            bttnCaMaLaVer.Enabled = false;
            tbNumVer.Enabled = false;
            //Act 4
            bttnPrim.Enabled = false;
            bttnKruskal.Enabled = false;
            bttnAnimatePrim.Enabled = false;
            bttnAnimateKruskal.Enabled = false;
            bttnBoth.Enabled = false;
            //Act 5
            bttnAddHunter.Enabled = false;
            bttnAddPrey.Enabled = false;
            //Act 6
            bttnBruFor.Enabled = false;
            bttnDivCon.Enabled = false;
        }
        private void UpdateTreeView()
        {
            List<Vertex> myLc;

            myLc = myBmpProcess.GetVerL();
            treeViewCircles.Nodes.Clear();
            for(int i = 0; i < myLc.Count; i++)
            {
                treeViewCircles.Nodes.Add(myLc[i].ToString());
                foreach(Edge e in myLc[i].GetLA())
                {
                    treeViewCircles.Nodes[i].Nodes.Add(e.ToString());
                }
            }
        }
        private void ScanImage()
        {
            agentL = new List<Agent>();//<-----
            bmpMan.bmpAnalyse = new Bitmap(bmpMan.bmpOrg);
            myBmpProcess = new BmpProcessor(bmpMan.bmpAnalyse, pictureBoxImg);

            //Modifyes bmpAnalyse
            myBmpProcess.FindCircles();
            UpdateTreeView();

            pictureBoxImg.Image = bmpMan.bmpAnalyse;
            tbNumVer.Maximum = myBmpProcess.GetVerL().Count;
            tbHunters.Maximum = myBmpProcess.GetVerL().Count;

            cbInitialVertex.Items.Clear();
            foreach(Vertex v in myBmpProcess.GetVerL())
            {
                cbInitialVertex.Items.Add(v.GetId());
                cbDestinationVertex.Items.Add(v.GetId());
            }

            //___Enables bttns___
            if(myBmpProcess.GetVerL().Count > 1)
            {
                tbNumVer.Enabled = true;
                bttnAddAge.Enabled = true;
                bttnPrim.Enabled = true;
                bttnKruskal.Enabled = true;
                bttnBoth.Enabled = true;
                bttnAddPrey.Enabled = true;
                bttnBruFor.Enabled = true;
                bttnDivCon.Enabled = true;
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
                return myBmpProcess.GetVerL()[ind - 1];
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
                return myBmpProcess.GetVerL()[ind - 1];
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
                bmpMan.bmpOrg = new Bitmap(openFileDialog1.FileName);
            } catch(Exception)
            {
                MessageBox.Show("Error: Archivo no existente");
                return;
            }
            //Fill picture box with Original (UNTOCHED)
            pictureBoxImg.Image = bmpMan.bmpOrg;
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

            if(numAgents > 0 && myBmpProcess.GetVerL().Count > 1)
            {
                for(int i = 0; i < numAgents; i++)
                {
                    do
                    {
                        uniqueNum = rand.Next(myBmpProcess.GetNumCircles());
                    } while(lNum.Contains(uniqueNum));

                    lNum.Add(uniqueNum);
                    //NewNum(lNum, numAgents);
                    org = myBmpProcess.GetVerL()[uniqueNum];
                    lAgents.Add(new Agent(org, i));
                }
                //Animate list of agents
                myBmpProcess.AnimateLAgents(lAgents);

                //___Enables bttns___
                bttnCaMaLaAg.Enabled = true;
                bttnCaMaLaVer.Enabled = true;
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
        private void BttnCaMaLaVer_Click( object sender, EventArgs e )
        {
            bmpMan.bmpRoad = new Bitmap(bmpMan.bmpAnalyse);

            myBmpProcess.DrawAgentRoad(lAgents[0], bmpMan.bmpRoad, treeViewCaMaLa);
            pictureBoxImg.Image = bmpMan.bmpRoad;
        }
        private void BttnCaMaLa_Click( object sender, EventArgs e )
        {
            object obj = cbAgents.SelectedItem;
            int index;
            if(obj != null)
            {
                bmpMan.bmpRoad = new Bitmap(bmpMan.bmpAnalyse);
                Int32.TryParse(obj.ToString(), out index);

                try
                {
                    Agent agn = lAgents[index];
                    myBmpProcess.DrawAgentRoad(agn, bmpMan.bmpRoad, treeViewCaMaLa);
                } catch(Exception)
                {
                    return;
                }
                pictureBoxImg.Image = bmpMan.bmpRoad;
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
            bmpMan.bmpTree = new Bitmap(bmpMan.bmpAnalyse);
            alg = new Algorithms(myBmpProcess.GetVerL());
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
                myBmpProcess.DrawARM(bmpMan.bmpTree, ARM_Prim, new SolidBrush(Color.Orange));
                pictureBoxImg.Image = bmpMan.bmpTree;
                UpdateTreeARM(ARM_Prim, "PRIM: ", TreeViewPrim);
                //bttn Enabled
                bttnAnimatePrim.Enabled = true;
                both[0] = true;
            }
        }
        private void BttnKruskal_Click( object sender, EventArgs e )
        {
            //ScanImage();

            bmpMan.bmpTree = new Bitmap(bmpMan.bmpAnalyse);
            alg = new Algorithms(myBmpProcess.GetVerL());

            ARM_Kruskal = alg.Kruskal();
            //Draw ARM 
            myBmpProcess.DrawARM(bmpMan.bmpTree, ARM_Kruskal, new SolidBrush(Color.Orange));
            pictureBoxImg.Image = bmpMan.bmpTree;
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
                    myBmpProcess.AnimateARM(bmpMan.bmpTree, ARM_Kruskal);
                }
            }).Start();
        }
        private void BttnAnimatePrim_Click( object sender, EventArgs e )
        {
            bttnAnimatePrim.Enabled = false;

            new Thread(() => {
                lock(this)
                {
                    myBmpProcess.AnimateARM(bmpMan.bmpTree, ARM_Prim);
                }
            }).Start();
        }
        private void BttnBoth_Click( object sender, EventArgs e )
        {
            if(both[0] && both[1])
            {
                try
                {
                    myBmpProcess.DrawARM(bmpMan.bmpTree, ARM_Kruskal, new SolidBrush(Color.Red));
                    myBmpProcess.DrawARM(bmpMan.bmpTree, ARM_Prim, new SolidBrush(Color.Purple));
                } catch(Exception)
                {
                    return;
                }
                pictureBoxImg.Image = bmpMan.bmpTree;
            }
        }

        //Act 5
        private void BttnAddPrey_Click( object sender, EventArgs e )
        {
            Agent Prey;
            Vertex vOrg, vDes;

            vOrg = SelcetedOrgVer();
            vDes = SelectedDesVer();

            if(vOrg != null && vDes != null)
            {
                //Dijkstra
                Prey = new Agent(vOrg, vDes, myBmpProcess.GetVerL());
                agentL.Add(Prey);

                //Draw Prey
                Bitmap bmpPrey = new Bitmap(bmpMan.bmpAnalyse);
                myBmpProcess.DrawAgent(Prey.CurrPoint, bmpMan.purpleBrsh, bmpPrey);
                pictureBoxImg.Image = bmpPrey;
                pictureBoxImg.Refresh();

                //Update Label
                labelOrgDes.Text = "Org: " + Prey.Tree.VerL[0].GetId()
                    + "   Des:" + Prey.Tree.VerL[Prey.Tree.VerL.Count - 1].GetId();
                //Enable button Add hunter
                bttnAddHunter.Enabled = true;
            } else
            {
                MessageBox.Show("No se ha seleccionado origen/destino");
            }
        }
        private void BttnAddHunter_Click( object sender, EventArgs e )
        {
            List<int> usedRanVer = new List<int>();
            int totVer = myBmpProcess.GetVerL().Count;
            int ranVer;

            for(int i = 0; i < tbHunters.Value; i++)
            {
                ranVer = NewNum(usedRanVer, totVer);
                agentL.Add(new Agent(myBmpProcess.GetVerL()[ranVer], myBmpProcess.GetVerL()));//DFS
            }
            myBmpProcess.AnimateAllAgents(bmpMan.bmpAnalyse, agentL);
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

        private void BttnBruFor_Click( object sender, EventArgs e )
        {
            myBmpProcess.ClosestPOP_BF(bmpMan.bmpAnalyse);
        }

        private void TbHunters_Scroll( object sender, EventArgs e )
        {
            labelHunters.Text = tbHunters.Value.ToString();
        }
    }
}
