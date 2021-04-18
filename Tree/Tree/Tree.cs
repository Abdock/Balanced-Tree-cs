using System;

namespace Tree
{
    public class Tree
    {
        private class Node
        {
            public readonly int Data;
            public int Height;
            public Node Left;
            public Node Right;
            public Node Parent;

            public Node(int data)
            {
                Data = data;
                Height = 1;
            }

            public Node(int data, Node parent)
            {
                Height = 1;
                Data = data;
                Parent = parent;
            }
        }

        private Node _root;
        public int Count { get; set; }

        public Tree()
        {
            Count = 0;
            _root = null;
        }

        public Tree(int data)
        {
            _root = new Node(data);
            Count = 1;
        }

        public void Add(int data)
        {
            if (_root == null)
            {
                _root = new Node(data);
            }
            else
            {
                Add(_root, data);
            }
            ++Count;
        }

        private void LeftRotation(Node node)
        {
            Node right = node.Right;
            right.Parent = node.Parent;
            node.Right = right.Left;
            if (right.Left != null)
            {
                right.Left.Parent = node;
            }
            right.Left = node;
            node.Parent = right;
            FixHeight(node);
            FixHeight(right);
            if (right.Parent == null)
            {
                _root = right;
            }
            else
            {
                if (right.Parent.Left == node)
                {
                    right.Parent.Left = right;
                }
                else
                {
                    right.Parent.Right = right;
                }
            }
        }

        private void RightRotation(Node node)
        {
            Node left = node.Left;
            left.Parent = node.Parent;
            node.Left = left.Right;
            if (left.Right != null)
            {
                left.Right.Parent = node;
            }
            left.Right = node;
            node.Parent = left;
            FixHeight(node);
            FixHeight(left);
            if (left.Parent == null)
            {
                _root = left;
            }
            else
            {
                if (left.Parent.Left == node)
                {
                    left.Parent.Left = left;
                }
                else
                {
                    left.Parent.Right = left;
                }
            }
        }

        private static void FixHeight(Node node)
        {
            node.Height = Math.Max(node.Left?.Height ?? 0, node.Right?.Height ?? 0) + 1;
        }

        private static int CheckHeight(Node node)
        {
            int left = node.Left?.Height ?? 0;
            int right = node.Right?.Height ?? 0;
            return right - left;
        }

        private void Balance(Node node)
        {
            FixHeight(node);
            if (CheckHeight(node) == -2)
            {
                if (CheckHeight(node.Left) > 0)
                {
                    LeftRotation(node.Left);
                }
                RightRotation(node);
            }
            else if (CheckHeight(node) == 2)
            {
                if (CheckHeight(node.Right) < 0)
                {
                    RightRotation(node.Right);
                }
                LeftRotation(node);
            }
        }

        private void Add(Node node, int data)
        {
            if (node.Data > data)
            {
                if (node.Left != null)
                {
                    Add(node.Left, data);
                }
                else
                {
                    node.Left = new Node(data, node);
                }
            }
            else
            {
                if (node.Right != null)
                {
                    Add(node.Right, data);
                }
                else
                {
                    node.Right = new Node(data, node);
                }
            }
            Balance(node);
        }

        public bool Contains(int data)
        {
            return Contains(_root, data);
        }

        private bool Contains(Node node, int data)
        {
            if (node.Data == data)
            {
                return true;
            }
            if (node.Data > data && node.Left != null)
            {
                return Contains(node.Left, data);
            }
            return node.Right != null && Contains(node.Right, data);
        }
        
        public void Remove(int data)
        {
            Remove(_root, data);
        }

        private void Remove(Node node, int data)
        {
            if (node.Data == data)
            {
                if (node.Right == null)
                {
                    node = null;
                }
                else
                {
                    Node swap = node.Right;
                    while (swap.Left != null)
                    {
                        swap = swap.Left;
                    }

                    if (swap.Parent != node)
                    {
                        swap.Parent.Left = null;
                    }
                    if (node.Parent.Left == node)
                    {
                        node.Parent.Left = swap;
                    }
                    else
                    {
                        node.Parent.Right = swap;
                    }
                    swap.Parent = node.Parent;
                    swap.Left = node.Left;
                    swap.Right = node.Right;
                    if (swap.Left.Parent == node)
                    {
                        swap.Left.Parent = swap;
                    }
                    if(swap.Right.Parent == node)
                    {
                        swap.Right.Parent = swap;
                    }
                    node = swap;
                }
            }
            else if (node.Data > data)
            {
                if (node.Left != null)
                {
                    Remove(node.Left, data);
                }
            }
            else
            {
                if (node.Right != null)
                {
                    Remove(node.Right, data);
                }
            }
            Balance(node);
        }
    }
}
