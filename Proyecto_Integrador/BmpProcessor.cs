using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;

namespace Proyecto_Integrador
{
    public class BmpProcessor
    {
        int totVertex;
        List<Vertex> lC;
        List<Agent> lAgn;
        List<Point> lPR;

        //MainForm
        Bitmap bmpAnalyse, bmpBack, bmpTemp, bmpARM;
        Brush whiteBrsh, blackBrsh, orangeBrsh, greenBrsh;
        PictureBox pbImg;
        Graphics gBmp;

        //Ctro
        public BmpProcessor( Bitmap bmpAnalyse, PictureBox pB )
        {
            this.bmpAnalyse = bmpAnalyse;
            this.totVertex = 0;
            this.pbImg = pB;

            this.lC = new List<Vertex>();

            blackBrsh = new SolidBrush(Color.Black);
            whiteBrsh = new SolidBrush(Color.White);
            orangeBrsh = new SolidBrush(Color.Orange);
        }

        //Act 1
        public void FindCircles()
        {
            bmpTemp = new Bitmap(bmpAnalyse);


            for(int i = 0; i < bmpTemp.Width; i += 3)
                for(int j = 0; j < bmpTemp.Height; j += 3)
                    if(IsBlack(bmpTemp.GetPixel(i, j)))
                    {
                        lC.Add(CalcCircleCenter(i, j, bmpTemp));
                    }

            FindObstacles(bmpAnalyse);
            //ShortestArista();
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
            //DrawCenter(pCent);
            return tempCircle;
        }
        private void ShortestArista()
        {
            gBmp = Graphics.FromImage(bmpAnalyse);
            Vertex orgMin;
            Edge ariMin;
            try
            {
                orgMin = lC[0];
                ariMin = lC[0].getLA()[0];
            } catch(Exception)
            {
                return;
            }
            foreach(Vertex cir in lC)
            {
                foreach(Edge ari in cir.getLA())
                {
                    if(ari.GetPonderacion() < ariMin.GetPonderacion())
                    {
                        orgMin = cir;
                        ariMin = ari;
                    }
                }
            }
            gBmp.DrawLine(new Pen(Color.Pink, 2), orgMin.GetX(), orgMin.GetY(), ariMin.GetOrigen().GetX(), ariMin.GetOrigen().GetY()); //dibujar menor dis
        }

        //Act 2
        private void FindObstacles( Bitmap bmp )
        {
            double distAct, distMin;
            int contAris = 0;

            Graphics gBmp_2 = Graphics.FromImage(bmp);
            bmpTemp = new Bitmap(bmp);

            Vertex p_0, p_f, ori, des;
            Pen penBlck, penYllw;
            List<Point> lP, lPR;

            try
            {
                p_0 = lC[0];
                p_f = lC[1];
            } catch(Exception)
            {
                return;
            }
            penBlck = new Pen(Color.Black, 4);
            penYllw = new Pen(Color.Yellow, 2);

            distMin = GetDist(p_0, p_f);

            for(var i = 0; i < lC.Count; i++)
                for(var j = i; j < lC.Count; j++)
                {
                    ori = lC[i];
                    des = lC[j];
                    distAct = GetDist(ori, des);

                    if(ori != des)
                    {
                        lP = new List<Point>();
                        if(DDA(ori, des, bmpTemp, lP))
                        {
                            ori.AddArista(ori, des, distAct, ++contAris, lP);
                            lPR = new List<Point>(lP);
                            lPR.Reverse();
                            des.AddArista(des, ori, distAct, ++contAris, lPR);
                            gBmp_2.DrawLine(penBlck, ori.GetX(), ori.GetY(),
                                                   des.GetX(), des.GetY());
                        }
                    }
                    if(distAct < distMin && distAct > 0)
                    {
                        distMin = distAct;
                        p_0 = ori;
                        p_f = des;
                    }
                }
            gBmp_2.DrawLine(penYllw, p_0.GetX(), p_0.GetY(), p_f.GetX(), p_f.GetY()); //dibujar menor dis
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
            tv.Nodes.Add("Agente("
                + agn.GetId() + "), Del vertice "
                + firstA.GetOrigen().GetId() + " al vertice "
                + lastA.GetDestino().GetId() + "\n Distancia total: " +
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

            if(lC.Count > 1)
            {
                DeepAnimation(ARM.EdgL[0].GetOrigen(), visited, ARM);
            }
        }

        //Act 4
        public void DeepAnimation( Vertex vertex, List<Vertex> visited, Graph Tree )
        {
            if(!visited.Contains(vertex))
            {
                visited.Add(vertex);
                for(int i = 0; i < Tree.EdgL.Count; ++i)
                {
                    Edge edgeA = Tree.EdgL[i];
                    if(vertex == edgeA.GetOrigen() && !visited.Contains(edgeA.GetDestino()))
                    {
                        lPR = new List<Point>(edgeA.GetLP());
                        AnimateEdge(bmpARM, lPR);
                        DeepAnimation(edgeA.GetDestino(), visited, Tree);//diff-
                        lPR = edgeA.GetLP();
                        lPR.Reverse();//diff
                        if(visited.Count <= Tree.EdgL.Count)
                        {
                            AnimateEdge(bmpARM, lPR);
                        }
                    } else if(vertex == edgeA.GetDestino() && !visited.Contains(edgeA.GetOrigen()))
                    {
                        lPR = new List<Point>(edgeA.GetLP());
                        lPR.Reverse();
                        AnimateEdge(bmpARM, lPR);
                        DeepAnimation(edgeA.GetOrigen(), visited, Tree);//diff-
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

            lAgents.Sort(CompLAg);

            foreach(Agent A in lAgents)
            {//por cada agente en ListaAgentes
                lPU = A.GetLPU();
                lLP.Add(lPU);
            }
            DrawAllAgents(lLP);
        }
        private void AnimateEdge( Bitmap bmp, List<Point> lP )
        {
            //new Thread(() => {
            //    lock(this)
            //    {
            for(int i = 0; i < lP.Count - 10; i += 10)
            {
                DrawAgent(lP[i], whiteBrsh, bmp);
                DrawAgent(lP[i + 1], orangeBrsh, bmp);
                pbImg.Refresh();
                ClearBitmap(bmp);
            }
            //    }
            //}).Start();
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
                    if(i < lP.Count - 6)
                    {
                        DrawAgent(lP[i], whiteBrsh, bmpAnalyse);
                        DrawAgent(lP[i + 1], orangeBrsh, bmpAnalyse);
                    }
                }
                pbImg.Refresh();
                ClearBitmap(bmpAnalyse);
                i += 6;
            }
        }

        //Act 5
        public void AnimateAllAgents( Bitmap bmp, List<Agent> agentL )
        {
            bmpBack = new Bitmap(bmp);
            pbImg.BackgroundImage = bmpBack;
            pbImg.BackgroundImageLayout = ImageLayout.Zoom;
            pbImg.Image = bmp;

            int j = 0, k = 0, i = 0;
            Point currP, lastP;
            Edge currE;
            Agent agn;

            //TODO: Especificar la(s) presa(s)//
            while(agentL.Count > 0)
            {
                j += 15;//Avanzo punto (+15)
                k++;//Avanzo arista

                for(i = 0; i < agentL.Count; i++)
                {
                    agn = agentL[i];//Agente actual
                    currE = agn.CurrEdge;//Arista actual
                    currP = agn.CurrPoint;//Punto actual
                    lastP = currE.GetLP()[0];//Ultimi punto de esa arista
                                             //^^^//
                    if(currE.GetLP().Contains(lastP))//Punto en que se encuentra de la arista
                    {
                        if(j < agn.LP.Count)//Total de todos lo puntos del Grafo
                        {
                            // [Checar (solo si presa) si choco con algun deprededor]
                            // [Sacar distancia con depredador]
                            // [Si no estoy en vertice, y dis es menor a diametro, destruir agente]
                            agn.CurrPoint = agn.LP[j];//Tomo el siguiente punto en la lista
                        } else
                            agentL.Remove(agn);//Si llegue al final, elimino agente
                    } else
                    {
                        if(k < agn.Tree.EdgL.Count)//Total vertices Grafo
                        {
                            //  [Checar antes de avanzar si hay un depredaror en la sig arista]
                            //      [Si no hay - Avanzar]
                                        agn.CurrEdge = agn.Tree.EdgL[k];//Tomo la siguiente arista
                            //      [Si hay alguien - Regresarse]
                        }
                    }
                    if(j - 1 < agn.LP.Count)//Si el sig punto no sale de los indices
                        DrawAgent(agn.LP[j - 1], whiteBrsh, bmp);
                    DrawAgent(agn.CurrPoint, orangeBrsh, bmp);
                }

                pbImg.Refresh();
                ClearBitmap(bmp);
            }
        }
        private bool IsInVertex( Agent agn )
        {

            return true;
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
        private void DrawAgent( Point p, Brush brush, Bitmap bmp )
        {
            gBmp = Graphics.FromImage(bmp);
            gBmp.FillEllipse(brush, p.X - 15, p.Y - 15, 30, 30);
        }
        private void DrawIds()
        {
            Font myFont = new Font("Arial", 14);
            gBmp = Graphics.FromImage(bmpAnalyse);

            for(int i = 0; i < lC.Count; i++)
            {
                gBmp.DrawString(lC[i].GetId().ToString(), myFont, whiteBrsh,
                    lC[i].GetX() - 8,
                    lC[i].GetY() - 9);
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
        public List<Vertex> GetLV()
        {
            return lC;
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
    }
}