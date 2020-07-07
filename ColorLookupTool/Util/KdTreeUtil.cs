using Supercluster.KDTree;
using System;
using System.Linq;

namespace ColorLookupTool.Util
{
    public class KdTreeUtil
    {
        private KDTree<double, KdColorNode> lookupTree;

        private static Func<double[], double[], double> metric = (x, y) => {
            double dist = 0;
            for (int i = 0; i < x.Length; i++)
            {
                dist += (x[i] - y[i]) * (x[i] - y[i]);
            }
            return dist;
        };

        public KdTreeUtil(params KdColorNode[] nodes)
        {
            var points = nodes.Select(node => node.color).ToArray();
            lookupTree = new KDTree<double, KdColorNode>(3, points, nodes, metric);
        }

        public KdColorNode NearestMatch(double[] color)
        {
            var match = lookupTree.NearestNeighbors(color, 1);

            if (match.Length > 0) return match[0].Item2;
            else return null;
        }
    }

    public class KdColorNode {

        public string colorName;
        public double[] color;
        
        public KdColorNode(string colorName, double[] color)
        {
            this.colorName = colorName;
            this.color = color;
        }
    }
}
