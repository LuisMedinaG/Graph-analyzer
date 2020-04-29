using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;

namespace GraphAnalyser
{
    public class BmpProcessor
    {
        int totVertex;
        public Graph graph { get; set; }
        List<Agent> lAgn;
        List<Point> lPR;

        //MainForm
        Bitmap bmpAnalyse, bmpBack, bmpTemp, bmpARM;
        Brush whiteBrsh, blackBrsh, orangeBrsh, greenBrsh, purpleBrsh, brsh;
        PictureBox pbImg;
        Graphics gBmp;

        //Ctro
        public BmpProcessor( Bitmap bmpAnalyse, PictureBox pB )
        {
            this.bmpAnalyse = bmpAnalyse;
            this.totVertex = 0;
            this.pbImg = pB;

            this.graph = new Graph();

            blackBrsh = new SolidBrush(Color.Black);
            whiteBrsh = new SolidBrush(Color.White);
            orangeBrsh = new SolidBrush(Color.Orange);
            purpleBrsh = new SolidBrush(Color.Purple);
        }

        //Act 1
        public void FindCircles()
        {
            bmpTemp = new Bitmap(bmpAnalyse);

            for(int i = 0; i < bmpTemp.Width; i += 3)
                for(int j = 0; j < bmpTemp.Height; j += 3)
                    if(IsBlack(bmpTemp.GetPixel(i, j)))
                    {
                        graph.VerL.Add(CalcCircleCenter(i, j, bmpTemp));
                    }
            FindObstacles(bmpAnalyse);
            DrawIds();
        }
        private Vertex CalcCircleCenter( int x, int y, Bitmap bmp )
        {
            Color c;
            Point pCent;
            Vertex tempCircle;
            int y_i, y_f, y_act, y_c, r;
            int x_i, x_f, x_act, x_c;

            y_act = y;
            y_i = y;

            do
            {
                c = bmp.GetPixel(x, ++y_act);
            } while(IsBlack(c));
            y_f = y_act - 1;
            y_c = (y_i + y_f) / 2;

            x_act = x;
            do
            {
                c = bmp.GetPixel(++x_act, y_c);
            } while(IsBlack(c));
            x_i = x_act + 1;

            x_act = x;
            do
            {
                c = bmp.GetPixel(--x_act, y_c);
            } while(IsBlack(c));
            x_f = x_act - 1;
            x_c = (x_i + x_f) / 2;
            r = (x_i - x_f) / 2 + 1;

            pCent = new Point(x_c, y_c);
            tempCircle = new Vertex(pCent, r, ++totVertex);
            greenBrsh = new SolidBrush(Color.Green);

            DrawCircle(tempCircle, bmp, greenBrsh);
            DrawCenter(pCent, bmp);
            return tempCircle;
        }

        private void DrawCenter( Point pCent, Bitmap bmp )
        {
            Pen penBlue = new Pen(Color.Red, 4);
            gBmp = Graphics.FromImage(bmp);
            gBmp.DrawLine(penBlue, pCent, new Point(pCent.X + 10, pCent.Y));
            pbImg.Refresh();
            const int k = 8;
            for(int i = -k; i < k; i++)
                for(int j = -k; j < k; j++)
                    bmp.SetPixel(pCent.X + i, pCent.Y + j, Color.Blue);
        }

        //Act 2
        private void FindObstacles( Bitmap bmp )
        {
            double distAct;
            int contAris = 0;

            Graphics gBmp_2 = Graphics.FromImage(bmp);
            List<Point> lP, lPR;
            Vertex ori, des;
            Edge e;
            Pen penBlck;

            penBlck = new Pen(Color.Black, 4);
            bmpTemp = new Bitmap(bmp);

            for(var i = 0; i < graph.VerL.Count; i++)
                for(var j = i; j < graph.VerL.Count; j++)
                {
                    ori = graph.VerL[i];
                    des = graph.VerL[j];

                    if(ori != des)
                    {
                        distAct = GetDist(ori, des);
                        lP = new List<Point>();
                        if(DDA(ori, des, bmpTemp, lP))
                        {
                            e = new Edge(ori, des, distAct, ++contAris, lP);
                            ori.EdgL.Add(e);

                            graph.EdgL.AddLast(e);//Optional

                            lPR = new List<Point>(lP);
                            lPR.Reverse();
                            des.EdgL.Add(new Edge(des, ori, distAct, ++contAris, lPR));

                            gBmp_2.DrawLine(penBlck, ori.GetX(), ori.GetY(),
                                                   des.GetX(), des.GetY());
                        }
                    }
                }
        }
        private bool DDA( Vertex org, Vertex des, Bitmap bmp, List<Point> lP )
        {
            Color c, cL, cR;
            Point pOrg = org.GetPoint(), pDes = des.GetPoint();

            int X0 = pOrg.X, X1 = pDes.X;
            int Y0 = pOrg.Y, Y1 = pDes.Y;
            int dx = X1 - X0;
            int dy = Y1 - Y0;

            int steps = Abs(dx) > Abs(dy) ? Abs(dx) : Abs(dy);

            float Xinc = dx / ( float )steps;
            float Yinc = dy / ( float )steps;

            float X = X0;
            float Y = Y0;

            DrawCircle(org, bmp, whiteBrsh);
            DrawCircle(des, bmp, whiteBrsh);

            for(int i = 0; i <= steps; i++)
            {
                lP.Add(new Point(( int )X, ( int )Y));

                X += Xinc;
                Y += Yinc;

                c = bmp.GetPixel(( int )X, ( int )Y);
                cL = bmp.GetPixel(( int )X - 1, ( int )Y);
                cR = bmp.GetPixel(( int )X + 1, ( int )Y);

                if(!IsWhite(c) || !IsWhite(cL) || !IsWhite(cR))
                {
                    lP.Clear();
                    DrawCircle(org, bmp, blackBrsh);
                    DrawCircle(des, bmp, blackBrsh);
                    return false;
                }
            }
            DrawCircle(org, bmp, blackBrsh);
            DrawCircle(des, bmp, blackBrsh);
            return true;
        }

        //Act 3
        public void DrawAgentRoad( Agent agn, Bitmap bmp, TreeView tv )
        {
            Vertex cir;
            Edge firstA, lastA;
            int size = agn.GetUsadas().Count;

            List<Point> CaMaLa = agn.GetLPU();

            for(int i = 0; i < CaMaLa.Count; i += 2)
            {
                cir = new Vertex(CaMaLa[i], 2, -1);
                DrawCircle(cir, bmp, orangeBrsh);
            }

            firstA = agn.GetUsadas()[0];
            lastA = agn.GetUsadas()[size - 1];

            tv.Nodes.Clear();
            tv.Nodes.Add("Agent("
                + agn.GetId() + "), of vertex "
                + firstA.GetOrigen().GetId() + " to vertex "
                + lastA.GetDestino().GetId() + "\n Total distance: " +
                agn.GetDistAcu()
            );
        }
        public void DrawARM( Bitmap bmp, Graph ARM, Brush brsh )
        {
            Vertex ver;
            List<Point> lP;
            Font myFont = new Font("Arial", 14);
            gBmp = Graphics.FromImage(bmp);

            foreach(Edge ari in ARM.EdgL)
            {
                lP = ari.GetLP();

                int indexLP = lP.Count / 2;
                int x = lP[indexLP].X + 5;
                int y = lP[indexLP].Y + 5;

                gBmp.DrawString(String.Format("{0:0.00}", ari.GetPonderacion()).ToString(), myFont, blackBrsh, x, y);

                for(int j = 0; j < lP.Count; j += 2)
                {
                    ver = new Vertex(lP[j], 2, -1);
                    DrawCircle(ver, bmp, brsh);
                }
            }
        }
        public void AnimateARM( Bitmap bmp, Graph ARM )
        {
            List<Vertex> visited = new List<Vertex>();
            bmpBack = new Bitmap(bmp);
            pbImg.BackgroundImage = bmpBack;
            pbImg.BackgroundImageLayout = ImageLayout.Zoom;
            pbImg.Image = bmp;
            this.bmpARM = bmp;

            if(graph.VerL.Count > 1)
            {
                if(ARM.EdgL.Count > 0)
                {
                    DeepAnimation(ARM.EdgL.First.Value.GetOrigen(), visited, ARM);
                } else
                {
                    MessageBox.Show("Does not contain edges.");
                }
            }
        }

        //Act 4
        public void DeepAnimation( Vertex vertex, List<Vertex> visited, Graph Tree )
        {
            if(!visited.Contains(vertex))
            {
                visited.Add(vertex);
                foreach(Edge edgeA in Tree.EdgL)
                {
                    if(vertex == edgeA.GetOrigen() && !visited.Contains(edgeA.GetDestino()))
                    {
                        AnimateEdge(bmpARM, edgeA.GetLP());
                        DeepAnimation(edgeA.GetDestino(), visited, Tree);
                        lPR = new List<Point>(edgeA.GetLP());
                        lPR.Reverse();
                        if(visited.Count <= Tree.EdgL.Count)
                        {
                            AnimateEdge(bmpARM, lPR);
                        }
                    } else if(vertex == edgeA.GetDestino() && !visited.Contains(edgeA.GetOrigen()))
                    {
                        lPR = new List<Point>(edgeA.GetLP());
                        lPR.Reverse();
                        AnimateEdge(bmpARM, lPR);
                        DeepAnimation(edgeA.GetOrigen(), visited, Tree);
                        lPR = edgeA.GetLP();
                        if(visited.Count <= Tree.EdgL.Count)
                        {
                            AnimateEdge(bmpARM, lPR);
                        }
                    }
                }
            }
        }
        public void AnimateLAgents( List<Agent> lAgents )
        {
            bmpBack = new Bitmap(bmpAnalyse);
            pbImg.BackgroundImage = bmpBack;
            pbImg.BackgroundImageLayout = ImageLayout.Zoom;
            pbImg.Image = bmpAnalyse;
            lAgn = lAgents;

            List<Point> lPU;

            List<List<Point>> lLP = new List<List<Point>>();

            if(graph.EdgL.Count > 0)
            {
                lAgents.Sort(CompLAg);

                foreach(Agent A in lAgents)
                {
                    lPU = A.GetLPU();
                    lLP.Add(lPU);
                }
                DrawAllAgents(lLP);
            } else
            {
                MessageBox.Show("Does not contain edges.");
            }
        }
        private void AnimateEdge( Bitmap bmp, List<Point> lP )
        {
            for(int i = 0; i < lP.Count - 15; i += 15)
            {
                DrawAgent(lP[i], whiteBrsh, bmp);
                DrawAgent(lP[i + 1], orangeBrsh, bmp);
                pbImg.Refresh();
                ClearBitmap(bmp);
            }
        }
        private void DrawAllAgents( List<List<Point>> lLP )
        {
            int i = 0;
            List<Point> lP;

            lLP.Sort(CompLP);

            while(i < lLP[0].Count)
            {
                for(int j = 0; j < lLP.Count; j++)
                {
                    lP = lLP[j];
                    if(i < lP.Count - 10)
                    {
                        DrawAgent(lP[i], whiteBrsh, bmpAnalyse);
                        DrawAgent(lP[i + 1], orangeBrsh, bmpAnalyse);
                    }
                }
                pbImg.Refresh();
                ClearBitmap(bmpAnalyse);
                i += 10;
            }
        }

        //Act 5
        public void AnimateAllAgents( Bitmap bmp, List<Agent> agentL )
        {
            pbImg.BackgroundImage = new Bitmap(bmp);
            pbImg.BackgroundImageLayout = ImageLayout.Zoom;
            pbImg.Image = bmp;

            Agent agn, agnDanger;
            Point currP;
            Edge currE;
            int i = 0;

            while(agentL.Count > 0)
            {
                for(i = 0; i < agentL.Count; i++)
                {
                    agn = agentL[i];// Current agent
                    currE = agn.CurrEdge.Value;// Current edge
                    currP = agn.CurrPoint;// Current point

                    brsh = agn.IsPrey ? purpleBrsh : orangeBrsh;
                    if(agn.CONT_P < agn.LP.Count)// All points in graph
                    {
                        if(!currE.GetLP().Contains(currP))
                        {
                            if(!agn.IsPrey)
                            {
                                agn.CurrEdge = agn.CurrEdge.Next;// HUNTER take next edge
                            } else
                            {
                                agn.CurrEdge = agn.NextEdg();
                            }
                        }
                        if(!agn.IsPrey)
                        {
                            agn.CurrPoint = agn.NextPoint();// HUNTER, get next point of the list
            } else
                        {
                            agnDanger = agn.IsInDanger(agentL, currE);
                            if(agnDanger == null)
                            {
                                agn.CurrPoint = agn.NextPoint(); // PREY, get next point of the list
                            } else
                            {
                                if(agn.CONT_P - 10 > 0 && !agn.IsInVertex())
                                {
                                    agn.CONT_P -= 10;
                                    agn.CurrPoint = agn.LP[agn.CONT_P];
                                }
                                if(agn.IsTouchingHunter(agnDanger))
                                {
                                    MessageBox.Show("Prey was caught.");
                                    agentL.Remove(agn);
                                }
                            }
                        }
                    } else
                    {
                        if(agn.IsPrey)
                        {
                            MessageBox.Show("End of tour.");
                        }
                        agentL.Remove(agn);// Get to end, delete agent
                    }

                    if(agn.CurrPoint != agn.LP[agn.LP.Count - 1])
                        DrawAgent(new Point(agn.CurrPoint.X - 1, agn.CurrPoint.Y - 1), whiteBrsh, bmp);
                    DrawAgent(agn.CurrPoint, brsh, bmp);
                }
                pbImg.Refresh();
                ClearBitmap(bmp);
            }
        }

        //Act 6
        public void CloPoi_BruteForce( Bitmap bmp )
        {
            Vertex pOrgM, pDesM;
            gBmp = Graphics.FromImage(bmp);

            if(graph.VerL.Count > 1)
            {
                var pL = CloPoi_BruteForce(graph.VerL, graph.VerL.Count);
                pOrgM = pL[0];
                pDesM = pL[1];
                //dibujar menor dis
                gBmp.DrawLine(new Pen(Color.Pink, 3), pOrgM.GetX(), pOrgM.GetY(), pDesM.GetX(), pDesM.GetY());
            }
            pbImg.Refresh();
        }
        public void CloPoi_DivideConquer( Bitmap bmp )
        {
            var VerL = graph.VerL;
            gBmp = Graphics.FromImage(bmp);

            VerL.Sort(( x, y ) => x.GetX().CompareTo(y.GetX()));//Sort in X

            int mid = VerL.Count / 2;
            Point mP = VerL[mid].GetPoint();

            var cloPoi = Recursive_DivCon(VerL, 0, VerL.Count - 1);// O(nlog(n))
            double dist_CloPoi = GetDist(cloPoi[0], cloPoi[1]);

            var stripPoiL = new List<Point>();
            for(int i = 0; i < VerL.Count; i++)// n veces
                if(Abs(VerL[i].GetPoint().X - mP.X) < dist_CloPoi)
                    stripPoiL.Add(VerL[i].GetPoint());

            var cloPoi_Strip = StripClosest(VerL, stripPoiL.Count, dist_CloPoi);//O(n^2)
            double dist_CloPoiStrip = GetDist(cloPoi_Strip[0], cloPoi_Strip[1]);

            cloPoi = dist_CloPoi < dist_CloPoiStrip ? cloPoi : cloPoi_Strip;
            gBmp.DrawLine(new Pen(Color.Pink, 3), cloPoi[0].GetX(), cloPoi[0].GetY(),
                                                    cloPoi[1].GetX(), cloPoi[1].GetY());
            pbImg.Refresh();
        }
        private List<Vertex> CloPoi_BruteForce( List<Vertex> VerL, int n )
        {
            double distMin, distAct;
            Vertex pOrg, pDes;
            Vertex pOrgM, pDesM;

            pOrgM = VerL[0];
            pDesM = VerL[1];
            distMin = double.MaxValue;
            for(int i = 0; i < n; i++)// n veces
            {
                for(int j = i + 1; j < n; j++)// n veces - i + 1
                {
                    pOrg = VerL[i];
                    pDes = VerL[j];
                    distAct = GetDist(pOrg, pDes);
                    if(distAct < distMin)
                    {
                        pOrgM = pOrg;
                        pDesM = pDes;
                        distMin = distAct;
                    }
                }
            }
            var pL = new List<Vertex>() {
                pOrgM,
                pDesM
            };
            return pL;
        }
        private List<Vertex> Recursive_DivCon( List<Vertex> VerL, int beg, int end )
        {
            if(VerL.Count <= 3)
            {
                return CloPoi_BruteForce(VerL, VerL.Count);// O(3)
            }
            int mid = VerL.Count / 2;
            Point mP = VerL[mid].GetPoint();

            var midArr_L = VerL.GetRange(0, mid);
            var midArr_R = VerL.GetRange(mid, mid);
            var pL_L = Recursive_DivCon(midArr_L, beg, mid);// O(nLog(n))
            var pL_R = Recursive_DivCon(midArr_R, mid, end);// O(nLog(n))

            double distPL_L = GetDist(pL_L[0], pL_L[1]);
            double distPL_R = GetDist(pL_R[0], pL_R[1]);

            double minDist = distPL_L < distPL_R ? distPL_L : distPL_R;
            return distPL_L < distPL_R ? pL_L : pL_R;
        }
        private List<Vertex> StripClosest( List<Vertex> stripL, int size, double minDist )
        {
            double min = minDist;
            Vertex pOrgM, pDesM;
            pOrgM = stripL[0];
            pDesM = stripL[1];

            stripL.Sort(( x, y ) => x.GetY().CompareTo(y.GetY()));

            for(int i = 0; i < size; ++i)
                for(int j = i + 1; j < size &&
                    (stripL[j].GetPoint().Y - stripL[i].GetPoint().Y) < min; ++j)
                    if(GetDist(stripL[i], stripL[j]) < min)
                    {
                        min = GetDist(stripL[i], stripL[j]);
                        pOrgM = stripL[i];
                        pDesM = stripL[j];
                    }

            var pL = new List<Vertex>() {
                pOrgM,
                pDesM
            };
            return pL;
        }

        public void Find_BFS( Bitmap bmp, Label label )
        {
            Graph Tree;
            List<Point> lP;
            bool TreeFounded = false;
            Algorithms alg = new Algorithms(graph.VerL);

            pbImg.Image = bmp;

            foreach(Vertex v in graph.VerL)
            {
                lP = new List<Point>();
                Tree = alg.BFS(v, lP);

                if(alg.SameHeightTree(Tree, v))
                {
                    TreeFounded = true;
                    DrawCircle(v, bmp, orangeBrsh);
                    label.Text = "Vertex of origin: " + v.GetId().ToString();
                    DrawARM(bmp, Tree, orangeBrsh);
                    pbImg.Refresh();
                    break;
                }
            }
            if(!TreeFounded)
            {
                MessageBox.Show("Tree not found.");
            }
        }

        //Drawing func.
        private void DrawCircle( Vertex cir, Bitmap bmp, Brush brush )
        {
            int r = cir.GetR();
            int x = cir.GetX();
            int y = cir.GetY();
            gBmp = Graphics.FromImage(bmp);
            gBmp.FillEllipse(brush, x - r, y - r, r * 2, r * 2);
        }
        public void DrawAgent( Point p, Brush brush, Bitmap bmp )
        {
            gBmp = Graphics.FromImage(bmp);
            gBmp.FillEllipse(brush, p.X - 15, p.Y - 15, 30, 30);
        }
        private void DrawIds()
        {
            Font myFont = new Font("Arial", 16);
            gBmp = Graphics.FromImage(bmpAnalyse);

            for(int i = 0; i < graph.VerL.Count; i++)
            {
                gBmp.DrawString(graph.VerL[i].GetId().ToString(), myFont, whiteBrsh,
                    graph.VerL[i].GetX() - 8,
                    graph.VerL[i].GetY() - 9);
            }
        }

        //Sorting func.
        private static int CompLAg( Agent a_1, Agent a_2 )
        {
            int numVer1 = a_1.GetUsadas().Count - 1, numVer2 = a_2.GetUsadas().Count - 1;

            if(a_1 == null)
            {
                if(a_2 == null)
                    return 0;
                return 1;
            } else
            {
                if(a_2 == null)
                    return -1;
                if(numVer1 <= numVer2)
                {
                    if(numVer1 == numVer2)
                    {
                        if(a_1.GetDistAcu() < a_2.GetDistAcu())
                        {
                            return 1;
                        } else
                        {
                            return -1;
                        }
                    }
                    return 1;
                }
                return -1;
            }
        }
        private static int CompLP( List<Point> l_1, List<Point> l_2 )
        {
            if(l_1 == null)
            {
                if(l_2 == null)
                {
                    return 0;
                } else
                {
                    return 1;
                }
            } else
            {
                if(l_2 == null)
                {
                    return -1;
                } else if(l_1.Count < l_2.Count)
                {
                    return 1;
                } else
                {
                    return -1;
                }
            }
        }

        //Extras
        int Abs( int n )
        {
            return ((n > 0) ? n : (n * (-1)));
        }
        bool IsBlack( Color cB )
        {
            return (cB.R == 0 && cB.G == 0 && cB.B == 0);
        }
        bool IsWhite( Color cB )
        {
            return (cB.R == 255 && cB.G == 255 && cB.B == 255);
        }
        void ClearBitmap( Bitmap bmp )
        {
            gBmp = Graphics.FromImage(bmp);
            gBmp.Clear(Color.Transparent);
        }

        //Getters
        public List<Vertex> GetVerL()
        {
            return graph.VerL;
        }
        public int GetNumCircles()
        {
            return totVertex;
        }
        public double GetDist( Vertex ini, Vertex fin )
        {
            int distX = Math.Abs((ini.GetX() - fin.GetX()));
            int distY = Math.Abs((ini.GetY() - fin.GetY()));
            return Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));
        }
        public double GetDist( Point ini, Point fin )
        {
            int distX = Math.Abs((ini.X - fin.X));
            int distY = Math.Abs((ini.Y - fin.Y));
            return Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));
        }
    }
}