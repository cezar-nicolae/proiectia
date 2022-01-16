/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_si_0.Game;

namespace X_si_0.Algorithm
{
    class MTCS
    {
        public TreeNode search(Board initial_state)
        {
            TreeNode root = new TreeNode(initial_state, null);
            // Cred ca asta e valoarea pe care o sa o dai tu de acolo din interfata
            int iterations = 1000;

            for(int i = 0; i < iterations; i++)
            {
                TreeNode node = select(root);

                int score = rollout(node.board);

                backpropagate(node, score);
            }

            try
            {
                return get_best_move(root);
            }
            catch(Exception ex)
            {
                // Pus aici doar pt ca latra c#
                return new TreeNode(null, null);
            }
        }

        TreeNode select(TreeNode node)
        {
            //verificam daca nodul e terminal sau nu
            while (!node.is_terminal)
            {
                //verificam daca toata tabla e completa cu x si 0
                if (node.is_fully_expanded)
                {
                    node = get_best_move(node);
                }
                else
                {
                    //daca tabla nu e completa expandam
                    return expand(node);
                }
            }

            return node;
        }

        TreeNode expand(TreeNode node)
        {
            List<Board> states = node.board.generate_states();
            //se expandeaza un nod cu toate mutarile posibile
            foreach(Board state in states)
            {
                // Nici aici nu sunt sigur ca e ok
                // Ideea e gen sa verifici daca mutarea asta deja exista sau nu
                //if(!node.children.ContainsKey(state.board.ToString()))
                //{
                    TreeNode new_node = new TreeNode(state, node);
                    // Aici iar e posibil sa fie o problema
                    node.children[state.board.ToString()] = new_node;

                    if(states.Count == node.children.Count)
                    {
                        node.is_fully_expanded = true;
                    }

                    return new_node;
                //}    
            }

            //Nu ar trb sa ajunga aici, e doar pusa sa nu mai latre C# la mine
            return new TreeNode(null, null);
        }

        int rollout(Board board)
        {
            Random random = new Random();

            while(!board.is_win())
            {
                try
                {
                    //se efectueaza o mutare random
                    List<Board> temp = board.generate_states();
                    board = temp[random.Next(temp.Count)];
                }
                catch(Exception ex)
                {
                    return 0;
                }
            }
            
            //Aici iar vine chestia aia ciudata cu player 2 egal cu x sau cu 0 
            return -1;
        }

        void backpropagate(TreeNode node, int score)
        {
            // Aici e posibil sa nu fie ok cu null-ul asta
            while(node != null)
            {
                node.visits += 1;
                node.score += score;
                node = node.parent;
            }
        }

        //aplicam functia UCB ca sa vedem care e cea mai buna miscare
        TreeNode get_best_move(TreeNode node)
        {
            double best_score = Single.NegativeInfinity;
            List<TreeNode> best_moves = new List<TreeNode>();
            Random random = new Random();

            foreach(TreeNode child_node in node.children.Values)
            {
                // Aici nu inteleg exact de ce e -1, presupun eu ca e mereu -1 pt ca la noi player-ul 2 e mereu cu 0
                // Incearca sa stergi -1 sa vezi daca mai merge
                double move_score =   child_node.score / child_node.visits +   Math.Sqrt(2* Math.Log(node.visits / child_node.visits));

                if(move_score > best_score)
                {
                    best_score = move_score;
                    best_moves.Clear();
                    best_moves.Add(child_node);
                }  
                

                //sincer nu stiu daca ma ajuta cu ceva else if ul pt ca noi oricum o sa facem doar 1 mutare nu mai multe. cel putin asa cred. 

                else if(move_score == best_score)
                {
                    best_moves.Add(child_node);
                }
            }

            return best_moves[0];
        }
    }
}


*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_si_0.Game;

namespace X_si_0.Algorithm
{
    class MTCS
    {
        public TreeNode search(Board initial_state)
        {
            TreeNode root = new TreeNode(initial_state, null);
            // Cred ca asta e valoarea pe care o sa o dai tu de acolo din interfata
            int iterations = 1000;

            for (int i = 0; i < iterations; i++)
            {
                TreeNode node = select(root);

                int score = rollout(node.board);

                backpropagate(node, score);
            }

            try
            {
                return get_best_move(root);
            }
            catch (Exception ex)
            {
                // Pus aici doar pt ca latra c#
                return new TreeNode(null, null);
            }
        }

        TreeNode select(TreeNode node)
        {
            while (!node.is_terminal)
            {
                if (node.is_fully_expanded)
                {
                    node = get_best_move(node);
                }
                else
                {
                    return expand(node);
                }
            }

            return node;
        }

        TreeNode expand(TreeNode node)
        {
            List<Board> states = node.board.generate_states();

            foreach (Board state in states)
            {
                // Nici aici nu sunt sigur ca e ok
                // Ideea e gen sa verifici daca mutarea asta deja exista sau nu
                //if(!node.children.ContainsKey(state.board.ToString()))
                //{
                TreeNode new_node = new TreeNode(state, node);
                // Aici iar e posibil sa fie o problema
                node.children[state.board.ToString()] = new_node;

                if (states.Count == node.children.Count)
                {
                    node.is_fully_expanded = true;
                }

                return new_node;  
            }

            //Nu ar trb sa ajunga aici, e doar pusa sa nu mai latre C# la mine
            return new TreeNode(null, null);
        }

        int rollout(Board board)
        {
            Random random = new Random();

            while (board != null && !board.is_win())
            {
                try
                {
                    List<Board> temp = board.generate_states();
                    board = temp[random.Next(temp.Count)];
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }

            //Aici iar vine chestia aia ciudata cu player 2 egal cu x sau cu 0 
            return -1;
        }

        void backpropagate(TreeNode node, int score)
        {
            // Aici e posibil sa nu fie ok cu null-ul asta
            while (node != null)
            {
                node.visits += 1;
                node.score += score;
                node = node.parent;
            }
        }

        TreeNode get_best_move(TreeNode node)
        {
            double best_score = -10000;
            List<TreeNode> best_moves = new List<TreeNode>();
            Random random = new Random();

            foreach (TreeNode child_node in node.children.Values)
            {
                // Aici nu inteleg exact de ce e -1, presupun eu ca e mereu -1 pt ca la noi player-ul 2 e mereu cu 0
                // Incearca sa stergi -1 sa vezi daca mai merge
                double move_score =  child_node.score / child_node.visits +  Math.Sqrt(2*Math.Log(node.visits) / child_node.visits);

                if (move_score > best_score)
                {
                    best_score = move_score;
                    best_moves.Clear();
                    best_moves.Add(child_node);
                }
                else if (move_score == best_score)
                {
                    best_moves.Add(child_node);
                }
            }

            return best_moves[random.Next(best_moves.Count)];
        }
    }
}

