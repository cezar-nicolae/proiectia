using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_si_0.Game;

namespace X_si_0.Algorithm
{
    class TreeNode
    {
        public Board board;
        public bool is_terminal;
        public bool is_fully_expanded;
        public TreeNode parent;
        public int visits;
        public int score;
        public Dictionary<String, TreeNode> children;
        public TreeNode(Board boardArg, TreeNode parentArg)
        {
            board = boardArg;

            if(board != null && (board.is_win() || board.is_draw()))
            {
                is_terminal = true;
            }
            else
            {
                is_terminal = false;
            }

            is_fully_expanded = is_terminal;

            parent = parentArg;

            visits = 0;

            score = 0;

            children = new Dictionary<String, TreeNode>();
        }
    }
}
