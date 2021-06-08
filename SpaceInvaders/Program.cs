using SpaceInvaders.ECSFramework;
using SpaceInvaders.ECSFramework.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SpaceInvaders
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameForm());

            //Engine en = new Engine();
            //Entity t = new Entity();
            //t.AddComponent(new Position());
            //t.AddComponent(new Movement());
            //en.AddEntity(t);
            //en.MatchComponent<DeplacementNode>();
            //en.MatchComponent<DeplacementNode>();
        }
    }
}
