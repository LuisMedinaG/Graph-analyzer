using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GraphAnalyser
{
    public partial class MainForm : Form
    {
        BmpProcessor myBmpProcess;
        Random rand;
        Algorithms alg;
        List<Agent> lAgents;
        List<Agent> agentL;
        List<int> usedRanVer;

        Graph ARM_Prim, ARM_Kruskal;
        BmpManager bmpMan;
        bool[] both = new bool[2];

        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;//Threads Warnig (Ilegal Cross) [OFF]
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
            // 4
            bttnPrim.Enabled = false;
            bttnKruskal.Enabled = false;
            bttnAnimatePrim.Enabled = false;
            bttnAnimateKruskal.Enabled = false;
            bttnBoth.Enabled = false;
            // 5
            bttnAddHunter.Enabled = false;
            bttnAddPrey.Enabled = false;
            // 6
            bttnBruFor.Enabled = false;
            bttnDivCon.Enabled = false;
            bttnFinBFS.Enabled = false;

            tbHunters.Enabled = false;
        }
        private void UpdateTreeView()
        {
            var myLc = myBmpProcess.GetVerL();

            treeViewCircles.Nodes.Clear();
            for(int i = 0; i < myLc.Count; i++)
            {
                treeViewCircles.Nodes.Add(myLc[i].ToString());
                foreach(Edge e in myLc[i].GetEdgL())
                {
                    treeViewCircles.Nodes[i].Nodes.Add(e.ToString());
                }
            }
        }
        private void ScanImage()
        {
            usedRanVer = new List<int>();
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
            cbDestinationVertex.Items.Clear();
            foreach(Vertex v in myBmpProcess.GetVerL())
            {
                cbInitialVertex.Items.Add(v.GetId());
                cbDestinationVertex.Items.Add(v.GetId());
            }

            //___Enables bttns___
            if(myBmpProcess.GetVerL().Count > 1 && myBmpProcess.graph.EdgL.Count > 0)
            {
                tbNumVer.Enabled = true;
                bttnAddAge.Enabled = true;
                bttnPrim.Enabled = true;
                bttnKruskal.Enabled = true;
                bttnBoth.Enabled = true;
                bttnAddPrey.Enabled = true;
                bttnBruFor.Enabled = true;
                bttnDivCon.Enabled = true;
                bttnFinBFS.Enabled = true;

                tbHunters.Enabled = true;

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

        // 1, 2
        private void BttnLoadImgClick( object sender, EventArgs e )
        {
            DisableAllBttns();
            openFileDialog1.ShowDialog();
            try
            {
                bmpMan.bmpOrg = new Bitmap(openFileDialog1.FileName);
            } catch(Exception)
            {
                MessageBox.Show("Error: The file does not exist.");
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

            if(myBmpProcess.graph.EdgL.Count > 0)
            {
                if(numAgents > 0 && myBmpProcess.GetVerL().Count > 1)
                {
                    for(int i = 0; i < numAgents; i++)
                    {
                        uniqueNum = NewNum(lNum, numAgents);
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
            } else
            {
                MessageBox.Show("Does not contain edges.");
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
        private void UpdateTreeARM( Graph ARM, String algStr, TreeView tV )
        {
            double aW = 0;
            tV.Nodes.Clear();
            tV.Nodes.Add(algStr);

            tV.Nodes[0].Nodes.Add("Acceptance order");
            foreach(Edge e in ARM.EdgL)
            {
                tV.Nodes[0].Nodes.Add(e.GetId() + 1 + ")  " + e.ToTree());
                aW += e.GetPonderacion();
            }
            tV.Nodes[0].Nodes.Add("Accumulated weight: " + String.Format("{0:0.00}", aW));
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
            bool AgentFoundFlag = false;

            vOrg = SelcetedOrgVer();
            vDes = SelectedDesVer();

            usedRanVer.Clear();

            if(vOrg != null && vDes != null)
            {
                if(vOrg != vDes)
                {
                    var bmpPrey = new Bitmap(bmpMan.bmpAnalyse);
                    foreach(Agent a in agentL)
                    {
                        if(a.IsPrey && a.CurrEdge.Value.GetOrigen() == vOrg)
                        {
                            AgentFoundFlag = true;
                        }
                    }
                    if(AgentFoundFlag == false)
                    {
                        Prey = new Agent(vOrg, vDes, myBmpProcess.GetVerL(), agentL.Count);

                        if(Prey.Tree != null)
                        {
                            //Draw agent
                            myBmpProcess.DrawAgent(Prey.CurrPoint, bmpMan.purpleBrsh, bmpPrey);

                            //Add to agentList
                            agentL.Add(Prey);
                            usedRanVer.Add(vOrg.GetId());//Agregar a la lista de numeros Aletorios

                            pictureBoxImg.Image = bmpPrey;
                            pictureBoxImg.Refresh();

                            tbHunters.Maximum--;
                            //Update Label
                            labelOrgDes.Text = "Org: " + Prey.Tree.VerL[0].GetId()
                                + "   Des:" + Prey.Tree.VerL[Prey.Tree.VerL.Count - 1].GetId();
                            //Enable button Add hunter
                            bttnAddHunter.Enabled = true;
                        }

                    } else
                    {
                        MessageBox.Show("There is already an agent with that origin.");
                    }
                } else
                {
                    MessageBox.Show("Origin and destination have to be different.");
                }
            } else
            {
                MessageBox.Show("Origin / destination not selected.");
            }
        }
        private void BttnAddHunter_Click( object sender, EventArgs e )
        {
            int totVer = myBmpProcess.GetVerL().Count;
            int ranVer;

            for(int i = 0; i < tbHunters.Value; i++)
            {
                //Thread.Sleep(1999);
                ranVer = NewNum(usedRanVer, totVer);
                agentL.Add(new Agent(myBmpProcess.GetVerL()[ranVer], myBmpProcess.GetVerL(), agentL.Count));//DFS
            }
            //Thread thr = new Thread(() => {
            myBmpProcess.AnimateAllAgents(bmpMan.bmpAnalyse, agentL);
            //});
            //thr.Start();
        }

        //Act 6
        private void BttnBruFor_Click( object sender, EventArgs e )
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            myBmpProcess.CloPoi_BruteForce(bmpMan.bmpAnalyse);//Process
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            labelTBF.Text = String.Format("{0:0.00} ms", elapsedMs);
        }
        private void BttnDivCon_Click( object sender, EventArgs e )
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            myBmpProcess.CloPoi_DivideConquer(bmpMan.bmpAnalyse);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            labelTDC.Text = String.Format("{0:0.00} ms", elapsedMs);
        }

        //Proy. Integrador
        private void BttnFinBFS_Click( object sender, EventArgs e )
        {
            myBmpProcess.Find_BFS(bmpMan.bmpAnalyse, labelFindBFS);
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
